using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

        public Dictionary<string, cVFunction> VFunctions { get; set; }

        /// <summary>
        /// Binding Source for the UserSource ListBox
        /// </summary>
        private BindingSource _UserSourceBindingSource;

        /// <summary>
        /// Constructs an instance of frmMain
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            InitializeUserSource();

            // Load the default UserControl to display on application Startup
            LoadDisplay(new ctlDisplaySingleEditor());
        }

        private void InitializeUserSource()
        {
            // Initialize our dictionary of VisiBoole Functions
            VFunctions = new Dictionary<string, cVFunction>();

            cVFunction val1 = new cVFunction("val1");
            cVFunction val2 = new cVFunction("val2");

            VFunctions.Add(val1.Name, val1);
            VFunctions.Add(val2.Name, val2);

            _UserSourceBindingSource = new BindingSource(VFunctions, null);

            lboSource.DisplayMember = "Key";
            lboSource.ValueMember = "Value";
            lboSource.DataSource = _UserSourceBindingSource;
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

        /// <summary>
        /// Displays the OpenFileDialog for user to select a *.vbi file to open
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult response = openFileDialog1.ShowDialog();

            if (response == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                
                // Check that the selected file is a valid VisiBoole file before attempting to open
                if (string.IsNullOrEmpty(fileName) || Path.GetExtension(fileName) != ".vbi")
                {
                    MessageBox.Show(Path.Combine(Application.StartupPath, "UserSource"));
                    MessageBox.Show("Invalid filename. Select a valid VisiBoole file (*.vbi) extension.", "Invalid Filename", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
                
                //Copy the file to our UserSource project directory
                File.Copy(fileName, Path.Combine(Application.StartupPath, "UserSource"));


            }
        }
    }
}
