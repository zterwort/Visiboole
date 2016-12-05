using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VisiBoole
{
    public class HtmlBuilder
    {
        string s = "<p>This is a paragraph.H<font color= 'red' > E </ font > LLO.</ p >";

        //color font = <font color='red'>string</font>
        //line should be contained in <p> line </p>
        //List<string> HtmlText = new List<string>();
        string HtmlText = "";
        string currentLine = "";
        public HtmlBuilder(List<string> text, string fileName)
        {
            foreach(string line in text)
            {
                currentLine = "<p>";
                string[] tokens = line.Split(' ');
                foreach(string token in tokens)
                {
                    if(!Globals.variables[fileName].ContainsKey(token))
                    {
                        currentLine += "<font color='black'>" + token + "</font>";
                        currentLine += " ";
                    }
                    else
                    {
                        if(Globals.variables[fileName][token] == 1)
                        {
                            currentLine += "<font color='red'>" + token + "</font>";
                            currentLine += " ";
                        }
                        else
                        {
                            currentLine += "<font color='green'>" + token + "</font>";
                            currentLine += " ";
                        }
                    }
                }
                currentLine = currentLine.Substring(0, currentLine.Length - 1);
                currentLine += "</p>";
                HtmlText += currentLine + "\n";
            }
        }

        public string GetHTML()
        {
            return HtmlText;
        }

        public void DisplayHtml(string html, WebBrowser browser)
        {
            browser.Refresh();
            browser.Navigate("about:blank");
            if (browser.Document != null)
            {
                browser.Document.Write(string.Empty);
            }
            browser.DocumentText = html;
        }
    }
}
