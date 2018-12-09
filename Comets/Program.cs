using Comets.BusinessLayer.Business;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Comets.Application
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			System.Windows.Forms.Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

			System.Windows.Forms.Application.EnableVisualStyles();
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
			System.Windows.Forms.Application.Run(new FormMain());
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs args)
		{
			Exception e = args.Exception;
			string message = e.Message.PadRight(100);
			MessageBoxIcon icon = e is ValidationException ? MessageBoxIcon.Warning : MessageBoxIcon.Error;
			MessageBox.Show(message, "Comets", MessageBoxButtons.OK, icon);

			if (e is ValidationException)
			{
				ValidationException vex = e as ValidationException;
				if (vex.Control != null)
					vex.Control.Focus();
			}
		}
	}
}
