namespace Comets.Application.OrbitViewer.Controls
{
	partial class ModeControl
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
			this.pnlMode = new System.Windows.Forms.Panel();
			this.rbtnSingleMode = new System.Windows.Forms.RadioButton();
			this.rbtnMultipleMode = new System.Windows.Forms.RadioButton();
			this.pnlMode.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMode
			// 
			this.pnlMode.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pnlMode.Controls.Add(this.rbtnSingleMode);
			this.pnlMode.Controls.Add(this.rbtnMultipleMode);
			this.pnlMode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMode.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.pnlMode.Location = new System.Drawing.Point(0, 0);
			this.pnlMode.Name = "pnlMode";
			this.pnlMode.Size = new System.Drawing.Size(173, 30);
			this.pnlMode.TabIndex = 0;
			// 
			// rbtnSingleMode
			// 
			this.rbtnSingleMode.AutoSize = true;
			this.rbtnSingleMode.Location = new System.Drawing.Point(23, 7);
			this.rbtnSingleMode.Name = "rbtnSingleMode";
			this.rbtnSingleMode.Size = new System.Drawing.Size(53, 17);
			this.rbtnSingleMode.TabIndex = 0;
			this.rbtnSingleMode.Text = "Single";
			this.rbtnSingleMode.UseVisualStyleBackColor = true;
			this.rbtnSingleMode.CheckedChanged += new System.EventHandler(this.rbtnCommon_CheckedChanged);
			// 
			// rbtnMultipleMode
			// 
			this.rbtnMultipleMode.AutoSize = true;
			this.rbtnMultipleMode.Location = new System.Drawing.Point(84, 7);
			this.rbtnMultipleMode.Name = "rbtnMultipleMode";
			this.rbtnMultipleMode.Size = new System.Drawing.Size(61, 17);
			this.rbtnMultipleMode.TabIndex = 1;
			this.rbtnMultipleMode.Text = "Multiple";
			this.rbtnMultipleMode.UseVisualStyleBackColor = true;
			this.rbtnMultipleMode.CheckedChanged += new System.EventHandler(this.rbtnCommon_CheckedChanged);
			// 
			// ModeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlMode);
			this.Name = "ModeControl";
			this.Size = new System.Drawing.Size(173, 30);
			this.pnlMode.ResumeLayout(false);
			this.pnlMode.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlMode;
		private System.Windows.Forms.RadioButton rbtnSingleMode;
		private System.Windows.Forms.RadioButton rbtnMultipleMode;
	}
}
