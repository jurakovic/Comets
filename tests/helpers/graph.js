// The cases of graph.json in the web app's shapes (docs/spec/07-graph.md).
import assert from 'node:assert/strict';
import { SAMPLE } from './ephemeris.js';
import { createFakeClock } from './fakes.js';
import { assertClose, readVector, utcInstant, VECTOR_NOW } from './vectors.js';
import { cometsToCalculate } from '../../app/js/core/ephemeris/calculation.js';
import { defaultGraphSettings } from '../../app/js/core/graph/settings.js';
import { instantToJd } from '../../app/js/core/time/julian.js';
import { createObservationsService } from '../../app/js/services/observations.js';

/** the whole of graph.json */
export const GRAPH = readVector('graph.json');

/**
 * a case's settings and selection, and the calculation's input at the vector clock for the
 * observer at 0, 0
 * @param {any} vector the case's settings
 */
export function graphCase(vector) {
	const limit = (/** @type {number | null} */ value) => ({ checked: value !== null, value });
	const settings = {
		...defaultGraphSettings(VECTOR_NOW),
		start: utcInstant(vector.startUtc),
		end: utcInstant(vector.stopUtc),
		chartType: vector.chartType,
		perihelionLine: { checked: vector.perihelionLine, color: '#4169E1' },
		nowLine: { checked: vector.nowLine, color: '#32CD32' },
		minimum: limit(vector.minValue),
		maximum: limit(vector.maxValue)
	};
	const selected = vector.multiple ? null : SAMPLE.find(c => c.full === vector.comets) ?? null;
	assert.ok(vector.multiple || selected, vector.comets);
	const selection = { multiple: vector.multiple, comets: SAMPLE, selected };
	const observer = { latitude: 0, longitude: 0 };
	const observations = createObservationsService({ clock: createFakeClock(VECTOR_NOW), observer: () => observer });
	const comets = cometsToCalculate(selection);
	const input = {
		comets, nearest: comets.map(observations.nearestPerihelion), settings, multiple: vector.multiple, observer, nowJd: instantToJd(VECTOR_NOW)
	};
	return { settings, selection, input };
}

const TAGS = Object.freeze({ value: 'VALUE:', perihelion: 'PERIHELION:', now: 'NOW:' });

/**
 * a model against a vector's chart: the axes exactly, x within a second (the desktop chart holds
 * whole seconds), values within 1e-8, line ends exactly
 * @param {import('../../app/js/core/graph/model.js').GraphModel} model
 * @param {any} chart
 * @param {string} label
 */
export function assertChart(model, chart, label) {
	assert.equal(model.title, chart.title, `${label} title`);
	assert.deepEqual(
		{ title: model.timeAxis.title, min: model.timeAxis.min, max: model.timeAxis.max, labelFormat: model.timeAxis.labelFormat },
		{ title: chart.xAxis.title, min: chart.xAxis.minJD, max: chart.xAxis.maxJD, labelFormat: chart.xAxis.labelFormat },
		`${label} time axis`
	);
	const { valueAxis } = model;
	assert.deepEqual(
		{ title: valueAxis.title, min: valueAxis.min, max: valueAxis.max, step: valueAxis.step, reversed: true, labelFormat: '00.00' },
		{ title: chart.yAxis.title, min: chart.yAxis.minimum, max: chart.yAxis.maximum, step: chart.yAxis.interval, reversed: chart.yAxis.reversed, labelFormat: chart.yAxis.labelFormat },
		`${label} value axis`
	);
	assert.deepEqual(model.series.map(s => TAGS[s.kind] + (s.comet ?? '')), chart.series.map((/** @type {any} */ s) => s.tag), `${label} series`);
	model.series.forEach((series, index) => {
		const { points, kind } = chart.series[index];
		assert.equal(series.kind, kind);
		assert.equal(series.x.length, points.length, `${label} ${index} points`);
		points.forEach((/** @type {[number, number]} */ [x, y], k) => {
			assertClose(series.x[k], x, 1 / 86400, `${label} ${index} x[${k}]`);
			if (kind === 'value') assertClose(series.y[k], y, 1e-8, `${label} ${index} y[${k}]`);
			else assert.equal(series.y[k], y, `${label} ${index} y[${k}]`);
		});
	});
}
