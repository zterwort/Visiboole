using System;
using System.Text.RegularExpressions;
using VisiBoole.Models;

namespace VisiBoole.ParsingEngine.Statements
{
	public class ModuleDeclarationStmt : Statement
	{
		public static Regex Pattern { get; } = new Regex(@"^\w{1,20}\(.*:.*:.*\);$");

		public ModuleDeclarationStmt(int lnNum, string txt) : base(lnNum, txt)
		{			
		}

		public override void Parse()
		{
			throw new NotImplementedException();
		}
	}
}
