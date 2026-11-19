// The Geolocator port over the Geolocation API: one position, which the browser asks the user to
// allow.

/** how long a position may take, in ms */
const TIMEOUT = 30_000;

/**
 * @param {() => Geolocation | undefined} [geolocation] read at each request: the API is missing
 *   outside secure contexts
 * @returns {import('../services/ports.js').Geolocator}
 */
export function createGeolocator(geolocation = () => navigator.geolocation) {
	return Object.freeze({
		locate: () => new Promise((resolve, reject) => {
			const api = geolocation();
			if (!api) {
				reject(new Error('geolocation is not available'));
				return;
			}
			api.getCurrentPosition(
				({ coords }) => resolve({ latitude: coords.latitude, longitude: coords.longitude }),
				error => reject(new Error(error.message || `geolocation error ${error.code}`)),
				{ timeout: TIMEOUT, maximumAge: 60_000 }
			);
		})
	});
}
