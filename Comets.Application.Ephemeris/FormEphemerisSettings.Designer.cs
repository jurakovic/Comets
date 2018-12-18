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
			this.modeControl = new Comets.Application.Common.Controls.Common.ModeControl();
			this.timespanControl = new Comets.Application.Ephemeris.TimespanControl();
			this.outputDataControl = new Comets.Application.Ephemeris.OutputDataControl();
			this.requirementsControl = new Comets.Application.Ephemeris.RequirementsControl();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnOk.Location = new System.Drawing.Point(539, 308);
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
			this.btnCancel.Location = new System.Drawing.Point(645, 308);
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
			this.selectCometControl.Size = new System.Drawing.Size(325, 135);
			this.selectCometControl.TabIndex = 0;
			// 
			// modeControl
			// 
			this.modeControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.modeControl.Location = new System.Drawing.Point(343, 6);
			this.modeControl.Name = "modeControl";
			this.modeControl.Size = new System.Drawing.Size(106, 135);
			this.modeControl.TabIndex = 1;
			// 
			// timespanControl
			// 
			this.timespanControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timespanControl.Location = new System.Drawing.Point(455, 6);
			this.timespanControl.Name = "timespanControl";
			this.timespanControl.Size = new System.Drawing.Size(288, 135);
			this.timespanControl.TabIndex = 2;
			// 
			// outputDataControl
			// 
			this.outputDataControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.outputDataControl.Location = new System.Drawing.Point(12, 147);
			this.outputDataControl.Name = "outputDataControl";
			this.outputDataControl.Size = new System.Drawing.Size(526, 149);
			this.outputDataControl.TabIndex = 3;
			// 
			// requirementsControl1
			// 
			this.requirementsControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.requirementsControl.Location = new System.Drawing.Point(544, 147);
			this.requirementsControl.Name = "requirementsControl1";
			this.requirementsControl.Size = new System.Drawing.Size(199, 149);
			this.requirementsControl.TabIndex = 4;
			// 
			// FormEphemerisSettings
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(757, 344);
			this.Controls.Add(this.requirementsControl);
			this.Controls.Add(this.outputDataControl);
			this.Controls.Add(this.timespanControl);
			this.Controls.Add(this.modeControl);
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
		private Comets.Application.Common.Controls.Common.ModeControl modeControl;
		private TimespanControl timespanControl;
		private OutputDataControl outputDataControl;
		private RequirementsControl requirementsControl;
	}
}