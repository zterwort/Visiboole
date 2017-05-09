using System.Text.RegularExpressions;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine.Statements
{
    /// <summary>
    /// Comment statements provide a way to document either just the code
    /// or to label the screen during simulation.
    /// </summary>
	public class CommentStmt : Statement, IObjectCodeElement
	{
	    /// <summary>
	    /// The identifying pattern that can be used to identify and extract this statement from raw text
	    /// </summary>
        public static Regex Pattern { get; } = new Regex(@"^"".*""$");

        /// <summary>
        /// Constructs an instance of CommentStmt
        /// </summary>
        /// <param name="lnNum">The line number that this statement is located on within edit mode - not simulation mode</param>
        /// <param name="txt">The raw, unparsed text of this statement</param>
        public CommentStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

	    /// <summary>
	    /// Parses the Text of this statement into a list of discrete IObjectCodeElement elements
	    /// to be used by the html parser to generate formatted output to be displayed in simulation mode.
	    /// </summary>
        public override void Parse()
		{
			// only add comments to simulator if the user has the setting enabled
			if (Properties.Settings.Default.SimulationComments)
			{
				Output.Add(this);
			}
            LineFeed lf = new LineFeed();
            Output.Add(lf);
		}

		#region IObjectCodeElement attributes

		public bool? ObjCodeValue { get; set; } = false;
		public string ObjCodeText { get { return Text; } set { } } 

		#endregion

	}
}
