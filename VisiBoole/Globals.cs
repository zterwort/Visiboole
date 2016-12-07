using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisiBoole
{
    public static class Globals
    {
        /// <summary>
        /// This dictionary stores a dictionary of variables. The main key is the filename. 
        ///     The secondary key is the name of the variable. The int value is the value 
        ///     of the variable which will either be a 1 or 0 (true or false).
        /// </summary>
        public static Dictionary<string, Dictionary<string, int>> variables = new Dictionary<string, Dictionary<string, int>>();

        public static Dictionary<string, Dictionary<string, List<string>>> dependencies = new Dictionary<string, Dictionary<string, List<string>>>();

        public static Dictionary<string, Dictionary<string, string>> expressions = new Dictionary<string, Dictionary<string, string>>();

        public static Dictionary<string, string> html = new Dictionary<string, string>();
        public static Dictionary<string, SubDesign> subDesigns { get; set; }

        public static DisplayBase CurrentDisplay { get; set; }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Global collection of all open subdesigns
        /// </summary>
        public static Dictionary<string, SubDesign> SubDesigns;

        /// <summary>
        /// The TabControl that is shared between all three displays
        /// </summary>
        public static TabControl tabControl;

        public static string CurrentTab;

        /// <summary>
        /// Constants corresponding to the three different display types hosted by the MainWindow
        /// </summary>
        public enum DisplayType
        {
            SINGLE,
            HORIZONTAL,
            VERTICAL,
            SINGLEOUTPUT
        }

    }
}
