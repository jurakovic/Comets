namespace Comets.Application.Common.Controls.Common
{
	partial class SelectCometControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			gbxSelectComet = new GroupBox();
			btnSelect = new Button();
			sortMenuControl = new SortMenuControl();
			btnAll = new Button();
			btnFilter = new Button();
			cbComet = new ComboBox();
			ctxSelect = new ContextMenuStrip(components);
			mnuBrightest = new ToolStripMenuItem();
			mnuClosestToPerihelion = new ToolStripMenuItem();
			mnuClosestToEarth = new ToolStripMenuItem();
			mnuClosestToSun = new ToolStripMenuItem();
			gbxSelectComet.SuspendLayout();
			ctxSelect.SuspendLayout();
			SuspendLayout();
			// 
			// gbxSelectComet
			// 
			gbxSelectComet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			gbxSelectComet.Controls.Add(btnSelect);
			gbxSelectComet.Controls.Add(sortMenuControl);
			gbxSelectComet.Controls.Add(btnAll);
			gbxSelectComet.Controls.Add(btnFilter);
			gbxSelectComet.Controls.Add(cbComet);
			gbxSelectComet.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			gbxSelectComet.Location = new Point(0, 0);
			gbxSelectComet.Name = "gbxSelectComet";
			gbxSelectComet.Size = new Size(290, 85);
			gbxSelectComet.TabIndex = 0;
			gbxSelectComet.TabStop = false;
			gbxSelectComet.Text = "Select comet";
			// 
			// btnSelect
			// 
			btnSelect.Location = new Point(72, 49);
			btnSelect.Name = "btnSelect";
			btnSelect.Size = new Size(24, 24);
			btnSelect.TabIndex = 2;
			btnSelect.Text = "▼";
			btnSelect.UseVisualStyleBackColor = true;
			btnSelect.Click += btnSelect_Click;
			// 
			// sortMenuControl
			// 
			sortMenuControl.Font = new Font("Tahoma", 8.25F);
			sortMenuControl.Location = new Point(193, 49);
			sortMenuControl.Name = "sortMenuControl";
			sortMenuControl.Size = new Size(85, 24);
			sortMenuControl.TabIndex = 4;
			sortMenuControl.Title = "SORT BY";
			// 
			// btnAll
			// 
			btnAll.Location = new Point(11, 49);
			btnAll.Name = "btnAll";
			btnAll.Size = new Size(62, 24);
			btnAll.TabIndex = 1;
			btnAll.Text = "ALL";
			btnAll.UseVisualStyleBackColor = true;
			btnAll.Click += btnAll_Click;
			// 
			// btnFilter
			// 
			btnFilter.Location = new Point(102, 49);
			btnFilter.Name = "btnFilter";
			btnFilter.Size = new Size(85, 24);
			btnFilter.TabIndex = 3;
			btnFilter.Text = "FILTER";
			btnFilter.UseVisualStyleBackColor = true;
			btnFilter.Click += btnFilter_Click;
			// 
			// cbComet
			// 
			cbComet.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			cbComet.DropDownStyle = ComboBoxStyle.DropDownList;
			cbComet.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			cbComet.FormattingEnabled = true;
			cbComet.IntegralHeight = false;
			cbComet.Location = new Point(12, 21);
			cbComet.MaxDropDownItems = 21;
			cbComet.Name = "cbComet";
			cbComet.Size = new Size(265, 22);
			cbComet.TabIndex = 0;
			cbComet.SelectedIndexChanged += cbComet_SelectedIndexChanged;
			// 
			// ctxSelect
			// 
			ctxSelect.Items.AddRange(new ToolStripItem[] { mnuBrightest, mnuClosestToPerihelion, mnuClosestToEarth, mnuClosestToSun });
			ctxSelect.Name = "ctxSelect";
			ctxSelect.Size = new Size(183, 92);
			// 
			// mnuBrightest
			// 
			mnuBrightest.MergeIndex = 0;
			mnuBrightest.Name = "mnuBrightest";
			mnuBrightest.Size = new Size(182, 22);
			mnuBrightest.Text = "Brightest";
			mnuBrightest.Click += mnuCommon_Click;
			// 
			// mnuClosestToPerihelion
			// 
			mnuClosestToPerihelion.MergeIndex = 1;
			mnuClosestToPerihelion.Name = "mnuClosestToPerihelion";
			mnuClosestToPerihelion.Size = new Size(182, 22);
			mnuClosestToPerihelion.Text = "Closest to perihelion";
			mnuClosestToPerihelion.Click += mnuCommon_Click;
			// 
			// mnuClosestToEarth
			// 
			mnuClosestToEarth.MergeIndex = 2;
			mnuClosestToEarth.Name = "mnuClosestToEarth";
			mnuClosestToEarth.Size = new Size(182, 22);
			mnuClosestToEarth.Text = "Closest to Earth";
			mnuClosestToEarth.Click += mnuCommon_Click;
			// 
			// mnuClosestToSun
			// 
			mnuClosestToSun.MergeIndex = 3;
			mnuClosestToSun.Name = "mnuClosestToSun";
			mnuClosestToSun.Size = new Size(182, 22);
			mnuClosestToSun.Text = "Closest to Sun";
			mnuClosestToSun.Click += mnuCommon_Click;
			// 
			// SelectCometControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(gbxSelectComet);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Name = "SelectCometControl";
			Size = new Size(290, 85);
			gbxSelectComet.ResumeLayout(false);
			ctxSelect.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.GroupBox gbxSelectComet;
		private System.Windows.Forms.Button btnFilter;
		private System.Windows.Forms.ComboBox cbComet;
		private System.Windows.Forms.Button btnAll;
		private SortMenuControl sortMenuControl;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.ContextMenuStrip ctxSelect;
		private System.Windows.Forms.ToolStripMenuItem mnuBrightest;
		private System.Windows.Forms.ToolStripMenuItem mnuClosestToEarth;
		private System.Windows.Forms.ToolStripMenuItem mnuClosestToSun;
		private System.Windows.Forms.ToolStripMenuItem mnuClosestToPerihelion;
	}
}
