using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisiBoole.Controllers;
using VisiBoole.Views;

namespace VisiBoole
{
	/// <summary>
	/// The entry-point of this application
	/// </summary>
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			DisplaySingle single = new DisplaySingle();
			DisplaySingleOutput singleOutput = new DisplaySingleOutput();
			DisplayHorizontal horizontal = new DisplayHorizontal();
			DisplayVertical vertical = new DisplayVertical();
			DisplayController dc = new DisplayController(single, horizontal, vertical, singleOutput);

			single.AttachController(dc);
			singleOutput.AttachController(dc);
			horizontal.AttachController(dc);
			vertical.AttachController(dc);

			MainWindow mw = new MainWindow();
			MainWindowController mwc = new MainWindowController(mw, dc);

			dc.AttachMainWindowController(mwc);
			
			//Application.Run(mw);

            Application.Run(new VisiBoole.Forms.MainWindow());
		}
	}
}
