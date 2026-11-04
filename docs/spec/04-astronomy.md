# 04 — Astronomy engine

The web app has **one** astronomy engine. It computes every number the app shows: ephemerides, graphs, sort and filter values, the database's live values, and positions in the 3D orbit viewer.

- **Comets and the Sun**: the desktop ephemeris engine (`Comets.Core.Managers.EphemerisManager`), transcribed exactly. Results MUST match [`ephemeris.json`](vectors/ephemeris.json) within the [tolerances](11-test-vectors.md#tolerances).
- **Planets**: Paul Schlyter's mean orbital elements ([Computing planetary positions](https://stjarnhimlen.se/comp/ppcomp.html)), solved with the comet engine's own Kepler solver. Results MUST match [`planets.json`](vectors/planets.json).

Positions are low-precision by design: no nutation, aberration, light time or ΔT, and UTC is used as the dynamical time. The accuracy is about one arcminute, which is plenty for planning comet observations.

## Constants and helpers

| Name | Value |
|---|---|
| `D0` | `2451543.5`, the JD of 1999 Dec 31.0 = 2000 Jan 0.0; day count `d = JD − D0` |
| `J2000` | `2451545.0` |
| `k` (Gauss) | `0.01720209895` rad/day |
| `EPS` | `1e-10`, the convergence threshold of every solver |
| Node precession | `3.82394E-5` °/day |
| Obliquity | `ε(d) = 23.439291111 − 3.563E-7·d` ° |
| Date range | 1700-01-01 00:00 UTC ≤ t ≤ 2300-01-01 00:00 UTC for every date the user picks |

```
rev(x)   = x − floor(x/360)·360             # [0, 360)
rev2(x)  = rev(x) ≥ 180 ? rev(x) − 360 : rev(x)  # [−180, 180)
sind, cosd, tand(x) = sin, cos, tan(x·π/180)
asind, acosd, atan2d = 180/π · asin, acos, atan2
log10(x) = 0.43429448190325182765 · ln(x)
sign(x)  = −1, 0 or +1                       # sign(0) = 0
roundEven(x) = round to nearest integer, ties to even
```

## Time

### Calendar to JD (`JD0`)

`JD0(year, month, day, h4)`, where `h4` is the fraction of the day in units of 10⁻⁴ day (the `Th` field). The formula is **always Gregorian**, even for years before 1582:

```
y = year; m = month; D = day + h4/10000
if m < 3: m += 12; y −= 1
A = floor(y/100); B = 2 − A + floor(A/4)
JD0 = floor(365.25·(y + 4716)) + floor(30.6001·(m + 1)) + D + B − 1524.5
```

`floor` rounds toward −∞, including for negative years.

### Instant to JD

`JD(t)` for a UTC instant. Milliseconds are **dropped**:

```
JD(t) = JD0(t.year, t.month, t.day, 0) + (t.hour + t.minute/60 + t.second/3600) / 24
```

### JD to instant (`JDToDateTime`)

```
jd' = jd + 0.5;  A = floor(jd')
if A ≥ 2299160.5:                        # Gregorian; Julian calendar before 1582-10-15
    α = floor((A − 1867216.25) / 36524.25);  A += α − floor(α/4) + 1
B = floor(A) + 1524
C = floor((B − 122.1) / 365.25)
K = floor(365.25·C)
E = floor((B − K) / 30.6001)
day0  = B − K − floor(30.6001·E) + (jd' − floor(jd'))
month = floor(E − (E ≥ 13.5 ? 13 : 1) + 0.5)
year  = floor(C − (month > 2 ? 4716 : 4715) + 0.5)
day   = floor(day0)
hour0 = (day0 − day)·24 + 0.0000001      # nudge so that 23:59:59.9999 does not drop a second
hour  = floor(hour0)
min0  = (hour0 − hour)·60;  minute = floor(min0)
second = trunc((min0 − minute)·60)
```

The result is a UTC instant with whole seconds. The tiny nudge also absorbs float64 error, which is why float64 JDs give the same results as the desktop's decimals. Years below 1 cannot be represented on the desktop; the web app returns them as astronomical years (for example `-146`). The date parts are then read as a Gregorian date, as on the desktop ([10](10-quirks.md#quirks-kept) Q8); a Julian 29 February in a year the Gregorian calendar does not leap (1500, for example) becomes 1 March, where the desktop throws. Vectors: [`time.json`](vectors/time.json).

### Local time

"Local time" means the browser's time zone (`Intl.DateTimeFormat().resolvedOptions().timeZone`), with its daylight-saving rules for the instant being converted. Calculations always use UTC. Only display uses local time.

### Date text

Dates print with .NET custom date formats in the invariant culture. The app uses `dd.MM.yyyy. HH:mm:ss`, `dd MMM yyyy HH:mm:ss`, `dd.MM.yyyy HH:mm`, `dd MMM yyyy HH:mm`, `dd MMM yyyy` and `yyyy-MM-dd_HHmmss` (file names). `dd`, `MM`, `HH`, `mm` and `ss` are two digits, `MMM` is `Jan` … `Dec`, and `yyyy` is at least four digits (`0146`). Astronomical years ≤ 0, which the desktop cannot show (D5), print with a minus before the four digits (`-0146`, `0000`).

## Comet position

`CometXyz(T, q, e, w, N, i, jd)` returns heliocentric rectangular coordinates referred to the **ecliptic and equinox of date** (AU).

### Orbital plane

The branch is chosen by an exact comparison of `e` with 1.

**Ellipse (e < 1)**, Laguerre–Conway iteration from Stellarium:

```
a = q/(1−e)
M = k·(jd − T) / (a·√a)
M = M mod 2π  (remainder with the sign of the dividend);  if M < 0: M += 2π
E = M + 0.85·e·sign(sin M)
n = 0
loop:
    Ep = E; f2 = e·sin E; f = E − f2 − M; f1 = 1 − e·cos E
    E += −5f / (f1 + sign(f1)·√|16·f1² − 20·f·f2|)
    if |E − Ep| < EPS: break
    n += 1; if n > 10: break
x = a·(cos E − e);  y = q·√((1+e)/(1−e))·sin E
v = atan2(y, x);  r = √(x² + y²)
```

**Hyperbola (e > 1)**:

```
a = q/(e−1);  nMotion = k / (q/(e−1))^1.5
M = (jd − T)·nMotion
E = sign(M)·ln(2|M|/e + 1.85)
loop:
    Ep = E; f2 = e·sinh E; f = f2 − E − M; f1 = e·cosh E − 1
    E += −5f / (f1 + sign(f1)·√|16·f1² − 20·f·f2|)
    if |E − Ep| < EPS: break
v = 2·atan(√((e+1)/(e−1))·tanh(E/2))
r = a·(e² − 1) / (1 + e·cos v)
```

**Parabola (e = 1)**, from Cartes du Ciel:

```
W = 3.649116245E-2·(jd − T) / (q·√q)
s = 0
loop: s0 = s; s = (2·s0³ + W) / (3·(s0² + 1)); if |s − s0| < EPS: break
v = 2·atan(s);  r = q·(1 + s²)
```

The hyperbolic and parabolic loops have no iteration limit on the desktop. A web implementation SHOULD stop after 100 iterations and use the last value; no element set in the vectors needs that many.

### To ecliptic of date

```
v° = v·180/π
d  = jd − D0
N' = N + 3.82394E-5·d                         # J2000 node precessed to the equinox of date
x = r·(cosd N'·cosd(v°+w) − sind N'·sind(v°+w)·cosd i)
y = r·(sind N'·cosd(v°+w) + cosd N'·sind(v°+w)·cosd i)
z = r·sind(v°+w)·sind i
```

Equivalently, `P = Rz(N')·Rx(i)·Rz(w)·(r cos v, r sin v, 0)`. Only the node is precessed, and the desktop does the same. Vectors: [`orbit-viewer-positions.json`](vectors/orbit-viewer-positions.json).

## Sun position

`SunXyz(jd)` returns the geocentric Sun in ecliptic-of-date coordinates. It is Schlyter's Sun, with a one-step eccentric anomaly:

```
d = jd − D0
w = 282.9404 + 4.70935E-5·d
e = 0.016709 − 1.151E-9·d
M = rev(356.0470 + 0.9856002585·d)
E = M + e·(180/π)·sind(M)·(1 + e·cosd(M))
xv = cosd(E) − e;  yv = √(1 − e²)·sind(E)
v = atan2d(yv, xv);  r = √(xv² + yv²)
λ = rev(v + w)
Sun = (r·cosd λ, r·sind λ, 0)
```

**Earth** (heliocentric) = −Sun. The 3D view and the ephemeris use this same Earth.

## Ephemeris

`GetEphemeris(T, q, e, w, N, i, g, k, jd, latitude, longitude)` returns one ephemeris record. Latitude is positive north; longitude is positive **east**.

```
S = SunXyz(jd);  C = CometXyz(T, q, e, w, N, i, jd);  d = jd − D0
gx = C.x + S.x;  gy = C.y + S.y;  gz = C.z                       # geocentric ecliptic
EcLon = rev(atan2d(gy, gx));  EcLat = atan2d(gz, √(gx² + gy²))

ε = 23.439291111 − 3.563E-7·d                                    # geocentric equatorial
x1 = gx;  y1 = gy·cosd ε − gz·sind ε;  z1 = gy·sind ε + gz·cosd ε
RA = rev(atan2d(y1, x1));  Dec = atan2d(z1, √(x1² + y1²))
EarthDist Δ = √(x1² + y1² + z1²)
SunDist   r = √(C.x² + C.y² + C.z²)

θ0 = 280.46061837 + 360.98564736629·(jd − 2451545.0)          # Greenwich sidereal time
H  = rev(θ0 + longitude − RA)
Alt = asind(sind(lat)·sind(Dec) + cosd(lat)·cosd(Dec)·cosd(H))
Az  = rev(atan2d(sind H, cosd H·sind(lat) − tand(Dec)·cosd(lat)) + 180)   # 0 = north, 90 = east

Magnitude = g + 5·log10(Δ) + 2.5·k·log10(r)

# Sun's RA/Dec, for the elongation
xe = S.x;  ye = S.y·cosd ε;  ze = S.y·sind ε
RAs = rev(atan2d(ye, xe));  Decs = atan2d(ze, √(xe² + ye²))

Elongation = acosd(sind Dec·sind Decs + cosd Dec·cosd Decs·cosd(RA − RAs))
if Elongation < 0.1:
    Elongation = √((rev2(RA − RAs)·cosd((Dec + Decs)/2))² + (Dec − Decs)²)
PositionAngle = rev(atan2d(sind(RA − RAs), cosd Decs·tand Dec − sind Decs·cosd(RA − RAs)))
```

The record is `{JD, RA, Dec, Alt, Az, EcLon, EcLat, Elongation, PositionAngle, SunDist, EarthDist, Magnitude}`. The elongation is labelled **W** when `PositionAngle ≥ 180`, otherwise **E**.

## Angle and time text

Ephemeris text and the database view print angles with these functions (`EphemerisManager.HMSString`, `AngleString`). Both use `roundEven`. Vectors: [`formatting.json`](vectors/formatting.json).

```
HMS(t):                         # t in hours (RA/15); the sign is dropped
    h = |t|; m = 60·(h − floor h); h = floor h
    s = roundEven(60·(m − floor m)); m = floor m
    if s ≥ 60: m += 1; s −= 60
    if m ≥ 60: h += 1; m −= 60
    if h ≥ 24: h −= 24
    return pad2(h) + "h" + pad2(m) + "m" + pad2(s)           # "05h07m24"

Angle(a, circle, arcmin):
    ar = roundEven(a·60)/60; deg = |ar|; mn = roundEven(60·(deg − floor deg))
    if mn ≥ 60: deg += 1; mn = 0
    text = circle ? (floor deg < 100 ? "0" : "") : (ar < 0 ? "-" : "+")
    text += pad2(floor deg)
    text += arcmin ? "°" + pad2(mn) + "'" : ":" + pad2(mn)      # "+45°30'", "045°30'", "+45:30"
```

`pad2` left-pads an integer to 2 digits with `0`. Values with 3 digits are left unchanged.

## Element precession (NASA import)

`PrecessElementsToJ2000(epochMjd, i, N, w)` uses the Lieske (1977) ecliptic precession. The NASA importer calls it ([03](03-element-formats.md#nasa-elementscomet-import-only)).

```
T = (epochMjd + 2400000.5 − 2451545.0) / 36525;  t = −T;  if |t| < 1e-8: unchanged
η  = ((47.0029 − (0.06603 − 0.000598·T)·T)·t − (0.03302 − 0.000598·T)·t² + 0.000060·t³) / 3600
Π  = 174.876384 + ((3289.4789 + 0.60622·T)·t − (869.8089 + 0.50491·T)·t² + 0.03536·t³) / 3600
p  = ((5029.0966 + (2.2226 − 0.000042·T)·T)·t + (1.1152 − 0.000042·T)·t² − 0.000052·t³) / 3600
Φ  = Π + p                                    (all in degrees; use radians below)
pole n   = (sin i sin N, −sin i cos N, cos i)
perih p⃗ = (cos N cos w − sin N sin w cos i,  sin N cos w + cos N sin w cos i,  sin i sin w)
R = [[ cΦcΠ + sΦcηsΠ, −cΦsΠ + sΦcηcΠ, −sΦsη ],
     [ −sΦcΠ + cΦcηsΠ, sΦsΠ + cΦcηcΠ, −cΦsη ],
     [ sηsΠ,           sηcΠ,           cη    ]]
n' = R·n;  p' = R·p⃗
i' = acos(clamp(n'z, −1, 1));  N' = atan2(n'x, −n'y);  w' = atan2(p'z / sin i', p'x cos N' + p'y sin N')
i = i'°;  N = ((N'° mod 360) + 360) mod 360;  w = ((w'° mod 360) + 360) mod 360
```

Vectors: [`precession.json`](vectors/precession.json).

## Planets

Planets appear only in the orbit viewer. The positions are heliocentric, ecliptic of date, in AU. `d = JD − D0`. The elements are Schlyter's (N, i, w, M in degrees; a in AU):

| Planet | N | i | w | a | e | M |
|---|---|---|---|---|---|---|
| Mercury | 48.3313 + 3.24587E-5 d | 7.0047 + 5.00E-8 d | 29.1241 + 1.01444E-5 d | 0.387098 | 0.205635 + 5.59E-10 d | 168.6562 + 4.0923344368 d |
| Venus | 76.6799 + 2.46590E-5 d | 3.3946 + 2.75E-8 d | 54.8910 + 1.38374E-5 d | 0.723330 | 0.006773 − 1.302E-9 d | 48.0052 + 1.6021302244 d |
| Earth¹ | 0 | 0 | 282.9404 + 4.70935E-5 d + 180 | 1.000000 | 0.016709 − 1.151E-9 d | 356.0470 + 0.9856002585 d |
| Mars | 49.5574 + 2.11081E-5 d | 1.8497 − 1.78E-8 d | 286.5016 + 2.92961E-5 d | 1.523688 | 0.093405 + 2.516E-9 d | 18.6021 + 0.5240207766 d |
| Jupiter | 100.4542 + 2.76854E-5 d | 1.3030 − 1.557E-7 d | 273.8777 + 1.64505E-5 d | 5.20256 | 0.048498 + 4.469E-9 d | 19.8950 + 0.0830853001 d |
| Saturn | 113.6634 + 2.38980E-5 d | 2.4886 − 1.081E-7 d | 339.3939 + 2.97661E-5 d | 9.55475 | 0.055546 − 9.499E-9 d | 316.9670 + 0.0334442282 d |
| Uranus | 74.0005 + 1.3978E-5 d | 0.7733 + 1.9E-8 d | 96.6612 + 3.0565E-5 d | 19.18171 − 1.55E-8 d | 0.047318 + 7.45E-9 d | 142.5905 + 0.011725806 d |
| Neptune | 131.7806 + 3.0173E-5 d | 1.7700 − 2.55E-7 d | 272.8461 − 6.027E-6 d | 30.05826 + 3.313E-8 d | 0.008606 + 2.15E-9 d | 260.2471 + 0.005995147 d |

¹ The Earth's elements are the Sun's, with `w + 180`. They are used **only to draw Earth's orbit**. Earth's position is `−SunXyz(jd)`.

Position of every planet except Earth:

```
E  = the elliptic Kepler loop above, applied to M·π/180 with this e
xv = a·(cos E − e);  yv = a·√(1 − e²)·sin E
v° = atan2d(yv, xv);  r = √(xv² + yv²)
x, y, z = r·(cosd N cosd(v°+w) − sind N sind(v°+w) cosd i,
             sind N cosd(v°+w) + cosd N sind(v°+w) cosd i,
             sind(v°+w) sind i)
```

For Jupiter, Saturn and Uranus, add the perturbations. Convert to `λ = atan2d(y, x)` and `β = atan2d(z, √(x² + y²))`, add `Δλ` and `Δβ`, then rebuild `x, y, z = r·(cosd β cosd λ, cosd β sind λ, sind β)` with the same r. Mj, Ms and Mu are the mean anomalies M of Jupiter, Saturn and Uranus at d:

```
Jupiter Δλ = −0.332 sind(2Mj − 5Ms − 67.6) − 0.056 sind(2Mj − 2Ms + 21) + 0.042 sind(3Mj − 5Ms + 21)
             −0.036 sind(Mj − 2Ms) + 0.022 cosd(Mj − Ms) + 0.023 sind(2Mj − 3Ms + 52) − 0.016 sind(Mj − 5Ms − 69)
Saturn  Δλ = +0.812 sind(2Mj − 5Ms − 67.6) − 0.229 cosd(2Mj − 4Ms − 2) + 0.119 sind(Mj − 2Ms − 3)
             +0.046 sind(2Mj − 6Ms − 69) + 0.014 sind(Mj − 3Ms + 32)
Saturn  Δβ = −0.020 cosd(2Mj − 4Ms − 2) + 0.018 sind(2Mj − 6Ms − 49)
Uranus  Δλ = +0.040 sind(Ms − 2Mu + 6) + 0.035 sind(Ms − 3Mu + 33) − 0.015 sind(Mj − Mu + 20)
```

Reference implementation: [`tools/spec-vectors/Canonical.cs`](../../tools/spec-vectors/Canonical.cs).

## 3D scene geometry

The orbit viewer works in **heliocentric ecliptic-of-date** coordinates, in AU: +X towards the vernal equinox of date, +Z towards the north ecliptic pole.

- **Bodies**: the Sun at the origin; planets as above; Earth `−SunXyz`; comets `CometXyz`.
- **Comet orbit lines**: sample the orbit in its plane with the [sampler below](#comet-orbit-sampling). Rotate each sample with `Rz(N)·Rx(i)·Rz(w)` once, then apply `Rz(3.82394E-5·d)` for the current date. A date change only changes that last rotation, which can be a uniform in the shader, so the comet line buffers never need rebuilding when the date changes.
- **Planet orbit lines**: for the current date, take the planet's elements and sample 301 points with E stepped uniformly by `360°/300` from 0 to 360 (the first and last points coincide). Use `(a(cos E − e), a√(1−e²) sin E, 0)` rotated by `Rz(N)·Rx(i)·Rz(w)`. For Jupiter, Saturn and Uranus, also rotate the whole line by `Rz(Δλ)` so the planet sits on its drawn orbit; the remaining `Δβ` is at most 0.04°. Rebuild on every date change; it is cheap.

### Comet orbit sampling

This is the desktop's adaptive sampler (`CometOrbit`). It returns points `(x, y)` in the orbital plane, with x towards perihelion and y along the motion at perihelion. `DIV = 500`, `RMAX = 500` AU. The branch uses a tolerance of `1e-16` around e = 1.

```
Ellipse (e < 1 − 1e-16):
    a = q/(1−e); w = 1 − e²; sw = √w; kMax = min(50, max(20, (1/w)^(2/3)))
    pt(E) = (a(cos E − e), a·sw·sin E)
    κ(E)  = sw / (sin²E + w·cos²E)^1.5
    if a(1+e) > RMAX:                               # open arc around perihelion
        Emax = acos(clamp((1 − RMAX/a)/e, −1, 1)); dE = 2·Emax/DIV
        E = −Emax; while E < Emax: emit pt(E); E += dE / max(min(κ(E), kMax), 1)
        emit pt(Emax)
    else:                                           # closed: two legs out from perihelion
        dE = 2π/DIV
        inbound = []; E = 0
        while E > −π: step = dE / max(min(κ(E), kMax), 1); E −= step; if E < −π: E = −π; inbound.add(pt(E))
        emit inbound in reverse order
        E = 0; while E < π: emit pt(E); E += dE / max(min(κ(E), kMax), 1)
        emit (a(−1 − e), 0)                         # aphelion
Hyperbola (e > 1 + 1e-16):
    a = q/(e−1); b = √(e² − 1); kMax = min(50, max(20, (1/b)^(2/3)))
    c = (RMAX/a + 1)/e; Fmax = c > 1 ? ln(m + √(m² − 1)) with m = min(c, 1e6) : 0; dF = 2·Fmax/DIV
    pt(F) = (a(e − cosh F), a·b·sinh F);  κ(F) = b / (sinh²F + b²cosh²F)^1.5
    F = −Fmax; while F < Fmax: emit pt(F); F += dF / max(min(κ(F), kMax), 1)
    emit pt(Fmax)
Parabola (otherwise):
    Dmax = √(max(RMAX/q − 1, 0)); kMax = min(50, max(20, Dmax^(4/3))); dD = 2·Dmax/DIV
    pt(D) = (q(1 − D²), 2qD);  κ(D) = kMax / (1 + D²)^0.75
    D = −Dmax; while D < Dmax: emit pt(D); D += dD / max(min(κ(D), kMax), 1)
    emit pt(Dmax)
```

The orbit is drawn as a line strip through the points in order. Vectors: [`orbit-sampling.json`](vectors/orbit-sampling.json).

## Decision record: one canonical engine

**Context.** The desktop orbit viewer has its own engine, a C# port of Ajiki and Baalke's OrbitViewer:

- comets: J2000 elements → equatorial J2000 → full precession matrix → ecliptic of date;
- planets: a series expansion for 1950–2060 and osculating elements with tabulated perturbations outside that range, which jumps at 1950 and 2060;
- orbit lines: drawn from yet another planet model, so a planet does not lie exactly on its own line.

Even inside the orbit viewer, the magnitude, distance readouts and date filters come from the ephemeris engine.

**Decision.** Use the ephemeris engine for comets everywhere. Use Schlyter's mean elements for planets, the model family the ephemeris engine already uses for the Sun, solved with the same Kepler code. There is one Earth (`−SunXyz`) for both the ephemeris and the 3D view.

**Consequences**, measured by [`engine-comparison.json`](vectors/engine-comparison.json):

| Body | Largest difference from the desktop orbit viewer |
|---|---|
| Comets, 1990–2060 | ≤ 0.0062° as seen from the Sun (≤ 0.045 AU even for interstellar objects far out) |
| Mercury, Venus, Earth, Mars, 1700–2300 | ≤ 0.062° heliocentric longitude, ≤ 0.0008 AU |
| Jupiter | ≤ 0.086°, ≤ 0.009 AU |
| Saturn | ≤ 0.30°, ≤ 0.05 AU |
| Uranus, Neptune | ≤ 1.08°, ≤ 0.38 AU, at the ends of the range (1700s), where both models are weakest |

Comet changes are invisible at any zoom. Outer-planet changes are visible only at extreme dates, and amount to a few pixels at the default zoom. Ephemeris, graph, sort and filter results do not change at all.

## Numeric precision

- Use float64 throughout. The desktop keeps T and JD in 128-bit decimal. Holding them in float64 gives errors around 5·10⁻¹⁰ day, which reaches Alt/Az as about 2·10⁻⁷° through sidereal time and is far below display precision.
- `Math.sin`, `Math.atan2` and so on may differ from .NET in the last ulp. The [tolerances](11-test-vectors.md#tolerances) allow for this.
- Displayed text is produced from the computed numbers with the [formatting rules](03-element-formats.md#number-formatting). The last digit of displayed text can differ from the desktop only when the value falls within a few ulps of a rounding boundary. Text vectors are therefore checked line by line, with any differing line re-checked numerically ([11](11-test-vectors.md#ephemeris-text)).
