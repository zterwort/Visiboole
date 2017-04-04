using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiBoole.ParsingEngine.ObjectCode
{
	public class DependentVariable : Variable, IObjectCodeElement
	{
		public bool? ObjCodeValue { get { return Value; } }
		public string ObjCodeText { get { return Name; } }

		public DependentVariable(string name, bool value)
		{
			Name = name;
			Value = value;
		}

	}
}
