namespace Comets.Application.OrbitViewer
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
			orbitViewerControl = new OrbitViewerControl();
			SuspendLayout();
			// 
			// orbitViewerControl
			// 
			orbitViewerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			orbitViewerControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			orbitViewerControl.Location = new System.Drawing.Point(0, 0);
			orbitViewerControl.MinimumSize = new System.Drawing.Size(720, 650);
			orbitViewerControl.Name = "orbitViewerControl";
			orbitViewerControl.Size = new System.Drawing.Size(934, 811);
			orbitViewerControl.TabIndex = 0;
			// 
			// FormOrbitViewer
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(934, 811);
			Controls.Add(orbitViewerControl);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			KeyPreview = true;
			MinimumSize = new System.Drawing.Size(720, 650);
			Name = "FormOrbitViewer";
			ShowIcon = false;
			ShowInTaskbar = false;
			Text = "Orbit Viewer";
			Activated += FormOrbitViewer_Activated;
			Deactivate += FormOrbitViewer_Deactivate;
			KeyDown += FormOrbitViewer_KeyDown;
			ResumeLayout(false);
		}

		#endregion

		private OrbitViewer.OrbitViewerControl orbitViewerControl;
	}
}