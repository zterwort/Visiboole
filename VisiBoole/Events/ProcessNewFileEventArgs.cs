using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiBoole.Events
{
    /// <summary>
    /// Arguments for ProcessNewFile event
    /// </summary>
    public class ProcessNewFileEventArgs : EventArgs
    {
        /// <summary>
        /// The path of the file that will be processed
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The name of the file that was processed
        /// </summary>
        public string FileName { get; set; }

        

        /// <summary>
        /// The display that is loaded by the MainWindowController
        /// </summary>
        public DisplayBase CurrentDisplay { get; set; }

        /// <summary>
        /// The display that was loaded before the CurrentDisplay in the MainWindowController
        /// </summary>
        public DisplayBase PreviousDisplay { get; set; }

        /// <summary>
        /// Constructs a new instance of ProcessNewFileEventArgs
        /// </summary>
        /// <param name="filename">The filename of the file that was opened by the user</param>
        public ProcessNewFileEventArgs(string filename)
        {
            this.FilePath = filename;
        }
    }
}
