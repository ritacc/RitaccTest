using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using CRM.Business;
using CSC.Business;

namespace CSC
{
    public class ExportCSV
    {

        public static void DownLoad(string fileName)
        {
            //下载模版
            var file = new FileInfo(fileName);
            var context = global::System.Web.HttpContext.Current;

            context.Response.Clear();
            //context.Response.Buffer = true;
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Charset = "utf-8";
            context.Response.AddHeader("content-disposition", "attachment; filename=TempFile.csv");

            byte[] fileData = null;
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            fileData = new byte[fs.Length];
            fs.Read(fileData, 0, fileData.Length);
            fs.Flush();
            fs.Close();

            var ms = new MemoryStream(fileData);
            context.Response.BinaryWrite(ms.ToArray());
            context.Response.End();
        }

        #region 导出
        public static string TitleToCSV(List<ExportInfo> exportInfos, Type resourceType)
        {
            string result = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ExportInfo exportInfo in exportInfos)
            {
                string titleName = string.Empty;
                try
                {
                    titleName = (string)resourceType.GetProperty(exportInfo.ResourceName).GetValue(null, null);
                }
                catch
                {
                    titleName = string.Empty;
                }
                if (string.IsNullOrWhiteSpace(titleName))
                {
                    stringBuilder.Append(exportInfo.FileName + "\t");
                    continue;
                }
                stringBuilder.Append(titleName + "\t");
            }
            result = stringBuilder.ToString();

            if (!string.IsNullOrWhiteSpace(result))
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        public static string DataRowToCSV(List<ExportInfo> exportInfos, DataRow dr, Func<string,object, object> setValue =null)
        {
            string result = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();

            foreach (ExportInfo exportInfo in exportInfos)
            {
                DataColumn dc = dr.Table.Columns[exportInfo.FileName];
                if (dc == null)
                {
                    throw new NotSupportedException(string.Format("{0} 上未发现 DataColumn 定义", exportInfo.FileName));
                }

               
                object dcValue = dr[exportInfo.FileName];
                if (setValue != null)
                {
                    dcValue = setValue(exportInfo.FileName,dcValue);
                }
                switch (dc.DataType.Name)
                {
                    case "String":
                        if (dcValue != null && dcValue != DBNull.Value)
                        {
                            stringBuilder.Append("=\"" + (string)dcValue + "\"\t");
                        }
                        else
                        {
                            stringBuilder.Append("\t");
                        }
                        break;
                    case "DateTime":
                        if (dcValue != null && dcValue != DBNull.Value)
                        {
                            stringBuilder.Append(((DateTime)dcValue).ToString(exportInfo.FormatString) + "\t");
                        }
                        else
                        {
                            stringBuilder.Append("\t");
                        }
                        break;
                    case "Boolean":
                        if (dcValue != null && dcValue != DBNull.Value)
                        {
                            stringBuilder.Append(((bool)dcValue) ? "YES\t" : "NO\t");
                        }
                        else
                        {
                            stringBuilder.Append("\t");
                        }
                        break;
                    #region 特殊处理
                    case "Int32":
                        if (dcValue != null && dcValue != DBNull.Value)
                        {
                            if (dcValue.ToString() == string.Empty)
                            {
                                stringBuilder.Append("0\t");
                            }
                            else
                            {
                                stringBuilder.Append(dcValue.ToString() + "\t");
                            }
                        }
                        else
                        {
                            stringBuilder.Append("0\t");
                        }
                        break;
                    case "Decimal":
                        if (dcValue != null && dcValue != DBNull.Value)
                        {
                            if (dcValue.ToString() == string.Empty)
                            {
                                stringBuilder.Append("0.00%\t");
                            }
                            else
                            {
                                stringBuilder.Append(dcValue.ToString() + "%\t");
                            }
                        }
                        else
                        {
                            stringBuilder.Append("0.00%\t");
                        }
                        break;
                    #endregion
                    default:
                        if (dcValue != null && dcValue != DBNull.Value)
                        {
                            stringBuilder.Append(dcValue.ToString() + "\t");
                        }
                        else
                        {
                            stringBuilder.Append("\t");
                        }
                        break;
                }
            }

            result = stringBuilder.ToString();

            if (!string.IsNullOrWhiteSpace(result))
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        public static int DataTableExportToCSV(StreamWriter writer, List<ExportInfo> exportInfos, Type resourceType, DataTable dt, Func<string, object, object> setValue = null)
        {
            if (dt.Rows == null || dt.Rows.Count <= 0)
            {
                return -1;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(TitleToCSV(exportInfos, resourceType));
            foreach (DataRow dr in dt.Rows)
            {
                string drCSV = DataRowToCSV(exportInfos, dr, setValue);
                if (!string.IsNullOrWhiteSpace(drCSV))
                {
                    stringBuilder.AppendLine(drCSV);
                }
            }
            writer.Write(stringBuilder.ToString());
            return 0;
        }

        public static int DataTableExportToCSV(string fileName, List<ExportInfo> exportInfos, Type resourceType, DataTable dt, Func<string, object, object> setValue = null)
        {
            int result = 0;
            using (var writer = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                result = DataTableExportToCSV(writer, exportInfos, resourceType, dt,setValue);
                writer.Close();
            }
            return result;
        }

        public  static void Output(string filePath,string fileName)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            //context.Response.Buffer = true;
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Charset = "utf-8";
            context.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}.csv",fileName));

            byte[] fileData = null;
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            fileData = new byte[fs.Length];
            fs.Read(fileData, 0, fileData.Length);
            fs.Flush();
            fs.Close();

            var ms = new MemoryStream(fileData);
            context.Response.BinaryWrite(ms.ToArray());
            context.Response.End();
        }

        #endregion
    }
}