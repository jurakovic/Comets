namespace Comets.Application
{
	partial class FormImport
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
			this.gbxLocalFile = new System.Windows.Forms.GroupBox();
			this.lblLocalFile = new System.Windows.Forms.Label();
			this.txtLocalFile = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.gbxDownload = new System.Windows.Forms.GroupBox();
			this.btnDownload = new System.Windows.Forms.Button();
			this.lblDownload = new System.Windows.Forms.Label();
			this.progressDownload = new System.Windows.Forms.ProgressBar();
			this.btnClose = new System.Windows.Forms.Button();
			this.gbxStatus = new System.Windows.Forms.GroupBox();
			this.labelDetectedComets = new System.Windows.Forms.Label();
			this.lblImportFormat = new System.Windows.Forms.Label();
			this.labelTotalCometsDescr = new System.Windows.Forms.Label();
			this.lblImportFormatDescr = new System.Windows.Forms.Label();
			this.btnImport = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.cbxClose = new System.Windows.Forms.CheckBox();
			this.gbxLocalFile.SuspendLayout();
			this.gbxDownload.SuspendLayout();
			this.gbxStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxLocalFile
			// 
			this.gbxLocalFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxLocalFile.Controls.Add(this.lblLocalFile);
			this.gbxLocalFile.Controls.Add(this.txtLocalFile);
			this.gbxLocalFile.Controls.Add(this.btnBrowse);
			this.gbxLocalFile.Location = new System.Drawing.Point(10, 99);
			this.gbxLocalFile.Name = "gbxLocalFile";
			this.gbxLocalFile.Size = new System.Drawing.Size(689, 94);
			this.gbxLocalFile.TabIndex = 1;
			this.gbxLocalFile.TabStop = false;
			// 
			// lblLocalFile
			// 
			this.lblLocalFile.AutoSize = true;
			this.lblLocalFile.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblLocalFile.Location = new System.Drawing.Point(6, 17);
			this.lblLocalFile.Name = "lblLocalFile";
			this.lblLocalFile.Size = new System.Drawing.Size(126, 13);
			this.lblLocalFile.TabIndex = 38;
			this.lblLocalFile.Text = "Import from local file";
			// 
			// txtLocalFile
			// 
			this.txtLocalFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLocalFile.Location = new System.Drawing.Point(155, 48);
			this.txtLocalFile.Name = "txtLocalFile";
			this.txtLocalFile.Size = new System.Drawing.Size(499, 21);
			this.txtLocalFile.TabIndex = 1;
			this.txtLocalFile.TextChanged += new System.EventHandler(this.txtImportFilename_TextChanged);
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(31, 47);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(118, 23);
			this.btnBrowse.TabIndex = 0;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// gbxDownload
			// 
			this.gbxDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxDownload.Controls.Add(this.btnDownload);
			this.gbxDownload.Controls.Add(this.lblDownload);
			this.gbxDownload.Controls.Add(this.progressDownload);
			this.gbxDownload.Location = new System.Drawing.Point(10, 4);
			this.gbxDownload.Name = "gbxDownload";
			this.gbxDownload.Size = new System.Drawing.Size(689, 94);
			this.gbxDownload.TabIndex = 0;
			this.gbxDownload.TabStop = false;
			// 
			// btnDownload
			// 
			this.btnDownload.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnDownload.Location = new System.Drawing.Point(31, 47);
			this.btnDownload.Name = "btnDownload";
			this.btnDownload.Size = new System.Drawing.Size(118, 23);
			this.btnDownload.TabIndex = 0;
			this.btnDownload.Text = "Download";
			this.btnDownload.UseVisualStyleBackColor = true;
			this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
			// 
			// lblDownload
			// 
			this.lblDownload.AutoSize = true;
			this.lblDownload.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblDownload.Location = new System.Drawing.Point(6, 17);
			this.lblDownload.Name = "lblDownload";
			this.lblDownload.Size = new System.Drawing.Size(297, 13);
			this.lblDownload.TabIndex = 25;
			this.lblDownload.Text = "Download the latest orbital elements from Internet";
			// 
			// progressDownload
			// 
			this.progressDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressDownload.Location = new System.Drawing.Point(155, 48);
			this.progressDownload.Name = "progressDownload";
			this.progressDownload.Size = new System.Drawing.Size(499, 21);
			this.progressDownload.TabIndex = 1;
			this.progressDownload.Visible = false;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(546, 351);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(118, 23);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			// 
			// gbxStatus
			// 
			this.gbxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxStatus.Controls.Add(this.labelDetectedComets);
			this.gbxStatus.Controls.Add(this.lblImportFormat);
			this.gbxStatus.Controls.Add(this.labelTotalCometsDescr);
			this.gbxStatus.Controls.Add(this.lblImportFormatDescr);
			this.gbxStatus.Controls.Add(this.btnImport);
			this.gbxStatus.Controls.Add(this.lblStatus);
			this.gbxStatus.Location = new System.Drawing.Point(10, 193);
			this.gbxStatus.Name = "gbxStatus";
			this.gbxStatus.Size = new System.Drawing.Size(689, 140);
			this.gbxStatus.TabIndex = 2;
			this.gbxStatus.TabStop = false;
			// 
			// labelDetectedComets
			// 
			this.labelDetectedComets.AutoSize = true;
			this.labelDetectedComets.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.labelDetectedComets.Location = new System.Drawing.Point(103, 69);
			this.labelDetectedComets.Name = "labelDetectedComets";
			this.labelDetectedComets.Size = new System.Drawing.Size(12, 13);
			this.labelDetectedComets.TabIndex = 47;
			this.labelDetectedComets.Text = "-";
			// 
			// lblImportFormat
			// 
			this.lblImportFormat.AutoSize = true;
			this.lblImportFormat.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblImportFormat.Location = new System.Drawing.Point(109, 48);
			this.lblImportFormat.Name = "lblImportFormat";
			this.lblImportFormat.Size = new System.Drawing.Size(102, 13);
			this.lblImportFormat.TabIndex = 46;
			this.lblImportFormat.Text = "(no file selected)";
			// 
			// labelTotalCometsDescr
			// 
			this.labelTotalCometsDescr.AutoSize = true;
			this.labelTotalCometsDescr.Location = new System.Drawing.Point(33, 69);
			this.labelTotalCometsDescr.Name = "labelTotalCometsDescr";
			this.labelTotalCometsDescr.Size = new System.Drawing.Size(74, 13);
			this.labelTotalCometsDescr.TabIndex = 45;
			this.labelTotalCometsDescr.Text = "Total Comets:";
			// 
			// lblImportFormatDescr
			// 
			this.lblImportFormatDescr.AutoSize = true;
			this.lblImportFormatDescr.Location = new System.Drawing.Point(33, 48);
			this.lblImportFormatDescr.Name = "lblImportFormatDescr";
			this.lblImportFormatDescr.Size = new System.Drawing.Size(83, 13);
			this.lblImportFormatDescr.TabIndex = 44;
			this.lblImportFormatDescr.Text = "Import Format: ";
			// 
			// btnImport
			// 
			this.btnImport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnImport.Location = new System.Drawing.Point(31, 93);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(118, 23);
			this.btnImport.TabIndex = 0;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblStatus.Location = new System.Drawing.Point(6, 17);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(44, 13);
			this.lblStatus.TabIndex = 25;
			this.lblStatus.Text = "Status";
			// 
			// cbxClose
			// 
			this.cbxClose.AutoSize = true;
			this.cbxClose.Checked = true;
			this.cbxClose.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxClose.Location = new System.Drawing.Point(19, 355);
			this.cbxClose.Name = "cbxClose";
			this.cbxClose.Size = new System.Drawing.Size(121, 17);
			this.cbxClose.TabIndex = 3;
			this.cbxClose.Text = "Close when finished";
			this.cbxClose.UseVisualStyleBackColor = true;
			// 
			// FormImport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(709, 394);
			this.Controls.Add(this.cbxClose);
			this.Controls.Add(this.gbxStatus);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.gbxLocalFile);
			this.Controls.Add(this.gbxDownload);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormImport";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Import";
			this.Load += new System.EventHandler(this.FormImport_Load);
			this.gbxLocalFile.ResumeLayout(false);
			this.gbxLocalFile.PerformLayout();
			this.gbxDownload.ResumeLayout(false);
			this.gbxDownload.PerformLayout();
			this.gbxStatus.ResumeLayout(false);
			this.gbxStatus.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

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
		private System.Windows.Forms.Label labelDetectedComets;
		private System.Windows.Forms.Label lblImportFormat;
		private System.Windows.Forms.Label labelTotalCometsDescr;
		private System.Windows.Forms.Label lblImportFormatDescr;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.CheckBox cbxClose;
	}
}