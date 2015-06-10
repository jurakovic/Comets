﻿namespace Comets.Forms.Orbit
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
			this.domMonth = new System.Windows.Forms.DomainUpDown();
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
			this.orbitPanel = new Comets.OrbitViewer.OrbitPanel();
			this.scrollZoom = new System.Windows.Forms.HScrollBar();
			((System.ComponentModel.ISupportInitialize)(this.numYear)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDay)).BeginInit();
			this.SuspendLayout();
			// 
			// btnSet
			// 
			this.btnSet.Location = new System.Drawing.Point(183, 524);
			this.btnSet.Name = "btnSet";
			this.btnSet.Size = new System.Drawing.Size(79, 23);
			this.btnSet.TabIndex = 63;
			this.btnSet.Text = "Set";
			this.btnSet.UseVisualStyleBackColor = true;
			this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
			// 
			// btnNow
			// 
			this.btnNow.Location = new System.Drawing.Point(98, 524);
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
			this.lblLabels.Location = new System.Drawing.Point(306, 561);
			this.lblLabels.Name = "lblLabels";
			this.lblLabels.Size = new System.Drawing.Size(37, 13);
			this.lblLabels.TabIndex = 61;
			this.lblLabels.Text = "Labels";
			// 
			// lblTimestep
			// 
			this.lblTimestep.AutoSize = true;
			this.lblTimestep.Location = new System.Drawing.Point(306, 526);
			this.lblTimestep.Name = "lblTimestep";
			this.lblTimestep.Size = new System.Drawing.Size(50, 13);
			this.lblTimestep.TabIndex = 60;
			this.lblTimestep.Text = "Timestep";
			// 
			// lblSimulation
			// 
			this.lblSimulation.AutoSize = true;
			this.lblSimulation.Location = new System.Drawing.Point(306, 500);
			this.lblSimulation.Name = "lblSimulation";
			this.lblSimulation.Size = new System.Drawing.Size(55, 13);
			this.lblSimulation.TabIndex = 59;
			this.lblSimulation.Text = "Simulation";
			// 
			// domMonth
			// 
			this.domMonth.BackColor = System.Drawing.SystemColors.Window;
			this.domMonth.Location = new System.Drawing.Point(147, 498);
			this.domMonth.Name = "domMonth";
			this.domMonth.ReadOnly = true;
			this.domMonth.Size = new System.Drawing.Size(58, 21);
			this.domMonth.TabIndex = 58;
			this.domMonth.Text = "May";
			this.domMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.domMonth.SelectedItemChanged += new System.EventHandler(this.domMonth_SelectedItemChanged);
			// 
			// lblDate
			// 
			this.lblDate.AutoSize = true;
			this.lblDate.Location = new System.Drawing.Point(12, 500);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(30, 13);
			this.lblDate.TabIndex = 57;
			this.lblDate.Text = "Date";
			// 
			// lblObject
			// 
			this.lblObject.AutoSize = true;
			this.lblObject.Location = new System.Drawing.Point(12, 561);
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
			this.cboObject.Location = new System.Drawing.Point(99, 558);
			this.cboObject.MaxDropDownItems = 15;
			this.cboObject.Name = "cboObject";
			this.cboObject.Size = new System.Drawing.Size(162, 21);
			this.cboObject.TabIndex = 55;
			this.cboObject.SelectedIndexChanged += new System.EventHandler(this.cboObject_SelectedIndexChanged);
			// 
			// numYear
			// 
			this.numYear.Location = new System.Drawing.Point(211, 498);
			this.numYear.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
			this.numYear.Minimum = new decimal(new int[] {
            1000,
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
			this.numYear.ValueChanged += new System.EventHandler(this.numYear_ValueChanged);
			// 
			// numDay
			// 
			this.numDay.BackColor = System.Drawing.SystemColors.Window;
			this.numDay.Location = new System.Drawing.Point(99, 498);
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
			this.numDay.ValueChanged += new System.EventHandler(this.numDay_ValueChanged);
			// 
			// lblZoom
			// 
			this.lblZoom.AutoSize = true;
			this.lblZoom.Location = new System.Drawing.Point(306, 625);
			this.lblZoom.Name = "lblZoom";
			this.lblZoom.Size = new System.Drawing.Size(33, 13);
			this.lblZoom.TabIndex = 52;
			this.lblZoom.Text = "Zoom";
			// 
			// lblOrbits
			// 
			this.lblOrbits.AutoSize = true;
			this.lblOrbits.Location = new System.Drawing.Point(12, 625);
			this.lblOrbits.Name = "lblOrbits";
			this.lblOrbits.Size = new System.Drawing.Size(36, 13);
			this.lblOrbits.TabIndex = 51;
			this.lblOrbits.Text = "Orbits";
			// 
			// lblCenter
			// 
			this.lblCenter.AutoSize = true;
			this.lblCenter.Location = new System.Drawing.Point(12, 593);
			this.lblCenter.Name = "lblCenter";
			this.lblCenter.Size = new System.Drawing.Size(40, 13);
			this.lblCenter.TabIndex = 50;
			this.lblCenter.Text = "Center";
			// 
			// cbxObject
			// 
			this.cbxObject.Checked = true;
			this.cbxObject.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxObject.Location = new System.Drawing.Point(397, 560);
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
			this.cbxDistance.Location = new System.Drawing.Point(528, 560);
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
			this.cbxPlanet.Location = new System.Drawing.Point(397, 592);
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
			this.cbxDate.Location = new System.Drawing.Point(528, 592);
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
			this.cboOrbits.Location = new System.Drawing.Point(99, 622);
			this.cboOrbits.Name = "cboOrbits";
			this.cboOrbits.Size = new System.Drawing.Size(162, 21);
			this.cboOrbits.TabIndex = 45;
			this.cboOrbits.SelectedIndexChanged += new System.EventHandler(this.cboOrbits_SelectedIndexChanged);
			// 
			// cboCenter
			// 
			this.cboCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCenter.FormattingEnabled = true;
			this.cboCenter.Location = new System.Drawing.Point(99, 590);
			this.cboCenter.Name = "cboCenter";
			this.cboCenter.Size = new System.Drawing.Size(162, 21);
			this.cboCenter.TabIndex = 44;
			this.cboCenter.SelectedIndexChanged += new System.EventHandler(this.cboCenter_SelectedIndexChanged);
			// 
			// cboTimestep
			// 
			this.cboTimestep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTimestep.FormattingEnabled = true;
			this.cboTimestep.Location = new System.Drawing.Point(397, 523);
			this.cboTimestep.Name = "cboTimestep";
			this.cboTimestep.Size = new System.Drawing.Size(161, 21);
			this.cboTimestep.TabIndex = 43;
			this.cboTimestep.SelectedIndexChanged += new System.EventHandler(this.cboTimestep_SelectedIndexChanged);
			// 
			// btnForPlay
			// 
			this.btnForPlay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnForPlay.Location = new System.Drawing.Point(528, 496);
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
			this.btnForStep.Location = new System.Drawing.Point(495, 496);
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
			this.btnStop.Location = new System.Drawing.Point(462, 496);
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
			this.btnRevStep.Location = new System.Drawing.Point(429, 496);
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
			this.btnRevPlay.Location = new System.Drawing.Point(396, 496);
			this.btnRevPlay.Name = "btnRevPlay";
			this.btnRevPlay.Size = new System.Drawing.Size(31, 23);
			this.btnRevPlay.TabIndex = 38;
			this.btnRevPlay.Text = "<<";
			this.btnRevPlay.UseVisualStyleBackColor = true;
			this.btnRevPlay.Click += new System.EventHandler(this.btnRevPlay_Click);
			// 
			// scrollVert
			// 
			this.scrollVert.LargeChange = 1;
			this.scrollVert.Location = new System.Drawing.Point(684, 0);
			this.scrollVert.Maximum = 180;
			this.scrollVert.Name = "scrollVert";
			this.scrollVert.Size = new System.Drawing.Size(17, 458);
			this.scrollVert.TabIndex = 36;
			this.scrollVert.Value = 130;
			this.scrollVert.ValueChanged += new System.EventHandler(this.scrollVert_ValueChanged);
			// 
			// scrollHorz
			// 
			this.scrollHorz.LargeChange = 1;
			this.scrollHorz.Location = new System.Drawing.Point(0, 460);
			this.scrollHorz.Maximum = 360;
			this.scrollHorz.Name = "scrollHorz";
			this.scrollHorz.Size = new System.Drawing.Size(682, 17);
			this.scrollHorz.TabIndex = 35;
			this.scrollHorz.Value = 255;
			this.scrollHorz.ValueChanged += new System.EventHandler(this.scrollHorz_ValueChanged);
			// 
			// orbitPanel
			// 
			this.orbitPanel.ATime = null;
			this.orbitPanel.BackColor = System.Drawing.Color.Black;
			this.orbitPanel.CenterObjectSelected = 0;
			this.orbitPanel.Comet = null;
			this.orbitPanel.Location = new System.Drawing.Point(0, 0);
			this.orbitPanel.Name = "orbitPanel";
			this.orbitPanel.Offscreen = null;
			this.orbitPanel.PaintEnabled = false;
			this.orbitPanel.RotateHorz = 0D;
			this.orbitPanel.RotateVert = 0D;
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
			// 
			// scrollZoom
			// 
			this.scrollZoom.Location = new System.Drawing.Point(397, 625);
			this.scrollZoom.Maximum = 1000;
			this.scrollZoom.Minimum = 5;
			this.scrollZoom.Name = "scrollZoom";
			this.scrollZoom.Size = new System.Drawing.Size(285, 17);
			this.scrollZoom.TabIndex = 37;
			this.scrollZoom.Value = 67;
			this.scrollZoom.ValueChanged += new System.EventHandler(this.scrollZoom_ValueChanged);
			// 
			// FormOrbitViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(703, 661);
			this.Controls.Add(this.orbitPanel);
			this.Controls.Add(this.btnSet);
			this.Controls.Add(this.btnNow);
			this.Controls.Add(this.lblLabels);
			this.Controls.Add(this.lblTimestep);
			this.Controls.Add(this.lblSimulation);
			this.Controls.Add(this.domMonth);
			this.Controls.Add(this.lblDate);
			this.Controls.Add(this.lblObject);
			this.Controls.Add(this.cboObject);
			this.Controls.Add(this.numYear);
			this.Controls.Add(this.numDay);
			this.Controls.Add(this.lblZoom);
			this.Controls.Add(this.lblOrbits);
			this.Controls.Add(this.lblCenter);
			this.Controls.Add(this.cbxObject);
			this.Controls.Add(this.cbxDistance);
			this.Controls.Add(this.cbxPlanet);
			this.Controls.Add(this.cbxDate);
			this.Controls.Add(this.cboOrbits);
			this.Controls.Add(this.cboCenter);
			this.Controls.Add(this.cboTimestep);
			this.Controls.Add(this.btnForPlay);
			this.Controls.Add(this.btnForStep);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnRevStep);
			this.Controls.Add(this.btnRevPlay);
			this.Controls.Add(this.scrollZoom);
			this.Controls.Add(this.scrollVert);
			this.Controls.Add(this.scrollHorz);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(719, 700);
			this.Name = "FormOrbitViewer";
			this.Text = "Orbit Viewer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOrbitViewer_FormClosing);
			this.Load += new System.EventHandler(this.FormOrbit_Load);
			((System.ComponentModel.ISupportInitialize)(this.numYear)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDay)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSet;
		private System.Windows.Forms.Button btnNow;
		private System.Windows.Forms.Label lblLabels;
		private System.Windows.Forms.Label lblTimestep;
		private System.Windows.Forms.Label lblSimulation;
		private System.Windows.Forms.DomainUpDown domMonth;
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

	}
}