using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiBoole.Events
{
    /// <summary>
    /// Arguments for event SaveFile
    /// </summary>
    public class SaveFileEventArgs : EventArgs
    {
        /// <summary>
        /// The index of the currently selected tab
        /// </summary>
        public int tabPageIndex { get; set; }

        /// <summary>
        /// Constructs an instance of SaveFileEventArgs
        /// </summary>
        public SaveFileEventArgs(int tabPageIndex)
        {
            this.tabPageIndex = tabPageIndex;
        }
    }
}
