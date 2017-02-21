using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingEngine
{
	/// <summary>
	/// An Expression represents a single line of instructions (code)
	/// within a visiboole source code, delimited by a ";" and contains a
	/// dependent variable along with its associated stack of SubExpressions.
	/// </summary>
	public class Expression
	{
		/// <summary>
		/// True if this Expression and all of its SubExpressions have been
		/// completely simplified
		/// </summary>
		public bool IsSimplified { get; set; }

		/// <summary>
		/// A stack of SubExpressions which compose this Expression
		/// </summary>
		public Stack<SubExpression> SubExpressions { get; set; }
	}
}
