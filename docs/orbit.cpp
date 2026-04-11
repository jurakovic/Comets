// orbit.cpp
//
// Copyright (C) 2001, Chris Laurel <claurel@shatters.net>
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.

#include <functional>
#include <algorithm>
#include <cmath>
#include <cassert>
#include <celmath/mathlib.h>
#include <celmath/solve.h>
#include "astro.h"
#include "orbit.h"
#include "body.h"

using namespace std;


// Orbital velocity is computed by differentiation for orbits that don't
// override velocityAtTime().
static const double ORBITAL_VELOCITY_DIFF_DELTA = 1.0 / 1440.0;

// Maximum distance at which any orbit trail is drawn (500 AU in km).
// Applies to all orbit types: elliptic orbits whose apocenter exceeds this
// value are treated as open arcs, same as parabolic/hyperbolic orbits.
static const double MaxOrbitRadius = 500.0 * 1.4959787e8;


EllipticalOrbit::EllipticalOrbit(double _pericenterDistance,
                                 double _eccentricity,
                                 double _inclination,
                                 double _ascendingNode,
                                 double _argOfPeriapsis,
                                 double _meanAnomalyAtEpoch,
                                 double _period,
                                 double _epoch) :
    pericenterDistance(_pericenterDistance),
    eccentricity(_eccentricity),
    inclination(_inclination),
    ascendingNode(_ascendingNode),
    argOfPeriapsis(_argOfPeriapsis),
    meanAnomalyAtEpoch(_meanAnomalyAtEpoch),
    period(_period),
    epoch(_epoch)
{
    orbitPlaneRotation = (Mat3d::zrotation(_ascendingNode) *
                          Mat3d::xrotation(_inclination) *
                          Mat3d::zrotation(_argOfPeriapsis));
}


// Standard iteration for solving Kepler's Equation
struct SolveKeplerFunc1 : public unary_function<double, double>
{
    double ecc;
    double M;

    SolveKeplerFunc1(double _ecc, double _M) : ecc(_ecc), M(_M) {};

    double operator()(double x) const
    {
        return M + ecc * sin(x);
    }
};


// Faster converging iteration for Kepler's Equation; more efficient
// than above for orbits with eccentricities greater than 0.3.  This
// is from Jean Meeus's _Astronomical Algorithms_ (2nd ed), p. 199
struct SolveKeplerFunc2 : public unary_function<double, double>
{
    double ecc;
    double M;

    SolveKeplerFunc2(double _ecc, double _M) : ecc(_ecc), M(_M) {};

    double operator()(double x) const
    {
        return x + (M + ecc * sin(x) - x) / (1 - ecc * cos(x));
    }
};


struct SolveKeplerLaguerreConway : public unary_function<double, double>
{
    double ecc;
    double M;

    SolveKeplerLaguerreConway(double _ecc, double _M) : ecc(_ecc), M(_M) {};

    double operator()(double x) const
    {
        double s = ecc * sin(x);
        double c = ecc * cos(x);
        double f = x - s - M;
        double f1 = 1 - c;
        double f2 = s;
        x += -5 * f / (f1 + sign(f1) * sqrt(abs(16 * f1 * f1 - 20 * f * f2)));

        return x;
    }
};

struct SolveKeplerLaguerreConwayHyp : public unary_function<double, double>
{
    double ecc;
    double M;

    SolveKeplerLaguerreConwayHyp(double _ecc, double _M) : ecc(_ecc), M(_M) {};

    double operator()(double x) const
    {
        double s = ecc * sinh(x);
        double c = ecc * cosh(x);
        double f = s - x - M;
        double f1 = c - 1;
        double f2 = s;
        x += -5 * f / (f1 + sign(f1) * sqrt(abs(16 * f1 * f1 - 20 * f * f2)));

        return x;
    }
};

typedef pair<double, double> Solution;


Vec3d Orbit::velocityAtTime(double tdb) const
{
	Point3d p0 = positionAtTime(tdb);
	Point3d p1 = positionAtTime(tdb + ORBITAL_VELOCITY_DIFF_DELTA);
	return (p1 - p0) * (1.0 / ORBITAL_VELOCITY_DIFF_DELTA);
}


double EllipticalOrbit::eccentricAnomaly(double M) const
{
    if (eccentricity == 0.0)
    {
        // Circular orbit
        return M;
    }
    else if (eccentricity < 0.2)
    {
        // Low eccentricity, so use the standard iteration technique
        Solution sol = solve_iteration_fixed(SolveKeplerFunc1(eccentricity, M), M, 5);
        return sol.first;
    }
    else if (eccentricity < 0.9)
    {
        // Higher eccentricity elliptical orbit; use a more complex but
        // much faster converging iteration.
        Solution sol = solve_iteration_fixed(SolveKeplerFunc2(eccentricity, M), M, 6);
        // Debugging
        // printf("ecc: %f, error: %f mas\n",
        //        eccentricity, radToDeg(sol.second) * 3600000);
        return sol.first;
    }
    else if (eccentricity < 1.0)
    {
        // Extremely stable Laguerre-Conway method for solving Kepler's
        // equation.  Only use this for high-eccentricity orbits, as it
        // requires more calcuation.
        double E = M + 0.85 * eccentricity * sign(sin(M));
        Solution sol = solve_iteration_fixed(SolveKeplerLaguerreConway(eccentricity, M), E, 8);
        return sol.first;
    }
    else if (eccentricity == 1.0)
    {
        // Parabolic orbit: solve Barker's equation D + D^3/3 = M for D,
        // where D = tan(true_anomaly/2).
        // Closed-form solution via Cardano's formula: D = Y - 1/Y,
        // Y = cbrt(W + sqrt(W^2 + 1)), W = 3*M/2.
        double W = 1.5 * M;
        double Y = cbrt(W + sqrt(W * W + 1.0));
        return Y - 1.0 / Y;
    }
    else
    {
        // Laguerre-Conway method for hyperbolic (ecc > 1) orbits.
        if (M == 0.0)
            return 0.0;
        double absM = fabs(M);
        double E = log(2.0 * absM / eccentricity + 1.85);
        Solution sol = solve_iteration_fixed(SolveKeplerLaguerreConwayHyp(eccentricity, absM), E, 30);
        return (M > 0.0) ? sol.first : -sol.first;
    }
}


// Compute the position at the specified eccentric
// anomaly E.
Point3d EllipticalOrbit::positionAtE(double E) const
{
    double x, y;

    if (eccentricity < 1.0)
    {
        double a = pericenterDistance / (1.0 - eccentricity);
        x = a * (cos(E) - eccentricity);
        y = a * sqrt(1 - square(eccentricity)) * sin(E);
    }
    else if (eccentricity > 1.0)
    {
        double a = pericenterDistance / (1.0 - eccentricity);
        x = -a * (eccentricity - cosh(E));
        y = -a * sqrt(square(eccentricity) - 1) * sinh(E);
    }
    else
    {
        // Parabolic orbit: E here is D = tan(true_anomaly/2).
        // Position: x = q*(1 - D^2), y = 2q*D  (pericenter at x=q, y=0)
        x = pericenterDistance * (1.0 - E * E);
        y = 2.0 * pericenterDistance * E;
    }

    Point3d p = orbitPlaneRotation * Point3d(x, y, 0);

    // Convert to Celestia's internal coordinate system
    return Point3d(p.x, p.z, -p.y);
}


// Compute the velocity at the specified eccentric
// anomaly E.
Vec3d EllipticalOrbit::velocityAtE(double E) const
{
    double x, y;

    if (eccentricity < 1.0)
    {
        double a = pericenterDistance / (1.0 - eccentricity);
        double sinE = sin(E);
        double cosE = cos(E);
        
        x = -a * sinE;
        y =  a * sqrt(1 - square(eccentricity)) * cosE;
        
        double meanMotion = 2.0 * PI / period;
        double edot = meanMotion / (1 - eccentricity * cosE);
        x *= edot;
        y *= edot;
    }
    else if (eccentricity > 1.0)
    {
        double a = pericenterDistance / (1.0 - eccentricity);
        double sinhE = sinh(E);
        double coshE = cosh(E);
        double meanMotion = 2.0 * PI / period;
        double edot = meanMotion / (eccentricity * coshE - 1.0);
        x = a * sinhE * edot;
        y = -a * sqrt(square(eccentricity) - 1.0) * coshE * edot;
    }
    else
    {
        // Parabolic orbit: E here is D = tan(true_anomaly/2).
        // From Barker's equation: dM/dt = meanMotion = (1 + D^2) * dD/dt
        // vx = -2q*D * dD/dt,  vy = 2q * dD/dt
        double meanMotion = 2.0 * PI / period;
        double Ddot = meanMotion / (1.0 + E * E);
        x = -2.0 * pericenterDistance * E * Ddot;
        y =  2.0 * pericenterDistance * Ddot;
    }

    Vec3d v = orbitPlaneRotation * Vec3d(x, y, 0);

    // Convert to Celestia's coordinate system
    return Vec3d(v.x, v.z, -v.y);
}


// Return the offset from the center
Point3d EllipticalOrbit::positionAtTime(double t) const
{
    t = t - epoch;
    double meanMotion = 2.0 * PI / period;
    double meanAnomaly = meanAnomalyAtEpoch + t * meanMotion;
    double E = eccentricAnomaly(meanAnomaly);

    return positionAtE(E);
}


Vec3d EllipticalOrbit::velocityAtTime(double t) const
{
    t = t - epoch;
    double meanMotion = 2.0 * PI / period;
    double meanAnomaly = meanAnomalyAtEpoch + t * meanMotion;
    double E = eccentricAnomaly(meanAnomaly);

    return velocityAtE(E);
}


double EllipticalOrbit::getPeriod() const
{
    return period;
}


double EllipticalOrbit::getBoundingRadius() const
{
    if (eccentricity < 1.0)
    {
        double apocenter = pericenterDistance * (1.0 + eccentricity) / (1.0 - eccentricity);
        return min(apocenter, MaxOrbitRadius);
    }
    else
        return MaxOrbitRadius;
}


bool EllipticalOrbit::isPeriodic() const
{
    if (eccentricity >= 1.0)
        return false;
    // Treat near-parabolic elliptic orbits whose apocenter exceeds MaxOrbitRadius
    // as open arcs so the renderer doesn't try to close the loop.
    double apocenter = pericenterDistance * (1.0 + eccentricity) / (1.0 - eccentricity);
    return apocenter <= MaxOrbitRadius;
}


void EllipticalOrbit::getValidRange(double& begin, double& end) const
{
    double meanMotion = 2.0 * PI / period;
    double M_max;

    if (eccentricity < 1.0)
    {
        double apocenter = pericenterDistance * (1.0 + eccentricity) / (1.0 - eccentricity);
        if (apocenter <= MaxOrbitRadius)
        {
            // Full elliptic orbit always valid; begin == end signals this.
            begin = end = 0.0;
            return;
        }
        // Near-parabolic: find E where the distance equals MaxOrbitRadius.
        // r = a*(1 - e*cos(E))  =>  cos(E) = (1 - MaxOrbitRadius/a) / e
        double a = pericenterDistance / (1.0 - eccentricity);
        double cosE = (1.0 - MaxOrbitRadius / a) / eccentricity;
        double E_max = acos(max(min(cosE, 1.0), -1.0));
        M_max = E_max - eccentricity * sin(E_max);
    }
    else if (eccentricity > 1.0)
    {
        double a_abs = pericenterDistance / (eccentricity - 1.0);
        double coshEmax = (MaxOrbitRadius / a_abs + 1.0) / eccentricity;
        double E_max = (coshEmax > 1.0) ? acosh(min(coshEmax, 1.0e6)) : 0.0;
        M_max = eccentricity * sinh(E_max) - E_max;
    }
    else // parabolic
    {
        double D_max = sqrt(max(MaxOrbitRadius / pericenterDistance - 1.0, 0.0));
        M_max = D_max + D_max * D_max * D_max / 3.0;
    }

    begin = epoch + (-M_max - meanAnomalyAtEpoch) / meanMotion;
    end   = epoch + ( M_max - meanAnomalyAtEpoch) / meanMotion;
}


void EllipticalOrbit::sample(double, double t, int nSamples,
                             OrbitSampleProc& proc) const
{
    if (eccentricity > 1.0)
    {
        // Sample both the inbound and outbound legs of the hyperbolic orbit,
        // centred on pericenter (E=0), out to the bounding radius.
        // Adaptive curvature-based stepping: more samples near pericenter where
        // curvature is high, fewer far out where the path is nearly straight.
        double a_abs = pericenterDistance / (eccentricity - 1.0);
        double coshEmax = (getBoundingRadius() / a_abs + 1.0) / eccentricity;
        double E_max = (coshEmax > 1.0) ? acosh(min(coshEmax, 1.0e6)) : 0.0;
        double meanMotion = 2.0 * PI / period;
        double b = sqrt(square(eccentricity) - 1.0); // semi-minor axis scale
        double dE = 2.0 * E_max / (double) nSamples;  // base step for a circular-curvature orbit
        // Dynamic clamp: k at pericenter = 1/b^2; scale clamp so segments stay visually smooth
        // even for near-hyperbolic orbits (b small). Formula: max(20, (1/b)^(2/3)).
        double kMax = max(20.0, pow(1.0 / b, 2.0 / 3.0));
        double E = -E_max;
        while (E < E_max)
        {
            double M = eccentricity * sinh(E) - E;
            double tsamp = epoch + (M - meanAnomalyAtEpoch) / meanMotion;
            proc.sample(tsamp, positionAtE(E));

            // Curvature of hyperbolic anomaly parameterisation:
            // k = b / (sinh^2(E) + b^2*cosh^2(E))^(3/2)
            double sinhE = sinh(E);
            double coshE = cosh(E);
            double k = b / pow(square(sinhE) + square(b) * square(coshE), 1.5);
            E += dE / max(min(k, kMax), 1.0);
        }
    }
    else if (eccentricity == 1.0)
    {
        // Sample both legs of the parabolic orbit from -D_max to +D_max,
        // where D_max corresponds to the bounding radius.
        // From r = q*(1 + D^2): D_max = sqrt(r_max/q - 1)
        // Adaptive curvature-based stepping concentrates samples near D=0 (pericenter).
        double D_max = sqrt(max(getBoundingRadius() / pericenterDistance - 1.0, 0.0));
        double meanMotion = 2.0 * PI / period;
        double dD = 2.0 * D_max / (double) nSamples;
        // Dynamic kMax: scaled so the fine-sampling transition falls near the
        // inner solar system scale for small-q comets.  kMax = D_max^(4/3) places
        // the transition at D_trans = D_max^(4/9), i.e. r ~ q * D_max^(8/9).
        // For SWAN (D_max ~ 268, q ~ 0.007 AU) this gives D_trans ~ 12 (r ~ 1 AU).
        double kMax = max(20.0, pow(D_max, 4.0 / 3.0));
        double D = -D_max;
        while (D < D_max)
        {
            double M = D + D * D * D / 3.0;
            double tsamp = epoch + (M - meanAnomalyAtEpoch) / meanMotion;
            proc.sample(tsamp, positionAtE(D));

            // Curvature-proportional step: k = kMax at D=0, falls as (1+D²)^(-3/4).
            // The (3/4) exponent gives step ∝ D^(3/2) for large D, which produces
            // constant relative chord error (sag/distance) across all distances —
            // i.e. uniform visual quality from pericenter out to the bounding radius.
            double k = kMax / pow(1.0 + D * D, 0.75);
            D += dD / max(min(k, kMax), 1.0);
        }
    }
    else
    {
        double apocenter = pericenterDistance * (1.0 + eccentricity) / (1.0 - eccentricity);

        if (apocenter > MaxOrbitRadius)
        {
            // Near-parabolic elliptic orbit: only sample the arc within MaxOrbitRadius,
            // symmetrically around pericenter (E=0), using adaptive curvature stepping.
            double a = pericenterDistance / (1.0 - eccentricity);
            double cosE = (1.0 - MaxOrbitRadius / a) / eccentricity;
            double E_max = acos(max(min(cosE, 1.0), -1.0));
            double meanMotion = 2.0 * PI / period;
            double w = 1.0 - square(eccentricity);
            double dE = 2.0 * E_max / (double) nSamples;
            // Dynamic clamp: k at pericenter = 1/w^2; scale clamp so segments stay visually smooth
            // even for very near-parabolic orbits (w small). Formula: max(20, (1/w)^(2/3)).
            double kMax = max(20.0, pow(1.0 / w, 2.0 / 3.0));
            double E = -E_max;
            while (E < E_max)
            {
                double M = E - eccentricity * sin(E);
                double tsamp = epoch + (M - meanAnomalyAtEpoch) / meanMotion;
                proc.sample(tsamp, positionAtE(E));

                double k = sqrt(w) * pow(square(sin(E)) + w * square(cos(E)), -1.5);
                E += dE / max(min(k, kMax), 1.0);
            }
        }
        else
        {
            // Adaptive sampling of the orbit; more samples in regions of high curvature.
            // nSamples is the number of samples that will be used for a perfectly circular
            // orbit. Elliptical orbits will have regions of higher curvature that require
            // additional sample points.
            double dE = 2 * PI / (double) nSamples;
            double w = (1 - square(eccentricity));
            // Dynamic clamp: k at pericenter = 1/w; scale clamp so segments stay visually smooth
            // even for very near-parabolic elliptic orbits (w small). Formula: max(20, (1/w)^(2/3)).
            // For typical orbits (e < 0.994, w > 0.011) this stays at 20, preserving the original
            // ~3*nSamples sample budget. For extreme orbits the clamp grows, keeping segments smooth.
            double kMax = max(20.0, pow(1.0 / w, 2.0 / 3.0));

            // Both legs must depart outward from pericenter (E=0) so that the high-curvature zone
            // is at the START of each leg and the adaptive stepper naturally uses fine steps there.
            // Stepping toward pericenter does not work: the last step before E=0 is coarse (low
            // curvature at the current point) and overshoots the tight bend.
            //
            // Strategy: collect the inbound leg (E: 0 -> -PI) into a temporary buffer by stepping
            // outward from E=0 in the negative direction, then emit those samples in reverse
            // (i.e. in increasing-time order: -PI -> 0) before emitting the outbound leg (0 -> PI).

            // --- inbound leg: step E from 0 to -PI, buffer in reverse ---
            struct Sample { double t; Point3d pos; };
            vector<Sample> inbound;
            {
                double E = 0.0;
                while (E > -PI)
                {
                    double k = sqrt(w) * pow(square(sin(E)) + w * square(cos(E)), -1.5);
                    double step = dE / max(min(k, kMax), 1.0);
                    E -= step;
                    if (E < -PI) E = -PI;
                    double M = E - eccentricity * sin(E);
                    double tsamp = t + M * period / (2 * PI);
                    inbound.push_back({tsamp, positionAtE(E)});
                }
            }
            for (int i = (int)inbound.size() - 1; i >= 0; i--)
                proc.sample(inbound[i].t, inbound[i].pos);

            // --- outbound leg: step E from 0 to +PI ---
            {
                double E = 0.0;
                while (E < PI)
                {
                    double M = E - eccentricity * sin(E);
                    double tsamp = t + M * period / (2 * PI);
                    proc.sample(tsamp, positionAtE(E));

                    double k = sqrt(w) * pow(square(sin(E)) + w * square(cos(E)), -1.5);
                    E += dE / max(min(k, kMax), 1.0);
                }
            }
        }
    }
}


CachingOrbit::CachingOrbit() :
	lastTime(-1.0e30),
	positionCacheValid(false),
	velocityCacheValid(false)
{
}


CachingOrbit::~CachingOrbit()
{
}


Point3d CachingOrbit::positionAtTime(double jd) const
{
    if (jd != lastTime)
    {
        lastTime = jd;
        lastPosition = computePosition(jd);
		positionCacheValid = true;
		velocityCacheValid = false;
    }
	else if (!positionCacheValid)
	{
		lastPosition = computePosition(jd);
		positionCacheValid = true;
	}

    return lastPosition;
}


Vec3d CachingOrbit::velocityAtTime(double jd) const
{
	if (jd != lastTime)
	{
		lastVelocity = computeVelocity(jd);
		lastTime = jd;  // must be set *after* call to computeVelocity
		positionCacheValid = false;
		velocityCacheValid = true;
	}
	else if (!velocityCacheValid)
	{
		lastVelocity = computeVelocity(jd);
		velocityCacheValid = true;
	}

	return lastVelocity;
}


/*! Calculate the velocity at the specified time (units are
 *  kilometers / Julian day.) The default implementation just
 *  differentiates the position.
 */
Vec3d CachingOrbit::computeVelocity(double jd) const
{
	// Compute the velocity by differentiating.
	Point3d p0 = positionAtTime(jd);

	// Call computePosition() instead of positionAtTime() so that we
	// don't affect the cached value. 
	// TODO: check the valid ranges of the orbit to make sure that
	// jd+dt is still in range.
	Point3d p1 = computePosition(jd + ORBITAL_VELOCITY_DIFF_DELTA);

	return (p1 - p0) * (1.0 / ORBITAL_VELOCITY_DIFF_DELTA);
}


void CachingOrbit::sample(double start, double t, int nSamples,
                          OrbitSampleProc& proc) const
{
    double dt = t / (double) nSamples;
    for (int i = 0; i < nSamples; i++)
        proc.sample(start + dt * i, positionAtTime(start + dt * i));
}


static EllipticalOrbit* StateVectorToOrbit(const Point3d& position,
                                           const Vec3d& v,
                                           double mass,
                                           double t)
{
    Vec3d R = position - Point3d(0.0, 0.0, 0.0);
    Vec3d L = R ^ v;
    double magR = R.length();
    double magL = L.length();
    double magV = v.length();
    L *= (1.0 / magL);

    Vec3d W = L ^ (R / magR);

    double G = astro::G * 1e-9; // convert from meters to kilometers
    double GM = G * mass;

    // Compute the semimajor axis
    double a = 1.0 / (2.0 / magR - square(magV) / GM);

    // Compute the eccentricity
    double p = square(magL) / GM;
    double q = R * v;
    double ex = 1.0 - magR / a;
    double ey = q / sqrt(a * GM);
    double e = sqrt(ex * ex + ey * ey);

    // Compute the mean anomaly
    double E = atan2(ey, ex);
    double M = E - e * sin(E);

    // Compute the inclination
    double cosi = L * Vec3d(0, 1.0, 0);
    double i = 0.0;
    if (cosi < 1.0)
        i = acos(cosi);

    // Compute the longitude of ascending node
    double Om = atan2(L.x, L.z);

    // Compute the argument of pericenter
    Vec3d U = R / magR;
    double s_nu = (v * U) * sqrt(p / GM);
    double c_nu = (v * W) * sqrt(p / GM) - 1;
    s_nu /= e;
    c_nu /= e;
    Vec3d P = U * c_nu - W * s_nu;
    Vec3d Q = U * s_nu + W * c_nu;
    double om = atan2(P.y, Q.y);

    // Compute the period
    double T = 2 * PI * sqrt(cube(a) / GM);
    T = T / 86400.0; // Convert from seconds to days

    return new EllipticalOrbit(a * (1 - e), e, i, Om, om, M, T, t);
}


MixedOrbit::MixedOrbit(Orbit* orbit, double t0, double t1, double mass) :
    primary(orbit),
    afterApprox(NULL),
    beforeApprox(NULL),
    begin(t0),
    end(t1),
    boundingRadius(0.0)
{
    assert(t1 > t0);
    assert(orbit != NULL);

    double dt = 1.0 / 1440.0; // 1 minute
    Point3d p0 = orbit->positionAtTime(t0);
    Point3d p1 = orbit->positionAtTime(t1);
    Vec3d v0 = (orbit->positionAtTime(t0 + dt) - p0) / (86400 * dt);
    Vec3d v1 = (orbit->positionAtTime(t1 + dt) - p1) / (86400 * dt);
    beforeApprox = StateVectorToOrbit(p0, v0, mass, t0);
    afterApprox = StateVectorToOrbit(p1, v1, mass, t1);

    boundingRadius = beforeApprox->getBoundingRadius();
    if (primary->getBoundingRadius() > boundingRadius)
        boundingRadius = primary->getBoundingRadius();
    if (afterApprox->getBoundingRadius() > boundingRadius)
        boundingRadius = afterApprox->getBoundingRadius();
}

MixedOrbit::~MixedOrbit()
{
    if (primary != NULL)
        delete primary;
    if (beforeApprox != NULL)
        delete beforeApprox;
    if (afterApprox != NULL)
        delete afterApprox;
}


Point3d MixedOrbit::positionAtTime(double jd) const
{
    if (jd < begin)
        return beforeApprox->positionAtTime(jd);
    else if (jd < end)
        return primary->positionAtTime(jd);
    else
        return afterApprox->positionAtTime(jd);
}


Vec3d MixedOrbit::velocityAtTime(double jd) const
{
    if (jd < begin)
        return beforeApprox->velocityAtTime(jd);
    else if (jd < end)
        return primary->velocityAtTime(jd);
    else
        return afterApprox->velocityAtTime(jd);
}


double MixedOrbit::getPeriod() const
{
    return primary->getPeriod();
}


double MixedOrbit::getBoundingRadius() const
{
    return boundingRadius;
}


void MixedOrbit::sample(double t0, double t1, int nSamples,
                        OrbitSampleProc& proc) const
{
    Orbit* o;
    if (t0 < begin)
        o = beforeApprox;
    else if (t0 < end)
        o = primary;
    else
        o = afterApprox;
    o->sample(t0, t1, nSamples, proc);
}


/*** FixedOrbit ***/

FixedOrbit::FixedOrbit(const Point3d& pos) :
    position(pos)
{
}


FixedOrbit::~FixedOrbit()
{
}


Point3d
FixedOrbit::positionAtTime(double /*tjd*/) const
{
    return position;
}


bool
FixedOrbit::isPeriodic() const
{
    return false;
}


double
FixedOrbit::getPeriod() const
{
    return 1.0;
}


double
FixedOrbit::getBoundingRadius() const
{
    return position.distanceFromOrigin() * 1.1;
}


void
FixedOrbit::sample(double, double, int, OrbitSampleProc&) const
{
    /*
    for (int i = 0; i < nSamples; i++)
        proc.sample(t, position);
    */
}


/*** SynchronousOrbit ***/
// TODO: eliminate this class once body-fixed reference frames are implemented
SynchronousOrbit::SynchronousOrbit(const Body& _body,
                                   const Point3d& _position) :
    body(_body),
    position(_position)
{
}


SynchronousOrbit::~SynchronousOrbit()
{
}


Point3d SynchronousOrbit::positionAtTime(double jd) const
{
    Quatd q = body.getEquatorialToBodyFixed(jd);
    return position * q.toMatrix3();
}


double SynchronousOrbit::getPeriod() const
{
    return body.getRotationModel(0.0)->getPeriod();
}


double SynchronousOrbit::getBoundingRadius() const
{
    return position.distanceFromOrigin();
}


void SynchronousOrbit::sample(double, double, int, OrbitSampleProc&) const
{
    // Empty method--we never want to show a synchronous orbit.
}


