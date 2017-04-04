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
				IndependentVariable val = Database.TryGetVariable<IndependentVariable>(match.Value) as IndependentVariable;
				if (val == null)
				{
					// Declare the variable as 'true' if preceded by an asterisk '*'
					val = new IndependentVariable(match.Value, match.Value.IndexOf('*') == 0);
					Database.AddVariable<IndependentVariable>(val);					
				}
				Database.AddObjectCodeElement(val);
				var x = Database.ObjectCode;
				match = match.NextMatch();
			}
		}
	}
}
