using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisiBoole.Views;

namespace VisiBoole.Controllers
{
	public interface IDisplayController
	{
		IDisplay PreviousDisplay { get; set; }

		IDisplay CurrentDisplay { get; set; }

		IDisplay GetDisplayOfType(Globals.DisplayType dType);

		void AttachMainWindowController(IMainWindowController mwController);

		bool CreateNewTab(SubDesign sd);

		bool SaveActiveTab();

		TabPage GetActiveTabPage();

		bool SelectTabPage(string fileName);

		void Run();
	}
}
