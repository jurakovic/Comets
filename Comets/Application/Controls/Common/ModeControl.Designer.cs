namespace Comets.Application.Controls.Common
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
			this.gbxMode = new System.Windows.Forms.GroupBox();
			this.lblCometCount = new System.Windows.Forms.Label();
			this.rbtnMultiple = new System.Windows.Forms.RadioButton();
			this.rbtnSingle = new System.Windows.Forms.RadioButton();
			this.gbxMode.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxMode
			// 
			this.gbxMode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxMode.Controls.Add(this.lblCometCount);
			this.gbxMode.Controls.Add(this.rbtnMultiple);
			this.gbxMode.Controls.Add(this.rbtnSingle);
			this.gbxMode.Location = new System.Drawing.Point(0, 0);
			this.gbxMode.Name = "gbxMode";
			this.gbxMode.Size = new System.Drawing.Size(106, 135);
			this.gbxMode.TabIndex = 0;
			this.gbxMode.TabStop = false;
			this.gbxMode.Text = "Mode";
			// 
			// lblMultipleCount
			// 
			this.lblCometCount.AutoSize = true;
			this.lblCometCount.Location = new System.Drawing.Point(30, 77);
			this.lblCometCount.Name = "lblMultipleCount";
			this.lblCometCount.Size = new System.Drawing.Size(51, 13);
			this.lblCometCount.TabIndex = 2;
			this.lblCometCount.Text = "N comets";
			// 
			// rbtnMultiple
			// 
			this.rbtnMultiple.AutoSize = true;
			this.rbtnMultiple.Location = new System.Drawing.Point(14, 47);
			this.rbtnMultiple.Name = "rbtnMultiple";
			this.rbtnMultiple.Size = new System.Drawing.Size(61, 17);
			this.rbtnMultiple.TabIndex = 1;
			this.rbtnMultiple.Text = "Multiple";
			this.rbtnMultiple.UseVisualStyleBackColor = true;
			// 
			// rbtnSingle
			// 
			this.rbtnSingle.AutoSize = true;
			this.rbtnSingle.Checked = true;
			this.rbtnSingle.Location = new System.Drawing.Point(14, 21);
			this.rbtnSingle.Name = "rbtnSingle";
			this.rbtnSingle.Size = new System.Drawing.Size(53, 17);
			this.rbtnSingle.TabIndex = 0;
			this.rbtnSingle.TabStop = true;
			this.rbtnSingle.Text = "Single";
			this.rbtnSingle.UseVisualStyleBackColor = true;
			// 
			// ModeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbxMode);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ModeControl";
			this.Size = new System.Drawing.Size(106, 135);
			this.gbxMode.ResumeLayout(false);
			this.gbxMode.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbxMode;
		private System.Windows.Forms.Label lblCometCount;
		private System.Windows.Forms.RadioButton rbtnMultiple;
		private System.Windows.Forms.RadioButton rbtnSingle;
	}
}
