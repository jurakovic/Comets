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
			pnlFilterOnDate = new System.Windows.Forms.Panel();
			cbxFodSunDist = new System.Windows.Forms.CheckBox();
			cbxFodMagnitude = new System.Windows.Forms.CheckBox();
			txtFodSunDist = new System.Windows.Forms.TextBox();
			cbxFodEarthDist = new System.Windows.Forms.CheckBox();
			txtFodEarthDist = new System.Windows.Forms.TextBox();
			txtFodMagnitude = new System.Windows.Forms.TextBox();
			cbxWeakColor = new System.Windows.Forms.CheckBox();
			pnlFilterOnDate.SuspendLayout();
			SuspendLayout();
			// 
			// pnlFilterOnDate
			// 
			pnlFilterOnDate.BackColor = System.Drawing.SystemColors.ControlDark;
			pnlFilterOnDate.Controls.Add(cbxFodSunDist);
			pnlFilterOnDate.Controls.Add(cbxFodMagnitude);
			pnlFilterOnDate.Controls.Add(txtFodSunDist);
			pnlFilterOnDate.Controls.Add(cbxFodEarthDist);
			pnlFilterOnDate.Controls.Add(txtFodEarthDist);
			pnlFilterOnDate.Controls.Add(txtFodMagnitude);
			pnlFilterOnDate.Controls.Add(cbxWeakColor);
			pnlFilterOnDate.Dock = System.Windows.Forms.DockStyle.Fill;
			pnlFilterOnDate.Location = new System.Drawing.Point(0, 0);
			pnlFilterOnDate.Name = "pnlFilterOnDate";
			pnlFilterOnDate.Size = new System.Drawing.Size(173, 98);
			pnlFilterOnDate.TabIndex = 0;
			// 
			// cbxFodSunDist
			// 
			cbxFodSunDist.AutoSize = true;
			cbxFodSunDist.Location = new System.Drawing.Point(6, 5);
			cbxFodSunDist.Name = "cbxFodSunDist";
			cbxFodSunDist.Size = new System.Drawing.Size(113, 17);
			cbxFodSunDist.TabIndex = 0;
			cbxFodSunDist.Text = "Distance from Sun";
			cbxFodSunDist.UseVisualStyleBackColor = true;
			cbxFodSunDist.CheckedChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			// 
			// cbxFodMagnitude
			// 
			cbxFodMagnitude.AutoSize = true;
			cbxFodMagnitude.Location = new System.Drawing.Point(6, 53);
			cbxFodMagnitude.Name = "cbxFodMagnitude";
			cbxFodMagnitude.Size = new System.Drawing.Size(76, 17);
			cbxFodMagnitude.TabIndex = 4;
			cbxFodMagnitude.Text = "Magnitude";
			cbxFodMagnitude.UseVisualStyleBackColor = true;
			cbxFodMagnitude.CheckedChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			// 
			// txtFodSunDist
			// 
			txtFodSunDist.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			txtFodSunDist.Location = new System.Drawing.Point(129, 2);
			txtFodSunDist.Name = "txtFodSunDist";
			txtFodSunDist.Size = new System.Drawing.Size(40, 21);
			txtFodSunDist.TabIndex = 1;
			txtFodSunDist.TextChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			txtFodSunDist.KeyPress += txtFilterOnDateCommon_KeyPress;
			// 
			// cbxFodEarthDist
			// 
			cbxFodEarthDist.AutoSize = true;
			cbxFodEarthDist.Location = new System.Drawing.Point(6, 29);
			cbxFodEarthDist.Name = "cbxFodEarthDist";
			cbxFodEarthDist.Size = new System.Drawing.Size(121, 17);
			cbxFodEarthDist.TabIndex = 2;
			cbxFodEarthDist.Text = "Distance from Earth";
			cbxFodEarthDist.UseVisualStyleBackColor = true;
			cbxFodEarthDist.CheckedChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			// 
			// txtFodEarthDist
			// 
			txtFodEarthDist.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			txtFodEarthDist.Location = new System.Drawing.Point(129, 26);
			txtFodEarthDist.Name = "txtFodEarthDist";
			txtFodEarthDist.Size = new System.Drawing.Size(40, 21);
			txtFodEarthDist.TabIndex = 3;
			txtFodEarthDist.TextChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			txtFodEarthDist.KeyPress += txtFilterOnDateCommon_KeyPress;
			// 
			// txtFodMagnitude
			// 
			txtFodMagnitude.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			txtFodMagnitude.Location = new System.Drawing.Point(129, 50);
			txtFodMagnitude.Name = "txtFodMagnitude";
			txtFodMagnitude.Size = new System.Drawing.Size(40, 21);
			txtFodMagnitude.TabIndex = 5;
			txtFodMagnitude.TextChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			txtFodMagnitude.KeyPress += txtFilterOnDateCommon_KeyPress;
			// 
			// cbxWeakColor
			// 
			cbxWeakColor.AutoSize = true;
			cbxWeakColor.Checked = true;
			cbxWeakColor.CheckState = System.Windows.Forms.CheckState.Checked;
			cbxWeakColor.Location = new System.Drawing.Point(6, 77);
			cbxWeakColor.Name = "cbxWeakColor";
			cbxWeakColor.Size = new System.Drawing.Size(117, 17);
			cbxWeakColor.TabIndex = 6;
			cbxWeakColor.Text = "Show in weak color";
			cbxWeakColor.UseVisualStyleBackColor = true;
			cbxWeakColor.CheckedChanged += cbxWeakColor_CheckedChanged;
			// 
			// FilterControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(pnlFilterOnDate);
			Font = new System.Drawing.Font("Tahoma", 8.25F);
			Name = "FilterControl";
			Size = new System.Drawing.Size(173, 98);
			pnlFilterOnDate.ResumeLayout(false);
			pnlFilterOnDate.PerformLayout();
			ResumeLayout(false);
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
