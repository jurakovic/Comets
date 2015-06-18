namespace Comets.Forms.Orbit
{
	partial class FormOrbitViewer
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
			this.btnSet = new System.Windows.Forms.Button();
			this.btnNow = new System.Windows.Forms.Button();
			this.lblLabels = new System.Windows.Forms.Label();
			this.lblTimestep = new System.Windows.Forms.Label();
			this.lblSimulation = new System.Windows.Forms.Label();
			this.lblDate = new System.Windows.Forms.Label();
			this.lblObject = new System.Windows.Forms.Label();
			this.cboObject = new System.Windows.Forms.ComboBox();
			this.numYear = new System.Windows.Forms.NumericUpDown();
			this.numDay = new System.Windows.Forms.NumericUpDown();
			this.lblZoom = new System.Windows.Forms.Label();
			this.lblOrbits = new System.Windows.Forms.Label();
			this.lblCenter = new System.Windows.Forms.Label();
			this.cbxObject = new System.Windows.Forms.CheckBox();
			this.cbxDistance = new System.Windows.Forms.CheckBox();
			this.cbxPlanet = new System.Windows.Forms.CheckBox();
			this.cbxDate = new System.Windows.Forms.CheckBox();
			this.cboOrbits = new System.Windows.Forms.ComboBox();
			this.cboCenter = new System.Windows.Forms.ComboBox();
			this.cboTimestep = new System.Windows.Forms.ComboBox();
			this.btnForPlay = new System.Windows.Forms.Button();
			this.btnForStep = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnRevStep = new System.Windows.Forms.Button();
			this.btnRevPlay = new System.Windows.Forms.Button();
			this.scrollVert = new System.Windows.Forms.VScrollBar();
			this.scrollHorz = new System.Windows.Forms.HScrollBar();
			this.scrollZoom = new System.Windows.Forms.HScrollBar();
			this.pnlToolbox = new System.Windows.Forms.Panel();
			this.numMonth = new System.Windows.Forms.NumericUpDown();
			this.orbitPanel = new Comets.OrbitViewer.OrbitPanel();
			((System.ComponentModel.ISupportInitialize)(this.numYear)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDay)).BeginInit();
			this.pnlToolbox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numMonth)).BeginInit();
			this.SuspendLayout();
			// 
			// btnSet
			// 
			this.btnSet.Location = new System.Drawing.Point(186, 41);
			this.btnSet.Name = "btnSet";
			this.btnSet.Size = new System.Drawing.Size(79, 23);
			this.btnSet.TabIndex = 63;
			this.btnSet.Text = "Set";
			this.btnSet.UseVisualStyleBackColor = true;
			this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
			// 
			// btnNow
			// 
			this.btnNow.Location = new System.Drawing.Point(101, 41);
			this.btnNow.Name = "btnNow";
			this.btnNow.Size = new System.Drawing.Size(79, 23);
			this.btnNow.TabIndex = 62;
			this.btnNow.Text = "Now";
			this.btnNow.UseVisualStyleBackColor = true;
			this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
			// 
			// lblLabels
			// 
			this.lblLabels.AutoSize = true;
			this.lblLabels.Location = new System.Drawing.Point(309, 78);
			this.lblLabels.Name = "lblLabels";
			this.lblLabels.Size = new System.Drawing.Size(37, 13);
			this.lblLabels.TabIndex = 61;
			this.lblLabels.Text = "Labels";
			// 
			// lblTimestep
			// 
			this.lblTimestep.AutoSize = true;
			this.lblTimestep.Location = new System.Drawing.Point(309, 43);
			this.lblTimestep.Name = "lblTimestep";
			this.lblTimestep.Size = new System.Drawing.Size(50, 13);
			this.lblTimestep.TabIndex = 60;
			this.lblTimestep.Text = "Timestep";
			// 
			// lblSimulation
			// 
			this.lblSimulation.AutoSize = true;
			this.lblSimulation.Location = new System.Drawing.Point(309, 17);
			this.lblSimulation.Name = "lblSimulation";
			this.lblSimulation.Size = new System.Drawing.Size(55, 13);
			this.lblSimulation.TabIndex = 59;
			this.lblSimulation.Text = "Simulation";
			// 
			// lblDate
			// 
			this.lblDate.AutoSize = true;
			this.lblDate.Location = new System.Drawing.Point(15, 17);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(30, 13);
			this.lblDate.TabIndex = 57;
			this.lblDate.Text = "Date";
			// 
			// lblObject
			// 
			this.lblObject.AutoSize = true;
			this.lblObject.Location = new System.Drawing.Point(15, 78);
			this.lblObject.Name = "lblObject";
			this.lblObject.Size = new System.Drawing.Size(39, 13);
			this.lblObject.TabIndex = 56;
			this.lblObject.Text = "Object";
			// 
			// cboObject
			// 
			this.cboObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboObject.FormattingEnabled = true;
			this.cboObject.IntegralHeight = false;
			this.cboObject.Location = new System.Drawing.Point(102, 75);
			this.cboObject.MaxDropDownItems = 15;
			this.cboObject.Name = "cboObject";
			this.cboObject.Size = new System.Drawing.Size(162, 21);
			this.cboObject.TabIndex = 55;
			this.cboObject.SelectedIndexChanged += new System.EventHandler(this.cboObject_SelectedIndexChanged);
			// 
			// numYear
			// 
			this.numYear.Location = new System.Drawing.Point(214, 15);
			this.numYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.numYear.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numYear.Name = "numYear";
			this.numYear.Size = new System.Drawing.Size(50, 21);
			this.numYear.TabIndex = 54;
			this.numYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numYear.Value = new decimal(new int[] {
            2015,
            0,
            0,
            0});
			this.numYear.ValueChanged += new System.EventHandler(this.dateCommon_ValueChanged);
			// 
			// numDay
			// 
			this.numDay.BackColor = System.Drawing.SystemColors.Window;
			this.numDay.Location = new System.Drawing.Point(102, 15);
			this.numDay.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.numDay.Name = "numDay";
			this.numDay.ReadOnly = true;
			this.numDay.Size = new System.Drawing.Size(42, 21);
			this.numDay.TabIndex = 53;
			this.numDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numDay.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			this.numDay.ValueChanged += new System.EventHandler(this.dateCommon_ValueChanged);
			// 
			// lblZoom
			// 
			this.lblZoom.AutoSize = true;
			this.lblZoom.Location = new System.Drawing.Point(309, 142);
			this.lblZoom.Name = "lblZoom";
			this.lblZoom.Size = new System.Drawing.Size(33, 13);
			this.lblZoom.TabIndex = 52;
			this.lblZoom.Text = "Zoom";
			// 
			// lblOrbits
			// 
			this.lblOrbits.AutoSize = true;
			this.lblOrbits.Location = new System.Drawing.Point(15, 142);
			this.lblOrbits.Name = "lblOrbits";
			this.lblOrbits.Size = new System.Drawing.Size(36, 13);
			this.lblOrbits.TabIndex = 51;
			this.lblOrbits.Text = "Orbits";
			// 
			// lblCenter
			// 
			this.lblCenter.AutoSize = true;
			this.lblCenter.Location = new System.Drawing.Point(15, 110);
			this.lblCenter.Name = "lblCenter";
			this.lblCenter.Size = new System.Drawing.Size(40, 13);
			this.lblCenter.TabIndex = 50;
			this.lblCenter.Text = "Center";
			// 
			// cbxObject
			// 
			this.cbxObject.Checked = true;
			this.cbxObject.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxObject.Location = new System.Drawing.Point(400, 77);
			this.cbxObject.Name = "cbxObject";
			this.cbxObject.Size = new System.Drawing.Size(70, 17);
			this.cbxObject.TabIndex = 49;
			this.cbxObject.Text = "Object";
			this.cbxObject.UseVisualStyleBackColor = true;
			this.cbxObject.CheckedChanged += new System.EventHandler(this.cbxObject_CheckedChanged);
			// 
			// cbxDistance
			// 
			this.cbxDistance.Checked = true;
			this.cbxDistance.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxDistance.Location = new System.Drawing.Point(531, 77);
			this.cbxDistance.Name = "cbxDistance";
			this.cbxDistance.Size = new System.Drawing.Size(70, 17);
			this.cbxDistance.TabIndex = 48;
			this.cbxDistance.Text = "Distance";
			this.cbxDistance.UseVisualStyleBackColor = true;
			this.cbxDistance.CheckedChanged += new System.EventHandler(this.cbxDistance_CheckedChanged);
			// 
			// cbxPlanet
			// 
			this.cbxPlanet.Checked = true;
			this.cbxPlanet.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxPlanet.Location = new System.Drawing.Point(400, 109);
			this.cbxPlanet.Name = "cbxPlanet";
			this.cbxPlanet.Size = new System.Drawing.Size(70, 17);
			this.cbxPlanet.TabIndex = 47;
			this.cbxPlanet.Text = "Planet";
			this.cbxPlanet.UseVisualStyleBackColor = true;
			this.cbxPlanet.CheckedChanged += new System.EventHandler(this.cbxPlanet_CheckedChanged);
			// 
			// cbxDate
			// 
			this.cbxDate.Checked = true;
			this.cbxDate.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxDate.Location = new System.Drawing.Point(531, 109);
			this.cbxDate.Name = "cbxDate";
			this.cbxDate.Size = new System.Drawing.Size(70, 17);
			this.cbxDate.TabIndex = 46;
			this.cbxDate.Text = "Date";
			this.cbxDate.UseVisualStyleBackColor = true;
			this.cbxDate.CheckedChanged += new System.EventHandler(this.cbxDate_CheckedChanged);
			// 
			// cboOrbits
			// 
			this.cboOrbits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOrbits.FormattingEnabled = true;
			this.cboOrbits.Location = new System.Drawing.Point(102, 139);
			this.cboOrbits.Name = "cboOrbits";
			this.cboOrbits.Size = new System.Drawing.Size(162, 21);
			this.cboOrbits.TabIndex = 45;
			this.cboOrbits.SelectedIndexChanged += new System.EventHandler(this.cboOrbits_SelectedIndexChanged);
			// 
			// cboCenter
			// 
			this.cboCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCenter.FormattingEnabled = true;
			this.cboCenter.Location = new System.Drawing.Point(102, 107);
			this.cboCenter.Name = "cboCenter";
			this.cboCenter.Size = new System.Drawing.Size(162, 21);
			this.cboCenter.TabIndex = 44;
			this.cboCenter.SelectedIndexChanged += new System.EventHandler(this.cboCenter_SelectedIndexChanged);
			// 
			// cboTimestep
			// 
			this.cboTimestep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTimestep.FormattingEnabled = true;
			this.cboTimestep.Location = new System.Drawing.Point(400, 40);
			this.cboTimestep.Name = "cboTimestep";
			this.cboTimestep.Size = new System.Drawing.Size(161, 21);
			this.cboTimestep.TabIndex = 43;
			this.cboTimestep.SelectedIndexChanged += new System.EventHandler(this.cboTimestep_SelectedIndexChanged);
			// 
			// btnForPlay
			// 
			this.btnForPlay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnForPlay.Location = new System.Drawing.Point(531, 13);
			this.btnForPlay.Name = "btnForPlay";
			this.btnForPlay.Size = new System.Drawing.Size(31, 23);
			this.btnForPlay.TabIndex = 42;
			this.btnForPlay.Text = ">>";
			this.btnForPlay.UseVisualStyleBackColor = true;
			this.btnForPlay.Click += new System.EventHandler(this.btnForPlay_Click);
			// 
			// btnForStep
			// 
			this.btnForStep.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnForStep.Location = new System.Drawing.Point(498, 13);
			this.btnForStep.Name = "btnForStep";
			this.btnForStep.Size = new System.Drawing.Size(31, 23);
			this.btnForStep.TabIndex = 41;
			this.btnForStep.Text = ">|";
			this.btnForStep.UseVisualStyleBackColor = true;
			this.btnForStep.Click += new System.EventHandler(this.btnForStep_Click);
			// 
			// btnStop
			// 
			this.btnStop.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnStop.Location = new System.Drawing.Point(465, 13);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(31, 23);
			this.btnStop.TabIndex = 40;
			this.btnStop.Text = "||";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnRevStep
			// 
			this.btnRevStep.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnRevStep.Location = new System.Drawing.Point(432, 13);
			this.btnRevStep.Name = "btnRevStep";
			this.btnRevStep.Size = new System.Drawing.Size(31, 23);
			this.btnRevStep.TabIndex = 39;
			this.btnRevStep.Text = "|<";
			this.btnRevStep.UseVisualStyleBackColor = true;
			this.btnRevStep.Click += new System.EventHandler(this.btnRevStep_Click);
			// 
			// btnRevPlay
			// 
			this.btnRevPlay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnRevPlay.Location = new System.Drawing.Point(399, 13);
			this.btnRevPlay.Name = "btnRevPlay";
			this.btnRevPlay.Size = new System.Drawing.Size(31, 23);
			this.btnRevPlay.TabIndex = 38;
			this.btnRevPlay.Text = "<<";
			this.btnRevPlay.UseVisualStyleBackColor = true;
			this.btnRevPlay.Click += new System.EventHandler(this.btnRevPlay_Click);
			// 
			// scrollVert
			// 
			this.scrollVert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.scrollVert.LargeChange = 1;
			this.scrollVert.Location = new System.Drawing.Point(684, 0);
			this.scrollVert.Maximum = 360;
			this.scrollVert.Name = "scrollVert";
			this.scrollVert.Size = new System.Drawing.Size(17, 458);
			this.scrollVert.TabIndex = 36;
			this.scrollVert.ValueChanged += new System.EventHandler(this.scrollVert_ValueChanged);
			// 
			// scrollHorz
			// 
			this.scrollHorz.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.scrollHorz.LargeChange = 1;
			this.scrollHorz.Location = new System.Drawing.Point(0, 460);
			this.scrollHorz.Maximum = 360;
			this.scrollHorz.Name = "scrollHorz";
			this.scrollHorz.Size = new System.Drawing.Size(682, 17);
			this.scrollHorz.TabIndex = 35;
			this.scrollHorz.ValueChanged += new System.EventHandler(this.scrollHorz_ValueChanged);
			// 
			// scrollZoom
			// 
			this.scrollZoom.Location = new System.Drawing.Point(400, 142);
			this.scrollZoom.Maximum = 1000;
			this.scrollZoom.Minimum = 5;
			this.scrollZoom.Name = "scrollZoom";
			this.scrollZoom.Size = new System.Drawing.Size(285, 17);
			this.scrollZoom.TabIndex = 37;
			this.scrollZoom.Value = 5;
			this.scrollZoom.ValueChanged += new System.EventHandler(this.scrollZoom_ValueChanged);
			// 
			// pnlToolbox
			// 
			this.pnlToolbox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.pnlToolbox.Controls.Add(this.numMonth);
			this.pnlToolbox.Controls.Add(this.numDay);
			this.pnlToolbox.Controls.Add(this.btnSet);
			this.pnlToolbox.Controls.Add(this.scrollZoom);
			this.pnlToolbox.Controls.Add(this.btnNow);
			this.pnlToolbox.Controls.Add(this.btnRevPlay);
			this.pnlToolbox.Controls.Add(this.lblLabels);
			this.pnlToolbox.Controls.Add(this.btnRevStep);
			this.pnlToolbox.Controls.Add(this.lblTimestep);
			this.pnlToolbox.Controls.Add(this.btnStop);
			this.pnlToolbox.Controls.Add(this.lblSimulation);
			this.pnlToolbox.Controls.Add(this.btnForStep);
			this.pnlToolbox.Controls.Add(this.btnForPlay);
			this.pnlToolbox.Controls.Add(this.lblDate);
			this.pnlToolbox.Controls.Add(this.cboTimestep);
			this.pnlToolbox.Controls.Add(this.lblObject);
			this.pnlToolbox.Controls.Add(this.cboCenter);
			this.pnlToolbox.Controls.Add(this.cboObject);
			this.pnlToolbox.Controls.Add(this.cboOrbits);
			this.pnlToolbox.Controls.Add(this.numYear);
			this.pnlToolbox.Controls.Add(this.cbxDate);
			this.pnlToolbox.Controls.Add(this.cbxPlanet);
			this.pnlToolbox.Controls.Add(this.lblZoom);
			this.pnlToolbox.Controls.Add(this.cbxDistance);
			this.pnlToolbox.Controls.Add(this.lblOrbits);
			this.pnlToolbox.Controls.Add(this.cbxObject);
			this.pnlToolbox.Controls.Add(this.lblCenter);
			this.pnlToolbox.Location = new System.Drawing.Point(8, 484);
			this.pnlToolbox.Name = "pnlToolbox";
			this.pnlToolbox.Size = new System.Drawing.Size(687, 169);
			this.pnlToolbox.TabIndex = 0;
			// 
			// numMonth
			// 
			this.numMonth.BackColor = System.Drawing.SystemColors.Window;
			this.numMonth.Location = new System.Drawing.Point(158, 15);
			this.numMonth.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
			this.numMonth.Name = "numMonth";
			this.numMonth.ReadOnly = true;
			this.numMonth.Size = new System.Drawing.Size(42, 21);
			this.numMonth.TabIndex = 64;
			this.numMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numMonth.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
			this.numMonth.ValueChanged += new System.EventHandler(this.dateCommon_ValueChanged);
			// 
			// orbitPanel
			// 
			this.orbitPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.orbitPanel.ATime = null;
			this.orbitPanel.BackColor = System.Drawing.Color.Black;
			this.orbitPanel.CenterObjectSelected = 0;
			this.orbitPanel.Comet = null;
			this.orbitPanel.Location = new System.Drawing.Point(0, 0);
			this.orbitPanel.MinimumSize = new System.Drawing.Size(682, 458);
			this.orbitPanel.Name = "orbitPanel";
			this.orbitPanel.Offscreen = null;
			this.orbitPanel.RotateHorz = 0D;
			this.orbitPanel.RotateVert = 0D;
			this.orbitPanel.SelectedIndex = 0;
			this.orbitPanel.ShowDateLabel = false;
			this.orbitPanel.ShowDistanceLabel = false;
			this.orbitPanel.ShowObjectName = false;
			this.orbitPanel.ShowPlanetName = false;
			this.orbitPanel.Size = new System.Drawing.Size(682, 458);
			this.orbitPanel.TabIndex = 64;
			this.orbitPanel.Zoom = 0D;
			this.orbitPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.orbitPanel_MouseDown);
			this.orbitPanel.MouseEnter += new System.EventHandler(this.orbitPanel_MouseEnter);
			this.orbitPanel.MouseLeave += new System.EventHandler(this.orbitPanel_MouseLeave);
			this.orbitPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.orbitPanel_MouseMove);
			this.orbitPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.orbitPanel_MouseUp);
			this.orbitPanel.Resize += new System.EventHandler(this.orbitPanel_Resize);
			// 
			// FormOrbitViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(703, 661);
			this.Controls.Add(this.pnlToolbox);
			this.Controls.Add(this.orbitPanel);
			this.Controls.Add(this.scrollVert);
			this.Controls.Add(this.scrollHorz);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(719, 700);
			this.Name = "FormOrbitViewer";
			this.Text = "Orbit Viewer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOrbitViewer_FormClosing);
			this.Load += new System.EventHandler(this.FormOrbit_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOrbitViewer_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.numYear)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDay)).EndInit();
			this.pnlToolbox.ResumeLayout(false);
			this.pnlToolbox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numMonth)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnSet;
		private System.Windows.Forms.Button btnNow;
		private System.Windows.Forms.Label lblLabels;
		private System.Windows.Forms.Label lblTimestep;
		private System.Windows.Forms.Label lblSimulation;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.Label lblObject;
		private System.Windows.Forms.ComboBox cboObject;
		private System.Windows.Forms.NumericUpDown numYear;
		private System.Windows.Forms.NumericUpDown numDay;
		private System.Windows.Forms.Label lblZoom;
		private System.Windows.Forms.Label lblOrbits;
		private System.Windows.Forms.Label lblCenter;
		private System.Windows.Forms.CheckBox cbxObject;
		private System.Windows.Forms.CheckBox cbxDistance;
		private System.Windows.Forms.CheckBox cbxPlanet;
		private System.Windows.Forms.CheckBox cbxDate;
		private System.Windows.Forms.ComboBox cboOrbits;
		private System.Windows.Forms.ComboBox cboCenter;
		private System.Windows.Forms.ComboBox cboTimestep;
		private System.Windows.Forms.Button btnForPlay;
		private System.Windows.Forms.Button btnForStep;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnRevStep;
		private System.Windows.Forms.Button btnRevPlay;
		private System.Windows.Forms.HScrollBar scrollZoom;
		private System.Windows.Forms.VScrollBar scrollVert;
		private System.Windows.Forms.HScrollBar scrollHorz;
		private OrbitViewer.OrbitPanel orbitPanel;
		private System.Windows.Forms.Panel pnlToolbox;
		private System.Windows.Forms.NumericUpDown numMonth;

	}
}