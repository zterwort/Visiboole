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

namespace VisiBoole
{
    /// <summary>
    /// Base class for our MainWindow displays - Single, Horizontal, and Vertical
    /// </summary>
    public class DisplayBase : VisiBooleAbstract.cDisplayBase
    {
        /// <summary>
        /// The TabControl shared by all of our MainWindow Displays
        /// </summary>
        public TabControl tabControl { get; set; }
        
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

            if (tabControl.TabPages.ContainsKey(sub.FileSourceName)) throw new Exception("A file with that name already exists.");
            tabControl.TabPages.Add(newTabPage);
            sub.TabPageIndex = tabControl.TabPages.IndexOf(newTabPage);
        }

        /// <summary>
        /// Set focus to the TabPage at the given index
        /// </summary>
        /// <param name="tabPageIndex">The index of the TabPage in our TabControl to select</param>
        public void SelectTabPage(int tabPageIndex)
        {
            if (tabPageIndex < 0) throw new Exception("TabPage index must be a positive integer.");

            tabControl.SelectTab(tabPageIndex);
        }

        /// <summary>
        /// Appends the independent variable to the given RichTextBox
        /// </summary>
        /// <param name="var">The independent variable to append</param>
        /// <param name="enable">Whether the given variable is currently True or False</param>
        /// <param name="tbox">The RichTextBox to append the independent variable to</param>
        public void AppendIndependentVariable(string var, bool enable, RichTextBox tbox)
        {
            var link = new LinkLabel();

            link.LinkColor = enable ? Color.Green : Color.Red;
            link.Text = var;
            link.AutoSize = true;
            link.Location = new Point(tbox.Controls.Count * 17, tbox.GetPositionFromCharIndex(tbox.TextLength).Y);

            tbox.Controls.Add(link);
        }

        /// <summary>
        /// Parse the contents of the given SubDesign and create output from it
        /// </summary>
        /// <param name="info">The SubDesign to parse</param>
        public void Run(SubDesign info)
        {
            InputParser parser = new InputParser(info);
            OutputParser output = new OutputParser(info.FileSource);
            List<string> outputText = output.GenerateOutput();
            HtmlBuilder html = new HtmlBuilder(outputText, info.Name);
            string htmlOutput = html.GetHTML();

            if (Globals.CurrentDisplay != null)
            {
                if (Globals.CurrentDisplay is DisplaySingleEditor)
                {
                    WebBrowser browser = new WebBrowser();
                    Form newForm = new Form();
                    html.DisplayHtml(htmlOutput, browser);
                    newForm.Controls.Add(browser);
                    newForm.ShowDialog();
                }
                else if (Globals.CurrentDisplay is DisplayVertical)
                {
                    WebBrowser browser = ((VisiBoole.DisplayVertical)Globals.CurrentDisplay).outputBrowser;
                    html.DisplayHtml(htmlOutput, browser);
                }
                else if (Globals.CurrentDisplay is DisplayHorizontal)
                {
                    WebBrowser browser = ((VisiBoole.DisplayHorizontal)Globals.CurrentDisplay).outputBrowser;
                    html.DisplayHtml(htmlOutput, browser);
                }
            }
        }
    }
}
