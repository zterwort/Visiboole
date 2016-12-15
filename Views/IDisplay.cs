using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisiBoole.Controllers;
using static VisiBoole.Globals;

namespace VisiBoole.Views
{
	public interface IDisplay
	{
		DisplayType TypeOfDisplay { get; }
		void AttachController(IDisplayController controller);
		void LoadTabControl(TabControl tc);
		void LoadWebBrowser(WebBrowser browser);
	}
}
