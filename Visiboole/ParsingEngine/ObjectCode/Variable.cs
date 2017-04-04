using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisiBoole.ParsingEngine;

namespace VisiBoole.ParsingEngine.ObjectCode
{
	public abstract class Variable
	{
		public string Name { get; set; }
		public bool Value { get; set; }
	}
}
