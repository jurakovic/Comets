# Simulation Clock and Camera Navigation — Implementation Notes

How playback advances time and how the keyboard drives the camera. The rendering pipeline is documented separately in `02b-opengl-true-3d-implementation.md`; the grid and axes in `05a-ecliptic-grid-implementation.md`.

All of this lives in `OrbitViewerControl.cs` unless stated otherwise.

---

## Simulation Clock

Playback used to be tick-driven: a 50 ms WinForms timer, one whole time step per tick. That ties the apparent speed to the timer's firing rate, which is neither the requested interval nor stable — a WinForms timer is quantized to the system clock granularity and its `WM_TIMER` messages are delivered only when the message queue is empty, so under load ticks are simply dropped and playback slows down.

It is now elapsed-time driven at roughly 60 fps.

```csharp
private const int TimerIntervalMs = 16;          // ~60 fps, both timers
private const double LegacyTimerIntervalMs = 50; // the rate the step is calibrated against
private const double MaxFrameMs = 100.0;         // stall clamp
```

Each tick:

```csharp
double elapsedMs = Math.Min(_simulationClock.Elapsed.TotalMilliseconds, MaxFrameMs);
_simulationClock.Restart();

double deltaDays = TimeStepJD * (elapsedMs / LegacyTimerIntervalMs) * (IsSimulationForward ? 1.0 : -1.0);
_simulationDateTime = _simulationDateTime.AddDays(deltaDays);
```

`LegacyTimerIntervalMs` preserves the original speed: one whole step per 50 ms, i.e. 20 steps per second, regardless of how often the timer actually fires. Dropping frames now costs smoothness rather than speed.

`MaxFrameMs` caps the frame time. A stall — a modal dialog, a window drag, waking from sleep — would otherwise report an elapsed time measured in seconds and jump the simulation forward by many steps at once. Time beyond the cap is discarded, so a stall costs movement instead of causing a leap. The same clamp is applied to camera navigation.

### `TimeStepJD`

`SetTimeStep` flattens the selected `ATimeSpan` into a single fractional-day figure, because the accumulator has to add a *fraction* of a step per frame and `ATime.ChangeDate` only moves in whole steps:

```csharp
TimeStepJD = timeStep.Year * 365.25
           + timeStep.Month * (365.25 / 12.0)
           + timeStep.Day
           + timeStep.Hour / 24.0;
```

Months and years become mean lengths, so month-stepped playback drifts against calendar month boundaries — acceptable for a continuously moving view, and the *step* buttons still use `ATime.ChangeDate` with the real `ATimeSpan` so single-stepping remains calendar-exact.

`Minute` and `Second` are ignored. The steps offered by `SimulationControl` start at 1 hour, so no reachable step is lost; a sub-hour step added there would evaluate to `TimeStepJD == 0` and freeze playback, so it would need a term here too.

### Display Rounding — `DateLabelOverride`

The accumulator carries a time-of-day that is not on a step boundary, which is correct for the rendered positions but noisy to read. Both readouts are therefore fed a rounded value:

- **Date/time control** — `dateTimeControl.SelectedDateTime` gets the date rounded to the nearest step interval (for sub-day steps) or truncated to midnight (for day-and-larger steps).
- **Panel label** — `OrbitPanel.DateLabelOverride` gets the same rounded `ATime`. The panel's date label prefers the override over `ATime` when one is set.

`DateLabelOverride` is cleared automatically inside the `ATime` setter, so a date set by the user is always shown exactly as given, and only the simulation's own tick can reinstate it. That ordering is load-bearing: the tick handler assigns `SelectedDateTime` first (whose setter feeds the panel the precise `ATime` and clears the override), then sets the override. The setter only invalidates the panel, so the repaint happens after the handler returns, by which point the override is in place.

### Pause, Resume, and External Date Changes

`_simulationDateTime` is the authoritative clock while playback is running; `_simulationDateTimeValid` says whether it can be trusted.

| Event | Effect |
|---|---|
| `StartSimulation` with the flag set | Resume from the accumulator. Reseeding from the date control would discard up to a full step of accumulated time and visibly jump the panel backwards |
| `StartSimulation` with the flag clear | Reseed the accumulator from `SelectedDateTime` |
| Any write to `SelectedDateTime` that is not the simulation's own (`!ValueChangedInternal`) | Clears the flag — the date moved under the simulation, so the accumulator must be reseeded on resume |
| `ChangeSimulationDate` (the step buttons) | Clears the flag explicitly. It is an internal write, but it deliberately moves the clock |
| Out-of-range date | The accumulator is clamped to the in-range value too |

The clamp matters because `FormDateTime.RangeDateTime` only clamps what reaches the date control. Left unclamped, the accumulator drifts further outside the supported range every time playback is run into the limit, and a later run in the opposite direction is spent getting back inside the range instead of moving the visible date.

`_simulationClock` is `Restart()`ed rather than `Start()`ed on resume, so time spent paused is not applied to the first frame after resuming, and `Stop()`ped alongside the timer.

---

## Camera Navigation from the Keyboard

Arrow keys rotate; `+`/`Q` and `-`/`A` zoom. Both are gated on `IsKeyboardNavigation`, which tracks whether the pointer is over the orbit panel.

### Why a Timer

Driving rotation straight from `KeyDown` inherits the keyboard's auto-repeat behaviour: a half-second pause before the second step, then a rate set by the user's Control Panel repeat speed, which varies by more than a factor of ten between machines. A key press now only *starts* `NavigationTimer` (16 ms, same as the simulation timer); the timer does the moving, paced by `_navigationClock` exactly as playback is paced by `_simulationClock`.

`StartNavigation()` returns early when the timer is already running, so the auto-repeat presses that keep arriving while a key is held cannot restart the clock and discard the time since the last frame.

### Why the Keyboard Is Polled

The timer does not read a set of pressed keys maintained by the key events. It asks the keyboard directly, every frame:

```csharp
private static bool IsKeyDown(Keys key) => (GetAsyncKeyState((int)key) & 0x8000) != 0;
```

Two reasons events cannot do this job:

- **A key release is only delivered to the window that holds focus.** Alt-tab away with an arrow key down, let go over there, and no release ever arrives — the camera would keep rotating on a key nobody is holding.
- **Windows auto-repeats only the most recently pressed key.** A second key held alongside it goes silent, so Left+Up could never rotate on both axes at once. Reading the keyboard as a set makes held directions combine naturally.

The held keys are mapped into a `[Flags] enum NavDirection` and resolved into a single rotation and zoom delta per frame. When nothing is held, the timer stops itself. Note that movement is gated on the keys alone and not on the window having focus, so it continues while the application sits in the background and ends when the last key comes up, wherever that happens.

The high bit of `GetAsyncKeyState` is the "currently down" bit; the low bit means "pressed since the last call", which is not wanted here.

### Rates

```csharp
private const double RotateDegreesPerSecond = 60.0;  // full turn in 6 s
private const double ZoomFactorPerSecond    = 2.5;
const double ZoomStepFactor = 1.15;                  // one mouse-wheel notch
```

Rotation is close to the sensitivity of a right-button drag, so the two ways of turning the scene feel alike.

Zoom is **geometric**, matching the wheel. The per-second factor is raised to the power of the elapsed seconds rather than multiplied by them:

```csharp
double factor = Math.Pow(ZoomFactorPerSecond, zoomSign * seconds);
orbitPanel.Zoom = Math.Clamp(orbitPanel.Zoom * factor, ZoomMin, ZoomMax);
```

At 2.5 per second, crossing the whole `ZoomMin`..`ZoomMax` range (1.5 to 5000) takes about nine seconds from wherever it starts. The old additive `Zoom ± 10.0` crawled at high zoom and jumped at low zoom.

### Pointer Gating

| Event | Effect |
|---|---|
| `MouseEnter` | Enables wheel zoom and keyboard navigation. If a navigation key is *already* held, starts the timer immediately — waiting for the next press would resume only the key Windows happens to be auto-repeating and lose any other |
| `MouseLeave` | Disables both and stops the timer. Nothing about the held keys is remembered, because the timer re-derives them from the keyboard on every frame |

### Key Handling in `OrbitViewerControl_KeyDown`

The form previews every key press, so anything claimed there never reaches the control that has focus. Keys a toolbox textbox needs are let past while `filterControl.Focused || miscControl.ContainsFocus`: the digits, `Back`, `Delete` and `Enter`. Without `Enter` and `Delete`, typing in a toolbox textbox would mark a comet and unmark every comet respectively.

The navigation cases fall through to a single `BeginNavigation(ctrl, shift)`, which returns `false` for modified presses so they still reach their own shortcuts. Which direction the key means is not recorded — the timer reads that itself.

### Naming

`IsKeyboardScroll` is now `IsKeyboardNavigation`, and the `DefaultRotateVert`/`DefaultRotateHorz` comments no longer describe themselves in terms of the scroll bars that were removed — `DefaultRotateVert = 70.0` is documented as "from top-down, so 20° above the ecliptic plane" rather than as `90 - DefaultScrollVert(20)`.

---

## Files

| File | Role |
|---|---|
| `src/Comets.Application.OrbitViewer/Controls/OrbitViewerControl.cs` | Simulation clock, navigation timer, key handling |
| `src/Comets.Application.OrbitViewer/Controls/Toolbox/SimulationControl.cs` | The `ATimeSpan` time-step list and play/step/stop events |
| `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs` | `DateLabelOverride` |
