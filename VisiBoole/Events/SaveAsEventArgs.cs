using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiBoole.Events
{
    /// <summary>
    /// Arguments for SaveAs event
    /// </summary>
    public class SaveAsEventArgs : EventArgs
    {
        /// <summary>
        /// The file location to save the file at
        /// </summary>
        public string FilePath;

        /// <summary>
        /// Constructs an instance of SaveAsEventArgs
        /// </summary>
        /// <param name="fileName">The file location to save the file at</param>
        public SaveAsEventArgs(string fileName)
        {
            this.FilePath = fileName;
        }

    }
}
