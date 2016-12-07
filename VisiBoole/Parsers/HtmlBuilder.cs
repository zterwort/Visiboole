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
                    if(token.Contains('~'))
                    {
                        if (!Globals.variables[fileName].ContainsKey(token.Substring(1)))
                        {
                            currentLine += "<font color='black' style=\"cursor: no-drop;\" >" + token + "</font>";
                            currentLine += " ";
                        }
                        else
                        {
                            if (Globals.variables[fileName][token.Substring(1)] == 1)
                            {
                                if (Globals.expressions[Globals.CurrentTab].ContainsKey(token))
                                {
                                    currentLine += "<font color='black' style=\"cursor: no-drop;\" >~</font><font color='green' >" + token.Substring(1) + "</font>";
                                }
                                else
                                {
                                    currentLine += "<font color='black' >~</font><font color='green' style=\"cursor: hand;\" onclick=\"window.external.Variable_Click('" + token.Substring(1) + "')\" >" + token.Substring(1) + "</font>";
                                }
                                currentLine += " ";
                            }
                            else
                            {
                                if (Globals.expressions[Globals.CurrentTab].ContainsKey(token))
                                {
                                    currentLine += "<font color='black' style=\"cursor: no-drop;\" >~</font><font color='red' >" + token.Substring(1) + "</font>";
                                }
                                else
                                {
                                    currentLine += "<font color='black' >~</font><font color='red' style=\"cursor: hand;\" onclick=\"window.external.Variable_Click('" + token.Substring(1) + "')\" >" + token.Substring(1) + "</font>";
                                }
                                currentLine += " ";
                            }
                        }
                    }
                    else
                    {
                        if (!Globals.variables[fileName].ContainsKey(token))
                        {
                            currentLine += "<font color='black' style=\"cursor: no-drop;\" >" + token + "</font>";
                            currentLine += " ";
                        }
                        else
                        {
                            if (Globals.variables[fileName][token] == 1)
                            {
                                if (Globals.expressions[Globals.CurrentTab].ContainsKey(token))
                                {
                                    currentLine += "<font color='red' style=\"cursor: no-drop;\" >" + token + "</font>";
                                }
                                else
                                {
                                    currentLine += "<font color='red' style=\"cursor: hand;\" onclick=\"window.external.Variable_Click('" + token + "')\" >" + token + "</font>";
                                }
                                currentLine += " ";
                            }
                            else
                            {
                                if (Globals.expressions[Globals.CurrentTab].ContainsKey(token))
                                {
                                    currentLine += "<font color='green' style=\"cursor: no-drop;\" >" + token + "</font>";
                                }
                                else
                                {
                                    currentLine += "<font color='green' style=\"cursor: hand;\" onclick=\"window.external.Variable_Click('" + token + "')\" >" + token + "</font>";
                                }
                                currentLine += " ";
                            }
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
