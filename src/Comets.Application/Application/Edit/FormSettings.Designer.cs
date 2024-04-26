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
			tabControl1 = new TabControl();
			tabGeneral = new TabPage();
			gbxGeneral = new GroupBox();
			cbxShowDeleteCometConfirmation = new CheckBox();
			txtDownloadUrl = new TextBox();
			lblDownloadUrl = new Label();
			cbxShowLongCalculationConfirmation = new CheckBox();
			lblDays = new Label();
			txtUpdateInterval = new TextBox();
			chShowStatusBar = new CheckBox();
			chExitWithoutConfirm = new CheckBox();
			chNewVersionOnStartup = new CheckBox();
			chRememberWindowPosition = new CheckBox();
			chAutomaticUpdate = new CheckBox();
			tabLocation = new TabPage();
			gbxLocation = new GroupBox();
			cbxEastWest = new ComboBox();
			cbxNorthSouth = new ComboBox();
			lblLonDeg = new Label();
			lblLatDeg = new Label();
			lblLongitude = new Label();
			txtLongitude = new TextBox();
			txtName = new TextBox();
			lblLatitude = new Label();
			txtLatitude = new TextBox();
			lblName = new Label();
			tabPrograms = new TabPage();
			gbxPrograms = new GroupBox();
			dgvPrograms = new DataGridView();
			colProgram = new DataGridViewTextBoxColumn();
			colDirectory = new DataGridViewTextBoxColumn();
			btnClear = new Button();
			btnRemove = new Button();
			btnEdit = new Button();
			btnAdd = new Button();
			gbxAddProgram = new GroupBox();
			txtDirectory = new TextBox();
			lblDirectory = new Label();
			btnBrowse = new Button();
			cbxExternalProgram = new ComboBox();
			lblSelectProgram = new Label();
			btnProgramsOk = new Button();
			btnProgramsCancel = new Button();
			tabNetwork = new TabPage();
			gbxNetwork = new GroupBox();
			pnlProxy = new Panel();
			lblPort = new Label();
			txtPort = new TextBox();
			lblProxy = new Label();
			txtProxy = new TextBox();
			lblUsername = new Label();
			txtUsername = new TextBox();
			txtPassword = new TextBox();
			lblDomain = new Label();
			txtDomain = new TextBox();
			lblPassword = new Label();
			rbManualProxy = new RadioButton();
			rbNoProxy = new RadioButton();
			btnCancel = new Button();
			btnOk = new Button();
			tabControl1.SuspendLayout();
			tabGeneral.SuspendLayout();
			gbxGeneral.SuspendLayout();
			tabLocation.SuspendLayout();
			gbxLocation.SuspendLayout();
			tabPrograms.SuspendLayout();
			gbxPrograms.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvPrograms).BeginInit();
			gbxAddProgram.SuspendLayout();
			tabNetwork.SuspendLayout();
			gbxNetwork.SuspendLayout();
			pnlProxy.SuspendLayout();
			SuspendLayout();
			// 
			// tabControl1
			// 
			tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			tabControl1.Controls.Add(tabGeneral);
			tabControl1.Controls.Add(tabLocation);
			tabControl1.Controls.Add(tabPrograms);
			tabControl1.Controls.Add(tabNetwork);
			tabControl1.ItemSize = new Size(120, 20);
			tabControl1.Location = new Point(8, 9);
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new Size(693, 324);
			tabControl1.SizeMode = TabSizeMode.Fixed;
			tabControl1.TabIndex = 0;
			// 
			// tabGeneral
			// 
			tabGeneral.BackColor = SystemColors.Control;
			tabGeneral.Controls.Add(gbxGeneral);
			tabGeneral.Location = new Point(4, 24);
			tabGeneral.Name = "tabGeneral";
			tabGeneral.Padding = new Padding(3);
			tabGeneral.Size = new Size(685, 296);
			tabGeneral.TabIndex = 0;
			tabGeneral.Text = "General";
			// 
			// gbxGeneral
			// 
			gbxGeneral.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			gbxGeneral.Controls.Add(cbxShowDeleteCometConfirmation);
			gbxGeneral.Controls.Add(txtDownloadUrl);
			gbxGeneral.Controls.Add(lblDownloadUrl);
			gbxGeneral.Controls.Add(cbxShowLongCalculationConfirmation);
			gbxGeneral.Controls.Add(lblDays);
			gbxGeneral.Controls.Add(txtUpdateInterval);
			gbxGeneral.Controls.Add(chShowStatusBar);
			gbxGeneral.Controls.Add(chExitWithoutConfirm);
			gbxGeneral.Controls.Add(chNewVersionOnStartup);
			gbxGeneral.Controls.Add(chRememberWindowPosition);
			gbxGeneral.Controls.Add(chAutomaticUpdate);
			gbxGeneral.Location = new Point(8, 3);
			gbxGeneral.Name = "gbxGeneral";
			gbxGeneral.Size = new Size(669, 285);
			gbxGeneral.TabIndex = 0;
			gbxGeneral.TabStop = false;
			// 
			// cbxShowDeleteCometConfirmation
			// 
			cbxShowDeleteCometConfirmation.AutoSize = true;
			cbxShowDeleteCometConfirmation.Location = new Point(13, 160);
			cbxShowDeleteCometConfirmation.Name = "cbxShowDeleteCometConfirmation";
			cbxShowDeleteCometConfirmation.Size = new Size(179, 17);
			cbxShowDeleteCometConfirmation.TabIndex = 9;
			cbxShowDeleteCometConfirmation.Text = "Show delete comet confirmation";
			cbxShowDeleteCometConfirmation.UseVisualStyleBackColor = true;
			// 
			// txtDownloadUrl
			// 
			txtDownloadUrl.Location = new Point(99, 17);
			txtDownloadUrl.Name = "txtDownloadUrl";
			txtDownloadUrl.Size = new Size(400, 21);
			txtDownloadUrl.TabIndex = 1;
			// 
			// lblDownloadUrl
			// 
			lblDownloadUrl.AutoSize = true;
			lblDownloadUrl.Location = new Point(13, 20);
			lblDownloadUrl.Name = "lblDownloadUrl";
			lblDownloadUrl.Size = new Size(80, 13);
			lblDownloadUrl.TabIndex = 0;
			lblDownloadUrl.Text = "Download URL:";
			// 
			// cbxShowLongCalculationConfirmation
			// 
			cbxShowLongCalculationConfirmation.AutoSize = true;
			cbxShowLongCalculationConfirmation.Location = new Point(13, 125);
			cbxShowLongCalculationConfirmation.Name = "cbxShowLongCalculationConfirmation";
			cbxShowLongCalculationConfirmation.Size = new Size(190, 17);
			cbxShowLongCalculationConfirmation.TabIndex = 6;
			cbxShowLongCalculationConfirmation.Text = "Show long calculation confirmation";
			cbxShowLongCalculationConfirmation.UseVisualStyleBackColor = true;
			// 
			// lblDays
			// 
			lblDays.AutoSize = true;
			lblDays.Location = new Point(270, 56);
			lblDays.Name = "lblDays";
			lblDays.Size = new Size(30, 13);
			lblDays.TabIndex = 4;
			lblDays.Text = "days";
			// 
			// txtUpdateInterval
			// 
			txtUpdateInterval.Location = new Point(220, 53);
			txtUpdateInterval.MaxLength = 2;
			txtUpdateInterval.Name = "txtUpdateInterval";
			txtUpdateInterval.Size = new Size(44, 21);
			txtUpdateInterval.TabIndex = 3;
			txtUpdateInterval.Text = "7";
			txtUpdateInterval.TextAlign = HorizontalAlignment.Center;
			txtUpdateInterval.KeyDown += txtUpdateInterval_KeyDown;
			txtUpdateInterval.KeyPress += txtUpdateInterval_KeyPress;
			// 
			// chShowStatusBar
			// 
			chShowStatusBar.AutoSize = true;
			chShowStatusBar.Location = new Point(13, 230);
			chShowStatusBar.Name = "chShowStatusBar";
			chShowStatusBar.Size = new Size(104, 17);
			chShowStatusBar.TabIndex = 8;
			chShowStatusBar.Text = "Show status bar";
			chShowStatusBar.UseVisualStyleBackColor = true;
			// 
			// chExitWithoutConfirm
			// 
			chExitWithoutConfirm.AutoSize = true;
			chExitWithoutConfirm.Location = new Point(13, 195);
			chExitWithoutConfirm.Name = "chExitWithoutConfirm";
			chExitWithoutConfirm.Size = new Size(121, 17);
			chExitWithoutConfirm.TabIndex = 7;
			chExitWithoutConfirm.Text = "Exit without confirm";
			chExitWithoutConfirm.UseVisualStyleBackColor = true;
			// 
			// chNewVersionOnStartup
			// 
			chNewVersionOnStartup.AutoSize = true;
			chNewVersionOnStartup.Location = new Point(13, 265);
			chNewVersionOnStartup.Name = "chNewVersionOnStartup";
			chNewVersionOnStartup.Size = new Size(186, 17);
			chNewVersionOnStartup.TabIndex = 0;
			chNewVersionOnStartup.Text = "Check for new version on startup";
			chNewVersionOnStartup.UseVisualStyleBackColor = true;
			chNewVersionOnStartup.Visible = false;
			// 
			// chRememberWindowPosition
			// 
			chRememberWindowPosition.AutoSize = true;
			chRememberWindowPosition.Location = new Point(13, 90);
			chRememberWindowPosition.Name = "chRememberWindowPosition";
			chRememberWindowPosition.Size = new Size(156, 17);
			chRememberWindowPosition.TabIndex = 5;
			chRememberWindowPosition.Text = "Remember window position";
			chRememberWindowPosition.UseVisualStyleBackColor = true;
			// 
			// chAutomaticUpdate
			// 
			chAutomaticUpdate.AutoSize = true;
			chAutomaticUpdate.Location = new Point(13, 55);
			chAutomaticUpdate.Name = "chAutomaticUpdate";
			chAutomaticUpdate.Size = new Size(204, 17);
			chAutomaticUpdate.TabIndex = 2;
			chAutomaticUpdate.Text = "Automatically update elements after:";
			chAutomaticUpdate.UseVisualStyleBackColor = true;
			// 
			// tabLocation
			// 
			tabLocation.BackColor = SystemColors.Control;
			tabLocation.Controls.Add(gbxLocation);
			tabLocation.Location = new Point(4, 24);
			tabLocation.Name = "tabLocation";
			tabLocation.Padding = new Padding(3);
			tabLocation.Size = new Size(685, 296);
			tabLocation.TabIndex = 1;
			tabLocation.Text = "Location";
			// 
			// gbxLocation
			// 
			gbxLocation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			gbxLocation.Controls.Add(cbxEastWest);
			gbxLocation.Controls.Add(cbxNorthSouth);
			gbxLocation.Controls.Add(lblLonDeg);
			gbxLocation.Controls.Add(lblLatDeg);
			gbxLocation.Controls.Add(lblLongitude);
			gbxLocation.Controls.Add(txtLongitude);
			gbxLocation.Controls.Add(txtName);
			gbxLocation.Controls.Add(lblLatitude);
			gbxLocation.Controls.Add(txtLatitude);
			gbxLocation.Controls.Add(lblName);
			gbxLocation.Location = new Point(8, 3);
			gbxLocation.Name = "gbxLocation";
			gbxLocation.Size = new Size(669, 285);
			gbxLocation.TabIndex = 0;
			gbxLocation.TabStop = false;
			// 
			// cbxEastWest
			// 
			cbxEastWest.DropDownStyle = ComboBoxStyle.DropDownList;
			cbxEastWest.FormattingEnabled = true;
			cbxEastWest.Items.AddRange(new object[] { "East", "West" });
			cbxEastWest.Location = new Point(181, 182);
			cbxEastWest.Name = "cbxEastWest";
			cbxEastWest.Size = new Size(100, 21);
			cbxEastWest.TabIndex = 4;
			// 
			// cbxNorthSouth
			// 
			cbxNorthSouth.DropDownStyle = ComboBoxStyle.DropDownList;
			cbxNorthSouth.FormattingEnabled = true;
			cbxNorthSouth.Items.AddRange(new object[] { "North", "South" });
			cbxNorthSouth.Location = new Point(181, 115);
			cbxNorthSouth.Name = "cbxNorthSouth";
			cbxNorthSouth.Size = new Size(100, 21);
			cbxNorthSouth.TabIndex = 2;
			// 
			// lblLonDeg
			// 
			lblLonDeg.AutoSize = true;
			lblLonDeg.Location = new Point(150, 182);
			lblLonDeg.Name = "lblLonDeg";
			lblLonDeg.Size = new Size(12, 13);
			lblLonDeg.TabIndex = 54;
			lblLonDeg.Text = "°";
			// 
			// lblLatDeg
			// 
			lblLatDeg.AutoSize = true;
			lblLatDeg.Location = new Point(150, 115);
			lblLatDeg.Name = "lblLatDeg";
			lblLatDeg.Size = new Size(12, 13);
			lblLatDeg.TabIndex = 45;
			lblLatDeg.Text = "°";
			// 
			// lblLongitude
			// 
			lblLongitude.AutoSize = true;
			lblLongitude.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblLongitude.Location = new Point(6, 151);
			lblLongitude.Name = "lblLongitude";
			lblLongitude.Size = new Size(63, 13);
			lblLongitude.TabIndex = 44;
			lblLongitude.Text = "Longitude";
			// 
			// txtLongitude
			// 
			txtLongitude.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtLongitude.Location = new Point(31, 182);
			txtLongitude.MaxLength = 100;
			txtLongitude.Name = "txtLongitude";
			txtLongitude.Size = new Size(115, 21);
			txtLongitude.TabIndex = 3;
			txtLongitude.KeyPress += txtLatitudeLongitude_KeyPress;
			// 
			// txtName
			// 
			txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtName.Location = new Point(31, 48);
			txtName.MaxLength = 128;
			txtName.Name = "txtName";
			txtName.Size = new Size(250, 21);
			txtName.TabIndex = 0;
			// 
			// lblLatitude
			// 
			lblLatitude.AutoSize = true;
			lblLatitude.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblLatitude.Location = new Point(6, 84);
			lblLatitude.Name = "lblLatitude";
			lblLatitude.Size = new Size(54, 13);
			lblLatitude.TabIndex = 41;
			lblLatitude.Text = "Latitude";
			// 
			// txtLatitude
			// 
			txtLatitude.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtLatitude.Location = new Point(31, 115);
			txtLatitude.MaxLength = 100;
			txtLatitude.Name = "txtLatitude";
			txtLatitude.Size = new Size(115, 21);
			txtLatitude.TabIndex = 1;
			txtLatitude.KeyPress += txtLatitudeLongitude_KeyPress;
			// 
			// lblName
			// 
			lblName.AutoSize = true;
			lblName.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblName.Location = new Point(6, 17);
			lblName.Name = "lblName";
			lblName.Size = new Size(39, 13);
			lblName.TabIndex = 38;
			lblName.Text = "Name";
			// 
			// tabPrograms
			// 
			tabPrograms.BackColor = SystemColors.Control;
			tabPrograms.Controls.Add(gbxPrograms);
			tabPrograms.Controls.Add(gbxAddProgram);
			tabPrograms.Location = new Point(4, 24);
			tabPrograms.Name = "tabPrograms";
			tabPrograms.Size = new Size(685, 296);
			tabPrograms.TabIndex = 2;
			tabPrograms.Text = "Programs";
			// 
			// gbxPrograms
			// 
			gbxPrograms.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			gbxPrograms.Controls.Add(dgvPrograms);
			gbxPrograms.Controls.Add(btnClear);
			gbxPrograms.Controls.Add(btnRemove);
			gbxPrograms.Controls.Add(btnEdit);
			gbxPrograms.Controls.Add(btnAdd);
			gbxPrograms.Location = new Point(8, 3);
			gbxPrograms.Name = "gbxPrograms";
			gbxPrograms.Size = new Size(669, 285);
			gbxPrograms.TabIndex = 4;
			gbxPrograms.TabStop = false;
			// 
			// dgvPrograms
			// 
			dgvPrograms.AllowUserToAddRows = false;
			dgvPrograms.AllowUserToDeleteRows = false;
			dgvPrograms.AllowUserToResizeRows = false;
			dgvPrograms.BackgroundColor = SystemColors.ControlLightLight;
			dgvPrograms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvPrograms.Columns.AddRange(new DataGridViewColumn[] { colProgram, colDirectory });
			dgvPrograms.Location = new Point(3, 10);
			dgvPrograms.MultiSelect = false;
			dgvPrograms.Name = "dgvPrograms";
			dgvPrograms.ReadOnly = true;
			dgvPrograms.RowHeadersVisible = false;
			dgvPrograms.RowTemplate.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
			dgvPrograms.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.White;
			dgvPrograms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvPrograms.Size = new Size(663, 231);
			dgvPrograms.TabIndex = 0;
			// 
			// colProgram
			// 
			colProgram.DataPropertyName = "Name";
			colProgram.HeaderText = "Program";
			colProgram.Name = "colProgram";
			colProgram.ReadOnly = true;
			colProgram.Width = 200;
			// 
			// colDirectory
			// 
			colDirectory.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			colDirectory.DataPropertyName = "Directory";
			colDirectory.HeaderText = "Directory";
			colDirectory.Name = "colDirectory";
			colDirectory.ReadOnly = true;
			// 
			// btnClear
			// 
			btnClear.Location = new Point(304, 252);
			btnClear.Name = "btnClear";
			btnClear.Size = new Size(94, 23);
			btnClear.TabIndex = 4;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = true;
			btnClear.Click += btnClear_Click;
			// 
			// btnRemove
			// 
			btnRemove.Location = new Point(206, 252);
			btnRemove.Name = "btnRemove";
			btnRemove.Size = new Size(94, 23);
			btnRemove.TabIndex = 3;
			btnRemove.Text = "Remove";
			btnRemove.UseVisualStyleBackColor = true;
			btnRemove.Click += btnRemove_Click;
			// 
			// btnEdit
			// 
			btnEdit.Location = new Point(108, 252);
			btnEdit.Name = "btnEdit";
			btnEdit.Size = new Size(94, 23);
			btnEdit.TabIndex = 2;
			btnEdit.Text = "Edit";
			btnEdit.UseVisualStyleBackColor = true;
			btnEdit.Click += btnEdit_Click;
			// 
			// btnAdd
			// 
			btnAdd.Location = new Point(10, 252);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new Size(94, 23);
			btnAdd.TabIndex = 1;
			btnAdd.Text = "Add";
			btnAdd.UseVisualStyleBackColor = true;
			btnAdd.Click += btnAdd_Click;
			// 
			// gbxAddProgram
			// 
			gbxAddProgram.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			gbxAddProgram.Controls.Add(txtDirectory);
			gbxAddProgram.Controls.Add(lblDirectory);
			gbxAddProgram.Controls.Add(btnBrowse);
			gbxAddProgram.Controls.Add(cbxExternalProgram);
			gbxAddProgram.Controls.Add(lblSelectProgram);
			gbxAddProgram.Controls.Add(btnProgramsOk);
			gbxAddProgram.Controls.Add(btnProgramsCancel);
			gbxAddProgram.Location = new Point(8, 3);
			gbxAddProgram.Name = "gbxAddProgram";
			gbxAddProgram.Size = new Size(669, 285);
			gbxAddProgram.TabIndex = 5;
			gbxAddProgram.TabStop = false;
			gbxAddProgram.Visible = false;
			// 
			// txtDirectory
			// 
			txtDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtDirectory.Location = new Point(155, 115);
			txtDirectory.Name = "txtDirectory";
			txtDirectory.Size = new Size(499, 21);
			txtDirectory.TabIndex = 2;
			// 
			// lblDirectory
			// 
			lblDirectory.AutoSize = true;
			lblDirectory.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblDirectory.Location = new Point(6, 84);
			lblDirectory.Name = "lblDirectory";
			lblDirectory.Size = new Size(60, 13);
			lblDirectory.TabIndex = 38;
			lblDirectory.Text = "Directory";
			// 
			// btnBrowse
			// 
			btnBrowse.Location = new Point(31, 114);
			btnBrowse.Name = "btnBrowse";
			btnBrowse.Size = new Size(118, 23);
			btnBrowse.TabIndex = 1;
			btnBrowse.Text = "Browse";
			btnBrowse.UseVisualStyleBackColor = true;
			btnBrowse.Click += btnBrowse_Click;
			// 
			// cbxExternalProgram
			// 
			cbxExternalProgram.DropDownStyle = ComboBoxStyle.DropDownList;
			cbxExternalProgram.FormattingEnabled = true;
			cbxExternalProgram.Location = new Point(31, 47);
			cbxExternalProgram.Name = "cbxExternalProgram";
			cbxExternalProgram.Size = new Size(280, 21);
			cbxExternalProgram.TabIndex = 0;
			cbxExternalProgram.SelectedIndexChanged += cbxProgram_SelectedIndexChanged;
			// 
			// lblSelectProgram
			// 
			lblSelectProgram.AutoSize = true;
			lblSelectProgram.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblSelectProgram.Location = new Point(6, 17);
			lblSelectProgram.Name = "lblSelectProgram";
			lblSelectProgram.Size = new Size(94, 13);
			lblSelectProgram.TabIndex = 27;
			lblSelectProgram.Text = "Select program";
			// 
			// btnProgramsOk
			// 
			btnProgramsOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnProgramsOk.Location = new Point(31, 186);
			btnProgramsOk.Name = "btnProgramsOk";
			btnProgramsOk.Size = new Size(118, 23);
			btnProgramsOk.TabIndex = 3;
			btnProgramsOk.Text = "OK";
			btnProgramsOk.UseVisualStyleBackColor = true;
			btnProgramsOk.Click += btnProgramsOk_Click;
			// 
			// btnProgramsCancel
			// 
			btnProgramsCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnProgramsCancel.Location = new Point(155, 186);
			btnProgramsCancel.Name = "btnProgramsCancel";
			btnProgramsCancel.Size = new Size(118, 23);
			btnProgramsCancel.TabIndex = 4;
			btnProgramsCancel.Text = "Cancel";
			btnProgramsCancel.UseVisualStyleBackColor = true;
			btnProgramsCancel.Click += btnProgramsCancel_Click;
			// 
			// tabNetwork
			// 
			tabNetwork.BackColor = SystemColors.Control;
			tabNetwork.Controls.Add(gbxNetwork);
			tabNetwork.Location = new Point(4, 24);
			tabNetwork.Name = "tabNetwork";
			tabNetwork.Size = new Size(685, 296);
			tabNetwork.TabIndex = 3;
			tabNetwork.Text = "Network";
			// 
			// gbxNetwork
			// 
			gbxNetwork.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			gbxNetwork.Controls.Add(pnlProxy);
			gbxNetwork.Controls.Add(rbManualProxy);
			gbxNetwork.Controls.Add(rbNoProxy);
			gbxNetwork.Location = new Point(8, 3);
			gbxNetwork.Name = "gbxNetwork";
			gbxNetwork.Size = new Size(669, 285);
			gbxNetwork.TabIndex = 0;
			gbxNetwork.TabStop = false;
			// 
			// pnlProxy
			// 
			pnlProxy.Controls.Add(lblPort);
			pnlProxy.Controls.Add(txtPort);
			pnlProxy.Controls.Add(lblProxy);
			pnlProxy.Controls.Add(txtProxy);
			pnlProxy.Controls.Add(lblUsername);
			pnlProxy.Controls.Add(txtUsername);
			pnlProxy.Controls.Add(txtPassword);
			pnlProxy.Controls.Add(lblDomain);
			pnlProxy.Controls.Add(txtDomain);
			pnlProxy.Controls.Add(lblPassword);
			pnlProxy.Enabled = false;
			pnlProxy.Location = new Point(3, 73);
			pnlProxy.Name = "pnlProxy";
			pnlProxy.Size = new Size(663, 209);
			pnlProxy.TabIndex = 2;
			// 
			// lblPort
			// 
			lblPort.AutoSize = true;
			lblPort.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblPort.Location = new Point(3, 78);
			lblPort.Name = "lblPort";
			lblPort.Size = new Size(31, 13);
			lblPort.TabIndex = 58;
			lblPort.Text = "Port";
			// 
			// txtPort
			// 
			txtPort.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtPort.Location = new Point(28, 109);
			txtPort.MaxLength = 5;
			txtPort.Name = "txtPort";
			txtPort.Size = new Size(62, 21);
			txtPort.TabIndex = 1;
			// 
			// lblProxy
			// 
			lblProxy.AutoSize = true;
			lblProxy.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblProxy.Location = new Point(3, 11);
			lblProxy.Name = "lblProxy";
			lblProxy.Size = new Size(40, 13);
			lblProxy.TabIndex = 57;
			lblProxy.Text = "Proxy";
			// 
			// txtProxy
			// 
			txtProxy.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtProxy.Location = new Point(28, 42);
			txtProxy.MaxLength = 128;
			txtProxy.Name = "txtProxy";
			txtProxy.Size = new Size(160, 21);
			txtProxy.TabIndex = 0;
			// 
			// lblUsername
			// 
			lblUsername.AutoSize = true;
			lblUsername.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblUsername.Location = new Point(248, 78);
			lblUsername.Name = "lblUsername";
			lblUsername.Size = new Size(65, 13);
			lblUsername.TabIndex = 54;
			lblUsername.Text = "Username";
			// 
			// txtUsername
			// 
			txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtUsername.Location = new Point(273, 109);
			txtUsername.MaxLength = 256;
			txtUsername.Name = "txtUsername";
			txtUsername.Size = new Size(243, 21);
			txtUsername.TabIndex = 3;
			// 
			// txtPassword
			// 
			txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtPassword.Location = new Point(273, 176);
			txtPassword.MaxLength = 256;
			txtPassword.Name = "txtPassword";
			txtPassword.PasswordChar = '•';
			txtPassword.Size = new Size(243, 21);
			txtPassword.TabIndex = 4;
			// 
			// lblDomain
			// 
			lblDomain.AutoSize = true;
			lblDomain.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblDomain.Location = new Point(248, 11);
			lblDomain.Name = "lblDomain";
			lblDomain.Size = new Size(50, 13);
			lblDomain.TabIndex = 53;
			lblDomain.Text = "Domain";
			// 
			// txtDomain
			// 
			txtDomain.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtDomain.Location = new Point(273, 42);
			txtDomain.MaxLength = 256;
			txtDomain.Name = "txtDomain";
			txtDomain.Size = new Size(243, 21);
			txtDomain.TabIndex = 2;
			// 
			// lblPassword
			// 
			lblPassword.AutoSize = true;
			lblPassword.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblPassword.Location = new Point(248, 145);
			lblPassword.Name = "lblPassword";
			lblPassword.Size = new Size(61, 13);
			lblPassword.TabIndex = 52;
			lblPassword.Text = "Password";
			// 
			// rbManualProxy
			// 
			rbManualProxy.AutoSize = true;
			rbManualProxy.Location = new Point(9, 48);
			rbManualProxy.Name = "rbManualProxy";
			rbManualProxy.Size = new Size(156, 17);
			rbManualProxy.TabIndex = 1;
			rbManualProxy.Text = "Manual proxy configuration";
			rbManualProxy.UseVisualStyleBackColor = true;
			rbManualProxy.CheckedChanged += rbCommon_CheckedChanged;
			// 
			// rbNoProxy
			// 
			rbNoProxy.AutoSize = true;
			rbNoProxy.Checked = true;
			rbNoProxy.Location = new Point(9, 17);
			rbNoProxy.Name = "rbNoProxy";
			rbNoProxy.Size = new Size(69, 17);
			rbNoProxy.TabIndex = 0;
			rbNoProxy.TabStop = true;
			rbNoProxy.Text = "No proxy";
			rbNoProxy.UseVisualStyleBackColor = true;
			rbNoProxy.CheckedChanged += rbCommon_CheckedChanged;
			// 
			// btnCancel
			// 
			btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Location = new Point(599, 345);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(100, 24);
			btnCancel.TabIndex = 2;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnOk.Location = new Point(493, 345);
			btnOk.Name = "btnOk";
			btnOk.Size = new Size(100, 24);
			btnOk.TabIndex = 1;
			btnOk.Text = "OK";
			btnOk.UseVisualStyleBackColor = true;
			btnOk.Click += btnOK_Click;
			// 
			// FormSettings
			// 
			AcceptButton = btnOk;
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new Size(709, 381);
			Controls.Add(btnOk);
			Controls.Add(btnCancel);
			Controls.Add(tabControl1);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FormSettings";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Settings";
			Load += FormSettings_Load;
			tabControl1.ResumeLayout(false);
			tabGeneral.ResumeLayout(false);
			gbxGeneral.ResumeLayout(false);
			gbxGeneral.PerformLayout();
			tabLocation.ResumeLayout(false);
			gbxLocation.ResumeLayout(false);
			gbxLocation.PerformLayout();
			tabPrograms.ResumeLayout(false);
			gbxPrograms.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvPrograms).EndInit();
			gbxAddProgram.ResumeLayout(false);
			gbxAddProgram.PerformLayout();
			tabNetwork.ResumeLayout(false);
			gbxNetwork.ResumeLayout(false);
			gbxNetwork.PerformLayout();
			pnlProxy.ResumeLayout(false);
			pnlProxy.PerformLayout();
			ResumeLayout(false);
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
	}
}