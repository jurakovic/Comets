// The Ephemeris view over the sample collection at the vectors' clock: the settings form and its
// errors, the long-calculation question, Cancel, result tabs with Edit settings, Save, Copy and
// Close, each saved text against its vector, the comet picker following the database, and a
// timespan in year −146.
import { readFileSync } from 'node:fs';
import { EPHEMERIS_CASES, SAMPLE as SAMPLE_COMETS } from '../helpers/ephemeris.js';
import { createFakeClock } from '../helpers/fakes.js';
import { readVectorText, toLf, VECTOR_NOW } from '../helpers/vectors.js';
import { calculateEphemerides } from '../../app/js/core/ephemeris/calculation.js';
import { defaultEphemerisSettings } from '../../app/js/core/ephemeris/settings.js';
import { ephemerisText } from '../../app/js/core/ephemeris/text.js';
import { addMonths } from '../../app/js/core/time/calendar.js';
import { jdToInstant } from '../../app/js/core/time/julian.js';
import { createObservationsService } from '../../app/js/services/observations.js';
import { expect, seed, test } from './fixtures.js';

const SAMPLE = readVectorText('import/sample.txt');
const MERGE_NEW = readVectorText('import/merge-new.txt');
const MPC_URL = 'https://minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt';
const NOW = new Date(VECTOR_NOW);
/** the sample's brightest comet at the vectors' clock, which a new form chooses */
const BRIGHTEST = 'C/2026 T99 (Parabola Test)';
/** the output columns' labels, by the vectors' keys */
const COLUMN_LABELS = Object.freeze({
	RA: 'Right ascension (RA)', Dec: 'Declination (Dec)', Alt: 'Altitude (Alt)', Az: 'Azimuth (Az)', EcLon: 'Ecliptic Longitude',
	EcLat: 'Ecliptic Latitude', Elongation: 'Elongation', HelioDist: 'Sun distance', GeoDist: 'Earth distance', Magnitude: 'Magnitude'
});
/** the requirements' fields, by the vectors' keys */
const REQUIREMENT_LABELS = Object.freeze({ maxSunDist: 'Max Sun distance', maxEarthDist: 'Max Earth distance', minMagnitude: 'Min magnitude' });
/** the local time of NOW in a saved file's name, by time zone */
const SAVED_AT = Object.freeze({ UTC: '2026-10-18_120000', 'Europe/Zagreb': '2026-10-18_140000' });

/** @param {import('@playwright/test').Page} page */
const viewOf = page => page.locator('[data-view="ephemeris"]');
/** @param {import('@playwright/test').Locator} view */
const comboOf = view => view.getByRole('combobox', { name: 'Comet' });
/** @param {import('@playwright/test').Locator} combo the text of the entry shown */
const shownOf = combo => combo.evaluate(select => /** @type {HTMLSelectElement} */ (select).selectedOptions[0]?.text ?? null);
/** @param {import('@playwright/test').Locator} view */
const tabsOf = view => view.getByRole('tablist', { name: 'Results' }).getByRole('tab');
/** @param {import('@playwright/test').Locator} view the text the selected result shows */
const resultText = view => view.getByRole('tabpanel').locator('pre');

/**
 * the sample stored, at the vectors' clock, and the Ephemeris view open over it
 * @param {import('@playwright/test').Page} page
 * @param {object} [settings] stored settings
 */
async function open(page, settings) {
	await page.clock.setFixedTime(NOW);
	await seed(page, { database: SAMPLE, settings });
	await page.goto('./#/ephemeris');
	const view = viewOf(page);
	await expect(comboOf(view).getByRole('option')).toHaveCount(35);
	return view;
}

/**
 * a date picker's date set through its editor's JD
 * @param {import('@playwright/test').Page} page
 * @param {string} name Start or End
 * @param {string} iso a UTC instant
 */
async function setDate(page, name, iso) {
	await viewOf(page).getByRole('group', { name, exact: true }).getByRole('button').first().click();
	const editor = page.getByRole('dialog', { name: 'Universal Time (UTC)' });
	await editor.getByRole('textbox', { name: 'JD' }).fill(String(Date.parse(iso) / 86_400_000 + 2440587.5));
	await editor.getByRole('button', { name: 'OK' }).click();
	await expect(editor).toHaveCount(0);
}

/**
 * a preset of a date picker's menu
 * @param {import('@playwright/test').Page} page
 * @param {string} name Start or End
 * @param {string} preset
 */
async function pickPreset(page, name, preset) {
	await viewOf(page).getByRole('group', { name, exact: true }).getByRole('button', { name: 'Date presets' }).click();
	await page.getByRole('menu', { name: 'Date presets' }).getByRole('menuitem', { name: preset }).click();
}

/**
 * the interval's D, H and M
 * @param {import('@playwright/test').Locator} view
 * @param {[number, number, number]} parts
 */
async function setInterval(view, [days, hours, minutes]) {
	await view.getByRole('textbox', { name: 'Days' }).fill(String(days));
	await view.getByRole('textbox', { name: 'Hours' }).fill(String(hours));
	await view.getByRole('textbox', { name: 'Minutes' }).fill(String(minutes));
}

/**
 * the form filled as a vector case's settings say
 * @param {import('@playwright/test').Page} page
 * @param {any} vector
 */
async function fillCase(page, vector) {
	const view = viewOf(page);
	await comboOf(view).selectOption({ label: vector.multiple ? '[ALL COMETS (34)]' : vector.comets });
	await setDate(page, 'Start', vector.startUtc);
	await setDate(page, 'End', vector.stopUtc);
	const total = Math.round(vector.intervalDays * 1440);
	await setInterval(view, [Math.floor(total / 1440), Math.floor(total / 60) % 24, total % 60]);
	await view.getByRole('radio', { name: vector.localTime ? 'Local Time' : 'Universal Time' }).check();
	for (const [key, label] of Object.entries(COLUMN_LABELS)) {
		await view.getByRole('checkbox', { name: label, exact: true }).setChecked(vector.columns[key]);
	}
	for (const [key, label] of Object.entries(REQUIREMENT_LABELS)) {
		const value = vector.requirements[key];
		await view.getByRole('textbox', { name: label }).fill(value === null ? '' : String(value));
		await expect(view.getByRole('checkbox', { name: label })).toBeChecked({ checked: value !== null });
	}
}

/**
 * Save's download: its name, and its text with LF line endings, checked to have CRLF
 * @param {import('@playwright/test').Page} page
 * @param {() => Promise<unknown>} save
 */
async function saved(page, save) {
	const [download] = await Promise.all([page.waitForEvent('download'), save()]);
	const text = readFileSync(await download.path(), 'utf8');
	expect(text).toMatch(/^(?:[^\r\n]*\r\n)*$/);
	return { name: download.suggestedFilename(), text: toLf(text) };
}

test('a new form: a copy of the app-wide selection, its brightest chosen, and the default settings', async ({ page }) => {
	const view = await open(page);
	const combo = comboOf(view);
	expect(await shownOf(combo)).toBe(BRIGHTEST);
	await expect(view.getByRole('group', { name: 'Start', exact: true }).getByRole('button').first()).toHaveText('01.09.2026. 00:00:00');
	await expect(view.getByRole('group', { name: 'End', exact: true }).getByRole('button').first()).toHaveText('01.03.2027. 00:00:00');
	await expect(view.getByRole('textbox', { name: 'Days' })).toHaveValue('1');
	await expect(view.getByRole('textbox', { name: 'Hours' })).toHaveValue('0');
	await expect(view.getByRole('textbox', { name: 'Minutes' })).toHaveValue('0');
	await expect(view.getByRole('radio', { name: 'Local Time' })).toBeChecked();
	const columns = await Promise.all(Object.values(COLUMN_LABELS).map(label => view.getByRole('checkbox', { name: label, exact: true }).isChecked()));
	expect(columns).toEqual([true, true, true, true, false, false, true, true, true, true]);
	await expect(view.getByRole('textbox', { name: 'Min magnitude' })).toHaveValue('12');
	await expect(view.getByRole('checkbox', { name: 'Min magnitude' })).toBeChecked();
	await expect(view.getByRole('checkbox', { name: 'Max Sun distance' })).not.toBeChecked();
	await expect(tabsOf(view)).toHaveText(['Settings']);
	// the Default preset gives the default dates back
	await pickPreset(page, 'End', 'Add 1 year');
	await pickPreset(page, 'End', 'Default');
	await expect(view.getByRole('group', { name: 'End', exact: true }).getByRole('button').first()).toHaveText('01.03.2027. 00:00:00');
	// as does Default the interval
	await setInterval(view, [0, 2, 30]);
	await view.getByRole('button', { name: 'Default' }).click();
	await expect(view.getByRole('textbox', { name: 'Hours' })).toHaveValue('0');
	await expect(view.getByRole('textbox', { name: 'Days' })).toHaveValue('1');
});

test('the form starts from the app-wide selection as it is when the view first opens', async ({ page }) => {
	await page.clock.setFixedTime(NOW);
	await seed(page, { database: SAMPLE });
	const database = page.locator('[data-view="database"]');
	await expect(database.getByRole('listbox', { name: 'Comets' }).getByRole('option')).toHaveCount(34);
	await database.getByRole('button', { name: 'Filters' }).click();
	await database.getByRole('group', { name: 'Filter 3', exact: true }).getByRole('textbox', { name: 'Value' }).pressSequentially('1');
	await expect(database.getByRole('heading', { level: 1 })).toHaveText(/^Database \(\d+\/34 comets\)$/);
	const selected = await database.getByRole('listbox', { name: 'Comets' }).getByRole('option').allTextContents();
	expect(selected.length).toBeLessThan(34);
	await page.locator('.shell-nav a[href="#/ephemeris"]').click();
	const options = comboOf(viewOf(page)).getByRole('option');
	await expect(options).toHaveText([`[ALL COMETS (${selected.length})]`, ...selected]);
});

for (const { id, file, settings: vector } of EPHEMERIS_CASES) {
	test.describe(`${id}, in ${vector.timeZone}`, () => {
		test.use({ timezoneId: vector.timeZone });

		test('the saved text equals its vector', async ({ page }) => {
			const { Name: name, Latitude: latitude, Longitude: longitude } = vector.location;
			const view = await open(page, { location: { name, latitude, longitude } });
			await fillCase(page, vector);
			await view.getByRole('button', { name: 'Calculate' }).click();
			const tab = tabsOf(view).nth(1);
			await expect(tab).toHaveText(vector.title);
			await expect(tab).toHaveAttribute('aria-selected', 'true');
			const { name: fileName, text } = await saved(page, () => view.getByRole('button', { name: 'Save' }).click());
			expect(fileName).toBe(`Comets_Ephemeris_${SAVED_AT[/** @type {keyof typeof SAVED_AT} */ (vector.timeZone)]}.txt`);
			expect(text).toBe(readVectorText(file));
			expect(await resultText(view).textContent()).toBe(text);
		});
	});
}

test('errors show by their fields, the first focused, and follow the fields once shown; nothing is calculated', async ({ page }) => {
	const view = await open(page);
	await setDate(page, 'End', '2026-08-01T00:00:00Z');
	await view.getByRole('checkbox', { name: 'Max Sun distance' }).check();
	await view.getByRole('button', { name: 'Calculate' }).click();
	const sun = view.getByRole('textbox', { name: 'Max Sun distance' });
	const start = view.getByRole('group', { name: 'Start', exact: true }).getByRole('button').first();
	await expect(view.getByRole('alert')).toHaveText(['End date must be greater than start date', 'Please enter Maximum Sun distance value']);
	await expect(sun).toBeFocused();
	await expect(sun).toHaveAttribute('aria-invalid', 'true');
	await expect(sun).toHaveAccessibleDescription('Please enter Maximum Sun distance value');
	await expect(start).toHaveAttribute('aria-invalid', 'true');
	await expect(start).toHaveAccessibleDescription('End date must be greater than start date');
	await sun.pressSequentially('1');
	await expect(view.getByRole('alert')).toHaveText(['End date must be greater than start date']);
	await expect(sun).not.toHaveAttribute('aria-invalid');
	await pickPreset(page, 'End', 'Default');
	await expect(view.getByRole('alert')).toHaveCount(0);
	await expect(start).not.toHaveAttribute('aria-invalid');
	await expect(tabsOf(view)).toHaveCount(1);
});

test('a long calculation asks first; No calculates nothing, Yes runs it with its progress, and Cancel discards it', async ({ page }) => {
	const view = await open(page);
	const calculate = view.getByRole('button', { name: 'Calculate' });
	const job = page.locator('.shell-status__job');
	await comboOf(view).selectOption({ label: '[ALL COMETS (34)]' });
	await setInterval(view, [0, 0, 1]);
	await calculate.click();
	const question = page.getByRole('alertdialog');
	await expect(question).toHaveText(/^Calculation using selected settings could take a while, depending on your PC performances\. Are you sure you want to continue\?/);
	await question.getByRole('button', { name: 'No' }).click();
	await expect(question).toHaveCount(0);
	await expect(job).toBeHidden();
	await expect(calculate).toBeEnabled();
	// Yes: the job runs, shown in the status area, and the form's Cancel stops it
	await calculate.click();
	await question.getByRole('button', { name: 'Yes' }).click();
	await expect(job).toBeVisible();
	await expect(calculate).toBeDisabled();
	await view.getByRole('button', { name: 'Cancel' }).click();
	await expect(job).toBeHidden();
	await expect(calculate).toBeEnabled();
	await expect(view.getByRole('button', { name: 'Cancel' })).toBeHidden();
	// the status area's Cancel stops it too
	await calculate.click();
	await question.getByRole('button', { name: 'Yes' }).click();
	await expect(job.locator('progress')).toHaveJSProperty('max', 34);
	await job.getByRole('button', { name: 'Cancel' }).click();
	await expect(job).toBeHidden();
	await expect(calculate).toBeEnabled();
	await expect(tabsOf(view)).toHaveCount(1);
});

test('without the question when its setting is off', async ({ page }) => {
	const view = await open(page, { showLongCalculationConfirmation: false });
	await setInterval(view, [0, 0, 1]);
	await view.getByRole('button', { name: 'Calculate' }).click();
	await expect(page.locator('.shell-status__job')).toBeVisible();
	await expect(page.getByRole('alertdialog')).toHaveCount(0);
	await view.getByRole('button', { name: 'Cancel' }).click();
	await expect(page.locator('.shell-status__job')).toBeHidden();
});

test('a tab per result; Edit settings, then Calculate replaces the tab; New ephemeris; Copy, Ctrl+S, Close and Delete', async ({ page, context }) => {
	await context.grantPermissions(['clipboard-read', 'clipboard-write']);
	const view = await open(page);
	const combo = comboOf(view);
	const calculate = view.getByRole('button', { name: 'Calculate' });
	const editing = view.locator('.ephemeris-form__editing');
	await calculate.click();
	await expect(tabsOf(view)).toHaveText(['Settings', `Ephemeris - ${BRIGHTEST}`]);
	const first = await resultText(view).textContent();
	expect(first).toMatch(new RegExp(`^Comet: {15}\\t${BRIGHTEST.replace(/[()]/g, '\\$&')}\\n`));
	// Copy: the text, with the line breaks it shows
	await view.getByRole('button', { name: 'Copy' }).click();
	await expect.poll(() => page.evaluate(() => navigator.clipboard.readText())).toBe(first);
	// Ctrl+S saves the result shown
	const { name, text } = await saved(page, () => page.keyboard.press('Control+s'));
	expect(name).toBe('Comets_Ephemeris_2026-10-18_120000.txt');
	expect(text).toBe(first);

	await tabsOf(view).first().click();
	await combo.selectOption({ label: '[ALL COMETS (34)]' });
	await calculate.click();
	await expect(tabsOf(view)).toHaveText(['Settings', `Ephemeris - ${BRIGHTEST}`, 'Ephemeris - 34 comets']);
	await expect(tabsOf(view).nth(2)).toHaveAttribute('aria-selected', 'true');

	// Edit settings shows the tab's own settings, and Calculate replaces it
	await tabsOf(view).nth(1).click();
	await view.getByRole('button', { name: 'Edit settings' }).click();
	await expect(tabsOf(view).first()).toHaveAttribute('aria-selected', 'true');
	await expect(tabsOf(view).first()).toBeFocused();
	await expect(editing).toContainText(`Editing Ephemeris - ${BRIGHTEST}`);
	expect(await shownOf(combo)).toBe(BRIGHTEST);
	await combo.selectOption({ label: '1P/Halley' });
	await view.getByRole('textbox', { name: 'Min magnitude' }).fill('');
	await calculate.click();
	await expect(tabsOf(view)).toHaveText(['Settings', 'Ephemeris - 1P/Halley', 'Ephemeris - 34 comets']);
	await expect(tabsOf(view).nth(1)).toHaveAttribute('aria-selected', 'true');
	await expect(resultText(view)).toContainText('Comet:               \t1P/Halley');
	// the new form as it was before the edit
	await expect(editing).toBeHidden();
	await tabsOf(view).first().click();
	expect(await shownOf(combo)).toBe('[ALL COMETS (34)]');
	await expect(view.getByRole('textbox', { name: 'Min magnitude' })).toHaveValue('12');

	// the edited tab's settings are those it was calculated with; New ephemeris leaves them
	await tabsOf(view).nth(1).click();
	await view.getByRole('button', { name: 'Edit settings' }).click();
	expect(await shownOf(combo)).toBe('1P/Halley');
	await expect(view.getByRole('textbox', { name: 'Min magnitude' })).toHaveValue('');
	await view.getByRole('button', { name: 'New ephemeris' }).click();
	await expect(editing).toBeHidden();
	expect(await shownOf(combo)).toBe('[ALL COMETS (34)]');

	// Close, and Delete on a tab; the tab before is selected
	await tabsOf(view).nth(2).click();
	await view.getByRole('button', { name: 'Close' }).click();
	await expect(tabsOf(view)).toHaveText(['Settings', 'Ephemeris - 1P/Halley']);
	await expect(tabsOf(view).nth(1)).toHaveAttribute('aria-selected', 'true');
	await expect(tabsOf(view).nth(1)).toBeFocused();
	await page.keyboard.press('Delete');
	await expect(tabsOf(view)).toHaveText(['Settings']);
	await expect(tabsOf(view).first()).toBeFocused();
});

test('closing the tab being edited goes back to the new form', async ({ page }) => {
	const view = await open(page);
	await view.getByRole('button', { name: 'Calculate' }).click();
	await expect(tabsOf(view)).toHaveCount(2);
	await view.getByRole('button', { name: 'Edit settings' }).click();
	await comboOf(view).selectOption({ label: '2P/Encke' });
	await tabsOf(view).nth(1).click();
	await view.getByRole('button', { name: 'Close' }).click();
	await expect(view.locator('.ephemeris-form__editing')).toBeHidden();
	expect(await shownOf(comboOf(view))).toBe(BRIGHTEST);
});

test('the comet picker reads the database again after a deletion or an import, keeping its sort and its comet', async ({ page }) => {
	await page.route(MPC_URL, route => route.fulfill({ status: 200, headers: { 'access-control-allow-origin': '*' }, body: MERGE_NEW }));
	const view = await open(page);
	const combo = comboOf(view);
	await view.getByRole('button', { name: 'Sort by' }).click();
	await page.getByRole('menu', { name: 'Sort by' }).getByRole('menuitemradio', { name: 'Perihelion distance', exact: true }).click();
	await combo.selectOption({ label: '12P/Pons-Brooks' });
	const byDistance = await combo.getByRole('option').allTextContents();

	// a deletion in the Database view, while the Ephemeris view is hidden
	await page.locator('.shell-nav a[href="#/database"]').click();
	const database = page.locator('[data-view="database"]');
	await database.getByRole('listbox', { name: 'Comets' }).getByRole('option', { name: '1P/Halley', exact: true }).click();
	await database.getByRole('button', { name: 'Delete' }).click();
	await page.getByRole('alertdialog').getByRole('button', { name: 'Yes' }).click();
	await expect(database.getByRole('heading', { level: 1 })).toHaveText('Database (33 comets)');
	await page.locator('.shell-nav a[href="#/ephemeris"]').click();
	await expect(combo.getByRole('option')).toHaveText(['[ALL COMETS (33)]', ...byDistance.slice(1).filter(full => full !== '1P/Halley')]);
	expect(await shownOf(combo)).toBe('12P/Pons-Brooks');

	// an import from the Update dialog over the view: 21P and 1P come in, 2P changes
	await page.locator('.shell-nav a[href="#/update"]').click();
	const dialog = page.getByRole('dialog', { name: 'Update' });
	await dialog.getByRole('button', { name: 'Download' }).click();
	await dialog.getByRole('button', { name: 'Import' }).click();
	await expect(combo.getByRole('option')).toHaveCount(36);
	await expect(combo.getByRole('option', { name: '21P/Giacobini-Zinner' })).toHaveCount(1);
	expect(await shownOf(combo)).toBe('12P/Pons-Brooks');
	const relisted = await combo.getByRole('option').allTextContents();
	const q = new Map(SAMPLE_COMETS.map(c => [c.full, c.q]));
	const known = relisted.slice(1).filter(full => q.has(full) && full !== '2P/Encke');
	expect(known).toEqual(known.toSorted((a, b) => /** @type {number} */ (q.get(a)) - /** @type {number} */ (q.get(b))));
});

test('Start and End from the Perihelion Date preset of C/-146 P1, in year −146', async ({ page }) => {
	const view = await open(page);
	await comboOf(view).selectOption({ label: 'C/-146 P1' });
	await pickPreset(page, 'Start', 'Perihelion Date');
	await pickPreset(page, 'End', 'Perihelion Date');
	await pickPreset(page, 'End', 'Add 3 months');
	await expect(view.getByRole('group', { name: 'Start', exact: true }).getByRole('button').first()).toHaveText('13.08.-0146. 12:00:00');
	await expect(view.getByRole('group', { name: 'End', exact: true }).getByRole('button').first()).toHaveText('13.11.-0146. 12:00:00');
	await view.getByRole('textbox', { name: 'Min magnitude' }).fill('');
	await view.getByRole('button', { name: 'Calculate' }).click();
	await expect(tabsOf(view).nth(1)).toHaveText('Ephemeris - C/-146 P1');

	// the same calculation in core, at the vectors' clock
	const comet = /** @type {import('../../app/js/core/comets/comet.js').Comet} */ (SAMPLE_COMETS.find(c => c.full === 'C/-146 P1'));
	const observations = createObservationsService({ clock: createFakeClock(VECTOR_NOW), observer: () => ({ latitude: 0, longitude: 0 }) });
	const Tn = observations.nearestPerihelion(comet);
	const start = jdToInstant(Tn);
	const none = { checked: false, value: null };
	const settings = { ...defaultEphemerisSettings(VECTOR_NOW), start, end: addMonths(start, 3), requirements: { maxSunDist: none, maxEarthDist: none, minMagnitude: none } };
	const calculation = calculateEphemerides({ comets: [comet], nearest: [Tn], settings, observer: { latitude: 0, longitude: 0 }, timeZone: 'UTC' });
	let next = calculation.next();
	while (!next.done) next = calculation.next();
	const expected = toLf(ephemerisText(next.value, settings, 'UTC'));
	expect(expected).toMatch(/^Comet: {15}\tC\/-146 P1\nPerihelion date: {5}\t13 Aug -0146 12:00:00\n/m);
	// a JD reads a date as Gregorian, and JDToDateTime gives a Julian one before 1582, so the rows
	// start three days after the Start shown, as on the desktop
	expect(expected).toMatch(/^ {5}Local Time .*\n16\.08\.-0146 12:00 /m);
	expect(expected).toMatch(/\n16\.11\.-0146 12:00 [^\n]*\n\n\n\n$/);
	expect(await resultText(view).textContent()).toBe(expected);
});
