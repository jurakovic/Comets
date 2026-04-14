# OpenGL 3D Rendering — Implementation Reference

Documents the current rendering pipeline in `OrbitPanel.cs` after the true-3D migration.

---

## Overview

The orbit viewer renders in a standard MVP (Model-View-Projection) pipeline using OpenGL 4.x via OpenTK. Orbital mechanics and coordinate math are unchanged; only the rendering path changed.

All orbital positions are in **ecliptic J2000 coordinates** (AU). The ecliptic north pole is along **+Z**, the vernal equinox along **+X**.

---

## Shaders

### Vertex shader

```glsl
#version 330 core
layout (location = 0) in vec3 aPos;
uniform mat4 uMVP;
out float vZ;

void main()
{
    vZ = aPos.z;                        // ecliptic Z — north/south of ecliptic plane
    gl_Position = uMVP * vec4(aPos, 1.0);
}
```

`vZ` carries the ecliptic Z coordinate (unchanged from the world position) through to the fragment shader to drive the above/below-ecliptic color split. It is **not** eye-space Z.

### Fragment shader

```glsl
#version 330 core
in float vZ;
uniform vec4 uColorUpper;
uniform vec4 uColorLower;
uniform int uMode; // 0 = orbit line, 1 = body dot

out vec4 FragColor;

void main()
{
    if (uMode == 1)
    {
        // Smooth circular point sprite
        float d = length(gl_PointCoord - vec2(0.5));
        float alpha = 1.0 - smoothstep(0.5 - fwidth(d), 0.5, d);
        if (alpha == 0.0) discard;
        FragColor = vec4(uColorUpper.rgb, uColorUpper.a * alpha);
    }
    else
    {
        // Above/below ecliptic color split (depth cue)
        FragColor = vZ >= 0.0 ? uColorUpper : uColorLower;
    }
}
```

---

## MVP Matrix

Built every frame in `RenderScene()`. OpenTK stores `Matrix4` in **row-major** order (transposed relative to the mathematical column-major convention). GPU upload uses `transpose: false`, so GLSL receives the correct column-major form automatically.

CPU-side multiplication order (reversed from math convention due to row-major storage):

```csharp
_mvp = model * view * projection;
GL.UniformMatrix4(_uMVP, false, ref _mvp);
```

CPU-side projection of a world position (row-vector multiplication):

```csharp
var clip = new Vector4(worldX, worldY, worldZ, 1.0f) * _mvp;
```

### Projection matrix

Orthographic, with a symmetric depth range so orbits that cross the camera plane render as complete ellipses without clipping:

```csharp
const float refFovY = MathF.PI / 4f;   // 45° reference — defines scene scale, not frustum shape
float camDist    = 1800f / (float)Zoom;
float orthoHalfH = camDist * MathF.Tan(refFovY / 2f);
float halfDepth  = camDist + 500f;      // symmetric near/far
Matrix4 projection = Matrix4.CreateOrthographic(
    orthoHalfH * aspect * 2f, orthoHalfH * 2f, -halfDepth, halfDepth);
```

### View matrix

Built analytically from `RotateHorz` (azimuth, degrees) and `RotateVert` (elevation, degrees) — no `LookAt` call, avoiding gimbal-lock singularities.

The effective scene rotation is `R = RotateX_std(-v) × RotateZ_std(-h)`, matching the original CPU rotation convention. In OpenTK row-major notation (Row i = math column i of V):

```csharp
float h = RotateHorz * PI / 180f;
float v = RotateVert * PI / 180f;

Matrix4 view = new Matrix4(
    new Vector4( cos(h), -cos(v)*sin(h),  sin(v)*sin(h), 0),
    new Vector4( sin(h),  cos(v)*cos(h), -sin(v)*cos(h), 0),
    new Vector4( 0,       sin(v),          cos(v),         0),
    new Vector4( 0,       0,              -camDist,          1)
);
```

Camera basis vectors in world space (ecliptic):
- **Right**: `view.Column0.Xyz = (cos(h), sin(h), 0)`
- **Up**: `view.Column1.Xyz = (-cos(v)·sin(h), cos(v)·cos(h), sin(v))`
- **Forward** (into screen): `view.Column2.Xyz = (sin(v)·sin(h), -sin(v)·cos(h), cos(v))`

At default orientation (`h = 0, v = 0`): camera sits on the +Z axis looking toward the Sun, with +Y (ecliptic summer solstice) pointing up. The ecliptic plane lies flat in the viewport.

### Zoom → camera distance

```csharp
float camDist = 1800f / (float)Zoom;
```

`Zoom` comes from the scroll wheel / scroll bar. Higher zoom = smaller `camDist` = camera moves closer.

### Model matrix — centering

```csharp
Vector3 target = Vector3.Zero;  // default: Sun at origin

if (CenteredObject == Object.Comet && CenteredIndex >= 0)
{
    Xyz p = CometsPos[CenteredIndex].Rotate(MtxToEcl);
    target = new Vector3((float)p.X, (float)p.Y, (float)p.Z);
}
else if (Planets.Contains(CenteredObject) && PlanetsPos[CenteredObject] != null)
{
    Xyz p = PlanetsPos[CenteredObject];
    target = new Vector3((float)p.X, (float)p.Y, (float)p.Z);
}

Matrix4 model = Matrix4.CreateTranslation(-target);
```

The model matrix shifts the entire world so the centered object lands at the camera's look-at point. No shader uniform changes needed for centering.

---

## Depth Testing

Enabled in `InitGL()`:

```csharp
GL.Enable(EnableCap.DepthTest);
GL.DepthFunc(DepthFunction.Less);
```

`GLControl` is created with `DepthBits = 24`. The `GL.Clear` call each frame clears both the color and depth buffers:

```csharp
GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
```

All orbit lines are fully opaque, so depth sorting is handled entirely by the GPU.

---

## Coordinate Systems

| Space | Description |
|---|---|
| Equatorial J2000 | Raw comet positions from `CometsPos` |
| Ecliptic J2000 | After applying `MtxToEcl` rotation: `CometsPos[i].Rotate(MtxToEcl)` |
| Eye space | After view matrix: camera at origin, scene in front |
| Clip / NDC | After projection + perspective divide |
| Screen pixels | NDC mapped to `[0, Width] × [0, Height]` |

Planet positions (`PlanetsPos`) are already in ecliptic J2000 and do not need `MtxToEcl`.

`MtxToEcl` is computed from precession and obliquity of the ecliptic for the current epoch and updated whenever the date changes.

---

## CPU-Side Projection — `MvpProject`

Used for text label placement (GDI+). Converts an ecliptic world position to screen pixels using the same `_mvp` matrix uploaded to the GPU:

```csharp
private Point? MvpProject(Xyz xyz)
{
    var v = new Vector4((float)xyz.X, (float)xyz.Y, (float)xyz.Z, 1.0f) * _mvp;
    if (v.W <= 0f) return null;  // behind the camera — skip label
    float ndcX  = v.X / v.W;
    float ndcY  = v.Y / v.W;
    int screenX = (int)((ndcX + 1f) / 2f * Width);
    int screenY = (int)((1f - ndcY) / 2f * Height);
    return new Point(screenX, screenY);
}
```

Returns `null` when the point is behind the camera (`W ≤ 0`). Label drawing call sites check for `null` and skip `DrawString`. `UpdateCometPanelLocations` unwraps with `?? new Point(-9999, -9999)` to keep the non-nullable `PanelLocation` property compatible with the click hit-test.

Used for: axis labels, planet name labels, comet name labels, comet panel locations.

---

## Crosshair

Built in world (ecliptic) space using the camera right and up vectors extracted from the view matrix. Four arms — up, down, left, right — each split into an inner gap and outer segment:

```csharp
Vector3 right = _view.Column0.Xyz;   // camera right in world space
Vector3 upVec = _view.Column1.Xyz;   // camera up in world space

// Perspective-correct arm length: 1 px = depth * tan(fovY/2) / (Height/2) AU
float depth  = _camDist - Vector3.Dot(pVec - _cameraTarget, _view.Column2.Xyz);
float pxSize = depth * tan(PI/8) / (Height / 2f);
float off    = (diameter + 4) * pxSize;  // gap radius
float len    = (diameter + 8) * pxSize;  // arm tip radius
```

The 8 arm endpoint vertices are uploaded directly in ecliptic space and transformed by the same `_mvp`, so they automatically follow centering and camera orientation.

---

## GPU Buffers

- **Planet orbits**: one VAO/VBO per planet, rebuilt whenever `_vbosNeedUpdate` is set (rotation matrix or planet elements changed). `LineStrip` topology.
- **Comet orbits**: lazy — only the selected comet and any marked comets get a VAO/VBO. Stored in `_cometOrbitBuffers` as a `Dictionary<int, (vao, vbo, count)>` keyed by comet index. `LineStrip` topology.
- **Bodies / crosshair**: a single shared streaming VAO/VBO (`_bodyVao`/`_bodyVbo`) re-filled each frame with `BufferUsageHint.StreamDraw` for Sun, planets, comets, axes, and crosshair lines.

Orbit VBOs store raw ecliptic float3 positions. The model matrix (centering) is applied on the GPU via `uMVP`, so the same buffer works regardless of which object is centered.

### Comet VBO lifecycle

`CometOrbit` objects are built on demand in `UploadOrbitsToGpu` and discarded immediately after upload — they are never stored between frames. Two flags drive updates:

| Flag | Set when | Effect |
|---|---|---|
| `_vbosNeedUpdate` | Rotation matrix or planet elements changed | Rebuilds planet VBOs and all existing comet VBOs |
| `_cometVbosDirty` | Selection or marking changed | Adds VBOs for newly required comets, deletes VBOs for no longer required ones |

`OrbitViewerControl` calls `orbitPanel.InvalidateCometVbos()` whenever `IsMarked` changes on any comet.

---

## Rendering Order

Each frame in `OnPaint`:

1. `GL.Clear` — color + depth
2. `RenderScene` — uploads MVP, draws all orbit lines and body dots via OpenGL
3. `RenderLabels` — GDI+ bitmap quad: text overlays projected with `MvpProject`
4. `SwapBuffers`
5. `UpdateCometPanelLocations` — projects comet positions to screen coords for the info panel
