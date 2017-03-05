using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VisiBoole
{
	/// <summary>
	/// Parses the VisiBoole source code input by the user
	/// </summary>
	public class InputParser
	{
        SubDesign subDesign;
		/// <summary>
		/// The current dependent variable
		/// </summary>
		public string currentDependent;

		/// <summary>
		/// Constructs an instance of InputParser
		/// </summary>
		public InputParser(SubDesign sub)//string[] codeText, string fileName)
		{
            this.subDesign = sub;
		}

		/// <summary>
		/// Parses the VisiBoole source code from the user into independent/dependent variables and their associated expressions
		/// </summary>
		/// <param name="sub">The SubDesign containing the text to be parsed</param>
		/// <param name="variableClicked">The variable that was last clicked by the user, if any</param>
		public void ParseInput(string variableClicked)
		{
			if (String.IsNullOrEmpty(variableClicked))
			{
				using (StreamReader reader = subDesign.FileSource.OpenText())
				{
					string text = "";
					int lineNumber = 1;

					while ((text = reader.ReadLine()) != null)
					{
						if (!text.Contains(';'))
						{

						}
						else
						{
							ContainsVariable(text.Substring(0, text.Length - 1), lineNumber);
						}
						lineNumber++;
					}
				}
			}
			else
			{
				//Globals.CurrentTab = subDesign.FileSourceName;
				int newValue = Negate(subDesign.Variables[variableClicked]);
				subDesign.Variables[variableClicked] = newValue;

				//build list of all dependent variables based on user click
				List<string> totalVariables = new List<string>();

				foreach (string dependentVariable in subDesign.Dependencies[variableClicked])
				{
					totalVariables.Add(dependentVariable);
				}

				int count = 0;
				int end = totalVariables.Count;

				while (count != end)
				{
					for (int i = count; i < end; i++)
					{
						foreach (string dependentVariable in subDesign.Dependencies[totalVariables[i]])
						{
							totalVariables.Add(dependentVariable);
						}
					}
					count = end;
					end = totalVariables.Count;
				}

				foreach (string dependentVariable in totalVariables)
				{
					//currentDependent is used in SolveExpression()
					currentDependent = dependentVariable;
					int updatedVariable = SolveExpression(subDesign.Expressions[dependentVariable], -1);
					subDesign.Variables[dependentVariable] = updatedVariable;
				}

				//all dependent variable list(loop through with foreach)
				/*foreach(string dependentVariable in Globals.dependencies[Globals.CurrentTab][variableClicked])
				{                  
					currentDependent = dependentVariable;
					int updatedVariable = SolveExpression(Globals.expressions[Globals.CurrentTab][dependentVariable], -1);
					Globals.variables[Globals.CurrentTab][dependentVariable] = updatedVariable;
				}*/
			}
		}

		/// <summary>
		/// Checks to see if the line of code contains variables; if so, splits them into independent/dependent variable expressions
		/// </summary>
		/// <param name="lineOfCode">The line of code to check</param>
		/// <param name="lineNumber">The line number of the line of code to check</param>
		/// <returns>Returns the expression or the line given to it, depending on whether variables were found</returns>
		public string ContainsVariable(string lineOfCode, int lineNumber)
		{
			if (!lineOfCode.Contains('='))
			{
				string[] independent = lineOfCode.Split(' ');
				foreach (string s in independent)
				{
					if (s.Contains('*'))
					{
						if (!subDesign.Variables.ContainsKey(s.Substring(1)))
						{
							subDesign.Variables.Add(s.Substring(1), 1);
							subDesign.Dependencies[s.Substring(1)] = new List<string>();
						}
					}
					else
					{
						if (!subDesign.Variables.ContainsKey(s))
						{
							subDesign.Variables.Add(s, 0);
							subDesign.Dependencies[s] = new List<string>();
						}
					}
				}
			}
			else
			{
				string dependent = lineOfCode.Substring(0, lineOfCode.IndexOf('='));
				currentDependent = dependent.Trim();
				if (!subDesign.Dependencies.ContainsKey(currentDependent))
				{
					subDesign.Dependencies.Add(currentDependent, new List<string>());
				}
				//Globals.dependencies[Globals.CurrentTab][dependent.Trim()] = new List<string>();
				string expression = lineOfCode.Substring(lineOfCode.IndexOf('=') + 1).Trim();
				if (!subDesign.Expressions.ContainsKey(currentDependent))
				{
					subDesign.Expressions.Add(currentDependent, expression);
				}
				//Globals.expressions[Globals.CurrentTab][dependent.Trim()] = expression;
				int x = SolveExpression(expression, lineNumber);
				if (!subDesign.Variables.ContainsKey(dependent.Trim()))
				{
					subDesign.Variables.Add(dependent.Trim(), x);
				}
				return expression;
			}
			return lineOfCode;
		}

		/// <summary>
		/// Solves the given expression
		/// </summary>
		/// <param name="expression">The expression to solve</param>
		/// <param name="lineNumber">The line number of the expression to solve</param>
		/// <returns>Returns the line number of the expression that is solved</returns>
		public int SolveExpression(string expression, int lineNumber)
		{
			int expFinal = -1;
			string operation = "";
			string[] tokens = expression.Split(' ');

			foreach (string s in tokens)
			{
				if (!s.Equals(string.Empty))
				{
					if (s[0].Equals('~'))
					{
						if (subDesign.Variables.ContainsKey(s.Substring(1)))
						{
							expFinal = Negate(subDesign.Variables[s.Substring(1)]);
							if (!subDesign.Dependencies[s.Substring(1)].Contains(currentDependent))
							{
								subDesign.Dependencies[s.Substring(1)].Add(currentDependent);
							}
						}
					}
					else if (subDesign.Variables.ContainsKey(s))
					{
						if (expFinal == -1)
						{
							expFinal = subDesign.Variables[s];
							if (!subDesign.Dependencies[s].Contains(currentDependent))
							{
								subDesign.Dependencies[s].Add(currentDependent);
							}
						}
						else
						{
							//if (String.IsNullOrEmpty(operation))
							//{
							//    operation = "*";
							//}
							if (String.IsNullOrEmpty(operation))
							{
								expFinal = expFinal * subDesign.Variables[s];
								if (!subDesign.Dependencies[s].Contains(currentDependent))
								{
									subDesign.Dependencies[s].Add(currentDependent);
								}
							}
							else if (operation.Equals("+"))
							{
								expFinal = expFinal + subDesign.Variables[s];
								if (!subDesign.Dependencies[s].Contains(currentDependent))
								{
									subDesign.Dependencies[s].Add(currentDependent);
								}
								if (expFinal == 2)
								{
									expFinal = 1;
								}
							}
						}
					}
					else
					{
						if (s.Equals("+"))
						{
							operation = s;
						}
						else
						{
							//Error
							//Console.WriteLine("Error {Line " + lineNumber + " | " + s + " is undefined }");
						}
					}
				}
			}
			return expFinal;
		}

		/// <summary>
		/// Negates the given value
		/// </summary>
		/// <param name="value">The value to negate</param>
		/// <returns>Returns the opposite of the given value</returns>
		public int Negate(int value)
		{
			if (value == 0)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Converts binary to decimal
		/// </summary>
		/// <param name="binary">The binary to convert to decimal</param>
		/// <returns>Returns the converted decimal</returns>
		public int BinaryToDecimal(string binary)
		{
			int dec = 0;

			for (int i = 0; i < binary.Length; i++)
			{
				if (binary[binary.Length - i - 1] == '0') continue;
				dec += (int)Math.Pow(2, i);
			}
			return dec;
		}
	}
}