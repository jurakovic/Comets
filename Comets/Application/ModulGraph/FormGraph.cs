using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Managers;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Comets.Application.ModulGraph
{
	public partial class FormGraph : Form
	{
		#region Properties

		public GraphSettings GraphSettings { get; set; }

		Point LastPoint { get; set; }

		ToolTip ToolTip { get; set; }

		#endregion

		#region Constructor

		public FormGraph(GraphSettings settings)
		{
			InitializeComponent();
			this.DoubleBuffered = true;
			this.GraphSettings = settings;
			this.LastPoint = new Point();
			this.ToolTip = new ToolTip();
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
				string lastExportDir = CommonManager.Settings.LastUsedExportDirectory;

				sfd.InitialDirectory = !String.IsNullOrEmpty(lastExportDir) ? lastExportDir : Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
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
					CommonManager.Settings.LastUsedExportDirectory = Path.GetDirectoryName(sfd.FileName);
					MessageBox.Show(String.Format("Graph saved as {0}\t\t\t", sfd.FileName), "Comets", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		#endregion

		#region MouseMove

		private void chart1_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Location != LastPoint)
			{
				ToolTip.RemoveAll();
				LastPoint = e.Location;

				HitTestResult result = chart1.HitTest(LastPoint.X, LastPoint.Y, ChartElementType.DataPoint);

				if (result.ChartElementType == ChartElementType.DataPoint)
				{
					DataPoint prop = result.Object as DataPoint;

					if (prop != null)
					{
						string message = String.Empty;

						string comet = String.Empty;
						string datetime = DateTime.FromOADate(prop.XValue).ToString(FormMain.DateTimeFormat);
						double value = prop.YValues[0];

						string series = result.Series.Tag as string;

						if (series.StartsWith(EphemerisManager.SeriesNow))
						{
							message = String.Format("{0}", datetime);
						}
						else if (series.StartsWith(EphemerisManager.SeriesPerihelion))
						{
							comet = series.Replace(EphemerisManager.SeriesPerihelion, String.Empty);
							message = String.Format("{0}, {1}", comet, datetime);
						}
						else if (series.StartsWith(EphemerisManager.SeriesValue))
						{
							comet = series.Replace(EphemerisManager.SeriesValue, String.Empty);

							string format = GraphSettings.GraphChartType == GraphSettings.ChartType.Magnitude
								? "{0}, {1}, {2:0.00}"
								: "{0}, {1}, {2:0.0000} AU";

							message = String.Format(format, comet, datetime, value);
						}

						ToolTip.Show(message, this.chart1, LastPoint.X, LastPoint.Y - 15);
					}
				}
			}
		}

		#endregion
	}
}
