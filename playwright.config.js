// Browser tests over the app as GitHub Pages serves it (tests/serve.js puts the
// repository under /Comets/). Every request leaving the local server is
// answered by tests/e2e/fixtures.js, so the suite runs offline.
import { defineConfig, devices } from '@playwright/test';

const port = 8085;
const SCREENSHOTS = /screenshots\.spec\.js/;

export default defineConfig({
	testDir: 'tests/e2e',
	fullyParallel: true,
	forbidOnly: Boolean(process.env.CI),
	retries: 0,
	reporter: process.env.CI ? [['list'], ['html', { open: 'never' }]] : [['list']],
	use: {
		baseURL: `http://127.0.0.1:${port}/Comets/app/`,
		timezoneId: 'UTC',
		locale: 'en-GB',
		trace: 'retain-on-failure'
	},
	projects: [
		{ name: 'desktop', testIgnore: SCREENSHOTS, use: { ...devices['Desktop Chrome'] } },
		{ name: 'phone', testIgnore: SCREENSHOTS, use: { ...devices['Pixel 7'] } },
		// every view in both themes, for looking at: npm run screenshots
		{ name: 'screenshots', testMatch: SCREENSHOTS, use: { ...devices['Desktop Chrome'] } }
	],
	webServer: {
		command: 'node tests/serve.js',
		env: { PORT: String(port) },
		url: `http://127.0.0.1:${port}/Comets/app/`,
		reuseExistingServer: !process.env.CI
	}
});
