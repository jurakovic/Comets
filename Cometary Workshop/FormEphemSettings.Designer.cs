namespace Cometary_Workshop
{
    partial class FormEphemSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chDst = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbTz = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboLon = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbLonSec = new System.Windows.Forms.TextBox();
            this.tbLonMin = new System.Windows.Forms.TextBox();
            this.tbLonDeg = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboLat = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLatSec = new System.Windows.Forms.TextBox();
            this.tbLatMin = new System.Windows.Forms.TextBox();
            this.tbLatDeg = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbIntervalHour = new System.Windows.Forms.TextBox();
            this.dtPickerStopDate = new System.Windows.Forms.DateTimePicker();
            this.tbIntervalDay = new System.Windows.Forms.TextBox();
            this.dtPickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.chDst);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tbTz);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.comboLon);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbLonSec);
            this.groupBox1.Controls.Add(this.tbLonMin);
            this.groupBox1.Controls.Add(this.tbLonDeg);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.comboLat);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbLatSec);
            this.groupBox1.Controls.Add(this.tbLatMin);
            this.groupBox1.Controls.Add(this.tbLatDeg);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Location";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(167, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Set time zone from computer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chDst
            // 
            this.chDst.AutoSize = true;
            this.chDst.Checked = true;
            this.chDst.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chDst.Location = new System.Drawing.Point(231, 101);
            this.chDst.Name = "chDst";
            this.chDst.Size = new System.Drawing.Size(141, 19);
            this.chDst.TabIndex = 21;
            this.chDst.Text = "Daylight Saving Time";
            this.chDst.UseVisualStyleBackColor = true;
            this.chDst.CheckedChanged += new System.EventHandler(this.chDst_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(136, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 15);
            this.label11.TabIndex = 20;
            this.label11.Text = "UTC";
            // 
            // tbTz
            // 
            this.tbTz.Location = new System.Drawing.Point(167, 99);
            this.tbTz.Name = "tbTz";
            this.tbTz.Size = new System.Drawing.Size(48, 21);
            this.tbTz.TabIndex = 19;
            this.tbTz.Text = "+02:00";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 15);
            this.label10.TabIndex = 18;
            this.label10.Text = "Time zone:";
            // 
            // comboLon
            // 
            this.comboLon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboLon.FormattingEnabled = true;
            this.comboLon.Items.AddRange(new object[] {
            "E",
            "W"});
            this.comboLon.Location = new System.Drawing.Point(364, 72);
            this.comboLon.Name = "comboLon";
            this.comboLon.Size = new System.Drawing.Size(53, 21);
            this.comboLon.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(345, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 18);
            this.label6.TabIndex = 16;
            this.label6.Text = "\'\'";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(281, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 18);
            this.label7.TabIndex = 15;
            this.label7.Text = "\'";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(217, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 18);
            this.label8.TabIndex = 14;
            this.label8.Text = "°";
            // 
            // tbLonSec
            // 
            this.tbLonSec.Location = new System.Drawing.Point(295, 72);
            this.tbLonSec.Name = "tbLonSec";
            this.tbLonSec.Size = new System.Drawing.Size(48, 21);
            this.tbLonSec.TabIndex = 13;
            this.tbLonSec.Text = "0";
            this.tbLonSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbLonMin
            // 
            this.tbLonMin.Location = new System.Drawing.Point(231, 72);
            this.tbLonMin.Name = "tbLonMin";
            this.tbLonMin.Size = new System.Drawing.Size(48, 21);
            this.tbLonMin.TabIndex = 12;
            this.tbLonMin.Text = "57";
            this.tbLonMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbLonDeg
            // 
            this.tbLonDeg.Location = new System.Drawing.Point(167, 72);
            this.tbLonDeg.Name = "tbLonDeg";
            this.tbLonDeg.Size = new System.Drawing.Size(48, 21);
            this.tbLonDeg.TabIndex = 11;
            this.tbLonDeg.Text = "15";
            this.tbLonDeg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 15);
            this.label9.TabIndex = 10;
            this.label9.Text = "Longitude:";
            // 
            // comboLat
            // 
            this.comboLat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboLat.FormattingEnabled = true;
            this.comboLat.Items.AddRange(new object[] {
            "N",
            "S"});
            this.comboLat.Location = new System.Drawing.Point(364, 45);
            this.comboLat.Name = "comboLat";
            this.comboLat.Size = new System.Drawing.Size(53, 21);
            this.comboLat.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(345, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "\'\'";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(281, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "\'";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(217, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "°";
            // 
            // tbLatSec
            // 
            this.tbLatSec.Location = new System.Drawing.Point(295, 45);
            this.tbLatSec.Name = "tbLatSec";
            this.tbLatSec.Size = new System.Drawing.Size(48, 21);
            this.tbLatSec.TabIndex = 5;
            this.tbLatSec.Text = "59";
            this.tbLatSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbLatMin
            // 
            this.tbLatMin.Location = new System.Drawing.Point(231, 45);
            this.tbLatMin.Name = "tbLatMin";
            this.tbLatMin.Size = new System.Drawing.Size(48, 21);
            this.tbLatMin.TabIndex = 4;
            this.tbLatMin.Text = "46";
            this.tbLatMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbLatDeg
            // 
            this.tbLatDeg.Location = new System.Drawing.Point(167, 45);
            this.tbLatDeg.Name = "tbLatDeg";
            this.tbLatDeg.Size = new System.Drawing.Size(48, 21);
            this.tbLatDeg.TabIndex = 3;
            this.tbLatDeg.Text = "45";
            this.tbLatDeg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Latitude:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(167, 18);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(250, 21);
            this.tbName.TabIndex = 1;
            this.tbName.Text = "Zagreb";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label59);
            this.groupBox2.Controls.Add(this.label58);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.tbIntervalHour);
            this.groupBox2.Controls.Add(this.dtPickerStopDate);
            this.groupBox2.Controls.Add(this.tbIntervalDay);
            this.groupBox2.Controls.Add(this.dtPickerStartDate);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(12, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(432, 111);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Time span";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 76);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 15);
            this.label14.TabIndex = 5;
            this.label14.Text = "Interval";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(253, 78);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(38, 15);
            this.label59.TabIndex = 3;
            this.label59.Text = "hours";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(192, 78);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(32, 15);
            this.label58.TabIndex = 2;
            this.label58.Text = "days";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(113, 15);
            this.label13.TabIndex = 4;
            this.label13.Text = "Stop date and time:";
            // 
            // tbIntervalHour
            // 
            this.tbIntervalHour.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbIntervalHour.Location = new System.Drawing.Point(228, 74);
            this.tbIntervalHour.MaxLength = 3;
            this.tbIntervalHour.Name = "tbIntervalHour";
            this.tbIntervalHour.Size = new System.Drawing.Size(23, 22);
            this.tbIntervalHour.TabIndex = 1;
            this.tbIntervalHour.Text = "0";
            this.tbIntervalHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dtPickerStopDate
            // 
            this.dtPickerStopDate.CustomFormat = "dd-MM-yyyy HH:mm";
            this.dtPickerStopDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtPickerStopDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPickerStopDate.Location = new System.Drawing.Point(167, 46);
            this.dtPickerStopDate.MaxDate = new System.DateTime(2300, 12, 31, 0, 0, 0, 0);
            this.dtPickerStopDate.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtPickerStopDate.Name = "dtPickerStopDate";
            this.dtPickerStopDate.Size = new System.Drawing.Size(140, 22);
            this.dtPickerStopDate.TabIndex = 3;
            // 
            // tbIntervalDay
            // 
            this.tbIntervalDay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbIntervalDay.Location = new System.Drawing.Point(167, 74);
            this.tbIntervalDay.MaxLength = 2;
            this.tbIntervalDay.Name = "tbIntervalDay";
            this.tbIntervalDay.Size = new System.Drawing.Size(23, 22);
            this.tbIntervalDay.TabIndex = 0;
            this.tbIntervalDay.Text = "1";
            this.tbIntervalDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dtPickerStartDate
            // 
            this.dtPickerStartDate.CustomFormat = "dd-MM-yyyy HH:mm";
            this.dtPickerStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtPickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPickerStartDate.Location = new System.Drawing.Point(167, 18);
            this.dtPickerStartDate.MaxDate = new System.DateTime(2300, 12, 31, 0, 0, 0, 0);
            this.dtPickerStartDate.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtPickerStartDate.Name = "dtPickerStartDate";
            this.dtPickerStartDate.Size = new System.Drawing.Size(140, 22);
            this.dtPickerStartDate.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 15);
            this.label12.TabIndex = 1;
            this.label12.Text = "Start date and time:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(354, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(244, 306);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormEphemSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 345);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEphemSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FormEphemSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboLat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLatSec;
        private System.Windows.Forms.TextBox tbLatMin;
        private System.Windows.Forms.TextBox tbLatDeg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chDst;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbTz;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboLon;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbLonSec;
        private System.Windows.Forms.TextBox tbLonMin;
        private System.Windows.Forms.TextBox tbLonDeg;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtPickerStartDate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtPickerStopDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.TextBox tbIntervalHour;
        private System.Windows.Forms.TextBox tbIntervalDay;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}