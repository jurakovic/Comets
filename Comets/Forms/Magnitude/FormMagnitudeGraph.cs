using System.Windows.Forms;
using Comets.Classes;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Linq;
using System;
using Comets.Helpers;

namespace Comets.Forms.Magnitude
{
    public partial class FormMagnitudeGraph : Form
    {
        #region Properties

        public GraphSettings GraphSettings { get; set; }

        #endregion

        #region Constructor

        public FormMagnitudeGraph(GraphSettings settings, int tag)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.GraphSettings = settings;
            this.Tag = tag;
        }

        #endregion

        #region Form_Load

        private void FormMagnitude_Load(object sender, System.EventArgs e)
        {
            FormMain main = this.MdiParent as FormMain;
            main.SetWindowMenuItemVisible(true);

            LoadGraph();
        }

        #endregion

        #region Form_Closing

        private void FormMagnitudeGraph_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain main = this.MdiParent as FormMain;
            main.RemoveWindowMenuItem((int)this.Tag);
            main.SetWindowMenuItemVisible(main.MdiChildren.Length > 1 ? true : false);
        }

        #endregion

        #region LoadGraph

        public void LoadGraph()
        {
            this.Text = this.Tag + " " + GraphSettings.ToString();

            string xDate = "Date";
            string yMag = "Magnitude";
            string chartAreaName = "ChartAreaGraph";

            double min = GraphSettings.Results.Min(x => x.Magnitude);
            double max = GraphSettings.Results.Max(x => x.Magnitude);

            min = Math.Floor(min - 0.25);
            max = Math.Ceiling(max + 0.25);

            //min = 0; max = 20;

            double JDfrom = GraphSettings.Results.Min(x => x.UtcJD);
            double JDto = GraphSettings.Results.Max(x => x.UtcJD);
            double T = GraphSettings.Comet.T;

            this.chart1.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.Text;

            ChartArea chartArea = new ChartArea();
            chartArea.AxisX2.MajorGrid.Enabled = false;
            chartArea.AxisX2.IsLabelAutoFit = false;
            chartArea.AxisX2.IsMarginVisible = false;
            chartArea.AxisX2.LabelStyle.Font = new System.Drawing.Font("Tahoma", 8.25F);
            chartArea.AxisY.IsReversed = true;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Tahoma", 8.25F);
            chartArea.AxisY.IsLabelAutoFit = false;
            chartArea.AxisY.IsMarginVisible = false;
            chartArea.AxisY.MajorTickMark.Size = 0.5F;
            chartArea.Name = chartAreaName;

            Double interval = 0D;

            if (max - min <= 1)
                interval = 0.1D;
            else if (max - min <= 2)
                interval = 0.2D;
            else if (max - min <= 5)
                interval = 0.5D;
            else if (max - min <= 10)
                interval = 1D;
            else
                interval = 2D;

            chartArea.AxisY.Interval = interval;

            chartArea.AxisX2.Interval = (JDto - JDfrom) / 10;

            this.chart1.ChartAreas.Clear();
            this.chart1.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.ChartArea = chartAreaName;
            series.Color = System.Drawing.Color.Red;
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series.XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;

            foreach (EphemerisResult er in GraphSettings.Results)
                series.Points.Add(new DataPoint(er.UtcJD, er.Magnitude));

            this.chart1.Series.Clear();
            this.chart1.Series.Add(series);

            this.chart1.ChartAreas[chartAreaName].AxisY.Minimum = min;
            this.chart1.ChartAreas[chartAreaName].AxisY.Maximum = max;

            //margine
            //this.chart1.ChartAreas[chartAreaName].AxisX2.Minimum = JDfrom - (JDto - JDfrom) * 0.02;
            //this.chart1.ChartAreas[chartAreaName].AxisX2.Maximum = JDto + (JDto - JDfrom) * 0.02;

            //generate perihelion lines
            double periodDays = GraphSettings.Comet.P * 365.25;

            if ((JDto - JDfrom > periodDays) || (JDfrom < T && JDto > T))
                while (T > JDfrom + periodDays)
                    T -= periodDays;
            else
                T += periodDays;

            while (T < JDto)
            {
                Series s = new Series();
                s.ChartArea = chartAreaName;
                s.Color = System.Drawing.Color.RoyalBlue;
                s.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                s.XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                s.Points.Add(new DataPoint(T, min));
                s.Points.Add(new DataPoint(T, max));
                this.chart1.Series.Add(s);
                T += periodDays;
            }
            //end

            Title title = new Title(GraphSettings.Comet.ToString());
            title.Font = new System.Drawing.Font("Tahoma", 11.25F);

            Title title2 = new Title(xDate);
            title2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            title2.Position.Auto = false;
            title2.Position.X = 95F;
            title2.Position.Y = 91F;

            Title title3 = new Title(yMag);
            title3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            title3.Position.Auto = false;
            title3.Position.X = 5.5F;
            title3.Position.Y = 7F;

            this.chart1.Titles.Clear();
            this.chart1.Titles.Add(title);
            this.chart1.Titles.Add(title2);
            this.chart1.Titles.Add(title3);
        }

        #endregion
    }
}
