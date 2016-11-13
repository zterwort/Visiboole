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
        /// The filename of the file that was opened by the user
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Constructs a new instance of ProcessNewFileEventArgs
        /// </summary>
        /// <param name="filename">The filename of the file that was opened by the user</param>
        public ProcessNewFileEventArgs(string filename)
        {
            this.FileName = filename;
        }
    }
}
