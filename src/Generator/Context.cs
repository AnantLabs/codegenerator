namespace iCodeGenerator.Generator
{
	public class Context
	{
		private static string _startDelimeter = "{";
		private static string _endingDelimiter = "}";

		public static string StartDelimeter
		{
			get { return _startDelimeter; }
			set { _startDelimeter = value; }
		}

		public static string EndingDelimiter
		{
			get { return _endingDelimiter; }
			set { _endingDelimiter = value; }
		}

		private string _input;
		private string _output;

		public string Input
		{
			get { return _input; }
			set {_input = value; }
		}

		public string Output
		{
			get { return _output; }
			set { _output = value; }
		}

		private object _extra;

		internal object Extra
		{
			set { _extra = value; }
			get { return _extra; }
		}
		
		public Context()
		{
		}
	}
}
