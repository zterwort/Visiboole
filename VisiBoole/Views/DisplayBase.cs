using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisiBooleAbstract;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using VisiBoole.Events;

namespace VisiBoole
{
    /// <summary>
    /// Base class for our MainWindow displays - Single, Horizontal, and Vertical
    /// </summary>
    public class DisplayBase : VisiBooleAbstract.cDisplayBase
    {        
        /// <summary>
        /// Creates a new TabPage with the given SubDesign and appends it to our TabControl
        /// </summary>
        /// <param name="sub">The SubDesign to be consumed by the new TabPage</param>
        public void CreateNewTab(SubDesign sub)
        {
            if (sub == null) return;

            TabPage newTabPage = new TabPage(sub.FileSourceName);
            newTabPage.Name = sub.FileSourceName;

            newTabPage.Controls.Add(sub);
            sub.Dock = DockStyle.Fill;

            if (Globals.tabControl.TabPages.ContainsKey(sub.FileSourceName)) throw new Exception("A file with that name already exists.");
            Globals.tabControl.TabPages.Add(newTabPage);
            sub.TabPageIndex = Globals.tabControl.TabPages.IndexOf(newTabPage);
        }

        /// <summary>
        /// Set focus to the TabPage at the given index
        /// </summary>
        /// <param name="tabPageIndex">The index of the TabPage in our TabControl to select</param>
        protected void SelectTabPage(int tabPageIndex)
        {
            if (tabPageIndex < 0) throw new Exception("TabPage index must be a positive integer.");

            Globals.tabControl.SelectTab(tabPageIndex);
        }

        /// <summary>
        /// Appends the independent variable to the given RichTextBox
        /// </summary>
        /// <param name="var">The independent variable to append</param>
        /// <param name="enable">Whether the given variable is currently True or False</param>
        /// <param name="tbox">The RichTextBox to append the independent variable to</param>
        protected void AppendIndependentVariable(string var, bool enable, RichTextBox tbox)
        {
            var link = new LinkLabel();

            link.LinkColor = enable ? Color.Green : Color.Red;
            link.Text = var;
            link.AutoSize = true;
            link.Location = new Point(tbox.Controls.Count * 17, tbox.GetPositionFromCharIndex(tbox.TextLength).Y);

            tbox.Controls.Add(link);
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            OnShowSingleOutput(sender, e);
        }

        public event ShowSingleOutputHandler ShowSingleOutput;

        private void OnShowSingleOutput(Object sender, EventArgs e)
        {
            ShowSingleOutput?.Invoke(sender, e);

        }
        
    }
}
