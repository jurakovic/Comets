// Running the core's generators (docs/spec/12-architecture.md, Long computations) in tests.

/**
 * runs a generator to its end, collecting what it yields
 * @template Y, R
 * @param {Generator<Y, R, void>} generator
 * @returns {{ yielded: Y[], result: R }}
 */
export function drain(generator) {
	const yielded = [];
	let next = generator.next();
	while (!next.done) {
		yielded.push(next.value);
		next = generator.next();
	}
	return { yielded, result: next.value };
}
