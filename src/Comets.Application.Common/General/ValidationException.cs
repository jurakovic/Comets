using System;
using System.Windows.Forms;

namespace Comets.Application.Common.General
{
	public class ValidationException : Exception
	{
		#region Properties

		public Control Control { get; private set; }

		#endregion

		#region Constructor

		public ValidationException(string message) : base(message)
		{

		}

		public ValidationException(string message, Control control) : base(message)
		{
			this.Control = control;
		}

		#endregion
	}
}
