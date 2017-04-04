using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiBoole.ParsingEngine.ObjectCode
{
	public class LineFeed : IObjectCodeElement
	{
		public string ObjCodeText { get { return Environment.NewLine; } }
		public bool? ObjCodeValue { get { return null; } }
	}
}
