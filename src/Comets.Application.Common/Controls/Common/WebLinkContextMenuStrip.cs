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
		private ToolStripMenuItem mnuAerith;

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

			int ix = 0;
			this.mnuNasaJpl = CreateToolStripMenuItem(nameof(this.mnuNasaJpl), "NASA JPL", ix++, this.mnuNasaJpl_Click);
			this.mnuVanBuitenen = CreateToolStripMenuItem(nameof(this.mnuVanBuitenen), "astro.vanbuitenen.nl", ix++, this.mnuVanBuitenen_Click);
			this.mnuAerith = CreateToolStripMenuItem(nameof(this.mnuAerith), "Aerith", ix++, this.mnuAerith_Click);

			this.Items.AddRange([this.mnuNasaJpl, this.mnuVanBuitenen, this.mnuAerith]);

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

		private void mnuAerith_Click(object sender, EventArgs e)
		{
			OpenAerithInfo(SelectedComet.id);
		}

		#endregion

		#region Methods

		private ToolStripMenuItem CreateToolStripMenuItem(string name, string text, int mergeIndex, EventHandler eventHandler)
		{
			ToolStripMenuItem retval = new ToolStripMenuItem();
			retval.MergeIndex = mergeIndex;
			retval.Name = name;
			retval.Size = new System.Drawing.Size(210, 22);
			retval.Text = text;
			retval.Click += eventHandler;
			return retval;
		}

		private void OpenUrl(string url)
		{
			Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
		}

		private void OpenJplInfo(string id)
		{
			OpenUrl($"https://ssd.jpl.nasa.gov/tools/sbdb_lookup.html#/?sstr={id}&view=VOP");
		}

		private void OpenVanBuitenenInfo(string id)
		{
			string query;

			if (Char.IsDigit(id[0])) // 1P
				query = id.EndsWith("I") ? id : id.Substring(0, id.Length - 1);
			else
				query = id.Substring(2).Replace(" ", "");

			OpenUrl($"http://astro.vanbuitenen.nl/comet/{query}");
		}

		private void OpenAerithInfo(string id)
		{
			string code;

			if (Char.IsDigit(id[0])) // 1P
				code = id.PadLeft(5, '0');
			else
				code = id.Substring(2).Replace(" ", "");

			OpenUrl($"http://www.aerith.net/comet/catalog/{code}/index.html");
		}

		#endregion
	}
}
