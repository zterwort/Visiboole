using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiBoole.Models
{
	public class EmptyStmt : Statement
	{
		public EmptyStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}
	}
}
