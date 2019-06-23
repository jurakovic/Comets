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
			this.gbxSelectComet = new System.Windows.Forms.GroupBox();
			this.btnSelect = new System.Windows.Forms.Button();
			this.sortMenuControl = new Comets.Application.Common.Controls.Common.SortMenuControl();
			this.btnAll = new System.Windows.Forms.Button();
			this.btnFilter = new System.Windows.Forms.Button();
			this.cbComet = new System.Windows.Forms.ComboBox();
			this.ctxSelect = new System.Windows.Forms.ContextMenu();
			this.mnuBrightest = new System.Windows.Forms.MenuItem();
			this.mnuClosestToPerihelion = new System.Windows.Forms.MenuItem();
			this.mnuClosestToEarth = new System.Windows.Forms.MenuItem();
			this.mnuClosestToSun = new System.Windows.Forms.MenuItem();
			this.gbxSelectComet.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxSelectComet
			// 
			this.gbxSelectComet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxSelectComet.Controls.Add(this.btnSelect);
			this.gbxSelectComet.Controls.Add(this.sortMenuControl);
			this.gbxSelectComet.Controls.Add(this.btnAll);
			this.gbxSelectComet.Controls.Add(this.btnFilter);
			this.gbxSelectComet.Controls.Add(this.cbComet);
			this.gbxSelectComet.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.gbxSelectComet.Location = new System.Drawing.Point(0, 0);
			this.gbxSelectComet.Name = "gbxSelectComet";
			this.gbxSelectComet.Size = new System.Drawing.Size(290, 85);
			this.gbxSelectComet.TabIndex = 0;
			this.gbxSelectComet.TabStop = false;
			this.gbxSelectComet.Text = "Select comet";
			// 
			// btnSelect
			// 
			this.btnSelect.Location = new System.Drawing.Point(72, 49);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(24, 24);
			this.btnSelect.TabIndex = 2;
			this.btnSelect.Text = "▼";
			this.btnSelect.UseVisualStyleBackColor = true;
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// sortMenuControl
			// 
			this.sortMenuControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.sortMenuControl.Location = new System.Drawing.Point(193, 49);
			this.sortMenuControl.Name = "sortMenuControl";
			this.sortMenuControl.Size = new System.Drawing.Size(85, 24);
			this.sortMenuControl.TabIndex = 4;
			this.sortMenuControl.Title = "SORT BY";
			// 
			// btnAll
			// 
			this.btnAll.Location = new System.Drawing.Point(11, 49);
			this.btnAll.Name = "btnAll";
			this.btnAll.Size = new System.Drawing.Size(62, 24);
			this.btnAll.TabIndex = 1;
			this.btnAll.Text = "ALL";
			this.btnAll.UseVisualStyleBackColor = true;
			this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
			// 
			// btnFilter
			// 
			this.btnFilter.Location = new System.Drawing.Point(102, 49);
			this.btnFilter.Name = "btnFilter";
			this.btnFilter.Size = new System.Drawing.Size(85, 24);
			this.btnFilter.TabIndex = 3;
			this.btnFilter.Text = "FILTER";
			this.btnFilter.UseVisualStyleBackColor = true;
			this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
			// 
			// cbComet
			// 
			this.cbComet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbComet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbComet.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.cbComet.FormattingEnabled = true;
			this.cbComet.IntegralHeight = false;
			this.cbComet.Location = new System.Drawing.Point(12, 21);
			this.cbComet.MaxDropDownItems = 21;
			this.cbComet.Name = "cbComet";
			this.cbComet.Size = new System.Drawing.Size(265, 22);
			this.cbComet.TabIndex = 0;
			this.cbComet.SelectedIndexChanged += new System.EventHandler(this.cbComet_SelectedIndexChanged);
			// 
			// ctxSelect
			// 
			this.ctxSelect.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuBrightest,
            this.mnuClosestToPerihelion,
            this.mnuClosestToEarth,
            this.mnuClosestToSun});
			// 
			// mnuBrightest
			// 
			this.mnuBrightest.Index = 0;
			this.mnuBrightest.Text = "Brightest";
			this.mnuBrightest.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuClosestToPerihelion
			// 
			this.mnuClosestToPerihelion.Index = 1;
			this.mnuClosestToPerihelion.Text = "Closest to perihelion";
			this.mnuClosestToPerihelion.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuClosestToEarth
			// 
			this.mnuClosestToEarth.Index = 2;
			this.mnuClosestToEarth.Text = "Closest to Earth";
			this.mnuClosestToEarth.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// mnuClosestToSun
			// 
			this.mnuClosestToSun.Index = 3;
			this.mnuClosestToSun.Text = "Closest to Sun";
			this.mnuClosestToSun.Click += new System.EventHandler(this.mnuCommon_Click);
			// 
			// SelectCometControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbxSelectComet);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "SelectCometControl";
			this.Size = new System.Drawing.Size(290, 85);
			this.gbxSelectComet.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbxSelectComet;
		private System.Windows.Forms.Button btnFilter;
		private System.Windows.Forms.ComboBox cbComet;
		private System.Windows.Forms.Button btnAll;
		private SortMenuControl sortMenuControl;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.ContextMenu ctxSelect;
		private System.Windows.Forms.MenuItem mnuBrightest;
		private System.Windows.Forms.MenuItem mnuClosestToEarth;
		private System.Windows.Forms.MenuItem mnuClosestToSun;
		private System.Windows.Forms.MenuItem mnuClosestToPerihelion;
	}
}
