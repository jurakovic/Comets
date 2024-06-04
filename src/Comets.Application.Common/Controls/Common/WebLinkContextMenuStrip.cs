using Comets.Core;
using Comets.Core.Managers;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Comets.Application.Common.Controls.Common
{
	public class WebLinkContextMenuStrip : ContextMenuStrip
	{
		public Comet SelectedComet { get; set; }

		private ToolStripMenuItem mnuNasaJpl;

		public WebLinkContextMenuStrip(IContainer c)
			: base(c)
		{
			this.SuspendLayout();

			this.Name = "ctxMenu";
			this.RenderMode = ToolStripRenderMode.System;
			this.Size = new System.Drawing.Size(211, 320);

			this.mnuNasaJpl = new ToolStripMenuItem();
			this.mnuNasaJpl.MergeIndex = 0;
			this.mnuNasaJpl.Name = nameof(this.mnuNasaJpl);
			this.mnuNasaJpl.Size = new System.Drawing.Size(210, 22);
			this.mnuNasaJpl.Text = "NASA JPL";
			this.mnuNasaJpl.Click += this.mnumnuNasaJpl_Click;

			this.Items.AddRange(new ToolStripItem[] { this.mnuNasaJpl });

			this.ResumeLayout(false);
		}

		private void mnumnuNasaJpl_Click(object sender, EventArgs e)
		{
			CometManager.OpenJplInfo(SelectedComet.id);
		}
	}
}
