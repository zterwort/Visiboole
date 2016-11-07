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
    /// Dummy class for cDisplayBase Inheritance - necessary due to a limitation of the .Net framework
    /// </summary>
    public class DisplayBase : VisiBooleAbstract.cDisplayBase
    {
        public void GenerateNewTab(TabControl tabs, SubDesign info)
        {

            TabPage newTabPage = new TabPage(info.File.Name);

            //Check if tabs all ready has the file in it, if so, return.
            if (tabs.Controls["{ TabPage: {"+ info.File.Name + "} }"] != null)
            {
                //Give focus
                return;
            }

            info.Multiline = true;
            info.ScrollBars = RichTextBoxScrollBars.Both;
            info.Anchor = AnchorStyles.Bottom & AnchorStyles.Left & AnchorStyles.Right & AnchorStyles.Top;
            info.Dock = DockStyle.Fill;

            using (StreamReader reader = info.File.OpenText())
            {
                string text = "";
                while ((text = reader.ReadLine()) != null)
                {
                    info.Text += text;
                    info.Text += Environment.NewLine;
                }
            }

            info.FileText = info.Text;

            newTabPage.Controls.Add(info);
            tabs.Controls.Add(newTabPage);
        }

        public void AppendIndependentVariable(string var, bool enable, RichTextBox tbox)
        {
            var link = new LinkLabel();

            link.LinkColor = enable ? Color.Green : Color.Red;
            link.Text = var;
            link.AutoSize = true;
            link.Location = new Point(tbox.Controls.Count * 17, tbox.GetPositionFromCharIndex(tbox.TextLength).Y);

            tbox.Controls.Add(link);
        }



        public void Run(SubDesign info)
        {
            Parser parser = new Parser(info);
            OutputParser output = new OutputParser(info.File);
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
                    browser.Anchor = AnchorStyles.Left & AnchorStyles.Right & AnchorStyles.Top & AnchorStyles.Bottom;
                    browser.Dock = DockStyle.Fill;
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
