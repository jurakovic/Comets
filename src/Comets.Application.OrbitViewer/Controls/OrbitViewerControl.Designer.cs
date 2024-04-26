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
			pnlToolbox = new Panel();
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
			scrollVert = new VScrollBar();
			scrollHorz = new HScrollBar();
			scrollZoom = new HScrollBar();
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
			pnlToolbox.BorderStyle = BorderStyle.Fixed3D;
			pnlToolbox.Controls.Add(cpnlComet);
			pnlToolbox.Controls.Add(cpnlMode);
			pnlToolbox.Controls.Add(cpnlDisplay);
			pnlToolbox.Controls.Add(cpnlDateTime);
			pnlToolbox.Controls.Add(cpnlSimulation);
			pnlToolbox.Controls.Add(cpnlFilterOnDate);
			pnlToolbox.Controls.Add(cpnlInfoLabels);
			pnlToolbox.Controls.Add(cpnlMisc);
			pnlToolbox.Dock = DockStyle.Left;
			pnlToolbox.Location = new Point(0, 0);
			pnlToolbox.Name = "pnlToolbox";
			pnlToolbox.Size = new Size(192, 900);
			pnlToolbox.TabIndex = 0;
			// 
			// cpnlComet
			// 
			cpnlComet.Font = new Font("Tahoma", 8.25F);
			cpnlComet.HeightExpanded = 114;
			cpnlComet.Location = new Point(0, -1);
			cpnlComet.Name = "cpnlComet";
			cpnlComet.Size = new Size(189, 114);
			cpnlComet.TabIndex = 0;
			cpnlComet.Title = "Comet";
			// 
			// 
			// 
			cpnlComet.WorkingArea.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			cpnlComet.WorkingArea.BackColor = SystemColors.Control;
			cpnlComet.WorkingArea.Controls.Add(cometControl);
			cpnlComet.WorkingArea.Location = new Point(4, 28);
			cpnlComet.WorkingArea.Name = "WorkingArea";
			cpnlComet.WorkingArea.Size = new Size(173, 79);
			cpnlComet.WorkingArea.TabIndex = 1;
			// 
			// cometControl
			// 
			cometControl.Dock = DockStyle.Fill;
			cometControl.Font = new Font("Tahoma", 8.25F);
			cometControl.Location = new Point(0, 0);
			cometControl.Name = "cometControl";
			cometControl.Size = new Size(173, 79);
			cometControl.TabIndex = 0;
			// 
			// cpnlMode
			// 
			cpnlMode.Font = new Font("Tahoma", 8.25F);
			cpnlMode.HeightExpanded = 64;
			cpnlMode.Location = new Point(0, 109);
			cpnlMode.Name = "cpnlMode";
			cpnlMode.Size = new Size(189, 65);
			cpnlMode.TabIndex = 1;
			cpnlMode.Title = "Mode";
			// 
			// 
			// 
			cpnlMode.WorkingArea.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			cpnlMode.WorkingArea.BackColor = SystemColors.Control;
			cpnlMode.WorkingArea.Controls.Add(modeControl);
			cpnlMode.WorkingArea.Location = new Point(4, 28);
			cpnlMode.WorkingArea.Name = "WorkingArea";
			cpnlMode.WorkingArea.Size = new Size(173, 30);
			cpnlMode.WorkingArea.TabIndex = 1;
			// 
			// modeControl
			// 
			modeControl.Dock = DockStyle.Fill;
			modeControl.Location = new Point(0, 0);
			modeControl.Margin = new Padding(4, 3, 4, 3);
			modeControl.Name = "modeControl";
			modeControl.Size = new Size(173, 30);
			modeControl.TabIndex = 0;
			// 
			// cpnlDisplay
			// 
			cpnlDisplay.Font = new Font("Tahoma", 8.25F);
			cpnlDisplay.HeightExpanded = 191;
			cpnlDisplay.Location = new Point(0, 170);
			cpnlDisplay.Name = "cpnlDisplay";
			cpnlDisplay.Size = new Size(189, 191);
			cpnlDisplay.TabIndex = 2;
			cpnlDisplay.Title = "Display";
			// 
			// 
			// 
			cpnlDisplay.WorkingArea.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			cpnlDisplay.WorkingArea.BackColor = SystemColors.Control;
			cpnlDisplay.WorkingArea.Controls.Add(displayControl);
			cpnlDisplay.WorkingArea.Location = new Point(4, 28);
			cpnlDisplay.WorkingArea.Name = "WorkingArea";
			cpnlDisplay.WorkingArea.Size = new Size(173, 156);
			cpnlDisplay.WorkingArea.TabIndex = 1;
			// 
			// displayControl
			// 
			displayControl.Dock = DockStyle.Fill;
			displayControl.Font = new Font("Tahoma", 8.25F);
			displayControl.Location = new Point(0, 0);
			displayControl.Name = "displayControl";
			displayControl.Size = new Size(173, 156);
			displayControl.TabIndex = 0;
			// 
			// cpnlDateTime
			// 
			cpnlDateTime.Font = new Font("Tahoma", 8.25F);
			cpnlDateTime.HeightExpanded = 66;
			cpnlDateTime.Location = new Point(0, 357);
			cpnlDateTime.Name = "cpnlDateTime";
			cpnlDateTime.Size = new Size(189, 66);
			cpnlDateTime.TabIndex = 3;
			cpnlDateTime.Title = "Date and time";
			// 
			// 
			// 
			cpnlDateTime.WorkingArea.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			cpnlDateTime.WorkingArea.BackColor = SystemColors.Control;
			cpnlDateTime.WorkingArea.Controls.Add(dateTimeControl);
			cpnlDateTime.WorkingArea.Location = new Point(4, 28);
			cpnlDateTime.WorkingArea.Name = "WorkingArea";
			cpnlDateTime.WorkingArea.Size = new Size(173, 31);
			cpnlDateTime.WorkingArea.TabIndex = 1;
			// 
			// dateTimeControl
			// 
			dateTimeControl.Dock = DockStyle.Fill;
			dateTimeControl.Font = new Font("Tahoma", 8.25F);
			dateTimeControl.Location = new Point(0, 0);
			dateTimeControl.Name = "dateTimeControl";
			dateTimeControl.Size = new Size(173, 31);
			dateTimeControl.TabIndex = 0;
			// 
			// cpnlSimulation
			// 
			cpnlSimulation.Font = new Font("Tahoma", 8.25F);
			cpnlSimulation.HeightExpanded = 95;
			cpnlSimulation.Location = new Point(0, 419);
			cpnlSimulation.Name = "cpnlSimulation";
			cpnlSimulation.Size = new Size(189, 95);
			cpnlSimulation.TabIndex = 4;
			cpnlSimulation.Title = "Simulation";
			// 
			// 
			// 
			cpnlSimulation.WorkingArea.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			cpnlSimulation.WorkingArea.BackColor = SystemColors.Control;
			cpnlSimulation.WorkingArea.Controls.Add(simulationControl);
			cpnlSimulation.WorkingArea.Location = new Point(4, 28);
			cpnlSimulation.WorkingArea.Name = "WorkingArea";
			cpnlSimulation.WorkingArea.Size = new Size(173, 60);
			cpnlSimulation.WorkingArea.TabIndex = 1;
			// 
			// simulationControl
			// 
			simulationControl.Dock = DockStyle.Fill;
			simulationControl.Font = new Font("Tahoma", 8.25F);
			simulationControl.Location = new Point(0, 0);
			simulationControl.Name = "simulationControl";
			simulationControl.Size = new Size(173, 60);
			simulationControl.TabIndex = 0;
			// 
			// cpnlFilterOnDate
			// 
			cpnlFilterOnDate.Font = new Font("Tahoma", 8.25F);
			cpnlFilterOnDate.HeightExpanded = 133;
			cpnlFilterOnDate.Location = new Point(0, 510);
			cpnlFilterOnDate.Name = "cpnlFilterOnDate";
			cpnlFilterOnDate.Size = new Size(189, 133);
			cpnlFilterOnDate.TabIndex = 5;
			cpnlFilterOnDate.Title = "Filter on date";
			// 
			// 
			// 
			cpnlFilterOnDate.WorkingArea.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			cpnlFilterOnDate.WorkingArea.BackColor = SystemColors.Control;
			cpnlFilterOnDate.WorkingArea.Controls.Add(filterControl);
			cpnlFilterOnDate.WorkingArea.Location = new Point(4, 28);
			cpnlFilterOnDate.WorkingArea.Name = "WorkingArea";
			cpnlFilterOnDate.WorkingArea.Size = new Size(173, 98);
			cpnlFilterOnDate.WorkingArea.TabIndex = 1;
			// 
			// filterControl
			// 
			filterControl.Dock = DockStyle.Fill;
			filterControl.Font = new Font("Tahoma", 8.25F);
			filterControl.Location = new Point(0, 0);
			filterControl.Name = "filterControl";
			filterControl.Size = new Size(173, 98);
			filterControl.TabIndex = 0;
			// 
			// cpnlInfoLabels
			// 
			cpnlInfoLabels.Font = new Font("Tahoma", 8.25F);
			cpnlInfoLabels.HeightExpanded = 86;
			cpnlInfoLabels.Location = new Point(0, 639);
			cpnlInfoLabels.Name = "cpnlInfoLabels";
			cpnlInfoLabels.Size = new Size(189, 86);
			cpnlInfoLabels.TabIndex = 6;
			cpnlInfoLabels.Title = "Info labels";
			// 
			// 
			// 
			cpnlInfoLabels.WorkingArea.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			cpnlInfoLabels.WorkingArea.BackColor = SystemColors.Control;
			cpnlInfoLabels.WorkingArea.Controls.Add(infoLabelsControl);
			cpnlInfoLabels.WorkingArea.Location = new Point(4, 28);
			cpnlInfoLabels.WorkingArea.Name = "WorkingArea";
			cpnlInfoLabels.WorkingArea.Size = new Size(173, 51);
			cpnlInfoLabels.WorkingArea.TabIndex = 1;
			// 
			// infoLabelsControl
			// 
			infoLabelsControl.Dock = DockStyle.Fill;
			infoLabelsControl.Font = new Font("Tahoma", 8.25F);
			infoLabelsControl.Location = new Point(0, 0);
			infoLabelsControl.Name = "infoLabelsControl";
			infoLabelsControl.Size = new Size(173, 51);
			infoLabelsControl.TabIndex = 0;
			// 
			// cpnlMisc
			// 
			cpnlMisc.Font = new Font("Tahoma", 8.25F);
			cpnlMisc.HeightExpanded = 112;
			cpnlMisc.Location = new Point(0, 721);
			cpnlMisc.Name = "cpnlMisc";
			cpnlMisc.Size = new Size(189, 112);
			cpnlMisc.TabIndex = 7;
			cpnlMisc.Title = "Misc.";
			// 
			// 
			// 
			cpnlMisc.WorkingArea.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			cpnlMisc.WorkingArea.BackColor = SystemColors.Control;
			cpnlMisc.WorkingArea.Controls.Add(miscControl);
			cpnlMisc.WorkingArea.Location = new Point(4, 28);
			cpnlMisc.WorkingArea.Name = "WorkingArea";
			cpnlMisc.WorkingArea.Size = new Size(173, 77);
			cpnlMisc.WorkingArea.TabIndex = 1;
			// 
			// miscControl
			// 
			miscControl.Dock = DockStyle.Fill;
			miscControl.Font = new Font("Tahoma", 8.25F);
			miscControl.Location = new Point(0, 0);
			miscControl.Name = "miscControl";
			miscControl.Size = new Size(173, 77);
			miscControl.TabIndex = 0;
			// 
			// orbitPanel
			// 
			orbitPanel.BackColor = Color.Black;
			orbitPanel.Controls.Add(scrollVert);
			orbitPanel.Controls.Add(scrollHorz);
			orbitPanel.Controls.Add(scrollZoom);
			orbitPanel.Dock = DockStyle.Fill;
			orbitPanel.Location = new Point(192, 0);
			orbitPanel.MinimumSize = new Size(682, 458);
			orbitPanel.Name = "orbitPanel";
			orbitPanel.Size = new Size(742, 900);
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
			scrollVert.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			scrollVert.LargeChange = 1;
			scrollVert.Location = new Point(708, 16);
			scrollVert.Maximum = 359;
			scrollVert.Name = "scrollVert";
			scrollVert.Size = new Size(17, 272);
			scrollVert.TabIndex = 0;
			scrollVert.Visible = false;
			scrollVert.ValueChanged += scrollVert_ValueChanged;
			// 
			// scrollHorz
			// 
			scrollHorz.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			scrollHorz.LargeChange = 1;
			scrollHorz.Location = new Point(432, 300);
			scrollHorz.Maximum = 359;
			scrollHorz.Name = "scrollHorz";
			scrollHorz.Size = new Size(293, 17);
			scrollHorz.TabIndex = 1;
			scrollHorz.Visible = false;
			scrollHorz.ValueChanged += scrollHorz_ValueChanged;
			// 
			// scrollZoom
			// 
			scrollZoom.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			scrollZoom.Location = new Point(432, 327);
			scrollZoom.Maximum = 5000;
			scrollZoom.Minimum = 5;
			scrollZoom.Name = "scrollZoom";
			scrollZoom.Size = new Size(293, 17);
			scrollZoom.TabIndex = 2;
			scrollZoom.Value = 5;
			scrollZoom.Visible = false;
			scrollZoom.ValueChanged += scrollZoom_ValueChanged;
			// 
			// OrbitViewerControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(orbitPanel);
			Controls.Add(pnlToolbox);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			MinimumSize = new Size(720, 650);
			Name = "OrbitViewerControl";
			Size = new Size(934, 900);
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