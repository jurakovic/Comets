namespace Comets.Application.OrbitViewer.Controls
{
	partial class FilterControl
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
			this.pnlFilterOnDate = new System.Windows.Forms.Panel();
			this.cbxFodSunDist = new System.Windows.Forms.CheckBox();
			this.cbxFodMagnitude = new System.Windows.Forms.CheckBox();
			this.txtFodSunDist = new System.Windows.Forms.TextBox();
			this.cbxFodEarthDist = new System.Windows.Forms.CheckBox();
			this.txtFodEarthDist = new System.Windows.Forms.TextBox();
			this.txtFodMagnitude = new System.Windows.Forms.TextBox();
			this.cbxWeakColor = new System.Windows.Forms.CheckBox();
			this.pnlFilterOnDate.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlFilterOnDate
			// 
			this.pnlFilterOnDate.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pnlFilterOnDate.Controls.Add(this.cbxFodSunDist);
			this.pnlFilterOnDate.Controls.Add(this.cbxFodMagnitude);
			this.pnlFilterOnDate.Controls.Add(this.txtFodSunDist);
			this.pnlFilterOnDate.Controls.Add(this.cbxFodEarthDist);
			this.pnlFilterOnDate.Controls.Add(this.txtFodEarthDist);
			this.pnlFilterOnDate.Controls.Add(this.txtFodMagnitude);
			this.pnlFilterOnDate.Controls.Add(this.cbxWeakColor);
			this.pnlFilterOnDate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlFilterOnDate.Location = new System.Drawing.Point(0, 0);
			this.pnlFilterOnDate.Name = "pnlFilterOnDate";
			this.pnlFilterOnDate.Size = new System.Drawing.Size(173, 98);
			this.pnlFilterOnDate.TabIndex = 0;
			// 
			// cbxFodSunDist
			// 
			this.cbxFodSunDist.AutoSize = true;
			this.cbxFodSunDist.Location = new System.Drawing.Point(6, 5);
			this.cbxFodSunDist.Name = "cbxFodSunDist";
			this.cbxFodSunDist.Size = new System.Drawing.Size(113, 17);
			this.cbxFodSunDist.TabIndex = 0;
			this.cbxFodSunDist.Text = "Distance from Sun";
			this.cbxFodSunDist.UseVisualStyleBackColor = true;
			this.cbxFodSunDist.CheckedChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			// 
			// cbxFodMagnitude
			// 
			this.cbxFodMagnitude.AutoSize = true;
			this.cbxFodMagnitude.Location = new System.Drawing.Point(6, 53);
			this.cbxFodMagnitude.Name = "cbxFodMagnitude";
			this.cbxFodMagnitude.Size = new System.Drawing.Size(76, 17);
			this.cbxFodMagnitude.TabIndex = 4;
			this.cbxFodMagnitude.Text = "Magnitude";
			this.cbxFodMagnitude.UseVisualStyleBackColor = true;
			this.cbxFodMagnitude.CheckedChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			// 
			// txtFodSunDist
			// 
			this.txtFodSunDist.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.txtFodSunDist.Location = new System.Drawing.Point(129, 2);
			this.txtFodSunDist.Name = "txtFodSunDist";
			this.txtFodSunDist.Size = new System.Drawing.Size(40, 21);
			this.txtFodSunDist.TabIndex = 1;
			this.txtFodSunDist.TextChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			this.txtFodSunDist.KeyPress += txtFilterOnDateCommon_KeyPress;
			// 
			// cbxFodEarthDist
			// 
			this.cbxFodEarthDist.AutoSize = true;
			this.cbxFodEarthDist.Location = new System.Drawing.Point(6, 29);
			this.cbxFodEarthDist.Name = "cbxFodEarthDist";
			this.cbxFodEarthDist.Size = new System.Drawing.Size(121, 17);
			this.cbxFodEarthDist.TabIndex = 2;
			this.cbxFodEarthDist.Text = "Distance from Earth";
			this.cbxFodEarthDist.UseVisualStyleBackColor = true;
			this.cbxFodEarthDist.CheckedChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			// 
			// txtFodEarthDist
			// 
			this.txtFodEarthDist.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.txtFodEarthDist.Location = new System.Drawing.Point(129, 26);
			this.txtFodEarthDist.Name = "txtFodEarthDist";
			this.txtFodEarthDist.Size = new System.Drawing.Size(40, 21);
			this.txtFodEarthDist.TabIndex = 3;
			this.txtFodEarthDist.TextChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			this.txtFodEarthDist.KeyPress += txtFilterOnDateCommon_KeyPress;
			// 
			// txtFodMagnitude
			// 
			this.txtFodMagnitude.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.txtFodMagnitude.Location = new System.Drawing.Point(129, 50);
			this.txtFodMagnitude.Name = "txtFodMagnitude";
			this.txtFodMagnitude.Size = new System.Drawing.Size(40, 21);
			this.txtFodMagnitude.TabIndex = 5;
			this.txtFodMagnitude.TextChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			this.txtFodMagnitude.KeyPress += txtFilterOnDateCommon_KeyPress;
			// 
			// cbxWeakColor
			// 
			this.cbxWeakColor.AutoSize = true;
			this.cbxWeakColor.Checked = true;
			this.cbxWeakColor.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxWeakColor.Location = new System.Drawing.Point(6, 77);
			this.cbxWeakColor.Name = "cbxWeakColor";
			this.cbxWeakColor.Size = new System.Drawing.Size(117, 17);
			this.cbxWeakColor.TabIndex = 6;
			this.cbxWeakColor.Text = "Show in weak color";
			this.cbxWeakColor.UseVisualStyleBackColor = true;
			this.cbxWeakColor.CheckedChanged += cbxWeakColor_CheckedChanged;
			// 
			// FilterControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlFilterOnDate);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "FilterControl";
			this.Size = new System.Drawing.Size(173, 98);
			this.pnlFilterOnDate.ResumeLayout(false);
			this.pnlFilterOnDate.PerformLayout();
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel pnlFilterOnDate;
		private System.Windows.Forms.CheckBox cbxFodSunDist;
		private System.Windows.Forms.CheckBox cbxFodMagnitude;
		private System.Windows.Forms.TextBox txtFodSunDist;
		private System.Windows.Forms.CheckBox cbxFodEarthDist;
		private System.Windows.Forms.TextBox txtFodEarthDist;
		private System.Windows.Forms.TextBox txtFodMagnitude;
		private System.Windows.Forms.CheckBox cbxWeakColor;
	}
}
