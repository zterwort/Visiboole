using System.Text.RegularExpressions;
using VisiBoole.Models;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine.Statements
{
	public class CommentStmt : Statement, IObjectCodeElement
	{
		public static Regex Pattern { get; } = new Regex(@"^//");

		public CommentStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

		public override void Parse()
		{
			// only add comments to simulator if the user has the setting enabled
			if (Properties.Settings.Default.SimulationComments)
			{
				Output.Add(this);
			}
		}

		#region IObjectCodeElement attributes

		public bool? Value { get; set; } = false;

		public string ElemToString()
		{
			return Text.Trim().Substring(2);
		}

		#endregion

	}
}
