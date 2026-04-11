# True 3D Rendering — Implementation Plan

Migrating from the current weak perspective hack to a proper MVP pipeline with depth testing.

---

## What changes and what stays

**Stays the same:**
- All orbital mechanics and coordinate math (unchanged)
- VAO/VBO structure and orbit data upload
- Text overlay (GDI+ bitmap quad)
- Color scheme, antialiasing, UI controls

**Changes:**
- Vertex shader: custom projection → standard MVP matrix
- Camera: rotation angles → LookAt + perspective
- Centering: NDC pixel offset → camera target point
- Zoom: half-width scale → camera distance or FOV
- Depth test: disabled → enabled
- Depth-cue color split: keep or replace (see Phase 4)
- Crosshair: inverse rotation hack → clip-space billboard

---

## Phase ordering

Phases 1–4 alone will leave labels visually broken because `GetDrawPoint` still uses the weak-perspective formula and `MtxRotate` still drives label positions. Do these phases together in a single pass:

- **Pass 1: Phases 1 + 2 + 3 + 4 + 8** — MVP matrix, LookAt camera, depth, and replace `GetDrawPoint` with the standard MVP project. After this pass the viewer is fully correct.
- **Pass 2: Phase 5** — swap centering to camera target, remove `X0`/`Y0`.
- **Pass 3: Phase 6** — crosshair cleanup, remove `InvRotateMtx`.
- **Phase 7** — optional polish, independent of the above.

---

## Phase 1 — MVP matrix in the vertex shader

Replace the five separate uniforms (`uRot`, `uHalfX`, `uHalfY`, `uOffsetX`, `uOffsetY`) and the custom `w` formula with a single `uMVP` matrix.

**`vZ` semantics — keep world-space Z, not eye-space Z.** Currently `vZ = aPos.z` is the ecliptic-space Z coordinate (north = positive), which is what makes `vZ >= 0` mean "north of the ecliptic plane." Using `vZ = viewPos.z` (eye-space Z) depends on camera orientation and has no connection to the ecliptic plane — the color split would show a random cut that rotates with the camera. Keep `vZ = aPos.z` in the new shader; the `uView` uniform is **not** needed in the vertex shader.

```glsl
#version 330 core
layout (location = 0) in vec3 aPos;
uniform mat4 uMVP;
out float vZ;

void main()
{
    vZ = aPos.z;                          // ecliptic Z — unchanged semantics
    gl_Position = uMVP * vec4(aPos, 1.0);
}
```

**OpenTK matrix convention.** OpenTK `Matrix4` is row-major in memory; GLSL expects column-major. `GL.UniformMatrix4(..., transpose: false, ...)` sends the matrix as-is, so GLSL receives the transpose. Use `transpose: true` and standard right-to-left multiplication order:

```csharp
Matrix4 model      = Matrix4.Identity;
Matrix4 view       = Matrix4.LookAt(eye, target, up);
Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(fovY, aspect, near, far);
Matrix4 mvp        = projection * view * model;   // right-to-left: model first
GL.UniformMatrix4(_uMVP, true, ref mvp);          // transpose: true → correct column-major in GLSL
```

**Clip planes:** `near = 0.001f` AU (well inside Mercury), `far = 2000f` AU (covers Oort cloud comets if needed, or 500 AU for practical use).

**Depth buffer:** `GLControlSettings` currently only sets `NumberOfSamples = 8`. Verify that OpenTK's `GLControl` allocates a depth buffer by default; if not, add `DepthBits = 24` (or similar) to the settings, otherwise `GL.Enable(EnableCap.DepthTest)` will silently do nothing.

---

## Phase 2 — Camera from rotation angles

Currently `RotateHorz`/`RotateVert` are rotation angles applied to the scene. In true 3D they become the camera's azimuth and elevation orbiting around the origin (the Sun).

**Coordinate convention — verify `up` before coding.** The VBOs are already in ecliptic coordinates (after `MtxToEcl`). In that frame the north ecliptic pole is along **+Z**, not +Y. Before wiring up `LookAt`, confirm which axis is screen-up at `RotateVert = 0` in the existing viewer. If it is Y use `Vector3.UnitY`; if it is Z use `Vector3.UnitZ`. Getting this wrong produces a 90° roll that looks correct at zero angles but breaks at non-zero elevation.

```csharp
float azimuth   = (float)(RotateHorz * Math.PI / 180.0);
float elevation = (float)(RotateVert * Math.PI / 180.0);

float camDist = GetCameraDistance();  // derived from Zoom (see Phase 3)
Vector3 eye = new Vector3(
    camDist * MathF.Cos(elevation) * MathF.Sin(azimuth),
    camDist * MathF.Sin(elevation),
    camDist * MathF.Cos(elevation) * MathF.Cos(azimuth)
);

Vector3 target = Vector3.Zero;  // Sun at origin; changes for centering (Phase 5)
Vector3 up     = Vector3.UnitY; // confirm against existing orientation first
Matrix4 view   = Matrix4.LookAt(eye, target, up);
```

The `MtxRotate` CPU matrix (used for crosshair and centering calculations) can be derived from the same angles and kept in sync, or replaced entirely once Phase 6 is done.

---

## Phase 3 — Zoom as camera distance

Currently `Zoom` scales `halfX/halfY` (the screen-space divisor). With perspective, zoom means moving the camera closer or farther, or narrowing the FOV.

**Option A — camera distance (recommended):** feels natural, objects get larger as you zoom in because the camera physically approaches.

```csharp
// Map Zoom (existing range) to camera distance in AU
float camDist = BaseDistance / Zoom;  // tune BaseDistance empirically
```

**Starting estimate for `BaseDistance`:** at `Zoom = 1` the current formula shows a half-width of `Width * 750 / 682 ≈ Width * 1.1` pixels, which frames roughly the inner solar system (~5 AU radius). A perspective camera with FOV `fovY` that shows 5 AU radius at `Zoom = 1` needs:

```
BaseDistance ≈ 5.0 / tan(fovY / 2)
```

For `fovY = 45°` this gives `BaseDistance ≈ 12 AU`. Adjust by feel from there.

**Option B — FOV:** keep camera at fixed distance and change the field-of-view angle. Simpler but has subtle distortion at wide angles.

The scroll wheel handler and `Zoom` property don't change; only the translation from `Zoom` to camera distance changes.

---

## Phase 4 — Enable depth testing

```csharp
// In InitGL:
GL.Enable(EnableCap.DepthTest);
GL.DepthFunc(DepthFunction.Less);
```

This makes the GPU automatically discard fragments that are behind already-drawn geometry, so closer orbits correctly occlude farther ones. `ClearBufferMask.DepthBufferBit` is already present in the `GL.Clear` call in `OnPaint`, so nothing extra is needed there.

**Depth-cue color split decision:** the `vZ >= 0 ? uColorUpper : uColorLower` split currently provides a visual front/back cue. With a real depth buffer you can keep it as a style choice (it's cheap), or drop it in favour of a single orbit color. Suggested: keep it — it still reads well and provides a "below/above ecliptic" cue (see Phase 1 note on `vZ` semantics).

**Blending + depth:** transparent objects (alpha < 1) must be drawn back-to-front after all opaque geometry, otherwise the depth buffer discards them incorrectly. The current orbit lines are fully opaque so this is not an issue unless glow effects are added later.

---

## Phase 5 — Centering via camera target

Currently centering moves `X0/Y0` which feeds an NDC offset into the shader. With LookAt, centering means changing the `target` point that the camera looks at.

```csharp
Vector3 target = Vector3.Zero;  // default: look at Sun

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
```

The NDC offset uniforms (`uOffsetX`, `uOffsetY`) and the `X0`/`Y0` centering CPU logic can be removed entirely.

---

## Phase 6 — Crosshair simplification

The current crosshair builds arms in view space then inverse-rotates them back to world space to survive the shader's rotation. With a proper MVP, the cleaner approach is to build the crosshair arms directly in world space at a fixed angular size from the camera:

```csharp
// Compute a screen-space size in world units at the comet's depth
float depth  = Vector3.Distance(eye, cometWorldPos);
float pxSize = depth * MathF.Tan(fovY / 2f) / (Height / 2f);  // 1 pixel in world units at this depth
float off    = (diameter + 4) * pxSize;
float len    = (diameter + 8) * pxSize;

// Arms aligned to camera right and up vectors (extracted from view matrix).
// With transpose: true upload, Row0/Row1 of the C# Matrix4 are the right and up vectors.
Vector3 right = new Vector3(view.Row0.X, view.Row0.Y, view.Row0.Z);
Vector3 upVec = new Vector3(view.Row1.X, view.Row1.Y, view.Row1.Z);
// Build 8 arm endpoints using right/upVec ± off/len
```

This replaces `InvRotateMtx` and stays correct regardless of camera angle.

---

## Phase 7 — Point size by distance (optional)

With perspective, large point sizes for distant objects look wrong. Drive point size from the vertex shader using `gl_PointSize`:

```glsl
uniform float uPointSize;   // base size in pixels
uniform float uNear;        // near plane distance

void main()
{
    gl_Position = uMVP * vec4(aPos, 1.0);
    gl_PointSize = uPointSize * uNear / gl_Position.w;  // shrinks with depth
}
```

Enable with `GL.Enable(EnableCap.ProgramPointSize)` in `InitGL`. The existing `GL.PointSize(...)` calls from C# become redundant once `ProgramPointSize` is active and can be removed. The C# side just sets `uPointSize` to the current fixed values (7, 6, diameter*2+1).

---

## Phase 8 — Text label positions

The text overlay currently converts world positions to screen pixels using the CPU `GetDrawPoint` function (which replicates the weak perspective formula). Replace it with the standard MVP project:

```csharp
Vector4 clip   = mvp * new Vector4(worldPos, 1.0f);
Vector3 ndc    = clip.Xyz / clip.W;
float   screenX = (ndc.X + 1f) / 2f * Width;
float   screenY = (1f - ndc.Y) / 2f * Height;
```

`mvp` here is the same matrix uploaded to the shader (built as `projection * view * model` with `transpose: true`). On the CPU side, OpenTK's row-major multiplication means `new Vector4(worldPos, 1.0f) * mvp` gives the same result as the column-major GLSL `uMVP * vec4(aPos, 1.0)` — use whichever matches the rest of the CPU-side code.

The `GetDrawPoint` method and `X0`/`Y0` pixel offset logic can then be removed. This phase **must be done in the same pass as Phases 1–4**, otherwise labels will appear at wrong positions as soon as the shader switches to the MVP transform.

---

## Summary of changes

| Area | Current | After |
|---|---|---|
| Vertex shader | 5 uniforms + custom w formula | `uMVP` (1 matrix) |
| Camera | scene rotation around origin | LookAt orbiting camera |
| Zoom | halfX/halfY scale | camera distance |
| Centering | NDC pixel offset | camera target point |
| Depth | disabled | enabled (`DepthTest`) |
| Crosshair | inverse rotation hack | camera right/up vectors |
| Point size | fixed px | optionally driven by depth |
| Label positions | custom `GetDrawPoint` | standard MVP project |
| Orbital mechanics | unchanged | unchanged |
| Text overlay | unchanged | unchanged |

Phases 1–4 + 8 are the core and must be done together in one pass. Phases 5–6 clean up the remaining hacks. Phase 7 is polish.
