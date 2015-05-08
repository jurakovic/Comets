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
            this.tbStartYear = new System.Windows.Forms.TextBox();
            this.tbIntervalMin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbIntervalHour = new System.Windows.Forms.TextBox();
            this.tbStartMonth = new System.Windows.Forms.TextBox();
            this.tbIntervalDay = new System.Windows.Forms.TextBox();
            this.tbStartDay = new System.Windows.Forms.TextBox();
            this.tbStartHour = new System.Windows.Forms.TextBox();
            this.tbStartMin = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbEndMin = new System.Windows.Forms.TextBox();
            this.tbEndYear = new System.Windows.Forms.TextBox();
            this.tbEndHour = new System.Windows.Forms.TextBox();
            this.tbEndMonth = new System.Windows.Forms.TextBox();
            this.tbEndDay = new System.Windows.Forms.TextBox();
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
            this.lblPeriod.Location = new System.Drawing.Point(9, 104);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(41, 13);
            this.lblPeriod.TabIndex = 300;
            this.lblPeriod.Text = "Period:";
            // 
            // lblPerihDist
            // 
            this.lblPerihDist.AutoSize = true;
            this.lblPerihDist.Location = new System.Drawing.Point(9, 77);
            this.lblPerihDist.Name = "lblPerihDist";
            this.lblPerihDist.Size = new System.Drawing.Size(100, 13);
            this.lblPerihDist.TabIndex = 299;
            this.lblPerihDist.Text = "Perihelion distance:";
            // 
            // lblPerihDate
            // 
            this.lblPerihDate.AutoSize = true;
            this.lblPerihDate.Location = new System.Drawing.Point(9, 50);
            this.lblPerihDate.Name = "lblPerihDate";
            this.lblPerihDate.Size = new System.Drawing.Size(82, 13);
            this.lblPerihDate.TabIndex = 298;
            this.lblPerihDate.Text = "Perihelion date:";
            // 
            // cbComet
            // 
            this.cbComet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComet.FormattingEnabled = true;
            this.cbComet.IntegralHeight = false;
            this.cbComet.Location = new System.Drawing.Point(12, 20);
            this.cbComet.MaxDropDownItems = 23;
            this.cbComet.Name = "cbComet";
            this.cbComet.Size = new System.Drawing.Size(299, 21);
            this.cbComet.TabIndex = 1;
            this.cbComet.SelectedIndexChanged += new System.EventHandler(this.cbComet_SelectedIndexChanged);
            // 
            // gbxTimestamp
            // 
            this.gbxTimestamp.Controls.Add(this.tbStartYear);
            this.gbxTimestamp.Controls.Add(this.tbIntervalMin);
            this.gbxTimestamp.Controls.Add(this.label3);
            this.gbxTimestamp.Controls.Add(this.tbIntervalHour);
            this.gbxTimestamp.Controls.Add(this.tbStartMonth);
            this.gbxTimestamp.Controls.Add(this.tbIntervalDay);
            this.gbxTimestamp.Controls.Add(this.tbStartDay);
            this.gbxTimestamp.Controls.Add(this.tbStartHour);
            this.gbxTimestamp.Controls.Add(this.tbStartMin);
            this.gbxTimestamp.Controls.Add(this.label20);
            this.gbxTimestamp.Controls.Add(this.label4);
            this.gbxTimestamp.Controls.Add(this.tbEndMin);
            this.gbxTimestamp.Controls.Add(this.tbEndYear);
            this.gbxTimestamp.Controls.Add(this.tbEndHour);
            this.gbxTimestamp.Controls.Add(this.tbEndMonth);
            this.gbxTimestamp.Controls.Add(this.tbEndDay);
            this.gbxTimestamp.Location = new System.Drawing.Point(350, 6);
            this.gbxTimestamp.Name = "gbxTimestamp";
            this.gbxTimestamp.Size = new System.Drawing.Size(436, 135);
            this.gbxTimestamp.TabIndex = 295;
            this.gbxTimestamp.TabStop = false;
            this.gbxTimestamp.Text = "Timespan";
            // 
            // tbStartYear
            // 
            this.tbStartYear.Location = new System.Drawing.Point(122, 20);
            this.tbStartYear.MaxLength = 4;
            this.tbStartYear.Name = "tbStartYear";
            this.tbStartYear.Size = new System.Drawing.Size(55, 21);
            this.tbStartYear.TabIndex = 294;
            this.tbStartYear.Text = "YYYY";
            this.tbStartYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbIntervalMin
            // 
            this.tbIntervalMin.Location = new System.Drawing.Point(366, 74);
            this.tbIntervalMin.MaxLength = 2;
            this.tbIntervalMin.Name = "tbIntervalMin";
            this.tbIntervalMin.Size = new System.Drawing.Size(55, 21);
            this.tbIntervalMin.TabIndex = 305;
            this.tbIntervalMin.Text = "MM";
            this.tbIntervalMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // tbIntervalHour
            // 
            this.tbIntervalHour.Location = new System.Drawing.Point(305, 74);
            this.tbIntervalHour.MaxLength = 2;
            this.tbIntervalHour.Name = "tbIntervalHour";
            this.tbIntervalHour.Size = new System.Drawing.Size(55, 21);
            this.tbIntervalHour.TabIndex = 304;
            this.tbIntervalHour.Text = "HH";
            this.tbIntervalHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbStartMonth
            // 
            this.tbStartMonth.Location = new System.Drawing.Point(183, 20);
            this.tbStartMonth.MaxLength = 2;
            this.tbStartMonth.Name = "tbStartMonth";
            this.tbStartMonth.Size = new System.Drawing.Size(55, 21);
            this.tbStartMonth.TabIndex = 293;
            this.tbStartMonth.Text = "MM";
            this.tbStartMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbIntervalDay
            // 
            this.tbIntervalDay.Location = new System.Drawing.Point(244, 74);
            this.tbIntervalDay.MaxLength = 2;
            this.tbIntervalDay.Name = "tbIntervalDay";
            this.tbIntervalDay.Size = new System.Drawing.Size(55, 21);
            this.tbIntervalDay.TabIndex = 303;
            this.tbIntervalDay.Text = "DD";
            this.tbIntervalDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbStartDay
            // 
            this.tbStartDay.Location = new System.Drawing.Point(244, 20);
            this.tbStartDay.MaxLength = 2;
            this.tbStartDay.Name = "tbStartDay";
            this.tbStartDay.Size = new System.Drawing.Size(55, 21);
            this.tbStartDay.TabIndex = 292;
            this.tbStartDay.Text = "DD";
            this.tbStartDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbStartHour
            // 
            this.tbStartHour.Location = new System.Drawing.Point(305, 20);
            this.tbStartHour.MaxLength = 2;
            this.tbStartHour.Name = "tbStartHour";
            this.tbStartHour.Size = new System.Drawing.Size(55, 21);
            this.tbStartHour.TabIndex = 295;
            this.tbStartHour.Text = "HH";
            this.tbStartHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbStartMin
            // 
            this.tbStartMin.Location = new System.Drawing.Point(366, 20);
            this.tbStartMin.MaxLength = 2;
            this.tbStartMin.Name = "tbStartMin";
            this.tbStartMin.Size = new System.Drawing.Size(55, 21);
            this.tbStartMin.TabIndex = 296;
            this.tbStartMin.Text = "MM";
            this.tbStartMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // tbEndMin
            // 
            this.tbEndMin.Location = new System.Drawing.Point(366, 47);
            this.tbEndMin.MaxLength = 2;
            this.tbEndMin.Name = "tbEndMin";
            this.tbEndMin.Size = new System.Drawing.Size(55, 21);
            this.tbEndMin.TabIndex = 302;
            this.tbEndMin.Text = "MM";
            this.tbEndMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbEndYear
            // 
            this.tbEndYear.Location = new System.Drawing.Point(122, 47);
            this.tbEndYear.MaxLength = 4;
            this.tbEndYear.Name = "tbEndYear";
            this.tbEndYear.Size = new System.Drawing.Size(55, 21);
            this.tbEndYear.TabIndex = 300;
            this.tbEndYear.Text = "YYYY";
            this.tbEndYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbEndHour
            // 
            this.tbEndHour.Location = new System.Drawing.Point(305, 47);
            this.tbEndHour.MaxLength = 2;
            this.tbEndHour.Name = "tbEndHour";
            this.tbEndHour.Size = new System.Drawing.Size(55, 21);
            this.tbEndHour.TabIndex = 301;
            this.tbEndHour.Text = "HH";
            this.tbEndHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbEndMonth
            // 
            this.tbEndMonth.Location = new System.Drawing.Point(183, 47);
            this.tbEndMonth.MaxLength = 2;
            this.tbEndMonth.Name = "tbEndMonth";
            this.tbEndMonth.Size = new System.Drawing.Size(55, 21);
            this.tbEndMonth.TabIndex = 299;
            this.tbEndMonth.Text = "MM";
            this.tbEndMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbEndDay
            // 
            this.tbEndDay.Location = new System.Drawing.Point(244, 47);
            this.tbEndDay.MaxLength = 2;
            this.tbEndDay.Name = "tbEndDay";
            this.tbEndDay.Size = new System.Drawing.Size(55, 21);
            this.tbEndDay.TabIndex = 297;
            this.tbEndDay.Text = "DD";
            this.tbEndDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.gbxOutputData.ResumeLayout(false);
            this.gbxOutputData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCalcEphem;
        private System.Windows.Forms.GroupBox gbxSelectComet;
        private System.Windows.Forms.GroupBox gbxTimestamp;
        private System.Windows.Forms.TextBox tbStartYear;
        private System.Windows.Forms.TextBox tbIntervalMin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbIntervalHour;
        private System.Windows.Forms.TextBox tbStartMonth;
        private System.Windows.Forms.TextBox tbIntervalDay;
        private System.Windows.Forms.TextBox tbStartDay;
        private System.Windows.Forms.TextBox tbStartHour;
        private System.Windows.Forms.TextBox tbStartMin;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbEndMin;
        private System.Windows.Forms.TextBox tbEndYear;
        private System.Windows.Forms.TextBox tbEndHour;
        private System.Windows.Forms.TextBox tbEndMonth;
        private System.Windows.Forms.TextBox tbEndDay;
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
    }
}