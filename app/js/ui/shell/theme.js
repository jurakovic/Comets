// Light, dark or the OS's choice, applied at once (docs/spec/09-settings-and-persistence.md, Theme).

/** @typedef {'light' | 'dark' | 'system'} Theme */

/** @param {Theme} theme */
export function applyTheme(theme) {
	const root = document.documentElement;
	if (theme === 'system') delete root.dataset.theme;
	else root.dataset.theme = theme;
}
