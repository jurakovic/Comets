namespace Comets.Application
{
	partial class FormDatabase
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
			this.lbxDatabase = new System.Windows.Forms.ListBox();
			this.btnFilters = new System.Windows.Forms.Button();
			this.btnSort = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.contextSort = new System.Windows.Forms.ContextMenu();
			this.mnuDesig = new System.Windows.Forms.MenuItem();
			this.mnuDiscoverer = new System.Windows.Forms.MenuItem();
			this.mnuPerihDate = new System.Windows.Forms.MenuItem();
			this.mnuPerihDist = new System.Windows.Forms.MenuItem();
			this.mnuPerihEarthDist = new System.Windows.Forms.MenuItem();
			this.mnuPerihMag = new System.Windows.Forms.MenuItem();
			this.mnuCurrSunDist = new System.Windows.Forms.MenuItem();
			this.mnuCurrEarthDist = new System.Windows.Forms.MenuItem();
			this.mnuCurrMag = new System.Windows.Forms.MenuItem();
			this.mnuPeriod = new System.Windows.Forms.MenuItem();
			this.mnuAphDistance = new System.Windows.Forms.MenuItem();
			this.mnuSemiMajorAxis = new System.Windows.Forms.MenuItem();
			this.mnuEcc = new System.Windows.Forms.MenuItem();
			this.mnuIncl = new System.Windows.Forms.MenuItem();
			this.mnuAscNode = new System.Windows.Forms.MenuItem();
			this.mnuArgPeri = new System.Windows.Forms.MenuItem();
			this.mnuSeparator = new System.Windows.Forms.MenuItem();
			this.mnuAsc = new System.Windows.Forms.MenuItem();
			this.mnuDesc = new System.Windows.Forms.MenuItem();
			this.pnlDetails = new System.Windows.Forms.Panel();
			this.tbcDetails = new System.Windows.Forms.TabControl();
			this.tbpEphemeris = new System.Windows.Forms.TabPage();
			this.ephemerisControl = new Comets.Application.Controls.Database.EphemerisControl();
			this.tbpElements = new System.Windows.Forms.TabPage();
			this.elementsControl = new Comets.Application.Controls.Database.ElementsControl();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnResetAllFilters = new System.Windows.Forms.Button();
			this.lblTotal = new System.Windows.Forms.Label();
			this.cbxImportResult = new System.Windows.Forms.ComboBox();
			this.lblImportResult = new System.Windows.Forms.Label();
			this.filterControl = new Comets.Application.Controls.Database.FilterControl();
			this.pnlDetails.SuspendLayout();
			this.tbcDetails.SuspendLayout();
			this.tbpEphemeris.SuspendLayout();
			this.tbpElements.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbxDatabase
			// 
			this.lbxDatabase.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lbxDatabase.FormattingEnabled = true;
			this.lbxDatabase.ItemHeight = 14;
			this.lbxDatabase.Location = new System.Drawing.Point(10, 53);
			this.lbxDatabase.Name = "lbxDatabase";
			this.lbxDatabase.Size = new System.Drawing.Size(238, 354);
			this.lbxDatabase.TabIndex = 2;
			this.lbxDatabase.SelectedIndexChanged += new System.EventHandler(this.lbxDatabase_SelectedIndexChanged);
			this.lbxDatabase.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxDatabase_MouseDoubleClick);
			// 
			// btnFilters
			// 
			this.btnFilters.Location = new System.Drawing.Point(702, 18);
			this.btnFilters.Name = "btnFilters";
			this.btnFilters.Size = new System.Drawing.Size(100, 23);
			this.btnFilters.TabIndex = 6;
			this.btnFilters.Text = "Filters ▼";
			this.btnFilters.UseVisualStyleBackColor = true;
			this.btnFilters.Click += new System.EventHandler(this.btnFilters_Click);
			// 
			// btnSort
			// 
			this.btnSort.Location = new System.Drawing.Point(273, 18);
			this.btnSort.Name = "btnSort";
			this.btnSort.Size = new System.Drawing.Size(100, 23);
			this.btnSort.TabIndex = 3;
			this.btnSort.Text = "Sort by";
			this.btnSort.UseVisualStyleBackColor = true;
			this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(598, 384);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(100, 23);
			this.btnOk.TabIndex = 6;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// contextSort
			// 
			this.contextSort.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuDesig,
            this.mnuDiscoverer,
            this.mnuPerihDate,
            this.mnuPerihDist,
            this.mnuPerihEarthDist,
            this.mnuPerihMag,
            this.mnuCurrSunDist,
            this.mnuCurrEarthDist,
            this.mnuCurrMag,
            this.mnuPeriod,
            this.mnuAphDistance,
            this.mnuSemiMajorAxis,
            this.mnuEcc,
            this.mnuIncl,
            this.mnuAscNode,
            this.mnuArgPeri,
            this.mnuSeparator,
            this.mnuAsc,
            this.mnuDesc});
			// 
			// mnuDesig
			// 
			this.mnuDesig.Index = 0;
			this.mnuDesig.RadioCheck = true;
			this.mnuDesig.Tag = "";
			this.mnuDesig.Text = "Designation";
			this.mnuDesig.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuDiscoverer
			// 
			this.mnuDiscoverer.Index = 1;
			this.mnuDiscoverer.RadioCheck = true;
			this.mnuDiscoverer.Tag = "";
			this.mnuDiscoverer.Text = "Discoverer";
			this.mnuDiscoverer.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPerihDate
			// 
			this.mnuPerihDate.Index = 2;
			this.mnuPerihDate.RadioCheck = true;
			this.mnuPerihDate.Tag = "";
			this.mnuPerihDate.Text = "Perihelion date";
			this.mnuPerihDate.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPerihDist
			// 
			this.mnuPerihDist.Index = 3;
			this.mnuPerihDist.Tag = "";
			this.mnuPerihDist.Text = "Perihelion distance";
			this.mnuPerihDist.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPerihEarthDist
			// 
			this.mnuPerihEarthDist.Index = 4;
			this.mnuPerihEarthDist.Tag = "";
			this.mnuPerihEarthDist.Text = "Perihelion distance from Earth";
			this.mnuPerihEarthDist.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPerihMag
			// 
			this.mnuPerihMag.Index = 5;
			this.mnuPerihMag.Tag = "";
			this.mnuPerihMag.Text = "Perihelion magnitude";
			this.mnuPerihMag.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuCurrSunDist
			// 
			this.mnuCurrSunDist.Index = 6;
			this.mnuCurrSunDist.Tag = "";
			this.mnuCurrSunDist.Text = "Current distance from Sun";
			this.mnuCurrSunDist.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuCurrEarthDist
			// 
			this.mnuCurrEarthDist.Index = 7;
			this.mnuCurrEarthDist.Tag = "";
			this.mnuCurrEarthDist.Text = "Current distance from Earth";
			this.mnuCurrEarthDist.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuCurrMag
			// 
			this.mnuCurrMag.Index = 8;
			this.mnuCurrMag.Tag = "";
			this.mnuCurrMag.Text = "Current magnitude";
			this.mnuCurrMag.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuPeriod
			// 
			this.mnuPeriod.Index = 9;
			this.mnuPeriod.RadioCheck = true;
			this.mnuPeriod.Tag = "";
			this.mnuPeriod.Text = "Period";
			this.mnuPeriod.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuAphDistance
			// 
			this.mnuAphDistance.Index = 10;
			this.mnuAphDistance.Tag = "";
			this.mnuAphDistance.Text = "Aphelion distance";
			this.mnuAphDistance.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuSemiMajorAxis
			// 
			this.mnuSemiMajorAxis.Index = 11;
			this.mnuSemiMajorAxis.Tag = "";
			this.mnuSemiMajorAxis.Text = "Semi-major axis";
			this.mnuSemiMajorAxis.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuEcc
			// 
			this.mnuEcc.Index = 12;
			this.mnuEcc.RadioCheck = true;
			this.mnuEcc.Tag = "";
			this.mnuEcc.Text = "Eccentricity";
			this.mnuEcc.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuIncl
			// 
			this.mnuIncl.Index = 13;
			this.mnuIncl.RadioCheck = true;
			this.mnuIncl.Tag = "";
			this.mnuIncl.Text = "Inclination";
			this.mnuIncl.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuAscNode
			// 
			this.mnuAscNode.Index = 14;
			this.mnuAscNode.RadioCheck = true;
			this.mnuAscNode.Tag = "";
			this.mnuAscNode.Text = "Long. of the Asc. Node";
			this.mnuAscNode.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuArgPeri
			// 
			this.mnuArgPeri.Index = 15;
			this.mnuArgPeri.RadioCheck = true;
			this.mnuArgPeri.Tag = "";
			this.mnuArgPeri.Text = "Arg. of Pericenter";
			this.mnuArgPeri.Click += new System.EventHandler(this.menuItemSortCommon_Click);
			// 
			// mnuSeparator
			// 
			this.mnuSeparator.Index = 16;
			this.mnuSeparator.Text = "-";
			// 
			// mnuAsc
			// 
			this.mnuAsc.Index = 17;
			this.mnuAsc.RadioCheck = true;
			this.mnuAsc.Text = "Ascending";
			this.mnuAsc.Click += new System.EventHandler(this.menuItemSortAscDesc_Click);
			// 
			// mnuDesc
			// 
			this.mnuDesc.Index = 18;
			this.mnuDesc.RadioCheck = true;
			this.mnuDesc.Text = "Descending";
			this.mnuDesc.Click += new System.EventHandler(this.menuItemSortAscDesc_Click);
			// 
			// pnlDetails
			// 
			this.pnlDetails.Controls.Add(this.tbcDetails);
			this.pnlDetails.Location = new System.Drawing.Point(253, 47);
			this.pnlDetails.Name = "pnlDetails";
			this.pnlDetails.Size = new System.Drawing.Size(549, 333);
			this.pnlDetails.TabIndex = 5;
			// 
			// tbcDetails
			// 
			this.tbcDetails.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tbcDetails.Controls.Add(this.tbpEphemeris);
			this.tbcDetails.Controls.Add(this.tbpElements);
			this.tbcDetails.ItemSize = new System.Drawing.Size(128, 21);
			this.tbcDetails.Location = new System.Drawing.Point(5, 10);
			this.tbcDetails.Name = "tbcDetails";
			this.tbcDetails.SelectedIndex = 0;
			this.tbcDetails.Size = new System.Drawing.Size(539, 317);
			this.tbcDetails.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tbcDetails.TabIndex = 0;
			// 
			// tbpEphemeris
			// 
			this.tbpEphemeris.BackColor = System.Drawing.SystemColors.Control;
			this.tbpEphemeris.Controls.Add(this.ephemerisControl);
			this.tbpEphemeris.Location = new System.Drawing.Point(4, 25);
			this.tbpEphemeris.Name = "tbpEphemeris";
			this.tbpEphemeris.Padding = new System.Windows.Forms.Padding(3);
			this.tbpEphemeris.Size = new System.Drawing.Size(531, 288);
			this.tbpEphemeris.TabIndex = 0;
			this.tbpEphemeris.Text = "Ephemeris";
			// 
			// ephemerisControl
			// 
			this.ephemerisControl.Location = new System.Drawing.Point(0, 0);
			this.ephemerisControl.Name = "ephemerisControl";
			this.ephemerisControl.Size = new System.Drawing.Size(531, 288);
			this.ephemerisControl.TabIndex = 0;
			// 
			// tbpElements
			// 
			this.tbpElements.BackColor = System.Drawing.SystemColors.Control;
			this.tbpElements.Controls.Add(this.elementsControl);
			this.tbpElements.Location = new System.Drawing.Point(4, 25);
			this.tbpElements.Name = "tbpElements";
			this.tbpElements.Padding = new System.Windows.Forms.Padding(3);
			this.tbpElements.Size = new System.Drawing.Size(531, 288);
			this.tbpElements.TabIndex = 1;
			this.tbpElements.Text = "Orbital Elements";
			// 
			// elementsControl
			// 
			this.elementsControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.elementsControl.Location = new System.Drawing.Point(0, 0);
			this.elementsControl.Name = "elementsControl";
			this.elementsControl.Size = new System.Drawing.Size(531, 288);
			this.elementsControl.TabIndex = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(702, 384);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnResetAllFilters
			// 
			this.btnResetAllFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnResetAllFilters.Location = new System.Drawing.Point(599, 18);
			this.btnResetAllFilters.Name = "btnResetAllFilters";
			this.btnResetAllFilters.Size = new System.Drawing.Size(100, 23);
			this.btnResetAllFilters.TabIndex = 5;
			this.btnResetAllFilters.Text = "Reset all";
			this.btnResetAllFilters.UseVisualStyleBackColor = true;
			this.btnResetAllFilters.Click += new System.EventHandler(this.btnResetAllFilters_Click);
			// 
			// lblTotal
			// 
			this.lblTotal.AutoSize = true;
			this.lblTotal.Location = new System.Drawing.Point(426, 22);
			this.lblTotal.Name = "lblTotal";
			this.lblTotal.Size = new System.Drawing.Size(50, 13);
			this.lblTotal.TabIndex = 4;
			this.lblTotal.Text = "Comets: ";
			// 
			// cbxImportResult
			// 
			this.cbxImportResult.BackColor = System.Drawing.SystemColors.Window;
			this.cbxImportResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxImportResult.FormattingEnabled = true;
			this.cbxImportResult.Location = new System.Drawing.Point(89, 19);
			this.cbxImportResult.Name = "cbxImportResult";
			this.cbxImportResult.Size = new System.Drawing.Size(159, 21);
			this.cbxImportResult.TabIndex = 1;
			this.cbxImportResult.SelectedIndexChanged += new System.EventHandler(this.cbxImportResult_SelectedIndexChanged);
			// 
			// lblImportResult
			// 
			this.lblImportResult.AutoSize = true;
			this.lblImportResult.Location = new System.Drawing.Point(8, 22);
			this.lblImportResult.Name = "lblImportResult";
			this.lblImportResult.Size = new System.Drawing.Size(73, 13);
			this.lblImportResult.TabIndex = 0;
			this.lblImportResult.Text = "Import result:";
			// 
			// filterControl
			// 
			this.filterControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.filterControl.Location = new System.Drawing.Point(253, 47);
			this.filterControl.Name = "filterControl";
			this.filterControl.Size = new System.Drawing.Size(549, 360);
			this.filterControl.TabIndex = 8;
			this.filterControl.Visible = false;
			this.filterControl.VisibleChanged += new System.EventHandler(this.filterControl_VisibleChanged);
			// 
			// FormDatabase
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(813, 417);
			this.Controls.Add(this.filterControl);
			this.Controls.Add(this.cbxImportResult);
			this.Controls.Add(this.btnResetAllFilters);
			this.Controls.Add(this.lblTotal);
			this.Controls.Add(this.btnFilters);
			this.Controls.Add(this.btnSort);
			this.Controls.Add(this.lbxDatabase);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblImportResult);
			this.Controls.Add(this.pnlDetails);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDatabase";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Database";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDatabase_FormClosing);
			this.Load += new System.EventHandler(this.FormDatabase_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormDatabase_KeyDown);
			this.pnlDetails.ResumeLayout(false);
			this.tbcDetails.ResumeLayout(false);
			this.tbpEphemeris.ResumeLayout(false);
			this.tbpElements.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ListBox lbxDatabase;
		private System.Windows.Forms.Button btnFilters;
		private System.Windows.Forms.Button btnSort;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.ContextMenu contextSort;
		private System.Windows.Forms.MenuItem mnuDesig;
		private System.Windows.Forms.MenuItem mnuDiscoverer;
		private System.Windows.Forms.MenuItem mnuPerihDate;
		private System.Windows.Forms.MenuItem mnuEcc;
		private System.Windows.Forms.MenuItem mnuAscNode;
		private System.Windows.Forms.MenuItem mnuArgPeri;
		private System.Windows.Forms.MenuItem mnuPeriod;
		private System.Windows.Forms.MenuItem mnuSeparator;
		private System.Windows.Forms.MenuItem mnuAsc;
		private System.Windows.Forms.MenuItem mnuDesc;
		private System.Windows.Forms.Panel pnlDetails;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblTotal;
		private System.Windows.Forms.MenuItem mnuPerihEarthDist;
		private System.Windows.Forms.MenuItem mnuIncl;
		private System.Windows.Forms.MenuItem mnuCurrSunDist;
		private System.Windows.Forms.MenuItem mnuCurrEarthDist;
		private System.Windows.Forms.MenuItem mnuPerihMag;
		private System.Windows.Forms.MenuItem mnuCurrMag;
		private System.Windows.Forms.MenuItem mnuPerihDist;
		private System.Windows.Forms.TabControl tbcDetails;
		private System.Windows.Forms.TabPage tbpEphemeris;
		private System.Windows.Forms.TabPage tbpElements;
		private System.Windows.Forms.Button btnResetAllFilters;
		private System.Windows.Forms.MenuItem mnuAphDistance;
		private System.Windows.Forms.MenuItem mnuSemiMajorAxis;
		private System.Windows.Forms.ComboBox cbxImportResult;
		private System.Windows.Forms.Label lblImportResult;
		private Controls.Database.EphemerisControl ephemerisControl;
		private Controls.Database.ElementsControl elementsControl;
		private Controls.Database.FilterControl filterControl;
	}
}