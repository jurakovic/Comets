namespace Comets.Application.OrbitViewer.Controls
{
	partial class SimulationControl
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
			pnlSimulation = new Panel();
			btnRevPlay = new Button();
			btnRevStep = new Button();
			cboTimestep = new ComboBox();
			btnStop = new Button();
			btnForPlay = new Button();
			btnForStep = new Button();
			pnlSimulation.SuspendLayout();
			SuspendLayout();
			// 
			// pnlSimulation
			// 
			pnlSimulation.BackColor = SystemColors.ControlDark;
			pnlSimulation.Controls.Add(btnRevPlay);
			pnlSimulation.Controls.Add(btnRevStep);
			pnlSimulation.Controls.Add(cboTimestep);
			pnlSimulation.Controls.Add(btnStop);
			pnlSimulation.Controls.Add(btnForPlay);
			pnlSimulation.Controls.Add(btnForStep);
			pnlSimulation.Dock = DockStyle.Fill;
			pnlSimulation.Location = new Point(0, 0);
			pnlSimulation.Name = "pnlSimulation";
			pnlSimulation.Size = new Size(173, 60);
			pnlSimulation.TabIndex = 0;
			// 
			// btnRevPlay
			// 
			btnRevPlay.Font = new Font("Tahoma", 6.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
			btnRevPlay.Location = new Point(5, 5);
			btnRevPlay.Name = "btnRevPlay";
			btnRevPlay.Size = new Size(31, 23);
			btnRevPlay.TabIndex = 0;
			btnRevPlay.Text = "<<";
			btnRevPlay.UseVisualStyleBackColor = true;
			btnRevPlay.Click += btnRevPlay_Click;
			// 
			// btnRevStep
			// 
			btnRevStep.Font = new Font("Tahoma", 6.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
			btnRevStep.Location = new Point(38, 5);
			btnRevStep.Name = "btnRevStep";
			btnRevStep.Size = new Size(31, 23);
			btnRevStep.TabIndex = 1;
			btnRevStep.Text = "|<";
			btnRevStep.UseVisualStyleBackColor = true;
			btnRevStep.Click += btnRevStep_Click;
			// 
			// cboTimestep
			// 
			cboTimestep.DropDownStyle = ComboBoxStyle.DropDownList;
			cboTimestep.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			cboTimestep.FormattingEnabled = true;
			cboTimestep.Location = new Point(5, 34);
			cboTimestep.Name = "cboTimestep";
			cboTimestep.Size = new Size(163, 21);
			cboTimestep.TabIndex = 5;
			cboTimestep.SelectedIndexChanged += cboTimestep_SelectedIndexChanged;
			// 
			// btnStop
			// 
			btnStop.Font = new Font("Tahoma", 6.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
			btnStop.Location = new Point(71, 5);
			btnStop.Name = "btnStop";
			btnStop.Size = new Size(31, 23);
			btnStop.TabIndex = 2;
			btnStop.Text = "||";
			btnStop.UseVisualStyleBackColor = true;
			btnStop.Click += btnStop_Click;
			// 
			// btnForPlay
			// 
			btnForPlay.Font = new Font("Tahoma", 6.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
			btnForPlay.Location = new Point(137, 5);
			btnForPlay.Name = "btnForPlay";
			btnForPlay.Size = new Size(31, 23);
			btnForPlay.TabIndex = 4;
			btnForPlay.Text = ">>";
			btnForPlay.UseVisualStyleBackColor = true;
			btnForPlay.Click += btnForPlay_Click;
			// 
			// btnForStep
			// 
			btnForStep.Font = new Font("Tahoma", 6.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
			btnForStep.Location = new Point(104, 5);
			btnForStep.Name = "btnForStep";
			btnForStep.Size = new Size(31, 23);
			btnForStep.TabIndex = 3;
			btnForStep.Text = ">|";
			btnForStep.UseVisualStyleBackColor = true;
			btnForStep.Click += btnForStep_Click;
			// 
			// SimulationControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(pnlSimulation);
			Font = new Font("Tahoma", 8.25F);
			Name = "SimulationControl";
			Size = new Size(173, 60);
			Load += SimulationControl_Load;
			pnlSimulation.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel pnlSimulation;
		private System.Windows.Forms.Button btnRevPlay;
		private System.Windows.Forms.Button btnRevStep;
		private System.Windows.Forms.ComboBox cboTimestep;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnForPlay;
		private System.Windows.Forms.Button btnForStep;
	}
}
