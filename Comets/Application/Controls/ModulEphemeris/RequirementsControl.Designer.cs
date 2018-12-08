namespace Comets.Application.Controls.ModulEphemeris
{
	partial class RequirementsControl
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
			this.gbxRequirements = new System.Windows.Forms.GroupBox();
			this.txtMinMag = new System.Windows.Forms.TextBox();
			this.txtMaxEarthDist = new System.Windows.Forms.TextBox();
			this.txtMaxSunDist = new System.Windows.Forms.TextBox();
			this.cbxMinMag = new System.Windows.Forms.CheckBox();
			this.cbxMaxEarthDist = new System.Windows.Forms.CheckBox();
			this.cbxMaxSunDist = new System.Windows.Forms.CheckBox();
			this.gbxRequirements.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxRequirements
			// 
			this.gbxRequirements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxRequirements.Controls.Add(this.txtMinMag);
			this.gbxRequirements.Controls.Add(this.txtMaxEarthDist);
			this.gbxRequirements.Controls.Add(this.txtMaxSunDist);
			this.gbxRequirements.Controls.Add(this.cbxMinMag);
			this.gbxRequirements.Controls.Add(this.cbxMaxEarthDist);
			this.gbxRequirements.Controls.Add(this.cbxMaxSunDist);
			this.gbxRequirements.Location = new System.Drawing.Point(0, 0);
			this.gbxRequirements.Name = "gbxRequirements";
			this.gbxRequirements.Size = new System.Drawing.Size(199, 149);
			this.gbxRequirements.TabIndex = 0;
			this.gbxRequirements.TabStop = false;
			this.gbxRequirements.Text = "Requirements";
			// 
			// txtMinMag
			// 
			this.txtMinMag.Location = new System.Drawing.Point(147, 87);
			this.txtMinMag.Name = "txtMinMag";
			this.txtMinMag.Size = new System.Drawing.Size(42, 21);
			this.txtMinMag.TabIndex = 5;
			this.txtMinMag.Text = "12";
			this.txtMinMag.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMinMag.TextChanged += new System.EventHandler(this.txtMinMag_TextChanged);
			this.txtMinMag.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMagDistCommon_KeyPress);
			// 
			// txtMaxEarthDist
			// 
			this.txtMaxEarthDist.Location = new System.Drawing.Point(147, 45);
			this.txtMaxEarthDist.Name = "txtMaxEarthDist";
			this.txtMaxEarthDist.Size = new System.Drawing.Size(42, 21);
			this.txtMaxEarthDist.TabIndex = 3;
			this.txtMaxEarthDist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMaxEarthDist.TextChanged += new System.EventHandler(this.txtMaxEarthDist_TextChanged);
			this.txtMaxEarthDist.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMagDistCommon_KeyPress);
			// 
			// txtMaxSunDist
			// 
			this.txtMaxSunDist.Location = new System.Drawing.Point(147, 20);
			this.txtMaxSunDist.Name = "txtMaxSunDist";
			this.txtMaxSunDist.Size = new System.Drawing.Size(42, 21);
			this.txtMaxSunDist.TabIndex = 1;
			this.txtMaxSunDist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMaxSunDist.TextChanged += new System.EventHandler(this.txtMaxSunDist_TextChanged);
			this.txtMaxSunDist.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMagDistCommon_KeyPress);
			// 
			// cbxMinMag
			// 
			this.cbxMinMag.AutoSize = true;
			this.cbxMinMag.Checked = true;
			this.cbxMinMag.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxMinMag.Location = new System.Drawing.Point(10, 89);
			this.cbxMinMag.Name = "cbxMinMag";
			this.cbxMinMag.Size = new System.Drawing.Size(119, 17);
			this.cbxMinMag.TabIndex = 4;
			this.cbxMinMag.Text = "Minimum magnitude";
			this.cbxMinMag.UseVisualStyleBackColor = true;
			// 
			// cbxMaxEarthDist
			// 
			this.cbxMaxEarthDist.AutoSize = true;
			this.cbxMaxEarthDist.Location = new System.Drawing.Point(10, 48);
			this.cbxMaxEarthDist.Name = "cbxMaxEarthDist";
			this.cbxMaxEarthDist.Size = new System.Drawing.Size(142, 17);
			this.cbxMaxEarthDist.TabIndex = 2;
			this.cbxMaxEarthDist.Text = "Maximum Earth distance";
			this.cbxMaxEarthDist.UseVisualStyleBackColor = true;
			// 
			// cbxMaxSunDist
			// 
			this.cbxMaxSunDist.AutoSize = true;
			this.cbxMaxSunDist.Location = new System.Drawing.Point(10, 22);
			this.cbxMaxSunDist.Name = "cbxMaxSunDist";
			this.cbxMaxSunDist.Size = new System.Drawing.Size(134, 17);
			this.cbxMaxSunDist.TabIndex = 0;
			this.cbxMaxSunDist.Text = "Maximum Sun distance";
			this.cbxMaxSunDist.UseVisualStyleBackColor = true;
			// 
			// RequirementsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbxRequirements);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "RequirementsControl";
			this.Size = new System.Drawing.Size(199, 149);
			this.gbxRequirements.ResumeLayout(false);
			this.gbxRequirements.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbxRequirements;
		private System.Windows.Forms.TextBox txtMinMag;
		private System.Windows.Forms.TextBox txtMaxEarthDist;
		private System.Windows.Forms.TextBox txtMaxSunDist;
		private System.Windows.Forms.CheckBox cbxMinMag;
		private System.Windows.Forms.CheckBox cbxMaxEarthDist;
		private System.Windows.Forms.CheckBox cbxMaxSunDist;
	}
}
