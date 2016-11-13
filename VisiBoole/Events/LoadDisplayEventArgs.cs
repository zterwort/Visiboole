using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiBoole.Events
{
    /// <summary>
    /// Arguments for Event LoadDisplay
    /// </summary>
    public class LoadDisplayEventArgs : EventArgs
    {
        /// <summary>
        /// The display type that is to be loaded by the MainWindowController
        /// </summary>
        public Globals.DisplayType DisplayType { get; set; }

        /// <summary>
        /// The display that is loaded by the MainWindowController
        /// </summary>
        public DisplayBase CurrentDisplay { get; set; }

        /// <summary>
        /// The display that was loaded before the CurrentDisplay in the MainWindowController
        /// </summary>
        public DisplayBase PreviousDisplay { get; set; }

        /// <summary>
        /// Constructs an instance of LoadDisplayEventArgs
        /// </summary>
        /// <param name="display">The display that has been loaded into the MainWindowController</param>
        public LoadDisplayEventArgs(Globals.DisplayType displayType)
        {
            this.DisplayType = displayType;
        }
    }
}
