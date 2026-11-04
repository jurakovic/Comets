// SpaceEngine (.sc), export only. docs/spec/03-element-formats.md, SpaceEngine.
import { compileFormat } from '../format/composite.js';

// Char.IsNumber
const NUMBER = /^\p{N}/u;
const ABS_MAGN = compileFormat('\tAbsMagn     {0:0.##}');
const SLOPE = compileFormat('\tSlopeParam  {0:0.##}');
const ORBIT = [
	compileFormat('\t\tEpoch            {0:0.0000}'),
	compileFormat('\t\tPericenterDist   {1:0.000000}'),
	compileFormat('\t\tEccentricity     {2:0.000000}'),
	compileFormat('\t\tInclination      {3:0.0000}'),
	compileFormat('\t\tAscendingNode    {4:0.0000}'),
	compileFormat('\t\tArgOfPericenter  {5:0.0000}')
];

/**
 * the object's name: the name alone without an id, spaces for slashes after a numbered id, and
 * no slashes after a lettered one
 * @param {import('../comets/comet.js').Comet} c
 */
function objectName(c) {
	if (c.id === '') return c.name;
	return NUMBER.test(c.id) ? c.full.replaceAll('/', ' ') : c.full.replaceAll('/', '');
}

/** @type {import('./registry.js').Writer} */
export const writer = {
	lines(c) {
		const orbit = [c.T, c.q, c.e, c.i, c.N, c.w];
		return [
			`Comet\t"${objectName(c)}"`,
			'{',
			'\tParentBody "Sol"',
			`\tCometType  "${c.id === '' || c.id[0] !== 'C' ? 'P' : 'C'}"`,
			ABS_MAGN([c.g]),
			SLOPE([c.k]),
			'',
			'\tOrbit',
			'\t{',
			...ORBIT.map(line => line(orbit)),
			'\t\tMeanAnomaly      0',
			'\t}',
			'}',
			''
		];
	}
};
