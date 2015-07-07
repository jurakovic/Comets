namespace Comets.Application.ModulOrbit
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOrbitViewer));
			this.btnPerihDate = new System.Windows.Forms.Button();
			this.btnNow = new System.Windows.Forms.Button();
			this.lblTimestep = new System.Windows.Forms.Label();
			this.lblSimulation = new System.Windows.Forms.Label();
			this.lblDate = new System.Windows.Forms.Label();
			this.lblObject = new System.Windows.Forms.Label();
			this.cboObject = new System.Windows.Forms.ComboBox();
			this.lblOrbits = new System.Windows.Forms.Label();
			this.lblCenter = new System.Windows.Forms.Label();
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
			this.btnDate = new System.Windows.Forms.Button();
			this.lblZoom = new System.Windows.Forms.Label();
			this.orbitPanel = new Comets.OrbitViewer.OrbitPanel();
			this.pnlToolbox.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnPerihDate
			// 
			this.btnPerihDate.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnPerihDate.Location = new System.Drawing.Point(354, 43);
			this.btnPerihDate.Name = "btnPerihDate";
			this.btnPerihDate.Size = new System.Drawing.Size(79, 23);
			this.btnPerihDate.TabIndex = 5;
			this.btnPerihDate.Text = "Perih. Date";
			this.btnPerihDate.UseVisualStyleBackColor = true;
			this.btnPerihDate.Click += new System.EventHandler(this.btnPerihDate_Click);
			// 
			// btnNow
			// 
			this.btnNow.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnNow.Location = new System.Drawing.Point(268, 43);
			this.btnNow.Name = "btnNow";
			this.btnNow.Size = new System.Drawing.Size(79, 23);
			this.btnNow.TabIndex = 4;
			this.btnNow.Text = "Now";
			this.btnNow.UseVisualStyleBackColor = true;
			this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
			// 
			// lblTimestep
			// 
			this.lblTimestep.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblTimestep.AutoSize = true;
			this.lblTimestep.Location = new System.Drawing.Point(457, 48);
			this.lblTimestep.Name = "lblTimestep";
			this.lblTimestep.Size = new System.Drawing.Size(50, 13);
			this.lblTimestep.TabIndex = 60;
			this.lblTimestep.Text = "Timestep";
			// 
			// lblSimulation
			// 
			this.lblSimulation.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblSimulation.AutoSize = true;
			this.lblSimulation.Location = new System.Drawing.Point(457, 16);
			this.lblSimulation.Name = "lblSimulation";
			this.lblSimulation.Size = new System.Drawing.Size(55, 13);
			this.lblSimulation.TabIndex = 59;
			this.lblSimulation.Text = "Simulation";
			// 
			// lblDate
			// 
			this.lblDate.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblDate.AutoSize = true;
			this.lblDate.Location = new System.Drawing.Point(232, 15);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(30, 13);
			this.lblDate.TabIndex = 57;
			this.lblDate.Text = "Date";
			// 
			// lblObject
			// 
			this.lblObject.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblObject.AutoSize = true;
			this.lblObject.Location = new System.Drawing.Point(5, 16);
			this.lblObject.Name = "lblObject";
			this.lblObject.Size = new System.Drawing.Size(39, 13);
			this.lblObject.TabIndex = 56;
			this.lblObject.Text = "Object";
			// 
			// cboObject
			// 
			this.cboObject.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cboObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboObject.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.cboObject.FormattingEnabled = true;
			this.cboObject.IntegralHeight = false;
			this.cboObject.Location = new System.Drawing.Point(50, 12);
			this.cboObject.MaxDropDownItems = 15;
			this.cboObject.Name = "cboObject";
			this.cboObject.Size = new System.Drawing.Size(162, 21);
			this.cboObject.TabIndex = 0;
			this.cboObject.SelectedIndexChanged += new System.EventHandler(this.cboObject_SelectedIndexChanged);
			// 
			// lblOrbits
			// 
			this.lblOrbits.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblOrbits.AutoSize = true;
			this.lblOrbits.Location = new System.Drawing.Point(5, 80);
			this.lblOrbits.Name = "lblOrbits";
			this.lblOrbits.Size = new System.Drawing.Size(36, 13);
			this.lblOrbits.TabIndex = 51;
			this.lblOrbits.Text = "Orbits";
			// 
			// lblCenter
			// 
			this.lblCenter.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblCenter.AutoSize = true;
			this.lblCenter.Location = new System.Drawing.Point(5, 48);
			this.lblCenter.Name = "lblCenter";
			this.lblCenter.Size = new System.Drawing.Size(40, 13);
			this.lblCenter.TabIndex = 50;
			this.lblCenter.Text = "Center";
			// 
			// cboOrbits
			// 
			this.cboOrbits.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cboOrbits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOrbits.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.cboOrbits.FormattingEnabled = true;
			this.cboOrbits.Location = new System.Drawing.Point(50, 76);
			this.cboOrbits.Name = "cboOrbits";
			this.cboOrbits.Size = new System.Drawing.Size(162, 21);
			this.cboOrbits.TabIndex = 2;
			this.cboOrbits.SelectedIndexChanged += new System.EventHandler(this.cboOrbits_SelectedIndexChanged);
			// 
			// cboCenter
			// 
			this.cboCenter.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cboCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCenter.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.cboCenter.FormattingEnabled = true;
			this.cboCenter.Location = new System.Drawing.Point(50, 44);
			this.cboCenter.Name = "cboCenter";
			this.cboCenter.Size = new System.Drawing.Size(162, 21);
			this.cboCenter.TabIndex = 1;
			this.cboCenter.SelectedIndexChanged += new System.EventHandler(this.cboCenter_SelectedIndexChanged);
			// 
			// cboTimestep
			// 
			this.cboTimestep.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cboTimestep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTimestep.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.cboTimestep.FormattingEnabled = true;
			this.cboTimestep.Location = new System.Drawing.Point(518, 44);
			this.cboTimestep.Name = "cboTimestep";
			this.cboTimestep.Size = new System.Drawing.Size(161, 21);
			this.cboTimestep.TabIndex = 11;
			this.cboTimestep.SelectedIndexChanged += new System.EventHandler(this.cboTimestep_SelectedIndexChanged);
			// 
			// btnForPlay
			// 
			this.btnForPlay.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnForPlay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnForPlay.Location = new System.Drawing.Point(650, 11);
			this.btnForPlay.Name = "btnForPlay";
			this.btnForPlay.Size = new System.Drawing.Size(31, 23);
			this.btnForPlay.TabIndex = 10;
			this.btnForPlay.Text = ">>";
			this.btnForPlay.UseVisualStyleBackColor = true;
			this.btnForPlay.Click += new System.EventHandler(this.btnForPlay_Click);
			// 
			// btnForStep
			// 
			this.btnForStep.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnForStep.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnForStep.Location = new System.Drawing.Point(617, 11);
			this.btnForStep.Name = "btnForStep";
			this.btnForStep.Size = new System.Drawing.Size(31, 23);
			this.btnForStep.TabIndex = 9;
			this.btnForStep.Text = ">|";
			this.btnForStep.UseVisualStyleBackColor = true;
			this.btnForStep.Click += new System.EventHandler(this.btnForStep_Click);
			// 
			// btnStop
			// 
			this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnStop.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnStop.Location = new System.Drawing.Point(584, 11);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(31, 23);
			this.btnStop.TabIndex = 8;
			this.btnStop.Text = "||";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnRevStep
			// 
			this.btnRevStep.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnRevStep.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnRevStep.Location = new System.Drawing.Point(551, 11);
			this.btnRevStep.Name = "btnRevStep";
			this.btnRevStep.Size = new System.Drawing.Size(31, 23);
			this.btnRevStep.TabIndex = 7;
			this.btnRevStep.Text = "|<";
			this.btnRevStep.UseVisualStyleBackColor = true;
			this.btnRevStep.Click += new System.EventHandler(this.btnRevStep_Click);
			// 
			// btnRevPlay
			// 
			this.btnRevPlay.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnRevPlay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnRevPlay.Location = new System.Drawing.Point(518, 11);
			this.btnRevPlay.Name = "btnRevPlay";
			this.btnRevPlay.Size = new System.Drawing.Size(31, 23);
			this.btnRevPlay.TabIndex = 6;
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
			this.scrollVert.TabIndex = 1;
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
			this.scrollHorz.TabIndex = 2;
			this.scrollHorz.ValueChanged += new System.EventHandler(this.scrollHorz_ValueChanged);
			// 
			// scrollZoom
			// 
			this.scrollZoom.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.scrollZoom.Location = new System.Drawing.Point(268, 77);
			this.scrollZoom.Maximum = 1000;
			this.scrollZoom.Minimum = 5;
			this.scrollZoom.Name = "scrollZoom";
			this.scrollZoom.Size = new System.Drawing.Size(411, 17);
			this.scrollZoom.TabIndex = 12;
			this.scrollZoom.Value = 5;
			this.scrollZoom.ValueChanged += new System.EventHandler(this.scrollZoom_ValueChanged);
			// 
			// pnlToolbox
			// 
			this.pnlToolbox.Controls.Add(this.btnDate);
			this.pnlToolbox.Controls.Add(this.lblZoom);
			this.pnlToolbox.Controls.Add(this.scrollZoom);
			this.pnlToolbox.Controls.Add(this.btnPerihDate);
			this.pnlToolbox.Controls.Add(this.btnNow);
			this.pnlToolbox.Controls.Add(this.btnRevPlay);
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
			this.pnlToolbox.Controls.Add(this.lblOrbits);
			this.pnlToolbox.Controls.Add(this.lblCenter);
			this.pnlToolbox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlToolbox.Location = new System.Drawing.Point(0, 486);
			this.pnlToolbox.Name = "pnlToolbox";
			this.pnlToolbox.Size = new System.Drawing.Size(703, 111);
			this.pnlToolbox.TabIndex = 3;
			// 
			// btnDate
			// 
			this.btnDate.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnDate.Location = new System.Drawing.Point(268, 11);
			this.btnDate.Name = "btnDate";
			this.btnDate.Size = new System.Drawing.Size(165, 23);
			this.btnDate.TabIndex = 3;
			this.btnDate.Text = "dd.MM.yyyy HH:mm:ss";
			this.btnDate.UseVisualStyleBackColor = true;
			this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
			// 
			// lblZoom
			// 
			this.lblZoom.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblZoom.AutoSize = true;
			this.lblZoom.Location = new System.Drawing.Point(232, 79);
			this.lblZoom.Name = "lblZoom";
			this.lblZoom.Size = new System.Drawing.Size(33, 13);
			this.lblZoom.TabIndex = 67;
			this.lblZoom.Text = "Zoom";
			// 
			// orbitPanel
			// 
			this.orbitPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.orbitPanel.Antiliasing = false;
			this.orbitPanel.ATime = null;
			this.orbitPanel.BackColor = System.Drawing.Color.Black;
			this.orbitPanel.CenterObjectSelected = 0;
			this.orbitPanel.Location = new System.Drawing.Point(0, 0);
			this.orbitPanel.MinimumSize = new System.Drawing.Size(682, 458);
			this.orbitPanel.MultipleMode = false;
			this.orbitPanel.Name = "orbitPanel";
			this.orbitPanel.Offscreen = null;
			this.orbitPanel.RotateHorz = 0D;
			this.orbitPanel.RotateVert = 0D;
			this.orbitPanel.SelectedIndex = 0;
			this.orbitPanel.ShowAxes = false;
			this.orbitPanel.ShowCometName = false;
			this.orbitPanel.ShowDate = false;
			this.orbitPanel.ShowDistance = false;
			this.orbitPanel.ShowMagnitude = false;
			this.orbitPanel.ShowPlanetName = false;
			this.orbitPanel.Size = new System.Drawing.Size(682, 458);
			this.orbitPanel.TabIndex = 0;
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
			this.ClientSize = new System.Drawing.Size(703, 597);
			this.Controls.Add(this.pnlToolbox);
			this.Controls.Add(this.orbitPanel);
			this.Controls.Add(this.scrollVert);
			this.Controls.Add(this.scrollHorz);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(719, 636);
			this.Name = "FormOrbitViewer";
			this.Text = "Orbit Viewer";
			this.Activated += new System.EventHandler(this.FormOrbitViewer_Activated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOrbitViewer_FormClosing);
			this.Load += new System.EventHandler(this.FormOrbit_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOrbitViewer_KeyDown);
			this.pnlToolbox.ResumeLayout(false);
			this.pnlToolbox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnPerihDate;
		private System.Windows.Forms.Button btnNow;
		private System.Windows.Forms.Label lblTimestep;
		private System.Windows.Forms.Label lblSimulation;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.Label lblObject;
		private System.Windows.Forms.ComboBox cboObject;
		private System.Windows.Forms.Label lblOrbits;
		private System.Windows.Forms.Label lblCenter;
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
		private System.Windows.Forms.Label lblZoom;
		private System.Windows.Forms.Button btnDate;

	}
}