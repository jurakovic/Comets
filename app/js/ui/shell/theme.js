// Light, dark or the OS's choice, applied whenever the setting changes; index.html applies the
// stored one before the first paint. What paints the theme's colours itself watches the theme
// shown. INTERNALS.md, The shell.

/** @typedef {import('../../services/settings.js').Theme} Theme */

/** @param {Theme} theme */
export function applyTheme(theme) {
	const root = document.documentElement;
	if (theme === 'system') delete root.dataset.theme;
	else root.dataset.theme = theme;
}

/**
 * applies the theme of the settings after every change, from their load on; until then the theme
 * is the one index.html read from storage, where the settings' defaults would show the OS's
 * @param {import('../../lib/store.js').ReadableStore<Readonly<import('../../services/settings.js').Settings>>} settings
 * @returns {() => void} stops following
 */
export function followTheme(settings) {
	return settings.subscribe(({ theme }) => applyTheme(theme));
}

/**
 * calls listener whenever the theme shown may have changed: the setting applied, or the OS's
 * choice changed, which shows while the setting follows the OS
 * @param {() => void} listener
 * @returns {() => void} stops watching
 */
export function watchTheme(listener) {
	const media = matchMedia('(prefers-color-scheme: dark)');
	const observer = new MutationObserver(() => listener());
	observer.observe(document.documentElement, { attributes: true, attributeFilter: ['data-theme'] });
	media.addEventListener('change', listener);
	return () => {
		observer.disconnect();
		media.removeEventListener('change', listener);
	};
}
