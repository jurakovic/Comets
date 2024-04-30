namespace Comets.Application
{
	partial class FormElements
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
			this.cboFormat = new System.Windows.Forms.ComboBox();
			this.lblFormat = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.pnlElements = new System.Windows.Forms.Panel();
			this.btnClose = new System.Windows.Forms.Button();
			this.rtxtElements = new System.Windows.Forms.RichTextBox();
			this.pnlElements.SuspendLayout();
			this.SuspendLayout();
			// 
			// cboFormat
			// 
			this.cboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFormat.FormattingEnabled = true;
			this.cboFormat.Location = new System.Drawing.Point(62, 9);
			this.cboFormat.Name = "cboFormat";
			this.cboFormat.Size = new System.Drawing.Size(280, 21);
			this.cboFormat.TabIndex = 1;
			this.cboFormat.SelectedIndexChanged += this.cboFormat_SelectedIndexChanged;
			// 
			// lblFormat
			// 
			this.lblFormat.AutoSize = true;
			this.lblFormat.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.lblFormat.Location = new System.Drawing.Point(8, 12);
			this.lblFormat.Name = "lblFormat";
			this.lblFormat.Size = new System.Drawing.Size(45, 13);
			this.lblFormat.TabIndex = 0;
			this.lblFormat.Text = "Format:";
			// 
			// btnSave
			// 
			this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.btnSave.Location = new System.Drawing.Point(351, 8);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(97, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Save As";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += this.btnSave_Click;
			// 
			// pnlElements
			// 
			this.pnlElements.Controls.Add(this.btnClose);
			this.pnlElements.Controls.Add(this.lblFormat);
			this.pnlElements.Controls.Add(this.btnSave);
			this.pnlElements.Controls.Add(this.cboFormat);
			this.pnlElements.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlElements.Location = new System.Drawing.Point(0, 0);
			this.pnlElements.Name = "pnlElements";
			this.pnlElements.Size = new System.Drawing.Size(1350, 40);
			this.pnlElements.TabIndex = 0;
			// 
			// btnClose
			// 
			this.btnClose.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.btnClose.Location = new System.Drawing.Point(454, 8);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(97, 23);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += this.btnClose_Click;
			// 
			// rtxtElements
			// 
			this.rtxtElements.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxtElements.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.rtxtElements.Location = new System.Drawing.Point(0, 40);
			this.rtxtElements.Name = "rtxtElements";
			this.rtxtElements.ReadOnly = true;
			this.rtxtElements.Size = new System.Drawing.Size(1350, 689);
			this.rtxtElements.TabIndex = 1;
			this.rtxtElements.Text = "";
			// 
			// FormElements
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1350, 729);
			this.Controls.Add(this.rtxtElements);
			this.Controls.Add(this.pnlElements);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.Name = "FormElements";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Orbital elements";
			this.Load += this.FormExport_Load;
			this.pnlElements.ResumeLayout(false);
			this.pnlElements.PerformLayout();
			this.ResumeLayout(false);
		}

		#endregion
		private System.Windows.Forms.Label lblFormat;
		private System.Windows.Forms.ComboBox cboFormat;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Panel pnlElements;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.RichTextBox rtxtElements;
	}
}
