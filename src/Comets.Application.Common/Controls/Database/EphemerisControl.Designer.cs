namespace Comets.Application.Common.Controls.Database
{
	partial class EphemerisControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			txtElemSortkey = new TextBox();
			lblElemSortkey = new Label();
			lblAzIndicator = new Label();
			lblDecIndicator = new Label();
			lblSunDistIndicator = new Label();
			lblMagIndicator = new Label();
			lblEarthDistIndicator = new Label();
			lblElongationIndicator = new Label();
			lblAltIndicator = new Label();
			lblRaIndicator = new Label();
			lblRA = new Label();
			lblAlt = new Label();
			lblAz = new Label();
			lblElongation = new Label();
			txtAz = new TextBox();
			txtDec = new TextBox();
			txtAlt = new TextBox();
			txtRA = new TextBox();
			txtElongation = new TextBox();
			lblDec = new Label();
			txtName = new TextBox();
			lblPerihEarthDistAU = new Label();
			txtCurrMag = new TextBox();
			lblAphDist = new Label();
			lblPerihDist = new Label();
			lblAphSunDistAU = new Label();
			lblPerihDistAU = new Label();
			txtAphSunDist = new TextBox();
			lblPerihEarthDist = new Label();
			lblName = new Label();
			lblCurrEarthDist = new Label();
			lblPerihMag = new Label();
			lblCurrEarthDistAU = new Label();
			txtPeriod = new TextBox();
			lblCurrMagg = new Label();
			lblPeriodYears = new Label();
			txtCurrEarthDist = new TextBox();
			txtNextPerihDate = new TextBox();
			txtCurrSunDist = new TextBox();
			lblNextPerihDate = new Label();
			txtPerihEarthDist = new TextBox();
			lblCurrSunDistAU = new Label();
			lblPeriod = new Label();
			txtPerihDist = new TextBox();
			txtPerihMag = new TextBox();
			lblCurrSunDist = new Label();
			selectDateControl = new DateAndTime.SelectDateControl();
			SuspendLayout();
			// 
			// txtElemSortkey
			// 
			txtElemSortkey.BackColor = SystemColors.Window;
			txtElemSortkey.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtElemSortkey.Location = new Point(446, 30);
			txtElemSortkey.Margin = new Padding(4, 3, 4, 3);
			txtElemSortkey.Name = "txtElemSortkey";
			txtElemSortkey.ReadOnly = true;
			txtElemSortkey.Size = new Size(135, 22);
			txtElemSortkey.TabIndex = 34;
			txtElemSortkey.Visible = false;
			// 
			// lblElemSortkey
			// 
			lblElemSortkey.AutoSize = true;
			lblElemSortkey.Font = new Font("Tahoma", 8F);
			lblElemSortkey.ForeColor = SystemColors.GrayText;
			lblElemSortkey.Location = new Point(444, 8);
			lblElemSortkey.Margin = new Padding(4, 0, 4, 0);
			lblElemSortkey.Name = "lblElemSortkey";
			lblElemSortkey.Size = new Size(44, 13);
			lblElemSortkey.TabIndex = 33;
			lblElemSortkey.Text = "Sortkey";
			lblElemSortkey.Visible = false;
			// 
			// lblAzIndicator
			// 
			lblAzIndicator.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblAzIndicator.ForeColor = Color.Black;
			lblAzIndicator.Location = new Point(219, 292);
			lblAzIndicator.Margin = new Padding(4, 0, 4, 0);
			lblAzIndicator.Name = "lblAzIndicator";
			lblAzIndicator.Size = new Size(15, 17);
			lblAzIndicator.TabIndex = 31;
			lblAzIndicator.Text = "▲";
			// 
			// lblDecIndicator
			// 
			lblDecIndicator.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblDecIndicator.ForeColor = Color.Black;
			lblDecIndicator.Location = new Point(9, 292);
			lblDecIndicator.Margin = new Padding(4, 0, 4, 0);
			lblDecIndicator.Name = "lblDecIndicator";
			lblDecIndicator.Size = new Size(15, 17);
			lblDecIndicator.TabIndex = 15;
			lblDecIndicator.Text = "▲";
			// 
			// lblSunDistIndicator
			// 
			lblSunDistIndicator.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblSunDistIndicator.ForeColor = Color.Black;
			lblSunDistIndicator.Location = new Point(9, 189);
			lblSunDistIndicator.Margin = new Padding(4, 0, 4, 0);
			lblSunDistIndicator.Name = "lblSunDistIndicator";
			lblSunDistIndicator.Size = new Size(15, 17);
			lblSunDistIndicator.TabIndex = 8;
			lblSunDistIndicator.Text = "▲";
			// 
			// lblMagIndicator
			// 
			lblMagIndicator.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblMagIndicator.ForeColor = Color.Black;
			lblMagIndicator.Location = new Point(426, 188);
			lblMagIndicator.Margin = new Padding(4, 0, 4, 0);
			lblMagIndicator.Name = "lblMagIndicator";
			lblMagIndicator.Size = new Size(15, 17);
			lblMagIndicator.TabIndex = 41;
			lblMagIndicator.Text = "▲";
			// 
			// lblEarthDistIndicator
			// 
			lblEarthDistIndicator.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblEarthDistIndicator.ForeColor = Color.Black;
			lblEarthDistIndicator.Location = new Point(219, 188);
			lblEarthDistIndicator.Margin = new Padding(4, 0, 4, 0);
			lblEarthDistIndicator.Name = "lblEarthDistIndicator";
			lblEarthDistIndicator.Size = new Size(15, 17);
			lblEarthDistIndicator.TabIndex = 24;
			lblEarthDistIndicator.Text = "▲";
			// 
			// lblElongationIndicator
			// 
			lblElongationIndicator.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblElongationIndicator.ForeColor = Color.Black;
			lblElongationIndicator.Location = new Point(426, 240);
			lblElongationIndicator.Margin = new Padding(4, 0, 4, 0);
			lblElongationIndicator.Name = "lblElongationIndicator";
			lblElongationIndicator.Size = new Size(15, 17);
			lblElongationIndicator.TabIndex = 44;
			lblElongationIndicator.Text = "▲";
			// 
			// lblAltIndicator
			// 
			lblAltIndicator.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblAltIndicator.ForeColor = Color.Black;
			lblAltIndicator.Location = new Point(219, 240);
			lblAltIndicator.Margin = new Padding(4, 0, 4, 0);
			lblAltIndicator.Name = "lblAltIndicator";
			lblAltIndicator.Size = new Size(15, 17);
			lblAltIndicator.TabIndex = 28;
			lblAltIndicator.Text = "▲";
			// 
			// lblRaIndicator
			// 
			lblRaIndicator.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblRaIndicator.ForeColor = Color.Black;
			lblRaIndicator.Location = new Point(9, 240);
			lblRaIndicator.Margin = new Padding(4, 0, 4, 0);
			lblRaIndicator.Name = "lblRaIndicator";
			lblRaIndicator.Size = new Size(15, 17);
			lblRaIndicator.TabIndex = 12;
			lblRaIndicator.Text = "▲";
			// 
			// lblRA
			// 
			lblRA.AutoSize = true;
			lblRA.Font = new Font("Tahoma", 8F);
			lblRA.ForeColor = SystemColors.GrayText;
			lblRA.Location = new Point(27, 218);
			lblRA.Margin = new Padding(4, 0, 4, 0);
			lblRA.Name = "lblRA";
			lblRA.Size = new Size(83, 13);
			lblRA.TabIndex = 11;
			lblRA.Text = "Right Ascension";
			// 
			// lblAlt
			// 
			lblAlt.AutoSize = true;
			lblAlt.Font = new Font("Tahoma", 8F);
			lblAlt.ForeColor = SystemColors.GrayText;
			lblAlt.Location = new Point(234, 218);
			lblAlt.Margin = new Padding(4, 0, 4, 0);
			lblAlt.Name = "lblAlt";
			lblAlt.Size = new Size(44, 13);
			lblAlt.TabIndex = 27;
			lblAlt.Text = "Altitude";
			// 
			// lblAz
			// 
			lblAz.AutoSize = true;
			lblAz.Font = new Font("Tahoma", 8F);
			lblAz.ForeColor = SystemColors.GrayText;
			lblAz.Location = new Point(234, 270);
			lblAz.Margin = new Padding(4, 0, 4, 0);
			lblAz.Name = "lblAz";
			lblAz.Size = new Size(45, 13);
			lblAz.TabIndex = 30;
			lblAz.Text = "Azimuth";
			// 
			// lblElongation
			// 
			lblElongation.AutoSize = true;
			lblElongation.Font = new Font("Tahoma", 8F);
			lblElongation.ForeColor = SystemColors.GrayText;
			lblElongation.Location = new Point(443, 217);
			lblElongation.Margin = new Padding(4, 0, 4, 0);
			lblElongation.Name = "lblElongation";
			lblElongation.Size = new Size(57, 13);
			lblElongation.TabIndex = 43;
			lblElongation.Text = "Elongation";
			// 
			// txtAz
			// 
			txtAz.BackColor = SystemColors.Window;
			txtAz.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtAz.Location = new Point(239, 290);
			txtAz.Margin = new Padding(4, 3, 4, 3);
			txtAz.Name = "txtAz";
			txtAz.ReadOnly = true;
			txtAz.Size = new Size(135, 22);
			txtAz.TabIndex = 32;
			// 
			// txtDec
			// 
			txtDec.BackColor = SystemColors.Window;
			txtDec.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtDec.Location = new Point(29, 290);
			txtDec.Margin = new Padding(4, 3, 4, 3);
			txtDec.Name = "txtDec";
			txtDec.ReadOnly = true;
			txtDec.Size = new Size(135, 22);
			txtDec.TabIndex = 16;
			// 
			// txtAlt
			// 
			txtAlt.BackColor = SystemColors.Window;
			txtAlt.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtAlt.Location = new Point(239, 238);
			txtAlt.Margin = new Padding(4, 3, 4, 3);
			txtAlt.Name = "txtAlt";
			txtAlt.ReadOnly = true;
			txtAlt.Size = new Size(135, 22);
			txtAlt.TabIndex = 29;
			// 
			// txtRA
			// 
			txtRA.BackColor = SystemColors.Window;
			txtRA.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtRA.Location = new Point(29, 238);
			txtRA.Margin = new Padding(4, 3, 4, 3);
			txtRA.Name = "txtRA";
			txtRA.ReadOnly = true;
			txtRA.Size = new Size(135, 22);
			txtRA.TabIndex = 13;
			// 
			// txtElongation
			// 
			txtElongation.BackColor = SystemColors.Window;
			txtElongation.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtElongation.Location = new Point(446, 238);
			txtElongation.Margin = new Padding(4, 3, 4, 3);
			txtElongation.Name = "txtElongation";
			txtElongation.ReadOnly = true;
			txtElongation.Size = new Size(135, 22);
			txtElongation.TabIndex = 45;
			// 
			// lblDec
			// 
			lblDec.AutoSize = true;
			lblDec.Font = new Font("Tahoma", 8F);
			lblDec.ForeColor = SystemColors.GrayText;
			lblDec.Location = new Point(27, 270);
			lblDec.Margin = new Padding(4, 0, 4, 0);
			lblDec.Name = "lblDec";
			lblDec.Size = new Size(59, 13);
			lblDec.TabIndex = 14;
			lblDec.Text = "Declination";
			// 
			// txtName
			// 
			txtName.BackColor = SystemColors.Window;
			txtName.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtName.Location = new Point(29, 30);
			txtName.Margin = new Padding(4, 3, 4, 3);
			txtName.Name = "txtName";
			txtName.ReadOnly = true;
			txtName.Size = new Size(345, 22);
			txtName.TabIndex = 1;
			// 
			// lblPerihEarthDistAU
			// 
			lblPerihEarthDistAU.AutoSize = true;
			lblPerihEarthDistAU.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblPerihEarthDistAU.ForeColor = SystemColors.WindowText;
			lblPerihEarthDistAU.Location = new Point(374, 137);
			lblPerihEarthDistAU.Margin = new Padding(4, 0, 4, 0);
			lblPerihEarthDistAU.Name = "lblPerihEarthDistAU";
			lblPerihEarthDistAU.Size = new Size(23, 14);
			lblPerihEarthDistAU.TabIndex = 22;
			lblPerihEarthDistAU.Text = "AU";
			// 
			// txtCurrMag
			// 
			txtCurrMag.BackColor = SystemColors.Window;
			txtCurrMag.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtCurrMag.Location = new Point(446, 186);
			txtCurrMag.Margin = new Padding(4, 3, 4, 3);
			txtCurrMag.Name = "txtCurrMag";
			txtCurrMag.ReadOnly = true;
			txtCurrMag.Size = new Size(135, 22);
			txtCurrMag.TabIndex = 42;
			// 
			// lblAphDist
			// 
			lblAphDist.AutoSize = true;
			lblAphDist.Font = new Font("Tahoma", 8F);
			lblAphDist.ForeColor = SystemColors.GrayText;
			lblAphDist.Location = new Point(443, 62);
			lblAphDist.Margin = new Padding(4, 0, 4, 0);
			lblAphDist.Name = "lblAphDist";
			lblAphDist.Size = new Size(91, 13);
			lblAphDist.TabIndex = 35;
			lblAphDist.Text = "Aphelion distance";
			// 
			// lblPerihDist
			// 
			lblPerihDist.AutoSize = true;
			lblPerihDist.Font = new Font("Tahoma", 8F);
			lblPerihDist.ForeColor = SystemColors.GrayText;
			lblPerihDist.Location = new Point(27, 114);
			lblPerihDist.Margin = new Padding(4, 0, 4, 0);
			lblPerihDist.Name = "lblPerihDist";
			lblPerihDist.Size = new Size(96, 13);
			lblPerihDist.TabIndex = 4;
			lblPerihDist.Text = "Perihelion distance";
			// 
			// lblAphSunDistAU
			// 
			lblAphSunDistAU.AutoSize = true;
			lblAphSunDistAU.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblAphSunDistAU.ForeColor = SystemColors.WindowText;
			lblAphSunDistAU.Location = new Point(581, 85);
			lblAphSunDistAU.Margin = new Padding(4, 0, 4, 0);
			lblAphSunDistAU.Name = "lblAphSunDistAU";
			lblAphSunDistAU.Size = new Size(23, 14);
			lblAphSunDistAU.TabIndex = 37;
			lblAphSunDistAU.Text = "AU";
			// 
			// lblPerihDistAU
			// 
			lblPerihDistAU.AutoSize = true;
			lblPerihDistAU.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblPerihDistAU.ForeColor = SystemColors.WindowText;
			lblPerihDistAU.Location = new Point(164, 137);
			lblPerihDistAU.Margin = new Padding(4, 0, 4, 0);
			lblPerihDistAU.Name = "lblPerihDistAU";
			lblPerihDistAU.Size = new Size(23, 14);
			lblPerihDistAU.TabIndex = 6;
			lblPerihDistAU.Text = "AU";
			// 
			// txtAphSunDist
			// 
			txtAphSunDist.BackColor = SystemColors.Window;
			txtAphSunDist.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtAphSunDist.Location = new Point(446, 82);
			txtAphSunDist.Margin = new Padding(4, 3, 4, 3);
			txtAphSunDist.Name = "txtAphSunDist";
			txtAphSunDist.ReadOnly = true;
			txtAphSunDist.Size = new Size(135, 22);
			txtAphSunDist.TabIndex = 36;
			// 
			// lblPerihEarthDist
			// 
			lblPerihEarthDist.AutoSize = true;
			lblPerihEarthDist.Font = new Font("Tahoma", 8F);
			lblPerihEarthDist.ForeColor = SystemColors.GrayText;
			lblPerihEarthDist.Location = new Point(237, 114);
			lblPerihEarthDist.Margin = new Padding(4, 0, 4, 0);
			lblPerihEarthDist.Name = "lblPerihEarthDist";
			lblPerihEarthDist.Size = new Size(150, 13);
			lblPerihEarthDist.TabIndex = 20;
			lblPerihEarthDist.Text = "Perihelion distance from Earth";
			// 
			// lblName
			// 
			lblName.AutoSize = true;
			lblName.Font = new Font("Tahoma", 8F);
			lblName.ForeColor = SystemColors.GrayText;
			lblName.Location = new Point(27, 8);
			lblName.Margin = new Padding(4, 0, 4, 0);
			lblName.Name = "lblName";
			lblName.Size = new Size(34, 13);
			lblName.TabIndex = 0;
			lblName.Text = "Name";
			// 
			// lblCurrEarthDist
			// 
			lblCurrEarthDist.AutoSize = true;
			lblCurrEarthDist.Font = new Font("Tahoma", 8F);
			lblCurrEarthDist.ForeColor = SystemColors.GrayText;
			lblCurrEarthDist.Location = new Point(234, 166);
			lblCurrEarthDist.Margin = new Padding(4, 0, 4, 0);
			lblCurrEarthDist.Name = "lblCurrEarthDist";
			lblCurrEarthDist.Size = new Size(141, 13);
			lblCurrEarthDist.TabIndex = 23;
			lblCurrEarthDist.Text = "Current distance from Earth";
			// 
			// lblPerihMag
			// 
			lblPerihMag.AutoSize = true;
			lblPerihMag.Font = new Font("Tahoma", 8F);
			lblPerihMag.ForeColor = SystemColors.GrayText;
			lblPerihMag.Location = new Point(443, 113);
			lblPerihMag.Margin = new Padding(4, 0, 4, 0);
			lblPerihMag.Name = "lblPerihMag";
			lblPerihMag.Size = new Size(106, 13);
			lblPerihMag.TabIndex = 38;
			lblPerihMag.Text = "Perihelion magnitude";
			// 
			// lblCurrEarthDistAU
			// 
			lblCurrEarthDistAU.AutoSize = true;
			lblCurrEarthDistAU.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblCurrEarthDistAU.ForeColor = SystemColors.WindowText;
			lblCurrEarthDistAU.Location = new Point(374, 189);
			lblCurrEarthDistAU.Margin = new Padding(4, 0, 4, 0);
			lblCurrEarthDistAU.Name = "lblCurrEarthDistAU";
			lblCurrEarthDistAU.Size = new Size(23, 14);
			lblCurrEarthDistAU.TabIndex = 26;
			lblCurrEarthDistAU.Text = "AU";
			// 
			// txtPeriod
			// 
			txtPeriod.BackColor = SystemColors.Window;
			txtPeriod.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtPeriod.Location = new Point(239, 82);
			txtPeriod.Margin = new Padding(4, 3, 4, 3);
			txtPeriod.Name = "txtPeriod";
			txtPeriod.ReadOnly = true;
			txtPeriod.Size = new Size(135, 22);
			txtPeriod.TabIndex = 18;
			// 
			// lblCurrMagg
			// 
			lblCurrMagg.AutoSize = true;
			lblCurrMagg.Font = new Font("Tahoma", 8F);
			lblCurrMagg.ForeColor = SystemColors.GrayText;
			lblCurrMagg.Location = new Point(443, 165);
			lblCurrMagg.Margin = new Padding(4, 0, 4, 0);
			lblCurrMagg.Name = "lblCurrMagg";
			lblCurrMagg.Size = new Size(97, 13);
			lblCurrMagg.TabIndex = 40;
			lblCurrMagg.Text = "Current magnitude";
			// 
			// lblPeriodYears
			// 
			lblPeriodYears.AutoSize = true;
			lblPeriodYears.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblPeriodYears.ForeColor = SystemColors.WindowText;
			lblPeriodYears.Location = new Point(374, 85);
			lblPeriodYears.Margin = new Padding(4, 0, 4, 0);
			lblPeriodYears.Name = "lblPeriodYears";
			lblPeriodYears.Size = new Size(35, 14);
			lblPeriodYears.TabIndex = 19;
			lblPeriodYears.Text = "years";
			// 
			// txtCurrEarthDist
			// 
			txtCurrEarthDist.BackColor = SystemColors.Window;
			txtCurrEarthDist.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtCurrEarthDist.Location = new Point(239, 186);
			txtCurrEarthDist.Margin = new Padding(4, 3, 4, 3);
			txtCurrEarthDist.Name = "txtCurrEarthDist";
			txtCurrEarthDist.ReadOnly = true;
			txtCurrEarthDist.Size = new Size(135, 22);
			txtCurrEarthDist.TabIndex = 25;
			// 
			// txtNextPerihDate
			// 
			txtNextPerihDate.BackColor = SystemColors.Window;
			txtNextPerihDate.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtNextPerihDate.Location = new Point(29, 82);
			txtNextPerihDate.Margin = new Padding(4, 3, 4, 3);
			txtNextPerihDate.Name = "txtNextPerihDate";
			txtNextPerihDate.ReadOnly = true;
			txtNextPerihDate.Size = new Size(171, 22);
			txtNextPerihDate.TabIndex = 3;
			// 
			// txtCurrSunDist
			// 
			txtCurrSunDist.BackColor = SystemColors.Window;
			txtCurrSunDist.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtCurrSunDist.Location = new Point(29, 186);
			txtCurrSunDist.Margin = new Padding(4, 3, 4, 3);
			txtCurrSunDist.Name = "txtCurrSunDist";
			txtCurrSunDist.ReadOnly = true;
			txtCurrSunDist.Size = new Size(135, 22);
			txtCurrSunDist.TabIndex = 9;
			// 
			// lblNextPerihDate
			// 
			lblNextPerihDate.AutoSize = true;
			lblNextPerihDate.Font = new Font("Tahoma", 8F);
			lblNextPerihDate.ForeColor = SystemColors.GrayText;
			lblNextPerihDate.Location = new Point(27, 62);
			lblNextPerihDate.Margin = new Padding(4, 0, 4, 0);
			lblNextPerihDate.Name = "lblNextPerihDate";
			lblNextPerihDate.Size = new Size(78, 13);
			lblNextPerihDate.TabIndex = 2;
			lblNextPerihDate.Text = "Perihelion date";
			// 
			// txtPerihEarthDist
			// 
			txtPerihEarthDist.BackColor = SystemColors.Window;
			txtPerihEarthDist.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtPerihEarthDist.Location = new Point(239, 134);
			txtPerihEarthDist.Margin = new Padding(4, 3, 4, 3);
			txtPerihEarthDist.Name = "txtPerihEarthDist";
			txtPerihEarthDist.ReadOnly = true;
			txtPerihEarthDist.Size = new Size(135, 22);
			txtPerihEarthDist.TabIndex = 21;
			// 
			// lblCurrSunDistAU
			// 
			lblCurrSunDistAU.AutoSize = true;
			lblCurrSunDistAU.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lblCurrSunDistAU.ForeColor = SystemColors.WindowText;
			lblCurrSunDistAU.Location = new Point(164, 189);
			lblCurrSunDistAU.Margin = new Padding(4, 0, 4, 0);
			lblCurrSunDistAU.Name = "lblCurrSunDistAU";
			lblCurrSunDistAU.Size = new Size(23, 14);
			lblCurrSunDistAU.TabIndex = 10;
			lblCurrSunDistAU.Text = "AU";
			// 
			// lblPeriod
			// 
			lblPeriod.AutoSize = true;
			lblPeriod.Font = new Font("Tahoma", 8F);
			lblPeriod.ForeColor = SystemColors.GrayText;
			lblPeriod.Location = new Point(237, 62);
			lblPeriod.Margin = new Padding(4, 0, 4, 0);
			lblPeriod.Name = "lblPeriod";
			lblPeriod.Size = new Size(37, 13);
			lblPeriod.TabIndex = 17;
			lblPeriod.Text = "Period";
			// 
			// txtPerihDist
			// 
			txtPerihDist.BackColor = SystemColors.Window;
			txtPerihDist.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtPerihDist.Location = new Point(29, 134);
			txtPerihDist.Margin = new Padding(4, 3, 4, 3);
			txtPerihDist.Name = "txtPerihDist";
			txtPerihDist.ReadOnly = true;
			txtPerihDist.Size = new Size(135, 22);
			txtPerihDist.TabIndex = 5;
			// 
			// txtPerihMag
			// 
			txtPerihMag.BackColor = SystemColors.Window;
			txtPerihMag.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtPerihMag.Location = new Point(446, 134);
			txtPerihMag.Margin = new Padding(4, 3, 4, 3);
			txtPerihMag.Name = "txtPerihMag";
			txtPerihMag.ReadOnly = true;
			txtPerihMag.Size = new Size(135, 22);
			txtPerihMag.TabIndex = 39;
			// 
			// lblCurrSunDist
			// 
			lblCurrSunDist.AutoSize = true;
			lblCurrSunDist.Font = new Font("Tahoma", 8F);
			lblCurrSunDist.ForeColor = SystemColors.GrayText;
			lblCurrSunDist.Location = new Point(27, 166);
			lblCurrSunDist.Margin = new Padding(4, 0, 4, 0);
			lblCurrSunDist.Name = "lblCurrSunDist";
			lblCurrSunDist.Size = new Size(133, 13);
			lblCurrSunDist.TabIndex = 7;
			lblCurrSunDist.Text = "Current distance from Sun";
			// 
			// selectDateControl
			// 
			selectDateControl.DefaultDateTime = null;
			selectDateControl.Font = new Font("Tahoma", 8.25F);
			selectDateControl.Location = new Point(425, 288);
			selectDateControl.Margin = new Padding(4, 3, 4, 3);
			selectDateControl.Name = "selectDateControl";
			selectDateControl.PerihelionDate = null;
			selectDateControl.SelectedDateTime = new DateTime(0L);
			selectDateControl.Size = new Size(183, 27);
			selectDateControl.TabIndex = 46;
			// 
			// EphemerisControl
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(selectDateControl);
			Controls.Add(txtElemSortkey);
			Controls.Add(lblElemSortkey);
			Controls.Add(lblAzIndicator);
			Controls.Add(lblDecIndicator);
			Controls.Add(lblSunDistIndicator);
			Controls.Add(lblMagIndicator);
			Controls.Add(lblEarthDistIndicator);
			Controls.Add(lblElongationIndicator);
			Controls.Add(lblAltIndicator);
			Controls.Add(lblRaIndicator);
			Controls.Add(lblRA);
			Controls.Add(lblAlt);
			Controls.Add(lblAz);
			Controls.Add(lblElongation);
			Controls.Add(txtAz);
			Controls.Add(txtDec);
			Controls.Add(txtAlt);
			Controls.Add(txtRA);
			Controls.Add(txtElongation);
			Controls.Add(lblDec);
			Controls.Add(txtName);
			Controls.Add(lblPerihEarthDistAU);
			Controls.Add(txtCurrMag);
			Controls.Add(lblAphDist);
			Controls.Add(lblPerihDist);
			Controls.Add(lblAphSunDistAU);
			Controls.Add(lblPerihDistAU);
			Controls.Add(txtAphSunDist);
			Controls.Add(lblPerihEarthDist);
			Controls.Add(lblName);
			Controls.Add(lblCurrEarthDist);
			Controls.Add(lblPerihMag);
			Controls.Add(lblCurrEarthDistAU);
			Controls.Add(txtPeriod);
			Controls.Add(lblCurrMagg);
			Controls.Add(lblPeriodYears);
			Controls.Add(txtCurrEarthDist);
			Controls.Add(txtNextPerihDate);
			Controls.Add(txtCurrSunDist);
			Controls.Add(lblNextPerihDate);
			Controls.Add(txtPerihEarthDist);
			Controls.Add(lblCurrSunDistAU);
			Controls.Add(lblPeriod);
			Controls.Add(txtPerihDist);
			Controls.Add(txtPerihMag);
			Controls.Add(lblCurrSunDist);
			Margin = new Padding(4, 3, 4, 3);
			Name = "EphemerisControl";
			Size = new Size(620, 332);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.TextBox txtElemSortkey;
		private System.Windows.Forms.Label lblElemSortkey;
		private System.Windows.Forms.Label lblAzIndicator;
		private System.Windows.Forms.Label lblDecIndicator;
		private System.Windows.Forms.Label lblSunDistIndicator;
		private System.Windows.Forms.Label lblMagIndicator;
		private System.Windows.Forms.Label lblEarthDistIndicator;
		private System.Windows.Forms.Label lblElongationIndicator;
		private System.Windows.Forms.Label lblAltIndicator;
		private System.Windows.Forms.Label lblRaIndicator;
		private System.Windows.Forms.Label lblRA;
		private System.Windows.Forms.Label lblAlt;
		private System.Windows.Forms.Label lblAz;
		private System.Windows.Forms.Label lblElongation;
		private System.Windows.Forms.TextBox txtAz;
		private System.Windows.Forms.TextBox txtDec;
		private System.Windows.Forms.TextBox txtAlt;
		private System.Windows.Forms.TextBox txtRA;
		private System.Windows.Forms.TextBox txtElongation;
		private System.Windows.Forms.Label lblDec;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblPerihEarthDistAU;
		private System.Windows.Forms.TextBox txtCurrMag;
		private System.Windows.Forms.Label lblAphDist;
		private System.Windows.Forms.Label lblPerihDist;
		private System.Windows.Forms.Label lblAphSunDistAU;
		private System.Windows.Forms.Label lblPerihDistAU;
		private System.Windows.Forms.TextBox txtAphSunDist;
		private System.Windows.Forms.Label lblPerihEarthDist;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblCurrEarthDist;
		private System.Windows.Forms.Label lblPerihMag;
		private System.Windows.Forms.Label lblCurrEarthDistAU;
		private System.Windows.Forms.TextBox txtPeriod;
		private System.Windows.Forms.Label lblCurrMagg;
		private System.Windows.Forms.Label lblPeriodYears;
		private System.Windows.Forms.TextBox txtCurrEarthDist;
		private System.Windows.Forms.TextBox txtNextPerihDate;
		private System.Windows.Forms.TextBox txtCurrSunDist;
		private System.Windows.Forms.Label lblNextPerihDate;
		private System.Windows.Forms.TextBox txtPerihEarthDist;
		private System.Windows.Forms.Label lblCurrSunDistAU;
		private System.Windows.Forms.Label lblPeriod;
		private System.Windows.Forms.TextBox txtPerihDist;
		private System.Windows.Forms.TextBox txtPerihMag;
		private System.Windows.Forms.Label lblCurrSunDist;
		private DateAndTime.SelectDateControl selectDateControl;
	}
}
