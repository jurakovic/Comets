// The composition root: the one module that knows every layer. It builds the
// adapters, services and UI and wires them (docs/spec/12-architecture.md).
import { query } from './ui/dom.js';
import { createNav } from './ui/shell/nav.js';
import { createRouter } from './ui/shell/router.js';
import { DEFAULT_ROUTE, ROUTES } from './ui/shell/routes.js';
import { applyTheme } from './ui/shell/theme.js';

applyTheme('system');
const router = createRouter({ routes: ROUTES, fallback: DEFAULT_ROUTE, outlet: query('#view') });
query('#nav').append(createNav(ROUTES, router.current).element);
router.start();
