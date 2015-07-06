namespace Comets.Application.ModulEphemeris
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
			this.btnFilter = new System.Windows.Forms.Button();
			this.lblPeriod = new System.Windows.Forms.Label();
			this.lblPerihDist = new System.Windows.Forms.Label();
			this.lblPerihDate = new System.Windows.Forms.Label();
			this.cbComet = new System.Windows.Forms.ComboBox();
			this.gbxTimestamp = new System.Windows.Forms.GroupBox();
			this.btnEndDate = new System.Windows.Forms.Button();
			this.btnStartDate = new System.Windows.Forms.Button();
			this.txtMinInterval = new System.Windows.Forms.TextBox();
			this.txtHourInterval = new System.Windows.Forms.TextBox();
			this.txtDayInterval = new System.Windows.Forms.TextBox();
			this.btnTimespanIntervalDefault = new System.Windows.Forms.Button();
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.gbxSelectComet.SuspendLayout();
			this.gbxTimestamp.SuspendLayout();
			this.gbxOutputData.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCalcEphem
			// 
			this.btnCalcEphem.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.btnCalcEphem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnCalcEphem.Location = new System.Drawing.Point(326, 316);
			this.btnCalcEphem.Name = "btnCalcEphem";
			this.btnCalcEphem.Size = new System.Drawing.Size(100, 24);
			this.btnCalcEphem.TabIndex = 4;
			this.btnCalcEphem.Text = "Calculate";
			this.btnCalcEphem.UseVisualStyleBackColor = true;
			this.btnCalcEphem.Click += new System.EventHandler(this.btnCalcEphem_Click);
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
			this.cbComet.MaxDropDownItems = 21;
			this.cbComet.Name = "cbComet";
			this.cbComet.Size = new System.Drawing.Size(226, 22);
			this.cbComet.TabIndex = 0;
			this.cbComet.SelectedIndexChanged += new System.EventHandler(this.cbComet_SelectedIndexChanged);
			// 
			// gbxTimestamp
			// 
			this.gbxTimestamp.Controls.Add(this.btnEndDate);
			this.gbxTimestamp.Controls.Add(this.btnStartDate);
			this.gbxTimestamp.Controls.Add(this.txtMinInterval);
			this.gbxTimestamp.Controls.Add(this.txtHourInterval);
			this.gbxTimestamp.Controls.Add(this.txtDayInterval);
			this.gbxTimestamp.Controls.Add(this.btnTimespanIntervalDefault);
			this.gbxTimestamp.Controls.Add(this.label3);
			this.gbxTimestamp.Controls.Add(this.label20);
			this.gbxTimestamp.Controls.Add(this.label4);
			this.gbxTimestamp.Location = new System.Drawing.Point(350, 6);
			this.gbxTimestamp.Name = "gbxTimestamp";
			this.gbxTimestamp.Size = new System.Drawing.Size(281, 135);
			this.gbxTimestamp.TabIndex = 1;
			this.gbxTimestamp.TabStop = false;
			this.gbxTimestamp.Text = "Timespan (Local Time)";
			// 
			// btnEndDate
			// 
			this.btnEndDate.Location = new System.Drawing.Point(87, 49);
			this.btnEndDate.Name = "btnEndDate";
			this.btnEndDate.Size = new System.Drawing.Size(172, 23);
			this.btnEndDate.TabIndex = 308;
			this.btnEndDate.Text = "dd.MM.yyyy HH:mm:ss";
			this.btnEndDate.UseVisualStyleBackColor = true;
			this.btnEndDate.Click += new System.EventHandler(this.btnEndDate_Click);
			// 
			// btnStartDate
			// 
			this.btnStartDate.Location = new System.Drawing.Point(87, 20);
			this.btnStartDate.Name = "btnStartDate";
			this.btnStartDate.Size = new System.Drawing.Size(172, 23);
			this.btnStartDate.TabIndex = 307;
			this.btnStartDate.Text = "dd.MM.yyyy HH:mm:ss";
			this.btnStartDate.UseVisualStyleBackColor = true;
			this.btnStartDate.Click += new System.EventHandler(this.btnStartDate_Click);
			// 
			// txtMinInterval
			// 
			this.txtMinInterval.Location = new System.Drawing.Point(191, 78);
			this.txtMinInterval.Name = "txtMinInterval";
			this.txtMinInterval.Size = new System.Drawing.Size(42, 21);
			this.txtMinInterval.TabIndex = 14;
			this.txtMinInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMinInterval.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIntervalCommon_KeyDown);
			this.txtMinInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIntervalCommon_KeyPress);
			// 
			// txtHourInterval
			// 
			this.txtHourInterval.Location = new System.Drawing.Point(143, 78);
			this.txtHourInterval.Name = "txtHourInterval";
			this.txtHourInterval.Size = new System.Drawing.Size(42, 21);
			this.txtHourInterval.TabIndex = 13;
			this.txtHourInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtHourInterval.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIntervalCommon_KeyDown);
			this.txtHourInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIntervalCommon_KeyPress);
			// 
			// txtDayInterval
			// 
			this.txtDayInterval.Location = new System.Drawing.Point(87, 78);
			this.txtDayInterval.Name = "txtDayInterval";
			this.txtDayInterval.Size = new System.Drawing.Size(42, 21);
			this.txtDayInterval.TabIndex = 12;
			this.txtDayInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtDayInterval.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIntervalCommon_KeyDown);
			this.txtDayInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIntervalCommon_KeyPress);
			// 
			// btnTimespanIntervalDefault
			// 
			this.btnTimespanIntervalDefault.Location = new System.Drawing.Point(239, 81);
			this.btnTimespanIntervalDefault.Name = "btnTimespanIntervalDefault";
			this.btnTimespanIntervalDefault.Size = new System.Drawing.Size(16, 16);
			this.btnTimespanIntervalDefault.TabIndex = 15;
			this.btnTimespanIntervalDefault.UseVisualStyleBackColor = true;
			this.btnTimespanIntervalDefault.Click += new System.EventHandler(this.btnTimespanIntervalDefault_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 291;
			this.label3.Text = "Start:";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(15, 81);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(49, 13);
			this.label20.TabIndex = 306;
			this.label20.Text = "Interval:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 54);
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
			this.gbxOutputData.Size = new System.Drawing.Size(619, 149);
			this.gbxOutputData.TabIndex = 2;
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
			this.radioLocalTime.TabIndex = 0;
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
			this.radioUnivTime.TabIndex = 1;
			this.radioUnivTime.Text = "Universal Time";
			this.radioUnivTime.UseVisualStyleBackColor = true;
			// 
			// chMag
			// 
			this.chMag.AutoSize = true;
			this.chMag.Checked = true;
			this.chMag.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chMag.Location = new System.Drawing.Point(465, 89);
			this.chMag.Name = "chMag";
			this.chMag.Size = new System.Drawing.Size(76, 17);
			this.chMag.TabIndex = 11;
			this.chMag.Text = "Magnitude";
			this.chMag.UseVisualStyleBackColor = true;
			// 
			// chGeoDist
			// 
			this.chGeoDist.AutoSize = true;
			this.chGeoDist.Checked = true;
			this.chGeoDist.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chGeoDist.Location = new System.Drawing.Point(465, 47);
			this.chGeoDist.Name = "chGeoDist";
			this.chGeoDist.Size = new System.Drawing.Size(120, 17);
			this.chGeoDist.TabIndex = 10;
			this.chGeoDist.Text = "Geocentric distance";
			this.chGeoDist.UseVisualStyleBackColor = true;
			// 
			// chHelioDist
			// 
			this.chHelioDist.AutoSize = true;
			this.chHelioDist.Checked = true;
			this.chHelioDist.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chHelioDist.Location = new System.Drawing.Point(465, 22);
			this.chHelioDist.Name = "chHelioDist";
			this.chHelioDist.Size = new System.Drawing.Size(124, 17);
			this.chHelioDist.TabIndex = 9;
			this.chHelioDist.Text = "Heliocentric distance";
			this.chHelioDist.UseVisualStyleBackColor = true;
			// 
			// chElong
			// 
			this.chElong.AutoSize = true;
			this.chElong.Checked = true;
			this.chElong.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chElong.Location = new System.Drawing.Point(310, 89);
			this.chElong.Name = "chElong";
			this.chElong.Size = new System.Drawing.Size(76, 17);
			this.chElong.TabIndex = 8;
			this.chElong.Text = "Elongation";
			this.chElong.UseVisualStyleBackColor = true;
			// 
			// chEcLat
			// 
			this.chEcLat.AutoSize = true;
			this.chEcLat.Location = new System.Drawing.Point(310, 47);
			this.chEcLat.Name = "chEcLat";
			this.chEcLat.Size = new System.Drawing.Size(100, 17);
			this.chEcLat.TabIndex = 7;
			this.chEcLat.Text = "Ecliptic Latitude";
			this.chEcLat.UseVisualStyleBackColor = true;
			// 
			// chEcLon
			// 
			this.chEcLon.AutoSize = true;
			this.chEcLon.Location = new System.Drawing.Point(310, 22);
			this.chEcLon.Name = "chEcLon";
			this.chEcLon.Size = new System.Drawing.Size(108, 17);
			this.chEcLon.TabIndex = 6;
			this.chEcLon.Text = "Ecliptic Longitude";
			this.chEcLon.UseVisualStyleBackColor = true;
			// 
			// chAz
			// 
			this.chAz.AutoSize = true;
			this.chAz.Checked = true;
			this.chAz.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chAz.Location = new System.Drawing.Point(152, 113);
			this.chAz.Name = "chAz";
			this.chAz.Size = new System.Drawing.Size(87, 17);
			this.chAz.TabIndex = 5;
			this.chAz.Text = "Azimuth (Az)";
			this.chAz.UseVisualStyleBackColor = true;
			// 
			// chAlt
			// 
			this.chAlt.AutoSize = true;
			this.chAlt.Checked = true;
			this.chAlt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chAlt.Location = new System.Drawing.Point(152, 89);
			this.chAlt.Name = "chAlt";
			this.chAlt.Size = new System.Drawing.Size(87, 17);
			this.chAlt.TabIndex = 4;
			this.chAlt.Text = "Altitude (Alt)";
			this.chAlt.UseVisualStyleBackColor = true;
			// 
			// chDec
			// 
			this.chDec.AutoSize = true;
			this.chDec.Checked = true;
			this.chDec.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chDec.Location = new System.Drawing.Point(152, 46);
			this.chDec.Name = "chDec";
			this.chDec.Size = new System.Drawing.Size(107, 17);
			this.chDec.TabIndex = 3;
			this.chDec.Text = "Declination (Dec)";
			this.chDec.UseVisualStyleBackColor = true;
			// 
			// chRA
			// 
			this.chRA.AutoSize = true;
			this.chRA.Checked = true;
			this.chRA.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chRA.Location = new System.Drawing.Point(152, 22);
			this.chRA.Name = "chRA";
			this.chRA.Size = new System.Drawing.Size(126, 17);
			this.chRA.TabIndex = 2;
			this.chRA.Text = "Right ascension (RA)";
			this.chRA.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(220, 316);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 24);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// FormEphemerisSettings
			// 
			this.AcceptButton = this.btnCalcEphem;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(644, 359);
			this.Controls.Add(this.gbxOutputData);
			this.Controls.Add(this.gbxTimestamp);
			this.Controls.Add(this.gbxSelectComet);
			this.Controls.Add(this.btnCalcEphem);
			this.Controls.Add(this.btnCancel);
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
		private System.Windows.Forms.Button btnFilter;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnStartDate;
		private System.Windows.Forms.TextBox txtMinInterval;
		private System.Windows.Forms.TextBox txtHourInterval;
		private System.Windows.Forms.TextBox txtDayInterval;
		private System.Windows.Forms.Button btnTimespanIntervalDefault;
		private System.Windows.Forms.Button btnEndDate;
	}
}