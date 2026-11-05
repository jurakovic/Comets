// How many comets the selection and the database hold, as the status area and the Database view
// say it. docs/spec/01-product.md, Shell; docs/spec/05-database-and-update.md, Layout.

/**
 * the status area's count (`FormMain.SetStatusCometsLabel`): `Comets: <selection>`, with the
 * database's size after it when filters hide some
 * @param {number} count the selection
 * @param {number} total the database
 * @returns {string}
 */
export function statusCount(count, total) {
	return count < total ? `Comets: ${count} (${total})` : `Comets: ${count}`;
}

/**
 * the Database view's title: `Database (<n> comets)`, or `Database (<n>/<total> comets)`
 * @param {number} count the selection
 * @param {number} total the database
 * @returns {string}
 */
export function databaseTitle(count, total) {
	return count < total ? `Database (${count}/${total} comets)` : `Database (${count} comets)`;
}
