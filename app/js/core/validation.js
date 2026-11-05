// Validation results: what a check of the user's input returns instead of throwing.
// docs/spec/12-architecture.md, Core; docs/spec/01-product.md, Messages.

/**
 * one error, with the input it belongs to so the UI can point at it
 * @typedef {object} ValidationError
 * @property {string} field the input, by the name its owner gives it
 * @property {number} [index] the row, where the inputs are a list (filter rows)
 * @property {string} message the spec's text
 */

/**
 * @template T
 * @typedef {{ ok: true, value: T } | { ok: false, errors: ReadonlyArray<ValidationError> }} Validation
 */

/**
 * @template T
 * @param {T} value
 * @returns {Validation<T>}
 */
export function valid(value) {
	return Object.freeze({ ok: /** @type {const} */ (true), value });
}

/**
 * @template T
 * @param {ReadonlyArray<ValidationError>} errors at least one
 * @returns {Validation<T>}
 */
export function invalid(errors) {
	return Object.freeze({ ok: /** @type {const} */ (false), errors: Object.freeze(errors.map(e => Object.freeze({ ...e }))) });
}
