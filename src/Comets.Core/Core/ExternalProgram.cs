﻿using Comets.Core.Managers;
using System.ComponentModel;

namespace Comets.Core
{
	public class ExternalProgram
	{
		#region Properties

		[Browsable(false)]
		public int Type { get; set; }
		public string Directory { get; set; }
		public string Name
		{
			get { return ElementTypesManager.TypeName[Type]; }
		}

		#endregion

		#region Constructor

		public ExternalProgram(int type, string directory)
		{
			this.Type = type;
			this.Directory = directory;
		}

		#endregion
	}
}
