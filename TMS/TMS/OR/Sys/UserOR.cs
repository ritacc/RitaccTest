using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TM.OR.Sys
{
    public class UserOR
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string UserCode { get; set; }

        public string PWD { get; set; }

        public UserOR()
        {

        }

        public UserOR(DataRow dr)
        {
            ID = Convert.ToInt32(dr["ID"].ToString());
            UserName = dr["Name"].ToString();

            UserCode = dr["UserCode"].ToString();
            PWD = dr["PWD"].ToString();
        }
    }
}
