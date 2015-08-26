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
			this.cboComet = new System.Windows.Forms.ComboBox();
			this.cboTimestep = new System.Windows.Forms.ComboBox();
			this.btnForPlay = new System.Windows.Forms.Button();
			this.btnForStep = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnRevStep = new System.Windows.Forms.Button();
			this.btnRevPlay = new System.Windows.Forms.Button();
			this.pnlToolbox = new System.Windows.Forms.Panel();
			this.gbxMisc = new System.Windows.Forms.GroupBox();
			this.btnSaveImage = new System.Windows.Forms.Button();
			this.cbxAntialiasing = new System.Windows.Forms.CheckBox();
			this.cbxShowAxes = new System.Windows.Forms.CheckBox();
			this.gbxInfoLabels = new System.Windows.Forms.GroupBox();
			this.cbxDateTime = new System.Windows.Forms.CheckBox();
			this.cbxMagDist = new System.Windows.Forms.CheckBox();
			this.gbxSimulation = new System.Windows.Forms.GroupBox();
			this.gbxDateTime = new System.Windows.Forms.GroupBox();
			this.btnDate = new System.Windows.Forms.Button();
			this.gbxOrbitsLabelsCenter = new System.Windows.Forms.GroupBox();
			this.cbxSelectedLabel = new System.Windows.Forms.CheckBox();
			this.cbxSelectedOrbit = new System.Windows.Forms.CheckBox();
			this.btnNoLabels = new System.Windows.Forms.Button();
			this.cbxMarker = new System.Windows.Forms.CheckBox();
			this.btnNoOrbits = new System.Windows.Forms.Button();
			this.btnAllLabels = new System.Windows.Forms.Button();
			this.btnAllOrbits = new System.Windows.Forms.Button();
			this.lblComet = new System.Windows.Forms.Label();
			this.lblNeptune = new System.Windows.Forms.Label();
			this.lblUranus = new System.Windows.Forms.Label();
			this.lblSaturn = new System.Windows.Forms.Label();
			this.lblJupiter = new System.Windows.Forms.Label();
			this.lblMars = new System.Windows.Forms.Label();
			this.lblEarth = new System.Windows.Forms.Label();
			this.lblVenus = new System.Windows.Forms.Label();
			this.lblMercury = new System.Windows.Forms.Label();
			this.lblSun = new System.Windows.Forms.Label();
			this.btnAllOrbitsLabels = new System.Windows.Forms.Button();
			this.btnDefaultOrbitsLabels = new System.Windows.Forms.Button();
			this.rbtnCenterMercury = new System.Windows.Forms.RadioButton();
			this.cbxLabelComet = new System.Windows.Forms.CheckBox();
			this.cbxOrbitComet = new System.Windows.Forms.CheckBox();
			this.rbtnCenterSun = new System.Windows.Forms.RadioButton();
			this.cbxLabelNeptune = new System.Windows.Forms.CheckBox();
			this.cbxLabelUranus = new System.Windows.Forms.CheckBox();
			this.rbtnCenterComet = new System.Windows.Forms.RadioButton();
			this.cbxOrbitNeptune = new System.Windows.Forms.CheckBox();
			this.cbxOrbitUranus = new System.Windows.Forms.CheckBox();
			this.rbtnCenterNeptune = new System.Windows.Forms.RadioButton();
			this.cbxLabelSaturn = new System.Windows.Forms.CheckBox();
			this.cbxLabelJupiter = new System.Windows.Forms.CheckBox();
			this.rbtnCenterUranus = new System.Windows.Forms.RadioButton();
			this.cbxOrbitSaturn = new System.Windows.Forms.CheckBox();
			this.cbxOrbitJupiter = new System.Windows.Forms.CheckBox();
			this.rbtnCenterSaturn = new System.Windows.Forms.RadioButton();
			this.cbxLabelMars = new System.Windows.Forms.CheckBox();
			this.cbxLabelEarth = new System.Windows.Forms.CheckBox();
			this.rbtnCenterJupiter = new System.Windows.Forms.RadioButton();
			this.cbxOrbitMars = new System.Windows.Forms.CheckBox();
			this.cbxOrbitEarth = new System.Windows.Forms.CheckBox();
			this.rbtnCenterMars = new System.Windows.Forms.RadioButton();
			this.cbxLabelVenus = new System.Windows.Forms.CheckBox();
			this.cbxLabelMercury = new System.Windows.Forms.CheckBox();
			this.rbtnCenterVenus = new System.Windows.Forms.RadioButton();
			this.rbtnCenterEarth = new System.Windows.Forms.RadioButton();
			this.cbxOrbitVenus = new System.Windows.Forms.CheckBox();
			this.cbxOrbitMercury = new System.Windows.Forms.CheckBox();
			this.gbxMode = new System.Windows.Forms.GroupBox();
			this.rbtnMultipleMode = new System.Windows.Forms.RadioButton();
			this.rbtnSingleMode = new System.Windows.Forms.RadioButton();
			this.gbxComet = new System.Windows.Forms.GroupBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnAll = new System.Windows.Forms.Button();
			this.btnFilter = new System.Windows.Forms.Button();
			this.orbitPanel = new Comets.OrbitViewer.OrbitPanel();
			this.scrollVert = new System.Windows.Forms.VScrollBar();
			this.scrollHorz = new System.Windows.Forms.HScrollBar();
			this.scrollZoom = new System.Windows.Forms.HScrollBar();
			this.pnlToolbox.SuspendLayout();
			this.gbxMisc.SuspendLayout();
			this.gbxInfoLabels.SuspendLayout();
			this.gbxSimulation.SuspendLayout();
			this.gbxDateTime.SuspendLayout();
			this.gbxOrbitsLabelsCenter.SuspendLayout();
			this.gbxMode.SuspendLayout();
			this.gbxComet.SuspendLayout();
			this.orbitPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnPerihDate
			// 
			this.btnPerihDate.Location = new System.Drawing.Point(94, 47);
			this.btnPerihDate.Name = "btnPerihDate";
			this.btnPerihDate.Size = new System.Drawing.Size(79, 23);
			this.btnPerihDate.TabIndex = 2;
			this.btnPerihDate.Text = "Perih. Date";
			this.btnPerihDate.UseVisualStyleBackColor = true;
			this.btnPerihDate.Click += new System.EventHandler(this.btnPerihDate_Click);
			// 
			// btnNow
			// 
			this.btnNow.Location = new System.Drawing.Point(8, 47);
			this.btnNow.Name = "btnNow";
			this.btnNow.Size = new System.Drawing.Size(79, 23);
			this.btnNow.TabIndex = 1;
			this.btnNow.Text = "Now";
			this.btnNow.UseVisualStyleBackColor = true;
			this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
			// 
			// cboComet
			// 
			this.cboComet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboComet.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.cboComet.FormattingEnabled = true;
			this.cboComet.IntegralHeight = false;
			this.cboComet.Location = new System.Drawing.Point(8, 18);
			this.cboComet.MaxDropDownItems = 17;
			this.cboComet.Name = "cboComet";
			this.cboComet.Size = new System.Drawing.Size(165, 21);
			this.cboComet.TabIndex = 0;
			this.cboComet.SelectedIndexChanged += new System.EventHandler(this.cboObject_SelectedIndexChanged);
			this.cboComet.MouseHover += new System.EventHandler(this.comboBoxCommon_MouseHover);
			// 
			// cboTimestep
			// 
			this.cboTimestep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTimestep.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.cboTimestep.FormattingEnabled = true;
			this.cboTimestep.Location = new System.Drawing.Point(8, 53);
			this.cboTimestep.Name = "cboTimestep";
			this.cboTimestep.Size = new System.Drawing.Size(163, 21);
			this.cboTimestep.TabIndex = 5;
			this.cboTimestep.SelectedIndexChanged += new System.EventHandler(this.cboTimestep_SelectedIndexChanged);
			this.cboTimestep.MouseHover += new System.EventHandler(this.comboBoxCommon_MouseHover);
			// 
			// btnForPlay
			// 
			this.btnForPlay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnForPlay.Location = new System.Drawing.Point(140, 20);
			this.btnForPlay.Name = "btnForPlay";
			this.btnForPlay.Size = new System.Drawing.Size(31, 23);
			this.btnForPlay.TabIndex = 4;
			this.btnForPlay.Text = ">>";
			this.btnForPlay.UseVisualStyleBackColor = true;
			this.btnForPlay.Click += new System.EventHandler(this.btnForPlay_Click);
			// 
			// btnForStep
			// 
			this.btnForStep.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnForStep.Location = new System.Drawing.Point(107, 20);
			this.btnForStep.Name = "btnForStep";
			this.btnForStep.Size = new System.Drawing.Size(31, 23);
			this.btnForStep.TabIndex = 3;
			this.btnForStep.Text = ">|";
			this.btnForStep.UseVisualStyleBackColor = true;
			this.btnForStep.Click += new System.EventHandler(this.btnForStep_Click);
			// 
			// btnStop
			// 
			this.btnStop.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnStop.Location = new System.Drawing.Point(74, 20);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(31, 23);
			this.btnStop.TabIndex = 2;
			this.btnStop.Text = "||";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnRevStep
			// 
			this.btnRevStep.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnRevStep.Location = new System.Drawing.Point(41, 20);
			this.btnRevStep.Name = "btnRevStep";
			this.btnRevStep.Size = new System.Drawing.Size(31, 23);
			this.btnRevStep.TabIndex = 1;
			this.btnRevStep.Text = "|<";
			this.btnRevStep.UseVisualStyleBackColor = true;
			this.btnRevStep.Click += new System.EventHandler(this.btnRevStep_Click);
			// 
			// btnRevPlay
			// 
			this.btnRevPlay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnRevPlay.Location = new System.Drawing.Point(8, 20);
			this.btnRevPlay.Name = "btnRevPlay";
			this.btnRevPlay.Size = new System.Drawing.Size(31, 23);
			this.btnRevPlay.TabIndex = 0;
			this.btnRevPlay.Text = "<<";
			this.btnRevPlay.UseVisualStyleBackColor = true;
			this.btnRevPlay.Click += new System.EventHandler(this.btnRevPlay_Click);
			// 
			// pnlToolbox
			// 
			this.pnlToolbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlToolbox.Controls.Add(this.gbxMisc);
			this.pnlToolbox.Controls.Add(this.gbxInfoLabels);
			this.pnlToolbox.Controls.Add(this.gbxSimulation);
			this.pnlToolbox.Controls.Add(this.gbxDateTime);
			this.pnlToolbox.Controls.Add(this.gbxOrbitsLabelsCenter);
			this.pnlToolbox.Controls.Add(this.gbxMode);
			this.pnlToolbox.Controls.Add(this.gbxComet);
			this.pnlToolbox.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlToolbox.Location = new System.Drawing.Point(0, 0);
			this.pnlToolbox.Name = "pnlToolbox";
			this.pnlToolbox.Size = new System.Drawing.Size(195, 682);
			this.pnlToolbox.TabIndex = 0;
			// 
			// gbxMisc
			// 
			this.gbxMisc.Controls.Add(this.btnSaveImage);
			this.gbxMisc.Controls.Add(this.cbxAntialiasing);
			this.gbxMisc.Controls.Add(this.cbxShowAxes);
			this.gbxMisc.Location = new System.Drawing.Point(5, 557);
			this.gbxMisc.Name = "gbxMisc";
			this.gbxMisc.Size = new System.Drawing.Size(181, 99);
			this.gbxMisc.TabIndex = 6;
			this.gbxMisc.TabStop = false;
			this.gbxMisc.Text = "Misc.";
			// 
			// btnSaveImage
			// 
			this.btnSaveImage.Location = new System.Drawing.Point(8, 65);
			this.btnSaveImage.Name = "btnSaveImage";
			this.btnSaveImage.Size = new System.Drawing.Size(165, 23);
			this.btnSaveImage.TabIndex = 2;
			this.btnSaveImage.Text = "Save image";
			this.btnSaveImage.UseVisualStyleBackColor = true;
			this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
			// 
			// cbxAntialiasing
			// 
			this.cbxAntialiasing.AutoSize = true;
			this.cbxAntialiasing.Location = new System.Drawing.Point(9, 43);
			this.cbxAntialiasing.Name = "cbxAntialiasing";
			this.cbxAntialiasing.Size = new System.Drawing.Size(80, 17);
			this.cbxAntialiasing.TabIndex = 1;
			this.cbxAntialiasing.Text = "Antialiasing";
			this.cbxAntialiasing.UseVisualStyleBackColor = true;
			this.cbxAntialiasing.CheckedChanged += new System.EventHandler(this.cbxAntialiasing_CheckedChanged);
			// 
			// cbxShowAxes
			// 
			this.cbxShowAxes.AutoSize = true;
			this.cbxShowAxes.Location = new System.Drawing.Point(9, 20);
			this.cbxShowAxes.Name = "cbxShowAxes";
			this.cbxShowAxes.Size = new System.Drawing.Size(78, 17);
			this.cbxShowAxes.TabIndex = 0;
			this.cbxShowAxes.Text = "Show axes";
			this.cbxShowAxes.UseVisualStyleBackColor = true;
			this.cbxShowAxes.CheckedChanged += new System.EventHandler(this.cbxShowAxes_CheckedChanged);
			// 
			// gbxInfoLabels
			// 
			this.gbxInfoLabels.Controls.Add(this.cbxDateTime);
			this.gbxInfoLabels.Controls.Add(this.cbxMagDist);
			this.gbxInfoLabels.Location = new System.Drawing.Point(5, 486);
			this.gbxInfoLabels.Name = "gbxInfoLabels";
			this.gbxInfoLabels.Size = new System.Drawing.Size(181, 70);
			this.gbxInfoLabels.TabIndex = 5;
			this.gbxInfoLabels.TabStop = false;
			this.gbxInfoLabels.Text = "Info labels";
			// 
			// cbxDateTime
			// 
			this.cbxDateTime.AutoSize = true;
			this.cbxDateTime.Checked = true;
			this.cbxDateTime.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxDateTime.Location = new System.Drawing.Point(9, 43);
			this.cbxDateTime.Name = "cbxDateTime";
			this.cbxDateTime.Size = new System.Drawing.Size(95, 17);
			this.cbxDateTime.TabIndex = 1;
			this.cbxDateTime.Text = "Date and Time";
			this.cbxDateTime.UseVisualStyleBackColor = true;
			this.cbxDateTime.CheckedChanged += new System.EventHandler(this.cbxDateTime_CheckedChanged);
			// 
			// cbxMagDist
			// 
			this.cbxMagDist.AutoSize = true;
			this.cbxMagDist.Checked = true;
			this.cbxMagDist.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxMagDist.Location = new System.Drawing.Point(9, 20);
			this.cbxMagDist.Name = "cbxMagDist";
			this.cbxMagDist.Size = new System.Drawing.Size(145, 17);
			this.cbxMagDist.TabIndex = 0;
			this.cbxMagDist.Text = "Magnitude and distances";
			this.cbxMagDist.UseVisualStyleBackColor = true;
			this.cbxMagDist.CheckedChanged += new System.EventHandler(this.cbxMagDist_CheckedChanged);
			// 
			// gbxSimulation
			// 
			this.gbxSimulation.Controls.Add(this.btnRevPlay);
			this.gbxSimulation.Controls.Add(this.cboTimestep);
			this.gbxSimulation.Controls.Add(this.btnForPlay);
			this.gbxSimulation.Controls.Add(this.btnForStep);
			this.gbxSimulation.Controls.Add(this.btnStop);
			this.gbxSimulation.Controls.Add(this.btnRevStep);
			this.gbxSimulation.Location = new System.Drawing.Point(5, 401);
			this.gbxSimulation.Name = "gbxSimulation";
			this.gbxSimulation.Size = new System.Drawing.Size(181, 84);
			this.gbxSimulation.TabIndex = 4;
			this.gbxSimulation.TabStop = false;
			this.gbxSimulation.Text = "Simulation";
			// 
			// gbxDateTime
			// 
			this.gbxDateTime.Controls.Add(this.btnDate);
			this.gbxDateTime.Controls.Add(this.btnNow);
			this.gbxDateTime.Controls.Add(this.btnPerihDate);
			this.gbxDateTime.Location = new System.Drawing.Point(5, 322);
			this.gbxDateTime.Name = "gbxDateTime";
			this.gbxDateTime.Size = new System.Drawing.Size(181, 78);
			this.gbxDateTime.TabIndex = 3;
			this.gbxDateTime.TabStop = false;
			this.gbxDateTime.Text = "Date and Time";
			// 
			// btnDate
			// 
			this.btnDate.Location = new System.Drawing.Point(8, 18);
			this.btnDate.Name = "btnDate";
			this.btnDate.Size = new System.Drawing.Size(165, 23);
			this.btnDate.TabIndex = 0;
			this.btnDate.Text = "dd.MM.yyyy HH:mm:ss";
			this.btnDate.UseVisualStyleBackColor = true;
			this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
			// 
			// gbxOrbitsLabelsCenter
			// 
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxSelectedLabel);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxSelectedOrbit);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.btnNoLabels);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxMarker);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.btnNoOrbits);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.btnAllLabels);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.btnAllOrbits);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.lblComet);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.lblNeptune);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.lblUranus);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.lblSaturn);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.lblJupiter);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.lblMars);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.lblEarth);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.lblVenus);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.lblMercury);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.lblSun);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.btnAllOrbitsLabels);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.btnDefaultOrbitsLabels);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.rbtnCenterMercury);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxLabelComet);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxOrbitComet);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.rbtnCenterSun);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxLabelNeptune);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxLabelUranus);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.rbtnCenterComet);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxOrbitNeptune);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxOrbitUranus);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.rbtnCenterNeptune);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxLabelSaturn);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxLabelJupiter);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.rbtnCenterUranus);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxOrbitSaturn);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxOrbitJupiter);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.rbtnCenterSaturn);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxLabelMars);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxLabelEarth);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.rbtnCenterJupiter);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxOrbitMars);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxOrbitEarth);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.rbtnCenterMars);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxLabelVenus);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxLabelMercury);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.rbtnCenterVenus);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.rbtnCenterEarth);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxOrbitVenus);
			this.gbxOrbitsLabelsCenter.Controls.Add(this.cbxOrbitMercury);
			this.gbxOrbitsLabelsCenter.Location = new System.Drawing.Point(5, 123);
			this.gbxOrbitsLabelsCenter.Name = "gbxOrbitsLabelsCenter";
			this.gbxOrbitsLabelsCenter.Size = new System.Drawing.Size(181, 198);
			this.gbxOrbitsLabelsCenter.TabIndex = 2;
			this.gbxOrbitsLabelsCenter.TabStop = false;
			this.gbxOrbitsLabelsCenter.Text = "Orbits - Labels - Center";
			// 
			// cbxSelectedLabel
			// 
			this.cbxSelectedLabel.AutoSize = true;
			this.cbxSelectedLabel.Checked = true;
			this.cbxSelectedLabel.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxSelectedLabel.Location = new System.Drawing.Point(8, 150);
			this.cbxSelectedLabel.Name = "cbxSelectedLabel";
			this.cbxSelectedLabel.Size = new System.Drawing.Size(124, 17);
			this.cbxSelectedLabel.TabIndex = 0;
			this.cbxSelectedLabel.Text = "Selected comet label";
			this.cbxSelectedLabel.UseVisualStyleBackColor = true;
			this.cbxSelectedLabel.CheckedChanged += new System.EventHandler(this.cbxLabel_CheckedChanged);
			// 
			// cbxSelectedOrbit
			// 
			this.cbxSelectedOrbit.AutoSize = true;
			this.cbxSelectedOrbit.Checked = true;
			this.cbxSelectedOrbit.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxSelectedOrbit.Location = new System.Drawing.Point(8, 127);
			this.cbxSelectedOrbit.Name = "cbxSelectedOrbit";
			this.cbxSelectedOrbit.Size = new System.Drawing.Size(124, 17);
			this.cbxSelectedOrbit.TabIndex = 3;
			this.cbxSelectedOrbit.Text = "Selected comet orbit";
			this.cbxSelectedOrbit.UseVisualStyleBackColor = true;
			this.cbxSelectedOrbit.CheckedChanged += new System.EventHandler(this.cbxOrbit_CheckedChanged);
			// 
			// btnNoLabels
			// 
			this.btnNoLabels.Location = new System.Drawing.Point(8, 36);
			this.btnNoLabels.Name = "btnNoLabels";
			this.btnNoLabels.Size = new System.Drawing.Size(15, 15);
			this.btnNoLabels.TabIndex = 11;
			this.btnNoLabels.UseVisualStyleBackColor = true;
			this.btnNoLabels.Click += new System.EventHandler(this.btnNoLabels_Click);
			// 
			// cbxMarker
			// 
			this.cbxMarker.AutoSize = true;
			this.cbxMarker.Checked = true;
			this.cbxMarker.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxMarker.Location = new System.Drawing.Point(8, 173);
			this.cbxMarker.Name = "cbxMarker";
			this.cbxMarker.Size = new System.Drawing.Size(59, 17);
			this.cbxMarker.TabIndex = 2;
			this.cbxMarker.Text = "Marker";
			this.cbxMarker.UseVisualStyleBackColor = true;
			this.cbxMarker.CheckedChanged += new System.EventHandler(this.cbxMarker_CheckedChanged);
			// 
			// btnNoOrbits
			// 
			this.btnNoOrbits.Location = new System.Drawing.Point(8, 21);
			this.btnNoOrbits.Name = "btnNoOrbits";
			this.btnNoOrbits.Size = new System.Drawing.Size(15, 15);
			this.btnNoOrbits.TabIndex = 0;
			this.btnNoOrbits.UseVisualStyleBackColor = true;
			this.btnNoOrbits.Click += new System.EventHandler(this.btnNoOrbits_Click);
			// 
			// btnAllLabels
			// 
			this.btnAllLabels.Location = new System.Drawing.Point(158, 36);
			this.btnAllLabels.Name = "btnAllLabels";
			this.btnAllLabels.Size = new System.Drawing.Size(15, 15);
			this.btnAllLabels.TabIndex = 21;
			this.btnAllLabels.UseVisualStyleBackColor = true;
			this.btnAllLabels.Click += new System.EventHandler(this.btnAllLabels_Click);
			// 
			// btnAllOrbits
			// 
			this.btnAllOrbits.Location = new System.Drawing.Point(158, 21);
			this.btnAllOrbits.Name = "btnAllOrbits";
			this.btnAllOrbits.Size = new System.Drawing.Size(15, 15);
			this.btnAllOrbits.TabIndex = 10;
			this.btnAllOrbits.UseVisualStyleBackColor = true;
			this.btnAllOrbits.Click += new System.EventHandler(this.btnAllOrbits_Click);
			// 
			// lblComet
			// 
			this.lblComet.AutoSize = true;
			this.lblComet.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblComet.Location = new System.Drawing.Point(144, 71);
			this.lblComet.Name = "lblComet";
			this.lblComet.Size = new System.Drawing.Size(14, 13);
			this.lblComet.TabIndex = 39;
			this.lblComet.Text = "C";
			// 
			// lblNeptune
			// 
			this.lblNeptune.AutoSize = true;
			this.lblNeptune.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblNeptune.Location = new System.Drawing.Point(129, 71);
			this.lblNeptune.Name = "lblNeptune";
			this.lblNeptune.Size = new System.Drawing.Size(14, 13);
			this.lblNeptune.TabIndex = 38;
			this.lblNeptune.Text = "N";
			// 
			// lblUranus
			// 
			this.lblUranus.AutoSize = true;
			this.lblUranus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblUranus.Location = new System.Drawing.Point(114, 71);
			this.lblUranus.Name = "lblUranus";
			this.lblUranus.Size = new System.Drawing.Size(15, 13);
			this.lblUranus.TabIndex = 37;
			this.lblUranus.Text = "U";
			// 
			// lblSaturn
			// 
			this.lblSaturn.AutoSize = true;
			this.lblSaturn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblSaturn.Location = new System.Drawing.Point(99, 71);
			this.lblSaturn.Name = "lblSaturn";
			this.lblSaturn.Size = new System.Drawing.Size(14, 13);
			this.lblSaturn.TabIndex = 36;
			this.lblSaturn.Text = "S";
			// 
			// lblJupiter
			// 
			this.lblJupiter.AutoSize = true;
			this.lblJupiter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblJupiter.Location = new System.Drawing.Point(85, 71);
			this.lblJupiter.Name = "lblJupiter";
			this.lblJupiter.Size = new System.Drawing.Size(13, 13);
			this.lblJupiter.TabIndex = 35;
			this.lblJupiter.Text = "J";
			// 
			// lblMars
			// 
			this.lblMars.AutoSize = true;
			this.lblMars.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblMars.Location = new System.Drawing.Point(68, 71);
			this.lblMars.Name = "lblMars";
			this.lblMars.Size = new System.Drawing.Size(17, 13);
			this.lblMars.TabIndex = 34;
			this.lblMars.Text = "M";
			// 
			// lblEarth
			// 
			this.lblEarth.AutoSize = true;
			this.lblEarth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblEarth.Location = new System.Drawing.Point(55, 71);
			this.lblEarth.Name = "lblEarth";
			this.lblEarth.Size = new System.Drawing.Size(13, 13);
			this.lblEarth.TabIndex = 33;
			this.lblEarth.Text = "E";
			// 
			// lblVenus
			// 
			this.lblVenus.AutoSize = true;
			this.lblVenus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblVenus.Location = new System.Drawing.Point(40, 71);
			this.lblVenus.Name = "lblVenus";
			this.lblVenus.Size = new System.Drawing.Size(14, 13);
			this.lblVenus.TabIndex = 32;
			this.lblVenus.Text = "V";
			// 
			// lblMercury
			// 
			this.lblMercury.AutoSize = true;
			this.lblMercury.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblMercury.Location = new System.Drawing.Point(23, 71);
			this.lblMercury.Name = "lblMercury";
			this.lblMercury.Size = new System.Drawing.Size(17, 13);
			this.lblMercury.TabIndex = 31;
			this.lblMercury.Text = "M";
			// 
			// lblSun
			// 
			this.lblSun.AutoSize = true;
			this.lblSun.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblSun.Location = new System.Drawing.Point(9, 71);
			this.lblSun.Name = "lblSun";
			this.lblSun.Size = new System.Drawing.Size(14, 13);
			this.lblSun.TabIndex = 30;
			this.lblSun.Text = "S";
			// 
			// btnAllOrbitsLabels
			// 
			this.btnAllOrbitsLabels.Location = new System.Drawing.Point(94, 93);
			this.btnAllOrbitsLabels.Name = "btnAllOrbitsLabels";
			this.btnAllOrbitsLabels.Size = new System.Drawing.Size(79, 23);
			this.btnAllOrbitsLabels.TabIndex = 33;
			this.btnAllOrbitsLabels.Text = "All";
			this.btnAllOrbitsLabels.UseVisualStyleBackColor = true;
			this.btnAllOrbitsLabels.Click += new System.EventHandler(this.btnAllOrbitsLabels_Click);
			// 
			// btnDefaultOrbitsLabels
			// 
			this.btnDefaultOrbitsLabels.Location = new System.Drawing.Point(8, 93);
			this.btnDefaultOrbitsLabels.Name = "btnDefaultOrbitsLabels";
			this.btnDefaultOrbitsLabels.Size = new System.Drawing.Size(79, 23);
			this.btnDefaultOrbitsLabels.TabIndex = 32;
			this.btnDefaultOrbitsLabels.Text = "Default";
			this.btnDefaultOrbitsLabels.UseVisualStyleBackColor = true;
			this.btnDefaultOrbitsLabels.Click += new System.EventHandler(this.btnDefaultOrbitsLabels_Click);
			// 
			// rbtnCenterMercury
			// 
			this.rbtnCenterMercury.AutoSize = true;
			this.rbtnCenterMercury.Location = new System.Drawing.Point(25, 54);
			this.rbtnCenterMercury.Name = "rbtnCenterMercury";
			this.rbtnCenterMercury.Size = new System.Drawing.Size(14, 13);
			this.rbtnCenterMercury.TabIndex = 23;
			this.rbtnCenterMercury.UseVisualStyleBackColor = true;
			this.rbtnCenterMercury.CheckedChanged += new System.EventHandler(this.rbtnCenterCommon_CheckedChanged);
			// 
			// cbxLabelComet
			// 
			this.cbxLabelComet.AutoSize = true;
			this.cbxLabelComet.Location = new System.Drawing.Point(144, 37);
			this.cbxLabelComet.Name = "cbxLabelComet";
			this.cbxLabelComet.Size = new System.Drawing.Size(15, 14);
			this.cbxLabelComet.TabIndex = 20;
			this.cbxLabelComet.UseVisualStyleBackColor = true;
			this.cbxLabelComet.CheckedChanged += new System.EventHandler(this.cbxLabelCommon_CheckedChanged);
			// 
			// cbxOrbitComet
			// 
			this.cbxOrbitComet.AutoSize = true;
			this.cbxOrbitComet.Location = new System.Drawing.Point(144, 22);
			this.cbxOrbitComet.Name = "cbxOrbitComet";
			this.cbxOrbitComet.Size = new System.Drawing.Size(15, 14);
			this.cbxOrbitComet.TabIndex = 9;
			this.cbxOrbitComet.UseVisualStyleBackColor = true;
			this.cbxOrbitComet.CheckedChanged += new System.EventHandler(this.cbxOrbitCommon_CheckedChanged);
			// 
			// rbtnCenterSun
			// 
			this.rbtnCenterSun.AutoSize = true;
			this.rbtnCenterSun.Checked = true;
			this.rbtnCenterSun.Location = new System.Drawing.Point(10, 54);
			this.rbtnCenterSun.Name = "rbtnCenterSun";
			this.rbtnCenterSun.Size = new System.Drawing.Size(14, 13);
			this.rbtnCenterSun.TabIndex = 22;
			this.rbtnCenterSun.TabStop = true;
			this.rbtnCenterSun.UseVisualStyleBackColor = true;
			this.rbtnCenterSun.CheckedChanged += new System.EventHandler(this.rbtnCenterCommon_CheckedChanged);
			// 
			// cbxLabelNeptune
			// 
			this.cbxLabelNeptune.AutoSize = true;
			this.cbxLabelNeptune.Checked = true;
			this.cbxLabelNeptune.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxLabelNeptune.Location = new System.Drawing.Point(129, 37);
			this.cbxLabelNeptune.Name = "cbxLabelNeptune";
			this.cbxLabelNeptune.Size = new System.Drawing.Size(15, 14);
			this.cbxLabelNeptune.TabIndex = 19;
			this.cbxLabelNeptune.UseVisualStyleBackColor = true;
			this.cbxLabelNeptune.CheckedChanged += new System.EventHandler(this.cbxLabelCommon_CheckedChanged);
			// 
			// cbxLabelUranus
			// 
			this.cbxLabelUranus.AutoSize = true;
			this.cbxLabelUranus.Checked = true;
			this.cbxLabelUranus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxLabelUranus.Location = new System.Drawing.Point(114, 37);
			this.cbxLabelUranus.Name = "cbxLabelUranus";
			this.cbxLabelUranus.Size = new System.Drawing.Size(15, 14);
			this.cbxLabelUranus.TabIndex = 18;
			this.cbxLabelUranus.UseVisualStyleBackColor = true;
			this.cbxLabelUranus.CheckedChanged += new System.EventHandler(this.cbxLabelCommon_CheckedChanged);
			// 
			// rbtnCenterComet
			// 
			this.rbtnCenterComet.AutoSize = true;
			this.rbtnCenterComet.Location = new System.Drawing.Point(145, 54);
			this.rbtnCenterComet.Name = "rbtnCenterComet";
			this.rbtnCenterComet.Size = new System.Drawing.Size(14, 13);
			this.rbtnCenterComet.TabIndex = 31;
			this.rbtnCenterComet.UseVisualStyleBackColor = true;
			this.rbtnCenterComet.CheckedChanged += new System.EventHandler(this.rbtnCenterCommon_CheckedChanged);
			// 
			// cbxOrbitNeptune
			// 
			this.cbxOrbitNeptune.AutoSize = true;
			this.cbxOrbitNeptune.Location = new System.Drawing.Point(129, 22);
			this.cbxOrbitNeptune.Name = "cbxOrbitNeptune";
			this.cbxOrbitNeptune.Size = new System.Drawing.Size(15, 14);
			this.cbxOrbitNeptune.TabIndex = 8;
			this.cbxOrbitNeptune.UseVisualStyleBackColor = true;
			this.cbxOrbitNeptune.CheckedChanged += new System.EventHandler(this.cbxOrbitCommon_CheckedChanged);
			// 
			// cbxOrbitUranus
			// 
			this.cbxOrbitUranus.AutoSize = true;
			this.cbxOrbitUranus.Location = new System.Drawing.Point(114, 22);
			this.cbxOrbitUranus.Name = "cbxOrbitUranus";
			this.cbxOrbitUranus.Size = new System.Drawing.Size(15, 14);
			this.cbxOrbitUranus.TabIndex = 7;
			this.cbxOrbitUranus.UseVisualStyleBackColor = true;
			this.cbxOrbitUranus.CheckedChanged += new System.EventHandler(this.cbxOrbitCommon_CheckedChanged);
			// 
			// rbtnCenterNeptune
			// 
			this.rbtnCenterNeptune.AutoSize = true;
			this.rbtnCenterNeptune.Location = new System.Drawing.Point(130, 54);
			this.rbtnCenterNeptune.Name = "rbtnCenterNeptune";
			this.rbtnCenterNeptune.Size = new System.Drawing.Size(14, 13);
			this.rbtnCenterNeptune.TabIndex = 30;
			this.rbtnCenterNeptune.UseVisualStyleBackColor = true;
			this.rbtnCenterNeptune.CheckedChanged += new System.EventHandler(this.rbtnCenterCommon_CheckedChanged);
			// 
			// cbxLabelSaturn
			// 
			this.cbxLabelSaturn.AutoSize = true;
			this.cbxLabelSaturn.Checked = true;
			this.cbxLabelSaturn.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxLabelSaturn.Location = new System.Drawing.Point(99, 37);
			this.cbxLabelSaturn.Name = "cbxLabelSaturn";
			this.cbxLabelSaturn.Size = new System.Drawing.Size(15, 14);
			this.cbxLabelSaturn.TabIndex = 17;
			this.cbxLabelSaturn.UseVisualStyleBackColor = true;
			this.cbxLabelSaturn.CheckedChanged += new System.EventHandler(this.cbxLabelCommon_CheckedChanged);
			// 
			// cbxLabelJupiter
			// 
			this.cbxLabelJupiter.AutoSize = true;
			this.cbxLabelJupiter.Checked = true;
			this.cbxLabelJupiter.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxLabelJupiter.Location = new System.Drawing.Point(84, 37);
			this.cbxLabelJupiter.Name = "cbxLabelJupiter";
			this.cbxLabelJupiter.Size = new System.Drawing.Size(15, 14);
			this.cbxLabelJupiter.TabIndex = 16;
			this.cbxLabelJupiter.UseVisualStyleBackColor = true;
			this.cbxLabelJupiter.CheckedChanged += new System.EventHandler(this.cbxLabelCommon_CheckedChanged);
			// 
			// rbtnCenterUranus
			// 
			this.rbtnCenterUranus.AutoSize = true;
			this.rbtnCenterUranus.Location = new System.Drawing.Point(115, 54);
			this.rbtnCenterUranus.Name = "rbtnCenterUranus";
			this.rbtnCenterUranus.Size = new System.Drawing.Size(14, 13);
			this.rbtnCenterUranus.TabIndex = 29;
			this.rbtnCenterUranus.UseVisualStyleBackColor = true;
			this.rbtnCenterUranus.CheckedChanged += new System.EventHandler(this.rbtnCenterCommon_CheckedChanged);
			// 
			// cbxOrbitSaturn
			// 
			this.cbxOrbitSaturn.AutoSize = true;
			this.cbxOrbitSaturn.Location = new System.Drawing.Point(99, 22);
			this.cbxOrbitSaturn.Name = "cbxOrbitSaturn";
			this.cbxOrbitSaturn.Size = new System.Drawing.Size(15, 14);
			this.cbxOrbitSaturn.TabIndex = 6;
			this.cbxOrbitSaturn.UseVisualStyleBackColor = true;
			this.cbxOrbitSaturn.CheckedChanged += new System.EventHandler(this.cbxOrbitCommon_CheckedChanged);
			// 
			// cbxOrbitJupiter
			// 
			this.cbxOrbitJupiter.AutoSize = true;
			this.cbxOrbitJupiter.Checked = true;
			this.cbxOrbitJupiter.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxOrbitJupiter.Location = new System.Drawing.Point(84, 22);
			this.cbxOrbitJupiter.Name = "cbxOrbitJupiter";
			this.cbxOrbitJupiter.Size = new System.Drawing.Size(15, 14);
			this.cbxOrbitJupiter.TabIndex = 5;
			this.cbxOrbitJupiter.UseVisualStyleBackColor = true;
			this.cbxOrbitJupiter.CheckedChanged += new System.EventHandler(this.cbxOrbitCommon_CheckedChanged);
			// 
			// rbtnCenterSaturn
			// 
			this.rbtnCenterSaturn.AutoSize = true;
			this.rbtnCenterSaturn.Location = new System.Drawing.Point(100, 54);
			this.rbtnCenterSaturn.Name = "rbtnCenterSaturn";
			this.rbtnCenterSaturn.Size = new System.Drawing.Size(14, 13);
			this.rbtnCenterSaturn.TabIndex = 28;
			this.rbtnCenterSaturn.UseVisualStyleBackColor = true;
			this.rbtnCenterSaturn.CheckedChanged += new System.EventHandler(this.rbtnCenterCommon_CheckedChanged);
			// 
			// cbxLabelMars
			// 
			this.cbxLabelMars.AutoSize = true;
			this.cbxLabelMars.Checked = true;
			this.cbxLabelMars.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxLabelMars.Location = new System.Drawing.Point(69, 37);
			this.cbxLabelMars.Name = "cbxLabelMars";
			this.cbxLabelMars.Size = new System.Drawing.Size(15, 14);
			this.cbxLabelMars.TabIndex = 15;
			this.cbxLabelMars.UseVisualStyleBackColor = true;
			this.cbxLabelMars.CheckedChanged += new System.EventHandler(this.cbxLabelCommon_CheckedChanged);
			// 
			// cbxLabelEarth
			// 
			this.cbxLabelEarth.AutoSize = true;
			this.cbxLabelEarth.Checked = true;
			this.cbxLabelEarth.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxLabelEarth.Location = new System.Drawing.Point(54, 37);
			this.cbxLabelEarth.Name = "cbxLabelEarth";
			this.cbxLabelEarth.Size = new System.Drawing.Size(15, 14);
			this.cbxLabelEarth.TabIndex = 14;
			this.cbxLabelEarth.UseVisualStyleBackColor = true;
			this.cbxLabelEarth.CheckedChanged += new System.EventHandler(this.cbxLabelCommon_CheckedChanged);
			// 
			// rbtnCenterJupiter
			// 
			this.rbtnCenterJupiter.AutoSize = true;
			this.rbtnCenterJupiter.Location = new System.Drawing.Point(85, 54);
			this.rbtnCenterJupiter.Name = "rbtnCenterJupiter";
			this.rbtnCenterJupiter.Size = new System.Drawing.Size(14, 13);
			this.rbtnCenterJupiter.TabIndex = 27;
			this.rbtnCenterJupiter.UseVisualStyleBackColor = true;
			this.rbtnCenterJupiter.CheckedChanged += new System.EventHandler(this.rbtnCenterCommon_CheckedChanged);
			// 
			// cbxOrbitMars
			// 
			this.cbxOrbitMars.AutoSize = true;
			this.cbxOrbitMars.Checked = true;
			this.cbxOrbitMars.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxOrbitMars.Location = new System.Drawing.Point(69, 22);
			this.cbxOrbitMars.Name = "cbxOrbitMars";
			this.cbxOrbitMars.Size = new System.Drawing.Size(15, 14);
			this.cbxOrbitMars.TabIndex = 4;
			this.cbxOrbitMars.UseVisualStyleBackColor = true;
			this.cbxOrbitMars.CheckedChanged += new System.EventHandler(this.cbxOrbitCommon_CheckedChanged);
			// 
			// cbxOrbitEarth
			// 
			this.cbxOrbitEarth.AutoSize = true;
			this.cbxOrbitEarth.Checked = true;
			this.cbxOrbitEarth.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxOrbitEarth.Location = new System.Drawing.Point(54, 22);
			this.cbxOrbitEarth.Name = "cbxOrbitEarth";
			this.cbxOrbitEarth.Size = new System.Drawing.Size(15, 14);
			this.cbxOrbitEarth.TabIndex = 3;
			this.cbxOrbitEarth.UseVisualStyleBackColor = true;
			this.cbxOrbitEarth.CheckedChanged += new System.EventHandler(this.cbxOrbitCommon_CheckedChanged);
			// 
			// rbtnCenterMars
			// 
			this.rbtnCenterMars.AutoSize = true;
			this.rbtnCenterMars.Location = new System.Drawing.Point(70, 54);
			this.rbtnCenterMars.Name = "rbtnCenterMars";
			this.rbtnCenterMars.Size = new System.Drawing.Size(14, 13);
			this.rbtnCenterMars.TabIndex = 26;
			this.rbtnCenterMars.UseVisualStyleBackColor = true;
			this.rbtnCenterMars.CheckedChanged += new System.EventHandler(this.rbtnCenterCommon_CheckedChanged);
			// 
			// cbxLabelVenus
			// 
			this.cbxLabelVenus.AutoSize = true;
			this.cbxLabelVenus.Checked = true;
			this.cbxLabelVenus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxLabelVenus.Location = new System.Drawing.Point(39, 37);
			this.cbxLabelVenus.Name = "cbxLabelVenus";
			this.cbxLabelVenus.Size = new System.Drawing.Size(15, 14);
			this.cbxLabelVenus.TabIndex = 13;
			this.cbxLabelVenus.UseVisualStyleBackColor = true;
			this.cbxLabelVenus.CheckedChanged += new System.EventHandler(this.cbxLabelCommon_CheckedChanged);
			// 
			// cbxLabelMercury
			// 
			this.cbxLabelMercury.AutoSize = true;
			this.cbxLabelMercury.Checked = true;
			this.cbxLabelMercury.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxLabelMercury.Location = new System.Drawing.Point(24, 37);
			this.cbxLabelMercury.Name = "cbxLabelMercury";
			this.cbxLabelMercury.Size = new System.Drawing.Size(15, 14);
			this.cbxLabelMercury.TabIndex = 12;
			this.cbxLabelMercury.UseVisualStyleBackColor = true;
			this.cbxLabelMercury.CheckedChanged += new System.EventHandler(this.cbxLabelCommon_CheckedChanged);
			// 
			// rbtnCenterVenus
			// 
			this.rbtnCenterVenus.AutoSize = true;
			this.rbtnCenterVenus.Location = new System.Drawing.Point(40, 54);
			this.rbtnCenterVenus.Name = "rbtnCenterVenus";
			this.rbtnCenterVenus.Size = new System.Drawing.Size(14, 13);
			this.rbtnCenterVenus.TabIndex = 24;
			this.rbtnCenterVenus.UseVisualStyleBackColor = true;
			this.rbtnCenterVenus.CheckedChanged += new System.EventHandler(this.rbtnCenterCommon_CheckedChanged);
			// 
			// rbtnCenterEarth
			// 
			this.rbtnCenterEarth.AutoSize = true;
			this.rbtnCenterEarth.Location = new System.Drawing.Point(55, 54);
			this.rbtnCenterEarth.Name = "rbtnCenterEarth";
			this.rbtnCenterEarth.Size = new System.Drawing.Size(14, 13);
			this.rbtnCenterEarth.TabIndex = 25;
			this.rbtnCenterEarth.UseVisualStyleBackColor = true;
			this.rbtnCenterEarth.CheckedChanged += new System.EventHandler(this.rbtnCenterCommon_CheckedChanged);
			// 
			// cbxOrbitVenus
			// 
			this.cbxOrbitVenus.AutoSize = true;
			this.cbxOrbitVenus.Checked = true;
			this.cbxOrbitVenus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxOrbitVenus.Location = new System.Drawing.Point(39, 22);
			this.cbxOrbitVenus.Name = "cbxOrbitVenus";
			this.cbxOrbitVenus.Size = new System.Drawing.Size(15, 14);
			this.cbxOrbitVenus.TabIndex = 2;
			this.cbxOrbitVenus.UseVisualStyleBackColor = true;
			this.cbxOrbitVenus.CheckedChanged += new System.EventHandler(this.cbxOrbitCommon_CheckedChanged);
			// 
			// cbxOrbitMercury
			// 
			this.cbxOrbitMercury.AutoSize = true;
			this.cbxOrbitMercury.Checked = true;
			this.cbxOrbitMercury.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbxOrbitMercury.Location = new System.Drawing.Point(24, 22);
			this.cbxOrbitMercury.Name = "cbxOrbitMercury";
			this.cbxOrbitMercury.Size = new System.Drawing.Size(15, 14);
			this.cbxOrbitMercury.TabIndex = 1;
			this.cbxOrbitMercury.UseVisualStyleBackColor = true;
			this.cbxOrbitMercury.CheckedChanged += new System.EventHandler(this.cbxOrbitCommon_CheckedChanged);
			// 
			// gbxMode
			// 
			this.gbxMode.Controls.Add(this.rbtnMultipleMode);
			this.gbxMode.Controls.Add(this.rbtnSingleMode);
			this.gbxMode.Location = new System.Drawing.Point(5, 80);
			this.gbxMode.Name = "gbxMode";
			this.gbxMode.Size = new System.Drawing.Size(181, 42);
			this.gbxMode.TabIndex = 1;
			this.gbxMode.TabStop = false;
			this.gbxMode.Text = "Mode";
			// 
			// rbtnMultipleMode
			// 
			this.rbtnMultipleMode.AutoSize = true;
			this.rbtnMultipleMode.Location = new System.Drawing.Point(89, 16);
			this.rbtnMultipleMode.Name = "rbtnMultipleMode";
			this.rbtnMultipleMode.Size = new System.Drawing.Size(61, 17);
			this.rbtnMultipleMode.TabIndex = 1;
			this.rbtnMultipleMode.Text = "Multiple";
			this.rbtnMultipleMode.UseVisualStyleBackColor = true;
			this.rbtnMultipleMode.CheckedChanged += new System.EventHandler(this.rbtnMode_CheckedChanged);
			// 
			// rbtnSingleMode
			// 
			this.rbtnSingleMode.AutoSize = true;
			this.rbtnSingleMode.Checked = true;
			this.rbtnSingleMode.Location = new System.Drawing.Point(28, 16);
			this.rbtnSingleMode.Name = "rbtnSingleMode";
			this.rbtnSingleMode.Size = new System.Drawing.Size(53, 17);
			this.rbtnSingleMode.TabIndex = 0;
			this.rbtnSingleMode.TabStop = true;
			this.rbtnSingleMode.Text = "Single";
			this.rbtnSingleMode.UseVisualStyleBackColor = true;
			this.rbtnSingleMode.CheckedChanged += new System.EventHandler(this.rbtnMode_CheckedChanged);
			// 
			// gbxComet
			// 
			this.gbxComet.Controls.Add(this.btnClear);
			this.gbxComet.Controls.Add(this.btnAll);
			this.gbxComet.Controls.Add(this.btnFilter);
			this.gbxComet.Controls.Add(this.cboComet);
			this.gbxComet.Location = new System.Drawing.Point(5, 3);
			this.gbxComet.Name = "gbxComet";
			this.gbxComet.Size = new System.Drawing.Size(181, 76);
			this.gbxComet.TabIndex = 0;
			this.gbxComet.TabStop = false;
			this.gbxComet.Text = "Comet";
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(120, 45);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(53, 23);
			this.btnClear.TabIndex = 3;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnAll
			// 
			this.btnAll.Location = new System.Drawing.Point(64, 45);
			this.btnAll.Name = "btnAll";
			this.btnAll.Size = new System.Drawing.Size(53, 23);
			this.btnAll.TabIndex = 2;
			this.btnAll.Text = "All";
			this.btnAll.UseVisualStyleBackColor = true;
			this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
			// 
			// btnFilter
			// 
			this.btnFilter.Location = new System.Drawing.Point(7, 45);
			this.btnFilter.Name = "btnFilter";
			this.btnFilter.Size = new System.Drawing.Size(53, 23);
			this.btnFilter.TabIndex = 1;
			this.btnFilter.Text = "Filter";
			this.btnFilter.UseVisualStyleBackColor = true;
			this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
			// 
			// orbitPanel
			// 
			this.orbitPanel.BackColor = System.Drawing.Color.Black;
			this.orbitPanel.Controls.Add(this.scrollVert);
			this.orbitPanel.Controls.Add(this.scrollHorz);
			this.orbitPanel.Controls.Add(this.scrollZoom);
			this.orbitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.orbitPanel.Location = new System.Drawing.Point(195, 0);
			this.orbitPanel.MinimumSize = new System.Drawing.Size(682, 458);
			this.orbitPanel.Name = "orbitPanel";
			this.orbitPanel.Size = new System.Drawing.Size(739, 682);
			this.orbitPanel.TabIndex = 1;
			this.orbitPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.orbitPanel_MouseClick);
			this.orbitPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.orbitPanel_MouseDoubleClick);
			this.orbitPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.orbitPanel_MouseDown);
			this.orbitPanel.MouseEnter += new System.EventHandler(this.orbitPanel_MouseEnter);
			this.orbitPanel.MouseLeave += new System.EventHandler(this.orbitPanel_MouseLeave);
			this.orbitPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.orbitPanel_MouseMove);
			this.orbitPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.orbitPanel_MouseUp);
			this.orbitPanel.Resize += new System.EventHandler(this.orbitPanel_Resize);
			// 
			// scrollVert
			// 
			this.scrollVert.LargeChange = 1;
			this.scrollVert.Location = new System.Drawing.Point(713, 345);
			this.scrollVert.Maximum = 360;
			this.scrollVert.Name = "scrollVert";
			this.scrollVert.Size = new System.Drawing.Size(17, 272);
			this.scrollVert.TabIndex = 1;
			this.scrollVert.Visible = false;
			this.scrollVert.ValueChanged += new System.EventHandler(this.scrollVert_ValueChanged);
			// 
			// scrollHorz
			// 
			this.scrollHorz.LargeChange = 1;
			this.scrollHorz.Location = new System.Drawing.Point(437, 629);
			this.scrollHorz.Maximum = 360;
			this.scrollHorz.Name = "scrollHorz";
			this.scrollHorz.Size = new System.Drawing.Size(293, 17);
			this.scrollHorz.TabIndex = 2;
			this.scrollHorz.Visible = false;
			this.scrollHorz.ValueChanged += new System.EventHandler(this.scrollHorz_ValueChanged);
			// 
			// scrollZoom
			// 
			this.scrollZoom.Location = new System.Drawing.Point(437, 656);
			this.scrollZoom.Maximum = 1000;
			this.scrollZoom.Minimum = 5;
			this.scrollZoom.Name = "scrollZoom";
			this.scrollZoom.Size = new System.Drawing.Size(293, 17);
			this.scrollZoom.TabIndex = 12;
			this.scrollZoom.Value = 5;
			this.scrollZoom.Visible = false;
			this.scrollZoom.ValueChanged += new System.EventHandler(this.scrollZoom_ValueChanged);
			// 
			// FormOrbitViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(934, 682);
			this.Controls.Add(this.orbitPanel);
			this.Controls.Add(this.pnlToolbox);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(720, 650);
			this.Name = "FormOrbitViewer";
			this.Text = "Orbit Viewer";
			this.Activated += new System.EventHandler(this.FormOrbitViewer_Activated);
			this.Deactivate += new System.EventHandler(this.FormOrbitViewer_Deactivate);
			this.Load += new System.EventHandler(this.FormOrbit_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOrbitViewer_KeyDown);
			this.pnlToolbox.ResumeLayout(false);
			this.gbxMisc.ResumeLayout(false);
			this.gbxMisc.PerformLayout();
			this.gbxInfoLabels.ResumeLayout(false);
			this.gbxInfoLabels.PerformLayout();
			this.gbxSimulation.ResumeLayout(false);
			this.gbxDateTime.ResumeLayout(false);
			this.gbxOrbitsLabelsCenter.ResumeLayout(false);
			this.gbxOrbitsLabelsCenter.PerformLayout();
			this.gbxMode.ResumeLayout(false);
			this.gbxMode.PerformLayout();
			this.gbxComet.ResumeLayout(false);
			this.orbitPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnPerihDate;
		private System.Windows.Forms.Button btnNow;
		private System.Windows.Forms.ComboBox cboComet;
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
		private System.Windows.Forms.Button btnDate;
		private System.Windows.Forms.GroupBox gbxComet;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnAll;
		private System.Windows.Forms.Button btnFilter;
		private System.Windows.Forms.GroupBox gbxMode;
		private System.Windows.Forms.RadioButton rbtnMultipleMode;
		private System.Windows.Forms.RadioButton rbtnSingleMode;
		private System.Windows.Forms.GroupBox gbxOrbitsLabelsCenter;
		private System.Windows.Forms.Label lblComet;
		private System.Windows.Forms.Label lblNeptune;
		private System.Windows.Forms.Label lblUranus;
		private System.Windows.Forms.Label lblSaturn;
		private System.Windows.Forms.Label lblJupiter;
		private System.Windows.Forms.Label lblMars;
		private System.Windows.Forms.Label lblEarth;
		private System.Windows.Forms.Label lblVenus;
		private System.Windows.Forms.Label lblMercury;
		private System.Windows.Forms.Label lblSun;
		private System.Windows.Forms.Button btnAllOrbitsLabels;
		private System.Windows.Forms.Button btnDefaultOrbitsLabels;
		private System.Windows.Forms.RadioButton rbtnCenterMercury;
		private System.Windows.Forms.CheckBox cbxLabelComet;
		private System.Windows.Forms.CheckBox cbxOrbitComet;
		private System.Windows.Forms.RadioButton rbtnCenterSun;
		private System.Windows.Forms.CheckBox cbxLabelNeptune;
		private System.Windows.Forms.CheckBox cbxLabelUranus;
		private System.Windows.Forms.RadioButton rbtnCenterComet;
		private System.Windows.Forms.CheckBox cbxOrbitNeptune;
		private System.Windows.Forms.CheckBox cbxOrbitUranus;
		private System.Windows.Forms.RadioButton rbtnCenterNeptune;
		private System.Windows.Forms.CheckBox cbxLabelSaturn;
		private System.Windows.Forms.CheckBox cbxLabelJupiter;
		private System.Windows.Forms.RadioButton rbtnCenterUranus;
		private System.Windows.Forms.CheckBox cbxOrbitSaturn;
		private System.Windows.Forms.CheckBox cbxOrbitJupiter;
		private System.Windows.Forms.RadioButton rbtnCenterSaturn;
		private System.Windows.Forms.CheckBox cbxLabelMars;
		private System.Windows.Forms.CheckBox cbxLabelEarth;
		private System.Windows.Forms.RadioButton rbtnCenterJupiter;
		private System.Windows.Forms.CheckBox cbxOrbitMars;
		private System.Windows.Forms.CheckBox cbxOrbitEarth;
		private System.Windows.Forms.RadioButton rbtnCenterMars;
		private System.Windows.Forms.CheckBox cbxLabelVenus;
		private System.Windows.Forms.CheckBox cbxLabelMercury;
		private System.Windows.Forms.RadioButton rbtnCenterVenus;
		private System.Windows.Forms.RadioButton rbtnCenterEarth;
		private System.Windows.Forms.CheckBox cbxOrbitVenus;
		private System.Windows.Forms.CheckBox cbxOrbitMercury;
		private System.Windows.Forms.GroupBox gbxDateTime;
		private System.Windows.Forms.GroupBox gbxSimulation;
		private System.Windows.Forms.GroupBox gbxInfoLabels;
		private System.Windows.Forms.CheckBox cbxDateTime;
		private System.Windows.Forms.CheckBox cbxMagDist;
		private System.Windows.Forms.GroupBox gbxMisc;
		private System.Windows.Forms.Button btnSaveImage;
		private System.Windows.Forms.CheckBox cbxAntialiasing;
		private System.Windows.Forms.CheckBox cbxShowAxes;
		private System.Windows.Forms.Button btnNoLabels;
		private System.Windows.Forms.Button btnNoOrbits;
		private System.Windows.Forms.Button btnAllLabels;
		private System.Windows.Forms.Button btnAllOrbits;
		private System.Windows.Forms.CheckBox cbxSelectedOrbit;
		private System.Windows.Forms.CheckBox cbxMarker;
		private System.Windows.Forms.CheckBox cbxSelectedLabel;

	}
}