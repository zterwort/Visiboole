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

        //get output text

        //loop through all the text broke up by spaces ' ';
        //if not in Globals.variables color black
        //if in Globals.variables get value
        //if value = true, color red
        //if value = false, color green
        //once finished looping

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
            browser.Navigate("about:blank");
            if (browser.Document != null)
            {
                browser.Document.Write(string.Empty);
            }
            browser.DocumentText = html;
        }

        /*public RichTextBox GetRTB()
        {
            return new RichTextBox();
        }*/

        /*public void AppendText(this RichTextBox rtb, string text, Color color)
        {
            rtb.SelectionStart = rtb.TextLength;
            rtb.SelectionLength = 0;

            rtb.SelectionColor = color;
            rtb.AppendText(text);
            rtb.SelectionColor = rtb.ForeColor;
        }*/
        /*var userid = "USER0001";
        var message = "Access denied";
        var box = new RichTextBox
        {
            Dock = DockStyle.Fill,
            Font = new Font("Courier New", 10)
        };

        box.AppendText("[" + DateTime.Now.ToShortTimeString() + "]", Color.Red);
        box.AppendText(" ");
        box.AppendText(userid, Color.Green);
        box.AppendText(": ");
        box.AppendText(message, Color.Blue);
        box.AppendText(Environment.NewLine);

        new Form {Controls = {box}}.ShowDialog();*/
    }
}
