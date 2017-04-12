namespace VisiBoole.ParsingEngine.ObjectCode
{
    /// <summary>
    /// A discrete element of output consumed by the html parser
    /// </summary>
	public interface IObjectCodeElement
	{
        /// <summary>
        /// The text representation printed in simulation mode output
        /// </summary>
		string ObjCodeText { get; }

        /// <summary>
        /// The boolean value of this element
        /// </summary>
		bool? ObjCodeValue { get; }
	}
}
