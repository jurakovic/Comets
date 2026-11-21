// The composition root: the one module that knows every layer. It builds the
// adapters, services and UI and wires them (INTERNALS.md, The shell).
import { createSystemClock } from './adapters/clock.js';
import { createGeolocator } from './adapters/geolocation.js';
import { createFallbackJobRunner, createLocalJobRunner, createWorkerJobRunner } from './adapters/job-runners.js';
import { createIndexedDbStore, createWebStorageStore } from './adapters/key-value-stores.js';
import { createFetchTextFetcher } from './adapters/text-fetcher.js';
import { createBlobTextReader } from './adapters/text-files.js';
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
import { createLastUpdate } from './ui/shell/last-update.js';
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
// the Worker starts with the first job; the jobs' code loads on this thread only if it fails
const runner = createFallbackJobRunner({
	worker: () => createWorkerJobRunner(new URL('./workers/compute.js', import.meta.url)),
	local: async () => createLocalJobRunner((await import('./core/jobs.js')).JOBS)
});

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
const lastUpdate = createLastUpdate({ settings, clock });
const router = createRouter({
	routes: ROUTES,
	fallback: DEFAULT_ROUTE,
	outlet: query('#view'),
	context: { clock, geolocator: createGeolocator(), settings, database, selection, observations, jobs, update, commands, dialogs, notify, lastUpdate, leaveOverlay: () => router.leaveOverlay() }
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

query('#nav').append(createNav(ROUTES, router.current, { update: lastUpdate }).element);
query('#status').append(createStatus({ selection: selection.state, jobs }).element);
// a view that fails to load is reported at once, and the start goes on
router.start().catch(reportError);

const { prompt, failures } = await startup.start();
// a load that failed is reported, and the question is asked all the same
for (const failure of failures) reportError(failure);
// the question waits for the navigations under way, the start's among them: a dialog route's
// <dialog> opened after it would cover it in the top layer
await router.idle();
if (prompt && await dialogs.confirm({ message: prompt.message })) location.hash = '#/update?automatic';
