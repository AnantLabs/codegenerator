using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
	public class TableStrategyMySQL : TableStrategy
	{		
		protected override DataSet TableSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
		{
			DataSet ds = new DataSet();
			IDbCommand sqlString = dataAccessProvider.CreateCommand("show tables", connection);
			sqlString.CommandType = CommandType.Text;
			IDbDataAdapter da = dataAccessProvider.CreateDataAdapter();
			da.SelectCommand = sqlString;
			da.Fill(ds);
			return ds;
		}

		/* Add by Ferhat */
		protected override DataSet ViewSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
		{
			DataSet ds = new DataSet();
			return ds;
		}
		protected override Table CreateTable(Database database, DataRow row)
		{
			Table table = new Table();
			table.ParentDatabase = database;
			table.Name = row[0].ToString();
			return table;
		}
	}
}