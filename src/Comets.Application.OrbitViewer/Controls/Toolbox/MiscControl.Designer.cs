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
			this.lblExtent = new System.Windows.Forms.Label();
			this.txtGridExtent = new System.Windows.Forms.TextBox();
			this.cbxShowAxes = new System.Windows.Forms.CheckBox();
			this.cbxShowGrid = new System.Windows.Forms.CheckBox();
			this.btnSaveImage = new System.Windows.Forms.Button();
			this.pnlMisc.SuspendLayout();
			this.SuspendLayout();
			//
			// pnlMisc
			//
			this.pnlMisc.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pnlMisc.Controls.Add(this.cbxShowAxes);
			this.pnlMisc.Controls.Add(this.cbxShowGrid);
			this.pnlMisc.Controls.Add(this.lblExtent);
			this.pnlMisc.Controls.Add(this.txtGridExtent);
			this.pnlMisc.Controls.Add(this.btnSaveImage);
			this.pnlMisc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMisc.Location = new System.Drawing.Point(0, 0);
			this.pnlMisc.Name = "pnlMisc";
			this.pnlMisc.Size = new System.Drawing.Size(173, 100);
			this.pnlMisc.TabIndex = 0;
			//
			// cbxShowAxes
			//
			this.cbxShowAxes.AutoSize = true;
			this.cbxShowAxes.Location = new System.Drawing.Point(6, 4);
			this.cbxShowAxes.Name = "cbxShowAxes";
			this.cbxShowAxes.Size = new System.Drawing.Size(78, 17);
			this.cbxShowAxes.TabIndex = 0;
			this.cbxShowAxes.Text = "Show axes";
			this.cbxShowAxes.UseVisualStyleBackColor = true;
			this.cbxShowAxes.CheckedChanged += this.cbxShowAxes_CheckedChanged;
			//
			// cbxShowGrid
			//
			this.cbxShowGrid.AutoSize = true;
			this.cbxShowGrid.Location = new System.Drawing.Point(6, 27);
			this.cbxShowGrid.Name = "cbxShowGrid";
			this.cbxShowGrid.Size = new System.Drawing.Size(72, 17);
			this.cbxShowGrid.TabIndex = 1;
			this.cbxShowGrid.Text = "Show grid";
			this.cbxShowGrid.UseVisualStyleBackColor = true;
			this.cbxShowGrid.CheckedChanged += this.cbxShowGrid_CheckedChanged;
			//
			// lblExtent
			//
			this.lblExtent.AutoSize = true;
			this.lblExtent.Location = new System.Drawing.Point(6, 53);
			this.lblExtent.Name = "lblExtent";
			this.lblExtent.Text = "Extent";
			//
			// txtGridExtent
			//
			this.txtGridExtent.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.txtGridExtent.Location = new System.Drawing.Point(129, 50);
			this.txtGridExtent.Name = "txtGridExtent";
			this.txtGridExtent.Size = new System.Drawing.Size(40, 20);
			this.txtGridExtent.TabIndex = 2;
			this.txtGridExtent.Text = "150";
			this.txtGridExtent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtGridExtent.TextChanged += this.txtGridExtent_TextChanged;
			this.txtGridExtent.KeyDown += this.txtGridExtent_KeyDown;
			this.txtGridExtent.KeyPress += this.txtGridExtent_KeyPress;
			this.txtGridExtent.Leave += this.txtGridExtent_Leave;
			//
			// btnSaveImage
			//
			this.btnSaveImage.Location = new System.Drawing.Point(4, 73);
			this.btnSaveImage.Name = "btnSaveImage";
			this.btnSaveImage.Size = new System.Drawing.Size(165, 23);
			this.btnSaveImage.TabIndex = 3;
			this.btnSaveImage.Text = "Save image";
			this.btnSaveImage.UseVisualStyleBackColor = true;
			this.btnSaveImage.Click += this.btnSaveImage_Click;
			//
			// MiscControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlMisc);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "MiscControl";
			this.Size = new System.Drawing.Size(173, 100);
			this.pnlMisc.ResumeLayout(false);
			this.pnlMisc.PerformLayout();
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel pnlMisc;
		private System.Windows.Forms.CheckBox cbxShowAxes;
		private System.Windows.Forms.CheckBox cbxShowGrid;
		private System.Windows.Forms.Label lblExtent;
		private System.Windows.Forms.TextBox txtGridExtent;
		private System.Windows.Forms.Button btnSaveImage;
	}
}
