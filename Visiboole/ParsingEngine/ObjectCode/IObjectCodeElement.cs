namespace VisiBoole.ParsingEngine.ObjectCode
{
	public interface IObjectCodeElement
	{
		string ObjCodeText { get; }
		bool? ObjCodeValue { get; }
	}
}
