using System;
using System.Text.RegularExpressions;
using VisiBoole.Models;

namespace VisiBoole.ParsingEngine.Statements
{
	public class FormatSpecifierStmt : Statement
	{
		public static Regex Pattern1 { get; } = new Regex(@"^%[ubhd]{[a-zA-z0-9_]{1,20}\[\d\.\.\d\]};$");
		public static Regex Pattern2 { get; } = new Regex(@"^%[ubhd]{([a-zA-Z0-9_]{1,20} ?)+};$");

		public FormatSpecifierStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

		public override void Parse()
		{
			throw new NotImplementedException();
		}
	}
}
