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
			pnlMisc = new Panel();
			cbxShowAxes = new CheckBox();
			btnSaveImage = new Button();
			cbxAntialiasing = new CheckBox();
			pnlMisc.SuspendLayout();
			SuspendLayout();
			// 
			// pnlMisc
			// 
			pnlMisc.BackColor = SystemColors.ControlDark;
			pnlMisc.Controls.Add(cbxShowAxes);
			pnlMisc.Controls.Add(btnSaveImage);
			pnlMisc.Controls.Add(cbxAntialiasing);
			pnlMisc.Dock = DockStyle.Fill;
			pnlMisc.Location = new Point(0, 0);
			pnlMisc.Name = "pnlMisc";
			pnlMisc.Size = new Size(173, 77);
			pnlMisc.TabIndex = 0;
			// 
			// cbxShowAxes
			// 
			cbxShowAxes.AutoSize = true;
			cbxShowAxes.Location = new Point(6, 5);
			cbxShowAxes.Name = "cbxShowAxes";
			cbxShowAxes.Size = new Size(78, 17);
			cbxShowAxes.TabIndex = 0;
			cbxShowAxes.Text = "Show axes";
			cbxShowAxes.UseVisualStyleBackColor = true;
			cbxShowAxes.CheckedChanged += cbxShowAxes_CheckedChanged;
			// 
			// btnSaveImage
			// 
			btnSaveImage.Location = new Point(4, 50);
			btnSaveImage.Name = "btnSaveImage";
			btnSaveImage.Size = new Size(165, 23);
			btnSaveImage.TabIndex = 2;
			btnSaveImage.Text = "Save image";
			btnSaveImage.UseVisualStyleBackColor = true;
			btnSaveImage.Click += btnSaveImage_Click;
			// 
			// cbxAntialiasing
			// 
			cbxAntialiasing.AutoSize = true;
			cbxAntialiasing.Location = new Point(6, 28);
			cbxAntialiasing.Name = "cbxAntialiasing";
			cbxAntialiasing.Size = new Size(80, 17);
			cbxAntialiasing.TabIndex = 1;
			cbxAntialiasing.Text = "Antialiasing";
			cbxAntialiasing.UseVisualStyleBackColor = true;
			cbxAntialiasing.CheckedChanged += cbxAntialiasing_CheckedChanged;
			// 
			// MiscControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(pnlMisc);
			Font = new Font("Tahoma", 8.25F);
			Name = "MiscControl";
			Size = new Size(173, 77);
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
