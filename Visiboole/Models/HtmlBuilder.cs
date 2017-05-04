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
                int openParenthesesCount = 0;
                int closedParenthesesCount = 0;
                string fullLine = "";
                foreach(var token in line)
                {
                    fullLine += token.ObjCodeText;
                }

                string outermost = "";
                if(fullLine.Contains("(") && fullLine.Contains(")"))
                {
                    int openCounter = 0;
                    int closeCounter = 0;
                    string holder = fullLine;
                    while (holder.Contains("("))
                    {
                        openCounter++;
                        holder = holder.Remove(holder.IndexOf("("), 1);
                    }
                    while (holder.Contains(")"))
                    {
                        closeCounter++;
                        holder = holder.Remove(holder.IndexOf(")"), 1);
                    }

                    if (openCounter == closeCounter)
                    {
                        int startIndex = fullLine.IndexOf('(');
                        int endIndex = fullLine.LastIndexOf(')');
                        outermost = fullLine.Substring(startIndex, endIndex - startIndex + 1);
                    }
                    else
                    {
                        MessageBox.Show("Parentheses do not match on line: " + lineNumber, "Syntax Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        HtmlText = null;
                        return;
                    }
                }



                if (!String.IsNullOrWhiteSpace(outermost))
                {
                    Expression exp = new Expression();
                    bool colorValue = exp.Solve(outermost);
                    line.First(x => x.ObjCodeText == "(").ObjCodeValue = colorValue;
                    line.Last(x => x.ObjCodeText == ")").ObjCodeValue = colorValue;
                }


                //string[] tokens = line.Split(' ');

                foreach (IObjectCodeElement token in line)
                {
                    string variable = token.ObjCodeText;
                    if(variable.Contains(';'))
                    {
                        variable = variable.Substring(0, variable.IndexOf(';'));
                    }
                    bool? value = token.ObjCodeValue;
                    Type varType = token.GetType();
                    //These lists are used to keep track of the amount of parenthesis in front or behind a variable
                    List<string> openParenthesisHolder = new List<string>();
                    List<string> closeParenthesisHolder = new List<string>();

                    if (variable.Contains('('))
                    {
                        if(token.ObjCodeValue == true)
                        {
                            currentLine += "<font color='green' style=\"cursor: no-drop;\" >" + variable + "</font>";
                            currentLine += " ";
                        }
                        else
                        {
                            currentLine += "<font color='red' style=\"cursor: no-drop;\" >" + variable + "</font>";
                            currentLine += " ";
                        }
                        continue;
                    }

                    if (variable.Contains(')'))
                    {
                        if (token.ObjCodeValue == true)
                        {
                            currentLine += "<font color='green' style=\"cursor: no-drop;\" >" + variable + "</font>";
                            currentLine += " ";
                        }
                        else
                        {
                            currentLine += "<font color='red' style=\"cursor: no-drop;\" >" + variable + "</font>";
                            currentLine += " ";
                        }
                        continue;
                    }

                    ////Used to holder the amount of parenthesis in front or behind each variable
                    //string openParenthesis = String.Join(String.Empty, openParenthesisHolder);
                    //string closeParenthesis = String.Join(String.Empty, closeParenthesisHolder);

                    if (variable.Contains('~'))
                    {
                        if (value.Equals(null))
                        {
                            //currentLine += "<font color='black' >" + openParenthesis + "</font>";
                            currentLine += "<font color='black' style=\"cursor: no-drop;\" >" + variable + "</font>";
                            //currentLine += "<font color='black' >" + closeParenthesis + "</font>";
                            currentLine += " ";
                        }
                        else
                        {
                            if (value.Equals(true))
                            {
                                if (varType == typeof(DependentVariable)) //if variable is dependent
                                {
                                    //currentLine += "<font color='black' >" + openParenthesis + "</font>";
                                    currentLine += "<font color='black' style=\"cursor: no-drop;\" >~</font><font color='green' >" + variable.Substring(1) + "</font>";
                                    //currentLine += "<font color='black' >" + closeParenthesis + "</font>";
                                }
                                else //if variable is independent
                                {
                                    //currentLine += "<font color='black' >" + openParenthesis + "</font>";
                                    currentLine += "<font color='black' >~</font><font color='green' style=\"cursor: hand;\" onclick=\"window.external.Variable_Click('" + variable.Substring(1) + "')\" >" + variable.Substring(1) + "</font>";
                                    //currentLine += "<font color='black' >" + closeParenthesis + "</font>";
                                }
                                currentLine += " ";
                            }
                            else
                            {
                                if (varType == typeof(DependentVariable)) //if variable is dependent
                                {
                                    //currentLine += "<font color='black' >" + openParenthesis + "</font>";
                                    currentLine += "<font color='black' style=\"cursor: no-drop;\" >~</font><font color='red' >" + variable.Substring(1) + "</font>";
                                    //currentLine += "<font color='black' >" + closeParenthesis + "</font>";
                                }
                                else //if variable is independent
                                {
                                    //currentLine += "<font color='black' >" + openParenthesis + "</font>";
                                    currentLine += "<font color='black' >~</font><font color='red' style=\"cursor: hand;\" onclick=\"window.external.Variable_Click('" + variable.Substring(1) + "')\" >" + variable.Substring(1) + "</font>";
                                    //currentLine += "<font color='black' >" + closeParenthesis + "</font>";
                                }
                                currentLine += " ";
                            }
                        }
                    }
                    else
                    {
                        if (value.Equals(null))
                        {
                            //currentLine += "<font color='black' >" + openParenthesis + "</font>";
                            currentLine += "<font color='black' style=\"cursor: no-drop;\" >" + variable + "</font>";
                            //currentLine += "<font color='black' >" + closeParenthesis + "</font>";
                            currentLine += " ";
                        }
                        else
                        {
                            if (value.Equals(true))
                            {
                                if (varType == typeof(DependentVariable)) //if variable is dependent
                                {
                                    //currentLine += "<font color='black' >" + openParenthesis + "</font>";
                                    currentLine += "<font color='red' style=\"cursor: no-drop;\" >" + variable + "</font>";
                                    //currentLine += "<font color='black' >" + closeParenthesis + "</font>";
                                }
                                else //if variable is independent
                                {
                                    //currentLine += "<font color='black' >" + openParenthesis + "</font>";
                                    currentLine += "<font color='red' style=\"cursor: hand;\" onclick=\"window.external.Variable_Click('" + variable + "')\" >" + variable + "</font>";
                                    //currentLine += "<font color='black' >" + closeParenthesis + "</font>";
                                }
                                currentLine += " ";
                            }
                            else
                            {
                                if (varType == typeof(DependentVariable)) //if variable is dependent
                                {
                                    //currentLine += "<font color='black' >" + openParenthesis + "</font>";
                                    currentLine += "<font color='green' style=\"cursor: no-drop;\" >" + variable + "</font>";
                                    //currentLine += "<font color='black' >" + closeParenthesis + "</font>";
                                }
                                else //if variable is independent
                                {
                                    //currentLine += "<font color='black' >" + openParenthesis + "</font>";
                                    currentLine += "<font color='green' style=\"cursor: hand;\" onclick=\"window.external.Variable_Click('" + variable + "')\" >" + variable + "</font>";
                                    //currentLine += "<font color='black' >" + closeParenthesis + "</font>";
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
            browser.DocumentText = html;
		}
	}
}