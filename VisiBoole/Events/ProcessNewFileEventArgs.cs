using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiBoole.Events
{
    public class ProcessNewFileEventArgs : EventArgs
    {
        public string FileName { get; set; }

        public ProcessNewFileEventArgs(string filename)
        {
            this.FileName = filename;
        }
    }
}
