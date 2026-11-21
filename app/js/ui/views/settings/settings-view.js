// The Settings view (`FormSettings`): every setting saved as it changes, without OK or Cancel; the
// location with its hemispheres and Use my location; Export database and Clear all data.
// INTERNALS.md, Views.
import { stampedFileName } from '../../../core/format/dates.js';
import { trim } from '../../../core/format/parse.js';
import { coordinateFields, coordinateValue, LATITUDE, LONGITUDE } from '../../../core/input/location.js';
import { numberOf } from '../../../core/input/number.js';
import { localPartsIn } from '../../../core/time/local.js';
import { DEFAULT_DOWNLOAD_URL, THEMES, UPDATE_INTERVAL } from '../../../services/settings.js';
import { createNumberField } from '../../components/number-field.js';
import { el, saveFile } from '../../dom.js';

/** @typedef {import('../../../services/settings.js').Settings} Settings */
/** @typedef {import('../../../services/settings.js').SettingsPatch} SettingsPatch */

/** the theme menu's labels (`SettingsManager.Themes`) */
const THEME_LABELS = Object.freeze({ light: 'Light', dark: 'Dark', system: 'System' });

/** what Clear all data asks first */
export const CLEAR_QUESTION = 'Do you really want to delete the database and reset all settings?';

/** what Use my location says when the browser gives no position */
export const NO_LOCATION = 'Your location could not be found.';

/** Export database's file, before its time stamp */
const EXPORT_PREFIX = 'Comets';

let ids = 0;

/**
 * a labelled row of the form
 * @param {string} label
 * @param {HTMLElement} control the labelled control
 * @param {...(Node | string)} rest after the control
 */
function row(label, control, ...rest) {
	control.id ||= `settings-${++ids}`;
	return el('div', { class: 'settings-view__row' },
		el('label', { class: 'settings-view__label', for: control.id }, label),
		el('div', { class: 'settings-view__control' }, control, ...rest));
}

/**
 * a checkbox with its label after it
 * @param {string} label
 * @returns {{ element: HTMLElement, input: HTMLInputElement }}
 */
function checkbox(label) {
	const input = el('input', { type: 'checkbox', class: 'settings-view__checkbox' });
	return { input, element: el('label', { class: 'settings-view__check' }, input, label) };
}

/**
 * @param {import('../../shell/context.js').AppContext} context
 * @returns {import('../../shell/router.js').View}
 */
export function createSettingsView({ settings, database, dialogs, notify, clock, geolocator, lastUpdate }) {
	const theme = el('select', { class: 'settings-view__select' },
		...THEMES.map(key => el('option', { value: key }, THEME_LABELS[key])));
	const downloadUrl = el('input', { type: 'url', class: 'text-field settings-view__url', autocomplete: 'off', spellcheck: 'false' });
	const restoreUrl = el('button', { type: 'button', class: 'button' }, 'Restore default');
	const automatic = checkbox('Automatically update elements after:');
	const interval = createNumberField({ range: UPDATE_INTERVAL, label: 'Update interval in days', className: 'settings-view__interval', onInput: saveUpdate });
	const intervalError = el('p', { id: 'settings-interval-error', class: 'settings-view__error', role: 'alert', hidden: true });
	const lastUpdateOutput = el('output', { class: 'settings-view__last-update' });
	const longCalculation = checkbox('Show long calculation confirmation');
	const deleteComet = checkbox('Show delete comet confirmation');
	const name = el('input', { type: 'text', class: 'text-field settings-view__name', autocomplete: 'off' });
	const coordinates = [LATITUDE, LONGITUDE].map(coordinate => {
		const field = createNumberField({ range: coordinate.range, className: 'settings-view__coordinate', onInput: saveLocation });
		const hemisphere = el('select', { class: 'settings-view__select', 'aria-label': `${coordinate.label} hemisphere` },
			...coordinate.hemispheres.map((label, index) => el('option', { value: String(index) }, label)));
		hemisphere.addEventListener('change', saveLocation);
		return { coordinate, field, hemisphere };
	});
	const [latitude, longitude] = coordinates;
	const locate = el('button', { type: 'button', class: 'button' }, 'Use my location');
	const exportButton = el('button', { type: 'button', class: 'button' }, 'Export database');
	const clearButton = el('button', { type: 'button', class: 'button button--danger' }, 'Clear all data');

	const element = el('section', { class: 'settings-view', 'data-view': 'settings', 'aria-labelledby': 'view-title-settings' },
		el('h1', { id: 'view-title-settings' }, 'Settings'),
		el('h2', {}, 'General'),
		row('Theme', theme),
		row('Elements download URL', downloadUrl, restoreUrl),
		el('div', { class: 'settings-view__row' },
			el('div', { class: 'settings-view__label' }),
			el('div', { class: 'settings-view__control' }, automatic.element, el('span', { class: 'settings-view__days' }, interval.element, 'days'), intervalError)),
		el('div', { class: 'settings-view__row' },
			el('span', { class: 'settings-view__label' }, 'Last update'),
			el('div', { class: 'settings-view__control' }, lastUpdateOutput)),
		el('div', { class: 'settings-view__row' }, el('div', { class: 'settings-view__label' }), el('div', { class: 'settings-view__control' }, longCalculation.element)),
		el('div', { class: 'settings-view__row' }, el('div', { class: 'settings-view__label' }), el('div', { class: 'settings-view__control' }, deleteComet.element)),
		el('h2', {}, 'Location'),
		row('Name', name),
		...coordinates.map(({ coordinate, field, hemisphere }) => row(coordinate.label, field.element, el('span', { 'aria-hidden': 'true' }, '°'), hemisphere)),
		el('div', { class: 'settings-view__row' }, el('div', { class: 'settings-view__label' }), el('div', { class: 'settings-view__control' }, locate)),
		el('h2', {}, 'Data management'),
		el('div', { class: 'settings-view__actions' }, exportButton, clearButton));

	/** @type {Readonly<Settings> | null} the settings the fields last showed */
	let shown = null;
	/** whether the view is saving a change of its own, which its fields already show */
	let saving = false;

	/**
	 * saves a change the fields made
	 * @param {SettingsPatch} patch
	 */
	function save(patch) {
		saving = true;
		try {
			return settings.update(patch);
		} finally {
			saving = false;
		}
	}

	// the checkbox and the interval together: the interval is required while the checkbox is on
	function saveUpdate() {
		const days = numberOf(interval.text());
		save({ automaticUpdate: automatic.input.checked, updateInterval: days === null ? null : Math.trunc(days) }).then(result => {
			const error = result.ok ? null : result.errors.find(e => e.field === 'updateInterval')?.message ?? null;
			intervalError.textContent = error ?? '';
			intervalError.hidden = error === null;
			if (error === null) {
				interval.element.removeAttribute('aria-invalid');
				interval.element.removeAttribute('aria-describedby');
			} else {
				interval.element.setAttribute('aria-invalid', 'true');
				interval.element.setAttribute('aria-describedby', intervalError.id);
			}
		});
	}

	function saveLocation() {
		save({
			location: {
				name: trim(name.value),
				latitude: coordinateValue(latitude.field.text(), latitude.hemisphere.value === '1'),
				longitude: coordinateValue(longitude.field.text(), longitude.hemisphere.value === '1')
			}
		});
	}

	/**
	 * a coordinate's fields from its value
	 * @param {typeof coordinates[number]} fields
	 * @param {number} value
	 */
	function showCoordinate({ field, hemisphere }, value) {
		const { text, negative } = coordinateFields(value);
		field.setText(text);
		hemisphere.value = negative ? '1' : '0';
	}

	/** every field from the settings */
	function render() {
		const current = settings.settings.get();
		shown = current;
		theme.value = current.theme;
		downloadUrl.value = current.downloadUrl;
		automatic.input.checked = current.automaticUpdate;
		interval.setText(current.updateInterval === null ? '' : String(current.updateInterval));
		intervalError.hidden = true;
		interval.element.removeAttribute('aria-invalid');
		interval.element.removeAttribute('aria-describedby');
		longCalculation.input.checked = current.showLongCalculationConfirmation;
		deleteComet.input.checked = current.showDeleteCometConfirmation;
		name.value = current.location.name;
		showCoordinate(latitude, current.location.latitude);
		showCoordinate(longitude, current.location.longitude);
	}

	/** Last update: the shell's count, which the navigation shows too and which moves on as days pass */
	const showLastUpdate = (/** @type {string} */ text) => { lastUpdateOutput.textContent = text; };

	/** the settings changed: by the view itself, which shows them already, or elsewhere */
	function follow() {
		if (!saving) render();
		else shown = settings.settings.get();
	}

	// nothing to export until the stored database has been read, or while it is empty
	const showDatabase = () => {
		const { loaded, comets } = database.state.get();
		exportButton.disabled = !loaded || comets.length === 0;
	};

	theme.addEventListener('change', () => { save({ theme: /** @type {import('../../../services/settings.js').Theme} */ (theme.value) }); });
	downloadUrl.addEventListener('input', () => { save({ downloadUrl: trim(downloadUrl.value) }); });
	restoreUrl.addEventListener('click', () => {
		downloadUrl.value = DEFAULT_DOWNLOAD_URL;
		save({ downloadUrl: DEFAULT_DOWNLOAD_URL });
	});
	automatic.input.addEventListener('change', saveUpdate);
	longCalculation.input.addEventListener('change', () => { save({ showLongCalculationConfirmation: longCalculation.input.checked }); });
	deleteComet.input.addEventListener('change', () => { save({ showDeleteCometConfirmation: deleteComet.input.checked }); });
	name.addEventListener('input', saveLocation);
	locate.addEventListener('click', async () => {
		locate.disabled = true;
		try {
			const position = await geolocator.locate();
			showCoordinate(latitude, position.latitude);
			showCoordinate(longitude, position.longitude);
			saveLocation();
		} catch {
			notify(NO_LOCATION, { kind: 'error' });
		} finally {
			locate.disabled = false;
		}
	});
	exportButton.addEventListener('click', async () => {
		const text = await database.storedText();
		saveFile(stampedFileName(EXPORT_PREFIX, localPartsIn(clock.timeZone())(clock.now()), 'txt'), new Blob([text], { type: 'text/plain' }));
	});
	clearButton.addEventListener('click', async () => {
		if (!await dialogs.confirm({ message: CLEAR_QUESTION })) return;
		await Promise.all([database.clear(), settings.reset()]);
	});

	/** @type {Array<() => void>} */
	let unsubscribes = [];
	return {
		element,
		show() {
			if (shown !== settings.settings.get()) render();
			showLastUpdate(lastUpdate.get());
			showDatabase();
			unsubscribes = [settings.settings.subscribe(follow), lastUpdate.subscribe(showLastUpdate), database.state.subscribe(showDatabase)];
		},
		hide() {
			for (const unsubscribe of unsubscribes) unsubscribe();
			unsubscribes = [];
		}
	};
}
