using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisiBoole.ParsingEngine;

namespace VisiBoole.ParsingEngine.ObjectCode
{
	public class Operator : IObjectCodeElement
	{
		public string ObjCodeText { get { return OperatorChar; } }
		public bool? ObjCodeValue { get { return null; } }
		public string OperatorChar { get; set; }

		public Operator(string opChar)
		{
			OperatorChar = opChar;
		}
	}
}
