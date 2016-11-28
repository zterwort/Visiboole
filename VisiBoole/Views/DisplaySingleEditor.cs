using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisiBoole
{
    /// <summary>
    /// The default view for the main menu display
    /// </summary>
    public partial class DisplaySingleEditor : DisplayBase
    {
        /// <summary>
        /// Constructs an instance of ctlDisplaySingleEditor
        /// </summary>
        public DisplaySingleEditor()
        {
            InitializeComponent();
            this.btnRun.Click += new System.EventHandler(base.btnRun_Click);
        }
    }
}
