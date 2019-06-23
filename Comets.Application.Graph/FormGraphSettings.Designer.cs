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
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.valueRangeControl = new Comets.Application.Graph.ValueRangeControl();
			this.chartOptionsControl = new Comets.Application.Graph.ChartOptionsControl();
			this.chartTypeControl = new Comets.Application.Graph.ChartTypeControl();
			this.selectCometControl = new Comets.Application.Common.Controls.Common.SelectCometControl();
			this.timespanControl = new Comets.Application.Common.Controls.Common.TimespanControl();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnOk.Location = new System.Drawing.Point(338, 245);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(100, 24);
			this.btnOk.TabIndex = 5;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(444, 245);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 24);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// valueRangeControl
			// 
			this.valueRangeControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.valueRangeControl.Location = new System.Drawing.Point(376, 97);
			this.valueRangeControl.Name = "valueRangeControl";
			this.valueRangeControl.Size = new System.Drawing.Size(167, 135);
			this.valueRangeControl.TabIndex = 4;
			// 
			// chartOptionsControl
			// 
			this.chartOptionsControl.Location = new System.Drawing.Point(181, 97);
			this.chartOptionsControl.Name = "chartOptionsControl";
			this.chartOptionsControl.Size = new System.Drawing.Size(189, 135);
			this.chartOptionsControl.TabIndex = 3;
			// 
			// chartTypeControl
			// 
			this.chartTypeControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.chartTypeControl.Location = new System.Drawing.Point(12, 97);
			this.chartTypeControl.Name = "chartTypeControl";
			this.chartTypeControl.Size = new System.Drawing.Size(163, 135);
			this.chartTypeControl.TabIndex = 2;
			// 
			// selectCometControl
			// 
			this.selectCometControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.selectCometControl.Location = new System.Drawing.Point(12, 6);
			this.selectCometControl.Name = "selectCometControl";
			this.selectCometControl.Size = new System.Drawing.Size(290, 85);
			this.selectCometControl.TabIndex = 0;
			// 
			// timespanControl
			// 
			this.timespanControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.timespanControl.Location = new System.Drawing.Point(308, 6);
			this.timespanControl.Name = "timespanControl";
			this.timespanControl.Size = new System.Drawing.Size(235, 85);
			this.timespanControl.TabIndex = 1;
			// 
			// FormGraphSettings
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(556, 281);
			this.Controls.Add(this.valueRangeControl);
			this.Controls.Add(this.timespanControl);
			this.Controls.Add(this.chartOptionsControl);
			this.Controls.Add(this.chartTypeControl);
			this.Controls.Add(this.selectCometControl);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormGraphSettings";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Graph settings";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGraphSettings_FormClosing);
			this.Load += new System.EventHandler(this.FormGraphSettings_Load);
			this.ResumeLayout(false);

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