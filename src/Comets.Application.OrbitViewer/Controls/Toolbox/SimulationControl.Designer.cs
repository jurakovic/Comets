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
			pnlSimulation = new System.Windows.Forms.Panel();
			btnRevPlay = new System.Windows.Forms.Button();
			btnRevStep = new System.Windows.Forms.Button();
			cboTimestep = new System.Windows.Forms.ComboBox();
			btnStop = new System.Windows.Forms.Button();
			btnForPlay = new System.Windows.Forms.Button();
			btnForStep = new System.Windows.Forms.Button();
			pnlSimulation.SuspendLayout();
			SuspendLayout();
			// 
			// pnlSimulation
			// 
			pnlSimulation.BackColor = System.Drawing.SystemColors.ControlDark;
			pnlSimulation.Controls.Add(btnRevPlay);
			pnlSimulation.Controls.Add(btnRevStep);
			pnlSimulation.Controls.Add(cboTimestep);
			pnlSimulation.Controls.Add(btnStop);
			pnlSimulation.Controls.Add(btnForPlay);
			pnlSimulation.Controls.Add(btnForStep);
			pnlSimulation.Dock = System.Windows.Forms.DockStyle.Fill;
			pnlSimulation.Location = new System.Drawing.Point(0, 0);
			pnlSimulation.Name = "pnlSimulation";
			pnlSimulation.Size = new System.Drawing.Size(173, 60);
			pnlSimulation.TabIndex = 0;
			// 
			// btnRevPlay
			// 
			btnRevPlay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			btnRevPlay.Location = new System.Drawing.Point(5, 5);
			btnRevPlay.Name = "btnRevPlay";
			btnRevPlay.Size = new System.Drawing.Size(31, 23);
			btnRevPlay.TabIndex = 0;
			btnRevPlay.Text = "<<";
			btnRevPlay.UseVisualStyleBackColor = true;
			btnRevPlay.Click += btnRevPlay_Click;
			// 
			// btnRevStep
			// 
			btnRevStep.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			btnRevStep.Location = new System.Drawing.Point(38, 5);
			btnRevStep.Name = "btnRevStep";
			btnRevStep.Size = new System.Drawing.Size(31, 23);
			btnRevStep.TabIndex = 1;
			btnRevStep.Text = "|<";
			btnRevStep.UseVisualStyleBackColor = true;
			btnRevStep.Click += btnRevStep_Click;
			// 
			// cboTimestep
			// 
			cboTimestep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboTimestep.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			cboTimestep.FormattingEnabled = true;
			cboTimestep.Location = new System.Drawing.Point(5, 34);
			cboTimestep.Name = "cboTimestep";
			cboTimestep.Size = new System.Drawing.Size(163, 21);
			cboTimestep.TabIndex = 5;
			cboTimestep.SelectedIndexChanged += cboTimestep_SelectedIndexChanged;
			// 
			// btnStop
			// 
			btnStop.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			btnStop.Location = new System.Drawing.Point(71, 5);
			btnStop.Name = "btnStop";
			btnStop.Size = new System.Drawing.Size(31, 23);
			btnStop.TabIndex = 2;
			btnStop.Text = "||";
			btnStop.UseVisualStyleBackColor = true;
			btnStop.Click += btnStop_Click;
			// 
			// btnForPlay
			// 
			btnForPlay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			btnForPlay.Location = new System.Drawing.Point(137, 5);
			btnForPlay.Name = "btnForPlay";
			btnForPlay.Size = new System.Drawing.Size(31, 23);
			btnForPlay.TabIndex = 4;
			btnForPlay.Text = ">>";
			btnForPlay.UseVisualStyleBackColor = true;
			btnForPlay.Click += btnForPlay_Click;
			// 
			// btnForStep
			// 
			btnForStep.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			btnForStep.Location = new System.Drawing.Point(104, 5);
			btnForStep.Name = "btnForStep";
			btnForStep.Size = new System.Drawing.Size(31, 23);
			btnForStep.TabIndex = 3;
			btnForStep.Text = ">|";
			btnForStep.UseVisualStyleBackColor = true;
			btnForStep.Click += btnForStep_Click;
			// 
			// SimulationControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(pnlSimulation);
			Font = new System.Drawing.Font("Tahoma", 8.25F);
			Name = "SimulationControl";
			Size = new System.Drawing.Size(173, 60);
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
