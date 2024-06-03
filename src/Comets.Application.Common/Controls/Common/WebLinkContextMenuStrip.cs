using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comets.Application.Common.Controls.Common
{

	public class WebLinkContextMenuStrip : ContextMenuStrip
	{
		private IContainer Container;

		private System.Windows.Forms.ToolStripMenuItem mnuDefault;

		public WebLinkContextMenuStrip(IContainer c)
			: base(c)
		{
			Container = c;

			this.SuspendLayout();

			this.Name = "ctxMenu";
			this.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.Size = new System.Drawing.Size(211, 320);

			this.mnuDefault = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDefault.MergeIndex = 0;
			this.mnuDefault.Name = "mnuDefault";
			this.mnuDefault.Size = new System.Drawing.Size(210, 22);
			this.mnuDefault.Text = "Default";

			this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.mnuDefault });

			this.ResumeLayout(false);
		}
	}
}
