namespace VisiBoole.ParsingEngine.ObjectCode
{
    /// <summary>
    /// A discrete element of output representing a mathematical operator
    /// </summary>
	public class Operator : IObjectCodeElement
	{
        /// <summary>
        /// The string representation of this output element
        /// </summary>
		public string ObjCodeText { get { return OperatorChar; } }

        /// <summary>
        /// The boolean value of this output element, null
        /// </summary>
		public bool? ObjCodeValue { get { return null; } }

        /// <summary>
        /// The string representation of this element
        /// </summary>
		public string OperatorChar { get; set; }

        /// <summary>
        /// Constructs an instance of Operator 
        /// </summary>
        /// <param name="opChar">The string representation of this element</param>
		public Operator(string opChar)
		{
			OperatorChar = opChar;
		}
	}
}
