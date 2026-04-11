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

## Phase 1 — MVP matrix in the vertex shader

Replace the five separate uniforms (`uRot`, `uHalfX`, `uHalfY`, `uOffsetX`, `uOffsetY`) and the custom `w` formula with a single `uMVP` matrix. The vertex shader becomes trivial:

```glsl
uniform mat4 uMVP;
out float vZ;  // eye-space Z, still used for color split

void main()
{
    vec4 viewPos = uView * vec4(aPos, 1.0);  // or fold into uMVP
    vZ = viewPos.z;
    gl_Position = uMVP * vec4(aPos, 1.0);
}
```

Pass two separate uniforms `uView` and `uMVP` (= projection * view * model) so the fragment shader can still access eye-space Z for the color split without extra cost.

On the C# side, build MVP each frame:

```csharp
Matrix4 model      = Matrix4.Identity;  // world = ecliptic space
Matrix4 view       = Matrix4.LookAt(eye, target, up);
Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(fovY, aspect, near, far);
Matrix4 mvp        = model * view * projection;
GL.UniformMatrix4(_uMVP, false, ref mvp);
```

**Clip planes:** `near = 0.001f` AU (well inside Mercury), `far = 2000f` AU (covers Oort cloud comets if needed, or 500 AU for practical use).

---

## Phase 2 — Camera from rotation angles

Currently `RotateHorz`/`RotateVert` are rotation angles applied to the scene. In true 3D they become the camera's azimuth and elevation orbiting around the origin (the Sun).

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
Vector3 up     = Vector3.UnitY;
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

**Option B — FOV:** keep camera at fixed distance and change the field-of-view angle. Simpler but has subtle distortion at wide angles.

The scroll wheel handler and `Zoom` property don't change; only the translation from `Zoom` to camera distance changes.

---

## Phase 4 — Enable depth testing

```csharp
// In InitGL:
GL.Enable(EnableCap.DepthTest);
GL.DepthFunc(DepthFunction.Less);
```

This makes the GPU automatically discard fragments that are behind already-drawn geometry, so closer orbits correctly occlude farther ones.

**Depth-cue color split decision:** the `vZ >= 0 ? uColorUpper : uColorLower` split currently provides a visual front/back cue. With a real depth buffer you can keep it as a style choice (it's cheap), or drop it in favour of a single orbit color. Suggested: keep it — it still reads well and provides a "below/above ecliptic" cue.

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

// Arms aligned to camera right and up vectors (extracted from view matrix)
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

Enable with `GL.Enable(EnableCap.ProgramPointSize)`. The C# side just sets `uPointSize` to the current fixed values (6, 5, diameter*2+1).

---

## Phase 8 — Text label positions

The text overlay currently converts world positions to screen pixels using the CPU `GetDrawPoint` function (which replicates the weak perspective formula). Replace it with the standard MVP project:

```csharp
Vector4 clip   = mvp * new Vector4(worldPos, 1.0f);
Vector3 ndc    = clip.Xyz / clip.W;
float   screenX = (ndc.X + 1f) / 2f * Width;
float   screenY = (1f - ndc.Y) / 2f * Height;
```

The `GetDrawPoint` method and `X0/Y0` pixel offset logic can then be removed.

---

## Summary of changes

| Area | Current | After |
|---|---|---|
| Vertex shader | 5 uniforms + custom w formula | `uMVP` + `uView` (2 matrices) |
| Camera | scene rotation around origin | LookAt orbiting camera |
| Zoom | halfX/halfY scale | camera distance |
| Centering | NDC pixel offset | camera target point |
| Depth | disabled | enabled (`DepthTest`) |
| Crosshair | inverse rotation hack | camera right/up vectors |
| Point size | fixed px | optionally driven by depth |
| Label positions | custom `GetDrawPoint` | standard MVP project |
| Orbital mechanics | unchanged | unchanged |
| Text overlay | unchanged | unchanged |

Phases 1–4 are the core and can be done together in one pass. Phases 5–6 clean up the remaining hacks. Phases 7–8 are polish.
