namespace Comets.Forms.Ephemeris
{
	partial class FormEphemerisSettings
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
			this.btnCalcEphem = new System.Windows.Forms.Button();
			this.gbxSelectComet = new System.Windows.Forms.GroupBox();
			this.lblPeriod = new System.Windows.Forms.Label();
			this.lblPerihDist = new System.Windows.Forms.Label();
			this.lblPerihDate = new System.Windows.Forms.Label();
			this.cbComet = new System.Windows.Forms.ComboBox();
			this.gbxTimestamp = new System.Windows.Forms.GroupBox();
			this.numMonthEnd = new System.Windows.Forms.NumericUpDown();
			this.numMonthStart = new System.Windows.Forms.NumericUpDown();
			this.btnTimespanIntervalDefault = new System.Windows.Forms.Button();
			this.btnTimespanEndDefault = new System.Windows.Forms.Button();
			this.btnTimespanStartDefault = new System.Windows.Forms.Button();
			this.numMinInterval = new System.Windows.Forms.NumericUpDown();
			this.numHourInterval = new System.Windows.Forms.NumericUpDown();
			this.numDayInterval = new System.Windows.Forms.NumericUpDown();
			this.numMinEnd = new System.Windows.Forms.NumericUpDown();
			this.numHourEnd = new System.Windows.Forms.NumericUpDown();
			this.numYearEnd = new System.Windows.Forms.NumericUpDown();
			this.numDayEnd = new System.Windows.Forms.NumericUpDown();
			this.numMinStart = new System.Windows.Forms.NumericUpDown();
			this.numHourStart = new System.Windows.Forms.NumericUpDown();
			this.numYearStart = new System.Windows.Forms.NumericUpDown();
			this.numDayStart = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.gbxOutputData = new System.Windows.Forms.GroupBox();
			this.radioLocalTime = new System.Windows.Forms.RadioButton();
			this.radioUnivTime = new System.Windows.Forms.RadioButton();
			this.chMag = new System.Windows.Forms.CheckBox();
			this.chGeoDist = new System.Windows.Forms.CheckBox();
			this.chHelioDist = new System.Windows.Forms.CheckBox();
			this.chElong = new System.Windows.Forms.CheckBox();
			this.chEcLat = new System.Windows.Forms.CheckBox();
			this.chEcLon = new System.Windows.Forms.CheckBox();
			this.chAz = new System.Windows.Forms.CheckBox();
			this.chAlt = new System.Windows.Forms.CheckBox();
			this.chDec = new System.Windows.Forms.CheckBox();
			this.chRA = new System.Windows.Forms.CheckBox();
			this.gbxSelectComet.SuspendLayout();
			this.gbxTimestamp.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numMonthEnd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numMonthStart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numMinInterval)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numHourInterval)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDayInterval)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numMinEnd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numHourEnd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numYearEnd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDayEnd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numMinStart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numHourStart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numYearStart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDayStart)).BeginInit();
			this.gbxOutputData.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCalcEphem
			// 
			this.btnCalcEphem.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.btnCalcEphem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnCalcEphem.Location = new System.Drawing.Point(304, 317);
			this.btnCalcEphem.Name = "btnCalcEphem";
			this.btnCalcEphem.Size = new System.Drawing.Size(190, 35);
			this.btnCalcEphem.TabIndex = 2;
			this.btnCalcEphem.Text = "Calculate";
			this.btnCalcEphem.UseVisualStyleBackColor = true;
			this.btnCalcEphem.Click += new System.EventHandler(this.btnCalcEphem_Click);
			// 
			// gbxSelectComet
			// 
			this.gbxSelectComet.Controls.Add(this.lblPeriod);
			this.gbxSelectComet.Controls.Add(this.lblPerihDist);
			this.gbxSelectComet.Controls.Add(this.lblPerihDate);
			this.gbxSelectComet.Controls.Add(this.cbComet);
			this.gbxSelectComet.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.gbxSelectComet.Location = new System.Drawing.Point(12, 6);
			this.gbxSelectComet.Name = "gbxSelectComet";
			this.gbxSelectComet.Size = new System.Drawing.Size(325, 135);
			this.gbxSelectComet.TabIndex = 294;
			this.gbxSelectComet.TabStop = false;
			this.gbxSelectComet.Text = "Select comet";
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
			this.cbComet.MaxDropDownItems = 21;
			this.cbComet.Name = "cbComet";
			this.cbComet.Size = new System.Drawing.Size(299, 22);
			this.cbComet.TabIndex = 1;
			this.cbComet.SelectedIndexChanged += new System.EventHandler(this.cbComet_SelectedIndexChanged);
			// 
			// gbxTimestamp
			// 
			this.gbxTimestamp.Controls.Add(this.numMonthEnd);
			this.gbxTimestamp.Controls.Add(this.numMonthStart);
			this.gbxTimestamp.Controls.Add(this.btnTimespanIntervalDefault);
			this.gbxTimestamp.Controls.Add(this.btnTimespanEndDefault);
			this.gbxTimestamp.Controls.Add(this.btnTimespanStartDefault);
			this.gbxTimestamp.Controls.Add(this.numMinInterval);
			this.gbxTimestamp.Controls.Add(this.numHourInterval);
			this.gbxTimestamp.Controls.Add(this.numDayInterval);
			this.gbxTimestamp.Controls.Add(this.numMinEnd);
			this.gbxTimestamp.Controls.Add(this.numHourEnd);
			this.gbxTimestamp.Controls.Add(this.numYearEnd);
			this.gbxTimestamp.Controls.Add(this.numDayEnd);
			this.gbxTimestamp.Controls.Add(this.numMinStart);
			this.gbxTimestamp.Controls.Add(this.numHourStart);
			this.gbxTimestamp.Controls.Add(this.numYearStart);
			this.gbxTimestamp.Controls.Add(this.numDayStart);
			this.gbxTimestamp.Controls.Add(this.label3);
			this.gbxTimestamp.Controls.Add(this.label20);
			this.gbxTimestamp.Controls.Add(this.label4);
			this.gbxTimestamp.Location = new System.Drawing.Point(350, 6);
			this.gbxTimestamp.Name = "gbxTimestamp";
			this.gbxTimestamp.Size = new System.Drawing.Size(436, 135);
			this.gbxTimestamp.TabIndex = 295;
			this.gbxTimestamp.TabStop = false;
			this.gbxTimestamp.Text = "Timespan (Local Time)";
			// 
			// numMonthEnd
			// 
			this.numMonthEnd.BackColor = System.Drawing.SystemColors.Window;
			this.numMonthEnd.Location = new System.Drawing.Point(211, 48);
			this.numMonthEnd.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
			this.numMonthEnd.Name = "numMonthEnd";
			this.numMonthEnd.Size = new System.Drawing.Size(42, 21);
			this.numMonthEnd.TabIndex = 324;
			this.numMonthEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numMonthEnd.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
			this.numMonthEnd.ValueChanged += new System.EventHandler(this.timespanEndCommon_ValueChanged);
			// 
			// numMonthStart
			// 
			this.numMonthStart.BackColor = System.Drawing.SystemColors.Window;
			this.numMonthStart.Location = new System.Drawing.Point(211, 21);
			this.numMonthStart.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
			this.numMonthStart.Name = "numMonthStart";
			this.numMonthStart.Size = new System.Drawing.Size(42, 21);
			this.numMonthStart.TabIndex = 323;
			this.numMonthStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numMonthStart.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
			this.numMonthStart.ValueChanged += new System.EventHandler(this.timespanStartCommon_ValueChanged);
			// 
			// btnTimespanIntervalDefault
			// 
			this.btnTimespanIntervalDefault.Location = new System.Drawing.Point(411, 77);
			this.btnTimespanIntervalDefault.Name = "btnTimespanIntervalDefault";
			this.btnTimespanIntervalDefault.Size = new System.Drawing.Size(16, 16);
			this.btnTimespanIntervalDefault.TabIndex = 322;
			this.btnTimespanIntervalDefault.UseVisualStyleBackColor = true;
			this.btnTimespanIntervalDefault.Click += new System.EventHandler(this.btnTimespanIntervalDefault_Click);
			// 
			// btnTimespanEndDefault
			// 
			this.btnTimespanEndDefault.Location = new System.Drawing.Point(411, 49);
			this.btnTimespanEndDefault.Name = "btnTimespanEndDefault";
			this.btnTimespanEndDefault.Size = new System.Drawing.Size(16, 16);
			this.btnTimespanEndDefault.TabIndex = 321;
			this.btnTimespanEndDefault.UseVisualStyleBackColor = true;
			this.btnTimespanEndDefault.Click += new System.EventHandler(this.btnTimespanEndDefault_Click);
			// 
			// btnTimespanStartDefault
			// 
			this.btnTimespanStartDefault.Location = new System.Drawing.Point(411, 22);
			this.btnTimespanStartDefault.Name = "btnTimespanStartDefault";
			this.btnTimespanStartDefault.Size = new System.Drawing.Size(16, 16);
			this.btnTimespanStartDefault.TabIndex = 320;
			this.btnTimespanStartDefault.UseVisualStyleBackColor = true;
			this.btnTimespanStartDefault.Click += new System.EventHandler(this.btnTimespanStartDefault_Click);
			// 
			// numMinInterval
			// 
			this.numMinInterval.BackColor = System.Drawing.SystemColors.Window;
			this.numMinInterval.Location = new System.Drawing.Point(363, 75);
			this.numMinInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.numMinInterval.Name = "numMinInterval";
			this.numMinInterval.Size = new System.Drawing.Size(42, 21);
			this.numMinInterval.TabIndex = 319;
			this.numMinInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numHourInterval
			// 
			this.numHourInterval.BackColor = System.Drawing.SystemColors.Window;
			this.numHourInterval.Location = new System.Drawing.Point(315, 75);
			this.numHourInterval.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
			this.numHourInterval.Name = "numHourInterval";
			this.numHourInterval.Size = new System.Drawing.Size(42, 21);
			this.numHourInterval.TabIndex = 318;
			this.numHourInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numDayInterval
			// 
			this.numDayInterval.BackColor = System.Drawing.SystemColors.Window;
			this.numDayInterval.Location = new System.Drawing.Point(259, 75);
			this.numDayInterval.Maximum = new decimal(new int[] {
            3652,
            0,
            0,
            0});
			this.numDayInterval.Name = "numDayInterval";
			this.numDayInterval.Size = new System.Drawing.Size(42, 21);
			this.numDayInterval.TabIndex = 317;
			this.numDayInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numDayInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numMinEnd
			// 
			this.numMinEnd.BackColor = System.Drawing.SystemColors.Window;
			this.numMinEnd.Location = new System.Drawing.Point(363, 48);
			this.numMinEnd.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.numMinEnd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.numMinEnd.Name = "numMinEnd";
			this.numMinEnd.Size = new System.Drawing.Size(42, 21);
			this.numMinEnd.TabIndex = 316;
			this.numMinEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numMinEnd.Value = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.numMinEnd.ValueChanged += new System.EventHandler(this.timespanEndCommon_ValueChanged);
			// 
			// numHourEnd
			// 
			this.numHourEnd.BackColor = System.Drawing.SystemColors.Window;
			this.numHourEnd.Location = new System.Drawing.Point(315, 48);
			this.numHourEnd.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
			this.numHourEnd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.numHourEnd.Name = "numHourEnd";
			this.numHourEnd.Size = new System.Drawing.Size(42, 21);
			this.numHourEnd.TabIndex = 315;
			this.numHourEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numHourEnd.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
			this.numHourEnd.ValueChanged += new System.EventHandler(this.timespanEndCommon_ValueChanged);
			// 
			// numYearEnd
			// 
			this.numYearEnd.Location = new System.Drawing.Point(155, 48);
			this.numYearEnd.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
			this.numYearEnd.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numYearEnd.Name = "numYearEnd";
			this.numYearEnd.Size = new System.Drawing.Size(50, 21);
			this.numYearEnd.TabIndex = 313;
			this.numYearEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numYearEnd.Value = new decimal(new int[] {
            2015,
            0,
            0,
            0});
			this.numYearEnd.ValueChanged += new System.EventHandler(this.timespanEndCommon_ValueChanged);
			// 
			// numDayEnd
			// 
			this.numDayEnd.BackColor = System.Drawing.SystemColors.Window;
			this.numDayEnd.Location = new System.Drawing.Point(259, 48);
			this.numDayEnd.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.numDayEnd.Name = "numDayEnd";
			this.numDayEnd.Size = new System.Drawing.Size(42, 21);
			this.numDayEnd.TabIndex = 312;
			this.numDayEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numDayEnd.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			this.numDayEnd.ValueChanged += new System.EventHandler(this.timespanEndCommon_ValueChanged);
			// 
			// numMinStart
			// 
			this.numMinStart.BackColor = System.Drawing.SystemColors.Window;
			this.numMinStart.Location = new System.Drawing.Point(363, 21);
			this.numMinStart.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.numMinStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.numMinStart.Name = "numMinStart";
			this.numMinStart.Size = new System.Drawing.Size(42, 21);
			this.numMinStart.TabIndex = 311;
			this.numMinStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numMinStart.Value = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.numMinStart.ValueChanged += new System.EventHandler(this.timespanStartCommon_ValueChanged);
			// 
			// numHourStart
			// 
			this.numHourStart.BackColor = System.Drawing.SystemColors.Window;
			this.numHourStart.Location = new System.Drawing.Point(315, 21);
			this.numHourStart.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
			this.numHourStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.numHourStart.Name = "numHourStart";
			this.numHourStart.Size = new System.Drawing.Size(42, 21);
			this.numHourStart.TabIndex = 310;
			this.numHourStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numHourStart.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
			this.numHourStart.ValueChanged += new System.EventHandler(this.timespanStartCommon_ValueChanged);
			// 
			// numYearStart
			// 
			this.numYearStart.Location = new System.Drawing.Point(155, 21);
			this.numYearStart.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
			this.numYearStart.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numYearStart.Name = "numYearStart";
			this.numYearStart.Size = new System.Drawing.Size(50, 21);
			this.numYearStart.TabIndex = 308;
			this.numYearStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numYearStart.Value = new decimal(new int[] {
            2015,
            0,
            0,
            0});
			this.numYearStart.ValueChanged += new System.EventHandler(this.timespanStartCommon_ValueChanged);
			// 
			// numDayStart
			// 
			this.numDayStart.BackColor = System.Drawing.SystemColors.Window;
			this.numDayStart.Location = new System.Drawing.Point(259, 21);
			this.numDayStart.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.numDayStart.Name = "numDayStart";
			this.numDayStart.Size = new System.Drawing.Size(42, 21);
			this.numDayStart.TabIndex = 307;
			this.numDayStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numDayStart.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			this.numDayStart.ValueChanged += new System.EventHandler(this.timespanStartCommon_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 23);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 291;
			this.label3.Text = "Start:";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(15, 77);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(49, 13);
			this.label20.TabIndex = 306;
			this.label20.Text = "Interval:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 50);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 13);
			this.label4.TabIndex = 298;
			this.label4.Text = "End:";
			// 
			// gbxOutputData
			// 
			this.gbxOutputData.Controls.Add(this.radioLocalTime);
			this.gbxOutputData.Controls.Add(this.radioUnivTime);
			this.gbxOutputData.Controls.Add(this.chMag);
			this.gbxOutputData.Controls.Add(this.chGeoDist);
			this.gbxOutputData.Controls.Add(this.chHelioDist);
			this.gbxOutputData.Controls.Add(this.chElong);
			this.gbxOutputData.Controls.Add(this.chEcLat);
			this.gbxOutputData.Controls.Add(this.chEcLon);
			this.gbxOutputData.Controls.Add(this.chAz);
			this.gbxOutputData.Controls.Add(this.chAlt);
			this.gbxOutputData.Controls.Add(this.chDec);
			this.gbxOutputData.Controls.Add(this.chRA);
			this.gbxOutputData.Location = new System.Drawing.Point(12, 147);
			this.gbxOutputData.Name = "gbxOutputData";
			this.gbxOutputData.Size = new System.Drawing.Size(774, 157);
			this.gbxOutputData.TabIndex = 297;
			this.gbxOutputData.TabStop = false;
			this.gbxOutputData.Text = "Output data";
			// 
			// radioLocalTime
			// 
			this.radioLocalTime.AutoSize = true;
			this.radioLocalTime.Checked = true;
			this.radioLocalTime.Location = new System.Drawing.Point(12, 22);
			this.radioLocalTime.Name = "radioLocalTime";
			this.radioLocalTime.Size = new System.Drawing.Size(74, 17);
			this.radioLocalTime.TabIndex = 27;
			this.radioLocalTime.TabStop = true;
			this.radioLocalTime.Text = "Local Time";
			this.radioLocalTime.UseVisualStyleBackColor = true;
			// 
			// radioUnivTime
			// 
			this.radioUnivTime.AutoSize = true;
			this.radioUnivTime.Location = new System.Drawing.Point(12, 47);
			this.radioUnivTime.Name = "radioUnivTime";
			this.radioUnivTime.Size = new System.Drawing.Size(94, 17);
			this.radioUnivTime.TabIndex = 26;
			this.radioUnivTime.Text = "Universal Time";
			this.radioUnivTime.UseVisualStyleBackColor = true;
			// 
			// chMag
			// 
			this.chMag.AutoSize = true;
			this.chMag.Checked = true;
			this.chMag.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chMag.Location = new System.Drawing.Point(540, 94);
			this.chMag.Name = "chMag";
			this.chMag.Size = new System.Drawing.Size(76, 17);
			this.chMag.TabIndex = 25;
			this.chMag.Text = "Magnitude";
			this.chMag.UseVisualStyleBackColor = true;
			// 
			// chGeoDist
			// 
			this.chGeoDist.AutoSize = true;
			this.chGeoDist.Checked = true;
			this.chGeoDist.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chGeoDist.Location = new System.Drawing.Point(540, 47);
			this.chGeoDist.Name = "chGeoDist";
			this.chGeoDist.Size = new System.Drawing.Size(120, 17);
			this.chGeoDist.TabIndex = 24;
			this.chGeoDist.Text = "Geocentric distance";
			this.chGeoDist.UseVisualStyleBackColor = true;
			// 
			// chHelioDist
			// 
			this.chHelioDist.AutoSize = true;
			this.chHelioDist.Checked = true;
			this.chHelioDist.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chHelioDist.Location = new System.Drawing.Point(540, 22);
			this.chHelioDist.Name = "chHelioDist";
			this.chHelioDist.Size = new System.Drawing.Size(124, 17);
			this.chHelioDist.TabIndex = 23;
			this.chHelioDist.Text = "Heliocentric distance";
			this.chHelioDist.UseVisualStyleBackColor = true;
			// 
			// chElong
			// 
			this.chElong.AutoSize = true;
			this.chElong.Checked = true;
			this.chElong.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chElong.Location = new System.Drawing.Point(360, 94);
			this.chElong.Name = "chElong";
			this.chElong.Size = new System.Drawing.Size(76, 17);
			this.chElong.TabIndex = 22;
			this.chElong.Text = "Elongation";
			this.chElong.UseVisualStyleBackColor = true;
			// 
			// chEcLat
			// 
			this.chEcLat.AutoSize = true;
			this.chEcLat.Location = new System.Drawing.Point(360, 47);
			this.chEcLat.Name = "chEcLat";
			this.chEcLat.Size = new System.Drawing.Size(100, 17);
			this.chEcLat.TabIndex = 21;
			this.chEcLat.Text = "Ecliptic Latitude";
			this.chEcLat.UseVisualStyleBackColor = true;
			// 
			// chEcLon
			// 
			this.chEcLon.AutoSize = true;
			this.chEcLon.Location = new System.Drawing.Point(360, 22);
			this.chEcLon.Name = "chEcLon";
			this.chEcLon.Size = new System.Drawing.Size(108, 17);
			this.chEcLon.TabIndex = 20;
			this.chEcLon.Text = "Ecliptic Longitude";
			this.chEcLon.UseVisualStyleBackColor = true;
			// 
			// chAz
			// 
			this.chAz.AutoSize = true;
			this.chAz.Checked = true;
			this.chAz.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chAz.Location = new System.Drawing.Point(177, 118);
			this.chAz.Name = "chAz";
			this.chAz.Size = new System.Drawing.Size(87, 17);
			this.chAz.TabIndex = 19;
			this.chAz.Text = "Azimuth (Az)";
			this.chAz.UseVisualStyleBackColor = true;
			// 
			// chAlt
			// 
			this.chAlt.AutoSize = true;
			this.chAlt.Checked = true;
			this.chAlt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chAlt.Location = new System.Drawing.Point(177, 94);
			this.chAlt.Name = "chAlt";
			this.chAlt.Size = new System.Drawing.Size(87, 17);
			this.chAlt.TabIndex = 18;
			this.chAlt.Text = "Altitude (Alt)";
			this.chAlt.UseVisualStyleBackColor = true;
			// 
			// chDec
			// 
			this.chDec.AutoSize = true;
			this.chDec.Checked = true;
			this.chDec.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chDec.Location = new System.Drawing.Point(177, 46);
			this.chDec.Name = "chDec";
			this.chDec.Size = new System.Drawing.Size(107, 17);
			this.chDec.TabIndex = 17;
			this.chDec.Text = "Declination (Dec)";
			this.chDec.UseVisualStyleBackColor = true;
			// 
			// chRA
			// 
			this.chRA.AutoSize = true;
			this.chRA.Checked = true;
			this.chRA.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chRA.Location = new System.Drawing.Point(177, 22);
			this.chRA.Name = "chRA";
			this.chRA.Size = new System.Drawing.Size(126, 17);
			this.chRA.TabIndex = 16;
			this.chRA.Text = "Right ascension (RA)";
			this.chRA.UseVisualStyleBackColor = true;
			// 
			// FormEphemerisSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(798, 367);
			this.Controls.Add(this.gbxOutputData);
			this.Controls.Add(this.gbxTimestamp);
			this.Controls.Add(this.gbxSelectComet);
			this.Controls.Add(this.btnCalcEphem);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEphemerisSettings";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Ephemeris settings";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEphemerisSettings_FormClosing);
			this.Load += new System.EventHandler(this.FormEphemerisSettings_Load);
			this.gbxSelectComet.ResumeLayout(false);
			this.gbxSelectComet.PerformLayout();
			this.gbxTimestamp.ResumeLayout(false);
			this.gbxTimestamp.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numMonthEnd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numMonthStart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numMinInterval)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numHourInterval)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDayInterval)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numMinEnd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numHourEnd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numYearEnd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDayEnd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numMinStart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numHourStart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numYearStart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDayStart)).EndInit();
			this.gbxOutputData.ResumeLayout(false);
			this.gbxOutputData.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnCalcEphem;
		private System.Windows.Forms.GroupBox gbxSelectComet;
		private System.Windows.Forms.GroupBox gbxTimestamp;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox gbxOutputData;
		private System.Windows.Forms.Label lblPeriod;
		private System.Windows.Forms.Label lblPerihDist;
		private System.Windows.Forms.Label lblPerihDate;
		private System.Windows.Forms.ComboBox cbComet;
		private System.Windows.Forms.RadioButton radioLocalTime;
		private System.Windows.Forms.RadioButton radioUnivTime;
		private System.Windows.Forms.CheckBox chMag;
		private System.Windows.Forms.CheckBox chGeoDist;
		private System.Windows.Forms.CheckBox chHelioDist;
		private System.Windows.Forms.CheckBox chElong;
		private System.Windows.Forms.CheckBox chEcLat;
		private System.Windows.Forms.CheckBox chEcLon;
		private System.Windows.Forms.CheckBox chAz;
		private System.Windows.Forms.CheckBox chAlt;
		private System.Windows.Forms.CheckBox chDec;
		private System.Windows.Forms.CheckBox chRA;
		private System.Windows.Forms.NumericUpDown numMinStart;
		private System.Windows.Forms.NumericUpDown numHourStart;
		private System.Windows.Forms.NumericUpDown numYearStart;
		private System.Windows.Forms.NumericUpDown numDayStart;
		private System.Windows.Forms.NumericUpDown numMinEnd;
		private System.Windows.Forms.NumericUpDown numHourEnd;
		private System.Windows.Forms.NumericUpDown numYearEnd;
		private System.Windows.Forms.NumericUpDown numDayEnd;
		private System.Windows.Forms.NumericUpDown numMinInterval;
		private System.Windows.Forms.NumericUpDown numHourInterval;
		private System.Windows.Forms.NumericUpDown numDayInterval;
		private System.Windows.Forms.Button btnTimespanIntervalDefault;
		private System.Windows.Forms.Button btnTimespanEndDefault;
		private System.Windows.Forms.Button btnTimespanStartDefault;
		private System.Windows.Forms.NumericUpDown numMonthEnd;
		private System.Windows.Forms.NumericUpDown numMonthStart;
	}
}