using {NAMESPACE}.DataAccess;

namespace {NAMESPACE}
{
	/// <summary>
	/// Summary description for {TABLE.NAME}.
	/// </summary>
	public class {TABLE.NAME} : DomainObject
	{
		#region Attributes
		private static IDataAccess _DataAccess = new {TABLE.NAME}DataAccess();
		{TABLE.COLUMNS NOPRIMARY}
		private {MAP COLUMN.TYPE} _{COLUMN.NAME};	{/TABLE.COLUMNS}
		#endregion

		#region Properties
		{TABLE.COLUMNS NOPRIMARY}
		public {MAP COLUMN.TYPE} {COLUMN.NAME}
		{
			get{ return _{COLUMN.NAME}; }
			set{ _{COLUMN.NAME} = value; }
		}		
		{/TABLE.COLUMNS}
		#endregion

		#region Constructor

		public {TABLE.NAME}()
		{
		}

		public {TABLE.NAME}(int id)
		{
			Load(id);
		}		

		#endregion

		#region Methods

		public bool Create()
		{
			return _DataAccess.Create(this);	
		}

		public bool Delete()
		{
			return _DataAccess.Delete(this);
		}

		public bool Update()
		{
			return _DataAccess.Update(this);
		}
	
		private void Load(int id)
		{
			_Id = id;
			_DataAccess.Load(this);
		}

		#endregion
	}
}

