namespace Comets.Forms
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
            this.groupBoxLocalFile = new System.Windows.Forms.GroupBox();
            this.label66 = new System.Windows.Forms.Label();
            this.txtImportFilename = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.groupBoxDownload = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.progressDownload = new System.Windows.Forms.ProgressBar();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelDetectedComets = new System.Windows.Forms.Label();
            this.lblImportFormat1 = new System.Windows.Forms.Label();
            this.labelTotalComets = new System.Windows.Forms.Label();
            this.labelImportFormat = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.bwDownload = new System.ComponentModel.BackgroundWorker();
            this.groupBoxLocalFile.SuspendLayout();
            this.groupBoxDownload.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxLocalFile
            // 
            this.groupBoxLocalFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLocalFile.Controls.Add(this.label66);
            this.groupBoxLocalFile.Controls.Add(this.txtImportFilename);
            this.groupBoxLocalFile.Controls.Add(this.btnBrowse);
            this.groupBoxLocalFile.Location = new System.Drawing.Point(7, 97);
            this.groupBoxLocalFile.Name = "groupBoxLocalFile";
            this.groupBoxLocalFile.Size = new System.Drawing.Size(690, 94);
            this.groupBoxLocalFile.TabIndex = 1;
            this.groupBoxLocalFile.TabStop = false;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label66.Location = new System.Drawing.Point(6, 17);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(126, 13);
            this.label66.TabIndex = 38;
            this.label66.Text = "Import from local file";
            // 
            // txtImportFilename
            // 
            this.txtImportFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImportFilename.Location = new System.Drawing.Point(140, 48);
            this.txtImportFilename.Name = "txtImportFilename";
            this.txtImportFilename.Size = new System.Drawing.Size(526, 21);
            this.txtImportFilename.TabIndex = 1;
            this.txtImportFilename.TextChanged += new System.EventHandler(this.txtImportFilename_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(31, 47);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(103, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // groupBoxDownload
            // 
            this.groupBoxDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDownload.Controls.Add(this.btnDownload);
            this.groupBoxDownload.Controls.Add(this.label19);
            this.groupBoxDownload.Controls.Add(this.progressDownload);
            this.groupBoxDownload.Location = new System.Drawing.Point(7, 2);
            this.groupBoxDownload.Name = "groupBoxDownload";
            this.groupBoxDownload.Size = new System.Drawing.Size(690, 94);
            this.groupBoxDownload.TabIndex = 0;
            this.groupBoxDownload.TabStop = false;
            // 
            // btnDownload
            // 
            this.btnDownload.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDownload.Location = new System.Drawing.Point(31, 47);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(103, 23);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label19.Location = new System.Drawing.Point(6, 17);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(297, 13);
            this.label19.TabIndex = 25;
            this.label19.Text = "Download the latest orbital elements from Internet";
            // 
            // progressDownload
            // 
            this.progressDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressDownload.Location = new System.Drawing.Point(140, 48);
            this.progressDownload.Name = "progressDownload";
            this.progressDownload.Size = new System.Drawing.Size(526, 21);
            this.progressDownload.TabIndex = 1;
            this.progressDownload.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(282, 351);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(142, 25);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelDetectedComets);
            this.groupBox1.Controls.Add(this.lblImportFormat1);
            this.groupBox1.Controls.Add(this.labelTotalComets);
            this.groupBox1.Controls.Add(this.labelImportFormat);
            this.groupBox1.Controls.Add(this.btnImport);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Location = new System.Drawing.Point(7, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(690, 140);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
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
            // lblImportFormat1
            // 
            this.lblImportFormat1.AutoSize = true;
            this.lblImportFormat1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblImportFormat1.Location = new System.Drawing.Point(109, 48);
            this.lblImportFormat1.Name = "lblImportFormat1";
            this.lblImportFormat1.Size = new System.Drawing.Size(102, 13);
            this.lblImportFormat1.TabIndex = 46;
            this.lblImportFormat1.Text = "(no file selected)";
            // 
            // labelTotalComets
            // 
            this.labelTotalComets.AutoSize = true;
            this.labelTotalComets.Location = new System.Drawing.Point(33, 69);
            this.labelTotalComets.Name = "labelTotalComets";
            this.labelTotalComets.Size = new System.Drawing.Size(74, 13);
            this.labelTotalComets.TabIndex = 45;
            this.labelTotalComets.Text = "Total Comets:";
            // 
            // labelImportFormat
            // 
            this.labelImportFormat.AutoSize = true;
            this.labelImportFormat.Location = new System.Drawing.Point(33, 48);
            this.labelImportFormat.Name = "labelImportFormat";
            this.labelImportFormat.Size = new System.Drawing.Size(83, 13);
            this.labelImportFormat.TabIndex = 44;
            this.labelImportFormat.Text = "Import Format: ";
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnImport.Location = new System.Drawing.Point(31, 93);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(103, 23);
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
            // bwDownload
            // 
            this.bwDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDownload_DoWork);
            // 
            // FormImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(704, 393);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBoxLocalFile);
            this.Controls.Add(this.groupBoxDownload);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormImport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import";
            this.groupBoxLocalFile.ResumeLayout(false);
            this.groupBoxLocalFile.PerformLayout();
            this.groupBoxDownload.ResumeLayout(false);
            this.groupBoxDownload.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLocalFile;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TextBox txtImportFilename;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox groupBoxDownload;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ProgressBar progressDownload;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelDetectedComets;
        private System.Windows.Forms.Label lblImportFormat1;
        private System.Windows.Forms.Label labelTotalComets;
        private System.Windows.Forms.Label labelImportFormat;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.BackgroundWorker bwDownload;
    }
}