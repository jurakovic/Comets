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
			this.pnlSimulation = new System.Windows.Forms.Panel();
			this.btnRevPlay = new System.Windows.Forms.Button();
			this.btnRevStep = new System.Windows.Forms.Button();
			this.cboTimestep = new System.Windows.Forms.ComboBox();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnForPlay = new System.Windows.Forms.Button();
			this.btnForStep = new System.Windows.Forms.Button();
			this.pnlSimulation.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlSimulation
			// 
			this.pnlSimulation.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pnlSimulation.Controls.Add(this.btnRevPlay);
			this.pnlSimulation.Controls.Add(this.btnRevStep);
			this.pnlSimulation.Controls.Add(this.cboTimestep);
			this.pnlSimulation.Controls.Add(this.btnStop);
			this.pnlSimulation.Controls.Add(this.btnForPlay);
			this.pnlSimulation.Controls.Add(this.btnForStep);
			this.pnlSimulation.Location = new System.Drawing.Point(0, 0);
			this.pnlSimulation.Name = "pnlSimulation";
			this.pnlSimulation.Size = new System.Drawing.Size(173, 60);
			this.pnlSimulation.TabIndex = 0;
			// 
			// btnRevPlay
			// 
			this.btnRevPlay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnRevPlay.Location = new System.Drawing.Point(5, 5);
			this.btnRevPlay.Name = "btnRevPlay";
			this.btnRevPlay.Size = new System.Drawing.Size(31, 23);
			this.btnRevPlay.TabIndex = 0;
			this.btnRevPlay.Text = "<<";
			this.btnRevPlay.UseVisualStyleBackColor = true;
			this.btnRevPlay.Click += new System.EventHandler(this.btnRevPlay_Click);
			// 
			// btnRevStep
			// 
			this.btnRevStep.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnRevStep.Location = new System.Drawing.Point(38, 5);
			this.btnRevStep.Name = "btnRevStep";
			this.btnRevStep.Size = new System.Drawing.Size(31, 23);
			this.btnRevStep.TabIndex = 1;
			this.btnRevStep.Text = "|<";
			this.btnRevStep.UseVisualStyleBackColor = true;
			this.btnRevStep.Click += new System.EventHandler(this.btnRevStep_Click);
			// 
			// cboTimestep
			// 
			this.cboTimestep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTimestep.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.cboTimestep.FormattingEnabled = true;
			this.cboTimestep.Location = new System.Drawing.Point(5, 34);
			this.cboTimestep.Name = "cboTimestep";
			this.cboTimestep.Size = new System.Drawing.Size(163, 21);
			this.cboTimestep.TabIndex = 5;
			this.cboTimestep.SelectedIndexChanged += new System.EventHandler(this.cboTimestep_SelectedIndexChanged);
			// 
			// btnStop
			// 
			this.btnStop.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnStop.Location = new System.Drawing.Point(71, 5);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(31, 23);
			this.btnStop.TabIndex = 2;
			this.btnStop.Text = "||";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnForPlay
			// 
			this.btnForPlay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnForPlay.Location = new System.Drawing.Point(137, 5);
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
			this.btnForStep.Location = new System.Drawing.Point(104, 5);
			this.btnForStep.Name = "btnForStep";
			this.btnForStep.Size = new System.Drawing.Size(31, 23);
			this.btnForStep.TabIndex = 3;
			this.btnForStep.Text = ">|";
			this.btnForStep.UseVisualStyleBackColor = true;
			this.btnForStep.Click += new System.EventHandler(this.btnForStep_Click);
			// 
			// SimulationControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlSimulation);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "SimulationControl";
			this.Size = new System.Drawing.Size(173, 60);
			this.Load += new System.EventHandler(this.SimulationControl_Load);
			this.pnlSimulation.ResumeLayout(false);
			this.ResumeLayout(false);

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
