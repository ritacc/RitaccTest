using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace TM.DA.DBCommon
{
    public class CommonDB
    {
        private string _errorInfo;
        public string ErrorInfo
        {
            get
            {
                return _errorInfo;
            }
        }


        protected OleDbConnection getConnection()
        {
			//Provider=Microsoft.Jet.OLEDB.4.0;
			string connectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};
User ID=admin;Password=;Jet OLEDB:Database Password=ABCabc123",appconifg.Instance.DBConnectionPath);
            OleDbConnection cn = new OleDbConnection(connectionString);
            return cn;
        }

        public DataSet executeQuery(String sql)
        {
            OleDbConnection conn = this.getConnection();
            DataSet ds = new DataSet();
            if (sql != "")
            {
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                }
                catch (Exception ex)
                {

                    _errorInfo = ex.Message;
                    return null;
                }
                try
                {
                    OleDbDataAdapter sqlAdapter = new OleDbDataAdapter();
                    sqlAdapter.SelectCommand = cmd;
                    sqlAdapter.Fill(ds);
                }
                catch (OleDbException ex)
                {

                    _errorInfo = ex.Message;
                    Error.WriteLog("ritacc.DBLzyer.CommonDB", "excuteNonquery", ex.Message);
                    throw ex;
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
            else
            {
                return null;
            }

            return ds;
        }

        public bool excuteNonquery(string strSQL)
        {
            OleDbConnection conn = this.getConnection();
            if (strSQL != "")
            {
                conn = getConnection();
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                }
                catch (Exception ex)
                {

					throw ex;
                }
                try
                {

                    OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {

                    _errorInfo = ex.Message;
                    Error.WriteLog("ritacc.DBLzyer.CommonDB", "excuteNonquery", ex.Message);
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}
