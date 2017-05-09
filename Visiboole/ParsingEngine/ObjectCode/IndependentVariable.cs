namespace VisiBoole.ParsingEngine.ObjectCode
{
    /// <summary>
    /// A variable whose value does not depend on that of any other; e.g to the right of the "=" sign
    /// </summary>
    public class IndependentVariable : Variable, IObjectCodeElement
	{

	    /// <summary>
	    /// The boolean value of this variable to be added to the statement's Output
	    /// </summary>
	    public bool? ObjCodeValue { get { return Value; } set { } }

	    /// <summary>
	    /// The string representation of this variable to be added to the statement's Output
	    /// </summary>
	    public string ObjCodeText { get { return Name; } set { } }

        public int Match { get; set; }
        public int MatchingIndex { get; set; }

        /// <summary>
        /// Constructs an instance of IndependentVariable with given name and value
        /// </summary>
        /// <param name="name">The string name of this variable</param>
        /// <param name="value">The boolean value of this variable</param>
        public IndependentVariable(string name, bool value)
		{
			Name = name;
			Value = value;
		}
	}
}
