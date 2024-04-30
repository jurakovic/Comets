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
			cboFormat = new System.Windows.Forms.ComboBox();
			lblFormat = new System.Windows.Forms.Label();
			btnSave = new System.Windows.Forms.Button();
			pnlElements = new System.Windows.Forms.Panel();
			btnClose = new System.Windows.Forms.Button();
			rtxtElements = new System.Windows.Forms.RichTextBox();
			pnlElements.SuspendLayout();
			SuspendLayout();
			// 
			// cboFormat
			// 
			cboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboFormat.FormattingEnabled = true;
			cboFormat.Location = new System.Drawing.Point(62, 9);
			cboFormat.Name = "cboFormat";
			cboFormat.Size = new System.Drawing.Size(280, 21);
			cboFormat.TabIndex = 1;
			cboFormat.SelectedIndexChanged += cboFormat_SelectedIndexChanged;
			// 
			// lblFormat
			// 
			lblFormat.AutoSize = true;
			lblFormat.Font = new System.Drawing.Font("Tahoma", 8.25F);
			lblFormat.Location = new System.Drawing.Point(8, 12);
			lblFormat.Name = "lblFormat";
			lblFormat.Size = new System.Drawing.Size(45, 13);
			lblFormat.TabIndex = 0;
			lblFormat.Text = "Format:";
			// 
			// btnSave
			// 
			btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F);
			btnSave.Location = new System.Drawing.Point(351, 8);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(97, 23);
			btnSave.TabIndex = 2;
			btnSave.Text = "Save As";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// pnlElements
			// 
			pnlElements.Controls.Add(btnClose);
			pnlElements.Controls.Add(lblFormat);
			pnlElements.Controls.Add(btnSave);
			pnlElements.Controls.Add(cboFormat);
			pnlElements.Dock = System.Windows.Forms.DockStyle.Top;
			pnlElements.Location = new System.Drawing.Point(0, 0);
			pnlElements.Name = "pnlElements";
			pnlElements.Size = new System.Drawing.Size(1350, 40);
			pnlElements.TabIndex = 0;
			// 
			// btnClose
			// 
			btnClose.Font = new System.Drawing.Font("Tahoma", 8.25F);
			btnClose.Location = new System.Drawing.Point(454, 8);
			btnClose.Name = "btnClose";
			btnClose.Size = new System.Drawing.Size(97, 23);
			btnClose.TabIndex = 3;
			btnClose.Text = "Close";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += btnClose_Click;
			// 
			// rtxtElements
			// 
			rtxtElements.Dock = System.Windows.Forms.DockStyle.Fill;
			rtxtElements.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			rtxtElements.Location = new System.Drawing.Point(0, 40);
			rtxtElements.Name = "rtxtElements";
			rtxtElements.ReadOnly = true;
			rtxtElements.Size = new System.Drawing.Size(1350, 689);
			rtxtElements.TabIndex = 1;
			rtxtElements.Text = "";
			// 
			// FormElements
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(1350, 729);
			Controls.Add(rtxtElements);
			Controls.Add(pnlElements);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			Name = "FormElements";
			ShowIcon = false;
			ShowInTaskbar = false;
			Text = "Orbital elements";
			Load += FormExport_Load;
			pnlElements.ResumeLayout(false);
			pnlElements.PerformLayout();
			ResumeLayout(false);
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
