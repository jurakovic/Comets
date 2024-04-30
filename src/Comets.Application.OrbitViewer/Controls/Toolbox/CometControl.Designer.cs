namespace Comets.Application.OrbitViewer.Controls
{
	partial class CometControl
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
			pnlComet = new System.Windows.Forms.Panel();
			cbxLabel = new System.Windows.Forms.CheckBox();
			cbxOrbit = new System.Windows.Forms.CheckBox();
			cbxMarker = new System.Windows.Forms.CheckBox();
			btnMark = new System.Windows.Forms.Button();
			cboComet = new System.Windows.Forms.ComboBox();
			btnFilter = new System.Windows.Forms.Button();
			pnlComet.SuspendLayout();
			SuspendLayout();
			// 
			// pnlComet
			// 
			pnlComet.BackColor = System.Drawing.SystemColors.ControlDark;
			pnlComet.Controls.Add(cbxLabel);
			pnlComet.Controls.Add(cbxOrbit);
			pnlComet.Controls.Add(cbxMarker);
			pnlComet.Controls.Add(btnMark);
			pnlComet.Controls.Add(cboComet);
			pnlComet.Controls.Add(btnFilter);
			pnlComet.Dock = System.Windows.Forms.DockStyle.Fill;
			pnlComet.Location = new System.Drawing.Point(0, 0);
			pnlComet.Name = "pnlComet";
			pnlComet.Size = new System.Drawing.Size(173, 79);
			pnlComet.TabIndex = 0;
			// 
			// cbxLabel
			// 
			cbxLabel.AutoSize = true;
			cbxLabel.Checked = true;
			cbxLabel.CheckState = System.Windows.Forms.CheckState.Checked;
			cbxLabel.Location = new System.Drawing.Point(62, 59);
			cbxLabel.Name = "cbxLabel";
			cbxLabel.Size = new System.Drawing.Size(51, 17);
			cbxLabel.TabIndex = 38;
			cbxLabel.Text = "Label";
			cbxLabel.UseVisualStyleBackColor = true;
			cbxLabel.CheckedChanged += cbxLabel_CheckedChanged;
			// 
			// cbxOrbit
			// 
			cbxOrbit.AutoSize = true;
			cbxOrbit.Checked = true;
			cbxOrbit.CheckState = System.Windows.Forms.CheckState.Checked;
			cbxOrbit.Location = new System.Drawing.Point(5, 59);
			cbxOrbit.Name = "cbxOrbit";
			cbxOrbit.Size = new System.Drawing.Size(50, 17);
			cbxOrbit.TabIndex = 37;
			cbxOrbit.Text = "Orbit";
			cbxOrbit.UseVisualStyleBackColor = true;
			cbxOrbit.CheckedChanged += cbxOrbit_CheckedChanged;
			// 
			// cbxMarker
			// 
			cbxMarker.AutoSize = true;
			cbxMarker.Checked = true;
			cbxMarker.CheckState = System.Windows.Forms.CheckState.Checked;
			cbxMarker.Location = new System.Drawing.Point(116, 59);
			cbxMarker.Name = "cbxMarker";
			cbxMarker.Size = new System.Drawing.Size(59, 17);
			cbxMarker.TabIndex = 39;
			cbxMarker.Text = "Marker";
			cbxMarker.UseVisualStyleBackColor = true;
			cbxMarker.CheckedChanged += cbxMarker_CheckedChanged;
			// 
			// btnMark
			// 
			btnMark.Location = new System.Drawing.Point(91, 30);
			btnMark.Name = "btnMark";
			btnMark.Size = new System.Drawing.Size(79, 23);
			btnMark.TabIndex = 2;
			btnMark.Text = "MARK";
			btnMark.UseVisualStyleBackColor = true;
			btnMark.Click += btnMark_Click;
			// 
			// cboComet
			// 
			cboComet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboComet.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			cboComet.FormattingEnabled = true;
			cboComet.IntegralHeight = false;
			cboComet.Location = new System.Drawing.Point(4, 4);
			cboComet.MaxDropDownItems = 17;
			cboComet.Name = "cboComet";
			cboComet.Size = new System.Drawing.Size(165, 21);
			cboComet.TabIndex = 0;
			cboComet.SelectedIndexChanged += cboObject_SelectedIndexChanged;
			// 
			// btnFilter
			// 
			btnFilter.Location = new System.Drawing.Point(3, 30);
			btnFilter.Name = "btnFilter";
			btnFilter.Size = new System.Drawing.Size(79, 23);
			btnFilter.TabIndex = 1;
			btnFilter.Text = "FILTER";
			btnFilter.UseVisualStyleBackColor = true;
			btnFilter.Click += btnFilter_Click;
			// 
			// CometControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(pnlComet);
			Font = new System.Drawing.Font("Tahoma", 8.25F);
			Name = "CometControl";
			Size = new System.Drawing.Size(173, 79);
			pnlComet.ResumeLayout(false);
			pnlComet.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel pnlComet;
		private System.Windows.Forms.ComboBox cboComet;
		private System.Windows.Forms.Button btnFilter;
		private System.Windows.Forms.Button btnMark;
		private System.Windows.Forms.CheckBox cbxLabel;
		private System.Windows.Forms.CheckBox cbxOrbit;
		private System.Windows.Forms.CheckBox cbxMarker;
	}
}
