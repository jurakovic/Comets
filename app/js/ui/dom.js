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
