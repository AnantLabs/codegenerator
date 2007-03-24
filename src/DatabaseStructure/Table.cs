using System.ComponentModel;
using iCodeGenerator.GenericDataAccess;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.DatabaseStructure
{
	public class Table
	{
		private string _name;
		private Database _parentDatabase;
		private ColumnStrategy _strategy;
		private bool _reload;
		private ColumnCollection _columns;
		private KeyCollection _keys;

		public void Reload()
		{
			_reload = true;			
		}

		public Table()
		{
			if(Server.ProviderType == DataProviderType.SqlClient)
			{
				_strategy = new ColumnStrategySQLServer();
			}
			else if(Server.ProviderType == DataProviderType.MySql)
			{
				_strategy = new ColumnStrategyMySQL();
			}
			else if(Server.ProviderType == DataProviderType.PostgresSql)
			{
				_strategy = new ColumnStrategyPostgres();
			}
			else if(Server.ProviderType == DataProviderType.Oracle)
			{
				_strategy = new ColumnStrategyOracle();
			}
		}

		[CategoryAttribute("Table"),
		ReadOnlyAttribute(true)]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		[BrowsableAttribute(false),
		DefaultValueAttribute(false)]
		public Database ParentDatabase
		{
			get { return _parentDatabase; }
			set { _parentDatabase = value; }
		}

		[BrowsableAttribute(false),
		DefaultValueAttribute(false)]
		public ColumnCollection Columns
		{
			get
			{
				if(_reload || _columns == null)
				{					
					if(_columns != null) _columns.Clear();
					_columns = _strategy.GetColumns(this);
				}
				return _columns;
			}
		}

		[BrowsableAttribute(false),
		DefaultValueAttribute(false)]
		public KeyCollection Keys
		{
			get
			{
				if(_reload || _keys == null)
				{				
					if(_keys != null) _keys.Clear();
					_keys = _strategy.GetKeys(this);
				}
				return _keys;
			}
		}				
	}
}
