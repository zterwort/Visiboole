using System;

namespace VisiBoole.ParsingEngine.ObjectCode
{
    /// <summary>
    /// A discrete element of output representing a linefeed
    /// </summary>
	public class LineFeed : IObjectCodeElement
	{
        /// <summary>
        /// The text representation of this outpute element, a newline character
        /// </summary>
		public string ObjCodeText { get { return Environment.NewLine; } }

        /// <summary>
        /// The value of this element is null as it is a newline character, not a variable
        /// </summary>
		public bool? ObjCodeValue { get { return null; } }
	}
}
