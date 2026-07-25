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

## Axes: Lines and Labels Are Separate Options

Two independent properties, two checkboxes in the Misc panel:

| Property | Checkbox | Draws |
|---|---|---|
| `ShowAxes` | Show axes | The 6 axis rays, in `RenderAxes()` |
| `ShowAxesLabels` | Show axes labels | The axis end names, in `RenderLabels()` |

The labels sit at the ends of the axis lines, so they are only meaningful while the lines are drawn: the label draw is gated on `ShowAxes && ShowAxesLabels`, and `MiscControl.SyncShowAxesLabelsEnabled()` greys the labels checkbox out whenever *Show axes* is unchecked. The split exists because the axis rays are a useful orientation cue on their own, while the names are text competing with the comet and planet labels.

---

## Adaptive Step Selection

The grid spacing adapts to zoom level so lines remain readable at any scale:

```
idealStep  = 50px / (pixelsPerAU × elevFactor)
```

`elevFactor = |cos(RotateVert)|` corrects for foreshortening — at edge-on view the lines appear much closer together, so finer steps would be needed to maintain the target 50 px gap.

Steps are snapped to a set of "nice" values: `{ 0.1, 0.2, 0.5, 1, 2, 5, 10, 25 }`. The largest step ≤ `idealStep` is chosen, then stepped down while fewer than `minCells` (9) cells would be shown within `GridExtent`.

The step-down stops early if the next finer step would put lines closer together than `minSpacingPx` (15 px) on screen — so a small `GridExtent` at low zoom gets fewer than 9 cells rather than an unreadable mesh. 0.1 AU is the finest step available and 25 AU the coarsest, which at the default 150 AU extent bounds the grid at 6 cells per side.

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

`GridExtent` (default 150 AU) controls the boundary of the drawn grid lines — and also the length of the axis lines — and can be changed at runtime via the **Extent** textbox in the Misc panel. The field is always enabled; applying a valid value automatically checks "Show grid" if it was unchecked (same behaviour as the Filter on Date panel). Only numeric input is accepted.

Typical use: set to 5 AU when studying inner-planet orbits so fine grid lines appear around Mercury/Venus/Earth/Mars without showing the full outer solar system scale.

### When the value is applied

`MiscControl.ApplyGridExtent()` runs on **Enter** (`KeyDown`) and on **leaving** the textbox — not on every keystroke. Typing "100" applies once, instead of rendering at 1, then 10, then 100.

Three guards sit in `ApplyGridExtent`:

- **Clamp to `MaxGridExtent` (150).** `ValNumManager` rejects a typed character that would exceed the maximum, but a paste bypasses that filter, and an unbounded extent costs thousands of grid line uploads per frame. The clamped value is written back into the textbox.
- **No-op when unchanged.** `_appliedGridExtent` records what was last handed to the panel, seeded in the constructor from the textbox's designer value (which matches the panel's own default). Without this check, tabbing through the toolbox — which leaves every control in turn — would tick *Show grid* on the way past without anything having been typed.
- **Reject non-positive / unparseable** input, leaving the previous extent in force.

There is no `MiscControl.SetGridExtent()`; the extent only ever travels outward, from the textbox to the panel via `OnGridExtentChanged`.

For the keyboard to reach the textbox at all, `OrbitViewerControl_KeyDown` has to let it past: the form previews every key press, so `Keys.Enter`, `Keys.Delete` and `Keys.Back` are excluded from the shortcut handling while `miscControl.ContainsFocus`. Otherwise Enter would mark a comet instead of committing the typed value, and Delete would unmark every comet instead of deleting a digit.

---

## Rendering Order and Depth

Grid is rendered before planet orbits so orbit lines draw on top. Grid lines are drawn at `LineWidth = 1.0f`; all other lines (orbits, axes) use `1.5f`.

When the grid is at full opacity (`alpha ≥ 0.99`) it writes to the depth buffer normally, so it can occlude geometry behind it. Once the elevation fade begins (`alpha < 0.99`), depth writes are disabled (`GL.DepthMask(false)`) for the grid draw call and restored immediately after. This prevents a nearly-invisible grid from silently cutting orbit lines that would otherwise be visible behind it.

---

## Files Changed

| File | Change |
|------|--------|
| `OrbitPanel.cs` | `RenderGrid()`, `AddPerspVertex()`, `PseudoPerspective(xyz, D)`, `ColorGrid`, `ShowGrid`, `GridExtent`, `ShowAxesLabels` properties, `RenderScene()` call, axis tips/labels updated; line width 1.0f for grid; depth-mask guard on fade |
| `MiscControl.cs` | `OnShowGridChanged`, `OnGridExtentChanged`, `OnShowAxesLabelsChanged` events; `ApplyGridExtent()` on Enter/Leave with clamp, change check and auto-check of *Show grid*; numeric `KeyPress` validation; `SyncShowAxesLabelsEnabled()` |
| `MiscControl.Designer.cs` | `cbxShowAxesLabels` added; *Show grid*, Extent label + textbox and *Save image* shifted down; panel height 123 |
| `OrbitViewerControl.cs` | `SetShowGrid()`, `SetGridExtent()`, `SetShowAxesLabels()` handlers; `miscControl.ContainsFocus` added to the shortcut guard, with Enter/Delete/Back let through |
| `OrbitViewerControl.Designer.cs` | `cpnlMisc` working area height 123, `HeightExpanded` 158 |

The Misc panel has grown twice — once for the grid controls and again for the axes-labels checkbox — so only the current heights are recorded here rather than a chain of deltas.

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

**Solid plane**: replaced the grid with a semi-transparent filled quad. The grid was preferred for its cleaner depth cue. The commits were rebased away and are no longer reachable.

**Drag direction fix** (`73460ed` / reverted `32bfabd`): a fix for horizontal drag reversing when viewing from below the ecliptic. Was reverted because the pseudo-perspective grid itself creates strong orientation cues that make the pre-fix behavior feel natural again. (An earlier pair, `9f4a000` / `13395c0`, is the same change on a since-rebased branch.)

**Axis length tied to GridExtent**: originally reverted because axes appeared as tiny stubs at small `GridExtent`. Later re-enabled — `SizeAU = GridExtent`, `D = GridExtent × (800/150)` — so axis lines and grid lines always share the same extent and the same perspective strength. This is what ships.

**Viewport-relative R**: tried making grid line endpoints track the viewport half-width so the perspective ratio `D/R` stays constant at every zoom. This caused lines to visibly "shrink" relative to orbits as you zoom in (edges always tracked the viewport) and introduced stripes when `GridExtent` < viewport. Reverted to `R = GridExtent`, with `D` scaling from it as `extent × (800/150)`.
