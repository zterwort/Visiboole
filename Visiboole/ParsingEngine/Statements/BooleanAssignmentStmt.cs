using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using VisiBoole.Models;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine.Statements
{
	public class BooleanAssignmentStmt : Statement
	{
		public static Regex Pattern { get; } = new Regex(@"^\w{1,20} <?= ~?(~?\(?\w{1,20}\)? ?\+? ?)+~?\w{1,20}\)?;$");

		public BooleanAssignmentStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

		public override void Parse()
		{			
			// var tokens = Text.Split(new char[] {'='}, StringSplitOptions.None);
			Regex regex = new Regex(@"<?=");
			List<string> tokens = regex.Split(Text).Select(x => x.Trim()).ToList();
			List<IndependentVariable> indVars = new List<IndependentVariable>();
			string inrTxt = GetInnermostParens(tokens.Last());

		}

		private string GetInnermostParens(string input)
		{
			// collect the contents of the outermost parens, non-inclusive
			Regex regex = new Regex(@"(?<=\().*(?=\))");
			Match match = regex.Match(input);
			if (match.Success)
			{
				return GetInnermostParens(match.Value);				
			}
			return input;
		}
	}

}
