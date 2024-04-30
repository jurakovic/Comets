namespace Comets.Application.OrbitViewer.Controls
{
	partial class InfoLabelsControl
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
			pnlInfoLabels = new System.Windows.Forms.Panel();
			cbxMagDist = new System.Windows.Forms.CheckBox();
			cbxDateTime = new System.Windows.Forms.CheckBox();
			pnlInfoLabels.SuspendLayout();
			SuspendLayout();
			// 
			// pnlInfoLabels
			// 
			pnlInfoLabels.BackColor = System.Drawing.SystemColors.ControlDark;
			pnlInfoLabels.Controls.Add(cbxMagDist);
			pnlInfoLabels.Controls.Add(cbxDateTime);
			pnlInfoLabels.Dock = System.Windows.Forms.DockStyle.Fill;
			pnlInfoLabels.Location = new System.Drawing.Point(0, 0);
			pnlInfoLabels.Name = "pnlInfoLabels";
			pnlInfoLabels.Size = new System.Drawing.Size(173, 51);
			pnlInfoLabels.TabIndex = 0;
			// 
			// cbxMagDist
			// 
			cbxMagDist.AutoSize = true;
			cbxMagDist.Checked = true;
			cbxMagDist.CheckState = System.Windows.Forms.CheckState.Checked;
			cbxMagDist.Location = new System.Drawing.Point(6, 5);
			cbxMagDist.Name = "cbxMagDist";
			cbxMagDist.Size = new System.Drawing.Size(145, 17);
			cbxMagDist.TabIndex = 0;
			cbxMagDist.Text = "Magnitude and distances";
			cbxMagDist.UseVisualStyleBackColor = true;
			cbxMagDist.CheckedChanged += cbxMagDist_CheckedChanged;
			// 
			// cbxDateTime
			// 
			cbxDateTime.AutoSize = true;
			cbxDateTime.Checked = true;
			cbxDateTime.CheckState = System.Windows.Forms.CheckState.Checked;
			cbxDateTime.Location = new System.Drawing.Point(6, 28);
			cbxDateTime.Name = "cbxDateTime";
			cbxDateTime.Size = new System.Drawing.Size(95, 17);
			cbxDateTime.TabIndex = 1;
			cbxDateTime.Text = "Date and Time";
			cbxDateTime.UseVisualStyleBackColor = true;
			cbxDateTime.CheckedChanged += cbxDateTime_CheckedChanged;
			// 
			// InfoLabelsControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(pnlInfoLabels);
			Font = new System.Drawing.Font("Tahoma", 8.25F);
			Name = "InfoLabelsControl";
			Size = new System.Drawing.Size(173, 51);
			pnlInfoLabels.ResumeLayout(false);
			pnlInfoLabels.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel pnlInfoLabels;
		private System.Windows.Forms.CheckBox cbxMagDist;
		private System.Windows.Forms.CheckBox cbxDateTime;
	}
}
