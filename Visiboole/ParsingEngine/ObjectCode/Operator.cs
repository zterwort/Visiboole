namespace VisiBoole.ParsingEngine.ObjectCode
{
    /// <summary>
    /// A discrete element of output representing a mathematical operator
    /// </summary>
	public class Operator : IObjectCodeElement
	{
        private bool? Value = false;
        /// <summary>
        /// The string representation of this output element
        /// </summary>
		public string ObjCodeText { get { return OperatorChar; } }

        /// <summary>
        /// The boolean value of this output element, null
        /// </summary>
		public bool? ObjCodeValue { get { return Value; } set { this.Value = value; } }

        /// <summary>
        /// The string representation of this element
        /// </summary>
		public string OperatorChar { get; set; }

        public int Match { get; set; }
        public int MatchingIndex { get; set; }

        /// <summary>
        /// Constructs an instance of Operator 
        /// </summary>
        /// <param name="opChar">The string representation of this element</param>
		public Operator(string opChar)
		{
			OperatorChar = opChar;
            this.Value = null;
		}
	}
}
