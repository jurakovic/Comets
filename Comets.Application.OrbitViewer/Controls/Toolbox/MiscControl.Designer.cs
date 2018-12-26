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
			this.pnlMisc = new System.Windows.Forms.Panel();
			this.cbxShowAxes = new System.Windows.Forms.CheckBox();
			this.btnSaveImage = new System.Windows.Forms.Button();
			this.cbxAntialiasing = new System.Windows.Forms.CheckBox();
			this.pnlMisc.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMisc
			// 
			this.pnlMisc.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pnlMisc.Controls.Add(this.cbxShowAxes);
			this.pnlMisc.Controls.Add(this.btnSaveImage);
			this.pnlMisc.Controls.Add(this.cbxAntialiasing);
			this.pnlMisc.Location = new System.Drawing.Point(0, 0);
			this.pnlMisc.Name = "pnlMisc";
			this.pnlMisc.Size = new System.Drawing.Size(173, 77);
			this.pnlMisc.TabIndex = 0;
			// 
			// cbxShowAxes
			// 
			this.cbxShowAxes.AutoSize = true;
			this.cbxShowAxes.Location = new System.Drawing.Point(6, 5);
			this.cbxShowAxes.Name = "cbxShowAxes";
			this.cbxShowAxes.Size = new System.Drawing.Size(78, 17);
			this.cbxShowAxes.TabIndex = 0;
			this.cbxShowAxes.Text = "Show axes";
			this.cbxShowAxes.UseVisualStyleBackColor = true;
			this.cbxShowAxes.CheckedChanged += new System.EventHandler(this.cbxShowAxes_CheckedChanged);
			// 
			// btnSaveImage
			// 
			this.btnSaveImage.Location = new System.Drawing.Point(4, 50);
			this.btnSaveImage.Name = "btnSaveImage";
			this.btnSaveImage.Size = new System.Drawing.Size(165, 23);
			this.btnSaveImage.TabIndex = 2;
			this.btnSaveImage.Text = "Save image";
			this.btnSaveImage.UseVisualStyleBackColor = true;
			this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
			// 
			// cbxAntialiasing
			// 
			this.cbxAntialiasing.AutoSize = true;
			this.cbxAntialiasing.Location = new System.Drawing.Point(6, 28);
			this.cbxAntialiasing.Name = "cbxAntialiasing";
			this.cbxAntialiasing.Size = new System.Drawing.Size(80, 17);
			this.cbxAntialiasing.TabIndex = 1;
			this.cbxAntialiasing.Text = "Antialiasing";
			this.cbxAntialiasing.UseVisualStyleBackColor = true;
			this.cbxAntialiasing.CheckedChanged += new System.EventHandler(this.cbxAntialiasing_CheckedChanged);
			// 
			// MiscControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlMisc);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "MiscControl";
			this.Size = new System.Drawing.Size(173, 77);
			this.pnlMisc.ResumeLayout(false);
			this.pnlMisc.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlMisc;
		private System.Windows.Forms.CheckBox cbxShowAxes;
		private System.Windows.Forms.Button btnSaveImage;
		private System.Windows.Forms.CheckBox cbxAntialiasing;
	}
}
