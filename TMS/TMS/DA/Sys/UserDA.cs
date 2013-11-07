using System;
using System.Collections.Generic;
using System.Text; 
using System.Data;
using TM.DA.DBCommon;
using TM.OR.Sys;
 

namespace TM.DA.Sys
{
    public class UserDA
    {

        CommonDB db = new CommonDB();

        #region  查询
        public DataTable selectViewData()
        {
            string sql = @"select ID as [编号],
[UserName] as [姓名],
[UserCode] as [帐号]
from [User] ";
             
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

        public UserOR Login(string userCode, string pwd)
        {
            string sql = string.Format("select * from [User] where UserCode='{0}' and PWD='{1}'", userCode, pwd);
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
            UserOR mObj = new UserOR(dr);
            return mObj;
        }

        #endregion

		public UserOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from [User] where id={0}", m_id);
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
            UserOR mObj = new UserOR(dr);
            return mObj;

        }

        #region 插入
        /// <summary>
        /// 插入BuyClothes
        /// </summary>
        public bool Insert(UserOR mobj)
        {
            string sql = string.Format("insert into  [User] (UserName,UserCode,PWD) values ('{0}','{1}','{2}')",
              mobj.UserName, mobj.UserCode, mobj.PWD);

            return db.excuteNonquery(sql);
        }
        #endregion

        #region 修改
        public bool Update(UserOR mobj)
        {
            string sql = string.Format("update [User] set UserName='{0}',UserCode='{1}',PWD='{2}' where id={3}",
              mobj.UserName, mobj.UserCode, mobj.PWD, mobj.ID);

            return db.excuteNonquery(sql);
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除BuyClothes
        /// </summary>
        public virtual bool Delete(string strId)
        {
            string sql = string.Format("delete from [User] where  ID ={0}", strId);
            return db.excuteNonquery(sql);
        }
        #endregion

    }
}
