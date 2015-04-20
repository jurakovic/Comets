using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comets.Classes
{
    public class Settings
    {
        public General General { get; set; }
        public Location Location { get; set; }
        public Programs Programs { get; set; }
        
        public Settings()
        {
            General = new General();
            Location = new Location();
            Programs = new Programs();
        }
    }

    #region General

    public class General
    {
        public string AppData { get; set; }
        public string Database { get; set; }
        public string Downloads { get; set; }
        public bool DownloadOnStartup { get; set; }
        public bool NewVersionOnStartup { get; set; }
        public bool RememberWindowPosition { get; set; }
        public bool ExitWithoutConfirm { get; set; }

        public General()
        {
            AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Comets";
            Database = AppData + "\\Comets.db";
            Downloads = AppData;
            DownloadOnStartup = false;
            NewVersionOnStartup = false;
            RememberWindowPosition = false;
            ExitWithoutConfirm = false;
        }
    }

    #endregion

    #region Location

    public class Location
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Timezone { get; set; }
        public bool DST { get; set; }

        public Location()
        {
            Name = "Home";
            Latitude = 0.0;
            Longitude = 0.0;
            Timezone = 0;
            DST = false;
        }
    }

    #endregion

    #region Programs

    public class Programs
    {
        public Dictionary<int, string> ProgramsDict { get; set; }

        public Programs()
        {
            ProgramsDict = new Dictionary<int, string>();
        }
    }

    #endregion
}
