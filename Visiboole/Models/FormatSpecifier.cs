using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiBoole.Models
{
	public class FormatSpecifier
	{
		/// <summary>
		/// The line number where this format specifier is located
		/// </summary>
		private int _lineNumber;

		/// <summary>
		/// The list of variables that this format specifier is composed of
		/// </summary>
		private List<int> _vals;

		/// <summary>
		/// The format that this calculated variable list should return
		/// </summary>
		private string _format;

		/// <summary>
		/// Constructs an instance of FormatSpecifier
		/// </summary>
		/// <param name="lineNumber">The line number where this format specifier is located</param>
		/// <param name="vals">The list of variables that this format specifier is composed of</param>
		/// <param name="format">The format that this calculated variable list should return</param>
		public FormatSpecifier(int lineNumber, string format, List<int> vals)
		{
			_lineNumber = lineNumber;
			_vals = vals;
			_format = format;
		}

		/// <summary>
		/// Calculates the varList field and returns a string in binary, hex, signed or unsigned decimal
		/// </summary>
		/// <returns>Returns a string in binary, hex, signed or unsigned decimal</returns>
		public string Calculate()
		{
			switch (_format.ToUpper())
			{
				case "B":
					break;
				case "D":
					break;
				case "U":
					break;
				default:
					break;
			}

			throw new NotImplementedException();
		}

		private string ToBinary()
		{
			string binary = "";
			foreach (var variable in _vals)
			{
				binary += variable.ToString();
			}
			return binary;
			//string[] arr = _vals.ToArray().Select(c => c.ToString()).ToArray();
			//string.Join()

		}

		public string ToUnsigned(string binary) // decimal
		{
			int dec = 0;

			for (int i = 0; i < binary.Length; i++)
			{
				if (binary[binary.Length - i - 1] == '0') continue;
				dec += (int)Math.Pow(2, i);
			}
			return dec.ToString();
		}

		private string ToHex()
		{
			string binary = ToBinary();
			return Convert.ToInt32(binary, 2).ToString("X");
		}

		private string ToSigned(string input)
		{
			throw new NotImplementedException();
		}
	}
}
