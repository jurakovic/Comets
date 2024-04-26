namespace Comets.Application.Edit
{
	partial class FormUpdate
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
			gbxLocalFile = new GroupBox();
			lblLocalFile = new Label();
			txtLocalFile = new TextBox();
			btnBrowse = new Button();
			gbxDownload = new GroupBox();
			btnDownload = new Button();
			lblDownload = new Label();
			progressDownload = new ProgressBar();
			linkOpen = new LinkLabel();
			btnClose = new Button();
			gbxStatus = new GroupBox();
			lblCometCount = new LinkLabel();
			lblImportFormat = new Label();
			labelTotalCometsDescr = new Label();
			lblImportFormatDescr = new Label();
			btnImport = new Button();
			lblStatus = new Label();
			cbxClose = new CheckBox();
			gbxLocalFile.SuspendLayout();
			gbxDownload.SuspendLayout();
			gbxStatus.SuspendLayout();
			SuspendLayout();
			// 
			// gbxLocalFile
			// 
			gbxLocalFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			gbxLocalFile.Controls.Add(lblLocalFile);
			gbxLocalFile.Controls.Add(txtLocalFile);
			gbxLocalFile.Controls.Add(btnBrowse);
			gbxLocalFile.Location = new Point(10, 99);
			gbxLocalFile.Name = "gbxLocalFile";
			gbxLocalFile.Size = new Size(689, 94);
			gbxLocalFile.TabIndex = 1;
			gbxLocalFile.TabStop = false;
			// 
			// lblLocalFile
			// 
			lblLocalFile.AutoSize = true;
			lblLocalFile.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblLocalFile.Location = new Point(6, 17);
			lblLocalFile.Name = "lblLocalFile";
			lblLocalFile.Size = new Size(126, 13);
			lblLocalFile.TabIndex = 38;
			lblLocalFile.Text = "Import from local file";
			// 
			// txtLocalFile
			// 
			txtLocalFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtLocalFile.Location = new Point(155, 48);
			txtLocalFile.Name = "txtLocalFile";
			txtLocalFile.Size = new Size(499, 21);
			txtLocalFile.TabIndex = 1;
			txtLocalFile.TextChanged += txtImportFilename_TextChanged;
			// 
			// btnBrowse
			// 
			btnBrowse.Location = new Point(31, 47);
			btnBrowse.Name = "btnBrowse";
			btnBrowse.Size = new Size(118, 23);
			btnBrowse.TabIndex = 0;
			btnBrowse.Text = "Browse";
			btnBrowse.UseVisualStyleBackColor = true;
			btnBrowse.Click += btnBrowse_Click;
			// 
			// gbxDownload
			// 
			gbxDownload.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			gbxDownload.Controls.Add(btnDownload);
			gbxDownload.Controls.Add(lblDownload);
			gbxDownload.Controls.Add(progressDownload);
			gbxDownload.Controls.Add(linkOpen);
			gbxDownload.Location = new Point(10, 4);
			gbxDownload.Name = "gbxDownload";
			gbxDownload.Size = new Size(689, 94);
			gbxDownload.TabIndex = 0;
			gbxDownload.TabStop = false;
			// 
			// btnDownload
			// 
			btnDownload.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			btnDownload.Location = new Point(31, 47);
			btnDownload.Name = "btnDownload";
			btnDownload.Size = new Size(118, 23);
			btnDownload.TabIndex = 0;
			btnDownload.Text = "Download";
			btnDownload.UseVisualStyleBackColor = true;
			btnDownload.Click += btnDownload_Click;
			// 
			// lblDownload
			// 
			lblDownload.AutoSize = true;
			lblDownload.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblDownload.Location = new Point(6, 17);
			lblDownload.Name = "lblDownload";
			lblDownload.Size = new Size(297, 13);
			lblDownload.TabIndex = 25;
			lblDownload.Text = "Download the latest orbital elements from Internet";
			// 
			// progressDownload
			// 
			progressDownload.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			progressDownload.Location = new Point(155, 48);
			progressDownload.Name = "progressDownload";
			progressDownload.Size = new Size(499, 21);
			progressDownload.TabIndex = 1;
			progressDownload.Visible = false;
			// 
			// linkOpen
			// 
			linkOpen.ActiveLinkColor = Color.Blue;
			linkOpen.AutoSize = true;
			linkOpen.DisabledLinkColor = Color.Blue;
			linkOpen.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			linkOpen.LinkColor = Color.Blue;
			linkOpen.Location = new Point(158, 51);
			linkOpen.Name = "linkOpen";
			linkOpen.Size = new Size(98, 13);
			linkOpen.TabIndex = 26;
			linkOpen.TabStop = true;
			linkOpen.Text = "Open in browser";
			linkOpen.VisitedLinkColor = Color.Blue;
			linkOpen.LinkClicked += linkOpen_LinkClicked;
			// 
			// btnClose
			// 
			btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnClose.DialogResult = DialogResult.Cancel;
			btnClose.Location = new Point(546, 351);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(118, 23);
			btnClose.TabIndex = 4;
			btnClose.Text = "Close";
			btnClose.UseVisualStyleBackColor = true;
			// 
			// gbxStatus
			// 
			gbxStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			gbxStatus.Controls.Add(lblCometCount);
			gbxStatus.Controls.Add(lblImportFormat);
			gbxStatus.Controls.Add(labelTotalCometsDescr);
			gbxStatus.Controls.Add(lblImportFormatDescr);
			gbxStatus.Controls.Add(btnImport);
			gbxStatus.Controls.Add(lblStatus);
			gbxStatus.Location = new Point(10, 193);
			gbxStatus.Name = "gbxStatus";
			gbxStatus.Size = new Size(689, 140);
			gbxStatus.TabIndex = 2;
			gbxStatus.TabStop = false;
			// 
			// lblCometCount
			// 
			lblCometCount.ActiveLinkColor = Color.Blue;
			lblCometCount.AutoSize = true;
			lblCometCount.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblCometCount.LinkBehavior = LinkBehavior.HoverUnderline;
			lblCometCount.LinkColor = Color.Blue;
			lblCometCount.Location = new Point(103, 69);
			lblCometCount.Name = "lblCometCount";
			lblCometCount.Size = new Size(12, 13);
			lblCometCount.TabIndex = 48;
			lblCometCount.TabStop = true;
			lblCometCount.Text = "-";
			lblCometCount.VisitedLinkColor = Color.Blue;
			lblCometCount.LinkClicked += labelDetectedComets_LinkClicked;
			// 
			// lblImportFormat
			// 
			lblImportFormat.AutoSize = true;
			lblImportFormat.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblImportFormat.Location = new Point(109, 48);
			lblImportFormat.Name = "lblImportFormat";
			lblImportFormat.Size = new Size(102, 13);
			lblImportFormat.TabIndex = 46;
			lblImportFormat.Text = "(no file selected)";
			// 
			// labelTotalCometsDescr
			// 
			labelTotalCometsDescr.AutoSize = true;
			labelTotalCometsDescr.Location = new Point(33, 69);
			labelTotalCometsDescr.Name = "labelTotalCometsDescr";
			labelTotalCometsDescr.Size = new Size(74, 13);
			labelTotalCometsDescr.TabIndex = 45;
			labelTotalCometsDescr.Text = "Total Comets:";
			// 
			// lblImportFormatDescr
			// 
			lblImportFormatDescr.AutoSize = true;
			lblImportFormatDescr.Location = new Point(33, 48);
			lblImportFormatDescr.Name = "lblImportFormatDescr";
			lblImportFormatDescr.Size = new Size(83, 13);
			lblImportFormatDescr.TabIndex = 44;
			lblImportFormatDescr.Text = "Import Format: ";
			// 
			// btnImport
			// 
			btnImport.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			btnImport.Location = new Point(31, 93);
			btnImport.Name = "btnImport";
			btnImport.Size = new Size(118, 23);
			btnImport.TabIndex = 0;
			btnImport.Text = "Import";
			btnImport.UseVisualStyleBackColor = true;
			btnImport.Click += btnImport_Click;
			// 
			// lblStatus
			// 
			lblStatus.AutoSize = true;
			lblStatus.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblStatus.Location = new Point(6, 17);
			lblStatus.Name = "lblStatus";
			lblStatus.Size = new Size(44, 13);
			lblStatus.TabIndex = 25;
			lblStatus.Text = "Status";
			// 
			// cbxClose
			// 
			cbxClose.AutoSize = true;
			cbxClose.Checked = true;
			cbxClose.CheckState = CheckState.Checked;
			cbxClose.Location = new Point(19, 355);
			cbxClose.Name = "cbxClose";
			cbxClose.Size = new Size(121, 17);
			cbxClose.TabIndex = 3;
			cbxClose.Text = "Close when finished";
			cbxClose.UseVisualStyleBackColor = true;
			// 
			// FormUpdate
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnClose;
			ClientSize = new Size(709, 394);
			Controls.Add(cbxClose);
			Controls.Add(gbxStatus);
			Controls.Add(btnClose);
			Controls.Add(gbxLocalFile);
			Controls.Add(gbxDownload);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FormUpdate";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Update";
			Load += FormImport_Load;
			gbxLocalFile.ResumeLayout(false);
			gbxLocalFile.PerformLayout();
			gbxDownload.ResumeLayout(false);
			gbxDownload.PerformLayout();
			gbxStatus.ResumeLayout(false);
			gbxStatus.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.GroupBox gbxLocalFile;
		private System.Windows.Forms.Label lblLocalFile;
		private System.Windows.Forms.TextBox txtLocalFile;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.GroupBox gbxDownload;
		private System.Windows.Forms.Button btnDownload;
		private System.Windows.Forms.Label lblDownload;
		private System.Windows.Forms.ProgressBar progressDownload;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.GroupBox gbxStatus;
		private System.Windows.Forms.Label lblImportFormat;
		private System.Windows.Forms.Label labelTotalCometsDescr;
		private System.Windows.Forms.Label lblImportFormatDescr;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.CheckBox cbxClose;
		private System.Windows.Forms.LinkLabel lblCometCount;
		private System.Windows.Forms.LinkLabel linkOpen;
	}
}