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
			this.pnlInfoLabels = new System.Windows.Forms.Panel();
			this.cbxMagDist = new System.Windows.Forms.CheckBox();
			this.cbxDateTime = new System.Windows.Forms.CheckBox();
			this.pnlInfoLabels.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlInfoLabels
			// 
			this.pnlInfoLabels.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pnlInfoLabels.Controls.Add(this.cbxMagDist);
			this.pnlInfoLabels.Controls.Add(this.cbxDateTime);
			this.pnlInfoLabels.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlInfoLabels.Location = new System.Drawing.Point(0, 0);
			this.pnlInfoLabels.Name = "pnlInfoLabels";
			this.pnlInfoLabels.Size = new System.Drawing.Size(173, 51);
			this.pnlInfoLabels.TabIndex = 0;
			// 
			// cbxMagDist
			// 
			this.cbxMagDist.AutoSize = true;
			this.cbxMagDist.Checked = true;
			this.cbxMagDist.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxMagDist.Location = new System.Drawing.Point(6, 5);
			this.cbxMagDist.Name = "cbxMagDist";
			this.cbxMagDist.Size = new System.Drawing.Size(145, 17);
			this.cbxMagDist.TabIndex = 0;
			this.cbxMagDist.Text = "Magnitude and distances";
			this.cbxMagDist.UseVisualStyleBackColor = true;
			this.cbxMagDist.CheckedChanged += new System.EventHandler(this.cbxMagDist_CheckedChanged);
			// 
			// cbxDateTime
			// 
			this.cbxDateTime.AutoSize = true;
			this.cbxDateTime.Checked = true;
			this.cbxDateTime.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxDateTime.Location = new System.Drawing.Point(6, 28);
			this.cbxDateTime.Name = "cbxDateTime";
			this.cbxDateTime.Size = new System.Drawing.Size(95, 17);
			this.cbxDateTime.TabIndex = 1;
			this.cbxDateTime.Text = "Date and Time";
			this.cbxDateTime.UseVisualStyleBackColor = true;
			this.cbxDateTime.CheckedChanged += new System.EventHandler(this.cbxDateTime_CheckedChanged);
			// 
			// InfoLabelsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlInfoLabels);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "InfoLabelsControl";
			this.Size = new System.Drawing.Size(173, 51);
			this.pnlInfoLabels.ResumeLayout(false);
			this.pnlInfoLabels.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlInfoLabels;
		private System.Windows.Forms.CheckBox cbxMagDist;
		private System.Windows.Forms.CheckBox cbxDateTime;
	}
}
