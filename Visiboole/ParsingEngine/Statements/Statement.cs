using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;
using VisiBoole.ParsingEngine;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine.Statements
{
	public abstract class Statement
	{
		public int LineNumber { get; set; }
		public string Text { get; set; }
		public List<IObjectCodeElement> Output { get; set; }

		protected Statement(int lnNum, string txt)
		{
			LineNumber = lnNum;
			Text = txt;
		}

		public abstract void Parse();
	}
}
