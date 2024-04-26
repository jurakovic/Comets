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
			pnlInfoLabels = new Panel();
			cbxMagDist = new CheckBox();
			cbxDateTime = new CheckBox();
			pnlInfoLabels.SuspendLayout();
			SuspendLayout();
			// 
			// pnlInfoLabels
			// 
			pnlInfoLabels.BackColor = SystemColors.ControlDark;
			pnlInfoLabels.Controls.Add(cbxMagDist);
			pnlInfoLabels.Controls.Add(cbxDateTime);
			pnlInfoLabels.Dock = DockStyle.Fill;
			pnlInfoLabels.Location = new Point(0, 0);
			pnlInfoLabels.Name = "pnlInfoLabels";
			pnlInfoLabels.Size = new Size(173, 51);
			pnlInfoLabels.TabIndex = 0;
			// 
			// cbxMagDist
			// 
			cbxMagDist.AutoSize = true;
			cbxMagDist.Checked = true;
			cbxMagDist.CheckState = CheckState.Checked;
			cbxMagDist.Location = new Point(6, 5);
			cbxMagDist.Name = "cbxMagDist";
			cbxMagDist.Size = new Size(145, 17);
			cbxMagDist.TabIndex = 0;
			cbxMagDist.Text = "Magnitude and distances";
			cbxMagDist.UseVisualStyleBackColor = true;
			cbxMagDist.CheckedChanged += cbxMagDist_CheckedChanged;
			// 
			// cbxDateTime
			// 
			cbxDateTime.AutoSize = true;
			cbxDateTime.Checked = true;
			cbxDateTime.CheckState = CheckState.Checked;
			cbxDateTime.Location = new Point(6, 28);
			cbxDateTime.Name = "cbxDateTime";
			cbxDateTime.Size = new Size(95, 17);
			cbxDateTime.TabIndex = 1;
			cbxDateTime.Text = "Date and Time";
			cbxDateTime.UseVisualStyleBackColor = true;
			cbxDateTime.CheckedChanged += cbxDateTime_CheckedChanged;
			// 
			// InfoLabelsControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(pnlInfoLabels);
			Font = new Font("Tahoma", 8.25F);
			Name = "InfoLabelsControl";
			Size = new Size(173, 51);
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
