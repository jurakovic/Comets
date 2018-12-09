﻿using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using Comets.BusinessLayer.Managers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Comets.Application.Controls.Database
{
	public partial class EphemerisControl : UserControl
	{
		#region Const

		private readonly string IndicatorIdentifier = "Indicator";

		#endregion

		#region Properties

		private Comet SelectedComet { get; set; }
		private Ephemeris PreviousEphemeris { get; set; }

		#endregion

		#region Constructor

		public EphemerisControl()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		public void DataBind(Comet c)
		{
			this.SelectedComet = c;
			this.PreviousEphemeris = EphemerisManager.GetEphemeris(c, DateTime.Now.AddMinutes(-1).JD(), CommonManager.Settings.Location);

			string format6 = "0.000000";
			int minPeriod = 1000;

			txtName.Text = c.full;

			txtNextPerihDate.Text = EphemerisManager.JDToDateTime(c.Tn).ToLocalTime().ToString(DateTimeFormat.Full);
			txtPeriod.Text = c.P < minPeriod ? c.P.ToString(format6) : String.Empty;
			txtAphSunDist.Text = c.P < minPeriod ? c.Q.ToString(format6) : String.Empty;

			txtPerihDist.Text = c.q.ToString(format6);
			txtPerihEarthDist.Text = c.PerihEarthDist.ToString(format6);
			txtPerihMag.Text = c.PerihMag.ToString("0.00");

			CalculateEphemeris();
		}

		public void CalculateEphemeris()
		{
			if (SelectedComet != null)
			{
				Ephemeris ep = EphemerisManager.GetEphemeris(SelectedComet, DateTime.Now.JD(), CommonManager.Settings.Location);

				txtCurrSunDist.Text = ep.SunDist.ToString("0.000000");
				txtCurrEarthDist.Text = ep.EarthDist.ToString("0.000000");
				txtCurrMag.Text = ep.Magnitude.ToString("0.00");

				txtRA.Text = EphemerisManager.HMSString(ep.RA / 15.0).Trim();
				txtDec.Text = EphemerisManager.AngleString(ep.Dec, false, true).Trim();
				txtAlt.Text = ep.Alt.ToString("0.00") + "°";
				txtAz.Text = ep.Az.ToString("0.00") + "°";
				txtElongation.Text = ep.Elongation.ToString("0.00") + "°" + (ep.PositionAngle >= 180 ? " W" : " E");

				bool rHigher = ep.SunDist >= PreviousEphemeris.SunDist;
				bool dHigher = ep.EarthDist >= PreviousEphemeris.EarthDist;
				bool mHigher = ep.Magnitude >= PreviousEphemeris.Magnitude;
				bool raHigher = ep.RA >= PreviousEphemeris.RA;
				bool decHigher = ep.Dec >= PreviousEphemeris.Dec;
				bool altHigher = ep.Alt >= PreviousEphemeris.Alt;
				bool azHigher = ep.Az >= PreviousEphemeris.Az;
				bool eloHigher = ep.Elongation >= PreviousEphemeris.Elongation;

				string up = "▲";
				string down = "▼";

				lblSunDistIndicator.Text = rHigher ? up : down;
				lblSunDistIndicator.ForeColor = rHigher ? Color.Red : Color.Green;

				lblEarthDistIndicator.Text = dHigher ? up : down;
				lblEarthDistIndicator.ForeColor = dHigher ? Color.Red : Color.Green;

				lblMagIndicator.Text = mHigher ? up : down;
				lblMagIndicator.ForeColor = mHigher ? Color.Red : Color.Green;

				lblRaIndicator.Text = raHigher ? up : down;
				lblRaIndicator.ForeColor = Color.Black;

				lblDecIndicator.Text = decHigher ? up : down;

				if (CommonManager.Settings.Location.Latitude >= 0)
					lblDecIndicator.ForeColor = decHigher ? Color.Green : Color.Red;
				else //for southern hemisphere it is better if dec is lower -> higher on sky
					lblDecIndicator.ForeColor = decHigher ? Color.Red : Color.Green;

				lblAltIndicator.Text = altHigher ? up : down;
				lblAltIndicator.ForeColor = altHigher ? Color.Green : Color.Red;

				lblAzIndicator.Text = azHigher ? up : down;
				lblAzIndicator.ForeColor = Color.Black;

				lblElongationIndicator.Text = eloHigher ? up : down;
				lblElongationIndicator.ForeColor = eloHigher ? Color.Green : Color.Red;

				PreviousEphemeris = ep;
			}
		}

		public void ClearText()
		{
			this.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
			this.Controls.OfType<Label>().Where(x => x.Name.EndsWith(IndicatorIdentifier)).ToList().ForEach(x => x.Text = String.Empty);
		}

		#endregion
	}
}
