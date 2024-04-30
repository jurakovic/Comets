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
			txtElemSortkey = new System.Windows.Forms.TextBox();
			lblElemSortkey = new System.Windows.Forms.Label();
			lblAzIndicator = new System.Windows.Forms.Label();
			lblDecIndicator = new System.Windows.Forms.Label();
			lblSunDistIndicator = new System.Windows.Forms.Label();
			lblMagIndicator = new System.Windows.Forms.Label();
			lblEarthDistIndicator = new System.Windows.Forms.Label();
			lblElongationIndicator = new System.Windows.Forms.Label();
			lblAltIndicator = new System.Windows.Forms.Label();
			lblRaIndicator = new System.Windows.Forms.Label();
			lblRA = new System.Windows.Forms.Label();
			lblAlt = new System.Windows.Forms.Label();
			lblAz = new System.Windows.Forms.Label();
			lblElongation = new System.Windows.Forms.Label();
			txtAz = new System.Windows.Forms.TextBox();
			txtDec = new System.Windows.Forms.TextBox();
			txtAlt = new System.Windows.Forms.TextBox();
			txtRA = new System.Windows.Forms.TextBox();
			txtElongation = new System.Windows.Forms.TextBox();
			lblDec = new System.Windows.Forms.Label();
			txtName = new System.Windows.Forms.TextBox();
			lblPerihEarthDistAU = new System.Windows.Forms.Label();
			txtCurrMag = new System.Windows.Forms.TextBox();
			lblAphDist = new System.Windows.Forms.Label();
			lblPerihDist = new System.Windows.Forms.Label();
			lblAphSunDistAU = new System.Windows.Forms.Label();
			lblPerihDistAU = new System.Windows.Forms.Label();
			txtAphSunDist = new System.Windows.Forms.TextBox();
			lblPerihEarthDist = new System.Windows.Forms.Label();
			lblName = new System.Windows.Forms.Label();
			lblCurrEarthDist = new System.Windows.Forms.Label();
			lblPerihMag = new System.Windows.Forms.Label();
			lblCurrEarthDistAU = new System.Windows.Forms.Label();
			txtPeriod = new System.Windows.Forms.TextBox();
			lblCurrMagg = new System.Windows.Forms.Label();
			lblPeriodYears = new System.Windows.Forms.Label();
			txtCurrEarthDist = new System.Windows.Forms.TextBox();
			txtNextPerihDate = new System.Windows.Forms.TextBox();
			txtCurrSunDist = new System.Windows.Forms.TextBox();
			lblNextPerihDate = new System.Windows.Forms.Label();
			txtPerihEarthDist = new System.Windows.Forms.TextBox();
			lblCurrSunDistAU = new System.Windows.Forms.Label();
			lblPeriod = new System.Windows.Forms.Label();
			txtPerihDist = new System.Windows.Forms.TextBox();
			txtPerihMag = new System.Windows.Forms.TextBox();
			lblCurrSunDist = new System.Windows.Forms.Label();
			selectDateControl = new DateAndTime.SelectDateControl();
			SuspendLayout();
			// 
			// txtElemSortkey
			// 
			txtElemSortkey.BackColor = System.Drawing.SystemColors.Window;
			txtElemSortkey.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtElemSortkey.Location = new System.Drawing.Point(446, 30);
			txtElemSortkey.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtElemSortkey.Name = "txtElemSortkey";
			txtElemSortkey.ReadOnly = true;
			txtElemSortkey.Size = new System.Drawing.Size(135, 22);
			txtElemSortkey.TabIndex = 34;
			txtElemSortkey.Visible = false;
			// 
			// lblElemSortkey
			// 
			lblElemSortkey.AutoSize = true;
			lblElemSortkey.Font = new System.Drawing.Font("Tahoma", 8F);
			lblElemSortkey.ForeColor = System.Drawing.SystemColors.GrayText;
			lblElemSortkey.Location = new System.Drawing.Point(444, 8);
			lblElemSortkey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblElemSortkey.Name = "lblElemSortkey";
			lblElemSortkey.Size = new System.Drawing.Size(44, 13);
			lblElemSortkey.TabIndex = 33;
			lblElemSortkey.Text = "Sortkey";
			lblElemSortkey.Visible = false;
			// 
			// lblAzIndicator
			// 
			lblAzIndicator.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblAzIndicator.ForeColor = System.Drawing.Color.Black;
			lblAzIndicator.Location = new System.Drawing.Point(219, 292);
			lblAzIndicator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblAzIndicator.Name = "lblAzIndicator";
			lblAzIndicator.Size = new System.Drawing.Size(15, 17);
			lblAzIndicator.TabIndex = 31;
			lblAzIndicator.Text = "▲";
			// 
			// lblDecIndicator
			// 
			lblDecIndicator.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblDecIndicator.ForeColor = System.Drawing.Color.Black;
			lblDecIndicator.Location = new System.Drawing.Point(9, 292);
			lblDecIndicator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblDecIndicator.Name = "lblDecIndicator";
			lblDecIndicator.Size = new System.Drawing.Size(15, 17);
			lblDecIndicator.TabIndex = 15;
			lblDecIndicator.Text = "▲";
			// 
			// lblSunDistIndicator
			// 
			lblSunDistIndicator.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblSunDistIndicator.ForeColor = System.Drawing.Color.Black;
			lblSunDistIndicator.Location = new System.Drawing.Point(9, 189);
			lblSunDistIndicator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblSunDistIndicator.Name = "lblSunDistIndicator";
			lblSunDistIndicator.Size = new System.Drawing.Size(15, 17);
			lblSunDistIndicator.TabIndex = 8;
			lblSunDistIndicator.Text = "▲";
			// 
			// lblMagIndicator
			// 
			lblMagIndicator.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblMagIndicator.ForeColor = System.Drawing.Color.Black;
			lblMagIndicator.Location = new System.Drawing.Point(426, 188);
			lblMagIndicator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblMagIndicator.Name = "lblMagIndicator";
			lblMagIndicator.Size = new System.Drawing.Size(15, 17);
			lblMagIndicator.TabIndex = 41;
			lblMagIndicator.Text = "▲";
			// 
			// lblEarthDistIndicator
			// 
			lblEarthDistIndicator.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblEarthDistIndicator.ForeColor = System.Drawing.Color.Black;
			lblEarthDistIndicator.Location = new System.Drawing.Point(219, 188);
			lblEarthDistIndicator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblEarthDistIndicator.Name = "lblEarthDistIndicator";
			lblEarthDistIndicator.Size = new System.Drawing.Size(15, 17);
			lblEarthDistIndicator.TabIndex = 24;
			lblEarthDistIndicator.Text = "▲";
			// 
			// lblElongationIndicator
			// 
			lblElongationIndicator.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblElongationIndicator.ForeColor = System.Drawing.Color.Black;
			lblElongationIndicator.Location = new System.Drawing.Point(426, 240);
			lblElongationIndicator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblElongationIndicator.Name = "lblElongationIndicator";
			lblElongationIndicator.Size = new System.Drawing.Size(15, 17);
			lblElongationIndicator.TabIndex = 44;
			lblElongationIndicator.Text = "▲";
			// 
			// lblAltIndicator
			// 
			lblAltIndicator.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblAltIndicator.ForeColor = System.Drawing.Color.Black;
			lblAltIndicator.Location = new System.Drawing.Point(219, 240);
			lblAltIndicator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblAltIndicator.Name = "lblAltIndicator";
			lblAltIndicator.Size = new System.Drawing.Size(15, 17);
			lblAltIndicator.TabIndex = 28;
			lblAltIndicator.Text = "▲";
			// 
			// lblRaIndicator
			// 
			lblRaIndicator.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblRaIndicator.ForeColor = System.Drawing.Color.Black;
			lblRaIndicator.Location = new System.Drawing.Point(9, 240);
			lblRaIndicator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblRaIndicator.Name = "lblRaIndicator";
			lblRaIndicator.Size = new System.Drawing.Size(15, 17);
			lblRaIndicator.TabIndex = 12;
			lblRaIndicator.Text = "▲";
			// 
			// lblRA
			// 
			lblRA.AutoSize = true;
			lblRA.Font = new System.Drawing.Font("Tahoma", 8F);
			lblRA.ForeColor = System.Drawing.SystemColors.GrayText;
			lblRA.Location = new System.Drawing.Point(27, 218);
			lblRA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblRA.Name = "lblRA";
			lblRA.Size = new System.Drawing.Size(83, 13);
			lblRA.TabIndex = 11;
			lblRA.Text = "Right Ascension";
			// 
			// lblAlt
			// 
			lblAlt.AutoSize = true;
			lblAlt.Font = new System.Drawing.Font("Tahoma", 8F);
			lblAlt.ForeColor = System.Drawing.SystemColors.GrayText;
			lblAlt.Location = new System.Drawing.Point(234, 218);
			lblAlt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblAlt.Name = "lblAlt";
			lblAlt.Size = new System.Drawing.Size(44, 13);
			lblAlt.TabIndex = 27;
			lblAlt.Text = "Altitude";
			// 
			// lblAz
			// 
			lblAz.AutoSize = true;
			lblAz.Font = new System.Drawing.Font("Tahoma", 8F);
			lblAz.ForeColor = System.Drawing.SystemColors.GrayText;
			lblAz.Location = new System.Drawing.Point(234, 270);
			lblAz.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblAz.Name = "lblAz";
			lblAz.Size = new System.Drawing.Size(45, 13);
			lblAz.TabIndex = 30;
			lblAz.Text = "Azimuth";
			// 
			// lblElongation
			// 
			lblElongation.AutoSize = true;
			lblElongation.Font = new System.Drawing.Font("Tahoma", 8F);
			lblElongation.ForeColor = System.Drawing.SystemColors.GrayText;
			lblElongation.Location = new System.Drawing.Point(443, 217);
			lblElongation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblElongation.Name = "lblElongation";
			lblElongation.Size = new System.Drawing.Size(57, 13);
			lblElongation.TabIndex = 43;
			lblElongation.Text = "Elongation";
			// 
			// txtAz
			// 
			txtAz.BackColor = System.Drawing.SystemColors.Window;
			txtAz.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtAz.Location = new System.Drawing.Point(239, 290);
			txtAz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtAz.Name = "txtAz";
			txtAz.ReadOnly = true;
			txtAz.Size = new System.Drawing.Size(135, 22);
			txtAz.TabIndex = 32;
			// 
			// txtDec
			// 
			txtDec.BackColor = System.Drawing.SystemColors.Window;
			txtDec.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtDec.Location = new System.Drawing.Point(29, 290);
			txtDec.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtDec.Name = "txtDec";
			txtDec.ReadOnly = true;
			txtDec.Size = new System.Drawing.Size(135, 22);
			txtDec.TabIndex = 16;
			// 
			// txtAlt
			// 
			txtAlt.BackColor = System.Drawing.SystemColors.Window;
			txtAlt.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtAlt.Location = new System.Drawing.Point(239, 238);
			txtAlt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtAlt.Name = "txtAlt";
			txtAlt.ReadOnly = true;
			txtAlt.Size = new System.Drawing.Size(135, 22);
			txtAlt.TabIndex = 29;
			// 
			// txtRA
			// 
			txtRA.BackColor = System.Drawing.SystemColors.Window;
			txtRA.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtRA.Location = new System.Drawing.Point(29, 238);
			txtRA.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtRA.Name = "txtRA";
			txtRA.ReadOnly = true;
			txtRA.Size = new System.Drawing.Size(135, 22);
			txtRA.TabIndex = 13;
			// 
			// txtElongation
			// 
			txtElongation.BackColor = System.Drawing.SystemColors.Window;
			txtElongation.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtElongation.Location = new System.Drawing.Point(446, 238);
			txtElongation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtElongation.Name = "txtElongation";
			txtElongation.ReadOnly = true;
			txtElongation.Size = new System.Drawing.Size(135, 22);
			txtElongation.TabIndex = 45;
			// 
			// lblDec
			// 
			lblDec.AutoSize = true;
			lblDec.Font = new System.Drawing.Font("Tahoma", 8F);
			lblDec.ForeColor = System.Drawing.SystemColors.GrayText;
			lblDec.Location = new System.Drawing.Point(27, 270);
			lblDec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblDec.Name = "lblDec";
			lblDec.Size = new System.Drawing.Size(59, 13);
			lblDec.TabIndex = 14;
			lblDec.Text = "Declination";
			// 
			// txtName
			// 
			txtName.BackColor = System.Drawing.SystemColors.Window;
			txtName.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtName.Location = new System.Drawing.Point(29, 30);
			txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtName.Name = "txtName";
			txtName.ReadOnly = true;
			txtName.Size = new System.Drawing.Size(345, 22);
			txtName.TabIndex = 1;
			// 
			// lblPerihEarthDistAU
			// 
			lblPerihEarthDistAU.AutoSize = true;
			lblPerihEarthDistAU.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblPerihEarthDistAU.ForeColor = System.Drawing.SystemColors.WindowText;
			lblPerihEarthDistAU.Location = new System.Drawing.Point(374, 137);
			lblPerihEarthDistAU.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblPerihEarthDistAU.Name = "lblPerihEarthDistAU";
			lblPerihEarthDistAU.Size = new System.Drawing.Size(23, 14);
			lblPerihEarthDistAU.TabIndex = 22;
			lblPerihEarthDistAU.Text = "AU";
			// 
			// txtCurrMag
			// 
			txtCurrMag.BackColor = System.Drawing.SystemColors.Window;
			txtCurrMag.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtCurrMag.Location = new System.Drawing.Point(446, 186);
			txtCurrMag.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtCurrMag.Name = "txtCurrMag";
			txtCurrMag.ReadOnly = true;
			txtCurrMag.Size = new System.Drawing.Size(135, 22);
			txtCurrMag.TabIndex = 42;
			// 
			// lblAphDist
			// 
			lblAphDist.AutoSize = true;
			lblAphDist.Font = new System.Drawing.Font("Tahoma", 8F);
			lblAphDist.ForeColor = System.Drawing.SystemColors.GrayText;
			lblAphDist.Location = new System.Drawing.Point(443, 62);
			lblAphDist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblAphDist.Name = "lblAphDist";
			lblAphDist.Size = new System.Drawing.Size(91, 13);
			lblAphDist.TabIndex = 35;
			lblAphDist.Text = "Aphelion distance";
			// 
			// lblPerihDist
			// 
			lblPerihDist.AutoSize = true;
			lblPerihDist.Font = new System.Drawing.Font("Tahoma", 8F);
			lblPerihDist.ForeColor = System.Drawing.SystemColors.GrayText;
			lblPerihDist.Location = new System.Drawing.Point(27, 114);
			lblPerihDist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblPerihDist.Name = "lblPerihDist";
			lblPerihDist.Size = new System.Drawing.Size(96, 13);
			lblPerihDist.TabIndex = 4;
			lblPerihDist.Text = "Perihelion distance";
			// 
			// lblAphSunDistAU
			// 
			lblAphSunDistAU.AutoSize = true;
			lblAphSunDistAU.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblAphSunDistAU.ForeColor = System.Drawing.SystemColors.WindowText;
			lblAphSunDistAU.Location = new System.Drawing.Point(581, 85);
			lblAphSunDistAU.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblAphSunDistAU.Name = "lblAphSunDistAU";
			lblAphSunDistAU.Size = new System.Drawing.Size(23, 14);
			lblAphSunDistAU.TabIndex = 37;
			lblAphSunDistAU.Text = "AU";
			// 
			// lblPerihDistAU
			// 
			lblPerihDistAU.AutoSize = true;
			lblPerihDistAU.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblPerihDistAU.ForeColor = System.Drawing.SystemColors.WindowText;
			lblPerihDistAU.Location = new System.Drawing.Point(164, 137);
			lblPerihDistAU.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblPerihDistAU.Name = "lblPerihDistAU";
			lblPerihDistAU.Size = new System.Drawing.Size(23, 14);
			lblPerihDistAU.TabIndex = 6;
			lblPerihDistAU.Text = "AU";
			// 
			// txtAphSunDist
			// 
			txtAphSunDist.BackColor = System.Drawing.SystemColors.Window;
			txtAphSunDist.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtAphSunDist.Location = new System.Drawing.Point(446, 82);
			txtAphSunDist.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtAphSunDist.Name = "txtAphSunDist";
			txtAphSunDist.ReadOnly = true;
			txtAphSunDist.Size = new System.Drawing.Size(135, 22);
			txtAphSunDist.TabIndex = 36;
			// 
			// lblPerihEarthDist
			// 
			lblPerihEarthDist.AutoSize = true;
			lblPerihEarthDist.Font = new System.Drawing.Font("Tahoma", 8F);
			lblPerihEarthDist.ForeColor = System.Drawing.SystemColors.GrayText;
			lblPerihEarthDist.Location = new System.Drawing.Point(237, 114);
			lblPerihEarthDist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblPerihEarthDist.Name = "lblPerihEarthDist";
			lblPerihEarthDist.Size = new System.Drawing.Size(150, 13);
			lblPerihEarthDist.TabIndex = 20;
			lblPerihEarthDist.Text = "Perihelion distance from Earth";
			// 
			// lblName
			// 
			lblName.AutoSize = true;
			lblName.Font = new System.Drawing.Font("Tahoma", 8F);
			lblName.ForeColor = System.Drawing.SystemColors.GrayText;
			lblName.Location = new System.Drawing.Point(27, 8);
			lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblName.Name = "lblName";
			lblName.Size = new System.Drawing.Size(34, 13);
			lblName.TabIndex = 0;
			lblName.Text = "Name";
			// 
			// lblCurrEarthDist
			// 
			lblCurrEarthDist.AutoSize = true;
			lblCurrEarthDist.Font = new System.Drawing.Font("Tahoma", 8F);
			lblCurrEarthDist.ForeColor = System.Drawing.SystemColors.GrayText;
			lblCurrEarthDist.Location = new System.Drawing.Point(234, 166);
			lblCurrEarthDist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblCurrEarthDist.Name = "lblCurrEarthDist";
			lblCurrEarthDist.Size = new System.Drawing.Size(141, 13);
			lblCurrEarthDist.TabIndex = 23;
			lblCurrEarthDist.Text = "Current distance from Earth";
			// 
			// lblPerihMag
			// 
			lblPerihMag.AutoSize = true;
			lblPerihMag.Font = new System.Drawing.Font("Tahoma", 8F);
			lblPerihMag.ForeColor = System.Drawing.SystemColors.GrayText;
			lblPerihMag.Location = new System.Drawing.Point(443, 113);
			lblPerihMag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblPerihMag.Name = "lblPerihMag";
			lblPerihMag.Size = new System.Drawing.Size(106, 13);
			lblPerihMag.TabIndex = 38;
			lblPerihMag.Text = "Perihelion magnitude";
			// 
			// lblCurrEarthDistAU
			// 
			lblCurrEarthDistAU.AutoSize = true;
			lblCurrEarthDistAU.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblCurrEarthDistAU.ForeColor = System.Drawing.SystemColors.WindowText;
			lblCurrEarthDistAU.Location = new System.Drawing.Point(374, 189);
			lblCurrEarthDistAU.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblCurrEarthDistAU.Name = "lblCurrEarthDistAU";
			lblCurrEarthDistAU.Size = new System.Drawing.Size(23, 14);
			lblCurrEarthDistAU.TabIndex = 26;
			lblCurrEarthDistAU.Text = "AU";
			// 
			// txtPeriod
			// 
			txtPeriod.BackColor = System.Drawing.SystemColors.Window;
			txtPeriod.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtPeriod.Location = new System.Drawing.Point(239, 82);
			txtPeriod.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtPeriod.Name = "txtPeriod";
			txtPeriod.ReadOnly = true;
			txtPeriod.Size = new System.Drawing.Size(135, 22);
			txtPeriod.TabIndex = 18;
			// 
			// lblCurrMagg
			// 
			lblCurrMagg.AutoSize = true;
			lblCurrMagg.Font = new System.Drawing.Font("Tahoma", 8F);
			lblCurrMagg.ForeColor = System.Drawing.SystemColors.GrayText;
			lblCurrMagg.Location = new System.Drawing.Point(443, 165);
			lblCurrMagg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblCurrMagg.Name = "lblCurrMagg";
			lblCurrMagg.Size = new System.Drawing.Size(97, 13);
			lblCurrMagg.TabIndex = 40;
			lblCurrMagg.Text = "Current magnitude";
			// 
			// lblPeriodYears
			// 
			lblPeriodYears.AutoSize = true;
			lblPeriodYears.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblPeriodYears.ForeColor = System.Drawing.SystemColors.WindowText;
			lblPeriodYears.Location = new System.Drawing.Point(374, 85);
			lblPeriodYears.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblPeriodYears.Name = "lblPeriodYears";
			lblPeriodYears.Size = new System.Drawing.Size(35, 14);
			lblPeriodYears.TabIndex = 19;
			lblPeriodYears.Text = "years";
			// 
			// txtCurrEarthDist
			// 
			txtCurrEarthDist.BackColor = System.Drawing.SystemColors.Window;
			txtCurrEarthDist.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtCurrEarthDist.Location = new System.Drawing.Point(239, 186);
			txtCurrEarthDist.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtCurrEarthDist.Name = "txtCurrEarthDist";
			txtCurrEarthDist.ReadOnly = true;
			txtCurrEarthDist.Size = new System.Drawing.Size(135, 22);
			txtCurrEarthDist.TabIndex = 25;
			// 
			// txtNextPerihDate
			// 
			txtNextPerihDate.BackColor = System.Drawing.SystemColors.Window;
			txtNextPerihDate.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtNextPerihDate.Location = new System.Drawing.Point(29, 82);
			txtNextPerihDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtNextPerihDate.Name = "txtNextPerihDate";
			txtNextPerihDate.ReadOnly = true;
			txtNextPerihDate.Size = new System.Drawing.Size(171, 22);
			txtNextPerihDate.TabIndex = 3;
			// 
			// txtCurrSunDist
			// 
			txtCurrSunDist.BackColor = System.Drawing.SystemColors.Window;
			txtCurrSunDist.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtCurrSunDist.Location = new System.Drawing.Point(29, 186);
			txtCurrSunDist.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtCurrSunDist.Name = "txtCurrSunDist";
			txtCurrSunDist.ReadOnly = true;
			txtCurrSunDist.Size = new System.Drawing.Size(135, 22);
			txtCurrSunDist.TabIndex = 9;
			// 
			// lblNextPerihDate
			// 
			lblNextPerihDate.AutoSize = true;
			lblNextPerihDate.Font = new System.Drawing.Font("Tahoma", 8F);
			lblNextPerihDate.ForeColor = System.Drawing.SystemColors.GrayText;
			lblNextPerihDate.Location = new System.Drawing.Point(27, 62);
			lblNextPerihDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblNextPerihDate.Name = "lblNextPerihDate";
			lblNextPerihDate.Size = new System.Drawing.Size(78, 13);
			lblNextPerihDate.TabIndex = 2;
			lblNextPerihDate.Text = "Perihelion date";
			// 
			// txtPerihEarthDist
			// 
			txtPerihEarthDist.BackColor = System.Drawing.SystemColors.Window;
			txtPerihEarthDist.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtPerihEarthDist.Location = new System.Drawing.Point(239, 134);
			txtPerihEarthDist.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtPerihEarthDist.Name = "txtPerihEarthDist";
			txtPerihEarthDist.ReadOnly = true;
			txtPerihEarthDist.Size = new System.Drawing.Size(135, 22);
			txtPerihEarthDist.TabIndex = 21;
			// 
			// lblCurrSunDistAU
			// 
			lblCurrSunDistAU.AutoSize = true;
			lblCurrSunDistAU.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lblCurrSunDistAU.ForeColor = System.Drawing.SystemColors.WindowText;
			lblCurrSunDistAU.Location = new System.Drawing.Point(164, 189);
			lblCurrSunDistAU.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblCurrSunDistAU.Name = "lblCurrSunDistAU";
			lblCurrSunDistAU.Size = new System.Drawing.Size(23, 14);
			lblCurrSunDistAU.TabIndex = 10;
			lblCurrSunDistAU.Text = "AU";
			// 
			// lblPeriod
			// 
			lblPeriod.AutoSize = true;
			lblPeriod.Font = new System.Drawing.Font("Tahoma", 8F);
			lblPeriod.ForeColor = System.Drawing.SystemColors.GrayText;
			lblPeriod.Location = new System.Drawing.Point(237, 62);
			lblPeriod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblPeriod.Name = "lblPeriod";
			lblPeriod.Size = new System.Drawing.Size(37, 13);
			lblPeriod.TabIndex = 17;
			lblPeriod.Text = "Period";
			// 
			// txtPerihDist
			// 
			txtPerihDist.BackColor = System.Drawing.SystemColors.Window;
			txtPerihDist.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtPerihDist.Location = new System.Drawing.Point(29, 134);
			txtPerihDist.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtPerihDist.Name = "txtPerihDist";
			txtPerihDist.ReadOnly = true;
			txtPerihDist.Size = new System.Drawing.Size(135, 22);
			txtPerihDist.TabIndex = 5;
			// 
			// txtPerihMag
			// 
			txtPerihMag.BackColor = System.Drawing.SystemColors.Window;
			txtPerihMag.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtPerihMag.Location = new System.Drawing.Point(446, 134);
			txtPerihMag.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtPerihMag.Name = "txtPerihMag";
			txtPerihMag.ReadOnly = true;
			txtPerihMag.Size = new System.Drawing.Size(135, 22);
			txtPerihMag.TabIndex = 39;
			// 
			// lblCurrSunDist
			// 
			lblCurrSunDist.AutoSize = true;
			lblCurrSunDist.Font = new System.Drawing.Font("Tahoma", 8F);
			lblCurrSunDist.ForeColor = System.Drawing.SystemColors.GrayText;
			lblCurrSunDist.Location = new System.Drawing.Point(27, 166);
			lblCurrSunDist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblCurrSunDist.Name = "lblCurrSunDist";
			lblCurrSunDist.Size = new System.Drawing.Size(133, 13);
			lblCurrSunDist.TabIndex = 7;
			lblCurrSunDist.Text = "Current distance from Sun";
			// 
			// selectDateControl
			// 
			selectDateControl.DefaultDateTime = null;
			selectDateControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			selectDateControl.Location = new System.Drawing.Point(425, 288);
			selectDateControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			selectDateControl.Name = "selectDateControl";
			selectDateControl.PerihelionDate = null;
			selectDateControl.SelectedDateTime = new System.DateTime(0L);
			selectDateControl.Size = new System.Drawing.Size(183, 27);
			selectDateControl.TabIndex = 46;
			// 
			// EphemerisControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
			Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			Name = "EphemerisControl";
			Size = new System.Drawing.Size(620, 332);
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
