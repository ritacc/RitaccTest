//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：HtmlHelperExtention.cs
//文件功能：
//
//创建标识：鲜红 || 2011-04-11
//
//修改标识：
//修改描述：新增 IsGuid() ToGuid() 方法
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Framework.Web
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtention
	{
		#region "Added by jason in 20130704"
		/// <summary>
		/// 从Request提取List
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="str"></param>
		/// <param name="fun"></param>
		/// <returns></returns>
		public static List<T> ToListByRequestEx<T>(this string str, Func<string, T> fun)
		{
			
			return str.ToListEx<T>(',', fun);
		}

		/// <summary>
		/// 转换为List T
		/// </summary>
		/// <typeparam name="T">List中的元素类型</typeparam>
		/// <param name="str">带分隔符的字符串</param>
		/// <param name="fun">转换委托</param>
		/// <param name="split">分隔符</param>
		/// <returns></returns>
		public static List<T> ToListEx<T>(this string str, char split, Func<string, T> fun)
		{
			string[] arr = str.Split(split);
			List<T> list = new List<T>();
			foreach (string s in arr)
			{
				list.Add(fun(s));
			}
			return list;
		}
		#endregion

		/// <summary>
        /// 转换为List string 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static List<string> ToList(this string str, string split)
        {
            string[] arr = str.Split(split.ToCharArray());
            List<string> strList = new List<string>();
            foreach (string item in arr)
            {
                if (!string.IsNullOrEmpty(item))
                    strList.Add(item);
            }
            return strList;
        }
        /// <summary>
        /// 转换为List string 
        /// </summary>
        /// <param name="str">带分隔符的字符串</param>
        /// <param name="split">分隔符</param>
        /// <returns></returns>
        public static List<string> ToList(this string str, char split)
        {
            string[] arr = str.Split(split);
            return arr.ToList();
        }

        /// <summary>
        /// 转换为List T 
        /// </summary>
        /// <typeparam name="T">List中的元素类型</typeparam>
        /// <param name="str">带分隔符的字符串</param>
        /// <param name="fun">转换委托</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this string str, Func<string, T> fun)
        {
            return ToList(str, ',', fun);
        }

        /// <summary>
        /// 转换为List T
        /// </summary>
        /// <typeparam name="T">List中的元素类型</typeparam>
        /// <param name="str">带分隔符的字符串</param>
        /// <param name="fun">转换委托</param>
        /// <param name="split">分隔符</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this string str, char split, Func<string, T> fun)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            string[] arr = str.Split(split);
            List<T> list = new List<T>();
            foreach (string s in arr)
            {
                if (!string.IsNullOrEmpty(s))
                    list.Add(fun(s));
            }
            return list;
        }

        /// <summary>
        /// 从Request提取List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static List<T> ToListByRequest<T>(this string str, Func<string, T> fun)
        {
            return str.ToList<T>(',', fun);
        }

        /// <summary>
        /// 转换为int,在之前进行格式检查，如果格式不正确，或者超了范围则返回null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int? ToNullAbleInt(this string str)
        {
            try
            {
                int result = Convert.ToInt32(str);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public static int ParseInt(this string str)
        {
            int result;
            int.TryParse(str, out result);
            return result;

        }
        /// <summary>
        /// 获取首字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFristCharter(this string str)
        {
            return StringHelper.GetFirstLetterOfChinese(str);
        }

        /// <summary>
        /// 把字符串用指定符号分割，并取第一个
        /// </summary>
        /// <param name="str"></param>
        /// <param name="split"></param>
        /// <param name="nullCall"></param>
        /// <returns></returns>
        public static string GetFirstOrDefaultBySplit(this string str, char split, Func<string> nullCall)
        {
            str = str ?? string.Empty;
            string[] strArr = str.Split(split);
            if (strArr.Length <= 0 || string.IsNullOrEmpty(strArr[0]))
            {
                if (nullCall != null)
                    return nullCall();
                return string.Empty;
            }
            return strArr[0];
        }

        /// <summary>
        /// 字符串用逗号分割，并取第一个
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultStr"></param>
        /// <returns></returns>
        public static string GetFirstOrDefaultByComma(this string str, string defaultStr)
        {
            return str.GetFirstOrDefaultBySplit(',', () => defaultStr);
        }

        #region 判断字符串数据是否可转换为GUID类型数据

        /// <summary>
        /// 判断字符串数据是否可转换为GUID类型数据
        /// </summary>
        /// <param name="str">字符串数据</param>
        /// <returns>Boolean</returns>
        public static Boolean IsGuid(this string str)
        {
            if (string.IsNullOrEmpty(str)) return false;

            try { new Guid(str.Trim()); return true; }
            catch { return false; }
        }

        #endregion

        #region 将字符串数据转换为GUID类型数据

        /// <summary>
        /// 将字符串数据转换为GUID类型数据
        /// </summary>
        /// <param name="str">字符串数据</param>
        /// <returns>Guid</returns>
        public static Guid ToGuid(this string str)
        {
            if (string.IsNullOrEmpty(str)) return Guid.Empty;

            try { return new Guid(str.Trim()); }
            catch { return Guid.Empty; }
        }

        #endregion

        #region 将字符串转换为Int类型，转换无效时返回默认值
        /// <summary>
        /// 将字符串转换为Int类型，转换无效时返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToFirstOrDefaultInt(this string str)
        {
            int result = 0;

            int.TryParse(str, out result);

            return result;
        }
        #endregion

        #region 正则验证数据有效性

        /// <summary>
        /// 是否为有效的电子邮箱
        /// </summary>
        public static bool IsEmail(this string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            //Regex RegEmail = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*\.(com|net|org|edu|mil|tv|biz|info|cn|name)$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
            Regex RegEmail = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*\.[a-zA-Z]+$");
            Match m = RegEmail.Match(str);
            return m.Success;
        }

        /// <summary>
        /// 检测字符串是否为有效的域名
        /// </summary>
        /// <param name="str">要检测的字符串</param>
        /// <returns>Boolean</returns>
        public static bool IsDomain(this string str)
        {
            if (string.IsNullOrEmpty(str)) return false;

            Regex RegDomain = new Regex(@"^\w+(\.\w+)+$");
            return RegDomain.Match(str).Success;
        }

        #endregion

        #region 获取默认Email登录地址
        /// <summary>
        /// 获取默认Email登录地址
        /// </summary>
        /// <param name="emailAddress">Email</param>
        /// <returns>String 例如：http://mail.163.com</returns>
        public static string GetEmailHost(this string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress)) return string.Empty;

            int index = emailAddress.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries).Length == 2 ? emailAddress.LastIndexOf("@") : -1;

            return index > -1 ? string.Format("http://mail.{0}", emailAddress.Substring(index + 1)) : emailAddress;
        }
        #endregion
    }
}
