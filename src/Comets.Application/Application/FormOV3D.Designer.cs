using OpenTK.WinForms;
using System;
using System.Windows.Forms;

namespace Comets.Application.Application
{
	partial class FormOV3D
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
			this.glControl = new GLControl();
			this.SuspendLayout();
			// 
			// glControl
			// 
			this.glControl.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
			this.glControl.APIVersion = new Version(3, 3, 0, 0);
			this.glControl.BackColor = System.Drawing.Color.Black;
			this.glControl.Dock = DockStyle.Fill;
			this.glControl.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
			this.glControl.IsEventDriven = true;
			this.glControl.Location = new System.Drawing.Point(0, 0);
			this.glControl.Name = "glControl";
			this.glControl.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
			this.glControl.Size = new System.Drawing.Size(800, 450);
			this.glControl.TabIndex = 1;
			this.glControl.Load += this.glControl_Load;
			this.glControl.Paint += this.glControl_Paint;
			this.glControl.KeyDown += this.glControl_KeyDown;
			this.glControl.Resize += this.glControl_Resize;
			// 
			// FormOV3D
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.glControl);
			this.Name = "FormOV3D";
			this.Text = "FormOV3D";
			this.FormClosed += this.FormOV3D_FormClosed;
			this.Load += this.FormOV3D_Load;
			this.Shown += this.FormOV3D_Shown;
			this.ResumeLayout(false);
		}

		private GLControl glControl;

		#endregion

		private GLControl glControl1;
	}
}
