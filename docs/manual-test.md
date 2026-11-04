# Manual test checklist

What automation cannot see: real browsers and devices, touch, how the app feels and performs. Run each milestone's section on current Chrome or Edge, Firefox and Safari on a desktop, and on a phone (Safari on iOS, Chrome on Android) where the section says so. When something fails, note the step, the browser and its version, and any message in the browser's console.

## Getting the app

- **From a checkout**: `npm ci` once, then `npm run serve` and open <http://127.0.0.1:8085/Comets/app/>. For a phone on the same network, `HOST=0.0.0.0 npm run serve` and open `http://<computer's address>:8085/Comets/app/`.
- **From GitHub Pages**, once `dev` is merged into `master`: <https://jurakovic.github.io/Comets/app/>.

To start over as a first-time visitor, clear the site's data in the browser (storage for the origin), or use a private window.

## M0 — Harness

The shell only: every view is a placeholder.

- [ ] The app opens on the Database view, and the address ends in `#/database`.
- [ ] Each link in the navigation bar opens its view and marks itself as current; the browser's Back and Forward move between the views visited.
- [ ] Reloading the page reopens the same view.
- [ ] Switching the OS between light and dark mode switches the app at once.
- [ ] On a phone, the navigation bar scrolls sideways and the page itself does not.
