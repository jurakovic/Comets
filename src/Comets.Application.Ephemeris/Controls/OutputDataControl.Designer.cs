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
			gbxOutputData = new System.Windows.Forms.GroupBox();
			radioLocalTime = new System.Windows.Forms.RadioButton();
			radioUnivTime = new System.Windows.Forms.RadioButton();
			chMag = new System.Windows.Forms.CheckBox();
			chGeoDist = new System.Windows.Forms.CheckBox();
			chHelioDist = new System.Windows.Forms.CheckBox();
			chElong = new System.Windows.Forms.CheckBox();
			chEcLat = new System.Windows.Forms.CheckBox();
			chEcLon = new System.Windows.Forms.CheckBox();
			chAz = new System.Windows.Forms.CheckBox();
			chAlt = new System.Windows.Forms.CheckBox();
			chDec = new System.Windows.Forms.CheckBox();
			chRA = new System.Windows.Forms.CheckBox();
			gbxOutputData.SuspendLayout();
			SuspendLayout();
			// 
			// gbxOutputData
			// 
			gbxOutputData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			gbxOutputData.Controls.Add(radioLocalTime);
			gbxOutputData.Controls.Add(radioUnivTime);
			gbxOutputData.Controls.Add(chMag);
			gbxOutputData.Controls.Add(chGeoDist);
			gbxOutputData.Controls.Add(chHelioDist);
			gbxOutputData.Controls.Add(chElong);
			gbxOutputData.Controls.Add(chEcLat);
			gbxOutputData.Controls.Add(chEcLon);
			gbxOutputData.Controls.Add(chAz);
			gbxOutputData.Controls.Add(chAlt);
			gbxOutputData.Controls.Add(chDec);
			gbxOutputData.Controls.Add(chRA);
			gbxOutputData.Location = new System.Drawing.Point(0, 0);
			gbxOutputData.Name = "gbxOutputData";
			gbxOutputData.Size = new System.Drawing.Size(503, 137);
			gbxOutputData.TabIndex = 0;
			gbxOutputData.TabStop = false;
			gbxOutputData.Text = "Output data";
			// 
			// radioLocalTime
			// 
			radioLocalTime.AutoSize = true;
			radioLocalTime.Checked = true;
			radioLocalTime.Location = new System.Drawing.Point(12, 22);
			radioLocalTime.Name = "radioLocalTime";
			radioLocalTime.Size = new System.Drawing.Size(74, 17);
			radioLocalTime.TabIndex = 0;
			radioLocalTime.TabStop = true;
			radioLocalTime.Text = "Local Time";
			radioLocalTime.UseVisualStyleBackColor = true;
			// 
			// radioUnivTime
			// 
			radioUnivTime.AutoSize = true;
			radioUnivTime.Location = new System.Drawing.Point(12, 47);
			radioUnivTime.Name = "radioUnivTime";
			radioUnivTime.Size = new System.Drawing.Size(94, 17);
			radioUnivTime.TabIndex = 1;
			radioUnivTime.Text = "Universal Time";
			radioUnivTime.UseVisualStyleBackColor = true;
			// 
			// chMag
			// 
			chMag.AutoSize = true;
			chMag.Checked = true;
			chMag.CheckState = System.Windows.Forms.CheckState.Checked;
			chMag.Location = new System.Drawing.Point(382, 89);
			chMag.Name = "chMag";
			chMag.Size = new System.Drawing.Size(76, 17);
			chMag.TabIndex = 11;
			chMag.Text = "Magnitude";
			chMag.UseVisualStyleBackColor = true;
			// 
			// chGeoDist
			// 
			chGeoDist.AutoSize = true;
			chGeoDist.Checked = true;
			chGeoDist.CheckState = System.Windows.Forms.CheckState.Checked;
			chGeoDist.Location = new System.Drawing.Point(382, 47);
			chGeoDist.Name = "chGeoDist";
			chGeoDist.Size = new System.Drawing.Size(95, 17);
			chGeoDist.TabIndex = 10;
			chGeoDist.Text = "Earth distance";
			chGeoDist.UseVisualStyleBackColor = true;
			// 
			// chHelioDist
			// 
			chHelioDist.AutoSize = true;
			chHelioDist.Checked = true;
			chHelioDist.CheckState = System.Windows.Forms.CheckState.Checked;
			chHelioDist.Location = new System.Drawing.Point(382, 22);
			chHelioDist.Name = "chHelioDist";
			chHelioDist.Size = new System.Drawing.Size(87, 17);
			chHelioDist.TabIndex = 9;
			chHelioDist.Text = "Sun distance";
			chHelioDist.UseVisualStyleBackColor = true;
			// 
			// chElong
			// 
			chElong.AutoSize = true;
			chElong.Checked = true;
			chElong.CheckState = System.Windows.Forms.CheckState.Checked;
			chElong.Location = new System.Drawing.Point(254, 89);
			chElong.Name = "chElong";
			chElong.Size = new System.Drawing.Size(76, 17);
			chElong.TabIndex = 8;
			chElong.Text = "Elongation";
			chElong.UseVisualStyleBackColor = true;
			// 
			// chEcLat
			// 
			chEcLat.AutoSize = true;
			chEcLat.Location = new System.Drawing.Point(254, 47);
			chEcLat.Name = "chEcLat";
			chEcLat.Size = new System.Drawing.Size(100, 17);
			chEcLat.TabIndex = 7;
			chEcLat.Text = "Ecliptic Latitude";
			chEcLat.UseVisualStyleBackColor = true;
			// 
			// chEcLon
			// 
			chEcLon.AutoSize = true;
			chEcLon.Location = new System.Drawing.Point(254, 22);
			chEcLon.Name = "chEcLon";
			chEcLon.Size = new System.Drawing.Size(108, 17);
			chEcLon.TabIndex = 6;
			chEcLon.Text = "Ecliptic Longitude";
			chEcLon.UseVisualStyleBackColor = true;
			// 
			// chAz
			// 
			chAz.AutoSize = true;
			chAz.Checked = true;
			chAz.CheckState = System.Windows.Forms.CheckState.Checked;
			chAz.Location = new System.Drawing.Point(124, 113);
			chAz.Name = "chAz";
			chAz.Size = new System.Drawing.Size(87, 17);
			chAz.TabIndex = 5;
			chAz.Text = "Azimuth (Az)";
			chAz.UseVisualStyleBackColor = true;
			// 
			// chAlt
			// 
			chAlt.AutoSize = true;
			chAlt.Checked = true;
			chAlt.CheckState = System.Windows.Forms.CheckState.Checked;
			chAlt.Location = new System.Drawing.Point(124, 89);
			chAlt.Name = "chAlt";
			chAlt.Size = new System.Drawing.Size(87, 17);
			chAlt.TabIndex = 4;
			chAlt.Text = "Altitude (Alt)";
			chAlt.UseVisualStyleBackColor = true;
			// 
			// chDec
			// 
			chDec.AutoSize = true;
			chDec.Checked = true;
			chDec.CheckState = System.Windows.Forms.CheckState.Checked;
			chDec.Location = new System.Drawing.Point(124, 47);
			chDec.Name = "chDec";
			chDec.Size = new System.Drawing.Size(107, 17);
			chDec.TabIndex = 3;
			chDec.Text = "Declination (Dec)";
			chDec.UseVisualStyleBackColor = true;
			// 
			// chRA
			// 
			chRA.AutoSize = true;
			chRA.Checked = true;
			chRA.CheckState = System.Windows.Forms.CheckState.Checked;
			chRA.Location = new System.Drawing.Point(124, 22);
			chRA.Name = "chRA";
			chRA.Size = new System.Drawing.Size(126, 17);
			chRA.TabIndex = 2;
			chRA.Text = "Right ascension (RA)";
			chRA.UseVisualStyleBackColor = true;
			// 
			// OutputDataControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(gbxOutputData);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			Name = "OutputDataControl";
			Size = new System.Drawing.Size(503, 137);
			gbxOutputData.ResumeLayout(false);
			gbxOutputData.PerformLayout();
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
