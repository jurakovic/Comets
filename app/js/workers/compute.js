// The compute Worker: serves the jobs of core/jobs.js over the protocol of lib/rpc.js.
// docs/spec/12-architecture.md, Long computations and the Worker.
import { JOBS } from '../core/jobs.js';
import { createRpcServer } from '../lib/rpc.js';

const server = createRpcServer({
	jobs: JOBS,
	post: reply => self.postMessage(reply),
	// a task, not a microtask, so that a cancel message is read between slices
	schedule: task => setTimeout(task, 0),
	now: () => performance.now()
});

self.addEventListener('message', (/** @type {MessageEvent} */ event) => server.receive(event.data));
