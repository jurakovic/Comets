// The ports: what services need from the outside world, as shapes the adapters
// implement and tests fake. See docs/spec/12-architecture.md, Services and ports.

/**
 * @typedef {object} Clock
 * @property {() => number} now the current instant, UTC milliseconds since the epoch
 * @property {() => string} timeZone the IANA time zone local time is shown in, e.g. "Europe/Zagreb"
 */

/**
 * Text values by key. Storage that fails, is full or is blocked never throws:
 * reads give undefined and writes are kept in memory for the session
 * (docs/spec/09-settings-and-persistence.md, Storage layout).
 * @typedef {object} KeyValueStore
 * @property {(key: string) => Promise<string | undefined>} get
 * @property {(key: string, value: string) => Promise<void>} put replaces the whole value in one write
 * @property {(key: string) => Promise<void>} remove
 */

/**
 * @typedef {object} FetchProgress
 * @property {number} received bytes received so far
 * @property {number | undefined} total the bytes to expect, when the response says so reliably
 */

/**
 * @typedef {object} TextFetcher
 * @property {(url: string, options: { signal?: AbortSignal, onProgress?: (progress: FetchProgress) => void }) => Promise<string>} fetchText
 *   the body as UTF-8 text; rejects on a network or HTTP error, and with an AbortError when signal aborts
 */

/**
 * @typedef {object} JobProgress
 * @property {number} done
 * @property {number} total
 */

/**
 * Runs a long computation of core/jobs.js by name, in a Worker or in slices
 * on this thread.
 * @typedef {object} JobRunner
 * @property {(name: string, input: unknown, options: { signal?: AbortSignal, onProgress?: (progress: JobProgress) => void }) => Promise<unknown>} run
 */

export {};
