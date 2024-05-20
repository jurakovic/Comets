namespace Comets.Application.Edit
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
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.gbxGeneral = new System.Windows.Forms.GroupBox();
			this.cboTheme = new System.Windows.Forms.ComboBox();
			this.lblTheme = new System.Windows.Forms.Label();
			this.cbxShowDeleteCometConfirmation = new System.Windows.Forms.CheckBox();
			this.txtDownloadUrl = new System.Windows.Forms.TextBox();
			this.lblDownloadUrl = new System.Windows.Forms.Label();
			this.cbxShowLongCalculationConfirmation = new System.Windows.Forms.CheckBox();
			this.lblDays = new System.Windows.Forms.Label();
			this.txtUpdateInterval = new System.Windows.Forms.TextBox();
			this.chShowStatusBar = new System.Windows.Forms.CheckBox();
			this.chExitWithoutConfirm = new System.Windows.Forms.CheckBox();
			this.chNewVersionOnStartup = new System.Windows.Forms.CheckBox();
			this.chRememberWindowPosition = new System.Windows.Forms.CheckBox();
			this.chAutomaticUpdate = new System.Windows.Forms.CheckBox();
			this.tabLocation = new System.Windows.Forms.TabPage();
			this.gbxLocation = new System.Windows.Forms.GroupBox();
			this.cbxEastWest = new System.Windows.Forms.ComboBox();
			this.cbxNorthSouth = new System.Windows.Forms.ComboBox();
			this.lblLonDeg = new System.Windows.Forms.Label();
			this.lblLatDeg = new System.Windows.Forms.Label();
			this.lblLongitude = new System.Windows.Forms.Label();
			this.txtLongitude = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblLatitude = new System.Windows.Forms.Label();
			this.txtLatitude = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.tabPrograms = new System.Windows.Forms.TabPage();
			this.gbxPrograms = new System.Windows.Forms.GroupBox();
			this.dgvPrograms = new System.Windows.Forms.DataGridView();
			this.colProgram = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colDirectory = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.gbxAddProgram = new System.Windows.Forms.GroupBox();
			this.txtDirectory = new System.Windows.Forms.TextBox();
			this.lblDirectory = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.cbxExternalProgram = new System.Windows.Forms.ComboBox();
			this.lblSelectProgram = new System.Windows.Forms.Label();
			this.btnProgramsOk = new System.Windows.Forms.Button();
			this.btnProgramsCancel = new System.Windows.Forms.Button();
			this.tabNetwork = new System.Windows.Forms.TabPage();
			this.gbxNetwork = new System.Windows.Forms.GroupBox();
			this.pnlProxy = new System.Windows.Forms.Panel();
			this.lblPort = new System.Windows.Forms.Label();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.lblProxy = new System.Windows.Forms.Label();
			this.txtProxy = new System.Windows.Forms.TextBox();
			this.lblUsername = new System.Windows.Forms.Label();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.lblDomain = new System.Windows.Forms.Label();
			this.txtDomain = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.rbManualProxy = new System.Windows.Forms.RadioButton();
			this.rbNoProxy = new System.Windows.Forms.RadioButton();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.gbxGeneral.SuspendLayout();
			this.tabLocation.SuspendLayout();
			this.gbxLocation.SuspendLayout();
			this.tabPrograms.SuspendLayout();
			this.gbxPrograms.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.dgvPrograms).BeginInit();
			this.gbxAddProgram.SuspendLayout();
			this.tabNetwork.SuspendLayout();
			this.gbxNetwork.SuspendLayout();
			this.pnlProxy.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.tabControl1.Controls.Add(this.tabGeneral);
			this.tabControl1.Controls.Add(this.tabLocation);
			this.tabControl1.Controls.Add(this.tabPrograms);
			this.tabControl1.Controls.Add(this.tabNetwork);
			this.tabControl1.ItemSize = new System.Drawing.Size(120, 20);
			this.tabControl1.Location = new System.Drawing.Point(8, 9);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(693, 324);
			this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl1.TabIndex = 0;
			// 
			// tabGeneral
			// 
			this.tabGeneral.BackColor = System.Drawing.SystemColors.Control;
			this.tabGeneral.Controls.Add(this.gbxGeneral);
			this.tabGeneral.Location = new System.Drawing.Point(4, 24);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabGeneral.Size = new System.Drawing.Size(685, 296);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			// 
			// gbxGeneral
			// 
			this.gbxGeneral.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.gbxGeneral.Controls.Add(this.cboTheme);
			this.gbxGeneral.Controls.Add(this.lblTheme);
			this.gbxGeneral.Controls.Add(this.cbxShowDeleteCometConfirmation);
			this.gbxGeneral.Controls.Add(this.txtDownloadUrl);
			this.gbxGeneral.Controls.Add(this.lblDownloadUrl);
			this.gbxGeneral.Controls.Add(this.cbxShowLongCalculationConfirmation);
			this.gbxGeneral.Controls.Add(this.lblDays);
			this.gbxGeneral.Controls.Add(this.txtUpdateInterval);
			this.gbxGeneral.Controls.Add(this.chShowStatusBar);
			this.gbxGeneral.Controls.Add(this.chExitWithoutConfirm);
			this.gbxGeneral.Controls.Add(this.chNewVersionOnStartup);
			this.gbxGeneral.Controls.Add(this.chRememberWindowPosition);
			this.gbxGeneral.Controls.Add(this.chAutomaticUpdate);
			this.gbxGeneral.Location = new System.Drawing.Point(8, 3);
			this.gbxGeneral.Name = "gbxGeneral";
			this.gbxGeneral.Size = new System.Drawing.Size(669, 285);
			this.gbxGeneral.TabIndex = 0;
			this.gbxGeneral.TabStop = false;
			// 
			// cboTheme
			// 
			this.cboTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTheme.FormattingEnabled = true;
			this.cboTheme.Location = new System.Drawing.Point(148, 17);
			this.cboTheme.Name = "cboTheme";
			this.cboTheme.Size = new System.Drawing.Size(130, 21);
			this.cboTheme.TabIndex = 14;
			// 
			// lblTheme
			// 
			this.lblTheme.AutoSize = true;
			this.lblTheme.Location = new System.Drawing.Point(13, 20);
			this.lblTheme.Name = "lblTheme";
			this.lblTheme.Size = new System.Drawing.Size(129, 13);
			this.lblTheme.TabIndex = 13;
			this.lblTheme.Text = "Theme (requires restart):";
			// 
			// cbxShowDeleteCometConfirmation
			// 
			this.cbxShowDeleteCometConfirmation.AutoSize = true;
			this.cbxShowDeleteCometConfirmation.Location = new System.Drawing.Point(13, 159);
			this.cbxShowDeleteCometConfirmation.Name = "cbxShowDeleteCometConfirmation";
			this.cbxShowDeleteCometConfirmation.Size = new System.Drawing.Size(179, 17);
			this.cbxShowDeleteCometConfirmation.TabIndex = 9;
			this.cbxShowDeleteCometConfirmation.Text = "Show delete comet confirmation";
			this.cbxShowDeleteCometConfirmation.UseVisualStyleBackColor = true;
			// 
			// txtDownloadUrl
			// 
			this.txtDownloadUrl.Location = new System.Drawing.Point(148, 46);
			this.txtDownloadUrl.Name = "txtDownloadUrl";
			this.txtDownloadUrl.Size = new System.Drawing.Size(400, 21);
			this.txtDownloadUrl.TabIndex = 1;
			// 
			// lblDownloadUrl
			// 
			this.lblDownloadUrl.AutoSize = true;
			this.lblDownloadUrl.Location = new System.Drawing.Point(13, 49);
			this.lblDownloadUrl.Name = "lblDownloadUrl";
			this.lblDownloadUrl.Size = new System.Drawing.Size(125, 13);
			this.lblDownloadUrl.TabIndex = 0;
			this.lblDownloadUrl.Text = "Elements download URL:";
			// 
			// cbxShowLongCalculationConfirmation
			// 
			this.cbxShowLongCalculationConfirmation.AutoSize = true;
			this.cbxShowLongCalculationConfirmation.Location = new System.Drawing.Point(13, 132);
			this.cbxShowLongCalculationConfirmation.Name = "cbxShowLongCalculationConfirmation";
			this.cbxShowLongCalculationConfirmation.Size = new System.Drawing.Size(190, 17);
			this.cbxShowLongCalculationConfirmation.TabIndex = 6;
			this.cbxShowLongCalculationConfirmation.Text = "Show long calculation confirmation";
			this.cbxShowLongCalculationConfirmation.UseVisualStyleBackColor = true;
			// 
			// lblDays
			// 
			this.lblDays.AutoSize = true;
			this.lblDays.Location = new System.Drawing.Point(270, 79);
			this.lblDays.Name = "lblDays";
			this.lblDays.Size = new System.Drawing.Size(30, 13);
			this.lblDays.TabIndex = 4;
			this.lblDays.Text = "days";
			// 
			// txtUpdateInterval
			// 
			this.txtUpdateInterval.Location = new System.Drawing.Point(220, 76);
			this.txtUpdateInterval.MaxLength = 2;
			this.txtUpdateInterval.Name = "txtUpdateInterval";
			this.txtUpdateInterval.Size = new System.Drawing.Size(44, 21);
			this.txtUpdateInterval.TabIndex = 3;
			this.txtUpdateInterval.Text = "7";
			this.txtUpdateInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtUpdateInterval.KeyDown += this.txtUpdateInterval_KeyDown;
			this.txtUpdateInterval.KeyPress += this.txtUpdateInterval_KeyPress;
			// 
			// chShowStatusBar
			// 
			this.chShowStatusBar.AutoSize = true;
			this.chShowStatusBar.Location = new System.Drawing.Point(13, 213);
			this.chShowStatusBar.Name = "chShowStatusBar";
			this.chShowStatusBar.Size = new System.Drawing.Size(104, 17);
			this.chShowStatusBar.TabIndex = 8;
			this.chShowStatusBar.Text = "Show status bar";
			this.chShowStatusBar.UseVisualStyleBackColor = true;
			// 
			// chExitWithoutConfirm
			// 
			this.chExitWithoutConfirm.AutoSize = true;
			this.chExitWithoutConfirm.Location = new System.Drawing.Point(13, 186);
			this.chExitWithoutConfirm.Name = "chExitWithoutConfirm";
			this.chExitWithoutConfirm.Size = new System.Drawing.Size(121, 17);
			this.chExitWithoutConfirm.TabIndex = 7;
			this.chExitWithoutConfirm.Text = "Exit without confirm";
			this.chExitWithoutConfirm.UseVisualStyleBackColor = true;
			// 
			// chNewVersionOnStartup
			// 
			this.chNewVersionOnStartup.AutoSize = true;
			this.chNewVersionOnStartup.Location = new System.Drawing.Point(13, 240);
			this.chNewVersionOnStartup.Name = "chNewVersionOnStartup";
			this.chNewVersionOnStartup.Size = new System.Drawing.Size(186, 17);
			this.chNewVersionOnStartup.TabIndex = 0;
			this.chNewVersionOnStartup.Text = "Check for new version on startup";
			this.chNewVersionOnStartup.UseVisualStyleBackColor = true;
			this.chNewVersionOnStartup.Visible = false;
			// 
			// chRememberWindowPosition
			// 
			this.chRememberWindowPosition.AutoSize = true;
			this.chRememberWindowPosition.Location = new System.Drawing.Point(13, 105);
			this.chRememberWindowPosition.Name = "chRememberWindowPosition";
			this.chRememberWindowPosition.Size = new System.Drawing.Size(156, 17);
			this.chRememberWindowPosition.TabIndex = 5;
			this.chRememberWindowPosition.Text = "Remember window position";
			this.chRememberWindowPosition.UseVisualStyleBackColor = true;
			// 
			// chAutomaticUpdate
			// 
			this.chAutomaticUpdate.AutoSize = true;
			this.chAutomaticUpdate.Location = new System.Drawing.Point(13, 78);
			this.chAutomaticUpdate.Name = "chAutomaticUpdate";
			this.chAutomaticUpdate.Size = new System.Drawing.Size(204, 17);
			this.chAutomaticUpdate.TabIndex = 2;
			this.chAutomaticUpdate.Text = "Automatically update elements after:";
			this.chAutomaticUpdate.UseVisualStyleBackColor = true;
			// 
			// tabLocation
			// 
			this.tabLocation.BackColor = System.Drawing.SystemColors.Control;
			this.tabLocation.Controls.Add(this.gbxLocation);
			this.tabLocation.Location = new System.Drawing.Point(4, 24);
			this.tabLocation.Name = "tabLocation";
			this.tabLocation.Padding = new System.Windows.Forms.Padding(3);
			this.tabLocation.Size = new System.Drawing.Size(685, 296);
			this.tabLocation.TabIndex = 1;
			this.tabLocation.Text = "Location";
			// 
			// gbxLocation
			// 
			this.gbxLocation.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.gbxLocation.Controls.Add(this.cbxEastWest);
			this.gbxLocation.Controls.Add(this.cbxNorthSouth);
			this.gbxLocation.Controls.Add(this.lblLonDeg);
			this.gbxLocation.Controls.Add(this.lblLatDeg);
			this.gbxLocation.Controls.Add(this.lblLongitude);
			this.gbxLocation.Controls.Add(this.txtLongitude);
			this.gbxLocation.Controls.Add(this.txtName);
			this.gbxLocation.Controls.Add(this.lblLatitude);
			this.gbxLocation.Controls.Add(this.txtLatitude);
			this.gbxLocation.Controls.Add(this.lblName);
			this.gbxLocation.Location = new System.Drawing.Point(8, 3);
			this.gbxLocation.Name = "gbxLocation";
			this.gbxLocation.Size = new System.Drawing.Size(669, 285);
			this.gbxLocation.TabIndex = 0;
			this.gbxLocation.TabStop = false;
			// 
			// cbxEastWest
			// 
			this.cbxEastWest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxEastWest.FormattingEnabled = true;
			this.cbxEastWest.Items.AddRange(new object[] { "East", "West" });
			this.cbxEastWest.Location = new System.Drawing.Point(181, 182);
			this.cbxEastWest.Name = "cbxEastWest";
			this.cbxEastWest.Size = new System.Drawing.Size(100, 21);
			this.cbxEastWest.TabIndex = 4;
			// 
			// cbxNorthSouth
			// 
			this.cbxNorthSouth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxNorthSouth.FormattingEnabled = true;
			this.cbxNorthSouth.Items.AddRange(new object[] { "North", "South" });
			this.cbxNorthSouth.Location = new System.Drawing.Point(181, 115);
			this.cbxNorthSouth.Name = "cbxNorthSouth";
			this.cbxNorthSouth.Size = new System.Drawing.Size(100, 21);
			this.cbxNorthSouth.TabIndex = 2;
			// 
			// lblLonDeg
			// 
			this.lblLonDeg.AutoSize = true;
			this.lblLonDeg.Location = new System.Drawing.Point(150, 182);
			this.lblLonDeg.Name = "lblLonDeg";
			this.lblLonDeg.Size = new System.Drawing.Size(12, 13);
			this.lblLonDeg.TabIndex = 54;
			this.lblLonDeg.Text = "°";
			// 
			// lblLatDeg
			// 
			this.lblLatDeg.AutoSize = true;
			this.lblLatDeg.Location = new System.Drawing.Point(150, 115);
			this.lblLatDeg.Name = "lblLatDeg";
			this.lblLatDeg.Size = new System.Drawing.Size(12, 13);
			this.lblLatDeg.TabIndex = 45;
			this.lblLatDeg.Text = "°";
			// 
			// lblLongitude
			// 
			this.lblLongitude.AutoSize = true;
			this.lblLongitude.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.lblLongitude.Location = new System.Drawing.Point(6, 151);
			this.lblLongitude.Name = "lblLongitude";
			this.lblLongitude.Size = new System.Drawing.Size(63, 13);
			this.lblLongitude.TabIndex = 44;
			this.lblLongitude.Text = "Longitude";
			// 
			// txtLongitude
			// 
			this.txtLongitude.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtLongitude.Location = new System.Drawing.Point(31, 182);
			this.txtLongitude.MaxLength = 100;
			this.txtLongitude.Name = "txtLongitude";
			this.txtLongitude.Size = new System.Drawing.Size(115, 21);
			this.txtLongitude.TabIndex = 3;
			this.txtLongitude.KeyPress += this.txtLatitudeLongitude_KeyPress;
			// 
			// txtName
			// 
			this.txtName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtName.Location = new System.Drawing.Point(31, 48);
			this.txtName.MaxLength = 128;
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(250, 21);
			this.txtName.TabIndex = 0;
			// 
			// lblLatitude
			// 
			this.lblLatitude.AutoSize = true;
			this.lblLatitude.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.lblLatitude.Location = new System.Drawing.Point(6, 84);
			this.lblLatitude.Name = "lblLatitude";
			this.lblLatitude.Size = new System.Drawing.Size(54, 13);
			this.lblLatitude.TabIndex = 41;
			this.lblLatitude.Text = "Latitude";
			// 
			// txtLatitude
			// 
			this.txtLatitude.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtLatitude.Location = new System.Drawing.Point(31, 115);
			this.txtLatitude.MaxLength = 100;
			this.txtLatitude.Name = "txtLatitude";
			this.txtLatitude.Size = new System.Drawing.Size(115, 21);
			this.txtLatitude.TabIndex = 1;
			this.txtLatitude.KeyPress += this.txtLatitudeLongitude_KeyPress;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.lblName.Location = new System.Drawing.Point(6, 17);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(39, 13);
			this.lblName.TabIndex = 38;
			this.lblName.Text = "Name";
			// 
			// tabPrograms
			// 
			this.tabPrograms.BackColor = System.Drawing.SystemColors.Control;
			this.tabPrograms.Controls.Add(this.gbxPrograms);
			this.tabPrograms.Controls.Add(this.gbxAddProgram);
			this.tabPrograms.Location = new System.Drawing.Point(4, 24);
			this.tabPrograms.Name = "tabPrograms";
			this.tabPrograms.Size = new System.Drawing.Size(685, 296);
			this.tabPrograms.TabIndex = 2;
			this.tabPrograms.Text = "Programs";
			// 
			// gbxPrograms
			// 
			this.gbxPrograms.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.gbxPrograms.Controls.Add(this.dgvPrograms);
			this.gbxPrograms.Controls.Add(this.btnClear);
			this.gbxPrograms.Controls.Add(this.btnRemove);
			this.gbxPrograms.Controls.Add(this.btnEdit);
			this.gbxPrograms.Controls.Add(this.btnAdd);
			this.gbxPrograms.Location = new System.Drawing.Point(8, 3);
			this.gbxPrograms.Name = "gbxPrograms";
			this.gbxPrograms.Size = new System.Drawing.Size(669, 285);
			this.gbxPrograms.TabIndex = 4;
			this.gbxPrograms.TabStop = false;
			// 
			// dgvPrograms
			// 
			this.dgvPrograms.AllowUserToAddRows = false;
			this.dgvPrograms.AllowUserToDeleteRows = false;
			this.dgvPrograms.AllowUserToResizeRows = false;
			this.dgvPrograms.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
			this.dgvPrograms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvPrograms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colProgram, this.colDirectory });
			this.dgvPrograms.Location = new System.Drawing.Point(3, 10);
			this.dgvPrograms.MultiSelect = false;
			this.dgvPrograms.Name = "dgvPrograms";
			this.dgvPrograms.ReadOnly = true;
			this.dgvPrograms.RowHeadersVisible = false;
			this.dgvPrograms.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			this.dgvPrograms.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
			this.dgvPrograms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvPrograms.Size = new System.Drawing.Size(663, 231);
			this.dgvPrograms.TabIndex = 0;
			// 
			// colProgram
			// 
			this.colProgram.DataPropertyName = "Name";
			this.colProgram.HeaderText = "Program";
			this.colProgram.Name = "colProgram";
			this.colProgram.ReadOnly = true;
			this.colProgram.Width = 200;
			// 
			// colDirectory
			// 
			this.colDirectory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colDirectory.DataPropertyName = "Directory";
			this.colDirectory.HeaderText = "Directory";
			this.colDirectory.Name = "colDirectory";
			this.colDirectory.ReadOnly = true;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(304, 252);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(94, 23);
			this.btnClear.TabIndex = 4;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += this.btnClear_Click;
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(206, 252);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(94, 23);
			this.btnRemove.TabIndex = 3;
			this.btnRemove.Text = "Remove";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += this.btnRemove_Click;
			// 
			// btnEdit
			// 
			this.btnEdit.Location = new System.Drawing.Point(108, 252);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(94, 23);
			this.btnEdit.TabIndex = 2;
			this.btnEdit.Text = "Edit";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += this.btnEdit_Click;
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(10, 252);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(94, 23);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += this.btnAdd_Click;
			// 
			// gbxAddProgram
			// 
			this.gbxAddProgram.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.gbxAddProgram.Controls.Add(this.txtDirectory);
			this.gbxAddProgram.Controls.Add(this.lblDirectory);
			this.gbxAddProgram.Controls.Add(this.btnBrowse);
			this.gbxAddProgram.Controls.Add(this.cbxExternalProgram);
			this.gbxAddProgram.Controls.Add(this.lblSelectProgram);
			this.gbxAddProgram.Controls.Add(this.btnProgramsOk);
			this.gbxAddProgram.Controls.Add(this.btnProgramsCancel);
			this.gbxAddProgram.Location = new System.Drawing.Point(8, 3);
			this.gbxAddProgram.Name = "gbxAddProgram";
			this.gbxAddProgram.Size = new System.Drawing.Size(669, 285);
			this.gbxAddProgram.TabIndex = 5;
			this.gbxAddProgram.TabStop = false;
			this.gbxAddProgram.Visible = false;
			// 
			// txtDirectory
			// 
			this.txtDirectory.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtDirectory.Location = new System.Drawing.Point(155, 115);
			this.txtDirectory.Name = "txtDirectory";
			this.txtDirectory.Size = new System.Drawing.Size(499, 21);
			this.txtDirectory.TabIndex = 2;
			// 
			// lblDirectory
			// 
			this.lblDirectory.AutoSize = true;
			this.lblDirectory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.lblDirectory.Location = new System.Drawing.Point(6, 84);
			this.lblDirectory.Name = "lblDirectory";
			this.lblDirectory.Size = new System.Drawing.Size(60, 13);
			this.lblDirectory.TabIndex = 38;
			this.lblDirectory.Text = "Directory";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(31, 114);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(118, 23);
			this.btnBrowse.TabIndex = 1;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += this.btnBrowse_Click;
			// 
			// cbxExternalProgram
			// 
			this.cbxExternalProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxExternalProgram.FormattingEnabled = true;
			this.cbxExternalProgram.Location = new System.Drawing.Point(31, 47);
			this.cbxExternalProgram.Name = "cbxExternalProgram";
			this.cbxExternalProgram.Size = new System.Drawing.Size(280, 21);
			this.cbxExternalProgram.TabIndex = 0;
			this.cbxExternalProgram.SelectedIndexChanged += this.cbxProgram_SelectedIndexChanged;
			// 
			// lblSelectProgram
			// 
			this.lblSelectProgram.AutoSize = true;
			this.lblSelectProgram.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.lblSelectProgram.Location = new System.Drawing.Point(6, 17);
			this.lblSelectProgram.Name = "lblSelectProgram";
			this.lblSelectProgram.Size = new System.Drawing.Size(94, 13);
			this.lblSelectProgram.TabIndex = 27;
			this.lblSelectProgram.Text = "Select program";
			// 
			// btnProgramsOk
			// 
			this.btnProgramsOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.btnProgramsOk.Location = new System.Drawing.Point(31, 186);
			this.btnProgramsOk.Name = "btnProgramsOk";
			this.btnProgramsOk.Size = new System.Drawing.Size(118, 23);
			this.btnProgramsOk.TabIndex = 3;
			this.btnProgramsOk.Text = "OK";
			this.btnProgramsOk.UseVisualStyleBackColor = true;
			this.btnProgramsOk.Click += this.btnProgramsOk_Click;
			// 
			// btnProgramsCancel
			// 
			this.btnProgramsCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.btnProgramsCancel.Location = new System.Drawing.Point(155, 186);
			this.btnProgramsCancel.Name = "btnProgramsCancel";
			this.btnProgramsCancel.Size = new System.Drawing.Size(118, 23);
			this.btnProgramsCancel.TabIndex = 4;
			this.btnProgramsCancel.Text = "Cancel";
			this.btnProgramsCancel.UseVisualStyleBackColor = true;
			this.btnProgramsCancel.Click += this.btnProgramsCancel_Click;
			// 
			// tabNetwork
			// 
			this.tabNetwork.BackColor = System.Drawing.SystemColors.Control;
			this.tabNetwork.Controls.Add(this.gbxNetwork);
			this.tabNetwork.Location = new System.Drawing.Point(4, 24);
			this.tabNetwork.Name = "tabNetwork";
			this.tabNetwork.Size = new System.Drawing.Size(685, 296);
			this.tabNetwork.TabIndex = 3;
			this.tabNetwork.Text = "Network";
			// 
			// gbxNetwork
			// 
			this.gbxNetwork.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.gbxNetwork.Controls.Add(this.pnlProxy);
			this.gbxNetwork.Controls.Add(this.rbManualProxy);
			this.gbxNetwork.Controls.Add(this.rbNoProxy);
			this.gbxNetwork.Location = new System.Drawing.Point(8, 3);
			this.gbxNetwork.Name = "gbxNetwork";
			this.gbxNetwork.Size = new System.Drawing.Size(669, 285);
			this.gbxNetwork.TabIndex = 0;
			this.gbxNetwork.TabStop = false;
			// 
			// pnlProxy
			// 
			this.pnlProxy.Controls.Add(this.lblPort);
			this.pnlProxy.Controls.Add(this.txtPort);
			this.pnlProxy.Controls.Add(this.lblProxy);
			this.pnlProxy.Controls.Add(this.txtProxy);
			this.pnlProxy.Controls.Add(this.lblUsername);
			this.pnlProxy.Controls.Add(this.txtUsername);
			this.pnlProxy.Controls.Add(this.txtPassword);
			this.pnlProxy.Controls.Add(this.lblDomain);
			this.pnlProxy.Controls.Add(this.txtDomain);
			this.pnlProxy.Controls.Add(this.lblPassword);
			this.pnlProxy.Enabled = false;
			this.pnlProxy.Location = new System.Drawing.Point(3, 73);
			this.pnlProxy.Name = "pnlProxy";
			this.pnlProxy.Size = new System.Drawing.Size(663, 209);
			this.pnlProxy.TabIndex = 2;
			// 
			// lblPort
			// 
			this.lblPort.AutoSize = true;
			this.lblPort.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.lblPort.Location = new System.Drawing.Point(3, 78);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(31, 13);
			this.lblPort.TabIndex = 58;
			this.lblPort.Text = "Port";
			// 
			// txtPort
			// 
			this.txtPort.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtPort.Location = new System.Drawing.Point(28, 109);
			this.txtPort.MaxLength = 5;
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(62, 21);
			this.txtPort.TabIndex = 1;
			// 
			// lblProxy
			// 
			this.lblProxy.AutoSize = true;
			this.lblProxy.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.lblProxy.Location = new System.Drawing.Point(3, 11);
			this.lblProxy.Name = "lblProxy";
			this.lblProxy.Size = new System.Drawing.Size(40, 13);
			this.lblProxy.TabIndex = 57;
			this.lblProxy.Text = "Proxy";
			// 
			// txtProxy
			// 
			this.txtProxy.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtProxy.Location = new System.Drawing.Point(28, 42);
			this.txtProxy.MaxLength = 128;
			this.txtProxy.Name = "txtProxy";
			this.txtProxy.Size = new System.Drawing.Size(160, 21);
			this.txtProxy.TabIndex = 0;
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.lblUsername.Location = new System.Drawing.Point(248, 78);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(65, 13);
			this.lblUsername.TabIndex = 54;
			this.lblUsername.Text = "Username";
			// 
			// txtUsername
			// 
			this.txtUsername.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtUsername.Location = new System.Drawing.Point(273, 109);
			this.txtUsername.MaxLength = 256;
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(243, 21);
			this.txtUsername.TabIndex = 3;
			// 
			// txtPassword
			// 
			this.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtPassword.Location = new System.Drawing.Point(273, 176);
			this.txtPassword.MaxLength = 256;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '•';
			this.txtPassword.Size = new System.Drawing.Size(243, 21);
			this.txtPassword.TabIndex = 4;
			// 
			// lblDomain
			// 
			this.lblDomain.AutoSize = true;
			this.lblDomain.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.lblDomain.Location = new System.Drawing.Point(248, 11);
			this.lblDomain.Name = "lblDomain";
			this.lblDomain.Size = new System.Drawing.Size(50, 13);
			this.lblDomain.TabIndex = 53;
			this.lblDomain.Text = "Domain";
			// 
			// txtDomain
			// 
			this.txtDomain.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtDomain.Location = new System.Drawing.Point(273, 42);
			this.txtDomain.MaxLength = 256;
			this.txtDomain.Name = "txtDomain";
			this.txtDomain.Size = new System.Drawing.Size(243, 21);
			this.txtDomain.TabIndex = 2;
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.lblPassword.Location = new System.Drawing.Point(248, 145);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(61, 13);
			this.lblPassword.TabIndex = 52;
			this.lblPassword.Text = "Password";
			// 
			// rbManualProxy
			// 
			this.rbManualProxy.AutoSize = true;
			this.rbManualProxy.Location = new System.Drawing.Point(9, 48);
			this.rbManualProxy.Name = "rbManualProxy";
			this.rbManualProxy.Size = new System.Drawing.Size(156, 17);
			this.rbManualProxy.TabIndex = 1;
			this.rbManualProxy.Text = "Manual proxy configuration";
			this.rbManualProxy.UseVisualStyleBackColor = true;
			this.rbManualProxy.CheckedChanged += this.rbCommon_CheckedChanged;
			// 
			// rbNoProxy
			// 
			this.rbNoProxy.AutoSize = true;
			this.rbNoProxy.Checked = true;
			this.rbNoProxy.Location = new System.Drawing.Point(9, 17);
			this.rbNoProxy.Name = "rbNoProxy";
			this.rbNoProxy.Size = new System.Drawing.Size(69, 17);
			this.rbNoProxy.TabIndex = 0;
			this.rbNoProxy.TabStop = true;
			this.rbNoProxy.Text = "No proxy";
			this.rbNoProxy.UseVisualStyleBackColor = true;
			this.rbNoProxy.CheckedChanged += this.rbCommon_CheckedChanged;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(599, 345);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.btnOk.Location = new System.Drawing.Point(493, 345);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(100, 24);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += this.btnOK_Click;
			// 
			// FormSettings
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(709, 381);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSettings";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Load += this.FormSettings_Load;
			this.tabControl1.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.gbxGeneral.ResumeLayout(false);
			this.gbxGeneral.PerformLayout();
			this.tabLocation.ResumeLayout(false);
			this.gbxLocation.ResumeLayout(false);
			this.gbxLocation.PerformLayout();
			this.tabPrograms.ResumeLayout(false);
			this.gbxPrograms.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.dgvPrograms).EndInit();
			this.gbxAddProgram.ResumeLayout(false);
			this.gbxAddProgram.PerformLayout();
			this.tabNetwork.ResumeLayout(false);
			this.gbxNetwork.ResumeLayout(false);
			this.gbxNetwork.PerformLayout();
			this.pnlProxy.ResumeLayout(false);
			this.pnlProxy.PerformLayout();
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabLocation;
		private System.Windows.Forms.TabPage tabPrograms;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox gbxGeneral;
		private System.Windows.Forms.CheckBox chExitWithoutConfirm;
		private System.Windows.Forms.CheckBox chNewVersionOnStartup;
		private System.Windows.Forms.CheckBox chRememberWindowPosition;
		private System.Windows.Forms.CheckBox chAutomaticUpdate;
		private System.Windows.Forms.GroupBox gbxLocation;
		private System.Windows.Forms.ComboBox cbxEastWest;
		private System.Windows.Forms.ComboBox cbxNorthSouth;
		private System.Windows.Forms.Label lblLonDeg;
		private System.Windows.Forms.Label lblLatDeg;
		private System.Windows.Forms.Label lblLongitude;
		private System.Windows.Forms.TextBox txtLongitude;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblLatitude;
		private System.Windows.Forms.TextBox txtLatitude;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox gbxPrograms;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.TabPage tabNetwork;
		private System.Windows.Forms.GroupBox gbxNetwork;
		private System.Windows.Forms.Panel pnlProxy;
		private System.Windows.Forms.Label lblPort;
		private System.Windows.Forms.TextBox txtPort;
		private System.Windows.Forms.Label lblProxy;
		private System.Windows.Forms.TextBox txtProxy;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label lblDomain;
		private System.Windows.Forms.TextBox txtDomain;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.RadioButton rbManualProxy;
		private System.Windows.Forms.RadioButton rbNoProxy;
		private System.Windows.Forms.CheckBox chShowStatusBar;
		private System.Windows.Forms.GroupBox gbxAddProgram;
		private System.Windows.Forms.TextBox txtDirectory;
		private System.Windows.Forms.Label lblDirectory;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.ComboBox cbxExternalProgram;
		private System.Windows.Forms.Label lblSelectProgram;
		private System.Windows.Forms.Button btnProgramsOk;
		private System.Windows.Forms.Button btnProgramsCancel;
		//private System.Windows.Forms.BindingSource externalProgramBindingSource;
		private System.Windows.Forms.DataGridView dgvPrograms;
		private System.Windows.Forms.DataGridViewTextBoxColumn colProgram;
		private System.Windows.Forms.DataGridViewTextBoxColumn colDirectory;
		private System.Windows.Forms.TextBox txtUpdateInterval;
		private System.Windows.Forms.Label lblDays;
		private System.Windows.Forms.CheckBox cbxShowLongCalculationConfirmation;
		private System.Windows.Forms.TextBox txtDownloadUrl;
		private System.Windows.Forms.Label lblDownloadUrl;
		private System.Windows.Forms.CheckBox cbxShowDeleteCometConfirmation;
		private System.Windows.Forms.Label lblTheme;
		private System.Windows.Forms.ComboBox cboTheme;
	}
}
