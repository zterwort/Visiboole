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
        /// Constructs an instance of MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #region "Utility Functions"

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
        /// <param name="display">The given display to show the user</param>
        public void ShowDisplay(DisplayBase display)
        {
            if (display == null) DisplayErrorMessage(new ArgumentNullException("Unable to load given display - the given display is null."));

            this.MainLayoutPanel.Controls.Add(display, 1, 0);
        }

        #endregion

        #region "Events handled by Controller"

        public event ProcessNewFileHandler ProcessNewFile;
        public event LoadDisplayHandler LoadDisplay;

        protected virtual void OnProcessNewFile(ProcessNewFileEventArgs e) { if (ProcessNewFile != null) ProcessNewFile(this, e); }
        protected virtual void OnLoadDisplay(LoadDisplayEventArgs e) { if (LoadDisplay != null) LoadDisplay(this, e); }

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

                ShowDisplay(args.CurrentDisplay);
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
                LoadDisplayEventArgs args = new LoadDisplayEventArgs(Globals.DisplayType.SINGLE);
                OnLoadDisplay(args);

                ShowDisplay(args.CurrentDisplay);
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
                LoadDisplayEventArgs args = new LoadDisplayEventArgs(Globals.DisplayType.SINGLE);
                OnLoadDisplay(args);

                ShowDisplay(args.CurrentDisplay);
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

                OnProcessNewFile(new ProcessNewFileEventArgs(filename));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
