# Orbit Sampling Analysis – Comets vs. Celestia Reference

## Overview

Comparison of the current `CometOrbit.cs` sampling implementation against the Celestia 1.6.x `orbit.cpp` reference (see `docs/orbit.cpp` and `docs/comet-orbits.md`).

---

## What's Already Correct

- **All three conic sections handled** — elliptic, hyperbolic, parabolic branches exist
- **Open orbits sampled symmetrically** — hyperbolic and parabolic both start at index 250 (pericenter), fill outward in both directions; `LineStrip` draws correctly from one limb to the other
- **Hyperbolic range limit** — `arccosh((MaxOrbitAU + a) / (a*e))` is the correct formula for F at distance 150 AU
- **Parabolic range limit** — `atan(sqrt(MaxOrbitAU/q - 1)) * 2` is the correct V limit

---

## Issues

### 1. No Curvature-Adaptive Sampling

Current approach uses **uniform or quadratic anomaly steps** regardless of eccentricity.

| Case | Current method | Celestia method |
|---|---|---|
| Elliptic, aphelion > 150 AU | Quadratic: `E = dE * i²` | Curvature: `k = √w / (sin²E + w·cos²E)^(3/2)` |
| Elliptic, aphelion ≤ 150 AU | Uniform `E` | Same curvature formula, two-leg approach |
| Hyperbolic | Uniform `F` | Curvature: `k = b / (sinh²E + b²·cosh²E)^(3/2)` |
| Parabolic | Uniform `V` | Curvature: `k = kMax / (1 + D²)^(3/4)` |

For **elliptic orbits** the uniform-E approach is problematic at high eccentricities — near perihelion, curvature spikes but E steps are the same size as in the low-curvature aphelion region. The quadratic approximation helps for the open-arc case but is a heuristic rather than the physically correct formula.

The correct elliptic curvature formula from the Celestia reference:

```
k = sqrt(w) / (sin²E + w·cos²E)^(3/2)   where w = 1 - e²
```

> **Warning from Celestia docs:** An earlier (incorrect) version used `w / (sin²E + w²·cos²E)^(3/2)` — wrong exponents that under-estimated curvature in the 0.1–5 AU zone, producing visibly jagged segments even when the pericenter region itself was smooth.

The curvature-based step size:

```
dE_actual = dE_base / clamp(k, 1, kMax)
```

This automatically produces fine steps where the orbit bends sharply and coarse steps where it is nearly straight.

The uniform-F and uniform-V approaches for hyperbolic and parabolic orbits are less urgent — both parameterizations already concentrate samples near pericenter naturally — but the curvature formulas give the physically optimal distribution.

---

### 2. MaxOrbitAU: 150 AU vs. 500 AU

| | Current | Celestia |
|---|---|---|
| Cap | 150 AU | 500 AU |

Hale-Bopp's aphelion is ~360 AU; typical long-period comets have aphelia 100–500 AU. At 150 AU the orbit arc may appear noticeably short for these objects. The 500 AU threshold was chosen by Celestia because it is the natural level where near-parabolic elliptic orbits (e = 0.9999) become visually indistinguishable from true open arcs.

Single constant change in `CometOrbit.cs`:
```csharp
private const double MaxOrbitAU = 150.0;  // → 500.0
```

---

### 3. Near-Parabolic Elliptic Orbits (dynamic kMax)

For orbits with `e` just below 1 (e.g. e = 0.9999), `w = 1 - e²` is tiny (~0.0002). Curvature at pericenter reaches `1/w ≈ 5000`. A fixed kMax would under-sample pericenter badly for these cases.

Celestia's solution: **dynamic kMax** that scales with eccentricity:

```
kMax = max(20.0, (1/w)^(2/3))
```

For e = 0.9999 this gives kMax ≈ 340 instead of 20, providing enough refinement to keep pericenter segments visually smooth. For typical orbits (e < 0.994, w > 0.011) the formula evaluates to 20 and behaviour is unchanged.

Equivalent formulas for the other conic sections:

```
kMax = max(20.0, (1/b)^(2/3))   [hyperbolic,  b = sqrt(e²-1)]
kMax = max(20.0, D_max^(4/3))   [parabolic,   D_max = sqrt(r_max/q - 1)]
```

---

### 4. Closed Elliptic Two-Leg Approach (minor)

For small orbits (aphelion ≤ 150 AU), the current code loops E from 0 to `2π/4` and fills 4 quadrant indices simultaneously. This is valid.

The Celestia two-leg strategy (step outward from E=0 in both directions, buffer the inbound leg and emit in reverse) exists specifically for very high eccentricities where a single uniform pass from `-π` to `+π` overshoots the perihelion zone on the return leg. For orbits with aphelion ≤ 150 AU the issue is unlikely to appear in practice at 500 samples.

---

## Recommended Changes (Priority Order)

| Priority | Change | Impact | Effort |
|---|---|---|---|
| 1 | `MaxOrbitAU` 150 → 500 | Better arc coverage for long-period comets | Trivial |
| 2 | Curvature-adaptive sampling for elliptic orbits | Smooth pericenter for high-e comets | Medium |
| 3 | Dynamic `kMax` for all conic sections | Handles near-parabolic extremes | Small (add to above) |
| 4 | Curvature-adaptive for hyperbolic/parabolic | Marginal improvement; uniform-F/V already reasonable | Medium |

---

## Reference Curvature Formulas

### Elliptic

```
w    = 1 - e²
k    = sqrt(w) / (sin²E + w·cos²E)^(3/2)
kMax = max(20.0, (1/w)^(2/3))
dE_actual = dE_base / clamp(k, 1, kMax)
```

### Hyperbolic

```
b    = sqrt(e²-1)
k    = b / (sinh²E + b²·cosh²E)^(3/2)
kMax = max(20.0, (1/b)^(2/3))
dE_actual = dE_base / clamp(k, 1, kMax)
```

### Parabolic (D = tan(ν/2))

```
D_max = sqrt(r_max/q - 1)
k     = kMax / (1 + D²)^(3/4)
kMax  = max(20.0, D_max^(4/3))
dD_actual = dD_base / clamp(k, 1, kMax)
```

The `3/4` exponent gives step ∝ D^(3/2) for large D — producing constant relative chord error (sag/distance) across all distances, i.e. uniform visual quality from pericenter out to the bounding radius.

---

---

## Projection

### Background

The old GDI renderer used a **weak-perspective** (near-orthographic) formula:

```csharp
// master branch — GetDrawPoint
double mul = (Zoom * MinimumSize.Width) / (1500.0 * (1.0 + xyz.Z / 625.0));
```

The depth term `1 + xyz.Z / 625` places the vanishing point at Z = −625 AU. For typical inner solar system orbits (a few AU), `Z / 625 ≈ 0`, making the projection essentially **orthographic** — you see the true geometric shape of the orbit.

The OpenGL renderer initially used a **45° perspective FOV**, which produces noticeable foreshortening: the near side of an orbit appears wider than the far side, turning a near-circular orbit into a horseshoe shape when viewed from the side. Celestia uses the same effect (it is a space simulator, not a charting tool).

### Projection options compared

| Option | Shape accuracy | Depth cues | Notes |
|---|---|---|---|
| Orthographic | Exact — circles look like circles | None | Best for orbit analysis; matches old app feel |
| Narrow FOV (≈ 15°) | Near-exact for typical zoom levels | Slight | Middle ground; camera pulled ~3× further back |
| Wide FOV (45°) | Distorted — horseshoe effect visible | Strong | Immersive but misleading for orbit geometry |

### Current implementation (orthographic)

`OrbitPanel.cs` — `RenderScene()`:

```csharp
const float refFovY = MathF.PI / 4f;          // 45° reference — defines scale, not frustum shape
float camDist       = 1800f / (float)Zoom;
float orthoHalfH    = camDist * MathF.Tan(refFovY / 2f);

Matrix4 projection = Matrix4.CreateOrthographic(
    orthoHalfH * aspect * 2f,
    orthoHalfH * 2f,
    0.001f,
    camDist * 2f + 500f);
```

`orthoHalfH` is derived from the same `camDist` and 45° reference angle so **zoom behaviour is identical** to what a 45° perspective camera would show at the centre plane — switching between the two modes does not rescale the scene.

The far plane is `camDist * 2 + 500` (dynamic) rather than a hardcoded constant, so the full scene is always visible regardless of zoom level.

### Crosshair/dot sizing

In perspective mode, the world-unit-to-pixel ratio depends on depth:

```csharp
float pxSize = depth * MathF.Tan(MathF.PI / 4f / 2f) / (Height / 2f);
```

In orthographic mode, every depth maps to the same pixel size, so `depth` is replaced by the constant `_orthoHalfH`:

```csharp
float pxSize = _orthoHalfH / (Height / 2f);
```

`_orthoHalfH` is stored as a field alongside `_camDist` and updated each frame in `RenderScene()`.

---

## Files

| File | Role |
|---|---|
| `src/Comets.OrbitViewer/OrbitViewer/CometOrbit.cs` | Orbit sampling — target for all changes |
| `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs` | Rendering, projection, crosshair sizing |
| `docs/orbit.cpp` | Celestia reference implementation |
| `docs/comet-orbits.md` | Celestia implementation notes |
