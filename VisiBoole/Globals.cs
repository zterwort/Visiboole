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

        public static Dictionary<string, string> html = new Dictionary<string, string>();
        public static Dictionary<string, SubDesign> subDesigns { get; set; }
        public static UserControl CurrentDisplay { get; set; }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Dictionary<string, SubDesign> SubDesigns;

    }
}
