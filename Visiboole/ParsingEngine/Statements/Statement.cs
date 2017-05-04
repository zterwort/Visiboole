using System.Collections.Generic;
using VisiBoole.ParsingEngine.Boolean;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine.Statements
{
    /// <summary>
    /// The base class for Visiboole statements. Visiboole statements represent the various
    /// different expressions that one can encounter within Visiboole HDL syntax.
    /// </summary>
	public abstract class Statement
	{
        /// <summary>
        /// The line number that this statement is located on within edit mode - not simulation mode
        /// </summary>
		public int LineNumber { get; set; }

        /// <summary>
        /// The raw, unparsed text of this statement
        /// </summary>
		public string Text { get; set; }

        /// <summary>
        /// A list of discrete output elements that comprise this statement
        /// </summary>
		public List<IObjectCodeElement> Output { get; set; } = new List<IObjectCodeElement>();

        /// <summary>
        /// Constructs an instance of this Statement with given line number and text representation
        /// </summary>
        /// <param name="lnNum">The line number that this statement is located on within edit mode - not simulation mode</param>
        /// <param name="txt">The raw, unparsed text of this statement</param>
		protected Statement(int lnNum, string txt)
		{
			LineNumber = lnNum;
			Text = txt;
		}

        /// <summary>
        /// Parses the Text of this statement into a list of discrete IObjectCodeElement elements
        /// to be used by the html parser to generate formatted output to be displayed in simulation mode.
        /// </summary>
		public abstract void Parse();
	}
}
