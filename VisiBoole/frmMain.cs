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

        public Dictionary<string, cVFunction> subRoutines { get; set; }

        /// <summary>
        /// Binding Source for the UserSubRoutines ListBox
        /// </summary>
        private BindingSource userSubRoutinesBindingSource;

        /// <summary>
        /// Constructs an instance of frmMain
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            InitializeUserSubRoutines();

            // Load the default UserControl to display on application Startup
            LoadDisplay(new ctlDisplaySingleEditor());
        }

        private void InitializeUserSubRoutines()
        {
            // Initialize our dictionary of VisiBoole Functions
            subRoutines = new Dictionary<string, cVFunction>();

            if (!Directory.Exists(Path.Combine(Application.StartupPath, "UserSubRoutines")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "UserSubRoutines"));
            }

            GetFiles(Path.Combine(Application.StartupPath, "UserSubRoutines"));
        }

        private void GetFiles(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] files = di.GetFiles("*.vbi");

            foreach (FileInfo file in files)
            {
                if (!subRoutines.ContainsKey(file.Name))
                {
                    cVFunction value = new cVFunction(file.Name);
                    subRoutines.Add(value.Name, value);
                    if(path != Path.Combine(Path.Combine(Application.StartupPath, "UserSubRoutines")))
                    {
                        file.CopyTo(Path.Combine(Path.Combine(Application.StartupPath, "UserSubRoutines"), file.Name));
                    }
                }
            }
            userSubRoutinesBindingSource = new BindingSource(subRoutines, null);
            lboSource.DisplayMember = "Key";
            lboSource.ValueMember = "Value";
            lboSource.DataSource = userSubRoutinesBindingSource;
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
                    MessageBox.Show(Path.Combine(Application.StartupPath, "UserSubRoutines"));
                    MessageBox.Show("Invalid filename. Select a valid VisiBoole file (*.vbi) extension.", "Invalid Filename", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
                
                //Copy the file to our UserSubRoutines project directory
                File.Copy(fileName, Path.Combine(Application.StartupPath, "UserSubRoutines"));
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult response = folderBrowserDialog1.ShowDialog();
            if (response == DialogResult.OK)
            {
                GetFiles(folderBrowserDialog1.SelectedPath);
            }
        }
    }
}
