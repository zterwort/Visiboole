namespace VisiBoole.ParsingEngine.ObjectCode
{
	public interface IObjectCodeElement
	{
		bool? Value { get; set; }
		string ElemToString();
	}
}
