// Static server for the browser tests and for trying the app: the repository
// under /Comets/, as GitHub Pages serves it, so the app is at /Comets/app/ and
// the vectors at /Comets/docs/spec/vectors/.
//
//   npm run serve            (PORT, default 8085)
import { createServer } from 'node:http';
import { readFile, stat } from 'node:fs/promises';
import { extname, join, normalize, resolve, sep } from 'node:path';
import { fileURLToPath } from 'node:url';

const root = resolve(fileURLToPath(new URL('..', import.meta.url)));
const base = '/Comets/';
const port = Number(process.env.PORT) || 8085;
const types = {
	'.html': 'text/html; charset=utf-8',
	'.js': 'text/javascript; charset=utf-8',
	'.css': 'text/css; charset=utf-8',
	'.json': 'application/json; charset=utf-8',
	'.txt': 'text/plain; charset=utf-8',
	'.svg': 'image/svg+xml',
	'.png': 'image/png',
	'.ico': 'image/x-icon',
	'.webmanifest': 'application/manifest+json'
};

/** @param {string} path the URL path */
async function resolveFile(path) {
	if (!path.startsWith(base)) return null;
	const file = join(root, normalize(decodeURIComponent(path.slice(base.length))));
	if (file !== root && !file.startsWith(root + sep)) return null;
	try {
		const info = await stat(file);
		if (!info.isDirectory()) return file;
		const index = join(file, 'index.html');
		await stat(index);
		return index;
	} catch {
		return null;
	}
}

createServer(async (request, response) => {
	const { pathname } = new URL(request.url ?? '/', 'http://localhost');
	// as Pages does: a folder without its slash, and the site root, redirect
	if (pathname === '/' || pathname === '/Comets' || pathname === '/Comets/app') {
		response.writeHead(301, { Location: '/Comets/app/' }).end();
		return;
	}
	const file = await resolveFile(pathname);
	if (!file) {
		response.writeHead(404, { 'Content-Type': 'text/plain' }).end('not found');
		return;
	}
	response.writeHead(200, {
		'Content-Type': types[/** @type {keyof typeof types} */ (extname(file))] ?? 'application/octet-stream',
		'Cache-Control': 'no-store'
	});
	response.end(await readFile(file));
}).listen(port, '127.0.0.1', () => console.log(`serving http://127.0.0.1:${port}/Comets/app/`));
