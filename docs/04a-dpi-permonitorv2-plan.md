# DPI Awareness — Current State & Plan for PerMonitorV2 Support

Plan for upgrading the app from `DpiUnaware` (current workaround) to `PerMonitorV2`, with a `SystemAware` mid-phase checkpoint to enable partial rollback if the OpenGL context-survival question proves unresolvable.

---

## Why this doc exists

Before the OpenGL migration (`OrbitPanel : Panel` with GDI+), the app had no explicit DPI configuration. No `Application.SetHighDpiMode` call, no app manifest, no `<ApplicationHighDpiMode>` csproj property. .NET Framework and .NET 8 without explicit opt-in both leave the process **DPI-unaware**, meaning Windows bitmap-scales the whole window when it appears on a non-96-DPI monitor. That produced a consistent if slightly-blurry experience across monitors — "worked on all monitors" from the user's perspective.

After the OpenGL migration (`OrbitPanel : GLControl`, OpenTK 4.x), the process DPI awareness was being flipped to `PER_MONITOR_AWARE_V2` mid-run by GLFW (OpenTK's native windowing dependency) as soon as the first GL context was created. Forms created before that call were laid out under one awareness context, the OrbitViewer under another, and nothing auto-scaled between monitors — resulting in broken layouts, oversized labels on the 200% monitor, and visible menu shrinkage when the main window crossed monitors.

**Current workaround** (`src/Comets.Application/Program.cs:15`):

```csharp
Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
```

This locks the process to DPI-unaware before GLFW can change it. GLFW's later attempt is a silent no-op. Windows bitmap-scales everything. Behaviour matches pre-branch exactly — but the OpenGL viewport renders at 96-DPI logical resolution and is stretched up on the 4K monitor, wasting pixel density and softening lines.

**Target: `PerMonitorV2`.** Each monitor renders at native resolution, with DPI changes handled via `WM_DPICHANGED`. Benefits:
- OrbitViewer 3D view renders crisp on both the 100% and 200% monitors
- WinForms chrome / text crisp on both monitors
- No bitmap-scale blur when dragging the window between monitors
- Matches the behaviour of VS, Chrome, and other modern Windows apps

`SystemAware` is used as a mid-phase checkpoint: it proves the designer-file audit and static OrbitPanel sizing work correctly before tackling the harder reactive layer. If PerMonitorV2 runs aground (most likely on GLControl context survival — see Phase 6), the SystemAware checkpoint is a shippable fallback.

---

## Current state (quick reference)

**Process-wide:**
- `Program.cs:15` — `Application.SetHighDpiMode(HighDpiMode.DpiUnaware)`
- No app.manifest, no `<ApplicationHighDpiMode>` in any csproj

**All 36 designer files use the same pattern:**
- `AutoScaleMode = AutoScaleMode.Font`
- `AutoScaleDimensions = new SizeF(6F, 13F)` (34 files) or `SizeF(7F, 15F)` (2 files: `FormGraph`, `ModeControl`)
- These numbers are MS Sans Serif / Segoe UI default glyph cell heights at 96 DPI; when the runtime font differs from the assumed design-time font, scaling factors go subtly wrong

**Forms' own font settings:**
- `FormMain` — `Tahoma 8.25F` (see `FormMain.Designer.cs:410`)
- Other forms/controls — designer default (Segoe UI on modern Windows), not explicitly set

**OrbitPanel (`src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs`):**
- Inherits from `OpenTK.GLControl` — transitively loads GLFW
- Fonts are created in `new Font("Helvetica", 10, ...)` etc. in **point units** (default) — scale with DPI via GDI+ when `Graphics.FromImage(bmp)` is created
- Label overlay (`RenderLabels`, line 1060) uses `new Bitmap(Width, Height, ...)` where `Width`/`Height` are the control's client-area size
- `GL.Viewport(0, 0, Width, Height)` at lines 401, 417 — same units assumption
- NDC → screen projection at lines 874-875 uses `Width`/`Height`
- Axis label point-size calc at 1021: `float pxSize = _orthoHalfH / (Height > 0 ? Height / 2f : 1f);`
- No `OnDpiChanged*` overrides anywhere in the app

**Other DPI-touchy spots:**
- `EphemerisManager.cs:275-386` — MS Chart fonts hardcoded `new Font("Tahoma", 8.25F)` without `GraphicsUnit.Point` explicit (defaults to point, so DPI-scaled)
- `FilterPanelManager.cs:242` — `new Font("Tahoma", 8.25F, ..., GraphicsUnit.Point, 238)` — explicit points, OK

No code anywhere calls `ScaleControl`, `OnDpiChanged`, `ScaleChildren`, `PerformAutoScale`, or `LogicalToDeviceUnits`.

---

## Why `SystemAware` didn't "just work" when we tried it

Two independent problems surfaced:

### 1. WinForms AutoScale produces wrong dimensions

With `AutoScaleMode.Font` and `AutoScaleDimensions = 6F, 13F`, WinForms computes `scaleFactor = currentFontHeight / designFontHeight`. Under SystemAware on the 200% monitor:
- The runtime font is Segoe UI 9pt (default) at 192 DPI — current height ≈ 20–24px
- The design-time assumption is MS Sans Serif 8pt at 96 DPI — height 13px
- Scale factor ≈ 1.5–1.8× applied to already-designer-sized controls

The designer file was built at 96 DPI with Segoe UI implicitly. When the process is DPI-unaware, it always sees 96 DPI, designer values round-trip cleanly. When it sees 192 DPI, the font-based auto-scale multiplies by the wrong ratio because design-time AutoScaleDimensions are stale.

### 2. OrbitPanel rendering dimensions weren't DPI-corrected

`GLControl.Width`/`.Height` are in logical pixels. The overlay bitmap was allocated at logical-pixel size, but fonts inside it rasterize at point-size physical dimensions (96 DPI of the bitmap, not the surface). Text came out visibly wrong-sized on the 200% monitor, compounded by the already-wrong WinForms auto-scale.

Both problems are fixable with a proper foundation pass — that's what Phase 1 and 2 address.

---

## Target end-state (PerMonitorV2)

- Process DPI awareness: `PerMonitorV2`
- All forms/controls lay out cleanly on both 100% and 200% monitors at startup, regardless of which is primary
- OrbitViewer renders at native resolution on whichever monitor it's on
- Moving a window between monitors: WinForms re-scales forms via `WM_DPICHANGED`, OrbitPanel reallocates framebuffer and overlay bitmap at new resolution, no visible bitmap-scaling blur
- OpenGL context survives DPI changes (either via HWND preservation or state reload)
- MDI container handles straddling-monitors case without layout drift
- Dark mode and MS Chart controls verify-clean

---

## Implementation phases

Phases 1-4 reach the **SystemAware checkpoint**. At that point the app is committable and shippable — better than today even though we haven't reached the final target. Phases 5-7 go from checkpoint to full PerMonitorV2.

### Phase 1 — Designer-file audit and font baseline

**File:** `src/Comets.Application/Program.cs`

Change line 15 from:
```csharp
Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
```
to:
```csharp
Application.SetHighDpiMode(HighDpiMode.SystemAware);
```

**Files:** all 36 `*.Designer.cs`

Make every top-level form explicitly set `this.Font` to a single consistent value, e.g. `Segoe UI 9F` (Windows default) or keep `Tahoma 8.25F` — but be consistent across all forms. Don't leave it implicit: the designer-emitted AutoScale factors depend on the runtime font matching the design-time font.

After changing `Font`, open each form in the VS designer to re-emit `AutoScaleDimensions`. The designer writes the correct pair for the current font; this silently fixes the `6F, 13F` / `7F, 15F` inconsistency. Do *not* hand-edit the numbers — let the designer regenerate them from a clean save.

Commit designer changes in small batches (e.g. per project) so regressions are bisectable.

### Phase 2 — OrbitPanel physical-pixel sizing (static DPI)

**File:** `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs`

Under SystemAware *and* PerMonitorV2, `Control.Width` / `Control.Height` report *logical* pixels. OpenGL's `GL.Viewport` and the overlay `Bitmap` must use **physical** pixels or the GL surface will be up-scaled by Windows.

Compute physical dimensions from `DeviceDpi`:

```csharp
float scale = DeviceDpi / 96f;
int pxW = (int)(Width * scale);
int pxH = (int)(Height * scale);
```

Change in this order:
1. `OnResize` (line 395) — `GL.Viewport(0, 0, pxW, pxH)` using scaled dimensions
2. `InitGL` (line 417) — same
3. Aspect ratio calc (line 635) — use `pxW / (float)pxH` (same ratio, but keep types consistent)
4. NDC → screen pixel projection (lines 874-875) — multiply by `pxW` / `pxH`, then divide by `scale` when placing WinForms child controls since those use logical coordinates (see `UpdateCometPanelLocations`, line 827)
5. `RenderLabels` bitmap allocation (line 1064) — `new Bitmap(pxW, pxH, ...)`
6. Label positioning inside the bitmap (lines 1086-1100) — all coordinates and margins need `* scale`
7. Axis-label point-size calc (line 1021) — divide `_orthoHalfH` by `pxH`, not `Height`
8. `glTexImage2D` upload (line 1181) — `pxW`, `pxH`

Fonts inside `RenderLabels` rasterize at bitmap DPI. Force it:

```csharp
bmp.SetResolution(96f * scale, 96f * scale);
```

This makes GDI+ render `new Font("Consolas", 10, Bold)` at point size 10 scaled for the bitmap's pixel density, matching the logical size the user sees.

### Phase 3 — Mouse coordinate audit

WinForms delivers mouse coordinates in logical pixels. With the GL viewport now in physical pixels, hit-testing that projects world coordinates to screen and then compares to mouse X/Y must do so in the *same* coordinate space. Either:
- Keep all mouse math in logical pixels; convert only at the GL viewport boundary, *or*
- Keep all mouse math in physical pixels; multiply incoming `e.X`/`e.Y` by `scale`

Grep for `PointToClient`, `MouseEventArgs`, `e.X`, `e.Y` in `OrbitPanel.cs` and `OrbitViewerControl.cs` and pick one convention consistently. Logical-pixel convention is simpler for label placement; physical-pixel convention is simpler for GL picking. Pick one and document it.

### Phase 4 — Verification of MS Chart, dark mode, toolbox

No code change expected — these need eyes-on verification after Phase 1+2:

- **`src/Comets.Core/Managers/EphemerisManager.cs` (lines 275-386).** Chart fonts are built as `new Font("Tahoma", 8.25F)` without explicit `GraphicsUnit` — defaults to Points, so GDI+ scales them. Confirm axis label overlap/clipping isn't introduced.
- **`src/Comets.Application.Common/External/DarkMode/DarkModeCS.cs` (line 147 comment).** DWM border widths depend on DPI. Verify dark title bar renders at correct thickness on both monitors.
- **`src/Comets.Application.OrbitViewer/Controls/Toolbox/*.cs` (CollapsiblePanel + 8 toolbox controls).** These compound controls have hand-computed `Location`/`Size` in code-behind. Any hardcoded `Point(10, 20)`-style positioning should be wrapped with `LogicalToDeviceUnits(new Point(10, 20))` — the built-in helper that's a no-op under DpiUnaware, a constant scale under SystemAware, and correct-per-monitor under PerMonitorV2.

---

### 🏁 **SystemAware checkpoint — commit here**

At this point the app:
- Fixes all the broken layouts under the current DpiUnaware workaround's replacement
- Renders OrbitViewer at native resolution on the primary monitor
- Bitmap-scales when dragged to the secondary monitor (acceptable, matches most WinForms apps)

If Phases 5-7 run aground on the GLControl context-survival question, this checkpoint is the shippable fallback. Tag it.

---

### Phase 5 — Flip to PerMonitorV2 and handle WM_DPICHANGED in forms

**File:** `src/Comets.Application/Program.cs`

```csharp
Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
```

WinForms handles most of `WM_DPICHANGED` automatically: on a DPI change, each form scales its controls by `newDpi / oldDpi` using the `AutoScaleMode.Font` machinery from Phase 1. That's why the designer-file audit had to come first — broken AutoScale values produce broken DPI-change results.

**Per-form work** — override `OnDpiChanged` on top-level forms that have custom-drawn content or hand-computed layouts that won't auto-scale:

- `FormMain.cs` — MDI container (see Phase 7 for the MDI specifics)
- `FormOrbitViewer.cs` — hosts the OrbitPanel (see Phase 6)
- `FormGraph.cs` — hosts MS Chart (probably needs no override; MS Chart is DPI-aware)

Standard override skeleton:

```csharp
protected override void OnDpiChanged(DpiChangedEventArgs e)
{
    base.OnDpiChanged(e);
    // Any custom rescaling of non-AutoScaled members goes here
}
```

Most forms won't need this. Find the ones that do by testing (Phase 8).

### Phase 6 — OrbitPanel DPI reactivity (the hard part)

**File:** `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs`

On DPI change, OrbitPanel must:
1. Reallocate the overlay bitmap at the new physical size (easy — just replace the `using var bmp = new Bitmap(...)` the next frame; it's per-frame anyway)
2. Reset `GL.Viewport` to the new physical size (easy — happens in `OnResize`, which fires after DPI change)
3. Recompute the axis-label point-size formula (easy — derives from `pxH` automatically)
4. **Keep the OpenGL context alive across the DPI change** (hard)

The last one is the unresolved risk. Under PerMonitorV2, WinForms may recreate the HWND when DPI changes to apply new window-frame metrics. OpenTK.GLControl 4.0.2 creates the GL context against the original HWND; HWND recreation destroys the context. All shaders, VBOs, textures, the body VAO, the text quad VAO, and the text texture would need to be recreated from scratch.

**Mitigation options, in order of preference:**

1. **Suppress HWND recreation.** Override `OnDpiChangedAfterParent` (or `WndProc` to intercept `WM_DPICHANGED_AFTERPARENT`) and return without triggering the default handle-recreation path. Relies on WinForms internals being cooperative.
2. **Reload all GL state on DPI change.** Store shader source / VBO data / texture sources in-memory, override `OnHandleDestroyed` to set a `_reloadNeeded` flag, rebuild everything in the next `OnHandleCreated` or first `OnPaint`. Guaranteed to work, expensive on the stutter of one frame during the crossing, acceptable for user experience.
3. **Switch to `OpenTK.WinForms` 4.0.0-pre.8.** Referenced in `docs/01b-opengl-implementation.md` but not actually installed — current csproj uses `OpenTK.GLControl 4.0.2`. Some forum reports suggest better DPI handling; others suggest it's worse. Investigate only if options 1 and 2 both fail.

**Recommended approach:** start with option 2 (state reload). It's the most robust and doesn't depend on GLControl internals. If the per-frame stutter is visible, optimise by caching shader binaries / texture sources so reload is near-instant. A crossing-monitors event is a rare interactive moment; a single dropped frame there is imperceptible.

**Additional code to add:**

```csharp
protected override void OnDpiChangedAfterParent(EventArgs e)
{
    base.OnDpiChangedAfterParent(e);
    // Viewport and bitmap pick up new size automatically via OnResize chain
    Invalidate();
}
```

Confirm `OnResize` fires after `OnDpiChangedAfterParent` — if not, call `GL.Viewport(0, 0, pxW, pxH)` directly with the freshly-recomputed `scale`.

### Phase 7 — MDI container behaviour

**File:** `src/Comets.Application/Application/FormMain.cs`

MDI child windows don't get per-monitor DPI — they always inherit the MDI parent's DPI context. When `FormMain` straddles two monitors:
- Windows picks the DPI of the monitor with greater overlap
- `WM_DPICHANGED` fires on boundary crossings
- MDI children receive the parent's new DPI, not their own per-monitor DPI

Known WinForms issues with MDI + PerMonitorV2:
- MDI child Z-order and position can drift after DPI change
- MDI child window title bar metrics may get out of sync with parent
- Status strip / menu strip scaling sometimes lags one DPI change behind

Test and add targeted overrides on `FormMain` if drift appears. A simple `PerformLayout()` call in `OnDpiChanged` often fixes ordering issues.

### Phase 8 — Testing matrix

Test for each of: (primary=100%, secondary=200%) and (primary=200%, secondary=100%), and for each of those four scenarios (app started on primary / started on secondary / dragged primary→secondary / dragged secondary→primary).

For each:
- [ ] FormMain opens at correct size, menu bar readable
- [ ] FormEphemeris renders cleanly
- [ ] FormGraph renders cleanly; chart axes readable, no label clipping
- [ ] FormOrbitViewer opens at correct size; toolbox controls not oversized or clipped
- [ ] **OrbitViewer 3D view: orbits render at native monitor resolution on whichever monitor the window is on**
- [ ] **OrbitViewer after dragging between monitors: framebuffer reallocated at new resolution, GL context still valid (no black / solid-color viewport, no GL errors in debug build)**
- [ ] OrbitViewer text overlays crisp, correctly positioned, correctly sized
- [ ] Comet selection markers land on correct orbit point
- [ ] FormDatabase / FormElements / FormSettings / FormFind: layout intact
- [ ] Dark mode on/off: chrome still correct
- [ ] **MDI container straddling boundary: no child-window drift**

---

## Files touched (summary)

| Phase | File | Change |
|---|---|---|
| 1 | `src/Comets.Application/Program.cs` | `DpiUnaware` → `SystemAware` |
| 1 | All 36 `*.Designer.cs` | Explicit Font + re-emit AutoScaleDimensions |
| 2 | `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs` | Physical-pixel viewport, bitmap, label math |
| 3 | `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs` | Mouse coordinate convention |
| 3 | `src/Comets.Application.OrbitViewer/Controls/OrbitViewerControl.cs` | Same, if it forwards mouse events |
| 4 | `src/Comets.Core/Managers/EphemerisManager.cs` | Verify only (likely no change) |
| 4 | `src/Comets.Application.Common/External/DarkMode/DarkModeCS.cs` | Verify only (likely no change) |
| 4 | `src/Comets.Application.OrbitViewer/Controls/Toolbox/*.cs` | Wrap hand-computed positions with `LogicalToDeviceUnits` if any |
| **—** | **SystemAware checkpoint commit** | |
| 5 | `src/Comets.Application/Program.cs` | `SystemAware` → `PerMonitorV2` |
| 5 | `src/Comets.Application/Application/FormMain.cs` | `OnDpiChanged` override if needed |
| 5 | `src/Comets.Application.OrbitViewer/FormOrbitViewer.cs` | `OnDpiChanged` override if needed |
| 6 | `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs` | `OnDpiChangedAfterParent`, GL state reload on HWND recreation |
| 7 | `src/Comets.Application/Application/FormMain.cs` | MDI-specific DPI-change fixups if testing reveals drift |

---

## Risks & notes

- **Designer re-save risk.** Opening designer files in VS to regenerate `AutoScaleDimensions` will also re-emit control `Size`, `Location`, resource references. Review each diff — the designer occasionally reorders control initialization or drops properties it considers "default". Commit Phase 1 designer changes in small batches.
- **GLControl DPI reporting consistency.** OpenTK 4.x's `GLControl` doesn't expose a clean "framebuffer size" property; `ClientSize` under SystemAware/PerMonitorV2 *should* equal the physical backbuffer, but on some driver paths (especially with MSAA — we use `NumberOfSamples = 8`) it has reported logical pixels. Phase 2 implementation should log `DeviceDpi`, `ClientSize`, and actual GL viewport size on first paint and confirm they agree before assuming.
- **GL context survival on HWND recreation** (Phase 6) is the single biggest unknown. Plan the Phase 6 implementation with the state-reload approach from day one. If reload turns out to be imperceptible, ship it. If it's visibly stuttery, optimize by caching shader/texture blobs.
- **OpenTK.WinForms alternative.** Current csproj uses `OpenTK.GLControl 4.0.2`. `OpenTK.WinForms 4.0.0-pre.8` exists but is pre-release and has its own set of reported bugs. Don't swap pre-emptively; swap only as a last resort in Phase 6.
- **Fallback plan.** If Phase 6 (GL context survival) proves unresolvable within reasonable effort, the Phase 4 SystemAware checkpoint is a legitimate end state — better than today's DpiUnaware, and the OrbitViewer is native-resolution on the primary monitor at minimum.
- **MDI + PerMonitorV2 is the second-biggest unknown.** WinForms documentation on MDI behaviour under PerMonitorV2 is sparse, and community reports mix success and failure. Phase 7 is exploratory; the remediation path (manual `PerformLayout` calls, targeted overrides) is known, but it's unclear how many applications of it will be needed.
- **Backward compatibility.** After this change, users on pre-1703 Windows won't see per-monitor scaling — PerMonitorV2 degrades to regular PerMonitor or SystemAware depending on OS version. No action needed.
- **Test on primary=200% specifically.** The previous breakage was worst when the *primary* monitor was the high-DPI one. That's the case to prioritize in both the SystemAware checkpoint validation and the final PerMonitorV2 testing.
