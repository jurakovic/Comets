namespace Comets.Application.Ephemeris
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
			gbxRequirements = new System.Windows.Forms.GroupBox();
			txtMinMag = new System.Windows.Forms.TextBox();
			txtMaxEarthDist = new System.Windows.Forms.TextBox();
			txtMaxSunDist = new System.Windows.Forms.TextBox();
			cbxMinMag = new System.Windows.Forms.CheckBox();
			cbxMaxEarthDist = new System.Windows.Forms.CheckBox();
			cbxMaxSunDist = new System.Windows.Forms.CheckBox();
			gbxRequirements.SuspendLayout();
			SuspendLayout();
			// 
			// gbxRequirements
			// 
			gbxRequirements.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			gbxRequirements.Controls.Add(txtMinMag);
			gbxRequirements.Controls.Add(txtMaxEarthDist);
			gbxRequirements.Controls.Add(txtMaxSunDist);
			gbxRequirements.Controls.Add(cbxMinMag);
			gbxRequirements.Controls.Add(cbxMaxEarthDist);
			gbxRequirements.Controls.Add(cbxMaxSunDist);
			gbxRequirements.Location = new System.Drawing.Point(0, 0);
			gbxRequirements.Name = "gbxRequirements";
			gbxRequirements.Size = new System.Drawing.Size(204, 137);
			gbxRequirements.TabIndex = 0;
			gbxRequirements.TabStop = false;
			gbxRequirements.Text = "Requirements";
			// 
			// txtMinMag
			// 
			txtMinMag.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			txtMinMag.Location = new System.Drawing.Point(152, 87);
			txtMinMag.Name = "txtMinMag";
			txtMinMag.Size = new System.Drawing.Size(42, 21);
			txtMinMag.TabIndex = 5;
			txtMinMag.Text = "12";
			txtMinMag.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			txtMinMag.TextChanged += txtMinMag_TextChanged;
			txtMinMag.KeyPress += txtMagDistCommon_KeyPress;
			// 
			// txtMaxEarthDist
			// 
			txtMaxEarthDist.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			txtMaxEarthDist.Location = new System.Drawing.Point(152, 45);
			txtMaxEarthDist.Name = "txtMaxEarthDist";
			txtMaxEarthDist.Size = new System.Drawing.Size(42, 21);
			txtMaxEarthDist.TabIndex = 3;
			txtMaxEarthDist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			txtMaxEarthDist.TextChanged += txtMaxEarthDist_TextChanged;
			txtMaxEarthDist.KeyPress += txtMagDistCommon_KeyPress;
			// 
			// txtMaxSunDist
			// 
			txtMaxSunDist.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			txtMaxSunDist.Location = new System.Drawing.Point(152, 20);
			txtMaxSunDist.Name = "txtMaxSunDist";
			txtMaxSunDist.Size = new System.Drawing.Size(42, 21);
			txtMaxSunDist.TabIndex = 1;
			txtMaxSunDist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			txtMaxSunDist.TextChanged += txtMaxSunDist_TextChanged;
			txtMaxSunDist.KeyPress += txtMagDistCommon_KeyPress;
			// 
			// cbxMinMag
			// 
			cbxMinMag.AutoSize = true;
			cbxMinMag.Checked = true;
			cbxMinMag.CheckState = System.Windows.Forms.CheckState.Checked;
			cbxMinMag.Location = new System.Drawing.Point(12, 89);
			cbxMinMag.Name = "cbxMinMag";
			cbxMinMag.Size = new System.Drawing.Size(95, 17);
			cbxMinMag.TabIndex = 4;
			cbxMinMag.Text = "Min magnitude";
			cbxMinMag.UseVisualStyleBackColor = true;
			// 
			// cbxMaxEarthDist
			// 
			cbxMaxEarthDist.AutoSize = true;
			cbxMaxEarthDist.Location = new System.Drawing.Point(12, 48);
			cbxMaxEarthDist.Name = "cbxMaxEarthDist";
			cbxMaxEarthDist.Size = new System.Drawing.Size(118, 17);
			cbxMaxEarthDist.TabIndex = 2;
			cbxMaxEarthDist.Text = "Max Earth distance";
			cbxMaxEarthDist.UseVisualStyleBackColor = true;
			// 
			// cbxMaxSunDist
			// 
			cbxMaxSunDist.AutoSize = true;
			cbxMaxSunDist.Location = new System.Drawing.Point(12, 22);
			cbxMaxSunDist.Name = "cbxMaxSunDist";
			cbxMaxSunDist.Size = new System.Drawing.Size(110, 17);
			cbxMaxSunDist.TabIndex = 0;
			cbxMaxSunDist.Text = "Max Sun distance";
			cbxMaxSunDist.UseVisualStyleBackColor = true;
			// 
			// RequirementsControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(gbxRequirements);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			Name = "RequirementsControl";
			Size = new System.Drawing.Size(204, 137);
			gbxRequirements.ResumeLayout(false);
			gbxRequirements.PerformLayout();
			ResumeLayout(false);
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
