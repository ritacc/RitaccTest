using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TM
{
    public class Error
    {
        public static void WriteLog(string clssWhere, string functionName, string errorInfo)
        {
            try
            {
                string m_logInfo = DateTime.Now.ToString() + "    clssWhere:" + clssWhere + "       functionName:" + functionName + "  errorInfo:" + errorInfo;
                FileInfo fileInfo = new FileInfo("log.txt");
                StreamWriter sw = fileInfo.AppendText();
                sw.WriteLine(m_logInfo);
                sw.Close();
            }
            catch { }
        }
    }
}
