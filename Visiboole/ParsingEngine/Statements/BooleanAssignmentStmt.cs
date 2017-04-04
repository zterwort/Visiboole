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

		public List<IObjectCodeElement> ParseSimpleExpression(ref string input)
		{						
			List<IObjectCodeElement> elems = new List<IObjectCodeElement>();
			Regex regex = new Regex(@"\w{1,20}");
			Match match = regex.Match(input);
			int beg = match.Index;
			string left = input.Substring(0, beg + 1);
			string right = input.Substring(beg + match.Length);
			// remove this expression from the containing string
			input = string.Concat(left, right);
			while (match.Success)
			{
				IndependentVariable iv = Database.TryGetVariable<IndependentVariable>(match.Value) as IndependentVariable;
				string mval = match.Value;
				if (iv == null)
				{
					// Declare the variable as 'false' if preceded by a tilda '~'
					iv = new IndependentVariable(mval.IndexOf('~') == 0 ? mval.Substring(1) : mval, mval.IndexOf('~') != 0);
					Database.AddVariable<IndependentVariable>(iv);
				}
				elems.Add(iv);
				match = match.NextMatch();
			}
			return elems;
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
