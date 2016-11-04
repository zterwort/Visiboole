using System;
using System.Collections.Generic;
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
                return;
            }

            TextBox textBox = new TextBox();

            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Both;
            textBox.Anchor = AnchorStyles.Bottom & AnchorStyles.Left & AnchorStyles.Right & AnchorStyles.Top;
            textBox.Dock = DockStyle.Fill;

            using (StreamReader reader = info.File.OpenText())
            {
                string text = "";
                while ((text = reader.ReadLine()) != null)
                {
                    textBox.Text += text;
                    textBox.Text += Environment.NewLine;
                }
            }

            info.FileText = textBox.Text;

            newTabPage.Controls.Add(textBox);
            tabs.Controls.Add(newTabPage);
        }
    }
}
