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
			pnlComet = new Panel();
			cbxLabel = new CheckBox();
			cbxOrbit = new CheckBox();
			cbxMarker = new CheckBox();
			btnMark = new Button();
			cboComet = new ComboBox();
			btnFilter = new Button();
			pnlComet.SuspendLayout();
			SuspendLayout();
			// 
			// pnlComet
			// 
			pnlComet.BackColor = SystemColors.ControlDark;
			pnlComet.Controls.Add(cbxLabel);
			pnlComet.Controls.Add(cbxOrbit);
			pnlComet.Controls.Add(cbxMarker);
			pnlComet.Controls.Add(btnMark);
			pnlComet.Controls.Add(cboComet);
			pnlComet.Controls.Add(btnFilter);
			pnlComet.Dock = DockStyle.Fill;
			pnlComet.Location = new Point(0, 0);
			pnlComet.Name = "pnlComet";
			pnlComet.Size = new Size(173, 79);
			pnlComet.TabIndex = 0;
			// 
			// cbxLabel
			// 
			cbxLabel.AutoSize = true;
			cbxLabel.Checked = true;
			cbxLabel.CheckState = CheckState.Checked;
			cbxLabel.Location = new Point(62, 59);
			cbxLabel.Name = "cbxLabel";
			cbxLabel.Size = new Size(51, 17);
			cbxLabel.TabIndex = 38;
			cbxLabel.Text = "Label";
			cbxLabel.UseVisualStyleBackColor = true;
			cbxLabel.CheckedChanged += cbxLabel_CheckedChanged;
			// 
			// cbxOrbit
			// 
			cbxOrbit.AutoSize = true;
			cbxOrbit.Checked = true;
			cbxOrbit.CheckState = CheckState.Checked;
			cbxOrbit.Location = new Point(5, 59);
			cbxOrbit.Name = "cbxOrbit";
			cbxOrbit.Size = new Size(50, 17);
			cbxOrbit.TabIndex = 37;
			cbxOrbit.Text = "Orbit";
			cbxOrbit.UseVisualStyleBackColor = true;
			cbxOrbit.CheckedChanged += cbxOrbit_CheckedChanged;
			// 
			// cbxMarker
			// 
			cbxMarker.AutoSize = true;
			cbxMarker.Checked = true;
			cbxMarker.CheckState = CheckState.Checked;
			cbxMarker.Location = new Point(116, 59);
			cbxMarker.Name = "cbxMarker";
			cbxMarker.Size = new Size(59, 17);
			cbxMarker.TabIndex = 39;
			cbxMarker.Text = "Marker";
			cbxMarker.UseVisualStyleBackColor = true;
			cbxMarker.CheckedChanged += cbxMarker_CheckedChanged;
			// 
			// btnMark
			// 
			btnMark.Location = new Point(91, 30);
			btnMark.Name = "btnMark";
			btnMark.Size = new Size(79, 23);
			btnMark.TabIndex = 2;
			btnMark.Text = "MARK";
			btnMark.UseVisualStyleBackColor = true;
			btnMark.Click += btnMark_Click;
			// 
			// cboComet
			// 
			cboComet.DropDownStyle = ComboBoxStyle.DropDownList;
			cboComet.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			cboComet.FormattingEnabled = true;
			cboComet.IntegralHeight = false;
			cboComet.Location = new Point(4, 4);
			cboComet.MaxDropDownItems = 17;
			cboComet.Name = "cboComet";
			cboComet.Size = new Size(165, 21);
			cboComet.TabIndex = 0;
			cboComet.SelectedIndexChanged += cboObject_SelectedIndexChanged;
			// 
			// btnFilter
			// 
			btnFilter.Location = new Point(3, 30);
			btnFilter.Name = "btnFilter";
			btnFilter.Size = new Size(79, 23);
			btnFilter.TabIndex = 1;
			btnFilter.Text = "FILTER";
			btnFilter.UseVisualStyleBackColor = true;
			btnFilter.Click += btnFilter_Click;
			// 
			// CometControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(pnlComet);
			Font = new Font("Tahoma", 8.25F);
			Name = "CometControl";
			Size = new Size(173, 79);
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
