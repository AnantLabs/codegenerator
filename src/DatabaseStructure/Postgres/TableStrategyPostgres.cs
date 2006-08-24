using System.Data;
using iCodeGenerator.GenericDataAccess;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.DatabaseStructure
{
	public class TableStrategyPostgres : TableStrategy
	{
		protected override DataSet TableSchema(DataAccessProviderFactory dataProvider, IDbConnection connection)
		{
			DataSet ds = new DataSet();
			IDbCommand sqlString = dataProvider.CreateCommand("SELECT tablename FROM pg_tables WHERE schemaname = 'public'",connection);
			sqlString.CommandType = CommandType.Text;
			IDbDataAdapter da = dataProvider.CreateDataAdapter();
			da.SelectCommand = sqlString;
			da.Fill(ds);
			return ds;
		}

		/* Add by Ferhat */
		protected override DataSet ViewSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
		{
			throw new System.Exception("The method or operation is not implemented.");
		}
		protected override Table CreateTable(Database database, DataRow row)
		{
			Table table = new Table();
			table.ParentDatabase = database;
			table.Name = row["tablename"].ToString();
			return table;
		}
	}
}
