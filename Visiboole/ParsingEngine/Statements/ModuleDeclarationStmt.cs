using System;
using System.Text.RegularExpressions;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine.Statements
{

    /// <summary>
    /// A module declaration statement gives the name of the module
    /// (component) and lists the names of its parameters, inputs, and outputs.It is optional for toplevel
    /// designs. If present, it must be the first statement in a design file.
    /// </summary>
    public class ModuleDeclarationStmt : Statement
	{
	    /// <summary>
	    /// The identifying pattern that can be used to identify and extract this statement from raw text
	    /// </summary>
        public static Regex Pattern { get; } = new Regex(@"^\w{1,20}\(.*:.*:.*\);$");

        /// <summary>
        /// Constructs an instance of ModuleDeclarationStmt at given linenumber with string text
        /// </summary>
        /// <param name="lnNum">The line number that this statement is located on within edit mode - not simulation mode</param>
        /// <param name="txt">The raw, unparsed text of this statement</param>
		public ModuleDeclarationStmt(int lnNum, string txt) : base(lnNum, txt)
		{			
		}

        public bool getMatch(string line)
        {
            string[] docs = System.IO.Directory.GetFiles("../../Data/");
            string all = "";
            foreach(string s in docs)
            {
                all += s;
            }
            return false;
        }

	    /// <summary>
	    /// Parses the Text of this statement into a list of discrete IObjectCodeElement elements
	    /// to be used by the html parser to generate formatted output to be displayed in simulation mode.
	    /// </summary>
        public override void Parse()
		{
            string fullExpression = Text;
            Operator stmt = new Operator(fullExpression);
            int index = fullExpression.IndexOf('(');
            string fileName = fullExpression.Substring(0, index);
            fullExpression = fullExpression.Substring(index+1);
            fullExpression = fullExpression.Substring(0, fullExpression.Length - 2);
            string[] parts = fullExpression.Split(':');
            string paramaters = parts[0];
            string inputs = parts[1];
            string outputs = parts[2];
            Output.Add(stmt);
            LineFeed lf = new LineFeed();
            Output.Add(lf);
		}

        private void MakeOrderedOutput(string fileName, string parameters, string inputs, string outputs)
        {
            Operator fn = new Operator(fileName);
            Operator open = new Operator("(");
            Operator p = new Operator(parameters);
            Operator colon = new Operator(":");
            Operator i = new Operator(inputs);
            Operator o = new Operator(outputs);
            Operator close = new Operator(")");
            Output.Add(fn);
            Output.Add(open);
            Output.Add(p);
            Output.Add(colon);
            Output.Add(i);
            Output.Add(colon);
            Output.Add(o);
            Output.Add(close);
            return;
        }

        

    }
}
