using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisiBooleAbstract;

namespace VisiBoole
{
    /// <summary>
    /// The Horizontal View for the main menu display
    /// </summary>
    public partial class DisplayHorizontal : DisplayBase
    {
        /// <summary>
        /// Constructs an instance of ctlDisplayHorizontal
        /// </summary>
        public DisplayHorizontal()
        {
            InitializeComponent();

            this.btnRun.Click += new System.EventHandler(base.btnRun_Click);
        }
    }
}
