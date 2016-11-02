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
    public class cDisplayBase : VisiBooleAbstract.cDisplayBase
    {
        public void GenerateNewTab(TabControl tabs, FileInfo file)
        {
            TabPage newTabPage = new TabPage(file.Name);
            using (StreamReader reader = file.OpenText())
            {
                string text = "";
                while ((text = reader.ReadLine()) != null)
                {
                    newTabPage.Text += text + "\n";
                }
            }
            tabs.Controls.Add(newTabPage);
        }
    }
}
