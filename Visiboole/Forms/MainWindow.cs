using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisiBoole.Models;

namespace VisiBoole.Forms
{
    public partial class MainWindow : Form
    {
        private Dictionary<string, SubDesign> SubDesigns = new Dictionary<string, SubDesign>();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            NavList.SetObjects(SubDesigns);
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            Form f = sender as Form;
            Rectangle baseRectangle = new Rectangle(0, 0, f.Width - 1, f.Height - 1);
            LinearGradientBrush gradientBrush = new LinearGradientBrush(baseRectangle, Color.LightSkyBlue, Color.WhiteSmoke, 45);
            e.Graphics.FillRectangle(gradientBrush, baseRectangle);
        }

        /// <summary>
        /// Creates a new SubDesign with a file created from the given filename
        /// </summary>
        /// <param name="filename">The filename of the file to create the new SubDesign with</param>
        /// <returns>Returns the SubDesign created from the given filename</returns>
        private SubDesign CreateNewSubDesign(string filename)
        {
            try
            {
                SubDesign newSubDesign = new SubDesign(filename);
                if (!SubDesigns.ContainsKey(newSubDesign.FileSourceName))
                    SubDesigns.Add(newSubDesign.FileSourceName, newSubDesign);
                return newSubDesign;
            }
            catch (Exception ex)
            {
                Globals.DisplayException(ex);
                return null;
            }
        }

        /// <summary>
        /// Creates a new tab on the TabControl
        /// </summary>
        /// <param name="sd">The SubDesign that is displayed in the new tab</param>
        /// <returns>Returns true if a new tab was successfully created</returns>
        public bool CreateNewTab(SubDesign sd)
        {
            TabPage tab = new TabPage(sd.FileSourceName);
            tab.Name = sd.FileSourceName;
            tab.Controls.Add(sd);
            sd.Dock = DockStyle.Fill;
            if (Editor.TabPages.ContainsKey(sd.FileSourceName))
            {
                int index = Editor.TabPages.IndexOfKey(sd.FileSourceName);
                Editor.TabPages.RemoveByKey(sd.FileSourceName);
                Editor.TabPages.Insert(index, tab);
                sd.TabPageIndex = Editor.TabPages.IndexOf(tab);
                Editor.SelectTab(tab);
                return false;
            }
            if (SubDesigns.Count == 1)
                Editor.TabPages.RemoveAt(0);
            Editor.TabPages.Add(tab);
            sd.TabPageIndex = Editor.TabPages.IndexOf(tab);
            Editor.SelectTab(tab);
            return true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult response = saveFileDialog1.ShowDialog();
                if (response != DialogResult.OK)
                    return;
                string path = saveFileDialog1.FileName;
                if (File.Exists(path))
                    File.Delete(path);
                SubDesign sd = CreateNewSubDesign(path);
                CreateNewTab(sd);
                NavList.SetObjects(SubDesigns);
            }
            catch (Exception ex)
            {
                Globals.DisplayException(ex);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult response = openFileDialog1.ShowDialog();
                if (response != DialogResult.OK)
                    return;
                string path = openFileDialog1.FileName;
                SubDesign sd = CreateNewSubDesign(path);
                CreateNewTab(sd);
                NavList.SetObjects(SubDesigns);
            }
            catch (Exception ex)
            {
                Globals.DisplayException(ex);
                openFileDialog1.FileName = string.Empty;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SubDesign sd = Editor.SelectedTab.SubDesign();
                if (sd == null)
                    MessageBox.Show("File save failed.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sd.SaveTextToFile();
                MessageBox.Show("File save successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                
            }
            catch (Exception ex)
            {
                Globals.DisplayException(ex);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnAction_Click(object sender, EventArgs e)
        {

        }
    }
}
