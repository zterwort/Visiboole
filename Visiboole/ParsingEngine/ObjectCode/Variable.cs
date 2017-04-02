using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisiBoole.ParsingEngine;

namespace VisiBoole.ParsingEngine.ObjectCode
{
	public abstract class Variable : IObjectCodeElement
	{
		public string Name { get; set; }
		public bool? Value { get; set; }

		bool? IObjectCodeElement.Value
		{
			get { return Value; }
			set { Value = value; }
		}

		public string ElemToString()
		{
			throw new NotImplementedException();
		}
	}
}
