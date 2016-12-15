using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiBoole.Controllers
{
	public interface IMainWindowController
	{
		void LoadDisplay(Globals.DisplayType dType);
		void ProcessNewFile(string path, bool overwriteExisting = false);
		void SaveFile();
		void SaveFileAs(string filePath);
		void ExitApplication();
        void SelectTabPage(string fileName);
        void ReturnToSingleDisplay();
	}
}
