using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VisiBoole
{
    /// <summary>
    /// A User-Created VisiBoole Function
    /// </summary>
    public class SubDesign
    {
        /// <summary>
        /// The name of this VFunction - will be displayed to user
        /// </summary>
        public String Name { get; set; }

        public string FileText { get; set; }

        public FileInfo File { get; set; }
        /// <summary>
        /// The full path of this VFunction
        /// </summary>
        public String FilePath { get; set; }

        public SubDesign(string pName)
        {
            if (!string.IsNullOrEmpty(pName)) 
            {
                this.Name = pName;
            }
            else
            {
                this.Name = "";
            }
        }
    }
}
