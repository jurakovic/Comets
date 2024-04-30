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
			lbxDatabase = new System.Windows.Forms.ListBox();
			btnFilters = new System.Windows.Forms.Button();
			btnOk = new System.Windows.Forms.Button();
			pnlDetails = new System.Windows.Forms.Panel();
			tbcDetails = new System.Windows.Forms.TabControl();
			tbpEphemeris = new System.Windows.Forms.TabPage();
			ephemerisControl = new Common.Controls.Database.EphemerisControl();
			tbpElements = new System.Windows.Forms.TabPage();
			elementsControl = new Common.Controls.Database.ElementsControl();
			btnCancel = new System.Windows.Forms.Button();
			btnReset = new System.Windows.Forms.Button();
			cbxImportResult = new System.Windows.Forms.ComboBox();
			lblImportResult = new System.Windows.Forms.Label();
			btnDelete = new System.Windows.Forms.Button();
			filterControl = new Common.Controls.Database.FilterControl();
			sortMenuControl = new Common.Controls.Common.SortMenuControl();
			pnlDetails.SuspendLayout();
			tbcDetails.SuspendLayout();
			tbpEphemeris.SuspendLayout();
			tbpElements.SuspendLayout();
			SuspendLayout();
			// 
			// lbxDatabase
			// 
			lbxDatabase.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lbxDatabase.FormattingEnabled = true;
			lbxDatabase.ItemHeight = 14;
			lbxDatabase.Location = new System.Drawing.Point(10, 39);
			lbxDatabase.Name = "lbxDatabase";
			lbxDatabase.Size = new System.Drawing.Size(238, 368);
			lbxDatabase.TabIndex = 2;
			lbxDatabase.SelectedIndexChanged += lbxDatabase_SelectedIndexChanged;
			lbxDatabase.MouseDoubleClick += lbxDatabase_MouseDoubleClick;
			// 
			// btnFilters
			// 
			btnFilters.Location = new System.Drawing.Point(702, 10);
			btnFilters.Name = "btnFilters";
			btnFilters.Size = new System.Drawing.Size(100, 23);
			btnFilters.TabIndex = 6;
			btnFilters.Text = "FILTERS ▼";
			btnFilters.UseVisualStyleBackColor = true;
			btnFilters.Click += btnFilters_Click;
			// 
			// btnOk
			// 
			btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnOk.Location = new System.Drawing.Point(598, 384);
			btnOk.Name = "btnOk";
			btnOk.Size = new System.Drawing.Size(100, 23);
			btnOk.TabIndex = 7;
			btnOk.Text = "OK";
			btnOk.UseVisualStyleBackColor = true;
			// 
			// pnlDetails
			// 
			pnlDetails.Controls.Add(tbcDetails);
			pnlDetails.Location = new System.Drawing.Point(253, 47);
			pnlDetails.Name = "pnlDetails";
			pnlDetails.Size = new System.Drawing.Size(549, 333);
			pnlDetails.TabIndex = 5;
			// 
			// tbcDetails
			// 
			tbcDetails.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			tbcDetails.Controls.Add(tbpEphemeris);
			tbcDetails.Controls.Add(tbpElements);
			tbcDetails.ItemSize = new System.Drawing.Size(128, 21);
			tbcDetails.Location = new System.Drawing.Point(5, 10);
			tbcDetails.Name = "tbcDetails";
			tbcDetails.SelectedIndex = 0;
			tbcDetails.Size = new System.Drawing.Size(539, 317);
			tbcDetails.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			tbcDetails.TabIndex = 0;
			// 
			// tbpEphemeris
			// 
			tbpEphemeris.BackColor = System.Drawing.SystemColors.Control;
			tbpEphemeris.Controls.Add(ephemerisControl);
			tbpEphemeris.Location = new System.Drawing.Point(4, 25);
			tbpEphemeris.Name = "tbpEphemeris";
			tbpEphemeris.Padding = new System.Windows.Forms.Padding(3);
			tbpEphemeris.Size = new System.Drawing.Size(531, 288);
			tbpEphemeris.TabIndex = 0;
			tbpEphemeris.Text = "Ephemeris";
			// 
			// ephemerisControl
			// 
			ephemerisControl.Location = new System.Drawing.Point(0, 0);
			ephemerisControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			ephemerisControl.Name = "ephemerisControl";
			ephemerisControl.Size = new System.Drawing.Size(531, 288);
			ephemerisControl.TabIndex = 0;
			// 
			// tbpElements
			// 
			tbpElements.BackColor = System.Drawing.SystemColors.Control;
			tbpElements.Controls.Add(elementsControl);
			tbpElements.Location = new System.Drawing.Point(4, 25);
			tbpElements.Name = "tbpElements";
			tbpElements.Padding = new System.Windows.Forms.Padding(3);
			tbpElements.Size = new System.Drawing.Size(531, 288);
			tbpElements.TabIndex = 1;
			tbpElements.Text = "Orbital Elements";
			// 
			// elementsControl
			// 
			elementsControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			elementsControl.Location = new System.Drawing.Point(0, 0);
			elementsControl.Name = "elementsControl";
			elementsControl.Size = new System.Drawing.Size(531, 288);
			elementsControl.TabIndex = 0;
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.Location = new System.Drawing.Point(702, 384);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(100, 23);
			btnCancel.TabIndex = 8;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnReset
			// 
			btnReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnReset.Location = new System.Drawing.Point(599, 10);
			btnReset.Name = "btnReset";
			btnReset.Size = new System.Drawing.Size(100, 23);
			btnReset.TabIndex = 5;
			btnReset.Text = "RESET";
			btnReset.UseVisualStyleBackColor = true;
			btnReset.Click += btnReset_Click;
			// 
			// cbxImportResult
			// 
			cbxImportResult.BackColor = System.Drawing.SystemColors.Window;
			cbxImportResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cbxImportResult.FormattingEnabled = true;
			cbxImportResult.Location = new System.Drawing.Point(89, 11);
			cbxImportResult.Name = "cbxImportResult";
			cbxImportResult.Size = new System.Drawing.Size(159, 21);
			cbxImportResult.TabIndex = 1;
			cbxImportResult.SelectedIndexChanged += cbxImportResult_SelectedIndexChanged;
			// 
			// lblImportResult
			// 
			lblImportResult.AutoSize = true;
			lblImportResult.Location = new System.Drawing.Point(8, 14);
			lblImportResult.Name = "lblImportResult";
			lblImportResult.Size = new System.Drawing.Size(73, 13);
			lblImportResult.TabIndex = 0;
			lblImportResult.Text = "Import result:";
			// 
			// btnDelete
			// 
			btnDelete.Location = new System.Drawing.Point(361, 10);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(100, 23);
			btnDelete.TabIndex = 4;
			btnDelete.Text = "DELETE";
			btnDelete.UseVisualStyleBackColor = true;
			btnDelete.Click += btnDelete_Click;
			// 
			// filterControl
			// 
			filterControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			filterControl.Location = new System.Drawing.Point(253, 47);
			filterControl.Name = "filterControl";
			filterControl.Size = new System.Drawing.Size(549, 360);
			filterControl.TabIndex = 0;
			filterControl.Visible = false;
			filterControl.VisibleChanged += filterControl_VisibleChanged;
			// 
			// sortMenuControl
			// 
			sortMenuControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			sortMenuControl.Location = new System.Drawing.Point(258, 10);
			sortMenuControl.Name = "sortMenuControl";
			sortMenuControl.Size = new System.Drawing.Size(100, 23);
			sortMenuControl.TabIndex = 3;
			sortMenuControl.Title = "SORT BY";
			// 
			// FormDatabase
			// 
			AcceptButton = btnOk;
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(813, 417);
			Controls.Add(sortMenuControl);
			Controls.Add(btnDelete);
			Controls.Add(cbxImportResult);
			Controls.Add(btnReset);
			Controls.Add(btnFilters);
			Controls.Add(lbxDatabase);
			Controls.Add(lblImportResult);
			Controls.Add(filterControl);
			Controls.Add(pnlDetails);
			Controls.Add(btnOk);
			Controls.Add(btnCancel);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			KeyPreview = true;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FormDatabase";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Database";
			FormClosing += FormDatabase_FormClosing;
			Load += FormDatabase_Load;
			KeyDown += FormDatabase_KeyDown;
			pnlDetails.ResumeLayout(false);
			tbcDetails.ResumeLayout(false);
			tbpEphemeris.ResumeLayout(false);
			tbpElements.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
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
