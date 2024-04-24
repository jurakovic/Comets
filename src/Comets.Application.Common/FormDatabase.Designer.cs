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
			this.btnOk = new System.Windows.Forms.Button();
			this.pnlDetails = new System.Windows.Forms.Panel();
			this.tbcDetails = new System.Windows.Forms.TabControl();
			this.tbpEphemeris = new System.Windows.Forms.TabPage();
			this.ephemerisControl = new Comets.Application.Common.Controls.Database.EphemerisControl();
			this.tbpElements = new System.Windows.Forms.TabPage();
			this.elementsControl = new Comets.Application.Common.Controls.Database.ElementsControl();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnReset = new System.Windows.Forms.Button();
			this.cbxImportResult = new System.Windows.Forms.ComboBox();
			this.lblImportResult = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.filterControl = new Comets.Application.Common.Controls.Database.FilterControl();
			this.sortMenuControl = new Comets.Application.Common.Controls.Common.SortMenuControl();
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
			this.lbxDatabase.Location = new System.Drawing.Point(10, 39);
			this.lbxDatabase.Name = "lbxDatabase";
			this.lbxDatabase.Size = new System.Drawing.Size(238, 368);
			this.lbxDatabase.TabIndex = 2;
			this.lbxDatabase.SelectedIndexChanged += new System.EventHandler(this.lbxDatabase_SelectedIndexChanged);
			this.lbxDatabase.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxDatabase_MouseDoubleClick);
			// 
			// btnFilters
			// 
			this.btnFilters.Location = new System.Drawing.Point(702, 10);
			this.btnFilters.Name = "btnFilters";
			this.btnFilters.Size = new System.Drawing.Size(100, 23);
			this.btnFilters.TabIndex = 6;
			this.btnFilters.Text = "FILTERS ▼";
			this.btnFilters.UseVisualStyleBackColor = true;
			this.btnFilters.Click += new System.EventHandler(this.btnFilters_Click);
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(598, 384);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(100, 23);
			this.btnOk.TabIndex = 7;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
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
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnReset
			// 
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnReset.Location = new System.Drawing.Point(599, 10);
			this.btnReset.Name = "btnResetAllFilters";
			this.btnReset.Size = new System.Drawing.Size(100, 23);
			this.btnReset.TabIndex = 5;
			this.btnReset.Text = "RESET";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// cbxImportResult
			// 
			this.cbxImportResult.BackColor = System.Drawing.SystemColors.Window;
			this.cbxImportResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxImportResult.FormattingEnabled = true;
			this.cbxImportResult.Location = new System.Drawing.Point(89, 11);
			this.cbxImportResult.Name = "cbxImportResult";
			this.cbxImportResult.Size = new System.Drawing.Size(159, 21);
			this.cbxImportResult.TabIndex = 1;
			this.cbxImportResult.SelectedIndexChanged += new System.EventHandler(this.cbxImportResult_SelectedIndexChanged);
			// 
			// lblImportResult
			// 
			this.lblImportResult.AutoSize = true;
			this.lblImportResult.Location = new System.Drawing.Point(8, 14);
			this.lblImportResult.Name = "lblImportResult";
			this.lblImportResult.Size = new System.Drawing.Size(73, 13);
			this.lblImportResult.TabIndex = 0;
			this.lblImportResult.Text = "Import result:";
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(361, 10);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(100, 23);
			this.btnDelete.TabIndex = 4;
			this.btnDelete.Text = "DELETE";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// filterControl
			// 
			this.filterControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.filterControl.Location = new System.Drawing.Point(253, 47);
			this.filterControl.Name = "filterControl";
			this.filterControl.Size = new System.Drawing.Size(549, 360);
			this.filterControl.TabIndex = 0;
			this.filterControl.Visible = false;
			this.filterControl.VisibleChanged += new System.EventHandler(this.filterControl_VisibleChanged);
			// 
			// sortMenuControl
			// 
			this.sortMenuControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.sortMenuControl.Location = new System.Drawing.Point(258, 10);
			this.sortMenuControl.Name = "sortMenuControl";
			this.sortMenuControl.Size = new System.Drawing.Size(100, 23);
			this.sortMenuControl.TabIndex = 3;
			this.sortMenuControl.Title = "SORT BY";
			// 
			// FormDatabase
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(813, 417);
			this.Controls.Add(this.sortMenuControl);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.cbxImportResult);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.btnFilters);
			this.Controls.Add(this.lbxDatabase);
			this.Controls.Add(this.lblImportResult);
			this.Controls.Add(this.filterControl);
			this.Controls.Add(this.pnlDetails);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
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
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Panel pnlDetails;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TabControl tbcDetails;
		private System.Windows.Forms.TabPage tbpEphemeris;
		private System.Windows.Forms.TabPage tbpElements;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.ComboBox cbxImportResult;
		private System.Windows.Forms.Label lblImportResult;
		private Common.Controls.Database.EphemerisControl ephemerisControl;
		private Common.Controls.Database.ElementsControl elementsControl;
		private Common.Controls.Database.FilterControl filterControl;
		private System.Windows.Forms.Button btnDelete;
		private Common.Controls.Common.SortMenuControl sortMenuControl;
	}
}