using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiBoole
{
	/// <summary>
	/// Parse an input file into an output formatted for use by the HTML parser
	/// </summary>
	public class OutputParser
	{
		public string Input { get; set; }

		/// <summary>
		/// Constructs an instance of OutputParser
		/// </summary>
		/// <param name="input">The input file text containing user source code to be parsed</param>
		public OutputParser()
		{
			this.Input = string.Empty;
		}

		/// <summary>
		/// Generates a List<string> from InputFile contents to be used by the HTML parser
		/// </summary>
		/// <returns>Returns a list of strings on success; Returns null on failure</returns>
		public List<string> GenerateOutput()
		{
			try
			{
				return TrimOutputText(ConvertStringToList(Input));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

		/// <summary>
		/// Converts the given string to an array of strings, delimited by NewLine
		/// </summary>
		/// <param name="pText">The text to convert</param>
		/// <returns>Returns an array of strings, each line in its own box, if successful; Returns null if otherwise</returns>
		private List<string> ConvertStringToList(string pText)
		{
			if (string.IsNullOrEmpty(pText)) return null;

			string[] splitText = pText.Split(new String[] { "\n", Environment.NewLine }, StringSplitOptions.None);
			List<string> newText = new List<string>();

			// Add the first element if there is one
			if (splitText.Length > 0) newText.Add(splitText[0]);

			// Add the rest of the elements if they exist
			for (int i = 1; i < splitText.Count(); i++)
			{
				string prevLine = splitText[i - 1];
				string curLine = splitText[i];

				// If the last line is valid, add it. If it isn't then exit the loop
				if (i == splitText.Length && !string.IsNullOrEmpty(curLine))
				{
					newText.Add(curLine);
					break;
				} 
				
				// If both the current and previous lines are empty, do not add (we only want one NewLine between data)
				if (string.IsNullOrEmpty(curLine) && string.IsNullOrEmpty(prevLine))
				{
					continue;
				}
				else
				{
					newText.Add(curLine);
				}
			}

			return newText;
		}

		/// <summary>
		/// Removes the symbols from the given text that will not be read by the HTML parser
		/// </summary>
		/// <param name="pText">The text to strip the extraneous symbols from</param>
		/// <returns>Returns the given text, minus the extraneous symbols</returns>
		private List<string> TrimOutputText(List<string> pText)
		{
			for (int i = 0; i < pText.Count; i++)
			{
				pText[i] = pText[i].Replace("*", "");
				//pText[i] = pText[i].Replace("~", "");
				pText[i] = pText[i].Replace(";", "");

				// TODO: What to do with the format specifiers? (%{...};)
				// TODO: Any other characters that need removed?
			}

			return pText;
		}
	}
}
