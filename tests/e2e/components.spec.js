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
