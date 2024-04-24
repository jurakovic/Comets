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
			this.orbitViewerControl = new Comets.Application.OrbitViewer.OrbitViewerControl();
			this.SuspendLayout();
			// 
			// orbitViewerControl
			// 
			this.orbitViewerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.orbitViewerControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.orbitViewerControl.Location = new System.Drawing.Point(0, 0);
			this.orbitViewerControl.MinimumSize = new System.Drawing.Size(720, 650);
			this.orbitViewerControl.Name = "orbitViewerControl";
			this.orbitViewerControl.Size = new System.Drawing.Size(934, 811);
			this.orbitViewerControl.TabIndex = 0;
			// 
			// FormOrbitViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(934, 811);
			this.Controls.Add(this.orbitViewerControl);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(720, 650);
			this.Name = "FormOrbitViewer";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Orbit Viewer";
			this.Activated += new System.EventHandler(this.FormOrbitViewer_Activated);
			this.Deactivate += new System.EventHandler(this.FormOrbitViewer_Deactivate);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOrbitViewer_KeyDown);
			this.ResumeLayout(false);

		}

		#endregion

		private OrbitViewer.OrbitViewerControl orbitViewerControl;
	}
}