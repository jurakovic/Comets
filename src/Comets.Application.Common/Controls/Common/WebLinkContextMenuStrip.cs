using Comets.Core;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Comets.Application.Common.Controls.Common
{
	public class WebLinkContextMenuStrip : ContextMenuStrip
	{
		#region Fields

		private ToolStripMenuItem mnuNasaJpl;
		private ToolStripMenuItem mnuVanBuitenen;

		#endregion

		#region Properties

		public Comet SelectedComet { get; set; }

		#endregion

		#region Constructor

		public WebLinkContextMenuStrip(IContainer container)
			: base(container)
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
			this.mnuNasaJpl.Click += this.mnuNasaJpl_Click;

			this.mnuVanBuitenen = new ToolStripMenuItem();
			this.mnuVanBuitenen.MergeIndex = 0;
			this.mnuVanBuitenen.Name = nameof(this.mnuVanBuitenen);
			this.mnuVanBuitenen.Size = new System.Drawing.Size(210, 22);
			this.mnuVanBuitenen.Text = "astro.vanbuitenen.nl";
			this.mnuVanBuitenen.Click += this.mnuVanBuitenen_Click;

			this.Items.AddRange(new ToolStripItem[] { this.mnuNasaJpl, this.mnuVanBuitenen });

			this.ResumeLayout(false);
		}

		#endregion

		#region EventHandling

		private void mnuNasaJpl_Click(object sender, EventArgs e)
		{
			OpenJplInfo(SelectedComet.id);
		}

		private void mnuVanBuitenen_Click(object sender, EventArgs e)
		{
			OpenVanBuitenenInfo(SelectedComet.id);
		}

		#endregion

		#region Methods

		public static void OpenJplInfo(string id)
		{
			Process.Start(new ProcessStartInfo("https://ssd.jpl.nasa.gov/tools/sbdb_lookup.html#/?sstr=" + id) { UseShellExecute = true });
		}

		public static void OpenVanBuitenenInfo(string id)
		{
			string query;

			if (Char.IsDigit(id[0])) // 1P
				query = id.EndsWith("I") ? id : id.Substring(0, id.Length - 1);
			else
				query = id.Substring(2, id.Length - 2).Replace(" ", "");

			Process.Start(new ProcessStartInfo("http://astro.vanbuitenen.nl/comet/" + query) { UseShellExecute = true });
		}

		#endregion
	}
}
