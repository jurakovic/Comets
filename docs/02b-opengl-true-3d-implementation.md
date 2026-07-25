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
uniform mat4 uEcl;   // equatorial J2000 -> ecliptic-of-date; identity for already-ecliptic geometry
out float vZ;

void main()
{
    vec4 pos = uEcl * vec4(aPos, 1.0);
    vZ = pos.z;                         // ecliptic Z — north/south of ecliptic plane
    gl_Position = uMVP * pos;
}
```

`vZ` carries the ecliptic Z coordinate through to the fragment shader to drive the above/below-ecliptic color split. It is **not** eye-space Z. Because `uEcl` may rotate the incoming vertex, `vZ` is taken from the **rotated** position — reading `aPos.z` would give equatorial Z for comet geometry and split the colors on the wrong plane.

### `uEcl` — precession applied on the GPU

Only comet orbits are uploaded unrotated (equatorial J2000). For those draws `uEcl` is set to `MtxToEcl`; for everything else — planet orbits, bodies, axes, grid, crosshair — it is identity, because that geometry is already rotated into ecliptic coordinates on the CPU before upload. `RenderScene()` toggles the uniform around the comet draw block and sets it back to identity afterwards.

`ToMatrix4()` performs the conversion. `Xyz.Rotate` treats the astronomy `Matrix` as row-by-column (`p' = M p`), while GLSL reads a uniform uploaded with `transpose: false` as column-major, so the matrix is transposed on the way in. The result is that `uEcl * pos` in the shader equals `pos.Rotate(mtx)` on the CPU.

The point of this is VBO lifetime: a change of date changes `MtxToEcl`, and with the rotation baked into the vertices every comet orbit buffer would have to be rebuilt on every simulation frame. Applying it in the shader makes a date change free for comet geometry.

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

Computed by `UpdateMVP()`, which is called from both `RenderScene()` and `UpdateCometPanelLocations()`. This ensures `_mvp`, `_view`, and `_orthoHalfH` are always current regardless of call order. `UpdateMVP()` is pure CPU math — no GL calls — so it is safe to invoke before the first rendered frame.

OpenTK stores `Matrix4` in **row-major** order (transposed relative to the mathematical column-major convention). GPU upload uses `transpose: false`, so GLSL receives the correct column-major form automatically.

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
private Point MvpProject(Xyz xyz)
{
    var v = new Vector4((float)xyz.X, (float)xyz.Y, (float)xyz.Z, 1.0f) * _mvp;
    float ndcX  = v.X / v.W;
    float ndcY  = v.Y / v.W;
    int screenX = (int)((ndcX + 1f) / 2f * Width);
    int screenY = (int)((1f - ndcY) / 2f * Height);
    return new Point(screenX, screenY);
}
```

Always returns a valid `Point`. With an orthographic projection `W` is always `1.0` after transformation, so there is no "behind the camera" concept and a null return path would be dead code.

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

- **Planet orbits**: one VAO/VBO per planet, rebuilt whenever `_planetVbosNeedUpdate` is set (rotation matrix or planet elements changed). `LineStrip` topology.
- **Comet orbits — individual**: only the selected comet and any marked comets get their own VAO/VBO, because they can be drawn in a color of their own. Stored in `_cometOrbitBuffers` as a `Dictionary<int, (vao, vbo, count)>` keyed by comet index. `LineStrip` topology.
- **Comet orbits — batch**: when `Object.Comet` is in `OrbitDisplay` (the "all comet orbits" checkbox), every *visible* comet's path is concatenated into one buffer (`_cometBatchVao`/`_cometBatchVbo`) and drawn with a single `GL.MultiDrawArrays` using the per-strip offset/length arrays `_cometBatchStarts`/`_cometBatchCounts`. One draw call for the whole collection instead of one per comet, which is what makes displaying a few thousand orbits practical. All strips in the batch share the standard comet orbit colors; the selected and marked comets are then redrawn on top from their individual buffers.
- **Bodies / crosshair**: a single shared streaming VAO/VBO (`_bodyVao`/`_bodyVbo`) re-filled each frame with `BufferUsageHint.StreamDraw` for Sun, planets, comets, axes, and crosshair lines.

Planet orbit VBOs store ecliptic float3 positions; comet orbit VBOs (both individual and batch) store **unrotated equatorial J2000** positions and rely on `uEcl` — see the shader section above. The model matrix (centering) is applied on the GPU via `uMVP`, so the same buffer works regardless of which object is centered.

### Comet VBO lifecycle

`CometOrbit` objects are built on demand in `UploadOrbitsToGpu` and discarded immediately after upload — they are never stored between frames. Two flags drive updates:

| Flag | Set when | Effect |
|---|---|---|
| `_planetVbosNeedUpdate` | Rotation matrix or planet elements changed (i.e. the date moved) | Rebuilds the 8 planet VBOs. Comet VBOs are untouched |
| `_cometVbosDirty` | The comet set, its visibility, the selection, or the marking changed | Adds VBOs for newly required comets, deletes VBOs for no longer required ones, and rebuilds the batch buffer |

A change of date deliberately does **not** set `_cometVbosDirty`: comet vertices are unrotated and the shader applies the new `MtxToEcl` on its own.

`_cometVbosDirty` is set from:

- `InvalidateCometVbos()` — called by `OrbitViewerControl` when `IsMarked` changes, when the comet-orbit display checkbox is toggled (the batch holds exactly the comets that were visible when it was built), and after filtering, which can change the comet list itself
- `UpdatePositions()` — when `UpdateCometVisibility()` reports that at least one comet's `IsVisible` actually flipped. The method returns `bool` for this reason; a date filter that changes what is visible mid-simulation must rebuild the batch, but the common case of no change must not
- `LoadPanel` / `SetSelectedComet` and the reset path in `SetPaintEnabled`

Assigning `ATime` now also runs `UpdatePlanetOrbit()` and `UpdateRotationMatrix()`, so callers no longer have to remember the trio — a date change is a single property write.

---

## Rendering Order

Each frame in `OnPaint`:

1. `ResolveCenteredIndex` — resolves `CenteredIndex` for camera target centering. Extracted from `OnPaint` so the frame-capture path shares it
2. `GL.Clear` — color + depth
3. `RenderScene` — calls `UpdateMVP()`, draws all orbit lines and body dots via OpenGL
4. `SwapBuffers`
5. `UpdateCometPanelLocations` — calls `UpdateMVP()` then projects comet positions to screen coords for the info panel

Within `RenderScene`, in order:

1. Upload any dirty VBOs, then `UpdateMVP()`
2. Antialiasing state (`Multisample`, `LineSmooth`) from the `Antialiasing` property
3. `uEcl` = identity
4. **Grid** — `LineWidth(1.0f)`, `RenderGrid()` if `ShowGrid`. First, so orbit lines draw over it (see `05a-ecliptic-grid-implementation.md`)
5. `LineWidth(1.5f)` for everything below
6. **Planet orbits** — skipped per planet when `Zoom * GetPlanetAU(planet) < 15.0`
7. `uEcl` = `MtxToEcl`, then **comet orbits**: the batched `MultiDrawArrays` first, then per-comet draws for the selected and marked comets in their own colors. `uEcl` back to identity
8. **Axes** — `RenderAxes()` if `ShowAxes`
9. **Bodies** — `RenderBodies()`: Sun, planets, comets, crosshair
10. **Labels** — `RenderLabels()`: GDI+ bitmap quad, text projected with `MvpProject`

---

## Shader Compilation Is Checked

`CompileShader(type, source, name)` and `LinkProgram(vs, fs, name)` check `CompileStatus` / `LinkStatus` and throw `InvalidOperationException` with the driver info log on failure.

Without the check a failure is silent in a particularly unhelpful way: on some drivers the program still links to a non-zero id, so `_shaderProgram != 0` passes and the panel simply renders black — indistinguishable from an empty scene. `LinkProgram` also detaches both shaders before returning, so the caller's `GL.DeleteShader` actually frees them.

---

## Disposal

`Dispose(bool)` deletes everything `InitGL` and `UploadOrbitsToGpu` allocated: both programs, the body VAO/VBO, the comet batch VAO/VBO, the text quad VAO/VBO, the label texture, and every planet and comet orbit buffer.

This matters because the orbit viewer is an MDI child: each open and close of the window built a fresh set of programs, VAOs, VBOs and a label texture, and without disposal they lived for the remaining lifetime of the GL context rather than the lifetime of the panel.

Deleting GL names requires the owning context to be current, so the body is wrapped in `MakeCurrent()` inside a `try` that swallows `InvalidOperationException`/`ObjectDisposedException` — if the context cannot be made current it is already gone, which frees the objects anyway. All handle fields are then zeroed and `_glLoaded` is cleared.

---

## Frame Capture — `CaptureFrame()`

Returns the current scene as a `Bitmap`, used by the *Save image* button.

`Control.DrawToBitmap` does not work on this panel: it asks the control to paint itself through GDI, and the scene exists only in the OpenGL framebuffer, so what came back was an empty image.

Instead the panel renders into the back buffer and reads it straight back **without swapping**, so capturing does not disturb what is currently on screen:

```csharp
GL.Clear(ColorBufferBit | DepthBufferBit);
RenderScene();
GL.Finish();
GL.ReadBuffer(ReadBufferMode.Back);
GL.ReadPixels(0, 0, Width, Height, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
```

The context is multisampled; the read resolves it down on the way out. OpenGL's first row is the bottom of the image while GDI+ expects it to be the top, so the result is flipped with `RotateFlipType.RotateNoneFlipY`. Returns `null` when there is no drawable surface (zero size, or `MakeCurrent` fails).

`OrbitViewerControl.Save()` captures **before** showing the save dialog. A modal dialog runs its own message loop, which keeps delivering the simulation timer's ticks, so the scene carries on moving while the dialog is open and a capture taken afterwards would save a moment the user never chose.
