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
			pnlMode = new System.Windows.Forms.Panel();
			rbtnSingleMode = new System.Windows.Forms.RadioButton();
			rbtnMultipleMode = new System.Windows.Forms.RadioButton();
			pnlMode.SuspendLayout();
			SuspendLayout();
			// 
			// pnlMode
			// 
			pnlMode.BackColor = System.Drawing.SystemColors.ControlDark;
			pnlMode.Controls.Add(rbtnSingleMode);
			pnlMode.Controls.Add(rbtnMultipleMode);
			pnlMode.Dock = System.Windows.Forms.DockStyle.Fill;
			pnlMode.Font = new System.Drawing.Font("Tahoma", 8.25F);
			pnlMode.Location = new System.Drawing.Point(0, 0);
			pnlMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			pnlMode.Name = "pnlMode";
			pnlMode.Size = new System.Drawing.Size(202, 35);
			pnlMode.TabIndex = 0;
			// 
			// rbtnSingleMode
			// 
			rbtnSingleMode.AutoSize = true;
			rbtnSingleMode.Location = new System.Drawing.Point(27, 8);
			rbtnSingleMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			rbtnSingleMode.Name = "rbtnSingleMode";
			rbtnSingleMode.Size = new System.Drawing.Size(53, 17);
			rbtnSingleMode.TabIndex = 0;
			rbtnSingleMode.Text = "Single";
			rbtnSingleMode.UseVisualStyleBackColor = true;
			rbtnSingleMode.CheckedChanged += rbtnCommon_CheckedChanged;
			// 
			// rbtnMultipleMode
			// 
			rbtnMultipleMode.AutoSize = true;
			rbtnMultipleMode.Location = new System.Drawing.Point(98, 8);
			rbtnMultipleMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			rbtnMultipleMode.Name = "rbtnMultipleMode";
			rbtnMultipleMode.Size = new System.Drawing.Size(61, 17);
			rbtnMultipleMode.TabIndex = 1;
			rbtnMultipleMode.Text = "Multiple";
			rbtnMultipleMode.UseVisualStyleBackColor = true;
			rbtnMultipleMode.CheckedChanged += rbtnCommon_CheckedChanged;
			// 
			// ModeControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(pnlMode);
			Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			Name = "ModeControl";
			Size = new System.Drawing.Size(202, 35);
			pnlMode.ResumeLayout(false);
			pnlMode.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel pnlMode;
		private System.Windows.Forms.RadioButton rbtnSingleMode;
		private System.Windows.Forms.RadioButton rbtnMultipleMode;
	}
}
