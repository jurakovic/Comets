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
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.selectCometControl = new Comets.Application.Common.Controls.Common.SelectCometControl();
			this.timespanControl = new Comets.Application.Common.Controls.Common.TimespanControl();
			this.outputDataControl = new Comets.Application.Ephemeris.OutputDataControl();
			this.requirementsControl = new Comets.Application.Ephemeris.RequirementsControl();
			this.intervalControl = new Comets.Application.Ephemeris.Controls.IntervalControl();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnOk.Location = new System.Drawing.Point(548, 245);
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
			this.btnCancel.Location = new System.Drawing.Point(654, 245);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 24);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
			this.timespanControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timespanControl.Location = new System.Drawing.Point(308, 6);
			this.timespanControl.Name = "timespanControl";
			this.timespanControl.Size = new System.Drawing.Size(235, 85);
			this.timespanControl.TabIndex = 1;
			// 
			// outputDataControl
			// 
			this.outputDataControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.outputDataControl.Location = new System.Drawing.Point(12, 97);
			this.outputDataControl.Name = "outputDataControl";
			this.outputDataControl.Size = new System.Drawing.Size(530, 137);
			this.outputDataControl.TabIndex = 3;
			// 
			// requirementsControl
			// 
			this.requirementsControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.requirementsControl.Location = new System.Drawing.Point(548, 97);
			this.requirementsControl.Name = "requirementsControl";
			this.requirementsControl.Size = new System.Drawing.Size(204, 137);
			this.requirementsControl.TabIndex = 4;
			// 
			// intervalControl
			// 
			this.intervalControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.intervalControl.Location = new System.Drawing.Point(549, 6);
			this.intervalControl.Name = "intervalControl";
			this.intervalControl.Size = new System.Drawing.Size(204, 85);
			this.intervalControl.TabIndex = 2;
			// 
			// FormEphemerisSettings
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(766, 281);
			this.Controls.Add(this.intervalControl);
			this.Controls.Add(this.requirementsControl);
			this.Controls.Add(this.outputDataControl);
			this.Controls.Add(this.timespanControl);
			this.Controls.Add(this.selectCometControl);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEphemerisSettings";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Ephemeris settings";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEphemerisSettings_FormClosing);
			this.Load += new System.EventHandler(this.FormEphemerisSettings_Load);
			this.ResumeLayout(false);

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