using System;
using System.Text.RegularExpressions;
using VisiBoole.Models;

namespace VisiBoole.ParsingEngine.Statements
{
	public class SubmoduleInstantiationStmt : Statement
	{
		public static Regex Pattern { get; } = new Regex(@"");

		public SubmoduleInstantiationStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

		public override void Parse()
		{
			throw new NotImplementedException();
		}
	}
}
