namespace VisiBoole.ParsingEngine.ObjectCode
{
    /// <summary>
    /// The base class for independent and dependent variables
    /// </summary>
	public abstract class Variable
	{
        /// <summary>
        /// The string representation of this variable
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// The boolean valuable of this variable
        /// </summary>
		public bool Value { get; set; }
	}
}
