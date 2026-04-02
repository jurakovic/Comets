# 3D Engine Migration — Analysis & Options

## Current Rendering Architecture

`OrbitPanel` (in `src/Comets.OrbitViewer/OrbitViewer/OrbitPanel.cs`) is a WinForms
`Panel` subclass that renders entirely on the CPU using GDI+ (`System.Drawing`).

The rendering pipeline is:
1. Pre-computed `Xyz[]` orbit point arrays from `CometOrbit` / `PlanetOrbit`
2. Apply rotation matrices (`MtxToEcl`, `MtxRotate`) via `Xyz.Rotate(Matrix)`
3. Project to screen with a manual perspective divide (`GetDrawPoint`, line ~564):
   ```csharp
   double mul = (Zoom * MinimumSize.Width) / (1500.0 * (1.0 + xyz.Z / 625.0));
   int X = X0 + (int)Math.Round(xyz.X * mul);
   int Y = Y0 - (int)Math.Round(xyz.Y * mul);
   ```
4. Draw with `Graphics.DrawLine` / `FillPie` — no GPU, no depth buffer, no lighting.

Upper/lower orbit halves are coloured differently by checking `xyz.Z >= 0` as a
poor-man's depth cue (e.g. `ColorCometOrbitUpper` vs `ColorCometOrbitLower`).

## What Would NOT Change

The orbital mechanics layer is completely decoupled from rendering and would carry
over untouched to any 3D engine:

- `OVComet.cs` — `CometStatusEllip`, `CometStatusPara`, `CometStatusHyper`, `GetPos`
- `CometOrbit.cs` — 500-point orbit path arrays (`GetOrbitEllip/Hyper/Para`)
- `PlanetOrbit.cs` / `PlanetElm.cs` / `PlanetExp.cs` — planet positions
- `Matrix.cs`, `Xyz.cs` — 3D vector/matrix math
- All of `Comets.Core` — ephemeris, import/export, filtering

Only `OrbitPanel.cs` and its direct rendering calls need to be replaced.

## Options

### Option 1 — OpenTK (recommended)

**Effort: Medium** (~1000 lines to rewrite, stays WinForms)

OpenTK provides OpenGL bindings for .NET and ships a `GLControl` that embeds an
OpenGL surface directly inside a WinForms form.

**Steps:**
1. Add NuGet packages: `OpenTK.WinForms`, `OpenTK.Mathematics`
2. Change `OrbitPanel : Panel` → `OrbitPanel : GLControl`
3. Replace `OnPaint`/`Update` with an OpenGL render loop:
   - Orbit paths → `GL_LINE_STRIP` (one draw call per orbit)
   - Bodies (Sun, planets, comets) → point sprites or billboarded quads
   - Camera → standard perspective matrix (`Matrix4.CreatePerspectiveFieldOfView`)
   - Rotation → feed `RotateHorz`/`RotateVert` into a view matrix instead of the
     manual `MtxRotate` multiply
4. Depth-based orbit colouring → real depth buffer or `gl_FragDepth`-based shading;
   the `xyz.Z >= 0` hack can be dropped entirely.
5. Text/label rendering → GDI+ `DrawString` has no OpenGL equivalent; render text
   to a `Bitmap` using `System.Drawing` (or **SixLabors.Fonts** for a pure managed
   approach), upload as a `GL_TEXTURE_2D`, and draw as a billboarded quad per label.
   Affects comet names, planet names, date/time overlay, and ephemeris info.
6. Antialiasing → replace `SmoothingMode.AntiAlias` with MSAA on the `GLControl`
   constructor: `new GLControl(new GraphicsMode(ColorFormat, 24, 0, 8))`.

**Packages:**
```
dotnet add package OpenTK.WinForms
dotnet add package OpenTK.Mathematics
```

---

### Option 2 — WPF + Helix Toolkit

**Effort: Medium-High**

Helix Toolkit is a WPF 3D library built on WPF's `Viewport3D`. Gives lighting,
materials, and mouse interaction for free. Better end-result visually than OpenTK
with less low-level work, but requires migrating the entire UI from WinForms to WPF
first, which is the bulk of the effort.

**Packages:**
```
dotnet add package HelixToolkit.Wpf
```

---

### Option 3 — WPF + SharpDX / Vortice

**Effort: High**

Full DirectX 11/12 via managed bindings. Most control over rendering (custom
shaders, bloom, glow effects on the Sun, comet tails). Requires both the WinForms→WPF
migration and writing a proper D3D render pipeline from scratch.

---

### Option 4 — MonoGame

**Effort: High**

A game framework that takes over the window and game loop entirely. Overkill for
this use case and would require significant restructuring of the application shell.

---

## Recommended Path

**OpenTK** is the most pragmatic choice:
- Stays in WinForms — no UI migration needed
- The `GLControl` is a drop-in replacement for `Panel`
- Orbit data is already in 3D `Xyz[]` arrays — just upload to a VBO and draw
- Immediate visual improvements: real depth buffer, smooth line anti-aliasing
  (`GL_LINE_SMOOTH`), point sprites for bodies, easy bloom via framebuffer effects
- Well-documented, actively maintained, .NET 6/7/8 compatible

## Rough Implementation Sketch (OpenTK)

Note: `GLControl` is a WinForms control — its render hook is `OnPaint`, not
`OnRenderFrame` (which belongs to `GameWindow`).

```csharp
// OrbitPanel.cs (new skeleton)
public class OrbitPanel : GLControl
{
    private int _vao, _vbo;
    private ShaderProgram _shader;

    public OrbitPanel()
        : base(new GraphicsMode(ColorFormat, 24, 0, 8))  // 8 = MSAA samples
    { }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        GL.ClearColor(Color4.Black);
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.Blend);
        // compile shaders, create VAO/VBO ...
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Matrix4 view = Matrix4.LookAt(eye, target, up);
        Matrix4 proj = Matrix4.CreatePerspectiveFieldOfView(
            MathHelper.DegreesToRadians(45f),
            (float)Width / Height, 0.01f, 10000f);

        _shader.SetMatrix4("mvp", view * proj);

        foreach (var orbit in CometOrbits)
            DrawLineStrip(orbit);  // upload Xyz[] → VBO, GL.DrawArrays

        SwapBuffers();
    }
}
```

The existing `Xyz[]` arrays from `CometOrbit.GetAt(i)` map directly to
`Vector3` vertices — no coordinate transformation changes needed.
