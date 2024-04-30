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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			((System.ComponentModel.ISupportInitialize)this.chart1).BeginInit();
			SuspendLayout();
			// 
			// chart1
			// 
			this.chart1.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.Text;
			chartArea1.AxisX2.IsLabelAutoFit = false;
			chartArea1.AxisX2.IsMarginVisible = false;
			chartArea1.AxisX2.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.None;
			chartArea1.AxisX2.LabelStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			chartArea1.AxisX2.LabelStyle.Format = "dd MMM yyyy";
			chartArea1.AxisX2.LabelStyle.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
			chartArea1.AxisX2.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
			chartArea1.AxisX2.LabelStyle.IsEndLabelVisible = false;
			chartArea1.AxisX2.MajorGrid.Enabled = false;
			chartArea1.AxisX2.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
			chartArea1.AxisX2.ScaleBreakStyle.LineColor = System.Drawing.Color.Lime;
			chartArea1.AxisX2.Title = "Date";
			chartArea1.AxisX2.TitleAlignment = System.Drawing.StringAlignment.Far;
			chartArea1.AxisX2.TitleFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			chartArea1.AxisY.IsLabelAutoFit = false;
			chartArea1.AxisY.IsMarginVisible = false;
			chartArea1.AxisY.IsReversed = true;
			chartArea1.AxisY.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.None;
			chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			chartArea1.AxisY.MajorGrid.Enabled = false;
			chartArea1.AxisY.MajorGrid.Interval = 1D;
			chartArea1.AxisY.MajorTickMark.Size = 0.5F;
			chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
			chartArea1.AxisY.Title = "Magnitude";
			chartArea1.AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
			chartArea1.AxisY.TitleFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			chartArea1.IsSameFontSizeForAllAxes = true;
			chartArea1.Name = "ChartAreaMagnitude";
			chartArea1.Position.Auto = false;
			chartArea1.Position.Height = 90F;
			chartArea1.Position.Width = 96F;
			chartArea1.Position.X = 1F;
			chartArea1.Position.Y = 8F;
			this.chart1.ChartAreas.Add(chartArea1);
			this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chart1.Location = new System.Drawing.Point(0, 0);
			this.chart1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chart1.Name = "chart1";
			this.chart1.Size = new System.Drawing.Size(1575, 841);
			this.chart1.TabIndex = 1;
			this.chart1.Text = "chart1";
			this.chart1.MouseMove += chart1_MouseMove;
			// 
			// FormGraph
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1575, 841);
			this.Controls.Add(this.chart1);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "FormGraph";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Graph";
			((System.ComponentModel.ISupportInitialize)this.chart1).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
	}
}
