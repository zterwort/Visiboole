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
    public partial class Form1 : Form
    {
        public UserControl CurrentDisplay { get; set; }

        public Form1()
        {
            InitializeComponent();

            LoadDisplay(new SingleViewEdit());
        }

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

        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
