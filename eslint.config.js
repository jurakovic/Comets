// ESLint over the web app, its tests and its tooling. Beyond the usual checks it
// holds the layers to the dependency rule of docs/spec/12-architecture.md: what
// each folder of app/js may import, and which globals it may touch.
import js from '@eslint/js';
import globals from 'globals';
import importX from 'eslint-plugin-import-x';

const JS = './app/js';

// a folder may not import from these (12-architecture.md, Layers and the dependency rule)
const zones = [
	{ target: `${JS}/lib`, from: [`${JS}/core`, `${JS}/services`, `${JS}/adapters`, `${JS}/workers`, `${JS}/ui`, `${JS}/main.js`] },
	{ target: `${JS}/core`, from: [`${JS}/services`, `${JS}/adapters`, `${JS}/workers`, `${JS}/ui`, `${JS}/main.js`] },
	{ target: `${JS}/services`, from: [`${JS}/adapters`, `${JS}/workers`, `${JS}/ui`, `${JS}/main.js`] },
	{ target: `${JS}/adapters`, from: [`${JS}/services`, `${JS}/workers`, `${JS}/ui`, `${JS}/main.js`] },
	{ target: `${JS}/workers`, from: [`${JS}/services`, `${JS}/adapters`, `${JS}/ui`, `${JS}/main.js`] },
	{ target: `${JS}/ui`, from: [`${JS}/adapters`, `${JS}/workers`, `${JS}/main.js`] }
].map(zone => ({ ...zone, message: 'breaks the dependency rule (docs/spec/12-architecture.md)' }));

// what services may use besides the language: everything else reaches them through a port
const serviceGlobals = Object.fromEntries(
	['AbortController', 'AbortSignal', 'DOMException', 'setTimeout', 'clearTimeout', 'queueMicrotask', 'structuredClone', 'console']
		.map(name => [name, 'readonly'])
);

// the clock and chance come in as parameters, so a test can fix them
const deterministic = [
	{ selector: "CallExpression[callee.object.name='Date'][callee.property.name='now']", message: 'take now from a parameter (a Clock in services)' },
	{ selector: "NewExpression[callee.name='Date'][arguments.length=0]", message: 'take now from a parameter (a Clock in services)' },
	{ selector: "CallExpression[callee.object.name='Math'][callee.property.name='random']", message: 'core is deterministic' }
];

export default [
	{ ignores: ['node_modules/**', 'test-results/**', 'playwright-report/**', 'src/**', 'tools/**', 'docs/**'] },
	js.configs.recommended,
	{
		files: ['app/js/**/*.js'],
		languageOptions: { sourceType: 'module', globals: {} },
		plugins: { 'import-x': importX },
		rules: {
			'import-x/no-unresolved': 'error',
			'import-x/named': 'error',
			'import-x/export': 'error',
			'import-x/extensions': ['error', 'always'],
			'import-x/no-default-export': 'error',
			'import-x/no-cycle': 'error',
			'import-x/no-restricted-paths': ['error', { zones }],
			'no-restricted-properties': ['error',
				{ property: 'toFixed', message: 'rounds unlike .NET; use core/format' },
				{ property: 'substr', message: 'does not fail on short lines; use the field access in core/formats' }
			],
			'max-lines': ['error', { max: 400, skipBlankLines: true, skipComments: true }],
			eqeqeq: 'error',
			'no-var': 'error',
			'prefer-const': 'error'
		}
	},
	{
		files: ['app/js/core/**/*.js', 'app/js/lib/**/*.js'],
		rules: { 'no-restricted-syntax': ['error', ...deterministic] }
	},
	{
		// where JavaScript differs from the .NET the vectors come from (CLAUDE.md); lib/math.js
		// builds roundEven on Math.round
		files: ['app/js/core/**/*.js'],
		rules: {
			'no-restricted-globals': ['error',
				{ name: 'Date', message: 'instants are numbers; use core/time (Date maps years 0–99 to 19xx)' },
				{ name: 'parseFloat', message: 'accepts text .NET rejects; parse with the strict rules of spec 03' },
				{ name: 'parseInt', message: 'accepts text .NET rejects; parse with the strict rules of spec 03' }
			],
			'no-restricted-properties': ['error',
				{ property: 'toFixed', message: 'rounds unlike .NET; use core/format' },
				{ property: 'substr', message: 'does not fail on short lines; use the field access in core/formats' },
				{ object: 'Math', property: 'round', message: 'rounds halves up; .NET rounds them to even (roundEven in lib/math.js)' },
				{ object: 'Number', property: 'parseFloat', message: 'accepts text .NET rejects; parse with the strict rules of spec 03' },
				{ object: 'Number', property: 'parseInt', message: 'accepts text .NET rejects; parse with the strict rules of spec 03' },
				{ property: 'localeCompare', message: 'sorts by the default locale; use Intl.Collator(\'und\') (spec 02, Sorting)' }
			]
		}
	},
	{
		files: ['app/js/services/**/*.js'],
		languageOptions: { globals: serviceGlobals }
	},
	{
		files: ['app/js/workers/**/*.js'],
		languageOptions: { globals: globals.worker }
	},
	{
		files: ['app/js/adapters/**/*.js', 'app/js/ui/**/*.js', 'app/js/main.js'],
		languageOptions: { globals: globals.browser }
	},
	{
		// page.evaluate callbacks run in the browser
		files: ['tests/**/*.js', '*.config.js'],
		languageOptions: { sourceType: 'module', globals: { ...globals.node, ...globals.browser } }
	}
];
