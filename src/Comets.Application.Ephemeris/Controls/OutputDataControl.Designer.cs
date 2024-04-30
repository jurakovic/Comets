namespace Comets.Application.Ephemeris
{
	partial class OutputDataControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
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
			this.gbxOutputData.SuspendLayout();
			SuspendLayout();
			// 
			// gbxOutputData
			// 
			this.gbxOutputData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
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
			this.gbxOutputData.Location = new System.Drawing.Point(0, 0);
			this.gbxOutputData.Name = "gbxOutputData";
			this.gbxOutputData.Size = new System.Drawing.Size(503, 137);
			this.gbxOutputData.TabIndex = 0;
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
			this.chMag.Location = new System.Drawing.Point(382, 89);
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
			this.chGeoDist.Location = new System.Drawing.Point(382, 47);
			this.chGeoDist.Name = "chGeoDist";
			this.chGeoDist.Size = new System.Drawing.Size(95, 17);
			this.chGeoDist.TabIndex = 10;
			this.chGeoDist.Text = "Earth distance";
			this.chGeoDist.UseVisualStyleBackColor = true;
			// 
			// chHelioDist
			// 
			this.chHelioDist.AutoSize = true;
			this.chHelioDist.Checked = true;
			this.chHelioDist.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chHelioDist.Location = new System.Drawing.Point(382, 22);
			this.chHelioDist.Name = "chHelioDist";
			this.chHelioDist.Size = new System.Drawing.Size(87, 17);
			this.chHelioDist.TabIndex = 9;
			this.chHelioDist.Text = "Sun distance";
			this.chHelioDist.UseVisualStyleBackColor = true;
			// 
			// chElong
			// 
			this.chElong.AutoSize = true;
			this.chElong.Checked = true;
			this.chElong.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chElong.Location = new System.Drawing.Point(254, 89);
			this.chElong.Name = "chElong";
			this.chElong.Size = new System.Drawing.Size(76, 17);
			this.chElong.TabIndex = 8;
			this.chElong.Text = "Elongation";
			this.chElong.UseVisualStyleBackColor = true;
			// 
			// chEcLat
			// 
			this.chEcLat.AutoSize = true;
			this.chEcLat.Location = new System.Drawing.Point(254, 47);
			this.chEcLat.Name = "chEcLat";
			this.chEcLat.Size = new System.Drawing.Size(100, 17);
			this.chEcLat.TabIndex = 7;
			this.chEcLat.Text = "Ecliptic Latitude";
			this.chEcLat.UseVisualStyleBackColor = true;
			// 
			// chEcLon
			// 
			this.chEcLon.AutoSize = true;
			this.chEcLon.Location = new System.Drawing.Point(254, 22);
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
			this.chAz.Location = new System.Drawing.Point(124, 113);
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
			this.chAlt.Location = new System.Drawing.Point(124, 89);
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
			this.chDec.Location = new System.Drawing.Point(124, 47);
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
			this.chRA.Location = new System.Drawing.Point(124, 22);
			this.chRA.Name = "chRA";
			this.chRA.Size = new System.Drawing.Size(126, 17);
			this.chRA.TabIndex = 2;
			this.chRA.Text = "Right ascension (RA)";
			this.chRA.UseVisualStyleBackColor = true;
			// 
			// OutputDataControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbxOutputData);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.Name = "OutputDataControl";
			this.Size = new System.Drawing.Size(503, 137);
			this.gbxOutputData.ResumeLayout(false);
			this.gbxOutputData.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.GroupBox gbxOutputData;
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
