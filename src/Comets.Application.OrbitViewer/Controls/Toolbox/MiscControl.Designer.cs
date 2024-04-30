namespace Comets.Application.OrbitViewer.Controls
{
	partial class MiscControl
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
			pnlMisc = new System.Windows.Forms.Panel();
			cbxShowAxes = new System.Windows.Forms.CheckBox();
			btnSaveImage = new System.Windows.Forms.Button();
			cbxAntialiasing = new System.Windows.Forms.CheckBox();
			pnlMisc.SuspendLayout();
			SuspendLayout();
			// 
			// pnlMisc
			// 
			pnlMisc.BackColor = System.Drawing.SystemColors.ControlDark;
			pnlMisc.Controls.Add(cbxShowAxes);
			pnlMisc.Controls.Add(btnSaveImage);
			pnlMisc.Controls.Add(cbxAntialiasing);
			pnlMisc.Dock = System.Windows.Forms.DockStyle.Fill;
			pnlMisc.Location = new System.Drawing.Point(0, 0);
			pnlMisc.Name = "pnlMisc";
			pnlMisc.Size = new System.Drawing.Size(173, 77);
			pnlMisc.TabIndex = 0;
			// 
			// cbxShowAxes
			// 
			cbxShowAxes.AutoSize = true;
			cbxShowAxes.Location = new System.Drawing.Point(6, 5);
			cbxShowAxes.Name = "cbxShowAxes";
			cbxShowAxes.Size = new System.Drawing.Size(78, 17);
			cbxShowAxes.TabIndex = 0;
			cbxShowAxes.Text = "Show axes";
			cbxShowAxes.UseVisualStyleBackColor = true;
			cbxShowAxes.CheckedChanged += cbxShowAxes_CheckedChanged;
			// 
			// btnSaveImage
			// 
			btnSaveImage.Location = new System.Drawing.Point(4, 50);
			btnSaveImage.Name = "btnSaveImage";
			btnSaveImage.Size = new System.Drawing.Size(165, 23);
			btnSaveImage.TabIndex = 2;
			btnSaveImage.Text = "Save image";
			btnSaveImage.UseVisualStyleBackColor = true;
			btnSaveImage.Click += btnSaveImage_Click;
			// 
			// cbxAntialiasing
			// 
			cbxAntialiasing.AutoSize = true;
			cbxAntialiasing.Location = new System.Drawing.Point(6, 28);
			cbxAntialiasing.Name = "cbxAntialiasing";
			cbxAntialiasing.Size = new System.Drawing.Size(80, 17);
			cbxAntialiasing.TabIndex = 1;
			cbxAntialiasing.Text = "Antialiasing";
			cbxAntialiasing.UseVisualStyleBackColor = true;
			cbxAntialiasing.CheckedChanged += cbxAntialiasing_CheckedChanged;
			// 
			// MiscControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(pnlMisc);
			Font = new System.Drawing.Font("Tahoma", 8.25F);
			Name = "MiscControl";
			Size = new System.Drawing.Size(173, 77);
			pnlMisc.ResumeLayout(false);
			pnlMisc.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel pnlMisc;
		private System.Windows.Forms.CheckBox cbxShowAxes;
		private System.Windows.Forms.Button btnSaveImage;
		private System.Windows.Forms.CheckBox cbxAntialiasing;
	}
}
