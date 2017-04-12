using System;
using System.Text.RegularExpressions;

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

	    /// <summary>
	    /// Parses the Text of this statement into a list of discrete IObjectCodeElement elements
	    /// to be used by the html parser to generate formatted output to be displayed in simulation mode.
	    /// </summary>
        public override void Parse()
		{
			throw new NotImplementedException();
		}
	}
}
