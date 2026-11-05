// The compute Worker: serves the jobs of core/jobs.js over the protocol of lib/rpc.js.
// docs/spec/12-architecture.md, Long computations and the Worker.
import { JOBS } from '../core/jobs.js';
import { createRpcServer } from '../lib/rpc.js';

// the next slice as a message to ourselves: a task, not a microtask, so that a cancel is read
// between slices, and without the 4 ms that nested timers wait
/** @type {Array<() => void>} */
const slices = [];
const channel = new MessageChannel();
channel.port1.addEventListener('message', () => slices.shift()?.());
channel.port1.start();

const server = createRpcServer({
	jobs: JOBS,
	post: reply => self.postMessage(reply),
	schedule: task => { slices.push(task); channel.port2.postMessage(null); },
	now: () => performance.now()
});

self.addEventListener('message', (/** @type {MessageEvent} */ event) => server.receive(event.data));
