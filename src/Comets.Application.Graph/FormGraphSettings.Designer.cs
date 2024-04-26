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
			btnOk = new Button();
			btnCancel = new Button();
			valueRangeControl = new ValueRangeControl();
			chartOptionsControl = new ChartOptionsControl();
			chartTypeControl = new ChartTypeControl();
			selectCometControl = new Common.Controls.Common.SelectCometControl();
			timespanControl = new Common.Controls.Common.TimespanControl();
			SuspendLayout();
			// 
			// btnOk
			// 
			btnOk.AccessibleRole = AccessibleRole.None;
			btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnOk.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			btnOk.Location = new Point(338, 245);
			btnOk.Name = "btnOk";
			btnOk.Size = new Size(100, 24);
			btnOk.TabIndex = 5;
			btnOk.Text = "OK";
			btnOk.UseVisualStyleBackColor = true;
			btnOk.Click += btnOk_Click;
			// 
			// btnCancel
			// 
			btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Location = new Point(444, 245);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(100, 24);
			btnCancel.TabIndex = 6;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// valueRangeControl
			// 
			valueRangeControl.Font = new Font("Tahoma", 8.25F);
			valueRangeControl.Location = new Point(376, 97);
			valueRangeControl.Name = "valueRangeControl";
			valueRangeControl.Size = new Size(167, 135);
			valueRangeControl.TabIndex = 4;
			// 
			// chartOptionsControl
			// 
			chartOptionsControl.Font = new Font("Tahoma", 8.25F);
			chartOptionsControl.Location = new Point(181, 97);
			chartOptionsControl.Margin = new Padding(4, 3, 4, 3);
			chartOptionsControl.Name = "chartOptionsControl";
			chartOptionsControl.Size = new Size(189, 135);
			chartOptionsControl.TabIndex = 3;
			// 
			// chartTypeControl
			// 
			chartTypeControl.Font = new Font("Tahoma", 8.25F);
			chartTypeControl.Location = new Point(12, 97);
			chartTypeControl.Name = "chartTypeControl";
			chartTypeControl.Size = new Size(163, 135);
			chartTypeControl.TabIndex = 2;
			// 
			// selectCometControl
			// 
			selectCometControl.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			selectCometControl.Location = new Point(12, 6);
			selectCometControl.Name = "selectCometControl";
			selectCometControl.Size = new Size(290, 85);
			selectCometControl.TabIndex = 0;
			// 
			// timespanControl
			// 
			timespanControl.Font = new Font("Tahoma", 8.25F);
			timespanControl.Location = new Point(308, 6);
			timespanControl.Name = "timespanControl";
			timespanControl.Size = new Size(235, 85);
			timespanControl.TabIndex = 1;
			// 
			// FormGraphSettings
			// 
			AcceptButton = btnOk;
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new Size(556, 281);
			Controls.Add(valueRangeControl);
			Controls.Add(timespanControl);
			Controls.Add(chartOptionsControl);
			Controls.Add(chartTypeControl);
			Controls.Add(selectCometControl);
			Controls.Add(btnOk);
			Controls.Add(btnCancel);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FormGraphSettings";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
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