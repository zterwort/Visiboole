namespace VisiBoole.ParsingEngine.ObjectCode
{
    /// <summary>
    /// A variable that is assigned an expression; e.g. to the left of the "=" sign
    /// </summary>
	public class DependentVariable : Variable, IObjectCodeElement
	{
        /// <summary>
        /// The boolean value of this variable to be added to the statement's Output
        /// </summary>
		public bool? ObjCodeValue { get { return Value; } }

        /// <summary>
        /// The string representation of this variable to be added to the statement's Output
        /// </summary>
		public string ObjCodeText { get { return Name; } }

        /// <summary>
        /// Constructs an instance of DependentVariable with name and value
        /// </summary>
        /// <param name="name">The string name of this variable</param>
        /// <param name="value">The boolean value of this variable</param>
		public DependentVariable(string name, bool value)
		{
			Name = name;
			Value = value;
		}
	}
}
