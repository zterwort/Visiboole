using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VisiBoole.Models;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine.Statements
{
    /// <summary>
    /// A list of visiboole independent variables that can be interacted with by the user
    /// </summary>
	public class VariableListStmt : Statement
	{
        /// <summary>
        /// The regex pattern that is used to identify a variable list statement
        /// </summary>
		public static Regex Pattern { get; } = new Regex(@"^((\*?\w{1,20}) ?)+;$");

        /// <summary>
        /// Constructs an instance of VariableListStmt
        /// </summary>
        /// <param name="lnNum">The line number that contains this statement</param>
        /// <param name="txt">The full text of this statement</param>
		public VariableListStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

        /// <summary>
        /// Parse this statement into object code
        /// </summary>
		public override void Parse()
		{
            // add each variable to our database
			string input = Text;
			Regex regex = new Regex(@"\*?\w{1,20}");
			Match match = regex.Match(input);

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
                if(indVar != null)
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
