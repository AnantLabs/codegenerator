using System;
using System.Data;
using iCodeGenerator.GenericDataAccess;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.DatabaseStructure
{
	public class TableStrategyOracle : TableStrategy
	{
		protected override DataSet TableSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
		{
			return new DataSet();
		}

		protected override DataSet TableSchema(DataAccessProviderFactory dataProvider, IDbConnection connection, Database database)
		{
			DataSet ds = new DataSet();
			
			IDbCommand sqlString = dataProvider.CreateCommand("SELECT OWNER, TABLE_NAME FROM all_tables where OWNER = '" + database.Name + "'",connection);
			sqlString.CommandType = CommandType.Text;
			IDbDataAdapter da = dataProvider.CreateDataAdapter();
			da.SelectCommand = sqlString;
			da.Fill(ds);
			
			return ds;
		}

		protected override Table CreateTable(Database database, DataRow row)
		{
			Table table = new Table();
			
			table.ParentDatabase = database;
			table.Name = row["table_name"].ToString();

			return table;
		}

		protected override DataSet ViewSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
		{
			return new DataSet();
		}
	}
}
