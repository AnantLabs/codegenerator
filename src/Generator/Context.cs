namespace iCodeGenerator.Generator
{
	public class Context
	{
		private static string _StartDelimeter = "{";
		private static string _EndingDelimiter = "}";

		public static string StartDelimeter
		{
			get { return _StartDelimeter; }
			set { _StartDelimeter = value; }
		}

		public static string EndingDelimiter
		{
			get { return _EndingDelimiter; }
			set { _EndingDelimiter = value; }
		}

	    public string Input { get; set; }

	    public string Output { get; set; }

	    internal object Extra { get; set; }
	}
}
