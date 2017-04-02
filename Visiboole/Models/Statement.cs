using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiBoole.Models
{
	public abstract class Statement
	{
		public int LineNumber { get; set; }
		public string Text { get; set; }

		protected Statement(int lnNum, string txt)
		{
			LineNumber = lnNum;
			Text = txt;
		}
	}
}
