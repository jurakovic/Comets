// Small DOM helpers the UI builds with.

/**
 * an element of tag with attributes set (true sets an empty attribute; false
 * and undefined leave it out) and children appended
 * @template {keyof HTMLElementTagNameMap} K
 * @param {K} tag
 * @param {Record<string, string | number | boolean | undefined>} [attributes]
 * @param {...(Node | string)} children
 * @returns {HTMLElementTagNameMap[K]}
 */
export function el(tag, attributes = {}, ...children) {
	const element = document.createElement(tag);
	for (const [name, value] of Object.entries(attributes)) {
		if (value === undefined || value === false) continue;
		element.setAttribute(name, value === true ? '' : String(value));
	}
	element.append(...children);
	return element;
}

/**
 * the element selector finds under root; the page is broken without it
 * @param {string} selector
 * @param {ParentNode} [root]
 * @returns {HTMLElement}
 */
export function query(selector, root = document) {
	const found = root.querySelector(selector);
	if (!(found instanceof HTMLElement)) throw new Error(`missing element ${selector}`);
	return found;
}

/**
 * offers data to the user as a file to save, under the name given
 * @param {string} name
 * @param {Blob} data
 */
export function saveFile(name, data) {
	const url = URL.createObjectURL(data);
	const link = el('a', { href: url, download: name, hidden: true });
	document.body.append(link);
	link.click();
	link.remove();
	// the download has its own copy once it starts
	setTimeout(() => URL.revokeObjectURL(url), 60_000);
}
