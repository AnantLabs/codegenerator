using System;
using System.Data;
using Arkus.ProspectManager.Configuration;
using Arkus.Shared.Data;

namespace {NAMESPACE}
{
	/// <summary>
	/// Summary description for {CLASS}DataAccess.
	/// </summary>
	public class {CLASS}DataAccess : IDataAccess
	{

		private static string _ConnectionString = Settings.Instance.ConnectionString;

		public {CLASS}DataAccess()
		{
		}

		public bool Create(DomainObject o)
		{
			if( !o.IsNew ) 
			{
				throw new {CLASS}Exception("Aready exist" + o.GetType().Name);
			}
			try
			{
				{CLASS} obj = ({CLASS}) o;
				obj.Id = Convert.ToInt32( DbAccess.ExecuteScalar(_ConnectionString, "{MODULE}_sp{TABLENAME}_Insert", {TABLE.COLUMNS NOPRIMARY}obj.{COLUMN.NAME}{IF NOT LAST}, {/IF}{/TABLE.COLUMNS} ) );
				return (obj.Id > 0);
			}
			catch(Exception ex)
			{
				throw new {CLASS}Exception("Unable to Create " + o.GetType().Name,ex);
			}
		}

		public bool Delete(DomainObject o)
		{
			if(o.IsNew) return false;

			try
			{
				return ( Convert.ToInt32( DbAccess.ExecuteNonQuery(_ConnectionString, "{MODULE}_sp{TABLENAME}_Delete", o.Id)) > 0);
			}
			catch(Exception ex)
			{
				throw new {CLASS}Exception("Unable to Delete " + o.GetType().Name,ex);
			}
		}

		public bool Update(DomainObject o)
		{
			if(o.IsNew) throw new {CLASS}Exception("Unable to Update New " + o.GetType().Name);
			try
			{
				{CLASS} obj = ({CLASS}) o;
				return ( Convert.ToInt32( DbAccess.ExecuteNonQuery(_ConnectionString,"{MODULE}_sp{TABLENAME}_Update",obj.Id, {TABLE.COLUMNS NOPRIMARY}obj.{COLUMN.NAME}{IF NOT LAST}, {/IF}{/TABLE.COLUMNS} )) > 0);
			}
			catch(Exception ex)
			{
				throw new {CLASS}Exception("Unable to Update " + o.GetType().Name,ex);
			}
		}

		public void Load(DomainObject o)
		{
			if( o.IsNew ) throw new {CLASS}Exception("Unable to Load New " + o.GetType().Name);
			try
			{
				DataRow row = DbAccess.ExecuteDataset(_ConnectionString, "{MODULE}_sp{TABLENAME}_Select", o.Id).Tables[0].Rows[0];				
				Populate(row,({CLASS}) o);
			}
			catch(Exception ex)
			{
				throw new {CLASS}Exception("Unable to Load " + o.GetType().Name,ex);
			}
		}

		public void LoadList(IList list)
		{
			if(list == null)
			{
				list = new ArrayList();
			}
			list.Clear();
			DataTable table = DbAccess.ExecuteDataset(_ConnectionString, "{MODULE}_sp{TABLENAME}_List").Tables[0];
			foreach(DataRow row in table.Rows)
			{
				{CLASS} c = new {CLASS}();
				Populate(row,c);
				list.Add(c);
			}
		}

		private void Populate(DataRow row, {CLASS} obj)
		{			
			{TABLE.COLUMNS PRIMARY}
			obj.Id = Convert.ToInt32( row["{COLUMN.NAME}"] );{/TABLE.COLUMNS}
			{TABLE.COLUMNS NOPRIMARY}{IF COLUMN.TYPE EQ 'int|bigint'}
			obj.{COLUMN.NAME} = Convert.ToInt32( row["{COLUMN.NAME}"] );{/IF}{IF COLUMN.TYPE EQ 'varchar'}
			obj.{COLUMN.NAME} = row["{COLUMN.NAME}"].ToString();{/IF}
			{/TABLE.COLUMNS}

		}
	}
}

