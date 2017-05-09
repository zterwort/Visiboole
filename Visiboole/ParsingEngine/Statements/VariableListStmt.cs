using System.Text.RegularExpressions;
using VisiBoole.ParsingEngine.ObjectCode;
using System;
using System.Collections.Generic;

namespace VisiBoole.ParsingEngine.Statements
{
    /// <summary>
    /// A list of visiboole independent variables that can be interacted with by the user
    /// </summary>
	public class VariableListStmt : Statement
	{
        /// <summary>
        /// The identifying pattern that can be used to identify and extract this statement from raw text
        /// </summary>
        //public static Regex Pattern { get; } = new Regex(@"^((\*?\w{1,20}) ?)$");
        public static Regex Pattern { get; } = new Regex(@"^\0*?(\w ?)*;$");
        public static Regex Pattern2 { get; } = new Regex(@"[a-zA-Z0-9_]+\[\d+\.\.\d\]", RegexOptions.None);
        //string match = regex.Match(content).Value;

        /// <summary>
        /// Constructs an instance of VariableListStmt
        /// </summary>
        /// <param name="lnNum">The line number that this statement is located on within edit mode - not simulation mode</param>
        /// <param name="txt">The raw, unparsed text of this statement</param>
		public VariableListStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

	    /// <summary>
	    /// Parses the Text of this statement into a list of discrete IObjectCodeElement elements
	    /// to be used by the html parser to generate formatted output to be displayed in simulation mode.
	    /// </summary>
        public override void Parse()
		{
            // add each variable to our database
			string input = Text;
			Regex regex = new Regex(@"\*?\w{1,20}");
			Match match = regex.Match(input);

            Regex regex2 = new Regex(@"[a-zA-Z0-9_]+\[\d+\.\.\d\]", RegexOptions.None);
            Match match2 = regex.Match(input);

            if(match2.Success && input.Contains("[") && input.Contains("]"))
            {
                List<int> valueList = new List<int>();
                Regex full = new Regex(@"[a-zA-Z0-9_]+\[\d+\.\.\d\]", RegexOptions.None);
                string fullMatch = full.Match(input).Value;
                regex = new Regex(@"[a-zA-Z0-9_]+", RegexOptions.None);
                string value = regex.Match(input).Value;
                regex = new Regex(@"\d");
                MatchCollection matches = regex.Matches(fullMatch);
                int beg = Convert.ToInt32(matches[0].Value);
                int end = Convert.ToInt32(matches[1].Value);

                List<int> order = new List<int>();

                if (beg < end)
                {
                    for (int i = beg; i <= end; i++)
                    {
                        order.Add(i);
                    }
                }
                else // beg > end
                {
                    for (int i = beg; i >= end; i--)
                    {
                        order.Add(i);
                    }
                }

                // add each variable to our output list of object code
                foreach (int i in order)
                {
                    string key = string.Concat(value, i);
                    IndependentVariable indVar = Database.TryGetVariable<IndependentVariable>(key) as IndependentVariable;
                    DependentVariable depVar = Database.TryGetVariable<DependentVariable>(key) as DependentVariable;
                    if (indVar != null)
                    {
                        if (indVar.Value)
                        {
                            valueList.Add(1);
                        }
                        else
                        {
                            valueList.Add(0);
                        }
                        Output.Add(indVar);
                    }
                    else if (depVar != null)
                    {
                        if (depVar.Value)
                        {
                            valueList.Add(1);
                        }
                        else
                        {
                            valueList.Add(0);
                        }
                        Output.Add(depVar);
                    }
                    else
                    {
                        IndependentVariable newVar = new IndependentVariable(key, false);
                        Database.AddVariable<IndependentVariable>(newVar);
                        valueList.Add(0);
                        Output.Add(newVar);
                    }
                }
                LineFeed lf = new LineFeed();
                Output.Add(lf);
            }
            else if (match.Success)
            {
                //used to specify variables name and value
                string variableName;
                bool variableValue;

                while (match.Success)
                {
                    if (match.Value.Contains("*"))
                    {
                        variableName = match.Value.Substring(1);
                        variableValue = true;
                    }
                    else
                    {
                        variableName = match.Value;
                        variableValue = false;
                    }
                    IndependentVariable indVar = Database.TryGetVariable<IndependentVariable>(variableName) as IndependentVariable;
                    if (indVar != null)
                    {
                        Output.Add(indVar);
                    }
                    else
                    {
                        indVar = new IndependentVariable(variableName, variableValue);
                        Database.AddVariable<IndependentVariable>(indVar);
                        Output.Add(indVar);
                    }
                    match = match.NextMatch();
                }
                LineFeed lf = new LineFeed();
                Output.Add(lf);
            }
            else
            {

            }

            /*while (match.Success)
			{
				IndependentVariable iv = Database.TryGetVariable<IndependentVariable>(match.Value) as IndependentVariable;
				string mval = match.Value;
				if (iv == null)
				{
					// Declare the variable as 'true' if preceded by an asterisk '*'
					iv = new IndependentVariable(mval.IndexOf('*') == 0 ? mval.Substring(1) : mval, mval.IndexOf('*') == 0);
					Database.AddVariable<IndependentVariable>(iv);
				}
                // add each discrete unit to our list of object code output
				Output.Add(iv);
				match = match.NextMatch();
			}
            LineFeed lf = new LineFeed();
            Output.Add(lf);*/
		}
	}
}
