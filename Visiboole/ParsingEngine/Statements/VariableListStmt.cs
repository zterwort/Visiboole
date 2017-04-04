using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VisiBoole.Models;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine.Statements
{
	public class VariableListStmt : Statement
	{
		public static Regex Pattern { get; } = new Regex(@"^((\*?\w{1,20}) ?)+;$");

		public VariableListStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

		public override void Parse()
		{
			string input = Text;
			Regex regex = new Regex(@"\*?\w{1,20}");
			Match match = regex.Match(input);
			while (match.Success)
			{
				IndependentVariable iv = Database.TryGetVariable<IndependentVariable>(match.Value) as IndependentVariable;
				string mval = match.Value;
				if (iv == null)
				{
					// Declare the variable as 'true' if preceded by an asterisk '*'
					iv = new IndependentVariable(mval.IndexOf('*') == 0 ? mval.Substring(1) : mval, mval.IndexOf('*') == 0);
					Database.AddVariable<IndependentVariable>(iv);
				}
				Output.Add(iv);
				match = match.NextMatch();
			}
		}
	}
}
