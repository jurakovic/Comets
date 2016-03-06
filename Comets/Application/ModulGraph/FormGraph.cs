using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Comets.Application.ModulGraph
{
	public partial class FormGraph : Form
	{
		#region Properties

		public GraphSettings GraphSettings { get; set; }

		#endregion

		#region Constructor

		public FormGraph(GraphSettings settings)
		{
			InitializeComponent();
			this.DoubleBuffered = true;
			this.GraphSettings = settings;
		}

		#endregion

		#region LoadGraph

		public void LoadGraph()
		{
			this.Text = GraphSettings.ToString();
			EphemerisManager.GenerateGraph(GraphSettings, this.chart1, FormMain.Progress);
			GraphSettings.AddNew = false;
			GraphSettings.Ephemerides.Clear();
			GC.Collect();
		}

		#endregion

		#region SaveGraph

		public void SaveGraph()
		{
			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				if (!String.IsNullOrEmpty(FormMain.Settings.LastUsedExportDirectory))
					sfd.InitialDirectory = FormMain.Settings.LastUsedExportDirectory;
				else
					sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

				sfd.Filter = "BMP (*.bmp)|*.bmp|" +
							"GIF (*.gif)|*.gif|" +
							"JPEG (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|" +
							"PNG (*.png)|*.png";
				sfd.FilterIndex = 4;

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					ImageFormat format;

					switch (sfd.FilterIndex)
					{
						case 1:
							format = ImageFormat.Bmp;
							break;
						case 2:
							format = ImageFormat.Gif;
							break;
						case 3:
							format = ImageFormat.Jpeg;
							break;
						default:
							format = ImageFormat.Png;
							break;
					}

					this.chart1.SaveImage(sfd.FileName, format);
					FormMain.Settings.LastUsedExportDirectory = Path.GetDirectoryName(sfd.FileName);
					MessageBox.Show(String.Format("Graph saved as {0}\t\t\t", sfd.FileName), "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		#endregion
	}
}
