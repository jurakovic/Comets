﻿namespace Comets.Application.OrbitViewer
{
	partial class OrbitViewerControl
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
			this.pnlToolbox = new System.Windows.Forms.Panel();
			this.cpnlComet = new Controls.CollapsiblePanel();
			this.cometControl = new Controls.CometControl();
			this.cpnlMode = new Controls.CollapsiblePanel();
			this.modeControl = new Controls.ModeControl();
			this.cpnlDisplay = new Controls.CollapsiblePanel();
			this.displayControl = new Controls.DisplayControl();
			this.cpnlDateTime = new Controls.CollapsiblePanel();
			this.dateTimeControl = new Controls.DateTimeControl();
			this.cpnlSimulation = new Controls.CollapsiblePanel();
			this.simulationControl = new Controls.SimulationControl();
			this.cpnlFilterOnDate = new Controls.CollapsiblePanel();
			this.filterControl = new Controls.FilterControl();
			this.cpnlInfoLabels = new Controls.CollapsiblePanel();
			this.infoLabelsControl = new Controls.InfoLabelsControl();
			this.cpnlMisc = new Controls.CollapsiblePanel();
			this.miscControl = new Controls.MiscControl();
			this.orbitPanel = new Comets.OrbitViewer.OrbitPanel();
			this.scrollVert = new System.Windows.Forms.VScrollBar();
			this.scrollHorz = new System.Windows.Forms.HScrollBar();
			this.scrollZoom = new System.Windows.Forms.HScrollBar();
			this.pnlToolbox.SuspendLayout();
			this.cpnlComet.WorkingArea.SuspendLayout();
			this.cpnlMode.WorkingArea.SuspendLayout();
			this.cpnlDisplay.WorkingArea.SuspendLayout();
			this.cpnlDateTime.WorkingArea.SuspendLayout();
			this.cpnlSimulation.WorkingArea.SuspendLayout();
			this.cpnlFilterOnDate.WorkingArea.SuspendLayout();
			this.cpnlInfoLabels.WorkingArea.SuspendLayout();
			this.cpnlMisc.WorkingArea.SuspendLayout();
			this.orbitPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlToolbox
			// 
			this.pnlToolbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlToolbox.Controls.Add(this.cpnlComet);
			this.pnlToolbox.Controls.Add(this.cpnlMode);
			this.pnlToolbox.Controls.Add(this.cpnlDisplay);
			this.pnlToolbox.Controls.Add(this.cpnlDateTime);
			this.pnlToolbox.Controls.Add(this.cpnlSimulation);
			this.pnlToolbox.Controls.Add(this.cpnlFilterOnDate);
			this.pnlToolbox.Controls.Add(this.cpnlInfoLabels);
			this.pnlToolbox.Controls.Add(this.cpnlMisc);
			this.pnlToolbox.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlToolbox.Location = new System.Drawing.Point(0, 0);
			this.pnlToolbox.Name = "pnlToolbox";
			this.pnlToolbox.Size = new System.Drawing.Size(192, 900);
			this.pnlToolbox.TabIndex = 0;
			// 
			// cpnlComet
			// 
			this.cpnlComet.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cpnlComet.HeightExpanded = 114;
			this.cpnlComet.Location = new System.Drawing.Point(0, -1);
			this.cpnlComet.Name = "cpnlComet";
			this.cpnlComet.Size = new System.Drawing.Size(189, 114);
			this.cpnlComet.TabIndex = 0;
			this.cpnlComet.Title = "Comet";
			// 
			// 
			// 
			this.cpnlComet.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.cpnlComet.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			this.cpnlComet.WorkingArea.Controls.Add(this.cometControl);
			this.cpnlComet.WorkingArea.Location = new System.Drawing.Point(4, 28);
			this.cpnlComet.WorkingArea.Name = "WorkingArea";
			this.cpnlComet.WorkingArea.Size = new System.Drawing.Size(173, 79);
			this.cpnlComet.WorkingArea.TabIndex = 1;
			// 
			// cometControl
			// 
			this.cometControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cometControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cometControl.Location = new System.Drawing.Point(0, 0);
			this.cometControl.Name = "cometControl";
			this.cometControl.Size = new System.Drawing.Size(173, 79);
			this.cometControl.TabIndex = 0;
			// 
			// cpnlMode
			// 
			this.cpnlMode.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cpnlMode.HeightExpanded = 64;
			this.cpnlMode.Location = new System.Drawing.Point(0, 109);
			this.cpnlMode.Name = "cpnlMode";
			this.cpnlMode.Size = new System.Drawing.Size(189, 65);
			this.cpnlMode.TabIndex = 1;
			this.cpnlMode.Title = "Mode";
			// 
			// 
			// 
			this.cpnlMode.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.cpnlMode.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			this.cpnlMode.WorkingArea.Controls.Add(this.modeControl);
			this.cpnlMode.WorkingArea.Location = new System.Drawing.Point(4, 28);
			this.cpnlMode.WorkingArea.Name = "WorkingArea";
			this.cpnlMode.WorkingArea.Size = new System.Drawing.Size(173, 30);
			this.cpnlMode.WorkingArea.TabIndex = 1;
			// 
			// modeControl
			// 
			this.modeControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.modeControl.Location = new System.Drawing.Point(0, 0);
			this.modeControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.modeControl.Name = "modeControl";
			this.modeControl.Size = new System.Drawing.Size(173, 30);
			this.modeControl.TabIndex = 0;
			// 
			// cpnlDisplay
			// 
			this.cpnlDisplay.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cpnlDisplay.HeightExpanded = 191;
			this.cpnlDisplay.Location = new System.Drawing.Point(0, 170);
			this.cpnlDisplay.Name = "cpnlDisplay";
			this.cpnlDisplay.Size = new System.Drawing.Size(189, 191);
			this.cpnlDisplay.TabIndex = 2;
			this.cpnlDisplay.Title = "Display";
			// 
			// 
			// 
			this.cpnlDisplay.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.cpnlDisplay.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			this.cpnlDisplay.WorkingArea.Controls.Add(this.displayControl);
			this.cpnlDisplay.WorkingArea.Location = new System.Drawing.Point(4, 28);
			this.cpnlDisplay.WorkingArea.Name = "WorkingArea";
			this.cpnlDisplay.WorkingArea.Size = new System.Drawing.Size(173, 156);
			this.cpnlDisplay.WorkingArea.TabIndex = 1;
			// 
			// displayControl
			// 
			this.displayControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.displayControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.displayControl.Location = new System.Drawing.Point(0, 0);
			this.displayControl.Name = "displayControl";
			this.displayControl.Size = new System.Drawing.Size(173, 156);
			this.displayControl.TabIndex = 0;
			// 
			// cpnlDateTime
			// 
			this.cpnlDateTime.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cpnlDateTime.HeightExpanded = 66;
			this.cpnlDateTime.Location = new System.Drawing.Point(0, 357);
			this.cpnlDateTime.Name = "cpnlDateTime";
			this.cpnlDateTime.Size = new System.Drawing.Size(189, 67);
			this.cpnlDateTime.TabIndex = 3;
			this.cpnlDateTime.Title = "Date and time";
			// 
			// 
			// 
			this.cpnlDateTime.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.cpnlDateTime.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			this.cpnlDateTime.WorkingArea.Controls.Add(this.dateTimeControl);
			this.cpnlDateTime.WorkingArea.Location = new System.Drawing.Point(4, 28);
			this.cpnlDateTime.WorkingArea.Name = "WorkingArea";
			this.cpnlDateTime.WorkingArea.Size = new System.Drawing.Size(173, 32);
			this.cpnlDateTime.WorkingArea.TabIndex = 1;
			// 
			// dateTimeControl
			// 
			this.dateTimeControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dateTimeControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.dateTimeControl.Location = new System.Drawing.Point(0, 0);
			this.dateTimeControl.Name = "dateTimeControl";
			this.dateTimeControl.Size = new System.Drawing.Size(173, 32);
			this.dateTimeControl.TabIndex = 0;
			// 
			// cpnlSimulation
			// 
			this.cpnlSimulation.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cpnlSimulation.HeightExpanded = 95;
			this.cpnlSimulation.Location = new System.Drawing.Point(0, 420);
			this.cpnlSimulation.Name = "cpnlSimulation";
			this.cpnlSimulation.Size = new System.Drawing.Size(189, 95);
			this.cpnlSimulation.TabIndex = 4;
			this.cpnlSimulation.Title = "Simulation";
			// 
			// 
			// 
			this.cpnlSimulation.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.cpnlSimulation.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			this.cpnlSimulation.WorkingArea.Controls.Add(this.simulationControl);
			this.cpnlSimulation.WorkingArea.Location = new System.Drawing.Point(4, 28);
			this.cpnlSimulation.WorkingArea.Name = "WorkingArea";
			this.cpnlSimulation.WorkingArea.Size = new System.Drawing.Size(173, 60);
			this.cpnlSimulation.WorkingArea.TabIndex = 1;
			// 
			// simulationControl
			// 
			this.simulationControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.simulationControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.simulationControl.Location = new System.Drawing.Point(0, 0);
			this.simulationControl.Name = "simulationControl";
			this.simulationControl.Size = new System.Drawing.Size(173, 60);
			this.simulationControl.TabIndex = 0;
			// 
			// cpnlFilterOnDate
			// 
			this.cpnlFilterOnDate.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cpnlFilterOnDate.HeightExpanded = 133;
			this.cpnlFilterOnDate.Location = new System.Drawing.Point(0, 511);
			this.cpnlFilterOnDate.Name = "cpnlFilterOnDate";
			this.cpnlFilterOnDate.Size = new System.Drawing.Size(189, 133);
			this.cpnlFilterOnDate.TabIndex = 5;
			this.cpnlFilterOnDate.Title = "Filter on date";
			// 
			// 
			// 
			this.cpnlFilterOnDate.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.cpnlFilterOnDate.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			this.cpnlFilterOnDate.WorkingArea.Controls.Add(this.filterControl);
			this.cpnlFilterOnDate.WorkingArea.Location = new System.Drawing.Point(4, 28);
			this.cpnlFilterOnDate.WorkingArea.Name = "WorkingArea";
			this.cpnlFilterOnDate.WorkingArea.Size = new System.Drawing.Size(173, 98);
			this.cpnlFilterOnDate.WorkingArea.TabIndex = 1;
			// 
			// filterControl
			// 
			this.filterControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.filterControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.filterControl.Location = new System.Drawing.Point(0, 0);
			this.filterControl.Name = "filterControl";
			this.filterControl.Size = new System.Drawing.Size(173, 98);
			this.filterControl.TabIndex = 0;
			// 
			// cpnlInfoLabels
			// 
			this.cpnlInfoLabels.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cpnlInfoLabels.HeightExpanded = 86;
			this.cpnlInfoLabels.Location = new System.Drawing.Point(0, 640);
			this.cpnlInfoLabels.Name = "cpnlInfoLabels";
			this.cpnlInfoLabels.Size = new System.Drawing.Size(189, 86);
			this.cpnlInfoLabels.TabIndex = 6;
			this.cpnlInfoLabels.Title = "Info labels";
			// 
			// 
			// 
			this.cpnlInfoLabels.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.cpnlInfoLabels.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			this.cpnlInfoLabels.WorkingArea.Controls.Add(this.infoLabelsControl);
			this.cpnlInfoLabels.WorkingArea.Location = new System.Drawing.Point(4, 28);
			this.cpnlInfoLabels.WorkingArea.Name = "WorkingArea";
			this.cpnlInfoLabels.WorkingArea.Size = new System.Drawing.Size(173, 51);
			this.cpnlInfoLabels.WorkingArea.TabIndex = 1;
			// 
			// infoLabelsControl
			// 
			this.infoLabelsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.infoLabelsControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.infoLabelsControl.Location = new System.Drawing.Point(0, 0);
			this.infoLabelsControl.Name = "infoLabelsControl";
			this.infoLabelsControl.Size = new System.Drawing.Size(173, 51);
			this.infoLabelsControl.TabIndex = 0;
			// 
			// cpnlMisc
			// 
			this.cpnlMisc.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cpnlMisc.HeightExpanded = 112;
			this.cpnlMisc.Location = new System.Drawing.Point(0, 722);
			this.cpnlMisc.Name = "cpnlMisc";
			this.cpnlMisc.Size = new System.Drawing.Size(189, 112);
			this.cpnlMisc.TabIndex = 7;
			this.cpnlMisc.Title = "Misc.";
			// 
			// 
			// 
			this.cpnlMisc.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.cpnlMisc.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			this.cpnlMisc.WorkingArea.Controls.Add(this.miscControl);
			this.cpnlMisc.WorkingArea.Location = new System.Drawing.Point(4, 28);
			this.cpnlMisc.WorkingArea.Name = "WorkingArea";
			this.cpnlMisc.WorkingArea.Size = new System.Drawing.Size(173, 77);
			this.cpnlMisc.WorkingArea.TabIndex = 1;
			// 
			// miscControl
			// 
			this.miscControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.miscControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.miscControl.Location = new System.Drawing.Point(0, 0);
			this.miscControl.Name = "miscControl";
			this.miscControl.Size = new System.Drawing.Size(173, 77);
			this.miscControl.TabIndex = 0;
			// 
			// orbitPanel
			// 
			this.orbitPanel.BackColor = System.Drawing.Color.Black;
			this.orbitPanel.Controls.Add(this.scrollVert);
			this.orbitPanel.Controls.Add(this.scrollHorz);
			this.orbitPanel.Controls.Add(this.scrollZoom);
			this.orbitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.orbitPanel.Location = new System.Drawing.Point(192, 0);
			this.orbitPanel.MinimumSize = new System.Drawing.Size(682, 458);
			this.orbitPanel.Name = "orbitPanel";
			this.orbitPanel.Size = new System.Drawing.Size(742, 900);
			this.orbitPanel.TabIndex = 0;
			this.orbitPanel.MouseClick += this.orbitPanel_MouseClick;
			this.orbitPanel.MouseDoubleClick += this.orbitPanel_MouseDoubleClick;
			this.orbitPanel.MouseDown += this.orbitPanel_MouseDown;
			this.orbitPanel.MouseEnter += this.orbitPanel_MouseEnter;
			this.orbitPanel.MouseLeave += this.orbitPanel_MouseLeave;
			this.orbitPanel.MouseMove += this.orbitPanel_MouseMove;
			this.orbitPanel.PreviewKeyDown += this.orbitPanel_PreviewKeyDown;
			this.orbitPanel.Resize += this.orbitPanel_Resize;
			// 
			// scrollVert
			// 
			this.scrollVert.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.scrollVert.LargeChange = 1;
			this.scrollVert.Location = new System.Drawing.Point(708, 16);
			this.scrollVert.Maximum = 359;
			this.scrollVert.Name = "scrollVert";
			this.scrollVert.Size = new System.Drawing.Size(17, 272);
			this.scrollVert.TabIndex = 0;
			this.scrollVert.Visible = false;
			this.scrollVert.ValueChanged += this.scrollVert_ValueChanged;
			// 
			// scrollHorz
			// 
			this.scrollHorz.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.scrollHorz.LargeChange = 1;
			this.scrollHorz.Location = new System.Drawing.Point(432, 300);
			this.scrollHorz.Maximum = 359;
			this.scrollHorz.Name = "scrollHorz";
			this.scrollHorz.Size = new System.Drawing.Size(293, 17);
			this.scrollHorz.TabIndex = 1;
			this.scrollHorz.Visible = false;
			this.scrollHorz.ValueChanged += this.scrollHorz_ValueChanged;
			// 
			// scrollZoom
			// 
			this.scrollZoom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.scrollZoom.Location = new System.Drawing.Point(432, 327);
			this.scrollZoom.Maximum = 5000;
			this.scrollZoom.Minimum = 5;
			this.scrollZoom.Name = "scrollZoom";
			this.scrollZoom.Size = new System.Drawing.Size(293, 17);
			this.scrollZoom.TabIndex = 2;
			this.scrollZoom.Value = 5;
			this.scrollZoom.Visible = false;
			this.scrollZoom.ValueChanged += this.scrollZoom_ValueChanged;
			// 
			// OrbitViewerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.orbitPanel);
			this.Controls.Add(this.pnlToolbox);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.MinimumSize = new System.Drawing.Size(720, 650);
			this.Name = "OrbitViewerControl";
			this.Size = new System.Drawing.Size(934, 900);
			this.pnlToolbox.ResumeLayout(false);
			this.cpnlComet.WorkingArea.ResumeLayout(false);
			this.cpnlMode.WorkingArea.ResumeLayout(false);
			this.cpnlDisplay.WorkingArea.ResumeLayout(false);
			this.cpnlDateTime.WorkingArea.ResumeLayout(false);
			this.cpnlSimulation.WorkingArea.ResumeLayout(false);
			this.cpnlFilterOnDate.WorkingArea.ResumeLayout(false);
			this.cpnlInfoLabels.WorkingArea.ResumeLayout(false);
			this.cpnlMisc.WorkingArea.ResumeLayout(false);
			this.orbitPanel.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion
		private System.Windows.Forms.HScrollBar scrollZoom;
		private System.Windows.Forms.VScrollBar scrollVert;
		private System.Windows.Forms.HScrollBar scrollHorz;
		private Comets.OrbitViewer.OrbitPanel orbitPanel;
		private System.Windows.Forms.Panel pnlToolbox;
		private Controls.CollapsiblePanel cpnlComet;
		private Controls.CollapsiblePanel cpnlMode;
		private Controls.ModeControl modeControl;
		private Controls.DisplayControl displayControl;
		private Controls.CollapsiblePanel cpnlDateTime;
		private Controls.DateTimeControl dateTimeControl;
		private Controls.CollapsiblePanel cpnlSimulation;
		private Controls.SimulationControl simulationControl;
		private Controls.CollapsiblePanel cpnlFilterOnDate;
		private Controls.FilterControl filterControl;
		private Controls.CollapsiblePanel cpnlInfoLabels;
		private Controls.InfoLabelsControl infoLabelsControl;
		private Controls.CollapsiblePanel cpnlMisc;
		private Controls.MiscControl miscControl;
		private Controls.CometControl cometControl;
		private Controls.CollapsiblePanel cpnlDisplay;
	}
}
