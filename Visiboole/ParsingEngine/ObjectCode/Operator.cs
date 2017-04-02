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
		public bool? Value { get; set; }

		public string ElemToString()
		{
			throw new NotImplementedException();
		}
	}
}
