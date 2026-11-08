// The shared components in a real browser, each mounted alone on the page: the number field and
// the sort menu (docs/spec/09-settings-and-persistence.md, Number fields; docs/spec/02-data-model.md,
// Sorting).
import { expect, test } from './fixtures.js';

/**
 * mounts a number field over the Help view and records what it reports
 * @param {import('@playwright/test').Page} page
 * @param {object} options the field's options, without onInput
 */
async function mountNumberField(page, options) {
	await page.goto('./#/help');
	await page.locator('[data-view="help"]').waitFor();
	await page.evaluate(async options => {
		const { createNumberField } = await import(new URL('js/ui/components/number-field.js', location.href).href);
		/** @type {any} */
		const probe = window;
		probe.inputs = [];
		probe.field = createNumberField({ ...options, label: 'Probe', onInput: (/** @type {string} */ text) => probe.inputs.push(text) });
		document.body.prepend(probe.field.element);
	}, options);
	return page.getByRole('textbox', { name: 'Probe' });
}

/** @param {import('@playwright/test').Page} page */
const inputs = page => page.evaluate(() => /** @type {any} */ (window).inputs);

test.describe('the number field', () => {
	test('refuses what would make the text invalid or out of range', async ({ page }) => {
		const field = await mountNumberField(page, { range: { min: -20, max: 40, decimals: 2 } });
		await field.pressSequentially('a-1x2.3,45');
		await expect(field).toHaveValue('-12.34');
		await field.fill('');
		await field.pressSequentially('41');
		await expect(field).toHaveValue('4');
		await field.press('End');
		await field.press('Backspace');
		await expect(field).toHaveValue('');
		expect(await inputs(page)).toEqual(['-', '-1', '-12', '-12.', '-12.3', '-12.34', '', '4', '']);
	});

	test('a typed or pasted , is a .', async ({ page }) => {
		const field = await mountNumberField(page, { range: { min: 0, max: 90, decimals: 6 } });
		await field.pressSequentially('45,5');
		await expect(field).toHaveValue('45.5');
		await field.fill('');
		await page.evaluate(() => navigator.clipboard?.writeText?.('12,25').catch(() => {}));
		// a paste as the browser sends it, without the clipboard
		await field.evaluate(input => {
			const event = new InputEvent('beforeinput', { inputType: 'insertFromPaste', data: '12,25', bubbles: true, cancelable: true });
			if (input.dispatchEvent(event)) throw new Error('the paste was not taken over');
		});
		await expect(field).toHaveValue('12.25');
		expect((await inputs(page)).at(-1)).toBe('12.25');
	});

	test('a change beforeinput did not foresee is undone', async ({ page }) => {
		const field = await mountNumberField(page, { range: { min: 0, max: 99, decimals: 0 }, text: '12' });
		await field.evaluate(input => {
			/** @type {HTMLInputElement} */ (input).value = '1x2';
			input.dispatchEvent(new Event('input', { bubbles: true }));
		});
		await expect(field).toHaveValue('12');
		expect(await inputs(page)).toEqual([]);
	});

	test('↑ and ↓ step an integer within the range', async ({ page }) => {
		const field = await mountNumberField(page, { range: { min: 1, max: 99, decimals: 0 }, text: '98' });
		await field.press('ArrowUp');
		await expect(field).toHaveValue('99');
		await field.press('ArrowUp');
		await expect(field).toHaveValue('99');
		await field.press('ArrowDown');
		await expect(field).toHaveValue('98');
		expect(await inputs(page)).toEqual(['99', '98']);
	});

	test('a field with decimals does not step', async ({ page }) => {
		const field = await mountNumberField(page, { range: { min: 0, max: 9.99, decimals: 2 }, text: '5' });
		await field.press('ArrowUp');
		await expect(field).toHaveValue('5');
	});

	test('a range typed into digit by digit is marked, not refused', async ({ page }) => {
		const field = await mountNumberField(page, { range: { min: 2341972.5, max: 2560755.5, decimals: 5 }, markOutOfRange: true });
		await field.pressSequentially('24x5');
		await expect(field).toHaveValue('245');
		await expect(field).toHaveAttribute('aria-invalid', 'true');
		expect(await page.evaluate(() => /** @type {any} */ (window).field.value())).toBeNull();
		await field.pressSequentially('1545.5');
		await expect(field).not.toHaveAttribute('aria-invalid');
		expect(await page.evaluate(() => /** @type {any} */ (window).field.value())).toBe(2451545.5);
	});
});

test.describe('the sort menu', () => {
	test.beforeEach(async ({ page }) => {
		await page.goto('./#/help');
		await page.locator('[data-view="help"]').waitFor();
		await page.evaluate(async () => {
			const { createSortMenu } = await import(new URL('js/ui/components/sort-menu.js', location.href).href);
			/** @type {any} */
			const probe = window;
			probe.sort = { property: 'sortkey', ascending: true };
			probe.sorts = [];
			const menu = createSortMenu({
				sort: () => probe.sort,
				onSort: (/** @type {any} */ sort) => { probe.sort = sort; probe.sorts.push(`${sort.property} ${sort.ascending ? 'asc' : 'desc'}`); }
			});
			document.body.prepend(menu.element);
		});
	});

	/** @param {import('@playwright/test').Page} page */
	const sorts = page => page.evaluate(() => /** @type {any} */ (window).sorts);

	test('lists the properties and the directions, the current ones marked', async ({ page }) => {
		const button = page.getByRole('button', { name: 'Sort by' });
		await expect(button).toHaveAttribute('aria-expanded', 'false');
		await button.click();
		const menu = page.getByRole('menu', { name: 'Sort by' });
		await expect(menu).toBeVisible();
		await expect(button).toHaveAttribute('aria-expanded', 'true');
		await expect(menu.getByRole('menuitemradio')).toHaveCount(18);
		await expect(menu.getByRole('menuitemradio', { checked: true })).toHaveText(['Designation', 'Ascending']);
		await expect(menu.getByRole('menuitemradio', { name: 'Designation' })).toBeFocused();
	});

	test('another property keeps the direction; the one chosen does nothing; a direction sorts', async ({ page }) => {
		const button = page.getByRole('button', { name: 'Sort by' });
		const menu = page.getByRole('menu', { name: 'Sort by' });
		await button.click();
		await menu.getByRole('menuitemradio', { name: 'Descending' }).click();
		await expect(menu).toBeHidden();
		await expect(button).toBeFocused();
		await button.click();
		await menu.getByRole('menuitemradio', { name: 'Perihelion distance', exact: true }).click();
		await button.click();
		await expect(menu.getByRole('menuitemradio', { checked: true })).toHaveText(['Perihelion distance', 'Descending']);
		await menu.getByRole('menuitemradio', { name: 'Perihelion distance', exact: true }).click();
		await button.click();
		await menu.getByRole('menuitemradio', { name: 'Descending' }).click();
		expect(await sorts(page)).toEqual(['sortkey desc', 'q desc', 'q desc']);
	});

	test('works from the keyboard, and Esc closes it', async ({ page }) => {
		const button = page.getByRole('button', { name: 'Sort by' });
		const menu = page.getByRole('menu', { name: 'Sort by' });
		await button.focus();
		await page.keyboard.press('Enter');
		await expect(menu.getByRole('menuitemradio', { name: 'Designation' })).toBeFocused();
		await page.keyboard.press('ArrowDown');
		await expect(menu.getByRole('menuitemradio', { name: 'Discoverer' })).toBeFocused();
		await page.keyboard.press('ArrowUp');
		await page.keyboard.press('ArrowUp');
		await expect(menu.getByRole('menuitemradio', { name: 'Descending' })).toBeFocused();
		await page.keyboard.press('Home');
		await expect(menu.getByRole('menuitemradio', { name: 'Designation' })).toBeFocused();
		await page.keyboard.press('End');
		await page.keyboard.press('Enter');
		await expect(menu).toBeHidden();
		expect(await sorts(page)).toEqual(['sortkey desc']);
		await page.keyboard.press('Enter');
		await expect(menu).toBeVisible();
		await page.keyboard.press('Escape');
		await expect(menu).toBeHidden();
		await expect(button).toBeFocused();
	});

	test('stays within the window', async ({ page }) => {
		await page.getByRole('button', { name: 'Sort by' }).click();
		const box = await page.getByRole('menu', { name: 'Sort by' }).boundingBox();
		const viewport = page.viewportSize();
		expect(box && viewport).toBeTruthy();
		if (!box || !viewport) return;
		expect(box.x).toBeGreaterThanOrEqual(0);
		expect(box.y).toBeGreaterThanOrEqual(0);
		expect(box.x + box.width).toBeLessThanOrEqual(viewport.width);
		expect(box.y + box.height).toBeLessThanOrEqual(viewport.height);
	});
});

test.describe('the date picker', () => {
	test.use({ timezoneId: 'Europe/Zagreb' });

	/**
	 * mounts a date picker over the Help view, with a fixed clock, and records its changes
	 * @param {import('@playwright/test').Page} page
	 * @param {{ value: string, defaultDate?: string, perihelionDate?: string }} dates ISO instants
	 */
	async function mountDatePicker(page, dates) {
		await page.clock.setFixedTime(new Date('2026-11-05T06:19:11Z'));
		await page.goto('./#/help');
		await page.locator('[data-view="help"]').waitFor();
		await page.evaluate(async ({ value, defaultDate, perihelionDate }) => {
			const { createDatePicker } = await import(new URL('js/ui/components/date-picker.js', location.href).href);
			/** @type {any} */
			const probe = window;
			probe.changes = [];
			probe.picker = createDatePicker({
				value: Date.parse(value),
				now: () => Date.now(),
				timeZone: () => Intl.DateTimeFormat().resolvedOptions().timeZone,
				defaultDate: defaultDate ? Date.parse(defaultDate) : null,
				perihelionDate: perihelionDate ? Date.parse(perihelionDate) : null,
				label: 'Probe date',
				onChange: (/** @type {number} */ instant) => probe.changes.push(new Date(instant).toISOString())
			});
			document.body.prepend(probe.picker.element);
		}, dates);
		return page.getByRole('group', { name: 'Probe date' });
	}

	/** @param {import('@playwright/test').Page} page */
	const changes = page => page.evaluate(() => /** @type {any} */ (window).changes);

	test('shows the UTC instant, with local time in its tooltip', async ({ page }) => {
		const picker = await mountDatePicker(page, { value: '2026-07-05T06:19:11Z' });
		const button = picker.getByRole('button', { name: '05.07.2026. 06:19:11' });
		await expect(button).toBeVisible();
		await expect(button).toHaveAttribute('title', 'Local Time\n05.07.2026. 08:19:11');
	});

	test('the editor: the fields in UTC, local time, the JD; OK focused', async ({ page }) => {
		const picker = await mountDatePicker(page, { value: '2026-11-05T06:19:11Z' });
		await picker.getByRole('button', { name: '05.11.2026. 06:19:11' }).click();
		const editor = page.getByRole('dialog', { name: 'Universal Time (UTC)' });
		await expect(editor).toBeVisible();
		await expect(editor.getByRole('button', { name: 'OK' })).toBeFocused();
		const values = await Promise.all(['Day', 'Month', 'Year', 'Hour', 'Minute', 'Second', 'Local Time', 'JD']
			.map(name => editor.getByRole('textbox', { name, exact: true }).inputValue()));
		expect(values).toEqual(['5', '11', '2026', '6', '19', '11', '05.11.2026. 07:19:11', '2461349.76332']);
	});

	test('typing sets the date; the day follows the month; OK keeps it', async ({ page }) => {
		const picker = await mountDatePicker(page, { value: '2024-01-31T10:20:30Z' });
		await picker.getByRole('button').first().click();
		const editor = page.getByRole('dialog', { name: 'Universal Time (UTC)' });
		const month = editor.getByRole('textbox', { name: 'Month' });
		await month.fill('');
		await month.pressSequentially('2');
		await expect(editor.getByRole('textbox', { name: 'Day' })).toHaveValue('29');
		await expect(editor.getByRole('textbox', { name: 'JD' })).toHaveValue('2460369.9309');
		const hour = editor.getByRole('textbox', { name: 'Hour' });
		await hour.fill('');
		await expect(editor.getByRole('textbox', { name: 'Local Time' })).toHaveValue('29.02.2024. 11:20:30');
		await hour.pressSequentially('23');
		await expect(editor.getByRole('textbox', { name: 'Local Time' })).toHaveValue('01.03.2024. 00:20:30');
		await hour.press('Enter');
		await expect(editor).toHaveCount(0);
		await expect(picker.getByRole('button').first()).toHaveText('29.02.2024. 23:20:30');
		await expect(picker.getByRole('button').first()).toBeFocused();
		expect(await changes(page)).toEqual(['2024-02-29T23:20:30.000Z']);
	});

	test('↑ and ↓ step every field with calendar arithmetic, the JD by a day', async ({ page }) => {
		const picker = await mountDatePicker(page, { value: '2024-01-31T10:20:30Z' });
		await picker.getByRole('button').first().click();
		const editor = page.getByRole('dialog', { name: 'Universal Time (UTC)' });
		await editor.getByRole('textbox', { name: 'Month' }).press('ArrowUp');
		await expect(editor.getByRole('textbox', { name: 'Day' })).toHaveValue('29');
		await expect(editor.getByRole('textbox', { name: 'Month' })).toHaveValue('2');
		await editor.getByRole('textbox', { name: 'Year' }).press('ArrowUp');
		await expect(editor.getByRole('textbox', { name: 'Day' })).toHaveValue('28');
		await editor.getByRole('textbox', { name: 'Second' }).press('ArrowDown');
		await expect(editor.getByRole('textbox', { name: 'Second' })).toHaveValue('29');
		await editor.getByRole('textbox', { name: 'JD' }).press('ArrowUp');
		await expect(editor.getByRole('textbox', { name: 'Day' })).toHaveValue('1');
		await expect(editor.getByRole('textbox', { name: 'Month' })).toHaveValue('3');
		await editor.getByRole('button', { name: 'OK' }).click();
		expect(await changes(page)).toEqual(['2025-03-01T10:20:29.000Z']);
	});

	test('a JD typed sets the other fields; out of the range it is marked and sets nothing', async ({ page }) => {
		const picker = await mountDatePicker(page, { value: '2026-11-05T06:19:11Z' });
		await picker.getByRole('button').first().click();
		const editor = page.getByRole('dialog', { name: 'Universal Time (UTC)' });
		const jd = editor.getByRole('textbox', { name: 'JD' });
		await jd.fill('');
		await jd.pressSequentially('2451545');
		await expect(jd).not.toHaveAttribute('aria-invalid');
		await expect(editor.getByRole('textbox', { name: 'Year' })).toHaveValue('2000');
		await expect(editor.getByRole('textbox', { name: 'Hour' })).toHaveValue('12');
		await jd.pressSequentially('0');
		await expect(jd).toHaveAttribute('aria-invalid', 'true');
		await expect(editor.getByRole('textbox', { name: 'Year' })).toHaveValue('2000');
		await editor.getByRole('button', { name: 'OK' }).click();
		expect(await changes(page)).toEqual(['2000-01-01T12:00:00.000Z']);
	});

	test('OK clamps to the date range; Cancel and Esc change nothing', async ({ page }) => {
		const picker = await mountDatePicker(page, { value: '2026-11-05T06:19:11Z' });
		const button = picker.getByRole('button').first();
		const editor = page.getByRole('dialog', { name: 'Universal Time (UTC)' });
		await button.click();
		await editor.getByRole('textbox', { name: 'Year' }).fill('2400');
		await editor.getByRole('button', { name: 'OK' }).click();
		await expect(button).toHaveText('01.01.2300. 00:00:00');
		await button.click();
		await editor.getByRole('textbox', { name: 'Year' }).fill('1650');
		await editor.getByRole('button', { name: 'Cancel' }).click();
		await expect(editor).toHaveCount(0);
		await button.click();
		await editor.getByRole('textbox', { name: 'Year' }).fill('1650');
		await page.keyboard.press('Escape');
		await expect(editor).toHaveCount(0);
		await expect(button).toHaveText('01.01.2300. 00:00:00');
		await button.click();
		await editor.getByRole('textbox', { name: 'Year' }).fill('1650');
		await editor.getByRole('button', { name: 'OK' }).click();
		await expect(button).toHaveText('01.01.1700. 00:00:00');
		expect(await changes(page)).toEqual(['2300-01-01T00:00:00.000Z', '1700-01-01T00:00:00.000Z']);
	});

	test('the presets: Today, the years, steps on the value; Default and Perihelion Date when set', async ({ page }) => {
		const picker = await mountDatePicker(page, { value: '2026-01-31T01:02:03Z' });
		const presets = picker.getByRole('button', { name: 'Date presets' });
		const menu = page.getByRole('menu', { name: 'Date presets' });
		await presets.click();
		await expect(menu.getByRole('menuitem')).toHaveText([
			'Today', '01 Jan 2025 00:00:00', '01 Jan 2026 00:00:00', '01 Jan 2027 00:00:00', '01 Jan 2028 00:00:00',
			'Add 3 months', 'Add 6 months', 'Add 1 year', 'Subtract 3 months', 'Subtract 6 months', 'Subtract 1 year'
		]);
		await menu.getByRole('menuitem', { name: 'Add 3 months' }).click();
		await expect(picker.getByRole('button').first()).toHaveText('30.04.2026. 01:02:03');
		await presets.click();
		await menu.getByRole('menuitem', { name: 'Today' }).click();
		await expect(picker.getByRole('button').first()).toHaveText('05.11.2026. 00:00:00');
		expect(await changes(page)).toEqual(['2026-04-30T01:02:03.000Z', '2026-11-05T00:00:00.000Z']);
	});

	test('Default and Perihelion Date show when set, here and in the editor', async ({ page }) => {
		const picker = await mountDatePicker(page, { value: '2026-01-31T01:02:03Z', defaultDate: '2026-11-01T00:00:00Z', perihelionDate: '2024-09-27T17:47:00Z' });
		await picker.getByRole('button', { name: 'Date presets' }).click();
		const menu = page.getByRole('menu', { name: 'Date presets' });
		await expect(menu.getByRole('menuitem').first()).toHaveText('Default');
		await menu.getByRole('menuitem', { name: 'Perihelion Date' }).click();
		await expect(picker.getByRole('button').first()).toHaveText('27.09.2024. 17:47:00');
		await picker.getByRole('button').first().click();
		const editor = page.getByRole('dialog', { name: 'Universal Time (UTC)' });
		await editor.getByRole('button', { name: 'Date presets' }).click();
		await page.getByRole('menu', { name: 'Date presets' }).getByRole('menuitem', { name: 'Default' }).click();
		await expect(editor).toBeVisible();
		await expect(editor.getByRole('textbox', { name: 'Month' })).toHaveValue('11');
		await editor.getByRole('button', { name: 'OK' }).click();
		expect(await changes(page)).toEqual(['2024-09-27T17:47:00.000Z', '2026-11-01T00:00:00.000Z']);
	});
});
