using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VisiBoole.ParsingEngine.Boolean;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.Models
{
	public class HtmlBuilder
	{
		//string s = "<p>This is a paragraph.H<font color= 'red' > E </ font > LLO.</ p >";

		//color font = <font color='red'>string</font>
		//line should be contained in <p> line </p>
		//List<string> HtmlText = new List<string>();
		public string HtmlText = "";
		public string currentLine = "";

        public HtmlBuilder(List<IObjectCodeElement> output)
        {
            List<List<IObjectCodeElement>> newOutput = PreParseHTML(output);
            int lineNumber = 0;
            foreach (List<IObjectCodeElement> line in newOutput)
            {
                lineNumber++;
                currentLine = "<p>";
                Dictionary<int, int> parenIndexes = new Dictionary<int, int>();

                string fullLine = "";
                int indexer = 0;
                foreach(var token in line)
                {
                    if(token.ObjCodeText == "(" || token.ObjCodeText == ")")
                    {
                        parenIndexes[fullLine.Length] = line.IndexOf(token, indexer);
                        indexer = line.IndexOf(token, indexer) + 1;
                    }
                    fullLine += token.ObjCodeText + " ";
                }

                string outermost = "";
                if(fullLine.Contains("(") && fullLine.Contains(")"))
                {
                    int startIndex = fullLine.IndexOf('(');
                    int endIndex = fullLine.IndexOf(')');
                    List<int> previousStartingIndexes = new List<int>();
                        
                    while(startIndex < endIndex)
                    {
                        int holdingIndex = startIndex;
                        startIndex = fullLine.IndexOf('(', startIndex + 1);

                        if(startIndex == -1 || previousStartingIndexes.Contains(startIndex))
                        {
                            startIndex = holdingIndex;

                            outermost = fullLine.Substring(startIndex, endIndex - startIndex + 1);
                            Expression exp = new Expression();
                            bool colorValue = exp.Solve(outermost);
                            line[parenIndexes[startIndex]].ObjCodeValue = colorValue;
                            line[parenIndexes[endIndex]].ObjCodeValue = colorValue;

                            previousStartingIndexes.Add(startIndex);

                            endIndex = fullLine.IndexOf(')', endIndex + 1);
                            startIndex = 0;

                            if(endIndex == -1)
                            {
                                break;
                            }
                        }
                    }
                }

                foreach (IObjectCodeElement token in line)
                {
                    string variable = token.ObjCodeText;
                    if(variable.Contains(';'))
                    {
                        variable = variable.Substring(0, variable.IndexOf(';'));
                    }
                    bool? value = token.ObjCodeValue;
                    Type varType = token.GetType();

                    if (variable.Contains('('))
                    {
                        if(token.ObjCodeValue == true)
                        {
                            currentLine += "<font color='red' style=\"cursor: no-drop;\" >" + variable + "</font>";
                            currentLine += " ";
                        }
                        else
                        {
                            currentLine += "<font color='green' style=\"cursor: no-drop;\" >" + variable + "</font>";
                            currentLine += " ";
                        }
                        continue;
                    }

                    if (variable.Contains(')'))
                    {
                        if (token.ObjCodeValue == true)
                        {
                            currentLine += "<font color='red' style=\"cursor: no-drop;\" >" + variable + "</font>";
                            currentLine += " ";
                        }
                        else
                        {
                            currentLine += "<font color='green' style=\"cursor: no-drop;\" >" + variable + "</font>";
                            currentLine += " ";
                        }
                        continue;
                    }

                    if (variable.Contains('~'))
                    {
                        if (value.Equals(null))
                        {
                            currentLine += "<font color='black' style=\"cursor: no-drop;\" >" + variable + "</font>";
                            currentLine += " ";
                        }
                        else
                        {
                            if (value.Equals(true))
                            {
                                if (varType == typeof(DependentVariable)) //if variable is dependent
                                {
                                    currentLine += "<font color='black' style=\"cursor: no-drop; text-decoration: overline;\" ></font><font color='green' >" + variable.Substring(1) + "</font>";
                                }
                                else //if variable is independent
                                {
                                    currentLine += "<font color='black' ></font><font color='green' style=\"cursor: hand; text-decoration: overline;\" onclick=\"window.external.Variable_Click('" + variable.Substring(1) + "')\" >" + variable.Substring(1) + "</font>";
                                }
                                currentLine += " ";
                            }
                            else
                            {
                                if (varType == typeof(DependentVariable)) //if variable is dependent
                                {
                                    currentLine += "<font color='black' style=\"cursor: no-drop; text-decoration: overline;\" ></font><font color='red' >" + variable.Substring(1) + "</font>";
                                }
                                else //if variable is independent
                                {
                                    currentLine += "<font color='black' ></font><font color='red' style=\"cursor: hand; text-decoration: overline;\" onclick=\"window.external.Variable_Click('" + variable.Substring(1) + "')\" >" + variable.Substring(1) + "</font>";
                                }
                                currentLine += " ";
                            }
                        }
                    }
                    else
                    {
                        if (value.Equals(null))
                        {
                            currentLine += "<font color='black' style=\"cursor: no-drop;\" >" + variable + "</font>";
                            currentLine += " ";
                        }
                        else
                        {
                            if (value.Equals(true))
                            {
                                if (varType == typeof(DependentVariable)) //if variable is dependent
                                {
                                    currentLine += "<font color='red' style=\"cursor: no-drop;\" >" + variable + "</font>";
                                }
                                else //if variable is independent
                                {
                                    currentLine += "<font color='red' style=\"cursor: hand;\" onclick=\"window.external.Variable_Click('" + variable + "')\" >" + variable + "</font>";
                                }
                                currentLine += " ";
                            }
                            else
                            {
                                if (varType == typeof(DependentVariable)) //if variable is dependent
                                {
                                    currentLine += "<font color='green' style=\"cursor: no-drop;\" >" + variable + "</font>";
                                }
                                else //if variable is independent
                                {
                                    currentLine += "<font color='green' style=\"cursor: hand;\" onclick=\"window.external.Variable_Click('" + variable + "')\" >" + variable + "</font>";
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

        private List<List<IObjectCodeElement>> PreParseHTML(List<IObjectCodeElement> output)
        {
            List<List<IObjectCodeElement>> fullText = new List<List<IObjectCodeElement>>();
            List<IObjectCodeElement> subText = new List<IObjectCodeElement>();
            foreach (IObjectCodeElement element in output)
            {
                Type elementType = element.GetType();
                if (elementType == typeof(LineFeed))
                {
                    fullText.Add(subText);
                    subText = new List<IObjectCodeElement>();
                }
                else
                {
                    subText.Add(element);
                }
            }
            return fullText;
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
            string styles = "<html><head><style type=\"text/css\"> p { margin: 0;} </style ></head >";
            html = styles + html;
            browser.DocumentText = html;
		}
	}
}