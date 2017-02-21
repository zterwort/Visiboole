using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParsingEngine.Chips
{
	/// <summary>
	/// A Chip represents a unique visiboole function and the syntax used
	/// to identify it, as well as the associated functions to solve it
	/// </summary>
	public interface IChip
	{
		/// <summary>
		/// This is the regular expression that will be used to identify
		/// and extract different visiboole functions (chips) from the 
		/// input fed to the parser
		/// </summary>
		Regex Identifier { get; set; }

		/// <summary>
		/// Simplifies the given expression
		/// </summary>
		/// <param name="expression">This is the expression that is fetched 
		/// by the parser using the identifier for this chip</param>
		/// <returns>Returns the simplified (computed) expression</returns>
		Expression Compute(Expression expression);
	}
}
