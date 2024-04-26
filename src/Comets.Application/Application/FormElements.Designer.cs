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
			cboFormat = new ComboBox();
			lblFormat = new Label();
			btnSave = new Button();
			pnlElements = new Panel();
			btnClose = new Button();
			rtxtElements = new RichTextBox();
			pnlElements.SuspendLayout();
			SuspendLayout();
			// 
			// cboFormat
			// 
			cboFormat.DropDownStyle = ComboBoxStyle.DropDownList;
			cboFormat.FormattingEnabled = true;
			cboFormat.Location = new Point(62, 9);
			cboFormat.Name = "cboFormat";
			cboFormat.Size = new Size(280, 21);
			cboFormat.TabIndex = 1;
			cboFormat.SelectedIndexChanged += cboFormat_SelectedIndexChanged;
			// 
			// lblFormat
			// 
			lblFormat.AutoSize = true;
			lblFormat.Font = new Font("Tahoma", 8.25F);
			lblFormat.Location = new Point(8, 12);
			lblFormat.Name = "lblFormat";
			lblFormat.Size = new Size(45, 13);
			lblFormat.TabIndex = 0;
			lblFormat.Text = "Format:";
			// 
			// btnSave
			// 
			btnSave.Font = new Font("Tahoma", 8.25F);
			btnSave.Location = new Point(351, 8);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(97, 23);
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
			pnlElements.Dock = DockStyle.Top;
			pnlElements.Location = new Point(0, 0);
			pnlElements.Name = "pnlElements";
			pnlElements.Size = new Size(1350, 40);
			pnlElements.TabIndex = 0;
			// 
			// btnClose
			// 
			btnClose.Font = new Font("Tahoma", 8.25F);
			btnClose.Location = new Point(454, 8);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(97, 23);
			btnClose.TabIndex = 3;
			btnClose.Text = "Close";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += btnClose_Click;
			// 
			// rtxtElements
			// 
			rtxtElements.Dock = DockStyle.Fill;
			rtxtElements.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
			rtxtElements.Location = new Point(0, 40);
			rtxtElements.Name = "rtxtElements";
			rtxtElements.ReadOnly = true;
			rtxtElements.Size = new Size(1350, 689);
			rtxtElements.TabIndex = 1;
			rtxtElements.Text = "";
			// 
			// FormElements
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1350, 729);
			Controls.Add(rtxtElements);
			Controls.Add(pnlElements);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
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