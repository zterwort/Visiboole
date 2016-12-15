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
		//string s = "<p>This is a paragraph.H<font color= 'red' > E </ font > LLO.</ p >";

		//color font = <font color='red'>string</font>
		//line should be contained in <p> line </p>
		//List<string> HtmlText = new List<string>();
		string HtmlText = "";
		string currentLine = "";
		public HtmlBuilder(List<string> text, string fileName, Dictionary<string, int> Variables, Dictionary<string, string> Expressions)
		{
			foreach (string line in text)
			{
				currentLine = "<p>";
				string[] tokens = line.Split(' ');
				foreach (string token in tokens)
				{
					if (token.Contains('~'))
					{
						if (!Variables.ContainsKey(token.Substring(1)))
						{
							currentLine += "<font color='black' style=\"cursor: no-drop;\" >" + token + "</font>";
							currentLine += " ";
						}
						else
						{
							if (Variables[token.Substring(1)] == 1)
							{
								if (Expressions.ContainsKey(token))
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
								if (Expressions.ContainsKey(token))
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
						if (!Variables.ContainsKey(token))
						{
							currentLine += "<font color='black' style=\"cursor: no-drop;\" >" + token + "</font>";
							currentLine += " ";
						}
						else
						{
							if (Variables[token] == 1)
							{
								if (Expressions.ContainsKey(token))
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
								if (Expressions.ContainsKey(token))
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

		/// <summary>
		/// Returns the generated HTML text
		/// </summary>
		/// <returns>Returns the generated HTML text</returns>
		public string GetHTML()
		{
			return HtmlText;
		}

		/// <summary>
		/// Displays the html text within the give WebBrowser object
		/// </summary>
		/// <param name="html">The html text to display</param>
		/// <param name="browser">The WebBrowser object to display the HTML in</param>
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