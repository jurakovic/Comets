# OpenGL Rendering — Implementation Reference

This document describes the GPU-accelerated rendering implementation in `OrbitPanel`, which replaced the original GDI+ CPU renderer. See `3d-engine-migration.md` for the pre-implementation analysis and option comparison.

---

## Overview

`OrbitPanel` was rewritten from `Panel` (GDI+) to `GLControl` (OpenTK 4.x) while keeping all orbital mechanics, matrix math, and application logic untouched. The rendering pipeline is now fully GPU-driven with the CPU handling only orbit path computation and text label rasterization.

**Files changed:**

| File | Change |
|---|---|
| `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs` | Complete rewrite |
| `src/Comets.Core/Comets.Core.csproj` | Added OpenTK NuGet packages |
| `src/Comets.Application.OrbitViewer/Controls/OrbitViewerControl.cs` | Minor wiring |
| `src/Comets.Application.OrbitViewer/Controls/Toolbox/MiscControl.Designer.cs` | AA checkbox on by default |

**Dependencies added:**

```xml
<PackageReference Include="OpenTK.WinForms"    Version="4.0.0-pre.8" />
<PackageReference Include="OpenTK.GLControl"   Version="4.0.2" />
<PackageReference Include="OpenTK.Graphics"    Version="4.9.4" />
<PackageReference Include="OpenTK.Mathematics" Version="4.9.4" />
```

---

## Shader Programs

There are two separate GLSL 330 core shader programs compiled at startup.

### 1. Main Shader (orbits, axes, bodies, markers)

**Vertex shader** — applies world rotation and weak perspective projection:

```glsl
uniform mat4  uRot;               // view rotation matrix
uniform float uHalfX, uHalfY;    // half-width / half-height scale
uniform float uOffsetX, uOffsetY; // centering offset in NDC

out float vZ;

void main() {
    vZ = aPos.z;
    vec4 v = uRot * vec4(aPos, 1.0);
    float w = 1.0 + v.z / 625.0;  // weak perspective divisor
    gl_Position = vec4(
        v.x / uHalfX + uOffsetX * w,
        v.y / uHalfY + uOffsetY * w,
        0.0, w
    );
}
```

The `625.0` constant is derived from the original GDI+ formula:
```csharp
double mul = (Zoom * MinimumSize.Width) / (1500.0 * (1.0 + xyz.Z / 625.0));
```
Setting `gl_Position.w = 1 + Z/625` causes the hardware perspective divide to reproduce the same depth-of-field scaling the CPU renderer had.

**Fragment shader** — depth-based color split for lines, circular disc for points:

```glsl
uniform vec4 uColorUpper, uColorLower;
uniform int  uMode;   // 0 = line, 1 = point

in float vZ;

void main() {
    if (uMode == 1) {
        vec2 c = gl_PointCoord - vec2(0.5);
        if (dot(c, c) > 0.25) discard;   // clip to circle
        FragColor = uColorUpper;
    } else {
        FragColor = vZ >= 0.0 ? uColorUpper : uColorLower;
    }
}
```

The `vZ >= 0` color split (e.g. `ColorCometOrbitUpper` / `ColorCometOrbitLower`) is the same visual depth cue used by the GDI+ renderer.

### 2. Text Overlay Shader

Renders a fullscreen textured quad carrying GDI+-rasterized label bitmaps.

```glsl
// Vertex
layout(location = 0) in vec2 aPos;   // NDC xy  [-1..1]
layout(location = 1) in vec2 aUV;    // texture UV [0..1]
out vec2 vUV;
void main() { gl_Position = vec4(aPos, 0.0, 1.0); vUV = aUV; }

// Fragment
uniform sampler2D uTex;
void main() { FragColor = texture(uTex, vUV); }
```

---

## GPU Buffer Layout

### Orbit buffers (planet and comet paths)

Each orbit path gets its own VAO + VBO:

- **Format:** tightly-packed `float[n*3]` (X, Y, Z per vertex)
- **Planet orbits:** `BufferUsageHint.DynamicDraw` — recreated when date changes
- **Comet orbits:** `BufferUsageHint.DynamicDraw` — recreated when comet list changes
- **Vertex count:** `OrbitDivisionCount + 1` per orbit (typically 500 points)
- **Draw call:** `GL.DrawArrays(PrimitiveType.LineStrip, 0, count)`

### Body buffer (Sun, planets, comets, markers)

A single reusable VAO + VBO streams all body/marker vertices per frame:

- **Format:** `float[n*3]` — same X/Y/Z layout
- **Usage:** `BufferUsageHint.StreamDraw` — full upload every frame
- **Draw calls:** multiple `GL.DrawArrays(Points/Lines, offset, count)` sub-ranges

### Text quad buffer

A static VAO + VBO with 6 vertices (2 triangles) in NDC space:

```
Vertices (aPos, aUV):
(-1,-1, 0,1)  (1,-1, 1,1)  (1,1, 1,0)
(-1,-1, 0,1)  (1,1, 1,0)  (-1,1, 0,0)
```

UV Y is flipped so the GDI+ bitmap's top-left maps to the screen's top-left.

---

## Coordinate Transform Pipeline

```
Orbital mechanics (CPU)
    OVComet.GetPos() → Xyz[] in J2000 equatorial

Precession + obliquity (CPU)
    MtxToEcl = RotateX(obliquity) * PrecMatrix(JD2000, JD_now)
    Xyz_ecl  = Xyz_eq.Rotate(MtxToEcl)
    ↓ uploaded to VBO

Rotation (GPU — vertex shader)
    v = uRot * vec4(Xyz_ecl, 1.0)
    where uRot encodes RotateX(-RotateVert) * RotateZ(-RotateHorz)

Weak perspective projection (GPU)
    w           = 1 + v.z / 625.0
    gl_Position = (v.x/halfX + offsetX*w,  v.y/halfY + offsetY*w,  0,  w)

Hardware perspective divide
    screen xy  = gl_Position.xy / w   →   final pixel position
```

Rotation angles are negated relative to what is passed in because OpenTK uses row-major matrices while GLSL operates on column-major; negating flips the `sin` terms to produce the correct handedness.

---

## Per-Frame Render Sequence

1. `GL.Clear(ColorBufferBit | DepthBufferBit)` — black background
2. Enable blending, set viewport, bind main shader
3. Upload uniforms: `uRot`, `uHalfX/Y`, `uOffsetX/Y`, colors
4. **Planet orbits** — `GL.DrawArrays(LineStrip)` per planet in `OrbitDisplay`
5. **Comet orbits** — drawn conditionally per comet (respects `PreserveSelectedOrbit`)
6. **Axes** — if `ShowAxes`: 6 rays ±X/Y/Z via `GL.DrawArrays(Lines)`
7. **Bodies** — Sun (origin), planets, comets as `GL_POINTS`
   - Point size varies: Sun=6 px, planets=5 px, comets=2–6 px by magnitude
8. **Markers** — crosshair lines around selected comet (see below)
9. **Text overlay** — bind text shader, upload label bitmap as `GL_TEXTURE_2D`, draw quad
10. `SwapBuffers()`

---

## Crosshair Marker

The crosshair must remain screen-aligned even as the scene rotates. Rather than converting to NDC/pixel coordinates, the arms are built in view space and then inverse-rotated back to ecliptic space so the vertex shader re-applies the forward rotation and lands them at the correct screen positions.

```
arm_view = unit vector in screen space (e.g. right = [1,0,0])
arm_ecl  = InvRotateMtx(arm_view, MtxRotate)   // MtxRotate^T (transpose = inverse for orthogonal)
vertex   = comet_ecl ± arm_ecl * scale
```

`scale` is adjusted by the comet's weak perspective factor `wFactor = 1 + Z_ecl / 625.0` so arms stay a consistent screen size at all depths.

---

## Text Labels

All text is rendered with GDI+ `Graphics.DrawString()` onto a `Bitmap` (Format32bppArgb, full window resolution) and uploaded as a single `GL_TEXTURE_2D` overlay per frame.

| Label group | Font | Color |
|---|---|---|
| Info overlay (name, mag, distances) | Consolas 10pt bold | White |
| Date/time (bottom-right) | Consolas 10pt bold | White |
| Axis labels | Helvetica 8.5pt | Gray |
| Planet names | Helvetica 10pt | LimeGreen |
| Comet names | Helvetica 10pt | White (selected in multi-mode) / Peru |

---

## Antialiasing

Antialiasing is enabled by default (checkbox in MiscControl).

- **MSAA (8 samples):** configured on the `GLControl` constructor via `new GLControlSettings { NumberOfSamples = 8 }` — always active
- **Line smooth:** `GL.Enable(EnableCap.LineSmooth)` — toggled by the AA checkbox

---

## What Was Not Changed

The following are completely unchanged and decoupled from the rendering layer:

- `OVComet.cs` — orbital position computation
- `CometOrbit.cs`, `PlanetOrbit.cs` — pre-computed orbit path arrays
- `Matrix.cs`, `Xyz.cs` — 3D vector/matrix math
- All of `Comets.Core` — ephemeris, import/export, filtering
- All application-level UI forms and controls
