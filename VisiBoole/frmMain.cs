using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisiBoole
{
    /// <summary>
    /// The Main Menu of the application
    /// </summary>
    public partial class frmMain : Form
    {
        /// <summary>
        /// The currently displayed UserControl
        /// </summary>
        public UserControl CurrentDisplay { get; set; }

        /// <summary>
        /// Constructs an instance of frmMain
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            // Load the default UserControl to display on application Startup
            LoadDisplay(new ctlDisplaySingleEditor());
        }

        /// <summary>
        /// Replace the currently displayed UserControl with the given UserControl
        /// </summary>
        /// <param name="pNewDisplay">The UserControl to replace the currently displayed one with</param>
        private void LoadDisplay(UserControl pNewDisplay)
        {
            if (pNewDisplay == null)
                return;

            pnlDisplay.Controls.Remove(CurrentDisplay);

            CurrentDisplay = pNewDisplay;

            pnlDisplay.Controls.Add(CurrentDisplay);

            CurrentDisplay.Dock = DockStyle.Fill;
            CurrentDisplay.Show();
        }

        /// <summary>
        /// Display the default Single View
        /// </summary>
        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDisplay(new ctlDisplaySingleEditor());
        }

        /// <summary>
        /// Display the Horizontal View
        /// </summary>
        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDisplay(new ctlDisplayHorizontal());
        }

        /// <summary>
        /// Display the Vertical View
        /// </summary>
        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDisplay(new ctlDisplayVertical());
        }


    }
}
