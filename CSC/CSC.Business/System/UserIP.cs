using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework.Data;

namespace CRM.Business
{
    [DbCommand("spSearchUserLock")]
    public class SearchUserLock : DataCriteria
    {
        [DbParameter("USER_ID")]
        public long? USER_ID        {get;set;}
        [DbParameter("USER_CODE")]
        public string USER_CODE     {get;set;}
        [DbParameter("LOGIN_IP")]
        public string LOGIN_IP      {get;set;}
        [DbParameter("WP_KEY")]
        public string WP_KEY { get; set; }
    }

    [DbCommand("spUpdateUserIp")]
    public class UserIP : DataCriteria
    {
        [DbParameter("USER_ID")]
        public long? USER_ID { get; set; }
        [DbParameter("IP")]
        public string IP { get; set; }
    }

    [DbCommand("spGetUserLoginIp")]
    public class GetUserLoginIp : DataCriteria
    {
        [DbParameter("USER_ID")]
        public long? USER_ID { get; set; }
        [DbParameter("LOGIN_IP", System.Data.ParameterDirection.Output)]
        public string LOGIN_IP { get; set; }

    }

    [DbCommand("spGetWpKey")]
    public class GetWpKey : DataCriteria
    {
        [DbParameter("USER_ID")]
        public long? USER_ID
        {
            get
            {
                return App.Framework.Security.User.Current.UserId;
            }
        }

    }

    [DbCommand("spSaveUserLock")]
    public class SaveUserLock : DataCriteria
    {
        private UserLock m_parent;
        public SaveUserLock(UserLock parent)
        {
            m_parent = parent;
        }

        [DbParameter("USER_ID")]
        public long? USER_ID
        {
            get
            {
                return App.Framework.Security.User.Current.UserId;
            }
        }
        [DbParameter("LOGIN_IP")]
        public string LOGIN_IP { get { return m_parent.LOGIN_IP; } set { m_parent.LOGIN_IP = value; } }
        [DbParameter("WP_KEY")]
        public string WP_KEY { get { return m_parent.WP_KEY; } set { m_parent.WP_KEY = value; } }

    }

    public class UserLock : DataEntity
    {
        [DbField("USER_CODE")]
        public string USER_CODE { get; set; }

        [DbField("USER_ID")]
        public long? USER_ID { get; set; }
        [DbField("LOGIN_IP")]
        public string LOGIN_IP { get; set; }
        [DbField("WP_KEY")]
        public string WP_KEY { get; set; }

        public override DataCriteria CreateDeleteCriteria()
        {
            throw new NotImplementedException();
        }

        public override DataCriteria CreateSaveCriteria()
        {
            return new SaveUserLock(this);
        }
    }

    [DbCommand("spClearWpKey")]
    public class ClearWpKey : DataCriteria
    {
        [DbParameter("USER_ID")]
        public long? USER_ID
        {
            get;
            set;
        }
    }



}
