// The curve through a value series: a cardinal spline, as the desktop's chart draws its series.

/**
 * what a curve is traced into: a Canvas 2D context or a Path2D
 * @typedef {object} PathSink
 * @property {(x: number, y: number) => void} moveTo
 * @property {(cx1: number, cy1: number, cx2: number, cy2: number, x: number, y: number) => void} bezierCurveTo
 */

/**
 * traces a cardinal spline through the points as GDI+ draws a curve of that tension: a cubic
 * Bézier from each point to the next, whose control points lie tension / 3 of the difference of
 * the point's neighbours away from it, or of the point and its one neighbour at an end of the
 * curve. Tension 0 traces straight segments. A point that is not finite breaks the curve, and a
 * lone point traces nothing to stroke.
 * @param {PathSink} path
 * @param {ArrayLike<number>} xs
 * @param {ArrayLike<number>} ys
 * @param {number} [tension]
 */
export function traceCurve(path, xs, ys, tension = 0.5) {
	const k = tension / 3;
	const finite = (/** @type {number} */ i) => Number.isFinite(xs[i]) && Number.isFinite(ys[i]);
	let start = 0;
	while (start < xs.length) {
		if (!finite(start)) {
			start++;
			continue;
		}
		let end = start + 1;
		while (end < xs.length && finite(end)) end++;
		// the run start … end − 1
		const before = (/** @type {number} */ i) => Math.max(start, i - 1);
		const after = (/** @type {number} */ i) => Math.min(end - 1, i + 1);
		path.moveTo(xs[start], ys[start]);
		for (let i = start; i < end - 1; i++) {
			const j = i + 1;
			path.bezierCurveTo(
				xs[i] + k * (xs[after(i)] - xs[before(i)]), ys[i] + k * (ys[after(i)] - ys[before(i)]),
				xs[j] - k * (xs[after(j)] - xs[before(j)]), ys[j] - k * (ys[after(j)] - ys[before(j)]),
				xs[j], ys[j]
			);
		}
		start = end;
	}
}
