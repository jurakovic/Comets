// The date presets menu, beside the date picker's button and in its editor: the presets of
// core/input/date.js for the date shown. docs/spec/09-settings-and-persistence.md, Date picker.
import { datePresets } from '../../core/input/date.js';
import { el } from '../dom.js';
import { createMenu } from './menu.js';

/**
 * @param {() => import('../../core/input/date.js').PresetContext} context the date and the dates
 *   the presets offer, read as the menu opens
 * @param {(instant: number) => void} onPick
 * @returns {import('./menu.js').Menu}
 */
export function createPresetsMenu(context, onPick) {
	return createMenu({
		label: el('span', { class: 'date-picker__presets-icon' }),
		name: 'Date presets',
		className: 'date-picker__presets',
		entries: () => datePresets(context()).map(preset => 'separator' in preset
			? preset
			: { label: preset.label, run: () => onPick(preset.value) })
	});
}
