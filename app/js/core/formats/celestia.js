// Celestia (.ssc), export only. docs/spec/03-element-formats.md, Celestia.
import { compileFormat } from '../format/composite.js';
import { monthAbbreviation } from './months.js';

const MAGNITUDE = compileFormat('\t#Magnitude g {0:0.0}, k {1:0.0}');
const PERIOD = compileFormat('\t\tPeriod         {0,20:0.000000}');
const ELEMENTS = [
	compileFormat('\t\tPericenterDistance        {0,9:0.000000}'),
	compileFormat('\t\tEccentricity               {1,8:0.000000}'),
	compileFormat('\t\tInclination              {2,8:0.0000}'),
	compileFormat('\t\tAscendingNode            {3,8:0.0000}'),
	compileFormat('\t\tArgOfPericenter          {4,8:0.0000}')
];
const EPOCH = compileFormat('\t\tEpoch                {0,12:0.0000}     # {1,4} {2} {3:00}.{4:0000}');

/** @type {import('./registry.js').Writer} */
export const writer = {
	lines(c) {
		const elements = [c.q, c.e, c.i, c.N, c.w];
		return [
			`"${c.full.replaceAll('/', ' ')}" "Sol"`,
			'{',
			'\tClass "comet"',
			'\tMesh "asteroid.cms"',
			'\tTexture "asteroid.jpg"',
			'\tRadius 5',
			'\tAlbedo 0.1',
			MAGNITUDE([c.g, c.k]),
			'\tEllipticalOrbit',
			'\t{',
			// open orbits have no period; Celestia computes it
			...(c.e < 1 ? [PERIOD([c.P])] : []),
			...ELEMENTS.map(line => line(elements)),
			'\t\tMeanAnomaly                0.0',
			EPOCH([c.T, c.Ty, monthAbbreviation(c.Tm), c.Td, c.Th]),
			'\t}',
			'}',
			''
		];
	}
};
