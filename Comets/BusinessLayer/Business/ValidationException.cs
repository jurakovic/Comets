using System;
using System.Windows.Forms;

namespace Comets.BusinessLayer.Business
{
	class ValidationException : Exception
	{
		public Control Control { get; private set; }

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
