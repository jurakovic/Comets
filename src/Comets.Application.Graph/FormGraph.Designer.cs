namespace Comets.Application.Graph
{
	partial class FormGraph
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
			SuspendLayout();
			// 
			// chart1
			// 
			chart1.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.Text;
			chartArea2.AxisX2.IsLabelAutoFit = false;
			chartArea2.AxisX2.IsMarginVisible = false;
			chartArea2.AxisX2.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.None;
			chartArea2.AxisX2.LabelStyle.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			chartArea2.AxisX2.LabelStyle.Format = "dd MMM yyyy";
			chartArea2.AxisX2.LabelStyle.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
			chartArea2.AxisX2.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
			chartArea2.AxisX2.LabelStyle.IsEndLabelVisible = false;
			chartArea2.AxisX2.MajorGrid.Enabled = false;
			chartArea2.AxisX2.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
			chartArea2.AxisX2.ScaleBreakStyle.LineColor = Color.Lime;
			chartArea2.AxisX2.Title = "Date";
			chartArea2.AxisX2.TitleAlignment = StringAlignment.Far;
			chartArea2.AxisX2.TitleFont = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			chartArea2.AxisY.IsLabelAutoFit = false;
			chartArea2.AxisY.IsMarginVisible = false;
			chartArea2.AxisY.IsReversed = true;
			chartArea2.AxisY.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.None;
			chartArea2.AxisY.LabelStyle.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			chartArea2.AxisY.MajorGrid.Enabled = false;
			chartArea2.AxisY.MajorGrid.Interval = 1D;
			chartArea2.AxisY.MajorTickMark.Size = 0.5F;
			chartArea2.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
			chartArea2.AxisY.Title = "Magnitude";
			chartArea2.AxisY.TitleAlignment = StringAlignment.Far;
			chartArea2.AxisY.TitleFont = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			chartArea2.IsSameFontSizeForAllAxes = true;
			chartArea2.Name = "ChartAreaMagnitude";
			chartArea2.Position.Auto = false;
			chartArea2.Position.Height = 90F;
			chartArea2.Position.Width = 96F;
			chartArea2.Position.X = 1F;
			chartArea2.Position.Y = 8F;
			chart1.ChartAreas.Add(chartArea2);
			chart1.Dock = DockStyle.Fill;
			chart1.Location = new Point(0, 0);
			chart1.Margin = new Padding(4, 3, 4, 3);
			chart1.Name = "chart1";
			chart1.Size = new Size(1575, 841);
			chart1.TabIndex = 1;
			chart1.Text = "chart1";
			chart1.MouseMove += chart1_MouseMove;
			// 
			// FormGraph
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1575, 841);
			Controls.Add(chart1);
			Margin = new Padding(4, 3, 4, 3);
			Name = "FormGraph";
			ShowIcon = false;
			ShowInTaskbar = false;
			Text = "Graph";
			((System.ComponentModel.ISupportInitialize)chart1).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
	}
}