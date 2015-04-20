namespace Comets.Forms
{
    partial class FormSettings
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chExitWithoutConfirm = new System.Windows.Forms.CheckBox();
            this.chNewVersionOnStartup = new System.Windows.Forms.CheckBox();
            this.chRememberWindowPosition = new System.Windows.Forms.CheckBox();
            this.chDownloadOnStartup = new System.Windows.Forms.CheckBox();
            this.gbxLocalFile = new System.Windows.Forms.GroupBox();
            this.lblDownloads = new System.Windows.Forms.Label();
            this.txtDownloads = new System.Windows.Forms.TextBox();
            this.btnDownloads = new System.Windows.Forms.Button();
            this.lblAppData = new System.Windows.Forms.Label();
            this.txtAppData = new System.Windows.Forms.TextBox();
            this.btnAppData = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSetDST = new System.Windows.Forms.Button();
            this.chDST = new System.Windows.Forms.CheckBox();
            this.lblTimezone = new System.Windows.Forms.Label();
            this.txtTimezone = new System.Windows.Forms.TextBox();
            this.cbxEastWest = new System.Windows.Forms.ComboBox();
            this.cbxNorthSouth = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLonSec = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLonMin = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLatSec = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLatMin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.txtLonDeg = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.txtLatDeg = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.listPrograms = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbxLocalFile.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.ItemSize = new System.Drawing.Size(120, 20);
            this.tabControl1.Location = new System.Drawing.Point(8, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(693, 324);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.gbxLocalFile);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(685, 296);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chExitWithoutConfirm);
            this.groupBox2.Controls.Add(this.chNewVersionOnStartup);
            this.groupBox2.Controls.Add(this.chRememberWindowPosition);
            this.groupBox2.Controls.Add(this.chDownloadOnStartup);
            this.groupBox2.Location = new System.Drawing.Point(8, 169);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(668, 93);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // chExitWithoutConfirm
            // 
            this.chExitWithoutConfirm.AutoSize = true;
            this.chExitWithoutConfirm.Location = new System.Drawing.Point(402, 55);
            this.chExitWithoutConfirm.Name = "chExitWithoutConfirm";
            this.chExitWithoutConfirm.Size = new System.Drawing.Size(121, 17);
            this.chExitWithoutConfirm.TabIndex = 3;
            this.chExitWithoutConfirm.Text = "Exit without confirm";
            this.chExitWithoutConfirm.UseVisualStyleBackColor = true;
            // 
            // chNewVersionOnStartup
            // 
            this.chNewVersionOnStartup.AutoSize = true;
            this.chNewVersionOnStartup.Location = new System.Drawing.Point(9, 55);
            this.chNewVersionOnStartup.Name = "chNewVersionOnStartup";
            this.chNewVersionOnStartup.Size = new System.Drawing.Size(186, 17);
            this.chNewVersionOnStartup.TabIndex = 1;
            this.chNewVersionOnStartup.Text = "Check for new version on startup";
            this.chNewVersionOnStartup.UseVisualStyleBackColor = true;
            // 
            // chRememberWindowPosition
            // 
            this.chRememberWindowPosition.AutoSize = true;
            this.chRememberWindowPosition.Location = new System.Drawing.Point(402, 20);
            this.chRememberWindowPosition.Name = "chRememberWindowPosition";
            this.chRememberWindowPosition.Size = new System.Drawing.Size(156, 17);
            this.chRememberWindowPosition.TabIndex = 2;
            this.chRememberWindowPosition.Text = "Remember window position";
            this.chRememberWindowPosition.UseVisualStyleBackColor = true;
            // 
            // chDownloadOnStartup
            // 
            this.chDownloadOnStartup.AutoSize = true;
            this.chDownloadOnStartup.Location = new System.Drawing.Point(9, 20);
            this.chDownloadOnStartup.Name = "chDownloadOnStartup";
            this.chDownloadOnStartup.Size = new System.Drawing.Size(322, 17);
            this.chDownloadOnStartup.TabIndex = 0;
            this.chDownloadOnStartup.Text = "Download the latest orbital elements from Internet on startup";
            this.chDownloadOnStartup.UseVisualStyleBackColor = true;
            // 
            // gbxLocalFile
            // 
            this.gbxLocalFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxLocalFile.Controls.Add(this.lblDownloads);
            this.gbxLocalFile.Controls.Add(this.txtDownloads);
            this.gbxLocalFile.Controls.Add(this.btnDownloads);
            this.gbxLocalFile.Controls.Add(this.lblAppData);
            this.gbxLocalFile.Controls.Add(this.txtAppData);
            this.gbxLocalFile.Controls.Add(this.btnAppData);
            this.gbxLocalFile.Location = new System.Drawing.Point(8, 3);
            this.gbxLocalFile.Name = "gbxLocalFile";
            this.gbxLocalFile.Size = new System.Drawing.Size(669, 160);
            this.gbxLocalFile.TabIndex = 2;
            this.gbxLocalFile.TabStop = false;
            // 
            // lblDownloads
            // 
            this.lblDownloads.AutoSize = true;
            this.lblDownloads.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDownloads.Location = new System.Drawing.Point(6, 84);
            this.lblDownloads.Name = "lblDownloads";
            this.lblDownloads.Size = new System.Drawing.Size(123, 13);
            this.lblDownloads.TabIndex = 41;
            this.lblDownloads.Text = "Downloads directory";
            // 
            // txtDownloads
            // 
            this.txtDownloads.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDownloads.Location = new System.Drawing.Point(155, 115);
            this.txtDownloads.Name = "txtDownloads";
            this.txtDownloads.Size = new System.Drawing.Size(479, 21);
            this.txtDownloads.TabIndex = 3;
            // 
            // btnDownloads
            // 
            this.btnDownloads.Location = new System.Drawing.Point(31, 114);
            this.btnDownloads.Name = "btnDownloads";
            this.btnDownloads.Size = new System.Drawing.Size(118, 23);
            this.btnDownloads.TabIndex = 2;
            this.btnDownloads.Text = "Browse";
            this.btnDownloads.UseVisualStyleBackColor = true;
            // 
            // lblAppData
            // 
            this.lblAppData.AutoSize = true;
            this.lblAppData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblAppData.Location = new System.Drawing.Point(6, 17);
            this.lblAppData.Name = "lblAppData";
            this.lblAppData.Size = new System.Drawing.Size(154, 13);
            this.lblAppData.TabIndex = 38;
            this.lblAppData.Text = "Application data directory";
            // 
            // txtAppData
            // 
            this.txtAppData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppData.Location = new System.Drawing.Point(155, 48);
            this.txtAppData.Name = "txtAppData";
            this.txtAppData.Size = new System.Drawing.Size(479, 21);
            this.txtAppData.TabIndex = 1;
            // 
            // btnAppData
            // 
            this.btnAppData.Location = new System.Drawing.Point(31, 47);
            this.btnAppData.Name = "btnAppData";
            this.btnAppData.Size = new System.Drawing.Size(118, 23);
            this.btnAppData.TabIndex = 0;
            this.btnAppData.Text = "Browse";
            this.btnAppData.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(685, 296);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Location";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnSetDST);
            this.groupBox1.Controls.Add(this.chDST);
            this.groupBox1.Controls.Add(this.lblTimezone);
            this.groupBox1.Controls.Add(this.txtTimezone);
            this.groupBox1.Controls.Add(this.cbxEastWest);
            this.groupBox1.Controls.Add(this.cbxNorthSouth);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtLonSec);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtLonMin);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtLatSec);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtLatMin);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblLongitude);
            this.groupBox1.Controls.Add(this.txtLonDeg);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.lblLatitude);
            this.groupBox1.Controls.Add(this.txtLatDeg);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(669, 285);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // btnSetDST
            // 
            this.btnSetDST.Location = new System.Drawing.Point(208, 247);
            this.btnSetDST.Name = "btnSetDST";
            this.btnSetDST.Size = new System.Drawing.Size(147, 23);
            this.btnSetDST.TabIndex = 11;
            this.btnSetDST.Text = "Set from PC";
            this.btnSetDST.UseVisualStyleBackColor = true;
            // 
            // chDST
            // 
            this.chDST.AutoSize = true;
            this.chDST.Location = new System.Drawing.Point(121, 251);
            this.chDST.Name = "chDST";
            this.chDST.Size = new System.Drawing.Size(45, 17);
            this.chDST.TabIndex = 10;
            this.chDST.Text = "DST";
            this.chDST.UseVisualStyleBackColor = true;
            // 
            // lblTimezone
            // 
            this.lblTimezone.AutoSize = true;
            this.lblTimezone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTimezone.Location = new System.Drawing.Point(6, 218);
            this.lblTimezone.Name = "lblTimezone";
            this.lblTimezone.Size = new System.Drawing.Size(62, 13);
            this.lblTimezone.TabIndex = 58;
            this.lblTimezone.Text = "Timezone";
            // 
            // txtTimezone
            // 
            this.txtTimezone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimezone.Location = new System.Drawing.Point(31, 249);
            this.txtTimezone.Name = "txtTimezone";
            this.txtTimezone.Size = new System.Drawing.Size(57, 21);
            this.txtTimezone.TabIndex = 9;
            // 
            // cbxEastWest
            // 
            this.cbxEastWest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEastWest.FormattingEnabled = true;
            this.cbxEastWest.Items.AddRange(new object[] {
            "East",
            "West"});
            this.cbxEastWest.Location = new System.Drawing.Point(301, 182);
            this.cbxEastWest.Name = "cbxEastWest";
            this.cbxEastWest.Size = new System.Drawing.Size(100, 21);
            this.cbxEastWest.TabIndex = 8;
            // 
            // cbxNorthSouth
            // 
            this.cbxNorthSouth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxNorthSouth.FormattingEnabled = true;
            this.cbxNorthSouth.Items.AddRange(new object[] {
            "North",
            "South"});
            this.cbxNorthSouth.Location = new System.Drawing.Point(301, 115);
            this.cbxNorthSouth.Name = "cbxNorthSouth";
            this.cbxNorthSouth.Size = new System.Drawing.Size(100, 21);
            this.cbxNorthSouth.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(92, 185);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(12, 13);
            this.label10.TabIndex = 54;
            this.label10.Text = "°";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(272, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 13);
            this.label7.TabIndex = 53;
            this.label7.Text = "\'\'";
            // 
            // txtLonSec
            // 
            this.txtLonSec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLonSec.Location = new System.Drawing.Point(211, 182);
            this.txtLonSec.MaxLength = 2;
            this.txtLonSec.Name = "txtLonSec";
            this.txtLonSec.Size = new System.Drawing.Size(57, 21);
            this.txtLonSec.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(182, 185);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(9, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "\'";
            // 
            // txtLonMin
            // 
            this.txtLonMin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLonMin.Location = new System.Drawing.Point(121, 182);
            this.txtLonMin.MaxLength = 2;
            this.txtLonMin.Name = "txtLonMin";
            this.txtLonMin.Size = new System.Drawing.Size(57, 21);
            this.txtLonMin.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(272, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "\'\'";
            // 
            // txtLatSec
            // 
            this.txtLatSec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLatSec.Location = new System.Drawing.Point(211, 115);
            this.txtLatSec.MaxLength = 2;
            this.txtLatSec.Name = "txtLatSec";
            this.txtLatSec.Size = new System.Drawing.Size(57, 21);
            this.txtLatSec.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(182, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(9, 13);
            this.label6.TabIndex = 47;
            this.label6.Text = "\'";
            // 
            // txtLatMin
            // 
            this.txtLatMin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLatMin.Location = new System.Drawing.Point(121, 115);
            this.txtLatMin.MaxLength = 2;
            this.txtLatMin.Name = "txtLatMin";
            this.txtLatMin.Size = new System.Drawing.Size(57, 21);
            this.txtLatMin.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(92, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "°";
            // 
            // lblLongitude
            // 
            this.lblLongitude.AutoSize = true;
            this.lblLongitude.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblLongitude.Location = new System.Drawing.Point(6, 151);
            this.lblLongitude.Name = "lblLongitude";
            this.lblLongitude.Size = new System.Drawing.Size(63, 13);
            this.lblLongitude.TabIndex = 44;
            this.lblLongitude.Text = "Longitude";
            // 
            // txtLonDeg
            // 
            this.txtLonDeg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLonDeg.Location = new System.Drawing.Point(31, 182);
            this.txtLonDeg.MaxLength = 4;
            this.txtLonDeg.Name = "txtLonDeg";
            this.txtLonDeg.Size = new System.Drawing.Size(57, 21);
            this.txtLonDeg.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(31, 48);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(237, 21);
            this.txtName.TabIndex = 0;
            // 
            // lblLatitude
            // 
            this.lblLatitude.AutoSize = true;
            this.lblLatitude.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblLatitude.Location = new System.Drawing.Point(6, 84);
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Size = new System.Drawing.Size(54, 13);
            this.lblLatitude.TabIndex = 41;
            this.lblLatitude.Text = "Latitude";
            // 
            // txtLatDeg
            // 
            this.txtLatDeg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLatDeg.Location = new System.Drawing.Point(31, 115);
            this.txtLatDeg.MaxLength = 4;
            this.txtLatDeg.Name = "txtLatDeg";
            this.txtLatDeg.Size = new System.Drawing.Size(57, 21);
            this.txtLatDeg.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblName.Location = new System.Drawing.Point(6, 17);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 13);
            this.lblName.TabIndex = 38;
            this.lblName.Text = "Name";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(685, 296);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Programs";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnClear);
            this.groupBox3.Controls.Add(this.btnRemove);
            this.groupBox3.Controls.Add(this.btnEdit);
            this.groupBox3.Controls.Add(this.listPrograms);
            this.groupBox3.Controls.Add(this.btnAdd);
            this.groupBox3.Location = new System.Drawing.Point(8, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(669, 285);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(304, 252);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(206, 252);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(94, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(108, 252);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(94, 23);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // listPrograms
            // 
            this.listPrograms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listPrograms.FullRowSelect = true;
            this.listPrograms.GridLines = true;
            this.listPrograms.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listPrograms.Location = new System.Drawing.Point(3, 10);
            this.listPrograms.Name = "listPrograms";
            this.listPrograms.Size = new System.Drawing.Size(663, 231);
            this.listPrograms.TabIndex = 0;
            this.listPrograms.UseCompatibleStateImageBehavior = false;
            this.listPrograms.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Program";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Directory";
            this.columnHeader2.Width = 509;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(10, 252);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(546, 351);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(422, 351);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(118, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 394);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbxLocalFile.ResumeLayout(false);
            this.gbxLocalFile.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox gbxLocalFile;
        private System.Windows.Forms.Label lblDownloads;
        private System.Windows.Forms.TextBox txtDownloads;
        private System.Windows.Forms.Button btnDownloads;
        private System.Windows.Forms.Label lblAppData;
        private System.Windows.Forms.TextBox txtAppData;
        private System.Windows.Forms.Button btnAppData;
        private System.Windows.Forms.CheckBox chExitWithoutConfirm;
        private System.Windows.Forms.CheckBox chNewVersionOnStartup;
        private System.Windows.Forms.CheckBox chRememberWindowPosition;
        private System.Windows.Forms.CheckBox chDownloadOnStartup;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSetDST;
        private System.Windows.Forms.CheckBox chDST;
        private System.Windows.Forms.Label lblTimezone;
        private System.Windows.Forms.TextBox txtTimezone;
        private System.Windows.Forms.ComboBox cbxEastWest;
        private System.Windows.Forms.ComboBox cbxNorthSouth;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLonSec;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLonMin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtLatSec;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLatMin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLongitude;
        private System.Windows.Forms.TextBox txtLonDeg;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblLatitude;
        private System.Windows.Forms.TextBox txtLatDeg;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ListView listPrograms;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnAdd;
    }
}