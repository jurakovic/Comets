// What the composition root hands each view when it is created: the services and the shell's
// own parts. docs/spec/12-architecture.md, UI.

/**
 * @typedef {object} AppContext
 * @property {import('../../services/ports.js').Clock} clock
 * @property {import('../../services/settings.js').SettingsService} settings
 * @property {import('../../services/database.js').DatabaseService} database
 * @property {import('../../services/selection.js').SelectionService} selection
 * @property {import('../../services/observations.js').ObservationsService} observations
 * @property {import('../../services/jobs.js').JobsService} jobs
 * @property {import('./shortcuts.js').CommandRegistry} commands
 * @property {import('./dialogs.js').Dialogs} dialogs
 * @property {import('./notifications.js').Notify} notify
 */

export {};
