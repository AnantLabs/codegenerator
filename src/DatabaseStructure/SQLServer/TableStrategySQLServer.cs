using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
	public class TableStrategySQLServer : TableStrategy
	{
		protected override DataSet TableSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
		{
			DataSet ds = new DataSet();
			IDbCommand sqlSp = dataAccessProvider.CreateCommand("sp_tables", connection);
			sqlSp.CommandType = CommandType.StoredProcedure;
			IDbDataParameter param = dataAccessProvider.CreateParameter();
			param.ParameterName = "@table_type";
			param.Value = "'TABLE'";
			sqlSp.Parameters.Add(param);
			IDbDataAdapter da = dataAccessProvider.CreateDataAdapter();
			da.SelectCommand = sqlSp;
			da.Fill(ds);
			return ds;
		}

		protected override DataSet ViewSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
		{
			DataSet ds = new DataSet();
			IDbCommand sqlSp = dataAccessProvider.CreateCommand("sp_tables", connection);
			sqlSp.CommandType = CommandType.StoredProcedure;
			IDbDataParameter param = dataAccessProvider.CreateParameter();
			param.ParameterName = "@table_type";
			param.Value = "'VIEW'";
			sqlSp.Parameters.Add(param);
			IDbDataAdapter da = dataAccessProvider.CreateDataAdapter();
			da.SelectCommand = sqlSp;
			da.Fill(ds);
			return ds;
		}

		protected override Table CreateTable(Database database, DataRow row)
		{
			Table table = new Table();
			table.ParentDatabase = database;
			table.Name = row["TABLE_NAME"].ToString();
			return table;
		}
	}
}