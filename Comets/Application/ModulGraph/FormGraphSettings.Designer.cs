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
			this.btnOk = new System.Windows.Forms.Button();
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
			this.rbtnRangeDaysFromT = new System.Windows.Forms.RadioButton();
			this.rbtnRangeDate = new System.Windows.Forms.RadioButton();
			this.gbxChartOptions = new System.Windows.Forms.GroupBox();
			this.lblValue = new System.Windows.Forms.Label();
			this.cbxValue = new System.Windows.Forms.CheckBox();
			this.pnlPerihLineColor = new System.Windows.Forms.Panel();
			this.pnlNowLineColor = new System.Windows.Forms.Panel();
			this.pnlMagnitudeColor = new System.Windows.Forms.Panel();
			this.cbxAntialiasing = new System.Windows.Forms.CheckBox();
			this.cbxNowLine = new System.Windows.Forms.CheckBox();
			this.cbxPerihelionLine = new System.Windows.Forms.CheckBox();
			this.gbxValueRange = new System.Windows.Forms.GroupBox();
			this.txtMaxValue = new System.Windows.Forms.TextBox();
			this.txtMinValue = new System.Windows.Forms.TextBox();
			this.cbxMaxValue = new System.Windows.Forms.CheckBox();
			this.cbxMinValue = new System.Windows.Forms.CheckBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.gbxMode = new System.Windows.Forms.GroupBox();
			this.lblMultipleCount = new System.Windows.Forms.Label();
			this.rbtnMultiple = new System.Windows.Forms.RadioButton();
			this.rbtnSingle = new System.Windows.Forms.RadioButton();
			this.gbxChartType = new System.Windows.Forms.GroupBox();
			this.rbtnEarthDistance = new System.Windows.Forms.RadioButton();
			this.rbtnSunDistance = new System.Windows.Forms.RadioButton();
			this.rbtnMagnitude = new System.Windows.Forms.RadioButton();
			this.gbxSelectComet.SuspendLayout();
			this.gbxTimespan.SuspendLayout();
			this.pnlRangeDaysFromT.SuspendLayout();
			this.pnlRangeDate.SuspendLayout();
			this.gbxChartOptions.SuspendLayout();
			this.gbxValueRange.SuspendLayout();
			this.gbxMode.SuspendLayout();
			this.gbxChartType.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnOk.Location = new System.Drawing.Point(524, 242);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(100, 24);
			this.btnOk.TabIndex = 6;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
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
			this.lblPeriod.TabIndex = 4;
			this.lblPeriod.Text = "Period:";
			// 
			// lblPerihDist
			// 
			this.lblPerihDist.AutoSize = true;
			this.lblPerihDist.Location = new System.Drawing.Point(10, 77);
			this.lblPerihDist.Name = "lblPerihDist";
			this.lblPerihDist.Size = new System.Drawing.Size(100, 13);
			this.lblPerihDist.TabIndex = 3;
			this.lblPerihDist.Text = "Perihelion distance:";
			// 
			// lblPerihDate
			// 
			this.lblPerihDate.AutoSize = true;
			this.lblPerihDate.Location = new System.Drawing.Point(10, 50);
			this.lblPerihDate.Name = "lblPerihDate";
			this.lblPerihDate.Size = new System.Drawing.Size(82, 13);
			this.lblPerihDate.TabIndex = 2;
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
			this.gbxTimespan.Controls.Add(this.rbtnRangeDaysFromT);
			this.gbxTimespan.Controls.Add(this.rbtnRangeDate);
			this.gbxTimespan.Location = new System.Drawing.Point(12, 147);
			this.gbxTimespan.Name = "gbxTimespan";
			this.gbxTimespan.Size = new System.Drawing.Size(558, 83);
			this.gbxTimespan.TabIndex = 4;
			this.gbxTimespan.TabStop = false;
			this.gbxTimespan.Text = "Timespan (Local time)";
			// 
			// pnlRangeDaysFromT
			// 
			this.pnlRangeDaysFromT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlRangeDaysFromT.Controls.Add(this.btnTimespanDaysFromTDefault);
			this.pnlRangeDaysFromT.Controls.Add(this.txtDaysFromTStop);
			this.pnlRangeDaysFromT.Controls.Add(this.txtDaysFromTStart);
			this.pnlRangeDaysFromT.Controls.Add(this.label4);
			this.pnlRangeDaysFromT.Controls.Add(this.label3);
			this.pnlRangeDaysFromT.Controls.Add(this.label2);
			this.pnlRangeDaysFromT.Location = new System.Drawing.Point(167, 45);
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
			this.pnlRangeDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlRangeDate.Controls.Add(this.btnEndDate);
			this.pnlRangeDate.Controls.Add(this.btnStartDate);
			this.pnlRangeDate.Controls.Add(this.label1);
			this.pnlRangeDate.Location = new System.Drawing.Point(167, 15);
			this.pnlRangeDate.Name = "pnlRangeDate";
			this.pnlRangeDate.Size = new System.Drawing.Size(362, 27);
			this.pnlRangeDate.TabIndex = 1;
			// 
			// btnEndDate
			// 
			this.btnEndDate.Location = new System.Drawing.Point(188, 2);
			this.btnEndDate.Name = "btnEndDate";
			this.btnEndDate.Size = new System.Drawing.Size(172, 23);
			this.btnEndDate.TabIndex = 1;
			this.btnEndDate.Text = "dd.MM.yyyy. HH:mm:ss";
			this.btnEndDate.UseVisualStyleBackColor = true;
			this.btnEndDate.Click += new System.EventHandler(this.btnEndDate_Click);
			// 
			// btnStartDate
			// 
			this.btnStartDate.Location = new System.Drawing.Point(2, 2);
			this.btnStartDate.Name = "btnStartDate";
			this.btnStartDate.Size = new System.Drawing.Size(172, 23);
			this.btnStartDate.TabIndex = 0;
			this.btnStartDate.Text = "dd.MM.yyyy. HH:mm:ss";
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
			// rbtnRangeDaysFromT
			// 
			this.rbtnRangeDaysFromT.AutoSize = true;
			this.rbtnRangeDaysFromT.Location = new System.Drawing.Point(12, 48);
			this.rbtnRangeDaysFromT.Name = "rbtnRangeDaysFromT";
			this.rbtnRangeDaysFromT.Size = new System.Drawing.Size(83, 17);
			this.rbtnRangeDaysFromT.TabIndex = 2;
			this.rbtnRangeDaysFromT.Text = "Days from T";
			this.rbtnRangeDaysFromT.UseVisualStyleBackColor = true;
			// 
			// rbtnRangeDate
			// 
			this.rbtnRangeDate.AutoSize = true;
			this.rbtnRangeDate.Checked = true;
			this.rbtnRangeDate.Location = new System.Drawing.Point(12, 21);
			this.rbtnRangeDate.Name = "rbtnRangeDate";
			this.rbtnRangeDate.Size = new System.Drawing.Size(48, 17);
			this.rbtnRangeDate.TabIndex = 0;
			this.rbtnRangeDate.TabStop = true;
			this.rbtnRangeDate.Text = "Date";
			this.rbtnRangeDate.UseVisualStyleBackColor = true;
			// 
			// gbxChartOptions
			// 
			this.gbxChartOptions.Controls.Add(this.lblValue);
			this.gbxChartOptions.Controls.Add(this.cbxValue);
			this.gbxChartOptions.Controls.Add(this.pnlPerihLineColor);
			this.gbxChartOptions.Controls.Add(this.pnlNowLineColor);
			this.gbxChartOptions.Controls.Add(this.pnlMagnitudeColor);
			this.gbxChartOptions.Controls.Add(this.cbxAntialiasing);
			this.gbxChartOptions.Controls.Add(this.cbxNowLine);
			this.gbxChartOptions.Controls.Add(this.cbxPerihelionLine);
			this.gbxChartOptions.Location = new System.Drawing.Point(576, 6);
			this.gbxChartOptions.Name = "gbxChartOptions";
			this.gbxChartOptions.Size = new System.Drawing.Size(153, 135);
			this.gbxChartOptions.TabIndex = 3;
			this.gbxChartOptions.TabStop = false;
			this.gbxChartOptions.Text = "Chart options";
			// 
			// lblValue
			// 
			this.lblValue.AutoSize = true;
			this.lblValue.Location = new System.Drawing.Point(31, 22);
			this.lblValue.Name = "lblValue";
			this.lblValue.Size = new System.Drawing.Size(33, 13);
			this.lblValue.TabIndex = 0;
			this.lblValue.Text = "Value";
			// 
			// cbxValue
			// 
			this.cbxValue.AutoSize = true;
			this.cbxValue.Checked = true;
			this.cbxValue.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxValue.Enabled = false;
			this.cbxValue.Location = new System.Drawing.Point(15, 21);
			this.cbxValue.Name = "cbxValue";
			this.cbxValue.Size = new System.Drawing.Size(29, 17);
			this.cbxValue.TabIndex = 6;
			this.cbxValue.Text = " ";
			this.cbxValue.UseVisualStyleBackColor = true;
			// 
			// pnlPerihLineColor
			// 
			this.pnlPerihLineColor.BackColor = System.Drawing.Color.RoyalBlue;
			this.pnlPerihLineColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlPerihLineColor.Location = new System.Drawing.Point(120, 72);
			this.pnlPerihLineColor.Name = "pnlPerihLineColor";
			this.pnlPerihLineColor.Size = new System.Drawing.Size(25, 20);
			this.pnlPerihLineColor.TabIndex = 5;
			this.pnlPerihLineColor.TabStop = true;
			this.pnlPerihLineColor.Click += new System.EventHandler(this.pnColorCommon_Click);
			// 
			// pnlNowLineColor
			// 
			this.pnlNowLineColor.BackColor = System.Drawing.Color.LimeGreen;
			this.pnlNowLineColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlNowLineColor.Location = new System.Drawing.Point(120, 45);
			this.pnlNowLineColor.Name = "pnlNowLineColor";
			this.pnlNowLineColor.Size = new System.Drawing.Size(25, 20);
			this.pnlNowLineColor.TabIndex = 3;
			this.pnlNowLineColor.TabStop = true;
			this.pnlNowLineColor.Click += new System.EventHandler(this.pnColorCommon_Click);
			// 
			// pnlMagnitudeColor
			// 
			this.pnlMagnitudeColor.BackColor = System.Drawing.Color.Red;
			this.pnlMagnitudeColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlMagnitudeColor.Location = new System.Drawing.Point(120, 19);
			this.pnlMagnitudeColor.Name = "pnlMagnitudeColor";
			this.pnlMagnitudeColor.Size = new System.Drawing.Size(25, 20);
			this.pnlMagnitudeColor.TabIndex = 1;
			this.pnlMagnitudeColor.TabStop = true;
			this.pnlMagnitudeColor.Click += new System.EventHandler(this.pnColorCommon_Click);
			// 
			// cbxAntialiasing
			// 
			this.cbxAntialiasing.AutoSize = true;
			this.cbxAntialiasing.Location = new System.Drawing.Point(15, 102);
			this.cbxAntialiasing.Name = "cbxAntialiasing";
			this.cbxAntialiasing.Size = new System.Drawing.Size(80, 17);
			this.cbxAntialiasing.TabIndex = 6;
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
			this.cbxNowLine.TabIndex = 2;
			this.cbxNowLine.Text = "Now line";
			this.cbxNowLine.UseVisualStyleBackColor = true;
			// 
			// cbxPerihelionLine
			// 
			this.cbxPerihelionLine.AutoSize = true;
			this.cbxPerihelionLine.Checked = true;
			this.cbxPerihelionLine.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxPerihelionLine.Location = new System.Drawing.Point(15, 75);
			this.cbxPerihelionLine.Name = "cbxPerihelionLine";
			this.cbxPerihelionLine.Size = new System.Drawing.Size(91, 17);
			this.cbxPerihelionLine.TabIndex = 4;
			this.cbxPerihelionLine.Text = "Perihelion line";
			this.cbxPerihelionLine.UseVisualStyleBackColor = true;
			// 
			// gbxValueRange
			// 
			this.gbxValueRange.Controls.Add(this.txtMaxValue);
			this.gbxValueRange.Controls.Add(this.txtMinValue);
			this.gbxValueRange.Controls.Add(this.cbxMaxValue);
			this.gbxValueRange.Controls.Add(this.cbxMinValue);
			this.gbxValueRange.Location = new System.Drawing.Point(576, 147);
			this.gbxValueRange.Name = "gbxValueRange";
			this.gbxValueRange.Size = new System.Drawing.Size(153, 83);
			this.gbxValueRange.TabIndex = 5;
			this.gbxValueRange.TabStop = false;
			this.gbxValueRange.Text = "Value range";
			// 
			// txtMaxValue
			// 
			this.txtMaxValue.Location = new System.Drawing.Point(99, 47);
			this.txtMaxValue.Name = "txtMaxValue";
			this.txtMaxValue.Size = new System.Drawing.Size(45, 21);
			this.txtMaxValue.TabIndex = 3;
			this.txtMaxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMaxValue.TextChanged += new System.EventHandler(this.txtMaxMag_TextChanged);
			this.txtMaxValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMagCommon_KeyPress);
			// 
			// txtMinValue
			// 
			this.txtMinValue.Location = new System.Drawing.Point(99, 20);
			this.txtMinValue.Name = "txtMinValue";
			this.txtMinValue.Size = new System.Drawing.Size(45, 21);
			this.txtMinValue.TabIndex = 1;
			this.txtMinValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMinValue.TextChanged += new System.EventHandler(this.txtMinMag_TextChanged);
			this.txtMinValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMagCommon_KeyPress);
			// 
			// cbxMaxValue
			// 
			this.cbxMaxValue.AutoSize = true;
			this.cbxMaxValue.Location = new System.Drawing.Point(15, 48);
			this.cbxMaxValue.Name = "cbxMaxValue";
			this.cbxMaxValue.Size = new System.Drawing.Size(70, 17);
			this.cbxMaxValue.TabIndex = 2;
			this.cbxMaxValue.Text = "Maximum";
			this.cbxMaxValue.UseVisualStyleBackColor = true;
			// 
			// cbxMinValue
			// 
			this.cbxMinValue.AutoSize = true;
			this.cbxMinValue.Location = new System.Drawing.Point(15, 21);
			this.cbxMinValue.Name = "cbxMinValue";
			this.cbxMinValue.Size = new System.Drawing.Size(66, 17);
			this.cbxMinValue.TabIndex = 0;
			this.cbxMinValue.Text = "Minimum";
			this.cbxMinValue.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(630, 242);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 24);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// gbxMode
			// 
			this.gbxMode.Controls.Add(this.lblMultipleCount);
			this.gbxMode.Controls.Add(this.rbtnMultiple);
			this.gbxMode.Controls.Add(this.rbtnSingle);
			this.gbxMode.Location = new System.Drawing.Point(343, 6);
			this.gbxMode.Name = "gbxMode";
			this.gbxMode.Size = new System.Drawing.Size(106, 135);
			this.gbxMode.TabIndex = 1;
			this.gbxMode.TabStop = false;
			this.gbxMode.Text = "Mode";
			// 
			// lblMultipleCount
			// 
			this.lblMultipleCount.AutoSize = true;
			this.lblMultipleCount.Location = new System.Drawing.Point(30, 77);
			this.lblMultipleCount.Name = "lblMultipleCount";
			this.lblMultipleCount.Size = new System.Drawing.Size(51, 13);
			this.lblMultipleCount.TabIndex = 2;
			this.lblMultipleCount.Text = "N comets";
			// 
			// rbtnMultiple
			// 
			this.rbtnMultiple.AutoSize = true;
			this.rbtnMultiple.Location = new System.Drawing.Point(14, 47);
			this.rbtnMultiple.Name = "rbtnMultiple";
			this.rbtnMultiple.Size = new System.Drawing.Size(61, 17);
			this.rbtnMultiple.TabIndex = 1;
			this.rbtnMultiple.Text = "Multiple";
			this.rbtnMultiple.UseVisualStyleBackColor = true;
			// 
			// rbtnSingle
			// 
			this.rbtnSingle.AutoSize = true;
			this.rbtnSingle.Checked = true;
			this.rbtnSingle.Location = new System.Drawing.Point(14, 21);
			this.rbtnSingle.Name = "rbtnSingle";
			this.rbtnSingle.Size = new System.Drawing.Size(53, 17);
			this.rbtnSingle.TabIndex = 0;
			this.rbtnSingle.TabStop = true;
			this.rbtnSingle.Text = "Single";
			this.rbtnSingle.UseVisualStyleBackColor = true;
			// 
			// gbxChartType
			// 
			this.gbxChartType.Controls.Add(this.rbtnEarthDistance);
			this.gbxChartType.Controls.Add(this.rbtnSunDistance);
			this.gbxChartType.Controls.Add(this.rbtnMagnitude);
			this.gbxChartType.Location = new System.Drawing.Point(455, 6);
			this.gbxChartType.Name = "gbxChartType";
			this.gbxChartType.Size = new System.Drawing.Size(115, 135);
			this.gbxChartType.TabIndex = 2;
			this.gbxChartType.TabStop = false;
			this.gbxChartType.Text = "Chart Type";
			// 
			// rbtnEarthDistance
			// 
			this.rbtnEarthDistance.AutoSize = true;
			this.rbtnEarthDistance.Location = new System.Drawing.Point(14, 75);
			this.rbtnEarthDistance.Name = "rbtnEarthDistance";
			this.rbtnEarthDistance.Size = new System.Drawing.Size(94, 17);
			this.rbtnEarthDistance.TabIndex = 2;
			this.rbtnEarthDistance.Text = "Earth distance";
			this.rbtnEarthDistance.UseVisualStyleBackColor = true;
			// 
			// rbtnSunDistance
			// 
			this.rbtnSunDistance.AutoSize = true;
			this.rbtnSunDistance.Location = new System.Drawing.Point(14, 47);
			this.rbtnSunDistance.Name = "rbtnSunDistance";
			this.rbtnSunDistance.Size = new System.Drawing.Size(86, 17);
			this.rbtnSunDistance.TabIndex = 1;
			this.rbtnSunDistance.Text = "Sun distance";
			this.rbtnSunDistance.UseVisualStyleBackColor = true;
			// 
			// rbtnMagnitude
			// 
			this.rbtnMagnitude.AutoSize = true;
			this.rbtnMagnitude.Checked = true;
			this.rbtnMagnitude.Location = new System.Drawing.Point(14, 21);
			this.rbtnMagnitude.Name = "rbtnMagnitude";
			this.rbtnMagnitude.Size = new System.Drawing.Size(75, 17);
			this.rbtnMagnitude.TabIndex = 0;
			this.rbtnMagnitude.TabStop = true;
			this.rbtnMagnitude.Text = "Magnitude";
			this.rbtnMagnitude.UseVisualStyleBackColor = true;
			// 
			// FormGraphSettings
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(742, 278);
			this.Controls.Add(this.gbxChartType);
			this.Controls.Add(this.gbxMode);
			this.Controls.Add(this.gbxValueRange);
			this.Controls.Add(this.gbxChartOptions);
			this.Controls.Add(this.gbxTimespan);
			this.Controls.Add(this.gbxSelectComet);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormGraphSettings";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Graph settings";
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
			this.gbxChartOptions.ResumeLayout(false);
			this.gbxChartOptions.PerformLayout();
			this.gbxValueRange.ResumeLayout(false);
			this.gbxValueRange.PerformLayout();
			this.gbxMode.ResumeLayout(false);
			this.gbxMode.PerformLayout();
			this.gbxChartType.ResumeLayout(false);
			this.gbxChartType.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox gbxSelectComet;
		private System.Windows.Forms.GroupBox gbxTimespan;
		private System.Windows.Forms.Label lblPeriod;
		private System.Windows.Forms.Label lblPerihDist;
		private System.Windows.Forms.Label lblPerihDate;
		private System.Windows.Forms.ComboBox cbComet;
		private System.Windows.Forms.RadioButton rbtnRangeDaysFromT;
		private System.Windows.Forms.RadioButton rbtnRangeDate;
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
		private System.Windows.Forms.GroupBox gbxValueRange;
		private System.Windows.Forms.TextBox txtMaxValue;
		private System.Windows.Forms.TextBox txtMinValue;
		private System.Windows.Forms.CheckBox cbxMaxValue;
		private System.Windows.Forms.CheckBox cbxMinValue;
		private System.Windows.Forms.TextBox txtDaysFromTStop;
		private System.Windows.Forms.TextBox txtDaysFromTStart;
		private System.Windows.Forms.Button btnTimespanDaysFromTDefault;
		private System.Windows.Forms.Button btnFilter;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnEndDate;
		private System.Windows.Forms.Button btnStartDate;
		private System.Windows.Forms.GroupBox gbxMode;
		private System.Windows.Forms.RadioButton rbtnMultiple;
		private System.Windows.Forms.RadioButton rbtnSingle;
		private System.Windows.Forms.Label lblMultipleCount;
		private System.Windows.Forms.Panel pnlNowLineColor;
		private System.Windows.Forms.Panel pnlMagnitudeColor;
		private System.Windows.Forms.CheckBox cbxValue;
		private System.Windows.Forms.Panel pnlPerihLineColor;
		private System.Windows.Forms.Label lblValue;
		private System.Windows.Forms.GroupBox gbxChartType;
		private System.Windows.Forms.RadioButton rbtnEarthDistance;
		private System.Windows.Forms.RadioButton rbtnSunDistance;
		private System.Windows.Forms.RadioButton rbtnMagnitude;
	}
}