using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisiBoole.Controllers;

namespace VisiBoole
{
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

            Globals.SubDesigns = new Dictionary<string, SubDesign>();
            Globals.tabControl = new TabControl();

            MainWindow mw = new MainWindow();
            MainWindowController mwc = new MainWindowController(mw);

            Application.Run(mw);
        }
    }
}
