using Comets.Application.Common.General;
using Comets.Core;
using Comets.Core.Managers;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Comets.Application.Graph
{
	public partial class FormGraph : ThemeForm, ISave
	{
		#region Properties

		public GraphSettings GraphSettings { get; set; }

		private IProgress<int> Progress { get; set; }

		Point LastPoint { get; set; }

		ToolTip ToolTip { get; set; }

		#endregion

		#region Constructor

		public FormGraph(GraphSettings settings, IProgress<int> progress)
		{
			InitializeComponent();
			this.DoubleBuffered = true;
			this.GraphSettings = settings;
			this.Progress = progress;
			this.LastPoint = new Point();
			this.ToolTip = new ToolTip();
		}

		#endregion

		#region LoadGraph

		public void LoadGraph()
		{
			this.Text = GraphSettings.ToString();
			EphemerisManager.GenerateGraph(GraphSettings, this.chart1, this.Progress);
			GraphSettings.AddNew = false;
			GraphSettings.Ephemerides.Clear();
			GC.Collect();
		}

		#endregion

		#region ISave

		public void Save()
		{
			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				string lastExportDir = CommonManager.Settings.LastUsedExportDirectory;

				sfd.InitialDirectory = !String.IsNullOrEmpty(lastExportDir) ? lastExportDir : Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
				sfd.FileName = "Comets_Graph_" + DateTime.Now.ToString(DateTimeFormat.Filename);
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

		#region EventHandling

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
						DateTime dt = new DateTime(DateTime.FromOADate(prop.XValue).Ticks, DateTimeKind.Utc);

						int offset = 0;
						string title = String.Empty;
						string message = String.Empty;
						string date = dt/*.ToLocalTime()*/.ToString(DateTimeFormat.Full);
						double value = prop.YValues[0];

						string series = result.Series.Tag as string;

						if (series.StartsWith(EphemerisManager.SeriesNow))
						{
							offset = 21;
							title = "Today";
							message = String.Format("Time: {0}", date);
						}
						else if (series.StartsWith(EphemerisManager.SeriesPerihelion))
						{
							offset = 37;
							title = series.Replace(EphemerisManager.SeriesPerihelion, String.Empty);
							message = String.Format("Perihelion time: {0}", date);
						}
						else if (series.StartsWith(EphemerisManager.SeriesValue))
						{
							offset = 52;
							title = series.Replace(EphemerisManager.SeriesValue, String.Empty);

							string format = GraphSettings.GraphChartType == GraphSettings.ChartType.Magnitude
								? "Time: {0}\nMagnitude: {1:0.00}"
								: "Time: {0}\nDistance: {1:0.0000} AU";

							message = String.Format(format, date, value);
						}

						ToolTip.ToolTipTitle = title;
						ToolTip.Show(message, this.chart1, LastPoint.X, LastPoint.Y - offset);
					}
				}
			}
		}

		#endregion
	}
}
