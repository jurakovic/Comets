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
			this.pnlComet = new System.Windows.Forms.Panel();
			this.cbxLabel = new System.Windows.Forms.CheckBox();
			this.cbxOrbit = new System.Windows.Forms.CheckBox();
			this.cbxMarker = new System.Windows.Forms.CheckBox();
			this.btnMark = new System.Windows.Forms.Button();
			this.cboComet = new System.Windows.Forms.ComboBox();
			this.btnFilter = new System.Windows.Forms.Button();
			this.pnlComet.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlComet
			// 
			this.pnlComet.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pnlComet.Controls.Add(this.cbxLabel);
			this.pnlComet.Controls.Add(this.cbxOrbit);
			this.pnlComet.Controls.Add(this.cbxMarker);
			this.pnlComet.Controls.Add(this.btnMark);
			this.pnlComet.Controls.Add(this.cboComet);
			this.pnlComet.Controls.Add(this.btnFilter);
			this.pnlComet.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlComet.Location = new System.Drawing.Point(0, 0);
			this.pnlComet.Name = "pnlComet";
			this.pnlComet.Size = new System.Drawing.Size(173, 79);
			this.pnlComet.TabIndex = 0;
			// 
			// cbxLabel
			// 
			this.cbxLabel.AutoSize = true;
			this.cbxLabel.Checked = true;
			this.cbxLabel.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxLabel.Location = new System.Drawing.Point(62, 59);
			this.cbxLabel.Name = "cbxLabel";
			this.cbxLabel.Size = new System.Drawing.Size(51, 17);
			this.cbxLabel.TabIndex = 38;
			this.cbxLabel.Text = "Label";
			this.cbxLabel.UseVisualStyleBackColor = true;
			this.cbxLabel.CheckedChanged += new System.EventHandler(this.cbxLabel_CheckedChanged);
			// 
			// cbxOrbit
			// 
			this.cbxOrbit.AutoSize = true;
			this.cbxOrbit.Checked = true;
			this.cbxOrbit.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxOrbit.Location = new System.Drawing.Point(5, 59);
			this.cbxOrbit.Name = "cbxOrbit";
			this.cbxOrbit.Size = new System.Drawing.Size(50, 17);
			this.cbxOrbit.TabIndex = 37;
			this.cbxOrbit.Text = "Orbit";
			this.cbxOrbit.UseVisualStyleBackColor = true;
			this.cbxOrbit.CheckedChanged += new System.EventHandler(this.cbxOrbit_CheckedChanged);
			// 
			// cbxMarker
			// 
			this.cbxMarker.AutoSize = true;
			this.cbxMarker.Checked = true;
			this.cbxMarker.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxMarker.Location = new System.Drawing.Point(116, 59);
			this.cbxMarker.Name = "cbxMarker";
			this.cbxMarker.Size = new System.Drawing.Size(59, 17);
			this.cbxMarker.TabIndex = 39;
			this.cbxMarker.Text = "Marker";
			this.cbxMarker.UseVisualStyleBackColor = true;
			this.cbxMarker.CheckedChanged += new System.EventHandler(this.cbxMarker_CheckedChanged);
			// 
			// btnMark
			// 
			this.btnMark.Location = new System.Drawing.Point(91, 30);
			this.btnMark.Name = "btnMark";
			this.btnMark.Size = new System.Drawing.Size(79, 23);
			this.btnMark.TabIndex = 2;
			this.btnMark.Text = "MARK";
			this.btnMark.UseVisualStyleBackColor = true;
			this.btnMark.Click += new System.EventHandler(this.btnMark_Click);
			// 
			// cboComet
			// 
			this.cboComet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboComet.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.cboComet.FormattingEnabled = true;
			this.cboComet.IntegralHeight = false;
			this.cboComet.Location = new System.Drawing.Point(4, 4);
			this.cboComet.MaxDropDownItems = 17;
			this.cboComet.Name = "cboComet";
			this.cboComet.Size = new System.Drawing.Size(165, 21);
			this.cboComet.TabIndex = 0;
			this.cboComet.SelectedIndexChanged += new System.EventHandler(this.cboObject_SelectedIndexChanged);
			// 
			// btnFilter
			// 
			this.btnFilter.Location = new System.Drawing.Point(3, 30);
			this.btnFilter.Name = "btnFilter";
			this.btnFilter.Size = new System.Drawing.Size(79, 23);
			this.btnFilter.TabIndex = 1;
			this.btnFilter.Text = "FILTER";
			this.btnFilter.UseVisualStyleBackColor = true;
			this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
			// 
			// CometControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlComet);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "CometControl";
			this.Size = new System.Drawing.Size(173, 79);
			this.pnlComet.ResumeLayout(false);
			this.pnlComet.PerformLayout();
			this.ResumeLayout(false);

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
