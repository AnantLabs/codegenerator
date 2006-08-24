using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
	public class ColumnNameExpression : Expression
	{
		public override void Interpret(Context context)
		{			
			Column column = (Column)Parameter;
			context.Output = Regex.Replace(context.Input,Context.StartDelimeter + "COLUMN.NAME" + Context.EndingDelimiter,column.Name);
			context.Input = context.Output;
		}
	}
}
