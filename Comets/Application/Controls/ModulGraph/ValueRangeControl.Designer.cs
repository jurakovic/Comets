namespace Comets.Application.Controls.ModulGraph
{
	partial class ValueRangeControl
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
			this.gbxValueRange = new System.Windows.Forms.GroupBox();
			this.txtMaxValue = new System.Windows.Forms.TextBox();
			this.txtMinValue = new System.Windows.Forms.TextBox();
			this.cbxMaxValue = new System.Windows.Forms.CheckBox();
			this.cbxMinValue = new System.Windows.Forms.CheckBox();
			this.gbxValueRange.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxValueRange
			// 
			this.gbxValueRange.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxValueRange.Controls.Add(this.txtMaxValue);
			this.gbxValueRange.Controls.Add(this.txtMinValue);
			this.gbxValueRange.Controls.Add(this.cbxMaxValue);
			this.gbxValueRange.Controls.Add(this.cbxMinValue);
			this.gbxValueRange.Location = new System.Drawing.Point(0, 0);
			this.gbxValueRange.Name = "gbxValueRange";
			this.gbxValueRange.Size = new System.Drawing.Size(153, 83);
			this.gbxValueRange.TabIndex = 0;
			this.gbxValueRange.TabStop = false;
			this.gbxValueRange.Text = "Value range";
			// 
			// txtMaxValue
			// 
			this.txtMaxValue.Location = new System.Drawing.Point(99, 47);
			this.txtMaxValue.Name = "txtMaxValue";
			this.txtMaxValue.Size = new System.Drawing.Size(45, 21);
			this.txtMaxValue.TabIndex = 3;
			this.txtMaxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMaxValue.TextChanged += new System.EventHandler(this.txtMaxMag_TextChanged);
			this.txtMaxValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMagCommon_KeyPress);
			// 
			// txtMinValue
			// 
			this.txtMinValue.Location = new System.Drawing.Point(99, 20);
			this.txtMinValue.Name = "txtMinValue";
			this.txtMinValue.Size = new System.Drawing.Size(45, 21);
			this.txtMinValue.TabIndex = 1;
			this.txtMinValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMinValue.TextChanged += new System.EventHandler(this.txtMinMag_TextChanged);
			this.txtMinValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMagCommon_KeyPress);
			// 
			// cbxMaxValue
			// 
			this.cbxMaxValue.AutoSize = true;
			this.cbxMaxValue.Location = new System.Drawing.Point(15, 48);
			this.cbxMaxValue.Name = "cbxMaxValue";
			this.cbxMaxValue.Size = new System.Drawing.Size(70, 17);
			this.cbxMaxValue.TabIndex = 2;
			this.cbxMaxValue.Text = "Maximum";
			this.cbxMaxValue.UseVisualStyleBackColor = true;
			// 
			// cbxMinValue
			// 
			this.cbxMinValue.AutoSize = true;
			this.cbxMinValue.Location = new System.Drawing.Point(15, 21);
			this.cbxMinValue.Name = "cbxMinValue";
			this.cbxMinValue.Size = new System.Drawing.Size(66, 17);
			this.cbxMinValue.TabIndex = 0;
			this.cbxMinValue.Text = "Minimum";
			this.cbxMinValue.UseVisualStyleBackColor = true;
			// 
			// ValueRangeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbxValueRange);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "ValueRangeControl";
			this.Size = new System.Drawing.Size(153, 83);
			this.gbxValueRange.ResumeLayout(false);
			this.gbxValueRange.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbxValueRange;
		private System.Windows.Forms.TextBox txtMaxValue;
		private System.Windows.Forms.TextBox txtMinValue;
		private System.Windows.Forms.CheckBox cbxMaxValue;
		private System.Windows.Forms.CheckBox cbxMinValue;
	}
}
