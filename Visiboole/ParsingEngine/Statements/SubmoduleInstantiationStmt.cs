using System;
using System.Text.RegularExpressions;

namespace VisiBoole.ParsingEngine.Statements
{
    /// <summary>
    /// The format of a submodule instantiation statement is identical to the module declaration statement except they
    /// are preceded by the commercial at(@) and give a numeric value to parameters. They create an
    /// instance of a module described in another file(with the same name) as part of the current design
    /// </summary>
	public class SubmoduleInstantiationStmt : Statement
	{
	    /// <summary>
	    /// The identifying pattern that can be used to identify and extract this statement from raw text
	    /// </summary>
        public static Regex Pattern { get; } = new Regex(@"^@\w{1,20}\(.*:.*:.*\);$");

        /// <summary>
        /// Constructs an instance of SubmoduleInstantiationStmt at given linenumber with txt string input
        /// </summary>
        /// <param name="lnNum">The line number that this statement is located on within edit mode - not simulation mode</param>
        /// <param name="txt">The raw, unparsed text of this statement</param>
		public SubmoduleInstantiationStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

	    /// <summary>
	    /// Parses the Text of this statement into a list of discrete IObjectCodeElement elements
	    /// to be used by the html parser to generate formatted output to be displayed in simulation mode.
	    /// </summary>
        public override void Parse()
		{
            //parse line to find the filename, parameters, inputs and outputs

            //use filename to get submodule code and read it into the program

            //now once this is done inject that code into the spot where the submodule is called(right under the submodule)

            //now keep parsing with the newly included submodule

			throw new NotImplementedException();
		}
	}
}
