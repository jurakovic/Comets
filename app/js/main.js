// The composition root: the one module that knows every layer. It builds the
// adapters, services and UI and wires them (docs/spec/12-architecture.md).
import { createSystemClock } from './adapters/clock.js';
import { createWorkerJobRunner } from './adapters/job-runners.js';
import { createIndexedDbStore, createWebStorageStore } from './adapters/key-value-stores.js';
import { createFetchTextFetcher } from './adapters/text-fetcher.js';
import { createBlobTextReader } from './adapters/text-files.js';
import { createStore } from './lib/store.js';
import { daysAgo } from './core/format/relative.js';
import { createDatabaseRepository } from './services/database-repository.js';
import { createDatabaseService } from './services/database.js';
import { createJobsService } from './services/jobs.js';
import { createObservationsService } from './services/observations.js';
import { createSelectionService } from './services/selection.js';
import { createSettingsService } from './services/settings.js';
import { createStartupService } from './services/startup.js';
import { createUpdateService } from './services/update.js';
import { query } from './ui/dom.js';
import { createDialogs } from './ui/shell/dialogs.js';
import { installErrorHandler } from './ui/shell/errors.js';
import { createNav } from './ui/shell/nav.js';
import { createNotifications } from './ui/shell/notifications.js';
import { createRouter } from './ui/shell/router.js';
import { DEFAULT_ROUTE, ROUTES } from './ui/shell/routes.js';
import { createCommandRegistry, listenForShortcuts } from './ui/shell/shortcuts.js';
import { createStatus } from './ui/shell/status.js';
import { followTheme } from './ui/shell/theme.js';

const notify = createNotifications(query('#notices'));
installErrorHandler(notify);

// adapters
const clock = createSystemClock();
const settingsStore = createWebStorageStore(() => localStorage);
const databaseStore = createIndexedDbStore({ factory: () => indexedDB, database: 'comets', store: 'kv' });
/** @type {import('./services/ports.js').JobRunner | null} the Worker starts with the first job */
let worker = null;
const runner = {
	/** @type {import('./services/ports.js').JobRunner['run']} */
	run: (name, input, options) => (worker ??= createWorkerJobRunner(new URL('./workers/compute.js', import.meta.url))).run(name, input, options)
};

// services
const settings = createSettingsService({ store: settingsStore });
const database = createDatabaseService({ repository: createDatabaseRepository({ store: databaseStore, clock }), clock, settings });
const observations = createObservationsService({ clock, observer: () => settings.settings.get().location });
const selection = createSelectionService({ database: database.state, value: observations.value, clock });
const jobs = createJobsService({ runner });
const startup = createStartupService({ settings, database, clock });
const update = createUpdateService({ fetcher: createFetchTextFetcher(), reader: createBlobTextReader(), database, settings });

// the shell
const commands = createCommandRegistry();
const dialogs = createDialogs(document.body);
const router = createRouter({
	routes: ROUTES,
	fallback: DEFAULT_ROUTE,
	outlet: query('#view'),
	context: { clock, settings, database, selection, observations, jobs, update, commands, dialogs, notify, leaveOverlay: () => router.leaveOverlay() }
});
commands.register({
	id: 'save',
	label: 'Save the current result: ephemeris text, graph image, orbit viewer image, or element file',
	keys: [{ key: 's', mod: true }],
	inTextFields: true,
	enabled: () => Boolean(router.shown()?.save),
	run: () => router.shown()?.save?.()
});
commands.register({
	id: 'close-dialog',
	label: 'Close the open dialog',
	keys: [{ key: 'Escape' }],
	inTextFields: true,
	enabled: dialogs.isOpen,
	run: dialogs.closeTop
});
listenForShortcuts(commands);
followTheme(settings.settings);

// how long ago the last update was, under Update; days pass while the page stays open
const lastUpdate = createStore(daysAgo(settings.settings.get().lastUpdateDate, clock.now()));
const showLastUpdate = () => lastUpdate.set(daysAgo(settings.settings.get().lastUpdateDate, clock.now()));
settings.settings.subscribe(showLastUpdate);
document.addEventListener('visibilitychange', showLastUpdate);
setInterval(showLastUpdate, 60 * 60_000);

query('#nav').append(createNav(ROUTES, router.current, { update: lastUpdate }).element);
query('#status').append(createStatus({ selection: selection.state, jobs }).element);
router.start();

const { prompt } = await startup.start();
if (prompt && await dialogs.confirm({ message: prompt.message })) location.hash = '#/update?automatic';
