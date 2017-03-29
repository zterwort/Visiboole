using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

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
                string txt = subDesign.Text;
                byte[] byteArr = Encoding.UTF8.GetBytes(txt);
                MemoryStream stream = new MemoryStream(byteArr);
                using (StreamReader reader = new StreamReader(stream))
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
				string expression = lineOfCode.Substring(lineOfCode.IndexOf('=') + 1).Trim();
				if (!subDesign.Expressions.ContainsKey(currentDependent))
				{
					subDesign.Expressions.Add(currentDependent, expression);
				}
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
            string fullExp = expression;
            string exp = "";
            string value = "";
            while(!GetInnerMostExpression(fullExp).Equals(fullExp))
            {
                exp = GetInnerMostExpression(fullExp);
                value = SolveBasicExpression(exp);
                exp = "(" + exp + ")";
                fullExp = fullExp.Replace(exp, value);
            }
            fullExp = SolveBasicExpression(fullExp);
            if(fullExp.Equals("TRUE"))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public string GetInnerMostExpression(string expression)
        {
            // this variable keeps track of the ('s in the expression.
            int innerStart;
            // this variable makes sure to keep the farthest inward  (  before hitting a  )  .
            int lastStart = 0;
            // this variable finds the index innermost  )  .
            int innerEnd = expression.IndexOf(')');
            // this will be the final expression if there are  ()  within the starting expression.
            string exp;
            // check to see if any  )'s  were found.
            if (innerEnd != -1)
            {
                // chop off the right side of the expression where the  )  starts.
                exp = expression.Substring(0, innerEnd);
                // chop off all  ('s  until there is only one left.
                do
                {
                    innerStart = exp.IndexOf('(');
                    // if there was a  (  found chop off the left side of expression where the  ( starts.
                    if (innerStart != -1)
                    {
                        lastStart = innerStart;
                        exp = exp.Substring(lastStart + 1);
                    }
                } while (innerStart != -1);
                // now return the inner most expression with no  ()'s  .
                return exp;
            }
            return expression;
        }

        public string SolveBasicExpression(string expression)
        {
            // set basicExpression variable
            string basicExpression = expression;

            ///
            /// look for [not] gates
            ///
            int notGate = basicExpression.IndexOf('~');

            // found a [not] gate
            while (notGate != -1)
            {
                // eleminating everything but the varible
                string oldVariable = basicExpression.Substring(notGate);
                if(!oldVariable.IndexOf(' ').Equals(-1))
                {
                    oldVariable = oldVariable.Substring(0, oldVariable.IndexOf(' '));
                }

                // get rid of the ~ so we can check for the variable in the dictionary
                string newVariable = oldVariable.Substring(1);

                // check to see variable is in subdesign
                if (subDesign.Variables.ContainsKey(newVariable))
                {
                    // applies [not] gate to the variable
                    if (Negate(subDesign.Variables[newVariable]) == 1)
                    {
                        // replace variable with TRUE
                        basicExpression = basicExpression.Replace(oldVariable, "TRUE");
                    }
                    else
                    {
                        // replace variable with FALSE
                        basicExpression = basicExpression.Replace(oldVariable, "FALSE");
                    }
                    // adds the current dependent variable to the dependencies of this variable
                    if (!subDesign.Dependencies[newVariable].Contains(currentDependent))
                    {
                        subDesign.Dependencies[newVariable].Add(currentDependent);
                    }
                }
                notGate = basicExpression.IndexOf('~');
            }

            ///
            /// look for [and] gates
            /// 

            // start by spliting the expression by [or] sign
            string[] andExpression = basicExpression.Split('+');

            // format the expression
            for(int i=0; i<andExpression.Length; i++)
            {
                andExpression[i] = andExpression[i].Trim();
            }

            // loop through each element
            foreach (string exp in andExpression)
            {
                // break element up to see if it has multiple variables
                string[] elements = exp.Split(' ');

                // make a new array to store int's instead of string's
                int[] inputs = new int[elements.Length];

                // loop through each element to get their boolean value
                for (int i=0; i<elements.Length; i++)
                {
                    // check for TRUE
                    if (elements[i].Equals("TRUE"))
                    {
                        inputs[i] = 1;
                    }
                    // check for FALSE
                    else if (elements[i].Equals("FALSE"))
                    {
                        inputs[i] = 0;
                    }
                    // check to see variable is in subdesign
                    if (subDesign.Variables.ContainsKey(elements[i]))
                    {
                        //set input
                        inputs[i] = subDesign.Variables[elements[i]];
                        // adds the current dependent variable to the dependencies of this variable
                        if (!subDesign.Dependencies[elements[i]].Contains(currentDependent))
                        {
                            subDesign.Dependencies[elements[i]].Add(currentDependent);
                        }
                    }
                }

                // applies [and] gate to each input/expression
                if (And(inputs) == 1)
                {
                    // replace variable with TRUE
                    basicExpression = basicExpression.Replace(exp, "TRUE");
                }
                else
                {
                    // replace variable with FALSE
                    basicExpression = basicExpression.Replace(exp, "FALSE");
                }
            }


            ///
            /// look for [or] gates
            ///
            string[] orExpression = basicExpression.Split('+');

            // format the expression
            for (int i = 0; i < orExpression.Length; i++)
            {
                orExpression[i] = orExpression[i].Trim();
            }

            // make a new array to store int's instead of string's
            int[] values = new int[orExpression.Length];

            // loop through each element of get their boolean value
            for (int i=0; i<orExpression.Length; i++)
            {
                // check for TRUE
                if (orExpression[i].Equals("TRUE"))
                {
                    values[i] = 1;
                }
                // check for FALSE
                else if (orExpression[i].Equals("FALSE"))
                {
                    values[i] = 0;
                }
                // it must be a variable
                else
                {
                    // check to see variable is in subdesign
                    if (subDesign.Variables.ContainsKey(orExpression[i]))
                    {
                        // get the boolean value of the variable
                        if (subDesign.Variables[orExpression[i]] == 1)
                        {
                            //set value
                            values[i] = 1;
                        }
                        else
                        {
                            //set value
                            values[i] = 0;
                        }
                    }
                    if (!subDesign.Dependencies[orExpression[i]].Contains(currentDependent))
                    {
                        subDesign.Dependencies[orExpression[i]].Add(currentDependent);
                    }
                }
            }

            ///
            /// now see what the final (potential) [or] gate is equal too and return "TRUE" or "FALSE"
            ///

            if(Or(values) == 1)
            {
                return "TRUE";
            }
            else
            {
                return "FALSE";
            }
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

        public int And(int[] values)
        {
            foreach(int value in values)
            {
                if(value == 0)
                {
                    return 0;
                }
            }
            return 1;
        }

        public int Or(int[] values)
        {
            foreach(int value in values)
            {
                if(value == 1)
                {
                    return 1;
                }
            }
            return 0;
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