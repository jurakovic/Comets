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
- `D = extent × (800/150)` — the virtual camera distance, scaled proportionally with `GridExtent` so the perspective strength (max `f` at the boundary, ~±23%) is constant at any grid size
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

The fade is suppressed in two cases where the grid is fully in view:

1. **Whole grid fits in the viewport** (`orthoHalfH ≥ extent`) — regardless of zoom, if the grid boundary is within the visible area, fade is always suppressed.
2. **Zoomed out** — when the grid appears small on screen, fade is suppressed so it stays visible even at shallow angles.

Both cases are unified through an extent-normalised zoom metric:

```
effectivePxPerAU = pixelsPerAU × (extent / 150)
zoomNorm = clamp((effectivePxPerAU - 5) / (70 - 5), 0, 1)   [0 when gridFitsInView]
alpha    = rawAlpha + (1 - rawAlpha) × (1 - zoomNorm)
```

Normalising by `extent / 150` makes the fade threshold relative to the grid's screen size rather than absolute zoom. A 5 AU grid at 30× higher `pixelsPerAU` occupies the same screen area as a 150 AU grid at the reference zoom — and now fades at the same apparent size. Without this, small-extent grids would fade out at shallower angles than large ones for an equivalent on-screen footprint.

---

## Z-Fighting Fix

The u=0 grid lines (constant Y=0 and constant X=0) are geometrically identical to the X and Y axis lines. Drawing both causes flickering Z-fighting. Solution: simply skip u=0 when axes are visible — no GL depth tricks needed.

```csharp
if (ShowAxes && Math.Abs(u) < step * 0.001)
    continue;
```

---

## Dynamic Grid Extent

`GridExtent` (default 150 AU) controls the boundary of the drawn grid lines — and also the length of the axis lines — and can be changed at runtime via the **Extent** textbox in the Misc panel. The field is always enabled; typing a valid value automatically checks "Show grid" if it was unchecked (same behaviour as the Filter on Date panel). Only numeric input is accepted, and changes apply immediately on each keystroke.

Typical use: set to 5 AU when studying inner-planet orbits so fine grid lines appear around Mercury/Venus/Earth/Mars without showing the full outer solar system scale.

---

## Rendering Order and Depth

Grid is rendered before planet orbits so orbit lines draw on top. Grid lines are drawn at `LineWidth = 1.0f`; all other lines (orbits, axes) use `1.5f`.

When the grid is at full opacity (`alpha ≥ 0.99`) it writes to the depth buffer normally, so it can occlude geometry behind it. Once the elevation fade begins (`alpha < 0.99`), depth writes are disabled (`GL.DepthMask(false)`) for the grid draw call and restored immediately after. This prevents a nearly-invisible grid from silently cutting orbit lines that would otherwise be visible behind it.

---

## Files Changed

| File | Change |
|------|--------|
| `OrbitPanel.cs` | `RenderGrid()`, `AddPerspVertex()`, `PseudoPerspective(xyz, D)`, `ColorGrid`, `ShowGrid`, `GridExtent` properties, `RenderScene()` call, axis tips/labels updated; line width 1.0f for grid; depth-mask guard on fade |
| `MiscControl.cs` | `OnShowGridChanged`, `OnGridExtentChanged` events, `SetGridExtent()`, `txtGridExtent` with auto-check, numeric `KeyPress` validation |
| `MiscControl.Designer.cs` | Extent label + textbox as top row; checkboxes shifted down; panel height 77→123; Save image button now visible |
| `OrbitViewerControl.cs` | `SetShowGrid()`, `SetGridExtent()` handlers; `miscControl.ContainsFocus` added to shortcut guard |
| `OrbitViewerControl.Designer.cs` | `cpnlMisc` working area height 77→123, `HeightExpanded` 112→158 |

---

## Grid vs. Comet Position Mismatch

The pseudo-perspective distortion is applied only to **grid vertices** (pre-distorted in world space on the CPU before GPU upload). Comet and orbit vertices are uploaded at their true AU positions and transformed only by the orthographic MVP — no distortion.

This means the grid is a visual depth cue, not a calibrated ruler:

- A comet at `(R, 0, 0)` renders at the true `R` AU position in screen space.
- The grid boundary at `R` AU is pushed outward to `f × R` — up to ~23% farther at the edge.
- A comet sitting at the grid boundary will appear slightly **inside** it, not on it.

The mismatch grows with distance from the origin and is most visible for objects near the grid edge. It is purely cosmetic — the grid's purpose is orientation, not measurement.

---

## What Was Tried and Reverted

**Solid plane** (`a15a2c8` / reverted `20d0a5b`): replaced the grid with a semi-transparent filled quad. The grid was preferred for its cleaner depth cue.

**Drag direction fix** (`9f4a000` / reverted `5dd1666`): a fix for horizontal drag reversing when viewing from below the ecliptic. Was reverted because the pseudo-perspective grid itself creates strong orientation cues that make the pre-fix behavior feel natural again.

**Axis length tied to GridExtent**: originally reverted because axes appeared as tiny stubs at small `GridExtent`. Later re-enabled for testing — `SizeAU = GridExtent`, `D = GridExtent × (800/150)` — so axis lines and grid lines always share the same extent and the same perspective strength.

**Viewport-relative R**: tried making grid line endpoints track the viewport half-width so the perspective ratio `D/R` stays constant at every zoom. This caused lines to visibly "shrink" relative to orbits as you zoom in (edges always tracked the viewport) and introduced stripes when `GridExtent` < viewport. Reverted to `R = GridExtent`, `D = 800`.
