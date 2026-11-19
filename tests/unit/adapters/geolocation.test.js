import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createGeolocator } from '../../../app/js/adapters/geolocation.js';

/**
 * a Geolocation that answers each request as given
 * @param {(success: PositionCallback, failure: PositionErrorCallback) => void} answer
 * @returns {Geolocation}
 */
function fakeGeolocation(answer) {
	return /** @type {Geolocation} */ (/** @type {unknown} */ ({ getCurrentPosition: answer }));
}

test('the position in degrees', async () => {
	const geolocation = fakeGeolocation(success => success(/** @type {GeolocationPosition} */ (/** @type {unknown} */ ({ coords: { latitude: 45.815, longitude: -15.9819 } }))));
	assert.deepEqual(await createGeolocator(() => geolocation).locate(), { latitude: 45.815, longitude: -15.9819 });
});

test('a refusal rejects with its message', async () => {
	const geolocation = fakeGeolocation((_, failure) => failure(/** @type {GeolocationPositionError} */ ({ code: 1, message: 'User denied Geolocation' })));
	await assert.rejects(createGeolocator(() => geolocation).locate(), /User denied Geolocation/);
	const silent = fakeGeolocation((_, failure) => failure(/** @type {GeolocationPositionError} */ ({ code: 3, message: '' })));
	await assert.rejects(createGeolocator(() => silent).locate(), /geolocation error 3/);
});

test('without the API it rejects', async () => {
	await assert.rejects(createGeolocator(() => undefined).locate(), /not available/);
});
