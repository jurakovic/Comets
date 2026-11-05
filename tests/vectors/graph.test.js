// graph.json: the sampling intervals, and the model of 5 graphs at the vector clock (spec 07).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { drain } from '../helpers/generators.js';
import { assertChart, GRAPH, graphCase } from '../helpers/graph.js';
import { utcInstant } from '../helpers/vectors.js';
import { calculateGraph } from '../../app/js/core/graph/model.js';
import { samplingInterval } from '../../app/js/core/graph/sampling.js';
import { graphTitle, validateGraphSettings } from '../../app/js/core/graph/settings.js';

test('the sampling interval by span', () => {
	assert.equal(GRAPH.samplingIntervals.length, 9);
	for (const { startUtc, stopUtc, intervalDays } of GRAPH.samplingIntervals) {
		assert.equal(samplingInterval(utcInstant(startUtc), utcInstant(stopUtc)), intervalDays, `${startUtc} – ${stopUtc}`);
	}
});

test('5 cases', () => {
	assert.equal(GRAPH.cases.length, 5);
});

for (const { id, settings: vector, chart } of GRAPH.cases) {
	test(id, () => {
		const { settings, selection, input } = graphCase(vector);
		assert.ok(validateGraphSettings(settings).ok);
		assert.equal(graphTitle(settings, selection), vector.title);
		const { result } = drain(calculateGraph(input));
		assertChart(result, chart, id);
	});
}
