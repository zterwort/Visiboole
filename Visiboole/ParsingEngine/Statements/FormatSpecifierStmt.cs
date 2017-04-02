using System;
using System.Text.RegularExpressions;
using VisiBoole.Models;

namespace VisiBoole.ParsingEngine.Statements
{
	public class FormatSpecifierStmt : Statement
	{
		public static Regex Pattern1 { get; } = new Regex(@"");
		public static Regex Pattern2 { get; } = new Regex(@"");

		public FormatSpecifierStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

		public override void Parse()
		{
			throw new NotImplementedException();
		}
	}
}
