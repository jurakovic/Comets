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
			pnlFilterOnDate = new Panel();
			cbxFodSunDist = new CheckBox();
			cbxFodMagnitude = new CheckBox();
			txtFodSunDist = new TextBox();
			cbxFodEarthDist = new CheckBox();
			txtFodEarthDist = new TextBox();
			txtFodMagnitude = new TextBox();
			cbxWeakColor = new CheckBox();
			pnlFilterOnDate.SuspendLayout();
			SuspendLayout();
			// 
			// pnlFilterOnDate
			// 
			pnlFilterOnDate.BackColor = SystemColors.ControlDark;
			pnlFilterOnDate.Controls.Add(cbxFodSunDist);
			pnlFilterOnDate.Controls.Add(cbxFodMagnitude);
			pnlFilterOnDate.Controls.Add(txtFodSunDist);
			pnlFilterOnDate.Controls.Add(cbxFodEarthDist);
			pnlFilterOnDate.Controls.Add(txtFodEarthDist);
			pnlFilterOnDate.Controls.Add(txtFodMagnitude);
			pnlFilterOnDate.Controls.Add(cbxWeakColor);
			pnlFilterOnDate.Dock = DockStyle.Fill;
			pnlFilterOnDate.Location = new Point(0, 0);
			pnlFilterOnDate.Name = "pnlFilterOnDate";
			pnlFilterOnDate.Size = new Size(173, 98);
			pnlFilterOnDate.TabIndex = 0;
			// 
			// cbxFodSunDist
			// 
			cbxFodSunDist.AutoSize = true;
			cbxFodSunDist.Location = new Point(6, 5);
			cbxFodSunDist.Name = "cbxFodSunDist";
			cbxFodSunDist.Size = new Size(113, 17);
			cbxFodSunDist.TabIndex = 0;
			cbxFodSunDist.Text = "Distance from Sun";
			cbxFodSunDist.UseVisualStyleBackColor = true;
			cbxFodSunDist.CheckedChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			// 
			// cbxFodMagnitude
			// 
			cbxFodMagnitude.AutoSize = true;
			cbxFodMagnitude.Location = new Point(6, 53);
			cbxFodMagnitude.Name = "cbxFodMagnitude";
			cbxFodMagnitude.Size = new Size(76, 17);
			cbxFodMagnitude.TabIndex = 4;
			cbxFodMagnitude.Text = "Magnitude";
			cbxFodMagnitude.UseVisualStyleBackColor = true;
			cbxFodMagnitude.CheckedChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			// 
			// txtFodSunDist
			// 
			txtFodSunDist.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			txtFodSunDist.Location = new Point(129, 2);
			txtFodSunDist.Name = "txtFodSunDist";
			txtFodSunDist.Size = new Size(40, 21);
			txtFodSunDist.TabIndex = 1;
			txtFodSunDist.TextChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			txtFodSunDist.KeyPress += txtFilterOnDateCommon_KeyPress;
			// 
			// cbxFodEarthDist
			// 
			cbxFodEarthDist.AutoSize = true;
			cbxFodEarthDist.Location = new Point(6, 29);
			cbxFodEarthDist.Name = "cbxFodEarthDist";
			cbxFodEarthDist.Size = new Size(121, 17);
			cbxFodEarthDist.TabIndex = 2;
			cbxFodEarthDist.Text = "Distance from Earth";
			cbxFodEarthDist.UseVisualStyleBackColor = true;
			cbxFodEarthDist.CheckedChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			// 
			// txtFodEarthDist
			// 
			txtFodEarthDist.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			txtFodEarthDist.Location = new Point(129, 26);
			txtFodEarthDist.Name = "txtFodEarthDist";
			txtFodEarthDist.Size = new Size(40, 21);
			txtFodEarthDist.TabIndex = 3;
			txtFodEarthDist.TextChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			txtFodEarthDist.KeyPress += txtFilterOnDateCommon_KeyPress;
			// 
			// txtFodMagnitude
			// 
			txtFodMagnitude.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			txtFodMagnitude.Location = new Point(129, 50);
			txtFodMagnitude.Name = "txtFodMagnitude";
			txtFodMagnitude.Size = new Size(40, 21);
			txtFodMagnitude.TabIndex = 5;
			txtFodMagnitude.TextChanged += filterOnDateTxtCbxCommon_TextChangedCheckedChanged;
			txtFodMagnitude.KeyPress += txtFilterOnDateCommon_KeyPress;
			// 
			// cbxWeakColor
			// 
			cbxWeakColor.AutoSize = true;
			cbxWeakColor.Checked = true;
			cbxWeakColor.CheckState = CheckState.Checked;
			cbxWeakColor.Location = new Point(6, 77);
			cbxWeakColor.Name = "cbxWeakColor";
			cbxWeakColor.Size = new Size(117, 17);
			cbxWeakColor.TabIndex = 6;
			cbxWeakColor.Text = "Show in weak color";
			cbxWeakColor.UseVisualStyleBackColor = true;
			cbxWeakColor.CheckedChanged += cbxWeakColor_CheckedChanged;
			// 
			// FilterControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(pnlFilterOnDate);
			Font = new Font("Tahoma", 8.25F);
			Name = "FilterControl";
			Size = new Size(173, 98);
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
