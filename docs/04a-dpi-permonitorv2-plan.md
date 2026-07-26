# DPI Awareness — Current State & Plan for PerMonitorV2 Support

Plan for upgrading the app from `DpiUnaware` (current workaround) to `PerMonitorV2`, with a `SystemAware` mid-phase checkpoint to enable partial rollback if the OpenGL context-survival question proves unresolvable.

> **Revision note.** This plan was re-grounded against the tree at `518ad11`. Several claims in the first draft did not survive contact with the code: the font baseline is already consistent (Phase 1 shrank), the Toolbox hardcoded-position claim was false (that work belongs to `FilterPanelManager`), mouse handling does not live where the plan said it did (Phase 3 shrank), and a certain functional breakage in `CollapsiblePanel` was missed entirely (new Phase 4). All line numbers have been refreshed — the previous draft's references predated the OrbitViewer rework and were off by 300–500 lines.

---

## Why this doc exists

Before the OpenGL migration (`OrbitPanel : Panel` with GDI+), the app had no explicit DPI configuration. No `Application.SetHighDpiMode` call, no app manifest, no `<ApplicationHighDpiMode>` csproj property. .NET Framework and .NET 8 without explicit opt-in both leave the process **DPI-unaware**, meaning Windows bitmap-scales the whole window when it appears on a non-96-DPI monitor. That produced a consistent if slightly-blurry experience across monitors — "worked on all monitors" from the user's perspective.

After the OpenGL migration (`OrbitPanel : GLControl`, OpenTK 4.x), the process DPI awareness was being flipped to `PER_MONITOR_AWARE_V2` mid-run by GLFW (OpenTK's native windowing dependency) as soon as the first GL context was created. Forms created before that call were laid out under one awareness context, the OrbitViewer under another, and nothing auto-scaled between monitors — resulting in broken layouts, oversized labels on the 200% monitor, and visible menu shrinkage when the main window crossed monitors.

**Current workaround** (`src/Comets.Application/Program.cs:16`):

```csharp
System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
```

This locks the process to DPI-unaware before GLFW can change it. GLFW's later attempt is a silent no-op. Windows bitmap-scales everything. Behaviour matches pre-branch exactly — but the OpenGL viewport renders at 96-DPI logical resolution and is stretched up on the 4K monitor, wasting pixel density and softening lines.

**Target: `PerMonitorV2`.** Each monitor renders at native resolution, with DPI changes handled via `WM_DPICHANGED`. Benefits:
- OrbitViewer 3D view renders crisp on both the 100% and 200% monitors
- WinForms chrome / text crisp on both monitors
- No bitmap-scale blur when dragging the window between monitors
- Matches the behaviour of VS, Chrome, and other modern Windows apps

`SystemAware` is used as a mid-phase checkpoint: it proves the layout and static OrbitPanel sizing work correctly before tackling the harder reactive layer. If PerMonitorV2 runs aground (most likely on GLControl context survival — see Phase 7), the SystemAware checkpoint is a shippable fallback.

---

## Current state (quick reference)

**Process-wide:**
- `src/Comets.Application/Program.cs:16` — `Application.SetHighDpiMode(HighDpiMode.DpiUnaware)`
- No app.manifest, no `<ApplicationHighDpiMode>` in any csproj
- `Comets.OrbitViewer.csproj` references `OpenTK.Mathematics 4.9.4`, `OpenTK.Graphics 4.9.4`, `OpenTK.GLControl 4.0.2`

**Designer files — 39 total, 38 with AutoScale settings.** All 38 use `AutoScaleMode.Font`. The split:

| Group | Count | `AutoScaleDimensions` | `this.Font` |
|---|---|---|---|
| Majority | 36 | `SizeF(6F, 13F)` | **explicitly** `Tahoma 8.25F` |
| Outliers | 2 | `SizeF(7F, 15F)` | **not set** → inherits |

The two outliers are `src/Comets.Application.Graph/FormGraph.Designer.cs:84` and `src/Comets.Application.OrbitViewer/Controls/Toolbox/ModeControl.Designer.cs:76`.

**This is the single biggest correction to the original plan.** The first draft asserted that most forms leave `Font` implicit and that `6F, 13F` is a stale MS Sans Serif artifact producing wrong scale factors. Neither holds:

- 36 of 38 files *do* set `this.Font = new Font("Tahoma", 8.25F, ...)` explicitly. Tahoma 8.25pt at 96 DPI has a 13px cell height, so `6F, 13F` is the **correct, matching** design-time pair. `AutoScaleMode.Font` will compute `192dpi_height / 13 ≈ 2.0` on the 200% monitor — which is exactly the intended behaviour, not a bug.
- The wholesale "open all 36 designer files in VS and re-save" step is therefore unnecessary, and it was the riskiest and least automatable item in the plan. Deleted.

The two outliers *are* a real (pre-existing) bug: `ModeControl` is a `UserControl` hosted inside `OrbitViewerControl`, which sets Tahoma 8.25 (13px). `ModeControl` declares a design-time baseline of 15px but inherits a 13px font, so WinForms scales it by `13/15 ≈ 0.867` — it renders ~13% small today, under `DpiUnaware`, and will keep doing so under any awareness mode. Same for `FormGraph`, which inherits nothing and gets Segoe UI 9pt — that one is self-consistent at 96 DPI but diverges from every other form.

**Forms' own font settings, non-designer:**
- `src/Comets.Application.Common/Managers/FilterPanelManager.cs:242` — `new Font("Tahoma", 8.25F, ..., GraphicsUnit.Point, 238)`, explicit points, scales correctly
- `src/Comets.Core/Managers/EphemerisManager.cs:275,283,288,298` — `new Font("Tahoma", 8.25F)` and `:386` — `new Font("Tahoma", 11.25F)`; unit defaults to Point, so DPI-scaled

**OrbitPanel (`src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs`, 1824 lines):**
- Inherits from `OpenTK.GLControl` — transitively loads GLFW
- Overlay/label fonts declared at lines 135-138 in **point units** (default) — scale with DPI via GDI+
- `GL.Viewport(0, 0, Width, Height)` at lines 453 (`OnResize`) and 590 (`InitGL`)
- Screenshot capture path at lines 490-500 — `new Bitmap(Width, Height, Format32bppRgb)` + `GL.ReadPixels(0, 0, Width, Height, ...)`
- Aspect ratio at line 913
- `MvpProject` (lines 1207-1215) — NDC → screen using `Width`/`Height`
- `pixelsPerAU = Height / (2.0 * _orthoHalfH)` at line 1306
- Axis-label point-size calc at line 1487
- `RenderLabels` (line 1526) — `new Bitmap(Width, Height, Format32bppArgb)` at 1530, label placement 1537-1567, `MvpProject` call sites 1586/1601/1634
- Texture upload at lines 1643-1649
- No `OnDpiChanged*` overrides anywhere in the app

**Hand-computed layout in code-behind** (the `LogicalToDeviceUnits` candidates):
- `src/Comets.Application.Common/Managers/FilterPanelManager.cs:164-245` — ~17 hardcoded `new Point(...)` / `new Size(...)` values building a filter row
- `src/Comets.Application.Common/Controls/Database/FilterControl.cs:169` — `new Point(20, 7)`
- `src/Comets.Application.Common/External/DarkMode/DarkModeCS.cs:1111` — `new Size(8, 4)` chevron glyph
- `src/Comets.Application.OrbitViewer/Controls/Toolbox/CollapsiblePanel.cs` — see Phase 4; different failure mode

The original plan named `src/Comets.Application.OrbitViewer/Controls/Toolbox/*.cs` as the hand-computed-layout hotspot. **It has none** — a grep for assignments to `Location`/`Size`/`Bounds` across all non-designer files under that directory returns zero hits. That item is removed and replaced with the real ones above.

**Mouse handling** lives entirely in `src/Comets.Application.OrbitViewer/Controls/OrbitViewerControl.cs`, not in `OrbitPanel.cs` (which has no mouse handlers at all). See Phase 5.

No code anywhere calls `ScaleControl`, `OnDpiChanged`, `ScaleChildren`, `PerformAutoScale`, or `LogicalToDeviceUnits`.

---

## Why `SystemAware` didn't "just work" when we tried it

The original draft attributed this to stale `AutoScaleDimensions`. As established above, that diagnosis does not hold — the values match the runtime font. **The root cause of the earlier SystemAware failure is therefore unconfirmed**, and Phase 0 exists to establish it before any code changes land.

The plausible candidates, in order:

1. **OrbitPanel rendering dimensions weren't DPI-corrected.** `GLControl.Width`/`.Height` are logical pixels. The overlay bitmap was allocated at logical size while its fonts rasterized against the bitmap's own 96-DPI resolution — text came out visibly wrong-sized on the 200% monitor. This one is certain and is what Phase 3 fixes.
2. **`CollapsiblePanel` height arithmetic breaks** (Phase 4) — a hard functional break, not just cosmetic, and very likely part of what "broken layout" meant.
3. **`FilterPanelManager` rows render at 96-DPI geometry** inside a 2×-scaled parent — overlapping, clipped controls.
4. **Residual GLFW awareness conflict.** Under `SystemAware` the process context is already set before GLFW loads, so GLFW's `SetProcessDpiAwareness` call should no-op the same way it does under `DpiUnaware`. Worth confirming rather than assuming.

Phase 0 distinguishes these cheaply.

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

Phases 0-6 reach the **SystemAware checkpoint**. At that point the app is committable and shippable — better than today even though we haven't reached the final target. Phases 7-9 go from checkpoint to full PerMonitorV2.

### Phase 0 — Instrumentation spike (do this first, throw it away after)

Cheapest possible de-risking. Add temporary logging, flip to `SystemAware`, run on the 200% monitor, read the numbers, revert the flip.

Log on first paint of `OrbitPanel`:

```csharp
GL.GetInteger(GetPName.Viewport, viewport);   // actual GL viewport, 4 ints
Debug.WriteLine($"DeviceDpi={DeviceDpi} ClientSize={ClientSize} " +
                $"GLViewport={viewport[2]}x{viewport[3]} " +
                $"BmpDpi={new Bitmap(1,1).HorizontalResolution}");
```

This answers, in one run, three questions the rest of the plan is currently guessing at:

1. **Does `ClientSize` report logical or physical pixels under `SystemAware`?** The whole of Phase 3 assumes logical. OpenTK 4.x's `GLControl` has no explicit framebuffer-size property, and with `NumberOfSamples = 8` some driver paths have been reported to differ. If `ClientSize` already reports physical, Phase 3 collapses to almost nothing.
2. **What resolution does a default `new Bitmap(w, h, fmt)` get?** Phase 3's `SetResolution` call is written defensively assuming 96; confirm rather than assume.
3. **Does the process awareness actually stick?** Query `GetThreadDpiAwarenessContext` after the first GL context creation to confirm GLFW no-ops under `SystemAware` the way it does under `DpiUnaware`. If it does *not*, everything downstream is moot and the plan needs rethinking at the GLFW level first.

Also, while here: capture a screenshot of each form under `SystemAware` **before** any fixes. That gives a concrete baseline for what "broken" meant and lets Phases 2-6 be validated against real evidence rather than recollection.

**Do not skip this phase.** It costs one run and it is the only thing standing between this plan and three phases of speculative work.

### Phase 1 — Flip to SystemAware, fix the two font outliers

**File:** `src/Comets.Application/Program.cs:16`

```csharp
System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.SystemAware);
```

**Files:** `FormGraph.Designer.cs`, `ModeControl.Designer.cs`

Bring both in line with the other 36: add `this.Font = new System.Drawing.Font("Tahoma", 8.25F);` and change `AutoScaleDimensions` to `new System.Drawing.SizeF(6F, 13F)`.

Hand-editing these two is fine and preferable to a designer round-trip — we are copying a known-correct pair from 36 sibling files, not inventing numbers. The original plan's blanket "never hand-edit AutoScaleDimensions, always let the designer regenerate" was sound advice for a 36-file rewrite; for two files matching an established baseline it is overhead.

Expect `ModeControl` to get visibly ~13% larger. That is the pre-existing bug being fixed, not a regression.

### Phase 2 — Hand-computed layout → `LogicalToDeviceUnits`

`LogicalToDeviceUnits` is a no-op under `DpiUnaware`, a constant scale under `SystemAware`, and correct-per-monitor under `PerMonitorV2` — so this phase is safe to land before the awareness flip and needs no revisiting in Phase 7.

**`src/Comets.Application.Common/Managers/FilterPanelManager.cs:164-245`** — the largest concentration. Every `new Point(x, y)` and `new Size(w, h)` in the filter-row builder needs wrapping:

```csharp
cboProperty.Location = LogicalToDeviceUnits(new Point(20, 2));
cboProperty.Size    = LogicalToDeviceUnits(new Size(190, 21));
```

`FilterPanelManager` is a manager class, not a `Control`, so it has no `LogicalToDeviceUnits` of its own — call it on the owning control, or add a small helper that takes the parent control's `DeviceDpi`.

Also `:464` and `:468`, which offset control positions by a computed delta — those deltas derive from already-scaled `Location` values, so verify rather than blindly wrap.

**`src/Comets.Application.Common/Controls/Database/FilterControl.cs:169`** — `btnAddNew.Location = new Point(20, 7)`. Also `:160`, same offset-arithmetic caveat.

**`src/Comets.Application.Common/External/DarkMode/DarkModeCS.cs:1111`** — `Size cSize = new Size(8, 4)` chevron. This is third-party vendored code; a minimal wrap is fine but note the divergence from upstream in a comment.

### Phase 3 — OrbitPanel physical-pixel sizing (static DPI)

**File:** `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs`

*Gated on Phase 0 question 1.* If `ClientSize` already reports physical pixels, most of this phase disappears.

Assuming it reports logical: `GL.Viewport` and the overlay `Bitmap` must use **physical** pixels or the GL surface gets up-scaled by Windows.

```csharp
private float DpiScale => DeviceDpi / 96f;
private int PxWidth  => (int)(Width  * DpiScale);
private int PxHeight => (int)(Height * DpiScale);
```

**Coordinate-space convention — decide once, here:**

> **Everything stays in logical pixels except the GL viewport and the overlay bitmap interior.** `MvpProject` continues to return logical coordinates. Conversion happens at exactly two boundaries: `GL.Viewport(...)` and inside `RenderLabels`.

This is the right split because `MvpProject`'s output feeds `OVComet.PanelLocation`, which `SelectComet` (line 1693) compares against WinForms mouse coordinates. Keeping both logical means the picking path needs no change at all.

Sites to change:

| Line | Site | Change |
|---|---|---|
| 453 | `OnResize` | `GL.Viewport(0, 0, PxWidth, PxHeight)` |
| 590 | `InitGL` | same |
| 490-500 | Screenshot capture | `new Bitmap(PxWidth, PxHeight, ...)`, `GL.ReadPixels(0, 0, PxWidth, PxHeight, ...)` — **missed by the original plan**; produces a torn/partial image otherwise |
| 913 | Aspect ratio | ratio is scale-invariant; no change needed, but switch to `PxWidth`/`PxHeight` for consistency |
| 1207-1215 | `MvpProject` | **no change** — stays logical per the convention above |
| 1306 | `pixelsPerAU` | `PxHeight / (2.0 * _orthoHalfH)` — **missed by the original plan**; check every consumer, since callers may expect logical |
| 1487 | Axis-label point size | divide by `PxHeight` |
| 1530 | `RenderLabels` bitmap | `new Bitmap(PxWidth, PxHeight, ...)` + `SetResolution` (below) |
| 1537-1567 | Label placement | `* DpiScale` — see mixed-units trap below |
| 1586/1601/1634 | `MvpProject` results used as bitmap coords | `* DpiScale` at the call site |
| 1643-1649 | Texture upload | `PxWidth`, `PxHeight` |

**Font rasterization inside the overlay bitmap.** A bitmap allocated at physical size still carries its own DPI (96 by default), and GDI+ maps point-unit fonts through *that*, not through the surface it eventually lands on. Without intervention, `FontInformation` (Consolas 10pt) renders 13px tall inside a 2×-sized bitmap — i.e. half the intended logical size. Fix:

```csharp
bmp.SetResolution(96f * DpiScale, 96f * DpiScale);
```

**Mixed-units trap in `RenderLabels` — read carefully.** After `SetResolution`, three kinds of number coexist in the same block and they do *not* all scale the same way:

- `labelMargin = 8` (line 1537) — a raw pixel coordinate. **Needs `* DpiScale`.**
- `fs = FontInformation.Size` (lines 1548, 1564) — a *point* value (10) being used as a *pixel* offset multiplier in `fs * 5.0`, `fs * 3.5`, `fs * 2.0`. **Needs `* DpiScale`**, and is worth replacing with `FontInformation.GetHeight(g)` which returns correctly-scaled pixels directly and removes the point/pixel conflation entirely.
- `strWidth = g.MeasureString(...).Width` (line 1563) — already in scaled bitmap pixels because `MeasureString` respects the bitmap resolution. **Must NOT be multiplied.**

Multiplying `strWidth` is the easy mistake here and produces a right-aligned date label that walks off the edge of the viewport at 200%. Add a comment at that line.

### Phase 4 — `CollapsiblePanel` height arithmetic (new — a hard break)

**Files:** `src/Comets.Application.OrbitViewer/Controls/Toolbox/CollapsiblePanel.cs`, `src/Comets.Application.OrbitViewer/Controls/OrbitViewerControl.Designer.cs:81-284`

Not in the original plan, and it is the only *functional* (rather than cosmetic) breakage identified.

`CollapsiblePanel` stores its two heights as plain `int`s:

```csharp
private readonly int HeightCollapsed = 30;      // :14
public int HeightExpanded { get; set; }         // :26 — set from designer: 114, 64, 191, 66, 95, 133, 86, 158
```

and drives state off an **exact equality** check:

```csharp
get { return this.Height == HeightCollapsed; }  // :32
set { this.Height = value ? HeightCollapsed : HeightExpanded; }  // :35
```

WinForms auto-scales `Control.Height`, but `HeightCollapsed` and `HeightExpanded` are ordinary integer members that no scaling machinery touches. At 200% the panel's real height becomes 60 while `HeightCollapsed` stays 30, so:

- `IsCollapsed` returns `false` for a collapsed panel — the toggle desynchronises from the visual state
- Setting `IsCollapsed` snaps the panel to a 96-DPI height, half its correct size
- `MovePanels` (`:93`) computes `offset = HeightExpanded - HeightCollapsed` in logical units and applies it to already-scaled `Top` values, so the whole toolbox stack drifts on every expand/collapse

Fix by scaling both at use time:

```csharp
private int ScaledHeightCollapsed => LogicalToDeviceUnits(HeightCollapsed);
private int ScaledHeightExpanded  => LogicalToDeviceUnits(HeightExpanded);
```

and replace the equality test with a tolerance comparison, since `LogicalToDeviceUnits` rounds and the round-trip is not guaranteed exact:

```csharp
get { return Math.Abs(this.Height - ScaledHeightCollapsed) <= 2; }
```

Better still, track collapsed state in an explicit `bool` field rather than inferring it from pixel height. Inferring UI state from a rounded measurement is what makes this fragile in the first place, and under PerMonitorV2 the panel can be re-scaled between the write and the read. Recommend the explicit field.

Under PerMonitorV2 this also needs re-evaluating on DPI change — `ScaledHeight*` being computed properties handles that automatically, which is why they are properties and not cached fields.

### Phase 5 — Mouse coordinate audit

Much smaller than the original plan assumed. `OrbitPanel.cs` has **no mouse handlers**; all of it is in `src/Comets.Application.OrbitViewer/Controls/OrbitViewerControl.cs`:

- `:1051` `orbitPanel_MouseDown`, `:1066` `MouseClick`, `:1075` `MouseDoubleClick`, `:1085` `MouseMove`, `:1105` `OnMouseWheel`
- `:1094-1095` — `deltaX = e.X - StartDrag.X`, `deltaY = e.Y - StartDrag.Y`

The drag math is purely **differential** — it converts a pixel delta into a rotation increment. There is no world-space projection in the mouse path, so no coordinate-space bug exists. What *does* change is feel: a fixed drag distance in logical pixels produces the same rotation at any DPI, which is arguably correct. Verify by hand; adjust the rotation-per-pixel constant only if it feels wrong at 200%.

The picking path (`SelectComet`, `OrbitPanel.cs:1693`) compares mouse coordinates to `PanelLocation`, which comes from `MvpProject`. Per the Phase 3 convention both remain logical, so **no change is required**. The hardcoded `range = 5` hit radius stays in logical pixels, which keeps the hit target a constant physical size on screen — correct.

`:285-290` in `OrbitViewerControl.cs` positions `FormFind` using `PointToScreen` plus `new Size(7, 7)` — wrap the margin with `LogicalToDeviceUnits`.

### Phase 6 — Verification of MS Chart, dark mode

No code change expected — eyes-on verification after Phases 1-5:

- **`src/Comets.Core/Managers/EphemerisManager.cs:275,283,288,298,386`.** Chart fonts built as `new Font("Tahoma", 8.25F)` / `11.25F` without explicit `GraphicsUnit` — defaults to Points, so GDI+ scales them. Confirm no axis-label overlap or clipping is introduced.
- **`src/Comets.Application.Common/External/DarkMode/DarkModeCS.cs`.** DWM border widths depend on DPI. Verify the dark title bar renders at correct thickness on both monitors, and that the Phase 2 chevron change looks right.

---

### 🏁 **SystemAware checkpoint — commit and tag here**

At this point the app:
- Lays out correctly on the primary monitor at any scale factor
- Renders OrbitViewer at native resolution on the primary monitor
- Has the `CollapsiblePanel` and `FilterPanelManager` breakages fixed permanently (both are awareness-mode-independent)
- Bitmap-scales when dragged to the secondary monitor (acceptable, matches most WinForms apps)

If Phases 7-9 run aground on the GLControl context-survival question, this checkpoint is the shippable fallback. Tag it.

---

### Phase 7 — Flip to PerMonitorV2 and handle WM_DPICHANGED in forms

**File:** `src/Comets.Application/Program.cs:16`

```csharp
System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
```

WinForms handles most of `WM_DPICHANGED` automatically: on a DPI change each form scales its controls by `newDpi / oldDpi` via the `AutoScaleMode.Font` machinery. This is why Phase 1's outlier fix comes first — a form with a mismatched design-time baseline compounds its error on every DPI transition rather than just at startup.

**Per-form work** — override `OnDpiChanged` only on forms with custom-drawn content or hand-computed layout that won't auto-scale:

```csharp
protected override void OnDpiChanged(DpiChangedEventArgs e)
{
    base.OnDpiChanged(e);
    // Rescale non-AutoScaled members here
}
```

Candidates: `FormMain` (MDI — see Phase 9), `FormOrbitViewer` (see Phase 8), `FormDatabase` (hosts the `FilterPanelManager`-built rows, which are constructed imperatively and may need a rebuild rather than a rescale). `FormGraph` probably needs nothing; MS Chart is DPI-aware.

Find the rest by testing (Phase 10).

### Phase 8 — OrbitPanel DPI reactivity (the hard part)

**File:** `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs`

On DPI change, OrbitPanel must:
1. Reallocate the overlay bitmap at the new physical size — easy; it is a per-frame `using var` already
2. Reset `GL.Viewport` to the new physical size — easy; `OnResize` fires after DPI change
3. Recompute the axis-label point-size formula — easy; derives from `PxHeight` automatically
4. **Keep the OpenGL context alive across the DPI change** — hard

The last one is the unresolved risk. Under PerMonitorV2, WinForms may recreate the HWND on DPI change to apply new window-frame metrics. `OpenTK.GLControl 4.0.2` creates the GL context against the original HWND; HWND recreation destroys it, taking all shaders, VBOs, textures, the body VAO, the text quad VAO, and the text texture with it.

**Mitigation options, in order of preference:**

1. **Reload all GL state on DPI change.** Set a `_reloadNeeded` flag in `OnHandleDestroyed`, rebuild everything in `OnHandleCreated` or the next `OnPaint`. Guaranteed to work; costs one stuttered frame during a rare interactive moment.
2. **Suppress HWND recreation.** Intercept `WM_DPICHANGED_AFTERPARENT` in `WndProc` and skip the default handle-recreation path. Cheaper if it works, but depends on WinForms internals staying cooperative across servicing updates.
3. **Switch to `OpenTK.WinForms 4.0.0-pre.8`.** Referenced in `docs/01b-opengl-implementation.md` but not installed. Pre-release with its own reported bugs. Last resort only.

**Recommended: option 1.** The original draft also recommended state reload but listed HWND suppression first; reversing the order here is deliberate. Option 2's failure mode is intermittent and version-dependent — the worst kind to debug — whereas option 1's cost is a single dropped frame on a monitor crossing, which is imperceptible. Build option 1 first and only reach for option 2 if profiling shows the reload is actually visible.

Structuring for option 1 is also worth doing regardless: GL context loss can happen for other reasons (driver reset, TDR), and a codebase that can rebuild its GL state on demand is more robust in general.

**Additional code:**

```csharp
protected override void OnDpiChangedAfterParent(EventArgs e)
{
    base.OnDpiChangedAfterParent(e);
    Invalidate();
}
```

Confirm `OnResize` fires after `OnDpiChangedAfterParent`. If it does not, call `GL.Viewport(0, 0, PxWidth, PxHeight)` directly — `DpiScale` reads `DeviceDpi` live, so it will already be current.

### Phase 9 — MDI container behaviour

**File:** `src/Comets.Application/Application/FormMain.cs`

MDI children don't get per-monitor DPI — they inherit the MDI parent's context. When `FormMain` straddles two monitors, Windows picks the DPI of the monitor with greater overlap and fires `WM_DPICHANGED` on boundary crossings.

Known WinForms issues with MDI + PerMonitorV2:
- MDI child Z-order and position drift after DPI change
- MDI child title-bar metrics get out of sync with the parent
- Status strip / menu strip scaling lags one DPI change behind

Test and add targeted overrides if drift appears; a `PerformLayout()` in `OnDpiChanged` often fixes ordering. This phase is exploratory — the remediation techniques are known, the number of applications needed is not.

### Phase 10 — Testing matrix

Test each of (primary=100%, secondary=200%) and (primary=200%, secondary=100%), and within each: app started on primary / started on secondary / dragged primary→secondary / dragged secondary→primary.

For each:
- [ ] FormMain opens at correct size, menu bar readable
- [ ] FormEphemeris renders cleanly
- [ ] FormGraph renders cleanly; chart axes readable, no label clipping *(Phase 1 changed this form's font baseline — check against the Phase 0 screenshot)*
- [ ] FormOrbitViewer opens at correct size; toolbox controls not oversized or clipped
- [ ] **Toolbox collapse/expand: every panel toggles to the correct height, no stack drift after repeated toggling** *(Phase 4)*
- [ ] **ModeControl sized consistently with sibling toolbox controls** *(Phase 1)*
- [ ] FormDatabase filter rows: no overlap or clipping; add/remove a filter and confirm rows reflow correctly *(Phase 2)*
- [ ] **OrbitViewer 3D view: orbits render at native monitor resolution on whichever monitor the window is on**
- [ ] **OrbitViewer after dragging between monitors: framebuffer reallocated at new resolution, GL context still valid (no black/solid-colour viewport, no GL errors in debug build)**
- [ ] OrbitViewer text overlays crisp, correctly positioned, correctly sized; **right-aligned date label not clipped at the right edge** *(the `strWidth` trap, Phase 3)*
- [ ] Comet selection markers land on the correct orbit point; click-to-select hits the right comet *(Phase 3 convention, Phase 5)*
- [ ] **Screenshot capture produces a complete, correctly-sized image** *(Phase 3)*
- [ ] FormElements / FormSettings / FormFind: layout intact; FormFind positions correctly relative to the panel *(Phase 5)*
- [ ] Dark mode on/off: chrome still correct
- [ ] **MDI container straddling boundary: no child-window drift**

---

## Files touched (summary)

| Phase | File | Change |
|---|---|---|
| 0 | `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs` | Temporary instrumentation — reverted after |
| 1 | `src/Comets.Application/Program.cs` | `DpiUnaware` → `SystemAware` |
| 1 | `src/Comets.Application.Graph/FormGraph.Designer.cs` | Font + AutoScaleDimensions → Tahoma 8.25 / 6,13 |
| 1 | `src/Comets.Application.OrbitViewer/Controls/Toolbox/ModeControl.Designer.cs` | Same |
| 2 | `src/Comets.Application.Common/Managers/FilterPanelManager.cs` | `LogicalToDeviceUnits` on ~17 hardcoded positions |
| 2 | `src/Comets.Application.Common/Controls/Database/FilterControl.cs` | Same |
| 2 | `src/Comets.Application.Common/External/DarkMode/DarkModeCS.cs` | Chevron size |
| 3 | `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs` | Physical-pixel viewport, bitmap, texture, screenshot, label math |
| 4 | `src/Comets.Application.OrbitViewer/Controls/Toolbox/CollapsiblePanel.cs` | Scaled heights + explicit collapsed-state field |
| 5 | `src/Comets.Application.OrbitViewer/Controls/OrbitViewerControl.cs` | FormFind margin; verify drag sensitivity |
| 6 | `src/Comets.Core/Managers/EphemerisManager.cs` | Verify only |
| 6 | `src/Comets.Application.Common/External/DarkMode/DarkModeCS.cs` | Verify only |
| **—** | **SystemAware checkpoint commit + tag** | |
| 7 | `src/Comets.Application/Program.cs` | `SystemAware` → `PerMonitorV2` |
| 7 | `src/Comets.Application/Application/FormMain.cs` | `OnDpiChanged` override if needed |
| 7 | `src/Comets.Application.OrbitViewer/FormOrbitViewer.cs` | `OnDpiChanged` override if needed |
| 7 | `src/Comets.Application.Common/FormDatabase.cs` | Filter-row rebuild on DPI change if needed |
| 8 | `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs` | `OnDpiChangedAfterParent`, GL state reload on HWND recreation |
| 9 | `src/Comets.Application/Application/FormMain.cs` | MDI-specific fixups if testing reveals drift |

---

## Risks & notes

- **The SystemAware failure is still undiagnosed.** The first draft's explanation (stale `AutoScaleDimensions`) is disproven. Phase 0 exists to replace speculation with measurement. If Phase 0 reveals something outside the four candidates listed above, revise this plan before writing Phase 3.
- **GLControl DPI reporting consistency.** `ClientSize` under SystemAware/PerMonitorV2 *should* equal the physical backbuffer, but with `NumberOfSamples = 8` some driver paths have reported logical pixels. Phase 0 question 1. Phase 3 is written for the logical case and shrinks substantially if the physical case holds.
- **GL context survival on HWND recreation** (Phase 8) remains the biggest unknown. Design for state reload from day one.
- **MDI + PerMonitorV2** is the second-biggest unknown. WinForms documentation is sparse and community reports are mixed. Phase 9 is exploratory.
- **`FilterPanelManager` builds controls imperatively at runtime**, which means rows created *before* a DPI change won't be rescaled by the auto-scale machinery the way designer-placed controls are. Phase 7 may need a rebuild-on-DPI-change path in `FormDatabase`. Flagged rather than solved — confirm during Phase 10 testing.
- **`OpenTK.WinForms` alternative.** Current csproj uses `OpenTK.GLControl 4.0.2`. `OpenTK.WinForms 4.0.0-pre.8` is pre-release with its own reported bugs. Swap only as a last resort in Phase 8.
- **Phases 2 and 4 are worth landing regardless of the outcome of the whole plan.** Both fix bugs that exist today under `DpiUnaware` (`ModeControl`'s 13% shrink) or would appear under any awareness mode (`CollapsiblePanel`, `FilterPanelManager`). If the DPI work is abandoned entirely, these should still be committed.
- **Backward compatibility.** On pre-1703 Windows, PerMonitorV2 degrades to PerMonitor or SystemAware. No action needed.
- **Test on primary=200% specifically.** The previous breakage was worst when the *primary* monitor was high-DPI. Prioritise that case in both the checkpoint validation and the final testing.
