using VisiBoole.Models;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine.Statements
{
	public class EmptyStmt : Statement
	{
		public EmptyStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

		public override void Parse()
		{
			Output.Add(new LineFeed());
		}
	}
}
