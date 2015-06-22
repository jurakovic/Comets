namespace Comets.Application
{
	partial class FormExport
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
			this.gbxExportFormat = new System.Windows.Forms.GroupBox();
			this.lblDownload = new System.Windows.Forms.Label();
			this.cbxExportFormat = new System.Windows.Forms.ComboBox();
			this.gbxSaveAs = new System.Windows.Forms.GroupBox();
			this.lblLocalFile = new System.Windows.Forms.Label();
			this.txtSaveAs = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.gbxStatus = new System.Windows.Forms.GroupBox();
			this.lblTotalComets = new System.Windows.Forms.Label();
			this.lblExportFormat = new System.Windows.Forms.Label();
			this.labelTotalCometsDescr = new System.Windows.Forms.Label();
			this.lblExportFormatDescr = new System.Windows.Forms.Label();
			this.btnExport = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.gbxExportFormat.SuspendLayout();
			this.gbxSaveAs.SuspendLayout();
			this.gbxStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxExportFormat
			// 
			this.gbxExportFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.gbxExportFormat.Controls.Add(this.cbxExportFormat);
			this.gbxExportFormat.Controls.Add(this.lblDownload);
			this.gbxExportFormat.Location = new System.Drawing.Point(10, 4);
			this.gbxExportFormat.Name = "gbxExportFormat";
			this.gbxExportFormat.Size = new System.Drawing.Size(689, 94);
			this.gbxExportFormat.TabIndex = 1;
			this.gbxExportFormat.TabStop = false;
			// 
			// lblDownload
			// 
			this.lblDownload.AutoSize = true;
			this.lblDownload.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblDownload.Location = new System.Drawing.Point(6, 17);
			this.lblDownload.Name = "lblDownload";
			this.lblDownload.Size = new System.Drawing.Size(125, 13);
			this.lblDownload.TabIndex = 25;
			this.lblDownload.Text = "Select export format";
			// 
			// cbxExportFormat
			// 
			this.cbxExportFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxExportFormat.FormattingEnabled = true;
			this.cbxExportFormat.Location = new System.Drawing.Point(32, 48);
			this.cbxExportFormat.Name = "cbxExportFormat";
			this.cbxExportFormat.Size = new System.Drawing.Size(280, 21);
			this.cbxExportFormat.TabIndex = 26;
			this.cbxExportFormat.SelectedIndexChanged += new System.EventHandler(this.cbxExportFormat_SelectedIndexChanged);
			// 
			// gbxSaveAs
			// 
			this.gbxSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.gbxSaveAs.Controls.Add(this.lblLocalFile);
			this.gbxSaveAs.Controls.Add(this.txtSaveAs);
			this.gbxSaveAs.Controls.Add(this.btnBrowse);
			this.gbxSaveAs.Location = new System.Drawing.Point(10, 99);
			this.gbxSaveAs.Name = "gbxSaveAs";
			this.gbxSaveAs.Size = new System.Drawing.Size(689, 94);
			this.gbxSaveAs.TabIndex = 2;
			this.gbxSaveAs.TabStop = false;
			// 
			// lblLocalFile
			// 
			this.lblLocalFile.AutoSize = true;
			this.lblLocalFile.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblLocalFile.Location = new System.Drawing.Point(6, 17);
			this.lblLocalFile.Name = "lblLocalFile";
			this.lblLocalFile.Size = new System.Drawing.Size(52, 13);
			this.lblLocalFile.TabIndex = 38;
			this.lblLocalFile.Text = "Save As";
			// 
			// txtSaveAs
			// 
			this.txtSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSaveAs.Location = new System.Drawing.Point(155, 48);
			this.txtSaveAs.Name = "txtSaveAs";
			this.txtSaveAs.Size = new System.Drawing.Size(499, 21);
			this.txtSaveAs.TabIndex = 1;
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
			// gbxStatus
			// 
			this.gbxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.gbxStatus.Controls.Add(this.lblTotalComets);
			this.gbxStatus.Controls.Add(this.lblExportFormat);
			this.gbxStatus.Controls.Add(this.labelTotalCometsDescr);
			this.gbxStatus.Controls.Add(this.lblExportFormatDescr);
			this.gbxStatus.Controls.Add(this.btnExport);
			this.gbxStatus.Controls.Add(this.lblStatus);
			this.gbxStatus.Location = new System.Drawing.Point(10, 193);
			this.gbxStatus.Name = "gbxStatus";
			this.gbxStatus.Size = new System.Drawing.Size(689, 140);
			this.gbxStatus.TabIndex = 3;
			this.gbxStatus.TabStop = false;
			// 
			// lblTotalComets
			// 
			this.lblTotalComets.AutoSize = true;
			this.lblTotalComets.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblTotalComets.Location = new System.Drawing.Point(103, 69);
			this.lblTotalComets.Name = "lblTotalComets";
			this.lblTotalComets.Size = new System.Drawing.Size(12, 13);
			this.lblTotalComets.TabIndex = 47;
			this.lblTotalComets.Text = "-";
			// 
			// lblExportFormat
			// 
			this.lblExportFormat.AutoSize = true;
			this.lblExportFormat.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblExportFormat.Location = new System.Drawing.Point(109, 48);
			this.lblExportFormat.Name = "lblExportFormat";
			this.lblExportFormat.Size = new System.Drawing.Size(165, 13);
			this.lblExportFormat.TabIndex = 46;
			this.lblExportFormat.Text = "(no export format selected)";
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
			// lblExportFormatDescr
			// 
			this.lblExportFormatDescr.AutoSize = true;
			this.lblExportFormatDescr.Location = new System.Drawing.Point(33, 48);
			this.lblExportFormatDescr.Name = "lblExportFormatDescr";
			this.lblExportFormatDescr.Size = new System.Drawing.Size(83, 13);
			this.lblExportFormatDescr.TabIndex = 44;
			this.lblExportFormatDescr.Text = "Export Format: ";
			// 
			// btnExport
			// 
			this.btnExport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnExport.Location = new System.Drawing.Point(31, 93);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(118, 23);
			this.btnExport.TabIndex = 0;
			this.btnExport.Text = "Export";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
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
			// FormExport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(709, 394);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.gbxStatus);
			this.Controls.Add(this.gbxSaveAs);
			this.Controls.Add(this.gbxExportFormat);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormExport";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Export";
			this.Load += new System.EventHandler(this.FormExport_Load);
			this.gbxExportFormat.ResumeLayout(false);
			this.gbxExportFormat.PerformLayout();
			this.gbxSaveAs.ResumeLayout(false);
			this.gbxSaveAs.PerformLayout();
			this.gbxStatus.ResumeLayout(false);
			this.gbxStatus.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbxExportFormat;
		private System.Windows.Forms.Label lblDownload;
		private System.Windows.Forms.ComboBox cbxExportFormat;
		private System.Windows.Forms.GroupBox gbxSaveAs;
		private System.Windows.Forms.Label lblLocalFile;
		private System.Windows.Forms.TextBox txtSaveAs;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.GroupBox gbxStatus;
		private System.Windows.Forms.Label lblTotalComets;
		private System.Windows.Forms.Label lblExportFormat;
		private System.Windows.Forms.Label labelTotalCometsDescr;
		private System.Windows.Forms.Label lblExportFormatDescr;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Button btnClose;
	}
}