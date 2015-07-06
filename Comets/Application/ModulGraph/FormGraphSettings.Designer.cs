namespace Comets.Application.ModulGraph
{
	partial class FormGraphSettings
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
			this.btnPlotGraph = new System.Windows.Forms.Button();
			this.gbxSelectComet = new System.Windows.Forms.GroupBox();
			this.btnFilter = new System.Windows.Forms.Button();
			this.lblPeriod = new System.Windows.Forms.Label();
			this.lblPerihDist = new System.Windows.Forms.Label();
			this.lblPerihDate = new System.Windows.Forms.Label();
			this.cbComet = new System.Windows.Forms.ComboBox();
			this.gbxTimespan = new System.Windows.Forms.GroupBox();
			this.pnlRangeDaysFromT = new System.Windows.Forms.Panel();
			this.btnTimespanDaysFromTDefault = new System.Windows.Forms.Button();
			this.txtDaysFromTStop = new System.Windows.Forms.TextBox();
			this.txtDaysFromTStart = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pnlRangeDate = new System.Windows.Forms.Panel();
			this.btnEndDate = new System.Windows.Forms.Button();
			this.btnStartDate = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.rbRangeDaysFromT = new System.Windows.Forms.RadioButton();
			this.rbRangeDate = new System.Windows.Forms.RadioButton();
			this.gbxDateFormat = new System.Windows.Forms.GroupBox();
			this.rbDaysFromT = new System.Windows.Forms.RadioButton();
			this.rbJulianDay2 = new System.Windows.Forms.RadioButton();
			this.rbJulianDay = new System.Windows.Forms.RadioButton();
			this.rbDate = new System.Windows.Forms.RadioButton();
			this.gbxChartOptions = new System.Windows.Forms.GroupBox();
			this.cbxAntialiasing = new System.Windows.Forms.CheckBox();
			this.cbxNowLine = new System.Windows.Forms.CheckBox();
			this.cbxPerihelionLine = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtMaxMag = new System.Windows.Forms.TextBox();
			this.txtMinMag = new System.Windows.Forms.TextBox();
			this.cbxMaxMag = new System.Windows.Forms.CheckBox();
			this.cbxMinMag = new System.Windows.Forms.CheckBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.gbxSelectComet.SuspendLayout();
			this.gbxTimespan.SuspendLayout();
			this.pnlRangeDaysFromT.SuspendLayout();
			this.pnlRangeDate.SuspendLayout();
			this.gbxDateFormat.SuspendLayout();
			this.gbxChartOptions.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnPlotGraph
			// 
			this.btnPlotGraph.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.btnPlotGraph.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnPlotGraph.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnPlotGraph.Location = new System.Drawing.Point(332, 251);
			this.btnPlotGraph.Name = "btnPlotGraph";
			this.btnPlotGraph.Size = new System.Drawing.Size(100, 24);
			this.btnPlotGraph.TabIndex = 6;
			this.btnPlotGraph.Text = "Plot Graph";
			this.btnPlotGraph.UseVisualStyleBackColor = true;
			this.btnPlotGraph.Click += new System.EventHandler(this.btnPlotGraph_Click);
			// 
			// gbxSelectComet
			// 
			this.gbxSelectComet.Controls.Add(this.btnFilter);
			this.gbxSelectComet.Controls.Add(this.lblPeriod);
			this.gbxSelectComet.Controls.Add(this.lblPerihDist);
			this.gbxSelectComet.Controls.Add(this.lblPerihDate);
			this.gbxSelectComet.Controls.Add(this.cbComet);
			this.gbxSelectComet.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.gbxSelectComet.Location = new System.Drawing.Point(12, 6);
			this.gbxSelectComet.Name = "gbxSelectComet";
			this.gbxSelectComet.Size = new System.Drawing.Size(325, 135);
			this.gbxSelectComet.TabIndex = 0;
			this.gbxSelectComet.TabStop = false;
			this.gbxSelectComet.Text = "Select comet";
			// 
			// btnFilter
			// 
			this.btnFilter.Location = new System.Drawing.Point(244, 19);
			this.btnFilter.Name = "btnFilter";
			this.btnFilter.Size = new System.Drawing.Size(75, 24);
			this.btnFilter.TabIndex = 1;
			this.btnFilter.Text = "Filter";
			this.btnFilter.UseVisualStyleBackColor = true;
			this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
			// 
			// lblPeriod
			// 
			this.lblPeriod.AutoSize = true;
			this.lblPeriod.Location = new System.Drawing.Point(10, 104);
			this.lblPeriod.Name = "lblPeriod";
			this.lblPeriod.Size = new System.Drawing.Size(41, 13);
			this.lblPeriod.TabIndex = 300;
			this.lblPeriod.Text = "Period:";
			// 
			// lblPerihDist
			// 
			this.lblPerihDist.AutoSize = true;
			this.lblPerihDist.Location = new System.Drawing.Point(10, 77);
			this.lblPerihDist.Name = "lblPerihDist";
			this.lblPerihDist.Size = new System.Drawing.Size(100, 13);
			this.lblPerihDist.TabIndex = 299;
			this.lblPerihDist.Text = "Perihelion distance:";
			// 
			// lblPerihDate
			// 
			this.lblPerihDate.AutoSize = true;
			this.lblPerihDate.Location = new System.Drawing.Point(10, 50);
			this.lblPerihDate.Name = "lblPerihDate";
			this.lblPerihDate.Size = new System.Drawing.Size(82, 13);
			this.lblPerihDate.TabIndex = 298;
			this.lblPerihDate.Text = "Perihelion date:";
			// 
			// cbComet
			// 
			this.cbComet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbComet.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.cbComet.FormattingEnabled = true;
			this.cbComet.IntegralHeight = false;
			this.cbComet.Location = new System.Drawing.Point(12, 20);
			this.cbComet.MaxDropDownItems = 16;
			this.cbComet.Name = "cbComet";
			this.cbComet.Size = new System.Drawing.Size(226, 22);
			this.cbComet.TabIndex = 0;
			this.cbComet.SelectedIndexChanged += new System.EventHandler(this.cbComet_SelectedIndexChanged);
			// 
			// gbxTimespan
			// 
			this.gbxTimespan.Controls.Add(this.pnlRangeDaysFromT);
			this.gbxTimespan.Controls.Add(this.pnlRangeDate);
			this.gbxTimespan.Controls.Add(this.rbRangeDaysFromT);
			this.gbxTimespan.Controls.Add(this.rbRangeDate);
			this.gbxTimespan.Location = new System.Drawing.Point(12, 147);
			this.gbxTimespan.Name = "gbxTimespan";
			this.gbxTimespan.Size = new System.Drawing.Size(480, 83);
			this.gbxTimespan.TabIndex = 3;
			this.gbxTimespan.TabStop = false;
			this.gbxTimespan.Text = "Timespan";
			// 
			// pnlRangeDaysFromT
			// 
			this.pnlRangeDaysFromT.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.pnlRangeDaysFromT.Controls.Add(this.btnTimespanDaysFromTDefault);
			this.pnlRangeDaysFromT.Controls.Add(this.txtDaysFromTStop);
			this.pnlRangeDaysFromT.Controls.Add(this.txtDaysFromTStart);
			this.pnlRangeDaysFromT.Controls.Add(this.label4);
			this.pnlRangeDaysFromT.Controls.Add(this.label3);
			this.pnlRangeDaysFromT.Controls.Add(this.label2);
			this.pnlRangeDaysFromT.Location = new System.Drawing.Point(112, 45);
			this.pnlRangeDaysFromT.Name = "pnlRangeDaysFromT";
			this.pnlRangeDaysFromT.Size = new System.Drawing.Size(362, 27);
			this.pnlRangeDaysFromT.TabIndex = 3;
			// 
			// btnTimespanDaysFromTDefault
			// 
			this.btnTimespanDaysFromTDefault.Location = new System.Drawing.Point(339, 6);
			this.btnTimespanDaysFromTDefault.Name = "btnTimespanDaysFromTDefault";
			this.btnTimespanDaysFromTDefault.Size = new System.Drawing.Size(16, 16);
			this.btnTimespanDaysFromTDefault.TabIndex = 2;
			this.btnTimespanDaysFromTDefault.UseVisualStyleBackColor = true;
			this.btnTimespanDaysFromTDefault.Click += new System.EventHandler(this.btnTimespanDaysFromTDefault_Click);
			// 
			// txtDaysFromTStop
			// 
			this.txtDaysFromTStop.Location = new System.Drawing.Point(190, 3);
			this.txtDaysFromTStop.Name = "txtDaysFromTStop";
			this.txtDaysFromTStop.Size = new System.Drawing.Size(42, 21);
			this.txtDaysFromTStop.TabIndex = 1;
			this.txtDaysFromTStop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtDaysFromTStop.TextChanged += new System.EventHandler(this.txtDaysFromTCommon_TextChanged);
			this.txtDaysFromTStop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDaysFromTCommon_KeyDown);
			this.txtDaysFromTStop.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDaysFromTCommon_KeyPress);
			// 
			// txtDaysFromTStart
			// 
			this.txtDaysFromTStart.Location = new System.Drawing.Point(65, 3);
			this.txtDaysFromTStart.Name = "txtDaysFromTStart";
			this.txtDaysFromTStart.Size = new System.Drawing.Size(42, 21);
			this.txtDaysFromTStart.TabIndex = 0;
			this.txtDaysFromTStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtDaysFromTStart.TextChanged += new System.EventHandler(this.txtDaysFromTCommon_TextChanged);
			this.txtDaysFromTStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDaysFromTCommon_KeyDown);
			this.txtDaysFromTStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDaysFromTCommon_KeyPress);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(244, 7);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(30, 13);
			this.label4.TabIndex = 319;
			this.label4.Text = "days";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(113, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 318;
			this.label3.Text = "days";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(175, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(11, 13);
			this.label2.TabIndex = 317;
			this.label2.Text = "-";
			// 
			// pnlRangeDate
			// 
			this.pnlRangeDate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.pnlRangeDate.Controls.Add(this.btnEndDate);
			this.pnlRangeDate.Controls.Add(this.btnStartDate);
			this.pnlRangeDate.Controls.Add(this.label1);
			this.pnlRangeDate.Location = new System.Drawing.Point(112, 15);
			this.pnlRangeDate.Name = "pnlRangeDate";
			this.pnlRangeDate.Size = new System.Drawing.Size(362, 27);
			this.pnlRangeDate.TabIndex = 1;
			// 
			// btnEndDate
			// 
			this.btnEndDate.Location = new System.Drawing.Point(188, 2);
			this.btnEndDate.Name = "btnEndDate";
			this.btnEndDate.Size = new System.Drawing.Size(172, 23);
			this.btnEndDate.TabIndex = 312;
			this.btnEndDate.Text = "dd.MM.yyyy HH:mm:ss";
			this.btnEndDate.UseVisualStyleBackColor = true;
			this.btnEndDate.Click += new System.EventHandler(this.btnEndDate_Click);
			// 
			// btnStartDate
			// 
			this.btnStartDate.Location = new System.Drawing.Point(2, 2);
			this.btnStartDate.Name = "btnStartDate";
			this.btnStartDate.Size = new System.Drawing.Size(172, 23);
			this.btnStartDate.TabIndex = 311;
			this.btnStartDate.Text = "dd.MM.yyyy HH:mm:ss";
			this.btnStartDate.UseVisualStyleBackColor = true;
			this.btnStartDate.Click += new System.EventHandler(this.btnStartDate_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(175, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(11, 13);
			this.label1.TabIndex = 310;
			this.label1.Text = "-";
			// 
			// rbRangeDaysFromT
			// 
			this.rbRangeDaysFromT.AutoSize = true;
			this.rbRangeDaysFromT.Location = new System.Drawing.Point(12, 48);
			this.rbRangeDaysFromT.Name = "rbRangeDaysFromT";
			this.rbRangeDaysFromT.Size = new System.Drawing.Size(83, 17);
			this.rbRangeDaysFromT.TabIndex = 2;
			this.rbRangeDaysFromT.Text = "Days from T";
			this.rbRangeDaysFromT.UseVisualStyleBackColor = true;
			// 
			// rbRangeDate
			// 
			this.rbRangeDate.AutoSize = true;
			this.rbRangeDate.Checked = true;
			this.rbRangeDate.Location = new System.Drawing.Point(12, 21);
			this.rbRangeDate.Name = "rbRangeDate";
			this.rbRangeDate.Size = new System.Drawing.Size(48, 17);
			this.rbRangeDate.TabIndex = 0;
			this.rbRangeDate.TabStop = true;
			this.rbRangeDate.Text = "Date";
			this.rbRangeDate.UseVisualStyleBackColor = true;
			// 
			// gbxDateFormat
			// 
			this.gbxDateFormat.Controls.Add(this.rbDaysFromT);
			this.gbxDateFormat.Controls.Add(this.rbJulianDay2);
			this.gbxDateFormat.Controls.Add(this.rbJulianDay);
			this.gbxDateFormat.Controls.Add(this.rbDate);
			this.gbxDateFormat.Location = new System.Drawing.Point(343, 6);
			this.gbxDateFormat.Name = "gbxDateFormat";
			this.gbxDateFormat.Size = new System.Drawing.Size(149, 135);
			this.gbxDateFormat.TabIndex = 1;
			this.gbxDateFormat.TabStop = false;
			this.gbxDateFormat.Text = "Date Format";
			// 
			// rbDaysFromT
			// 
			this.rbDaysFromT.AutoSize = true;
			this.rbDaysFromT.Location = new System.Drawing.Point(15, 102);
			this.rbDaysFromT.Name = "rbDaysFromT";
			this.rbDaysFromT.Size = new System.Drawing.Size(83, 17);
			this.rbDaysFromT.TabIndex = 3;
			this.rbDaysFromT.TabStop = true;
			this.rbDaysFromT.Text = "Days from T";
			this.rbDaysFromT.UseVisualStyleBackColor = true;
			// 
			// rbJulianDay2
			// 
			this.rbJulianDay2.AutoSize = true;
			this.rbJulianDay2.Location = new System.Drawing.Point(15, 75);
			this.rbJulianDay2.Name = "rbJulianDay2";
			this.rbJulianDay2.Size = new System.Drawing.Size(126, 17);
			this.rbJulianDay2.TabIndex = 2;
			this.rbJulianDay2.TabStop = true;
			this.rbJulianDay2.Text = "Julian Day - 2450000";
			this.rbJulianDay2.UseVisualStyleBackColor = true;
			// 
			// rbJulianDay
			// 
			this.rbJulianDay.AutoSize = true;
			this.rbJulianDay.Location = new System.Drawing.Point(15, 48);
			this.rbJulianDay.Name = "rbJulianDay";
			this.rbJulianDay.Size = new System.Drawing.Size(74, 17);
			this.rbJulianDay.TabIndex = 1;
			this.rbJulianDay.TabStop = true;
			this.rbJulianDay.Text = "Julian Day";
			this.rbJulianDay.UseVisualStyleBackColor = true;
			// 
			// rbDate
			// 
			this.rbDate.AutoSize = true;
			this.rbDate.Checked = true;
			this.rbDate.Location = new System.Drawing.Point(15, 21);
			this.rbDate.Name = "rbDate";
			this.rbDate.Size = new System.Drawing.Size(48, 17);
			this.rbDate.TabIndex = 0;
			this.rbDate.TabStop = true;
			this.rbDate.Text = "Date";
			this.rbDate.UseVisualStyleBackColor = true;
			// 
			// gbxChartOptions
			// 
			this.gbxChartOptions.Controls.Add(this.cbxAntialiasing);
			this.gbxChartOptions.Controls.Add(this.cbxNowLine);
			this.gbxChartOptions.Controls.Add(this.cbxPerihelionLine);
			this.gbxChartOptions.Location = new System.Drawing.Point(498, 6);
			this.gbxChartOptions.Name = "gbxChartOptions";
			this.gbxChartOptions.Size = new System.Drawing.Size(149, 135);
			this.gbxChartOptions.TabIndex = 2;
			this.gbxChartOptions.TabStop = false;
			this.gbxChartOptions.Text = "Chart options";
			// 
			// cbxAntialiasing
			// 
			this.cbxAntialiasing.AutoSize = true;
			this.cbxAntialiasing.Location = new System.Drawing.Point(15, 102);
			this.cbxAntialiasing.Name = "cbxAntialiasing";
			this.cbxAntialiasing.Size = new System.Drawing.Size(80, 17);
			this.cbxAntialiasing.TabIndex = 2;
			this.cbxAntialiasing.Text = "Antialiasing";
			this.cbxAntialiasing.UseVisualStyleBackColor = true;
			// 
			// cbxNowLine
			// 
			this.cbxNowLine.AutoSize = true;
			this.cbxNowLine.Checked = true;
			this.cbxNowLine.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxNowLine.Location = new System.Drawing.Point(15, 48);
			this.cbxNowLine.Name = "cbxNowLine";
			this.cbxNowLine.Size = new System.Drawing.Size(66, 17);
			this.cbxNowLine.TabIndex = 1;
			this.cbxNowLine.Text = "Now line";
			this.cbxNowLine.UseVisualStyleBackColor = true;
			// 
			// cbxPerihelionLine
			// 
			this.cbxPerihelionLine.AutoSize = true;
			this.cbxPerihelionLine.Checked = true;
			this.cbxPerihelionLine.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxPerihelionLine.Location = new System.Drawing.Point(15, 21);
			this.cbxPerihelionLine.Name = "cbxPerihelionLine";
			this.cbxPerihelionLine.Size = new System.Drawing.Size(91, 17);
			this.cbxPerihelionLine.TabIndex = 0;
			this.cbxPerihelionLine.Text = "Perihelion line";
			this.cbxPerihelionLine.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtMaxMag);
			this.groupBox1.Controls.Add(this.txtMinMag);
			this.groupBox1.Controls.Add(this.cbxMaxMag);
			this.groupBox1.Controls.Add(this.cbxMinMag);
			this.groupBox1.Location = new System.Drawing.Point(498, 147);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(149, 83);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Magnitude";
			// 
			// txtMaxMag
			// 
			this.txtMaxMag.Location = new System.Drawing.Point(94, 47);
			this.txtMaxMag.Name = "txtMaxMag";
			this.txtMaxMag.Size = new System.Drawing.Size(49, 21);
			this.txtMaxMag.TabIndex = 3;
			this.txtMaxMag.Text = "0";
			this.txtMaxMag.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMaxMag.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMagCommon_KeyPress);
			// 
			// txtMinMag
			// 
			this.txtMinMag.Location = new System.Drawing.Point(94, 20);
			this.txtMinMag.Name = "txtMinMag";
			this.txtMinMag.Size = new System.Drawing.Size(49, 21);
			this.txtMinMag.TabIndex = 1;
			this.txtMinMag.Text = "0";
			this.txtMinMag.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMinMag.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMagCommon_KeyPress);
			// 
			// cbxMaxMag
			// 
			this.cbxMaxMag.AutoSize = true;
			this.cbxMaxMag.Location = new System.Drawing.Point(15, 48);
			this.cbxMaxMag.Name = "cbxMaxMag";
			this.cbxMaxMag.Size = new System.Drawing.Size(70, 17);
			this.cbxMaxMag.TabIndex = 2;
			this.cbxMaxMag.Text = "Maximum";
			this.cbxMaxMag.UseVisualStyleBackColor = true;
			// 
			// cbxMinMag
			// 
			this.cbxMinMag.AutoSize = true;
			this.cbxMinMag.Location = new System.Drawing.Point(15, 21);
			this.cbxMinMag.Name = "cbxMinMag";
			this.cbxMinMag.Size = new System.Drawing.Size(66, 17);
			this.cbxMinMag.TabIndex = 0;
			this.cbxMinMag.Text = "Minimum";
			this.cbxMinMag.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(226, 251);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 24);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// FormGraphSettings
			// 
			this.AcceptButton = this.btnPlotGraph;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(659, 295);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gbxChartOptions);
			this.Controls.Add(this.gbxDateFormat);
			this.Controls.Add(this.gbxTimespan);
			this.Controls.Add(this.gbxSelectComet);
			this.Controls.Add(this.btnPlotGraph);
			this.Controls.Add(this.btnCancel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormGraphSettings";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Magnitude graph settings";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGraphSettings_FormClosing);
			this.Load += new System.EventHandler(this.FormGraphSettings_Load);
			this.gbxSelectComet.ResumeLayout(false);
			this.gbxSelectComet.PerformLayout();
			this.gbxTimespan.ResumeLayout(false);
			this.gbxTimespan.PerformLayout();
			this.pnlRangeDaysFromT.ResumeLayout(false);
			this.pnlRangeDaysFromT.PerformLayout();
			this.pnlRangeDate.ResumeLayout(false);
			this.pnlRangeDate.PerformLayout();
			this.gbxDateFormat.ResumeLayout(false);
			this.gbxDateFormat.PerformLayout();
			this.gbxChartOptions.ResumeLayout(false);
			this.gbxChartOptions.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnPlotGraph;
		private System.Windows.Forms.GroupBox gbxSelectComet;
		private System.Windows.Forms.GroupBox gbxTimespan;
		private System.Windows.Forms.Label lblPeriod;
		private System.Windows.Forms.Label lblPerihDist;
		private System.Windows.Forms.Label lblPerihDate;
		private System.Windows.Forms.ComboBox cbComet;
		private System.Windows.Forms.RadioButton rbRangeDaysFromT;
		private System.Windows.Forms.RadioButton rbRangeDate;
		private System.Windows.Forms.GroupBox gbxDateFormat;
		private System.Windows.Forms.RadioButton rbDaysFromT;
		private System.Windows.Forms.RadioButton rbJulianDay2;
		private System.Windows.Forms.RadioButton rbJulianDay;
		private System.Windows.Forms.RadioButton rbDate;
		private System.Windows.Forms.Panel pnlRangeDaysFromT;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel pnlRangeDate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox gbxChartOptions;
		private System.Windows.Forms.CheckBox cbxAntialiasing;
		private System.Windows.Forms.CheckBox cbxNowLine;
		private System.Windows.Forms.CheckBox cbxPerihelionLine;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtMaxMag;
		private System.Windows.Forms.TextBox txtMinMag;
		private System.Windows.Forms.CheckBox cbxMaxMag;
		private System.Windows.Forms.CheckBox cbxMinMag;
		private System.Windows.Forms.TextBox txtDaysFromTStop;
		private System.Windows.Forms.TextBox txtDaysFromTStart;
		private System.Windows.Forms.Button btnTimespanDaysFromTDefault;
		private System.Windows.Forms.Button btnFilter;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnEndDate;
		private System.Windows.Forms.Button btnStartDate;
	}
}