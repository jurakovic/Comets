namespace Comets.Application.Graph
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
			gbxValueRange = new GroupBox();
			txtMaxValue = new TextBox();
			txtMinValue = new TextBox();
			cbxMaxValue = new CheckBox();
			cbxMinValue = new CheckBox();
			gbxValueRange.SuspendLayout();
			SuspendLayout();
			// 
			// gbxValueRange
			// 
			gbxValueRange.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			gbxValueRange.Controls.Add(txtMaxValue);
			gbxValueRange.Controls.Add(txtMinValue);
			gbxValueRange.Controls.Add(cbxMaxValue);
			gbxValueRange.Controls.Add(cbxMinValue);
			gbxValueRange.Location = new Point(0, 0);
			gbxValueRange.Name = "gbxValueRange";
			gbxValueRange.Size = new Size(153, 135);
			gbxValueRange.TabIndex = 0;
			gbxValueRange.TabStop = false;
			gbxValueRange.Text = "Value range";
			// 
			// txtMaxValue
			// 
			txtMaxValue.Location = new Point(99, 47);
			txtMaxValue.Name = "txtMaxValue";
			txtMaxValue.Size = new Size(45, 21);
			txtMaxValue.TabIndex = 3;
			txtMaxValue.Text = "12";
			txtMaxValue.TextAlign = HorizontalAlignment.Right;
			txtMaxValue.TextChanged += txtMaxMag_TextChanged;
			txtMaxValue.KeyPress += txtMagCommon_KeyPress;
			// 
			// txtMinValue
			// 
			txtMinValue.Location = new Point(99, 20);
			txtMinValue.Name = "txtMinValue";
			txtMinValue.Size = new Size(45, 21);
			txtMinValue.TabIndex = 1;
			txtMinValue.TextAlign = HorizontalAlignment.Right;
			txtMinValue.TextChanged += txtMinMag_TextChanged;
			txtMinValue.KeyPress += txtMagCommon_KeyPress;
			// 
			// cbxMaxValue
			// 
			cbxMaxValue.AutoSize = true;
			cbxMaxValue.Checked = true;
			cbxMaxValue.CheckState = CheckState.Checked;
			cbxMaxValue.Location = new Point(15, 48);
			cbxMaxValue.Name = "cbxMaxValue";
			cbxMaxValue.Size = new Size(70, 17);
			cbxMaxValue.TabIndex = 2;
			cbxMaxValue.Text = "Maximum";
			cbxMaxValue.UseVisualStyleBackColor = true;
			// 
			// cbxMinValue
			// 
			cbxMinValue.AutoSize = true;
			cbxMinValue.Location = new Point(15, 21);
			cbxMinValue.Name = "cbxMinValue";
			cbxMinValue.Size = new Size(66, 17);
			cbxMinValue.TabIndex = 0;
			cbxMinValue.Text = "Minimum";
			cbxMinValue.UseVisualStyleBackColor = true;
			// 
			// ValueRangeControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(gbxValueRange);
			Font = new Font("Tahoma", 8.25F);
			Name = "ValueRangeControl";
			Size = new Size(153, 135);
			gbxValueRange.ResumeLayout(false);
			gbxValueRange.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.GroupBox gbxValueRange;
		private System.Windows.Forms.TextBox txtMaxValue;
		private System.Windows.Forms.TextBox txtMinValue;
		private System.Windows.Forms.CheckBox cbxMaxValue;
		private System.Windows.Forms.CheckBox cbxMinValue;
	}
}
