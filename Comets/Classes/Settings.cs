﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Comets.Classes
{
    public class Settings
    {
        const string settingsini = "settings.ini";

        //General
        public string AppData { get; set; }
        public string Database { get; set; }
        public string Downloads { get; set; }
        public bool DownloadOnStartup { get; set; }
        public bool RememberWindowPosition { get; set; }
        public bool NewVersionOnStartup { get; set; }
        public bool ExitWithoutConfirm { get; set; }

        //Window
        public bool Maximized { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }


        //Network
        public bool UseProxy { get; set; }
        public string Domain { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Proxy { get; set; }
        public string Port { get; set; }

        // Location
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Altitude { get; set; }
        public int Timezone { get; set; }
        public bool DST { get; set; }

        //Programs
        public Dictionary<int, string> ProgramsDict { get; set; }

        public bool HasErrors { get; set; }
        
        public Settings()
        {
            AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Comets";
            Database = AppData + "\\Comets.db";
            Downloads = AppData + "\\Downloads";
            DownloadOnStartup = false;
            NewVersionOnStartup = false;
            RememberWindowPosition = false;
            ExitWithoutConfirm = false;

            Maximized = false;
            Width = 0;
            Height = 0;

            UseProxy = false;
            Domain = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            Proxy = string.Empty;
            Port = string.Empty;

            Name = "Home";
            Latitude = 0.0;
            Longitude = 0.0;
            Altitude = 0;
            Timezone = 0;
            DST = false;

            ProgramsDict = new Dictionary<int, string>();

            HasErrors = false;
        }

        public static Settings LoadSettings()
        {
            Settings settings = new Settings();

            if (File.Exists(settingsini))
            {
                string property = string.Empty;
                string value = string.Empty;

                int exceptions = 0;

                string[] lines = File.ReadAllLines(settingsini);
                int count = lines.Count();

                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        string[] split = lines[i].Split('=');

                        if (split.Count() == 2)
                        {
                            property = split[0].Trim();
                            value = split[1].Trim();

                            if (property == "AppData") { settings.AppData = value; continue; }
                            if (property == "Database") { settings.Database = value; continue; }
                            if (property == "Downloads") { settings.Downloads = value; continue; }
                            if (property == "DownloadOnStartup") { settings.DownloadOnStartup = Convert.ToBoolean(value); continue; }
                            if (property == "RememberWindowPosition") { settings.RememberWindowPosition = Convert.ToBoolean(value); continue; }
                            
                            //if (property == "NewVersionOnStartup") { settings.NewVersionOnStartup = Convert.ToBoolean(value); continue; } 
                            //if (property == "ExitWithoutConfirm") { settings.ExitWithoutConfirm = Convert.ToBoolean(value); continue; }

                            if (property == "Maximized") { settings.Maximized = Convert.ToBoolean(value); continue; }
                            if (property == "Left") { settings.Left = Convert.ToInt32(value); continue; }
                            if (property == "Top") { settings.Top = Convert.ToInt32(value); continue; }
                            if (property == "Width") { settings.Width = Convert.ToInt32(value); continue; }
                            if (property == "Height") { settings.Height = Convert.ToInt32(value); continue; }

                            if (property == "UseProxy") { settings.UseProxy = Convert.ToBoolean(value); continue; }
                            if (property == "Domain") { settings.Domain = value; continue; }
                            if (property == "Username") { settings.Username = value; continue; }
                            if (property == "Password") { settings.Password = value; continue; }
                            if (property == "Proxy") { settings.Proxy = value; continue; }
                            if (property == "Port") { settings.Port = value; continue; }

                            if (property == "Name") { settings.Name = value; continue; }
                            if (property == "Latitude") { settings.Latitude = Convert.ToDouble(value); continue; }
                            if (property == "Longitude") { settings.Longitude = Convert.ToDouble(value); continue; }
                            if (property == "Altitude") { settings.Altitude = Convert.ToInt32(value); continue; }
                            if (property == "Timezone") { settings.Timezone = Convert.ToInt32(value); continue; }
                            if (property == "DST") { settings.DST = Convert.ToBoolean(value); continue; }
                        }
                    }
                    catch
                    {
                        //samo 3 puta pokazi messagebox
                        if (exceptions++ < 3)
                            MessageBox.Show("Invalid value at property " + property + "\t\t\t", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        settings.HasErrors = true;
                        continue;
                    }
                }
            }

            return settings;
        }

        public static void SaveSettings(Settings settings)
        {
            StringBuilder sb = new StringBuilder();

            string format = "{0,-38} = {1}";

            sb.AppendLine("[General]");
            sb.AppendLine(String.Format(format, "AppData", settings.AppData));
            sb.AppendLine(String.Format(format, "Database", settings.Database));
            sb.AppendLine(String.Format(format, "Downloads", settings.Downloads));
            sb.AppendLine(String.Format(format, "DownloadOnStartup", settings.DownloadOnStartup));
            sb.AppendLine(String.Format(format, "RememberWindowPosition", settings.RememberWindowPosition));
            //sb.AppendLine(String.Format(format, "NewVersionOnStartup", settings.NewVersionOnStartup));
            //sb.AppendLine(String.Format(format, "ExitWithoutConfirm", settings.ExitWithoutConfirm));
            sb.AppendLine();

            if (settings.RememberWindowPosition)
            {
                sb.AppendLine("[Window]");
                if (settings.Maximized)
                {
                    sb.AppendLine(String.Format(format, "Maximized", settings.Maximized));
                }
                else
                {
                    sb.AppendLine(String.Format(format, "Left", settings.Left));
                    sb.AppendLine(String.Format(format, "Top", settings.Top));
                    sb.AppendLine(String.Format(format, "Width", settings.Width));
                    sb.AppendLine(String.Format(format, "Height", settings.Height));
                }
                sb.AppendLine();
            }

            if (settings.UseProxy)
            {
                sb.AppendLine("[Network]");
                sb.AppendLine(String.Format(format, "UseProxy", settings.UseProxy));
                sb.AppendLine(String.Format(format, "Domain", settings.Domain));
                sb.AppendLine(String.Format(format, "Username", settings.Username));
                sb.AppendLine(String.Format(format, "Password", settings.Password));
                sb.AppendLine(String.Format(format, "Proxy", settings.Proxy));
                sb.AppendLine(String.Format(format, "Port", settings.Port));
                sb.AppendLine();
            }

            sb.AppendLine("[Location]");
            sb.AppendLine(String.Format(format, "Name", settings.Name));
            sb.AppendLine(String.Format(format, "Latitude", settings.Latitude.ToString("0.000000")));
            sb.AppendLine(String.Format(format, "Longitude", settings.Longitude.ToString("0.000000")));
            sb.AppendLine(String.Format(format, "Altitude", settings.Altitude));
            sb.AppendLine(String.Format(format, "Timezone", settings.Timezone));
            sb.AppendLine(String.Format(format, "DST", settings.DST));
            sb.AppendLine();


            File.WriteAllText(settingsini, sb.ToString());
        }
    }
}
