using Comets.Classes;
using System;
using System.Windows.Forms;
using System.Xml;

namespace Comets.Forms
{
    public partial class FormSettings : Form
    {
        const string settingsxml = "settings.xml";

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            txtAppData.Text = FormMain.Settings.General.AppData;
            txtDownloads.Text = FormMain.Settings.General.Downloads;
            chDownloadOnStartup.Checked = FormMain.Settings.General.DownloadOnStartup;
            chNewVersionOnStartup.Checked = FormMain.Settings.General.NewVersionOnStartup;
            chRememberWindowPosition.Checked = FormMain.Settings.General.RememberWindowPosition;
            chExitWithoutConfirm.Checked = FormMain.Settings.General.ExitWithoutConfirm;

            txtName.Text = FormMain.Settings.Location.Name;

            bool north = FormMain.Settings.Location.Latitude >= 0.0;
            int latDeg = Math.Abs((int)FormMain.Settings.Location.Latitude);

            double la1 = FormMain.Settings.Location.Latitude - latDeg;
            double la2 = la1 * 60; //latMin
            double la3 = la2 - (int)la2;
            double la4 = la3 * 60; //latSec

            int latMin = (int)la2;
            int latSec = (int)la4;

            txtLatDeg.Text = latDeg.ToString();
            txtLatMin.Text = latMin.ToString("00");
            txtLatSec.Text = latSec.ToString("00");

            bool east = FormMain.Settings.Location.Longitude >= 0.0;
            int lonDeg = Math.Abs((int)FormMain.Settings.Location.Longitude);

            double lo1 = FormMain.Settings.Location.Longitude - lonDeg;
            double lo2 = lo1 * 60; //lotMin
            double lo3 = lo2 - (int)lo2;
            double lo4 = lo3 * 60; //lotSec

            int lonMin = (int)lo2;
            int lonSec = (int)lo4;

            txtLonDeg.Text = lonDeg.ToString();
            txtLonMin.Text = lonMin.ToString("00");
            txtLonSec.Text = lonSec.ToString("00");

            cbxNorthSouth.SelectedIndex = north ? 0 : 1;
            cbxEastWest.SelectedIndex = east ? 0 : 1;

            //txtTimezone

            chDST.Checked = FormMain.Settings.Location.DST;


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //TO DO
            //Collect data

            SaveSettings(FormMain.Settings);
        }

        public static Settings LoadSettings()
        {
            Settings settings = new Settings();

            //TO DO

            return settings;
        }

        private void SaveSettings(Settings settings)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };

            using (XmlWriter writer = XmlWriter.Create(settingsxml, xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Settings");

                writer.WriteStartElement("General");
                writer.WriteElementString("AppData", settings.General.AppData);
                writer.WriteElementString("Downloads", settings.General.Downloads);
                writer.WriteElementString("DownloadOnStartup", settings.General.DownloadOnStartup.ToString());
                writer.WriteElementString("NewVersionOnStartup", settings.General.NewVersionOnStartup.ToString());
                writer.WriteElementString("RememberWindowPosition", settings.General.RememberWindowPosition.ToString());
                writer.WriteElementString("ExitWithoutConfirm", settings.General.ExitWithoutConfirm.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("Location");
                writer.WriteElementString("Name", settings.Location.Name);
                writer.WriteElementString("Latitude", settings.Location.Latitude.ToString());
                writer.WriteElementString("Longitude", settings.Location.Longitude.ToString());
                writer.WriteElementString("Timezone", settings.Location.Timezone.ToString());
                writer.WriteElementString("DST", settings.Location.DST.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("Programs");
                foreach (var a in settings.Programs.ProgramsDict)
                {
                    string name = ElementTypes.TypeName[a.Key];
                    string dir = a.Value;
                    writer.WriteElementString(name, dir);
                }
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
