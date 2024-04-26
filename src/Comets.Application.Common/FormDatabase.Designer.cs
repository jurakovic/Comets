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
			lbxDatabase = new ListBox();
			btnFilters = new Button();
			btnOk = new Button();
			pnlDetails = new Panel();
			tbcDetails = new TabControl();
			tbpEphemeris = new TabPage();
			ephemerisControl = new Common.Controls.Database.EphemerisControl();
			tbpElements = new TabPage();
			elementsControl = new Common.Controls.Database.ElementsControl();
			btnCancel = new Button();
			btnReset = new Button();
			cbxImportResult = new ComboBox();
			lblImportResult = new Label();
			btnDelete = new Button();
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
			lbxDatabase.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lbxDatabase.FormattingEnabled = true;
			lbxDatabase.ItemHeight = 14;
			lbxDatabase.Location = new Point(10, 39);
			lbxDatabase.Name = "lbxDatabase";
			lbxDatabase.Size = new Size(238, 368);
			lbxDatabase.TabIndex = 2;
			lbxDatabase.SelectedIndexChanged += lbxDatabase_SelectedIndexChanged;
			lbxDatabase.MouseDoubleClick += lbxDatabase_MouseDoubleClick;
			// 
			// btnFilters
			// 
			btnFilters.Location = new Point(702, 10);
			btnFilters.Name = "btnFilters";
			btnFilters.Size = new Size(100, 23);
			btnFilters.TabIndex = 6;
			btnFilters.Text = "FILTERS ▼";
			btnFilters.UseVisualStyleBackColor = true;
			btnFilters.Click += btnFilters_Click;
			// 
			// btnOk
			// 
			btnOk.DialogResult = DialogResult.OK;
			btnOk.Location = new Point(598, 384);
			btnOk.Name = "btnOk";
			btnOk.Size = new Size(100, 23);
			btnOk.TabIndex = 7;
			btnOk.Text = "OK";
			btnOk.UseVisualStyleBackColor = true;
			// 
			// pnlDetails
			// 
			pnlDetails.Controls.Add(tbcDetails);
			pnlDetails.Location = new Point(253, 47);
			pnlDetails.Name = "pnlDetails";
			pnlDetails.Size = new Size(549, 333);
			pnlDetails.TabIndex = 5;
			// 
			// tbcDetails
			// 
			tbcDetails.Appearance = TabAppearance.FlatButtons;
			tbcDetails.Controls.Add(tbpEphemeris);
			tbcDetails.Controls.Add(tbpElements);
			tbcDetails.ItemSize = new Size(128, 21);
			tbcDetails.Location = new Point(5, 10);
			tbcDetails.Name = "tbcDetails";
			tbcDetails.SelectedIndex = 0;
			tbcDetails.Size = new Size(539, 317);
			tbcDetails.SizeMode = TabSizeMode.Fixed;
			tbcDetails.TabIndex = 0;
			// 
			// tbpEphemeris
			// 
			tbpEphemeris.BackColor = SystemColors.Control;
			tbpEphemeris.Controls.Add(ephemerisControl);
			tbpEphemeris.Location = new Point(4, 25);
			tbpEphemeris.Name = "tbpEphemeris";
			tbpEphemeris.Padding = new Padding(3);
			tbpEphemeris.Size = new Size(531, 288);
			tbpEphemeris.TabIndex = 0;
			tbpEphemeris.Text = "Ephemeris";
			// 
			// ephemerisControl
			// 
			ephemerisControl.Location = new Point(0, 0);
			ephemerisControl.Margin = new Padding(4, 3, 4, 3);
			ephemerisControl.Name = "ephemerisControl";
			ephemerisControl.Size = new Size(531, 288);
			ephemerisControl.TabIndex = 0;
			// 
			// tbpElements
			// 
			tbpElements.BackColor = SystemColors.Control;
			tbpElements.Controls.Add(elementsControl);
			tbpElements.Location = new Point(4, 25);
			tbpElements.Name = "tbpElements";
			tbpElements.Padding = new Padding(3);
			tbpElements.Size = new Size(531, 288);
			tbpElements.TabIndex = 1;
			tbpElements.Text = "Orbital Elements";
			// 
			// elementsControl
			// 
			elementsControl.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			elementsControl.Location = new Point(0, 0);
			elementsControl.Name = "elementsControl";
			elementsControl.Size = new Size(531, 288);
			elementsControl.TabIndex = 0;
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Location = new Point(702, 384);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(100, 23);
			btnCancel.TabIndex = 8;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnReset
			// 
			btnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnReset.Location = new Point(599, 10);
			btnReset.Name = "btnReset";
			btnReset.Size = new Size(100, 23);
			btnReset.TabIndex = 5;
			btnReset.Text = "RESET";
			btnReset.UseVisualStyleBackColor = true;
			btnReset.Click += btnReset_Click;
			// 
			// cbxImportResult
			// 
			cbxImportResult.BackColor = SystemColors.Window;
			cbxImportResult.DropDownStyle = ComboBoxStyle.DropDownList;
			cbxImportResult.FormattingEnabled = true;
			cbxImportResult.Location = new Point(89, 11);
			cbxImportResult.Name = "cbxImportResult";
			cbxImportResult.Size = new Size(159, 21);
			cbxImportResult.TabIndex = 1;
			cbxImportResult.SelectedIndexChanged += cbxImportResult_SelectedIndexChanged;
			// 
			// lblImportResult
			// 
			lblImportResult.AutoSize = true;
			lblImportResult.Location = new Point(8, 14);
			lblImportResult.Name = "lblImportResult";
			lblImportResult.Size = new Size(73, 13);
			lblImportResult.TabIndex = 0;
			lblImportResult.Text = "Import result:";
			// 
			// btnDelete
			// 
			btnDelete.Location = new Point(361, 10);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(100, 23);
			btnDelete.TabIndex = 4;
			btnDelete.Text = "DELETE";
			btnDelete.UseVisualStyleBackColor = true;
			btnDelete.Click += btnDelete_Click;
			// 
			// filterControl
			// 
			filterControl.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			filterControl.Location = new Point(253, 47);
			filterControl.Name = "filterControl";
			filterControl.Size = new Size(549, 360);
			filterControl.TabIndex = 0;
			filterControl.Visible = false;
			filterControl.VisibleChanged += filterControl_VisibleChanged;
			// 
			// sortMenuControl
			// 
			sortMenuControl.Font = new Font("Tahoma", 8.25F);
			sortMenuControl.Location = new Point(258, 10);
			sortMenuControl.Name = "sortMenuControl";
			sortMenuControl.Size = new Size(100, 23);
			sortMenuControl.TabIndex = 3;
			sortMenuControl.Title = "SORT BY";
			// 
			// FormDatabase
			// 
			AcceptButton = btnOk;
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(813, 417);
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
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			KeyPreview = true;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FormDatabase";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
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