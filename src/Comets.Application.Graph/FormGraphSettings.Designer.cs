namespace Comets.Application.Graph
{
	partial class FormGraphSettings
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			btnOk = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			valueRangeControl = new ValueRangeControl();
			chartOptionsControl = new ChartOptionsControl();
			chartTypeControl = new ChartTypeControl();
			selectCometControl = new Common.Controls.Common.SelectCometControl();
			timespanControl = new Common.Controls.Common.TimespanControl();
			SuspendLayout();
			// 
			// btnOk
			// 
			btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			btnOk.Location = new System.Drawing.Point(338, 245);
			btnOk.Name = "btnOk";
			btnOk.Size = new System.Drawing.Size(100, 24);
			btnOk.TabIndex = 5;
			btnOk.Text = "OK";
			btnOk.UseVisualStyleBackColor = true;
			btnOk.Click += btnOk_Click;
			// 
			// btnCancel
			// 
			btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.Location = new System.Drawing.Point(444, 245);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(100, 24);
			btnCancel.TabIndex = 6;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// valueRangeControl
			// 
			valueRangeControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			valueRangeControl.Location = new System.Drawing.Point(376, 97);
			valueRangeControl.Name = "valueRangeControl";
			valueRangeControl.Size = new System.Drawing.Size(167, 135);
			valueRangeControl.TabIndex = 4;
			// 
			// chartOptionsControl
			// 
			chartOptionsControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			chartOptionsControl.Location = new System.Drawing.Point(181, 97);
			chartOptionsControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			chartOptionsControl.Name = "chartOptionsControl";
			chartOptionsControl.Size = new System.Drawing.Size(189, 135);
			chartOptionsControl.TabIndex = 3;
			// 
			// chartTypeControl
			// 
			chartTypeControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			chartTypeControl.Location = new System.Drawing.Point(12, 97);
			chartTypeControl.Name = "chartTypeControl";
			chartTypeControl.Size = new System.Drawing.Size(163, 135);
			chartTypeControl.TabIndex = 2;
			// 
			// selectCometControl
			// 
			selectCometControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			selectCometControl.Location = new System.Drawing.Point(12, 6);
			selectCometControl.Name = "selectCometControl";
			selectCometControl.Size = new System.Drawing.Size(290, 85);
			selectCometControl.TabIndex = 0;
			// 
			// timespanControl
			// 
			timespanControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			timespanControl.Location = new System.Drawing.Point(308, 6);
			timespanControl.Name = "timespanControl";
			timespanControl.Size = new System.Drawing.Size(235, 85);
			timespanControl.TabIndex = 1;
			// 
			// FormGraphSettings
			// 
			AcceptButton = btnOk;
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new System.Drawing.Size(556, 281);
			Controls.Add(valueRangeControl);
			Controls.Add(timespanControl);
			Controls.Add(chartOptionsControl);
			Controls.Add(chartTypeControl);
			Controls.Add(selectCometControl);
			Controls.Add(btnOk);
			Controls.Add(btnCancel);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FormGraphSettings";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Graph settings";
			FormClosing += FormGraphSettings_FormClosing;
			Load += FormGraphSettings_Load;
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private Common.Controls.Common.SelectCometControl selectCometControl;
		private ChartTypeControl chartTypeControl;
		private ChartOptionsControl chartOptionsControl;
		private ValueRangeControl valueRangeControl;
		private Common.Controls.Common.TimespanControl timespanControl;
	}
}
