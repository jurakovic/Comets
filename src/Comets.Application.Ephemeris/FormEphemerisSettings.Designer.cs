namespace Comets.Application.Ephemeris
{
	partial class FormEphemerisSettings
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
			selectCometControl = new Common.Controls.Common.SelectCometControl();
			timespanControl = new Common.Controls.Common.TimespanControl();
			outputDataControl = new OutputDataControl();
			requirementsControl = new RequirementsControl();
			intervalControl = new Controls.IntervalControl();
			SuspendLayout();
			// 
			// btnOk
			// 
			btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			btnOk.Location = new System.Drawing.Point(548, 245);
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
			btnCancel.Location = new System.Drawing.Point(654, 245);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(100, 24);
			btnCancel.TabIndex = 6;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
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
			timespanControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			timespanControl.Location = new System.Drawing.Point(308, 6);
			timespanControl.Name = "timespanControl";
			timespanControl.Size = new System.Drawing.Size(235, 85);
			timespanControl.TabIndex = 1;
			// 
			// outputDataControl
			// 
			outputDataControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			outputDataControl.Location = new System.Drawing.Point(12, 97);
			outputDataControl.Name = "outputDataControl";
			outputDataControl.Size = new System.Drawing.Size(530, 137);
			outputDataControl.TabIndex = 3;
			// 
			// requirementsControl
			// 
			requirementsControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			requirementsControl.Location = new System.Drawing.Point(548, 97);
			requirementsControl.Name = "requirementsControl";
			requirementsControl.Size = new System.Drawing.Size(204, 137);
			requirementsControl.TabIndex = 4;
			// 
			// intervalControl
			// 
			intervalControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			intervalControl.Location = new System.Drawing.Point(549, 6);
			intervalControl.Name = "intervalControl";
			intervalControl.Size = new System.Drawing.Size(204, 85);
			intervalControl.TabIndex = 2;
			// 
			// FormEphemerisSettings
			// 
			AcceptButton = btnOk;
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new System.Drawing.Size(766, 281);
			Controls.Add(intervalControl);
			Controls.Add(requirementsControl);
			Controls.Add(outputDataControl);
			Controls.Add(timespanControl);
			Controls.Add(selectCometControl);
			Controls.Add(btnOk);
			Controls.Add(btnCancel);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FormEphemerisSettings";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Ephemeris settings";
			FormClosing += FormEphemerisSettings_FormClosing;
			Load += FormEphemerisSettings_Load;
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private Comets.Application.Common.Controls.Common.SelectCometControl selectCometControl;
		private Comets.Application.Common.Controls.Common.TimespanControl timespanControl;
		private OutputDataControl outputDataControl;
		private RequirementsControl requirementsControl;
		private Controls.IntervalControl intervalControl;
	}
}
