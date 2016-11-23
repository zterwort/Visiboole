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
using VisiBoole.Events;

namespace VisiBoole
{
    /// <summary>
    /// The Main Menu of the application
    /// </summary>
    public partial class MainWindow : Form, IMainWindow
    {
        /// <summary>
        /// The display that is currently hosted by the MainWindow
        /// </summary>
        private DisplayBase currentDisplay;

        /// <summary>
        /// Constructs an instance of MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #region "Utility Functions"

        /// <summary>
        /// Adds the toplevel filename of the given path as a new node on our NavTree
        /// </summary>
        /// <param name="path">The full path of the file to add</param>
        private void AddNavTreeNode(string path)
        {
            string filename = path.Substring(path.LastIndexOf("\\") + 1);

            TreeNode node = new TreeNode(filename);
            node.Name = filename;

            if (NavTree.Nodes.ContainsKey(filename)) DisplayErrorMessage(new Exception(string.Concat("Node ", filename, " already exists in 'My SubDesings'.")));
            NavTree.Nodes[0].Nodes.Add(node);

            NavTree.ExpandAll();
        }

        /// <summary>
        /// Displays any errors that are caught to the user
        /// </summary>
        /// <param name="ex">The Exception to display to the user</param>
        public void DisplayErrorMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Loads the given display into our MainWindow for the user to view
        /// </summary>
        /// <param name="curDisplay">The given display to show the user</param>
        public void ShowDisplay(DisplayBase previousDisplay, DisplayBase curDisplay)
        {
            if (curDisplay == null) DisplayErrorMessage(new ArgumentNullException("Unable to load given display - the given display is null."));

            if (this.MainLayoutPanel.Controls.Contains(previousDisplay)) this.MainLayoutPanel.Controls.Remove(previousDisplay);
            if (this.MainLayoutPanel.Controls.Contains(OpenFileLinkLabel)) this.MainLayoutPanel.Controls.Remove(OpenFileLinkLabel);

            this.MainLayoutPanel.Controls.Add(curDisplay, 1, 0);
            this.currentDisplay = curDisplay;
        }

        #endregion

        #region "Events handled by Controller"

        public event ProcessNewFileHandler ProcessNewFile;
        public event LoadDisplayHandler LoadDisplay;
        public event SaveFileHandler SaveFile;
        public event SaveAsHandler SaveAs;

        protected virtual void OnProcessNewFile(ProcessNewFileEventArgs e) { if (ProcessNewFile != null) ProcessNewFile(this, e); }
        protected virtual void OnLoadDisplay(LoadDisplayEventArgs e) { if (LoadDisplay != null) LoadDisplay(this, e); }
        protected virtual void OnSaveFile(SaveFileEventArgs e) { if (SaveFile != null) SaveFile(this, e); }
        protected virtual void OnSaveAs(SaveAsEventArgs e) { if (SaveAs != null) SaveAs(this, e); }

        #endregion

        #region "Click Events"

        /// <summary>
        /// Loads the single display usercontrol into this MainWindow
        /// </summary>
        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDisplayEventArgs args = new LoadDisplayEventArgs(Globals.DisplayType.SINGLE);
                OnLoadDisplay(args);

                ShowDisplay(args.PreviousDisplay, args.CurrentDisplay);
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex);
            }
        }

        /// <summary>
        /// Loads the horizontal display usercontrol into this MainWindow
        /// </summary>
        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDisplayEventArgs args = new LoadDisplayEventArgs(Globals.DisplayType.HORIZONTAL);
                OnLoadDisplay(args);

                ShowDisplay(args.PreviousDisplay, args.CurrentDisplay);
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex);
            }
        }

        /// <summary>
        /// Loads the vertical display usercontrol into this MainWindow
        /// </summary>
        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDisplayEventArgs args = new LoadDisplayEventArgs(Globals.DisplayType.VERTICAL);
                OnLoadDisplay(args);

                ShowDisplay(args.PreviousDisplay, args.CurrentDisplay);
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex);
            }
        }

        /// <summary>
        /// Display OpenFileDialog and process the selected File
        /// </summary>
        private void OpenFileLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                var response = openFileDialog1.ShowDialog();
                if (response != DialogResult.OK) return;

                string filename = openFileDialog1.FileName;

                ProcessNewFileEventArgs args = new ProcessNewFileEventArgs(filename);
                OnProcessNewFile(args);

                AddNavTreeNode(filename);

                ShowDisplay(args.PreviousDisplay, args.CurrentDisplay);
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex);
            }
        }

        /// <summary>
        /// Creates and processes a new file created by the user
        /// </summary>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.FileName = "VisiBooleFile1.vbi";
                var response = saveFileDialog1.ShowDialog();
                if (response != DialogResult.OK) return;

                ProcessNewFileEventArgs args = new ProcessNewFileEventArgs(saveFileDialog1.FileName);
                OnProcessNewFile(args);
                AddNavTreeNode(args.FileName);

                ShowDisplay(args.PreviousDisplay, args.CurrentDisplay);
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex);
            }
        }

        /// <summary>
        /// Display OpenFileDialog and process the selected File
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var response = openFileDialog1.ShowDialog();
                if (response != DialogResult.OK) return;

                string filename = openFileDialog1.FileName;

                ProcessNewFileEventArgs args = new ProcessNewFileEventArgs(filename);
                OnProcessNewFile(args);

                AddNavTreeNode(filename);

                ShowDisplay(args.PreviousDisplay, args.CurrentDisplay);
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex);
            }
        }

        /// <summary>
        /// Saves the contents of the currently selected tab
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileEventArgs args = new SaveFileEventArgs(Globals.tabControl.SelectedIndex);
            OnSaveFile(args);

            MessageBox.Show("File save successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Saves the content in the selected tabpage in the file indicated by the user
        /// </summary>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult response = saveFileDialog1.ShowDialog();
            if (response == DialogResult.OK)
            {
                SaveAsEventArgs args = new SaveAsEventArgs(saveFileDialog1.FileName);
                OnSaveAs(args);
                AddNavTreeNode(args.FileName);
                MessageBox.Show("File save successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

    }
}

//private void OutputParser(FileInfo f)
//{
//    var Parser = new OutputParser(f);

//    List<string> OutputList = Parser.GenerateOutput();
//    string OutputText = string.Empty;

//    for (int i = 0; i < OutputList.Count; i++)
//    {
//        OutputText += OutputList[i] + Environment.NewLine;
//    }
//    var html = new HtmlBuilder(OutputList, f.Name);
//    if(!Globals.html.ContainsKey(f.Name))
//    {
//        Globals.html.Add(f.Name, html.GetHTML());
//    }
//    else
//    {
//        Globals.html[f.Name] = html.GetHTML();
//    }
//    MessageBox.Show(OutputText);
//}
