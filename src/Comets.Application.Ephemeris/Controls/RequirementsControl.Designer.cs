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
			gbxRequirements = new GroupBox();
			txtMinMag = new TextBox();
			txtMaxEarthDist = new TextBox();
			txtMaxSunDist = new TextBox();
			cbxMinMag = new CheckBox();
			cbxMaxEarthDist = new CheckBox();
			cbxMaxSunDist = new CheckBox();
			gbxRequirements.SuspendLayout();
			SuspendLayout();
			// 
			// gbxRequirements
			// 
			gbxRequirements.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			gbxRequirements.Controls.Add(txtMinMag);
			gbxRequirements.Controls.Add(txtMaxEarthDist);
			gbxRequirements.Controls.Add(txtMaxSunDist);
			gbxRequirements.Controls.Add(cbxMinMag);
			gbxRequirements.Controls.Add(cbxMaxEarthDist);
			gbxRequirements.Controls.Add(cbxMaxSunDist);
			gbxRequirements.Location = new Point(0, 0);
			gbxRequirements.Name = "gbxRequirements";
			gbxRequirements.Size = new Size(204, 137);
			gbxRequirements.TabIndex = 0;
			gbxRequirements.TabStop = false;
			gbxRequirements.Text = "Requirements";
			// 
			// txtMinMag
			// 
			txtMinMag.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			txtMinMag.Location = new Point(152, 87);
			txtMinMag.Name = "txtMinMag";
			txtMinMag.Size = new Size(42, 21);
			txtMinMag.TabIndex = 5;
			txtMinMag.Text = "12";
			txtMinMag.TextAlign = HorizontalAlignment.Right;
			txtMinMag.TextChanged += txtMinMag_TextChanged;
			txtMinMag.KeyPress += txtMagDistCommon_KeyPress;
			// 
			// txtMaxEarthDist
			// 
			txtMaxEarthDist.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			txtMaxEarthDist.Location = new Point(152, 45);
			txtMaxEarthDist.Name = "txtMaxEarthDist";
			txtMaxEarthDist.Size = new Size(42, 21);
			txtMaxEarthDist.TabIndex = 3;
			txtMaxEarthDist.TextAlign = HorizontalAlignment.Right;
			txtMaxEarthDist.TextChanged += txtMaxEarthDist_TextChanged;
			txtMaxEarthDist.KeyPress += txtMagDistCommon_KeyPress;
			// 
			// txtMaxSunDist
			// 
			txtMaxSunDist.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			txtMaxSunDist.Location = new Point(152, 20);
			txtMaxSunDist.Name = "txtMaxSunDist";
			txtMaxSunDist.Size = new Size(42, 21);
			txtMaxSunDist.TabIndex = 1;
			txtMaxSunDist.TextAlign = HorizontalAlignment.Right;
			txtMaxSunDist.TextChanged += txtMaxSunDist_TextChanged;
			txtMaxSunDist.KeyPress += txtMagDistCommon_KeyPress;
			// 
			// cbxMinMag
			// 
			cbxMinMag.AutoSize = true;
			cbxMinMag.Checked = true;
			cbxMinMag.CheckState = CheckState.Checked;
			cbxMinMag.Location = new Point(12, 89);
			cbxMinMag.Name = "cbxMinMag";
			cbxMinMag.Size = new Size(95, 17);
			cbxMinMag.TabIndex = 4;
			cbxMinMag.Text = "Min magnitude";
			cbxMinMag.UseVisualStyleBackColor = true;
			// 
			// cbxMaxEarthDist
			// 
			cbxMaxEarthDist.AutoSize = true;
			cbxMaxEarthDist.Location = new Point(12, 48);
			cbxMaxEarthDist.Name = "cbxMaxEarthDist";
			cbxMaxEarthDist.Size = new Size(118, 17);
			cbxMaxEarthDist.TabIndex = 2;
			cbxMaxEarthDist.Text = "Max Earth distance";
			cbxMaxEarthDist.UseVisualStyleBackColor = true;
			// 
			// cbxMaxSunDist
			// 
			cbxMaxSunDist.AutoSize = true;
			cbxMaxSunDist.Location = new Point(12, 22);
			cbxMaxSunDist.Name = "cbxMaxSunDist";
			cbxMaxSunDist.Size = new Size(110, 17);
			cbxMaxSunDist.TabIndex = 0;
			cbxMaxSunDist.Text = "Max Sun distance";
			cbxMaxSunDist.UseVisualStyleBackColor = true;
			// 
			// RequirementsControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(gbxRequirements);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Name = "RequirementsControl";
			Size = new Size(204, 137);
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
