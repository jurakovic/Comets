// Inert stand-ins for the handful of WinForms / GDI+ types the desktop engine sources
// reference. They exist only so those sources compile unchanged on a non-Windows host.
// None of the code paths the harness exercises shows UI; if one ever does, the stub
// fails loudly instead of silently answering a dialog.

using System;
using System.Collections.Generic;

namespace System.Windows.Forms
{
	public enum DialogResult { None, OK, Cancel, Abort, Retry, Ignore, Yes, No }
	public enum MessageBoxButtons { OK, OKCancel, AbortRetryIgnore, YesNoCancel, YesNo, RetryCancel }
	public enum MessageBoxIcon { None, Error, Question, Warning, Information }
	public enum MessageBoxDefaultButton { Button1, Button2, Button3 }

	public static class MessageBox
	{
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			throw new InvalidOperationException("Unexpected MessageBox in harness: " + text);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			throw new InvalidOperationException("Unexpected MessageBox in harness: " + text);
		}
	}

	public class SaveFileDialog : IDisposable
	{
		public string InitialDirectory { get; set; }
		public string Filter { get; set; }
		public string FileName { get; set; }
		public DialogResult ShowDialog() { throw new InvalidOperationException("Unexpected SaveFileDialog in harness"); }
		public void Dispose() { }
	}

	public class Control
	{
		public bool Visible { get; set; }
		public string Text { get; set; }
	}

	public class TextBoxBase : Control { }

	public class CheckBox : Control
	{
		public bool Checked { get; set; }
	}

	public class ToolStripMenuItem
	{
		public bool Checked { get; set; }
	}
}

namespace System.Drawing
{
	public class Font
	{
		public Font(string familyName, float emSize) { FamilyName = familyName; Size = emSize; }
		public string FamilyName { get; }
		public float Size { get; }
	}

	public enum StringAlignment { Near, Center, Far }
}

namespace System.Windows.Forms.DataVisualization.Charting
{
	using System.Drawing;

	// A recording chart: GenerateGraph writes its axes and series here and the harness
	// reads them back as the graph test vectors.

	public enum AntiAliasingStyles { None, Text, Graphics, All }
	public enum LabelAutoFitStyles { None, WordWrap }
	public enum TextOrientation { Auto, Horizontal, Rotated90, Rotated270, Stacked }
	public enum DateTimeIntervalType { Auto, Number, Years, Months, Weeks, Days, Hours, Minutes, Seconds, Milliseconds, NotSet }
	public enum SeriesChartType { Point, Line, Spline }
	public enum AxisType { Primary, Secondary }
	public enum ChartValueType { Auto, Double, DateTime }

	public class Chart
	{
		public AntiAliasingStyles AntiAliasing { get; set; }
		public Color BackColor { get; set; }
		public List<ChartArea> ChartAreas { get; } = new List<ChartArea>();
		public List<Series> Series { get; } = new List<Series>();
		public List<Title> Titles { get; } = new List<Title>();
	}

	public class ChartArea
	{
		public string Name { get; set; }
		public Color BackColor { get; set; }
		public Axis AxisX2 { get; } = new Axis();
		public Axis AxisY { get; } = new Axis();
		public ElementPosition Position { get; } = new ElementPosition();
	}

	public class Axis
	{
		public string Title { get; set; }
		public StringAlignment TitleAlignment { get; set; }
		public Font TitleFont { get; set; }
		public Color TitleForeColor { get; set; }
		public Grid MajorGrid { get; } = new Grid();
		public bool IsLabelAutoFit { get; set; }
		public LabelAutoFitStyles LabelAutoFitStyle { get; set; }
		public bool IsMarginVisible { get; set; }
		public TickMark MajorTickMark { get; } = new TickMark();
		public Color LineColor { get; set; }
		public LabelStyle LabelStyle { get; } = new LabelStyle();
		public TextOrientation TextOrientation { get; set; }
		public bool IsReversed { get; set; }
		public double Minimum { get; set; }
		public double Maximum { get; set; }
		public double Interval { get; set; }
		public double IntervalOffset { get; set; }
		public DateTimeIntervalType IntervalType { get; set; }
	}

	public class Grid { public bool Enabled { get; set; } }
	public class TickMark { public Color LineColor { get; set; } public float Size { get; set; } }
	public class LabelStyle { public Font Font { get; set; } public Color ForeColor { get; set; } public string Format { get; set; } }
	public class ElementPosition { public bool Auto { get; set; } public float Height { get; set; } public float Width { get; set; } public float X { get; set; } public float Y { get; set; } }

	public class Series
	{
		public object Tag { get; set; }
		public string ChartArea { get; set; }
		public Color Color { get; set; }
		public SeriesChartType ChartType { get; set; }
		public AxisType XAxisType { get; set; }
		public ChartValueType XValueType { get; set; }
		public List<DataPoint> Points { get; } = new List<DataPoint>();
	}

	public class DataPoint
	{
		public DataPoint(double xValue, double yValue) { XValue = xValue; YValues = new[] { yValue }; }
		public double XValue { get; }
		public double[] YValues { get; }
	}

	public class Title
	{
		public Title(string text) { Text = text; }
		public string Text { get; }
		public Font Font { get; set; }
		public Color ForeColor { get; set; }
	}
}
