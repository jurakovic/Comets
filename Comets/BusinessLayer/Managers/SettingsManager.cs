﻿using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Comets.BusinessLayer.Managers
{
	public static class SettingsManager
	{
		#region Const

		const string settingsini = "settings.ini";

		#endregion

		#region LoadSettings

		public static Settings LoadSettings()
		{
			Settings settings = new Settings();

			if (File.Exists(settingsini))
			{
				string property = String.Empty;
				string value = String.Empty;

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

							switch (property)
							{
								case "AppData": settings.AppData = value; break;
								case "Database": settings.Database = value; break;
								case "Downloads": settings.Downloads = value; break;
								case "DownloadOnStartup": settings.DownloadOnStartup = Convert.ToBoolean(value); break;
								case "RememberWindowPosition": settings.RememberWindowPosition = Convert.ToBoolean(value); break;
								case "ShowStatusBar": settings.ShowStatusBar = Convert.ToBoolean(value); break;
								case "ExitWithoutConfirm": settings.ExitWithoutConfirm = Convert.ToBoolean(value); break;
								//case "NewVersionOnStartup": settings.NewVersionOnStartup = Convert.ToBoolean(value); break; 

								case "Maximized": settings.Maximized = Convert.ToBoolean(value); break;
								case "Left": settings.Left = value.Int(); break;
								case "Top": settings.Top = value.Int(); break;
								case "Width": settings.Width = value.Int(); break;
								case "Height": settings.Height = value.Int(); break;

								case "UseProxy": settings.UseProxy = Convert.ToBoolean(value); break;
								case "Domain": settings.Domain = value; break;
								case "Username": settings.Username = value; break;
								case "Password": settings.Password = value; break;
								case "Proxy": settings.Proxy = value; break;
								case "Port": settings.Port = value.Int(); break;

								case "Name": settings.Location.Name = value; break;
								case "Latitude": settings.Location.Latitude = value.Double(); break;
								case "Longitude": settings.Location.Longitude = value.Double(); break;
								case "Altitude": settings.Location.Altitude = value.Int(); break;
								case "Timezone": settings.Location.Offset = value.Int(); break;
								case "DST": settings.Location.DST = Convert.ToBoolean(value); break;

								default:
									if (ElementTypes.TypeName.Contains(property))
										settings.ExternalPrograms.Add(new ExternalProgram(Array.IndexOf(ElementTypes.TypeName, property), value));
									else if (exceptions++ < 3)
										MessageBox.Show("Unknown property \"" + property + "\"\t\t\t\t", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
									break;
							}
						}
					}
					catch
					{
						//samo 3 puta pokazi messagebox
						if (exceptions++ < 3)
							MessageBox.Show("Invalid value \"" + value + "\" at property \"" + property + "\"\t\t", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

						continue;
					}
				}

				if (exceptions > 0)
					settings.HasErrors = true;
			}

			return settings;
		}

		#endregion

		#region SaveSettings

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
			sb.AppendLine(String.Format(format, "ShowStatusBar", settings.ShowStatusBar));
			sb.AppendLine(String.Format(format, "ExitWithoutConfirm", settings.ExitWithoutConfirm));
			//sb.AppendLine(String.Format(format, "NewVersionOnStartup", settings.NewVersionOnStartup));
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

			if (settings.UseProxy ||
				!String.IsNullOrEmpty(settings.Domain) ||
				!String.IsNullOrEmpty(settings.Username) ||
				!String.IsNullOrEmpty(settings.Password) ||
				!String.IsNullOrEmpty(settings.Proxy) ||
				settings.Port > 0)
			{
				sb.AppendLine("[Network]");
				sb.AppendLine(String.Format(format, "UseProxy", settings.UseProxy));
				if (!String.IsNullOrEmpty(settings.Domain)) sb.AppendLine(String.Format(format, "Domain", settings.Domain));
				if (!String.IsNullOrEmpty(settings.Username)) sb.AppendLine(String.Format(format, "Username", settings.Username));
				if (!String.IsNullOrEmpty(settings.Password)) sb.AppendLine(String.Format(format, "Password", settings.Password));
				if (!String.IsNullOrEmpty(settings.Proxy)) sb.AppendLine(String.Format(format, "Proxy", settings.Proxy));
				if (settings.Port > 0) sb.AppendLine(String.Format(format, "Port", settings.Port));
				sb.AppendLine();
			}

			sb.AppendLine("[Location]");
			sb.AppendLine(String.Format(format, "Name", settings.Location.Name));
			sb.AppendLine(String.Format(format, "Latitude", settings.Location.Latitude.ToString("0.000000")));
			sb.AppendLine(String.Format(format, "Longitude", settings.Location.Longitude.ToString("0.000000")));
			sb.AppendLine(String.Format(format, "Altitude", settings.Location.Altitude));
			sb.AppendLine(String.Format(format, "Timezone", settings.Location.Offset));
			sb.AppendLine(String.Format(format, "DST", settings.Location.DST));
			sb.AppendLine();

			if (settings.ExternalPrograms.Any())
			{
				sb.AppendLine("[ExternalPrograms]");
				foreach (ExternalProgram ep in settings.ExternalPrograms)
				{
					sb.AppendLine(String.Format(format, ep.Name, ep.Directory));
				}
			}

			File.WriteAllText(settingsini, sb.ToString());
		}

		#endregion
	}
}