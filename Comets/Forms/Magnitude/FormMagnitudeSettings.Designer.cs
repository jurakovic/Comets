﻿namespace Comets.Forms.Magnitude
{
    partial class FormMagnitudeSettings
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
            this.lblPeriod = new System.Windows.Forms.Label();
            this.lblPerihDist = new System.Windows.Forms.Label();
            this.lblPerihDate = new System.Windows.Forms.Label();
            this.cbComet = new System.Windows.Forms.ComboBox();
            this.gbxTimestamp = new System.Windows.Forms.GroupBox();
            this.pnlRangeDaysFromT = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStartDaysFromT = new System.Windows.Forms.TextBox();
            this.tbEndDaysFromT = new System.Windows.Forms.TextBox();
            this.pnlRangeDate = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbStartYear = new System.Windows.Forms.TextBox();
            this.tbStartMonth = new System.Windows.Forms.TextBox();
            this.tbStartDay = new System.Windows.Forms.TextBox();
            this.tbEndYear = new System.Windows.Forms.TextBox();
            this.tbEndMonth = new System.Windows.Forms.TextBox();
            this.tbEndDay = new System.Windows.Forms.TextBox();
            this.rbRangeDaysFromT = new System.Windows.Forms.RadioButton();
            this.rbRangeDate = new System.Windows.Forms.RadioButton();
            this.gbxDateFormat = new System.Windows.Forms.GroupBox();
            this.rbDaysFromT = new System.Windows.Forms.RadioButton();
            this.rbJulianDay2 = new System.Windows.Forms.RadioButton();
            this.rbJulianDay = new System.Windows.Forms.RadioButton();
            this.rbDate = new System.Windows.Forms.RadioButton();
            this.gbxSelectComet.SuspendLayout();
            this.gbxTimestamp.SuspendLayout();
            this.pnlRangeDaysFromT.SuspendLayout();
            this.pnlRangeDate.SuspendLayout();
            this.gbxDateFormat.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPlotGraph
            // 
            this.btnPlotGraph.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnPlotGraph.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPlotGraph.Location = new System.Drawing.Point(167, 243);
            this.btnPlotGraph.Name = "btnPlotGraph";
            this.btnPlotGraph.Size = new System.Drawing.Size(190, 35);
            this.btnPlotGraph.TabIndex = 2;
            this.btnPlotGraph.Text = "PlotGraph";
            this.btnPlotGraph.UseVisualStyleBackColor = true;
            this.btnPlotGraph.Click += new System.EventHandler(this.btnPlotGraph_Click);
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
            // 
            // gbxTimestamp
            // 
            this.gbxTimestamp.Controls.Add(this.pnlRangeDaysFromT);
            this.gbxTimestamp.Controls.Add(this.pnlRangeDate);
            this.gbxTimestamp.Controls.Add(this.rbRangeDaysFromT);
            this.gbxTimestamp.Controls.Add(this.rbRangeDate);
            this.gbxTimestamp.Location = new System.Drawing.Point(12, 147);
            this.gbxTimestamp.Name = "gbxTimestamp";
            this.gbxTimestamp.Size = new System.Drawing.Size(499, 83);
            this.gbxTimestamp.TabIndex = 295;
            this.gbxTimestamp.TabStop = false;
            this.gbxTimestamp.Text = "Range";
            // 
            // pnlRangeDaysFromT
            // 
            this.pnlRangeDaysFromT.Controls.Add(this.label4);
            this.pnlRangeDaysFromT.Controls.Add(this.label3);
            this.pnlRangeDaysFromT.Controls.Add(this.label2);
            this.pnlRangeDaysFromT.Controls.Add(this.tbStartDaysFromT);
            this.pnlRangeDaysFromT.Controls.Add(this.tbEndDaysFromT);
            this.pnlRangeDaysFromT.Enabled = false;
            this.pnlRangeDaysFromT.Location = new System.Drawing.Point(149, 43);
            this.pnlRangeDaysFromT.Name = "pnlRangeDaysFromT";
            this.pnlRangeDaysFromT.Size = new System.Drawing.Size(337, 27);
            this.pnlRangeDaysFromT.TabIndex = 312;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(209, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 319;
            this.label4.Text = "days";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 318;
            this.label3.Text = "days";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 317;
            this.label2.Text = "-";
            // 
            // tbStartDaysFromT
            // 
            this.tbStartDaysFromT.Location = new System.Drawing.Point(3, 3);
            this.tbStartDaysFromT.MaxLength = 4;
            this.tbStartDaysFromT.Name = "tbStartDaysFromT";
            this.tbStartDaysFromT.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbStartDaysFromT.Size = new System.Drawing.Size(55, 21);
            this.tbStartDaysFromT.TabIndex = 313;
            this.tbStartDaysFromT.Text = "DDDD";
            // 
            // tbEndDaysFromT
            // 
            this.tbEndDaysFromT.Location = new System.Drawing.Point(148, 3);
            this.tbEndDaysFromT.MaxLength = 4;
            this.tbEndDaysFromT.Name = "tbEndDaysFromT";
            this.tbEndDaysFromT.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbEndDaysFromT.Size = new System.Drawing.Size(55, 21);
            this.tbEndDaysFromT.TabIndex = 316;
            this.tbEndDaysFromT.Text = "DDDD";
            // 
            // pnlRangeDate
            // 
            this.pnlRangeDate.Controls.Add(this.label1);
            this.pnlRangeDate.Controls.Add(this.tbStartYear);
            this.pnlRangeDate.Controls.Add(this.tbStartMonth);
            this.pnlRangeDate.Controls.Add(this.tbStartDay);
            this.pnlRangeDate.Controls.Add(this.tbEndYear);
            this.pnlRangeDate.Controls.Add(this.tbEndMonth);
            this.pnlRangeDate.Controls.Add(this.tbEndDay);
            this.pnlRangeDate.Location = new System.Drawing.Point(149, 13);
            this.pnlRangeDate.Name = "pnlRangeDate";
            this.pnlRangeDate.Size = new System.Drawing.Size(337, 27);
            this.pnlRangeDate.TabIndex = 311;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 310;
            this.label1.Text = "-";
            // 
            // tbStartYear
            // 
            this.tbStartYear.Location = new System.Drawing.Point(3, 3);
            this.tbStartYear.MaxLength = 4;
            this.tbStartYear.Name = "tbStartYear";
            this.tbStartYear.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbStartYear.Size = new System.Drawing.Size(55, 21);
            this.tbStartYear.TabIndex = 306;
            this.tbStartYear.Text = "YYYY";
            // 
            // tbStartMonth
            // 
            this.tbStartMonth.Location = new System.Drawing.Point(64, 3);
            this.tbStartMonth.MaxLength = 2;
            this.tbStartMonth.Name = "tbStartMonth";
            this.tbStartMonth.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbStartMonth.Size = new System.Drawing.Size(25, 21);
            this.tbStartMonth.TabIndex = 305;
            this.tbStartMonth.Text = "MM";
            // 
            // tbStartDay
            // 
            this.tbStartDay.Location = new System.Drawing.Point(95, 3);
            this.tbStartDay.MaxLength = 2;
            this.tbStartDay.Name = "tbStartDay";
            this.tbStartDay.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbStartDay.Size = new System.Drawing.Size(25, 21);
            this.tbStartDay.TabIndex = 304;
            this.tbStartDay.Text = "DD";
            // 
            // tbEndYear
            // 
            this.tbEndYear.Location = new System.Drawing.Point(148, 3);
            this.tbEndYear.MaxLength = 4;
            this.tbEndYear.Name = "tbEndYear";
            this.tbEndYear.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbEndYear.Size = new System.Drawing.Size(55, 21);
            this.tbEndYear.TabIndex = 309;
            this.tbEndYear.Text = "YYYY";
            // 
            // tbEndMonth
            // 
            this.tbEndMonth.Location = new System.Drawing.Point(209, 3);
            this.tbEndMonth.MaxLength = 2;
            this.tbEndMonth.Name = "tbEndMonth";
            this.tbEndMonth.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbEndMonth.Size = new System.Drawing.Size(25, 21);
            this.tbEndMonth.TabIndex = 308;
            this.tbEndMonth.Text = "MM";
            // 
            // tbEndDay
            // 
            this.tbEndDay.Location = new System.Drawing.Point(240, 3);
            this.tbEndDay.MaxLength = 2;
            this.tbEndDay.Name = "tbEndDay";
            this.tbEndDay.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbEndDay.Size = new System.Drawing.Size(25, 21);
            this.tbEndDay.TabIndex = 307;
            this.tbEndDay.Text = "DD";
            // 
            // rbRangeDaysFromT
            // 
            this.rbRangeDaysFromT.AutoSize = true;
            this.rbRangeDaysFromT.Location = new System.Drawing.Point(12, 48);
            this.rbRangeDaysFromT.Name = "rbRangeDaysFromT";
            this.rbRangeDaysFromT.Size = new System.Drawing.Size(83, 17);
            this.rbRangeDaysFromT.TabIndex = 302;
            this.rbRangeDaysFromT.Text = "Days from T";
            this.rbRangeDaysFromT.UseVisualStyleBackColor = true;
            this.rbRangeDaysFromT.CheckedChanged += new System.EventHandler(this.rbRangeDaysFromT_CheckedChanged);
            // 
            // rbRangeDate
            // 
            this.rbRangeDate.AutoSize = true;
            this.rbRangeDate.Checked = true;
            this.rbRangeDate.Location = new System.Drawing.Point(12, 21);
            this.rbRangeDate.Name = "rbRangeDate";
            this.rbRangeDate.Size = new System.Drawing.Size(48, 17);
            this.rbRangeDate.TabIndex = 301;
            this.rbRangeDate.TabStop = true;
            this.rbRangeDate.Text = "Date";
            this.rbRangeDate.UseVisualStyleBackColor = true;
            this.rbRangeDate.CheckedChanged += new System.EventHandler(this.rbRangeDate_CheckedChanged);
            // 
            // gbxDateFormat
            // 
            this.gbxDateFormat.Controls.Add(this.rbDaysFromT);
            this.gbxDateFormat.Controls.Add(this.rbJulianDay2);
            this.gbxDateFormat.Controls.Add(this.rbJulianDay);
            this.gbxDateFormat.Controls.Add(this.rbDate);
            this.gbxDateFormat.Location = new System.Drawing.Point(343, 6);
            this.gbxDateFormat.Name = "gbxDateFormat";
            this.gbxDateFormat.Size = new System.Drawing.Size(168, 135);
            this.gbxDateFormat.TabIndex = 296;
            this.gbxDateFormat.TabStop = false;
            this.gbxDateFormat.Text = "Date Format";
            // 
            // rbDaysFromT
            // 
            this.rbDaysFromT.AutoSize = true;
            this.rbDaysFromT.Location = new System.Drawing.Point(20, 102);
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
            this.rbJulianDay2.Location = new System.Drawing.Point(20, 75);
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
            this.rbJulianDay.Location = new System.Drawing.Point(20, 48);
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
            this.rbDate.Location = new System.Drawing.Point(20, 21);
            this.rbDate.Name = "rbDate";
            this.rbDate.Size = new System.Drawing.Size(48, 17);
            this.rbDate.TabIndex = 0;
            this.rbDate.TabStop = true;
            this.rbDate.Text = "Date";
            this.rbDate.UseVisualStyleBackColor = true;
            // 
            // FormMagnitudeSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 295);
            this.Controls.Add(this.gbxDateFormat);
            this.Controls.Add(this.gbxTimestamp);
            this.Controls.Add(this.gbxSelectComet);
            this.Controls.Add(this.btnPlotGraph);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMagnitudeSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Magnitude graph settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMagnitudeSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormMagnitudeSettings_Load);
            this.gbxSelectComet.ResumeLayout(false);
            this.gbxSelectComet.PerformLayout();
            this.gbxTimestamp.ResumeLayout(false);
            this.gbxTimestamp.PerformLayout();
            this.pnlRangeDaysFromT.ResumeLayout(false);
            this.pnlRangeDaysFromT.PerformLayout();
            this.pnlRangeDate.ResumeLayout(false);
            this.pnlRangeDate.PerformLayout();
            this.gbxDateFormat.ResumeLayout(false);
            this.gbxDateFormat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPlotGraph;
        private System.Windows.Forms.GroupBox gbxSelectComet;
        private System.Windows.Forms.GroupBox gbxTimestamp;
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
        private System.Windows.Forms.TextBox tbStartDaysFromT;
        private System.Windows.Forms.TextBox tbEndDaysFromT;
        private System.Windows.Forms.Panel pnlRangeDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbStartYear;
        private System.Windows.Forms.TextBox tbStartMonth;
        private System.Windows.Forms.TextBox tbStartDay;
        private System.Windows.Forms.TextBox tbEndYear;
        private System.Windows.Forms.TextBox tbEndMonth;
        private System.Windows.Forms.TextBox tbEndDay;
    }
}