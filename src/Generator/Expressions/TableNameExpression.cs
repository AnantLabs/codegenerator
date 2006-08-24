using System;
using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
	public class TableNameExpression : Expression
	{
		
		public TableNameExpression()
		{

		}

		public override void Interpret(Context context)
		{
			Table table = (Table)Parameter;
			context.Output = Regex.Replace(context.Input,Context.StartDelimeter + "TABLE.NAME" + Context.EndingDelimiter,table.Name);
			context.Input = context.Output;
		}
	}
}
