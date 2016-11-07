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
using System.Collections;

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

        public string[] subDesignKeys { get; set; }
        

        DisplayBase display = new DisplayBase();

        /// <summary>
        /// Binding Source for the UsersubDesigns ListBox
        /// </summary>
        private BindingSource usersubDesignsBindingSource;

        /// <summary>
        /// Constructs an instance of frmMain
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            InitializeUsersubDesigns();

            // Load the default UserControl to display on application Startup
            LoadDisplay(new DisplaySingleEditor());
        }

        private void InitializeUsersubDesigns()
        {
            // Initialize our dictionary of VisiBoole Functions
            Globals.subDesigns = new Dictionary<string, SubDesign>();

            if (!Directory.Exists(Path.Combine(Application.StartupPath, "UserSubDesigns")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "UserSubDesigns"));
            }

            GetFiles(Path.Combine(Application.StartupPath, "UserSubDesigns"));
        }

        private void GetFiles(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] files;
            try
            {
                files = di.GetFiles("*.vbi");
                foreach (FileInfo file in files)
                {
                    if (!Globals.subDesigns.ContainsKey(file.Name))
                    {
                        SubDesign value = new SubDesign(file.Name);
                        value.File = file;
                        Globals.subDesigns.Add(value.Name, value);
                        if (path != Path.Combine(Path.Combine(Application.StartupPath, "UserSubDesigns")))
                        {
                            file.CopyTo(Path.Combine(Path.Combine(Application.StartupPath, "UserSubDesigns"), file.Name));
                        }
                    }
                }
            }
            catch
            {
                FileInfo file = new FileInfo(path);
           
                if (!Globals.subDesigns.ContainsKey(file.Name))
                {
                    SubDesign value = new SubDesign(file.Name);
                    value.File = file;
                    Globals.subDesigns.Add(value.Name, value);
                    if (path != Path.Combine(Path.Combine(Application.StartupPath, "UserSubDesigns")))
                    {
                        file.CopyTo(Path.Combine(Path.Combine(Application.StartupPath, "UserSubDesigns"), file.Name));
                    }
                }
            }
            
            usersubDesignsBindingSource = new BindingSource(Globals.subDesigns, null);
            
            lboSource.DisplayMember = "Key";
            lboSource.ValueMember = "Value";
            lboSource.DataSource = usersubDesignsBindingSource;
        }

        /// <summary>
        /// Replace the currently displayed UserControl with the given UserControl
        /// </summary>
        /// <param name="pNewDisplay">The UserControl to replace the currently displayed one with</param>
        private void LoadDisplay(UserControl pNewDisplay)
        {
            if (pNewDisplay == null)
                return;

            pnlDisplay.Controls.Remove(Globals.CurrentDisplay);

            Globals.CurrentDisplay = pNewDisplay;

            pnlDisplay.Controls.Add(Globals.CurrentDisplay);

            Globals.CurrentDisplay.Dock = DockStyle.Fill;
            Globals.CurrentDisplay.Show();
        }

        /// <summary>
        /// Display the default Single View
        /// </summary>
        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDisplay(new DisplaySingleEditor());
        }

        /// <summary>
        /// Display the Horizontal View
        /// </summary>
        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDisplay(new DisplayHorizontal());
        }

        /// <summary>
        /// Display the Vertical View
        /// </summary>
        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDisplay(new DisplayVertical());
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
                    MessageBox.Show(Path.Combine(Application.StartupPath, "UserSubDesigns"));
                    MessageBox.Show("Invalid filename. Select a valid VisiBoole file (*.vbi) extension.", "Invalid Filename", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                //Copy the file to our UsersubDesigns project directory
                GetFiles(fileName);
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

        private void lboSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            //How to get tab and file we need?
            if(Globals.CurrentDisplay != null)
            {
                if(Globals.CurrentDisplay is DisplaySingleEditor)
                {
                    TabControl tabs = ((VisiBoole.DisplaySingleEditor)Globals.CurrentDisplay).tabEditor;
                    display.GenerateNewTab(tabs, Globals.subDesigns[((lboSource.SelectedItem.ToString().Substring(1)).Split(','))[0]]);
                }
                else if (Globals.CurrentDisplay is DisplayVertical)
                {
                    TabControl tabs = ((VisiBoole.DisplayVertical)Globals.CurrentDisplay).tabEditor;
                    display.GenerateNewTab(tabs, Globals.subDesigns[((lboSource.SelectedItem.ToString().Substring(1)).Split(','))[0]]);
                }
                else if(Globals.CurrentDisplay is DisplayHorizontal)
                {
                    TabControl tabs = ((VisiBoole.DisplayHorizontal)Globals.CurrentDisplay).tabEditor;
                    display.GenerateNewTab(tabs, Globals.subDesigns[((lboSource.SelectedItem.ToString().Substring(1)).Split(','))[0]]);
                }
            }
        }

        private void OutputParser(FileInfo f)
        {
            var Parser = new OutputParser(f);

            List<string> OutputList = Parser.GenerateOutput();
            string OutputText = string.Empty;

            for (int i = 0; i < OutputList.Count; i++)
            {
                OutputText += OutputList[i] + Environment.NewLine;
            }
            var html = new HtmlBuilder(OutputList, f.Name);
            if(!Globals.html.ContainsKey(f.Name))
            {
                Globals.html.Add(f.Name, html.GetHTML());
            }
            else
            {
                Globals.html[f.Name] = html.GetHTML();
            }
            MessageBox.Show(OutputText);
        }

        private void CodeParser(SubDesign s)
        { 
            var parser = new Parser(s);
        }
    }
}
