using System;
using System.Collections.Generic;
using System.Text;
 
using System.Data;
using TM.OR.Sys;
using TM.DA.DBCommon;
 

namespace TM.DA.Sys
{
	public class TMDA
	{
		CommonDB db = new CommonDB();

		#region  查询
		public DataTable selectViewData()
		{
			string sql = @"select NO as [编号],
[Name] as [姓名],
[ISUse] as [是否使用]
from [TM] ";

			DataTable dt = null;
			try
			{
				dt = db.executeQuery(sql).Tables[0];
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return dt;
		}

		 
		#endregion

		public TMOR selectARowDate(string m_id)
		{
			string sql = string.Format("select * from [TM] where id={0}", m_id);
			DataTable dt = null;
			try
			{
				dt = db.executeQuery(sql).Tables[0];
			}
			catch (Exception ex)
			{
				throw ex;
			}
			if (dt == null)
				return null;
			if (dt.Rows.Count == 0)
				return null;
			DataRow dr = dt.Rows[0];
			TMOR mObj = new TMOR(dr);
			return mObj;

		}

		#region 插入
		/// <summary>
		/// 插入BuyClothes
		/// </summary>
		public bool Insert(TMOR mobj)
		{
			string sql = string.Format("insert into  [TM] (NO,Name,ISUse) values ('{0}','{1}','{2}')",
			   mobj.NO, mobj.Name, mobj.ISUse);

			return db.excuteNonquery(sql);
		}
		#endregion

		#region 修改
		public bool Update(TMOR mobj)
		{
			string sql = string.Format("update [TM] set NO='{0}',Name='{1}',ISUse='{2}' where id={3}",
			  mobj.NO, mobj.Name, mobj.ISUse, mobj.ID);

			return db.excuteNonquery(sql);
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除BuyClothes
		/// </summary>
		public virtual bool Delete(string strId)
		{
			string sql = string.Format("delete from [TM] where  ID ={0}", strId);
			return db.excuteNonquery(sql);
		}
		#endregion
	}
}
