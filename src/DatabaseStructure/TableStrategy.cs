using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
	public abstract class TableStrategy
	{
		protected internal TableCollection GetTables(Database database)
		{
			TableCollection tables = new TableCollection();
			DataAccessProviderFactory dataAccessProviderFactory = new DataAccessProviderFactory(Server.ProviderType);
			IDbConnection connection = dataAccessProviderFactory.CreateConnection(Server.ConnectionString);
			if(connection.State == ConnectionState.Closed)
			{
				connection.Open();	
			}			
			connection.ChangeDatabase(database.Name);
			DataSet ds = TableSchema(dataAccessProviderFactory, connection);
			connection.Close();

			/* Changed by Ferhat */
			if (ds.Tables.Count > 0)
			{
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					tables.Add(CreateTable(database, row));
				}
			}
			return tables;
		}
		
		/* Add by Ferhat */
		protected internal TableCollection GetViews(Database database)
		{
			TableCollection tables = new TableCollection();
			DataAccessProviderFactory dataAccessProviderFactory = new DataAccessProviderFactory(Server.ProviderType);
			IDbConnection connection = dataAccessProviderFactory.CreateConnection(Server.ConnectionString);
			if (connection.State == ConnectionState.Closed)
			{
				connection.Open();
			}
			connection.ChangeDatabase(database.Name);
			DataSet ds = ViewSchema(dataAccessProviderFactory, connection);
			connection.Close();
			if (ds.Tables.Count > 0)
			{
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					tables.Add(CreateTable(database, row));
				}
			}
			return tables;
		}

		/* Add by Ferhat */
		protected abstract DataSet ViewSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection);
		protected abstract DataSet TableSchema(DataAccessProviderFactory dataAccessProvider,IDbConnection connection);
		protected abstract Table CreateTable(Database database, DataRow row);
	}
}
