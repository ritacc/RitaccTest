using System.Text;

namespace App.Framework.Web
{
    /// <summary>
    /// 字符处理 公共方法类
    /// </summary>
    public static class CharacterHelper
    {
        #region 替换内容中的换行符
        /// <summary>
        /// 替换内容中的换行符
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string StrReplace(string message)
        {
            if (string.IsNullOrEmpty(message)) return string.Empty;

            return message.Replace("'", "\'").Replace("\r", "").Replace("\n", "");
        }
        #endregion

        #region 过滤内容中的特殊字符
        /// <summary>
        /// 过滤内容中的特殊字符
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string StrFiltrate(string message)
        {
            if (string.IsNullOrEmpty(message)) return string.Empty;

            StringBuilder sb = new StringBuilder(message);
            sb.Replace(" ", "");
            sb.Replace("'", "");
            sb.Replace("&", "");
            sb.Replace("%", "");
            sb.Replace("#", "");
            sb.Replace("=", "");

            return sb.ToString();
        }
        #endregion

        #region 转换GB2312的字符串为UTF8编码
        /// <summary>
        /// 转换GB2312的字符串为UTF8编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GB2312ToUTF8(string str)
        {
            Encoding uft8 = Encoding.GetEncoding(65001);
            Encoding gb2312 = Encoding.GetEncoding("gb2312");

            byte[] temp = gb2312.GetBytes(str);
            byte[] temp1 = Encoding.Convert(gb2312, uft8, temp);

            return uft8.GetString(temp1);
        }
        #endregion

        #region 转换UTF8的字符串为GB2312编码
        /// <summary>
        /// 转换UTF8的字符串为GB2312编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UTF8ToGB2312(string str)
        {
            Encoding uft8 = Encoding.GetEncoding(65001);
            Encoding gb2312 = Encoding.GetEncoding("gb2312");

            byte[] temp = uft8.GetBytes(str);
            byte[] temp1 = Encoding.Convert(uft8, gb2312, temp);

            return gb2312.GetString(temp1);
        }
        #endregion
    }
}
