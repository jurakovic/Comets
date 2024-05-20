using System.Windows.Forms;

namespace Comets.Application.Common.Controls.Common
{
	public class ValueChangeControl : UserControl
	{
		protected bool ValueChangedInternal { get; set; }
	}

	public class ValueChangeForm : ThemeForm
	{
		protected bool ValueChangedInternal { get; set; }
	}
}
