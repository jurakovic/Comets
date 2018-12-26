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
			this.cboComet = new System.Windows.Forms.ComboBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnFilter = new System.Windows.Forms.Button();
			this.btnAll = new System.Windows.Forms.Button();
			this.pnlComet.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlComet
			// 
			this.pnlComet.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pnlComet.Controls.Add(this.cboComet);
			this.pnlComet.Controls.Add(this.btnClear);
			this.pnlComet.Controls.Add(this.btnFilter);
			this.pnlComet.Controls.Add(this.btnAll);
			this.pnlComet.Location = new System.Drawing.Point(0, 0);
			this.pnlComet.Name = "pnlComet";
			this.pnlComet.Size = new System.Drawing.Size(173, 57);
			this.pnlComet.TabIndex = 0;
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
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(116, 30);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(53, 23);
			this.btnClear.TabIndex = 3;
			this.btnClear.Text = "CLEAR";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnFilter
			// 
			this.btnFilter.Location = new System.Drawing.Point(3, 30);
			this.btnFilter.Name = "btnFilter";
			this.btnFilter.Size = new System.Drawing.Size(53, 23);
			this.btnFilter.TabIndex = 1;
			this.btnFilter.Text = "FILTER";
			this.btnFilter.UseVisualStyleBackColor = true;
			this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
			// 
			// btnAll
			// 
			this.btnAll.Location = new System.Drawing.Point(60, 30);
			this.btnAll.Name = "btnAll";
			this.btnAll.Size = new System.Drawing.Size(53, 23);
			this.btnAll.TabIndex = 2;
			this.btnAll.Text = "ALL";
			this.btnAll.UseVisualStyleBackColor = true;
			this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
			// 
			// CometControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlComet);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "CometControl";
			this.Size = new System.Drawing.Size(173, 57);
			this.pnlComet.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlComet;
		private System.Windows.Forms.ComboBox cboComet;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnFilter;
		private System.Windows.Forms.Button btnAll;
	}
}
