namespace Comets.Application.OrbitViewer
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
			pnlToolbox = new System.Windows.Forms.Panel();
			cpnlComet = new Controls.CollapsiblePanel();
			cometControl = new Controls.CometControl();
			cpnlMode = new Controls.CollapsiblePanel();
			modeControl = new Controls.ModeControl();
			cpnlDisplay = new Controls.CollapsiblePanel();
			displayControl = new Controls.DisplayControl();
			cpnlDateTime = new Controls.CollapsiblePanel();
			dateTimeControl = new Controls.DateTimeControl();
			cpnlSimulation = new Controls.CollapsiblePanel();
			simulationControl = new Controls.SimulationControl();
			cpnlFilterOnDate = new Controls.CollapsiblePanel();
			filterControl = new Controls.FilterControl();
			cpnlInfoLabels = new Controls.CollapsiblePanel();
			infoLabelsControl = new Controls.InfoLabelsControl();
			cpnlMisc = new Controls.CollapsiblePanel();
			miscControl = new Controls.MiscControl();
			orbitPanel = new Comets.OrbitViewer.OrbitPanel();
			scrollVert = new System.Windows.Forms.VScrollBar();
			scrollHorz = new System.Windows.Forms.HScrollBar();
			scrollZoom = new System.Windows.Forms.HScrollBar();
			pnlToolbox.SuspendLayout();
			cpnlComet.WorkingArea.SuspendLayout();
			cpnlMode.WorkingArea.SuspendLayout();
			cpnlDisplay.WorkingArea.SuspendLayout();
			cpnlDateTime.WorkingArea.SuspendLayout();
			cpnlSimulation.WorkingArea.SuspendLayout();
			cpnlFilterOnDate.WorkingArea.SuspendLayout();
			cpnlInfoLabels.WorkingArea.SuspendLayout();
			cpnlMisc.WorkingArea.SuspendLayout();
			orbitPanel.SuspendLayout();
			SuspendLayout();
			// 
			// pnlToolbox
			// 
			pnlToolbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			pnlToolbox.Controls.Add(cpnlComet);
			pnlToolbox.Controls.Add(cpnlMode);
			pnlToolbox.Controls.Add(cpnlDisplay);
			pnlToolbox.Controls.Add(cpnlDateTime);
			pnlToolbox.Controls.Add(cpnlSimulation);
			pnlToolbox.Controls.Add(cpnlFilterOnDate);
			pnlToolbox.Controls.Add(cpnlInfoLabels);
			pnlToolbox.Controls.Add(cpnlMisc);
			pnlToolbox.Dock = System.Windows.Forms.DockStyle.Left;
			pnlToolbox.Location = new System.Drawing.Point(0, 0);
			pnlToolbox.Name = "pnlToolbox";
			pnlToolbox.Size = new System.Drawing.Size(192, 900);
			pnlToolbox.TabIndex = 0;
			// 
			// cpnlComet
			// 
			cpnlComet.Font = new System.Drawing.Font("Tahoma", 8.25F);
			cpnlComet.HeightExpanded = 114;
			cpnlComet.Location = new System.Drawing.Point(0, -1);
			cpnlComet.Name = "cpnlComet";
			cpnlComet.Size = new System.Drawing.Size(189, 114);
			cpnlComet.TabIndex = 0;
			cpnlComet.Title = "Comet";
			// 
			// 
			// 
			cpnlComet.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			cpnlComet.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			cpnlComet.WorkingArea.Controls.Add(cometControl);
			cpnlComet.WorkingArea.Location = new System.Drawing.Point(4, 28);
			cpnlComet.WorkingArea.Name = "WorkingArea";
			cpnlComet.WorkingArea.Size = new System.Drawing.Size(173, 79);
			cpnlComet.WorkingArea.TabIndex = 1;
			// 
			// cometControl
			// 
			cometControl.Dock = System.Windows.Forms.DockStyle.Fill;
			cometControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			cometControl.Location = new System.Drawing.Point(0, 0);
			cometControl.Name = "cometControl";
			cometControl.Size = new System.Drawing.Size(173, 79);
			cometControl.TabIndex = 0;
			// 
			// cpnlMode
			// 
			cpnlMode.Font = new System.Drawing.Font("Tahoma", 8.25F);
			cpnlMode.HeightExpanded = 64;
			cpnlMode.Location = new System.Drawing.Point(0, 109);
			cpnlMode.Name = "cpnlMode";
			cpnlMode.Size = new System.Drawing.Size(189, 65);
			cpnlMode.TabIndex = 1;
			cpnlMode.Title = "Mode";
			// 
			// 
			// 
			cpnlMode.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			cpnlMode.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			cpnlMode.WorkingArea.Controls.Add(modeControl);
			cpnlMode.WorkingArea.Location = new System.Drawing.Point(4, 28);
			cpnlMode.WorkingArea.Name = "WorkingArea";
			cpnlMode.WorkingArea.Size = new System.Drawing.Size(173, 30);
			cpnlMode.WorkingArea.TabIndex = 1;
			// 
			// modeControl
			// 
			modeControl.Dock = System.Windows.Forms.DockStyle.Fill;
			modeControl.Location = new System.Drawing.Point(0, 0);
			modeControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			modeControl.Name = "modeControl";
			modeControl.Size = new System.Drawing.Size(173, 30);
			modeControl.TabIndex = 0;
			// 
			// cpnlDisplay
			// 
			cpnlDisplay.Font = new System.Drawing.Font("Tahoma", 8.25F);
			cpnlDisplay.HeightExpanded = 191;
			cpnlDisplay.Location = new System.Drawing.Point(0, 170);
			cpnlDisplay.Name = "cpnlDisplay";
			cpnlDisplay.Size = new System.Drawing.Size(189, 191);
			cpnlDisplay.TabIndex = 2;
			cpnlDisplay.Title = "Display";
			// 
			// 
			// 
			cpnlDisplay.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			cpnlDisplay.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			cpnlDisplay.WorkingArea.Controls.Add(displayControl);
			cpnlDisplay.WorkingArea.Location = new System.Drawing.Point(4, 28);
			cpnlDisplay.WorkingArea.Name = "WorkingArea";
			cpnlDisplay.WorkingArea.Size = new System.Drawing.Size(173, 156);
			cpnlDisplay.WorkingArea.TabIndex = 1;
			// 
			// displayControl
			// 
			displayControl.Dock = System.Windows.Forms.DockStyle.Fill;
			displayControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			displayControl.Location = new System.Drawing.Point(0, 0);
			displayControl.Name = "displayControl";
			displayControl.Size = new System.Drawing.Size(173, 156);
			displayControl.TabIndex = 0;
			// 
			// cpnlDateTime
			// 
			cpnlDateTime.Font = new System.Drawing.Font("Tahoma", 8.25F);
			cpnlDateTime.HeightExpanded = 66;
			cpnlDateTime.Location = new System.Drawing.Point(0, 357);
			cpnlDateTime.Name = "cpnlDateTime";
			cpnlDateTime.Size = new System.Drawing.Size(189, 66);
			cpnlDateTime.TabIndex = 3;
			cpnlDateTime.Title = "Date and time";
			// 
			// 
			// 
			cpnlDateTime.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			cpnlDateTime.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			cpnlDateTime.WorkingArea.Controls.Add(dateTimeControl);
			cpnlDateTime.WorkingArea.Location = new System.Drawing.Point(4, 28);
			cpnlDateTime.WorkingArea.Name = "WorkingArea";
			cpnlDateTime.WorkingArea.Size = new System.Drawing.Size(173, 31);
			cpnlDateTime.WorkingArea.TabIndex = 1;
			// 
			// dateTimeControl
			// 
			dateTimeControl.Dock = System.Windows.Forms.DockStyle.Fill;
			dateTimeControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			dateTimeControl.Location = new System.Drawing.Point(0, 0);
			dateTimeControl.Name = "dateTimeControl";
			dateTimeControl.Size = new System.Drawing.Size(173, 31);
			dateTimeControl.TabIndex = 0;
			// 
			// cpnlSimulation
			// 
			cpnlSimulation.Font = new System.Drawing.Font("Tahoma", 8.25F);
			cpnlSimulation.HeightExpanded = 95;
			cpnlSimulation.Location = new System.Drawing.Point(0, 419);
			cpnlSimulation.Name = "cpnlSimulation";
			cpnlSimulation.Size = new System.Drawing.Size(189, 95);
			cpnlSimulation.TabIndex = 4;
			cpnlSimulation.Title = "Simulation";
			// 
			// 
			// 
			cpnlSimulation.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			cpnlSimulation.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			cpnlSimulation.WorkingArea.Controls.Add(simulationControl);
			cpnlSimulation.WorkingArea.Location = new System.Drawing.Point(4, 28);
			cpnlSimulation.WorkingArea.Name = "WorkingArea";
			cpnlSimulation.WorkingArea.Size = new System.Drawing.Size(173, 60);
			cpnlSimulation.WorkingArea.TabIndex = 1;
			// 
			// simulationControl
			// 
			simulationControl.Dock = System.Windows.Forms.DockStyle.Fill;
			simulationControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			simulationControl.Location = new System.Drawing.Point(0, 0);
			simulationControl.Name = "simulationControl";
			simulationControl.Size = new System.Drawing.Size(173, 60);
			simulationControl.TabIndex = 0;
			// 
			// cpnlFilterOnDate
			// 
			cpnlFilterOnDate.Font = new System.Drawing.Font("Tahoma", 8.25F);
			cpnlFilterOnDate.HeightExpanded = 133;
			cpnlFilterOnDate.Location = new System.Drawing.Point(0, 510);
			cpnlFilterOnDate.Name = "cpnlFilterOnDate";
			cpnlFilterOnDate.Size = new System.Drawing.Size(189, 133);
			cpnlFilterOnDate.TabIndex = 5;
			cpnlFilterOnDate.Title = "Filter on date";
			// 
			// 
			// 
			cpnlFilterOnDate.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			cpnlFilterOnDate.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			cpnlFilterOnDate.WorkingArea.Controls.Add(filterControl);
			cpnlFilterOnDate.WorkingArea.Location = new System.Drawing.Point(4, 28);
			cpnlFilterOnDate.WorkingArea.Name = "WorkingArea";
			cpnlFilterOnDate.WorkingArea.Size = new System.Drawing.Size(173, 98);
			cpnlFilterOnDate.WorkingArea.TabIndex = 1;
			// 
			// filterControl
			// 
			filterControl.Dock = System.Windows.Forms.DockStyle.Fill;
			filterControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			filterControl.Location = new System.Drawing.Point(0, 0);
			filterControl.Name = "filterControl";
			filterControl.Size = new System.Drawing.Size(173, 98);
			filterControl.TabIndex = 0;
			// 
			// cpnlInfoLabels
			// 
			cpnlInfoLabels.Font = new System.Drawing.Font("Tahoma", 8.25F);
			cpnlInfoLabels.HeightExpanded = 86;
			cpnlInfoLabels.Location = new System.Drawing.Point(0, 639);
			cpnlInfoLabels.Name = "cpnlInfoLabels";
			cpnlInfoLabels.Size = new System.Drawing.Size(189, 86);
			cpnlInfoLabels.TabIndex = 6;
			cpnlInfoLabels.Title = "Info labels";
			// 
			// 
			// 
			cpnlInfoLabels.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			cpnlInfoLabels.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			cpnlInfoLabels.WorkingArea.Controls.Add(infoLabelsControl);
			cpnlInfoLabels.WorkingArea.Location = new System.Drawing.Point(4, 28);
			cpnlInfoLabels.WorkingArea.Name = "WorkingArea";
			cpnlInfoLabels.WorkingArea.Size = new System.Drawing.Size(173, 51);
			cpnlInfoLabels.WorkingArea.TabIndex = 1;
			// 
			// infoLabelsControl
			// 
			infoLabelsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			infoLabelsControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			infoLabelsControl.Location = new System.Drawing.Point(0, 0);
			infoLabelsControl.Name = "infoLabelsControl";
			infoLabelsControl.Size = new System.Drawing.Size(173, 51);
			infoLabelsControl.TabIndex = 0;
			// 
			// cpnlMisc
			// 
			cpnlMisc.Font = new System.Drawing.Font("Tahoma", 8.25F);
			cpnlMisc.HeightExpanded = 112;
			cpnlMisc.Location = new System.Drawing.Point(0, 721);
			cpnlMisc.Name = "cpnlMisc";
			cpnlMisc.Size = new System.Drawing.Size(189, 112);
			cpnlMisc.TabIndex = 7;
			cpnlMisc.Title = "Misc.";
			// 
			// 
			// 
			cpnlMisc.WorkingArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			cpnlMisc.WorkingArea.BackColor = System.Drawing.SystemColors.Control;
			cpnlMisc.WorkingArea.Controls.Add(miscControl);
			cpnlMisc.WorkingArea.Location = new System.Drawing.Point(4, 28);
			cpnlMisc.WorkingArea.Name = "WorkingArea";
			cpnlMisc.WorkingArea.Size = new System.Drawing.Size(173, 77);
			cpnlMisc.WorkingArea.TabIndex = 1;
			// 
			// miscControl
			// 
			miscControl.Dock = System.Windows.Forms.DockStyle.Fill;
			miscControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			miscControl.Location = new System.Drawing.Point(0, 0);
			miscControl.Name = "miscControl";
			miscControl.Size = new System.Drawing.Size(173, 77);
			miscControl.TabIndex = 0;
			// 
			// orbitPanel
			// 
			orbitPanel.BackColor = System.Drawing.Color.Black;
			orbitPanel.Controls.Add(scrollVert);
			orbitPanel.Controls.Add(scrollHorz);
			orbitPanel.Controls.Add(scrollZoom);
			orbitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			orbitPanel.Location = new System.Drawing.Point(192, 0);
			orbitPanel.MinimumSize = new System.Drawing.Size(682, 458);
			orbitPanel.Name = "orbitPanel";
			orbitPanel.Size = new System.Drawing.Size(742, 900);
			orbitPanel.TabIndex = 0;
			orbitPanel.MouseClick += orbitPanel_MouseClick;
			orbitPanel.MouseDoubleClick += orbitPanel_MouseDoubleClick;
			orbitPanel.MouseDown += orbitPanel_MouseDown;
			orbitPanel.MouseEnter += orbitPanel_MouseEnter;
			orbitPanel.MouseLeave += orbitPanel_MouseLeave;
			orbitPanel.MouseMove += orbitPanel_MouseMove;
			orbitPanel.PreviewKeyDown += orbitPanel_PreviewKeyDown;
			orbitPanel.Resize += orbitPanel_Resize;
			// 
			// scrollVert
			// 
			scrollVert.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			scrollVert.LargeChange = 1;
			scrollVert.Location = new System.Drawing.Point(708, 16);
			scrollVert.Maximum = 359;
			scrollVert.Name = "scrollVert";
			scrollVert.Size = new System.Drawing.Size(17, 272);
			scrollVert.TabIndex = 0;
			scrollVert.Visible = false;
			scrollVert.ValueChanged += scrollVert_ValueChanged;
			// 
			// scrollHorz
			// 
			scrollHorz.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			scrollHorz.LargeChange = 1;
			scrollHorz.Location = new System.Drawing.Point(432, 300);
			scrollHorz.Maximum = 359;
			scrollHorz.Name = "scrollHorz";
			scrollHorz.Size = new System.Drawing.Size(293, 17);
			scrollHorz.TabIndex = 1;
			scrollHorz.Visible = false;
			scrollHorz.ValueChanged += scrollHorz_ValueChanged;
			// 
			// scrollZoom
			// 
			scrollZoom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			scrollZoom.Location = new System.Drawing.Point(432, 327);
			scrollZoom.Maximum = 5000;
			scrollZoom.Minimum = 5;
			scrollZoom.Name = "scrollZoom";
			scrollZoom.Size = new System.Drawing.Size(293, 17);
			scrollZoom.TabIndex = 2;
			scrollZoom.Value = 5;
			scrollZoom.Visible = false;
			scrollZoom.ValueChanged += scrollZoom_ValueChanged;
			// 
			// OrbitViewerControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(orbitPanel);
			Controls.Add(pnlToolbox);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			MinimumSize = new System.Drawing.Size(720, 650);
			Name = "OrbitViewerControl";
			Size = new System.Drawing.Size(934, 900);
			pnlToolbox.ResumeLayout(false);
			cpnlComet.WorkingArea.ResumeLayout(false);
			cpnlMode.WorkingArea.ResumeLayout(false);
			cpnlDisplay.WorkingArea.ResumeLayout(false);
			cpnlDateTime.WorkingArea.ResumeLayout(false);
			cpnlSimulation.WorkingArea.ResumeLayout(false);
			cpnlFilterOnDate.WorkingArea.ResumeLayout(false);
			cpnlInfoLabels.WorkingArea.ResumeLayout(false);
			cpnlMisc.WorkingArea.ResumeLayout(false);
			orbitPanel.ResumeLayout(false);
			ResumeLayout(false);
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
