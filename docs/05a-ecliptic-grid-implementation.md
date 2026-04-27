# Ecliptic Plane Reference Grid — Implementation Notes

## Overview

Added a toggleable ecliptic plane reference grid to the orbit viewer. The grid renders in the Z=0 plane (the ecliptic) and is visible via a new **Show grid** checkbox in the Misc toolbox panel. A companion textbox lets the user specify the grid extent in AU.

---

## Core Technique: Pseudo-Perspective

The orbit viewer uses an **orthographic projection**. An orthographic camera renders parallel lines as parallel — a flat grid viewed at an angle looks like a parallelogram, not a trapezoid. This gives no sense of depth.

To restore the depth cue, each grid vertex is pre-distorted in world space before the MVP transform using a **pseudo-perspective** scale factor:

```
vd = sinV * (px * sinH - py * cosH)
f  = D / max(1, D - vd)
px' = f * px
py' = f * py
```

where:
- `sinV = sin(RotateVert)`, `sinH/cosH = sin/cos(RotateHorz)` — the current camera angles
- `vd` is the camera-space depth of the ecliptic-plane point `(px, py, 0)`, derived by expanding the `Rx(v)·Rz(h)` camera transform and projecting onto the view direction
- `D = 800 AU` — the virtual camera distance; with the default 150 AU grid this keeps `f` within roughly ±23% at the boundary, giving a visible depth cue without distorting the orbital paths drawn nearby
- Points with `Z ≠ 0` (the ecliptic-pole axis) pass through unchanged

The formula was proved correct by expanding `Rx(v)·Rz(h)·[px, py, 0]` and showing that the camera-space depth is exactly `sinV·(px·sinH − py·cosH)`, so scaling `(px, py)` by `f` is equivalent to a pinhole projection from `(0, 0, D)` onto the `Z=0` plane and back.

The same `PseudoPerspective()` helper is applied to:
- Grid vertex endpoints (via `AddPerspVertex`)
- Axis line tip positions (so axis lines stay parallel to grid lines)
- Axis label world positions (so labels stay at the corrected tip)

---

## Adaptive Step Selection

The grid spacing adapts to zoom level so lines remain readable at any scale:

```
idealStep  = 50px / (pixelsPerAU × elevFactor)
```

`elevFactor = |cos(RotateVert)|` corrects for foreshortening — at edge-on view the lines appear much closer together, so finer steps would be needed to maintain the target 50 px gap.

Steps are snapped to a set of "nice" values: `{ 0.1, 0.2, 0.5, 1, 2, 5, 10, 25 }`. The largest step ≤ `idealStep` is chosen, then stepped down if fewer than 9 cells would be shown within `GridExtent`.

The coarsest step (25 AU) is also the minimum, so the grid is always at least 12×12 cells (6 cells per side) regardless of zoom.

---

## Elevation and Zoom Fade

The grid fades out as the view approaches edge-on, since at 90° elevation the grid becomes a single line and carries no information.

```
rawAlpha = clamp(elevFactor × 3.86, 0, 1) ^ 1.5
```

`3.86 ≈ 1/sin(15°)` means full opacity is reached once the viewer is more than 15° above the ecliptic. The `^1.5` exponent makes the fade non-linear (faster drop near edge-on).

At low zoom (zoomed out, large pixelsPerAU is small) the fade is suppressed so the grid stays visible even at shallow angles — useful for the wide view where the full grid is the main reference. At high zoom (small area, many px/AU) the elevation fade is allowed to operate fully.

```
zoomNorm = clamp((pixelsPerAU - 5) / (100 - 5), 0, 1)
alpha    = rawAlpha + (1 - rawAlpha) × (1 - zoomNorm)
```

---

## Z-Fighting Fix

The u=0 grid lines (constant Y=0 and constant X=0) are geometrically identical to the X and Y axis lines. Drawing both causes flickering Z-fighting. Solution: simply skip u=0 when axes are visible — no GL depth tricks needed.

```csharp
if (ShowAxes && Math.Abs(u) < step * 0.001)
    continue;
```

---

## Dynamic Grid Extent

`GridExtent` (default 150 AU) controls the boundary of the drawn grid lines and can be changed at runtime via a textbox in the Misc panel (enabled only when "Show grid" is checked).

Typical use: set to 5 AU when studying inner-planet orbits so fine grid lines appear around Mercury/Venus/Earth/Mars without showing the full outer solar system scale.

The value is applied on Enter or focus-leave, validated as a positive double using `InvariantCulture` parsing.

---

## Rendering Order

Grid is rendered before planet orbits so orbit lines draw on top. This avoids the grid visually obscuring the thin orbit lines in the ecliptic plane.

---

## Files Changed

| File | Change |
|------|--------|
| `OrbitPanel.cs` | `RenderGrid()`, `AddPerspVertex()`, `PseudoPerspective()`, `ColorGrid`, `ShowGrid`, `GridExtent` properties, `RenderScene()` call, axis tips/labels updated |
| `MiscControl.cs` | `OnShowGridChanged`, `OnGridExtentChanged` events, `SetGridExtent()`, `cbxShowGrid`, `txtGridExtent`, `lblGridAU` |
| `MiscControl.Designer.cs` | Layout for new grid checkbox and extent textbox; panel height 77→100 |
| `OrbitViewerControl.cs` | `SetShowGrid()`, `SetGridExtent()` handlers wired to `miscControl` events |

---

## What Was Tried and Reverted

**Solid plane** (`a15a2c8` / reverted `20d0a5b`): replaced the grid with a semi-transparent filled quad. The grid was preferred for its cleaner depth cue.

**Drag direction fix** (`9f4a000` / reverted `5dd1666`): a fix for horizontal drag reversing when viewing from below the ecliptic. Was reverted because the pseudo-perspective grid itself creates strong orientation cues that make the pre-fix behavior feel natural again.

**Axis length tied to GridExtent**: tried making axis lines scale with `GridExtent` so they stay parallel to grid lines at small extents. Reverted because it made axes appear as tiny stubs when `GridExtent` was set to a small value. Final decision: `SizeAU = 150` fixed, `D = 800` fixed — axes are always 150 AU and parallel to the grid at the default extent.

**Viewport-relative R**: tried making grid line endpoints track the viewport half-width so the perspective ratio `D/R` stays constant at every zoom. This caused lines to visibly "shrink" relative to orbits as you zoom in (edges always tracked the viewport) and introduced stripes when `GridExtent` < viewport. Reverted to `R = GridExtent`, `D = 800`.
