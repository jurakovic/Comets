// Light, dark or the OS's choice, applied at once and whenever the setting changes
// (docs/spec/09-settings-and-persistence.md, Theme).

/** @typedef {import('../../services/settings.js').Theme} Theme */

/** @param {Theme} theme */
export function applyTheme(theme) {
	const root = document.documentElement;
	if (theme === 'system') delete root.dataset.theme;
	else root.dataset.theme = theme;
}

/**
 * applies the theme of the settings now and after every change
 * @param {import('../../lib/store.js').ReadableStore<Readonly<import('../../services/settings.js').Settings>>} settings
 * @returns {() => void} stops following
 */
export function followTheme(settings) {
	applyTheme(settings.get().theme);
	return settings.subscribe(({ theme }) => applyTheme(theme));
}
