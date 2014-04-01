using System;
using System.Collections.Generic;
using System.Linq;

namespace CSC {
    /// <summary>
    /// 数据类型转换
    /// </summary>
    public static class ConvertHelper {


        /// <summary>
        /// 转换成 double
        /// </summary>
        /// <param name="s">要转换的参数</param>
        /// <param name="isSuccess">转换成是否成功</param>
        /// <returns>double</returns>
        public static double AsDouble(string s, out bool isSuccess)
        {
            double result;
            return (isSuccess = double.TryParse(s, out result)) ? result : 0;
        }
        /// <summary>
        /// 转换成 bool
        /// </summary>
        /// <param name="s">要转换的参数</param>
        /// <param name="defaultValue">转换失败缺省值</param>
        /// <returns>Boolean</returns>
        public static Boolean AsBool(this String s, bool defaultValue = false) {
            bool result;
			if (!string.IsNullOrEmpty(s) && s.ToUpper() == "Y")//仅仅针对checkbox里选Y
				return true;
            return Boolean.TryParse(s, out result) ? result : defaultValue;
        }
        /// <summary>
        /// 转换成 bool
        /// </summary>
        /// <param name="s">要转换的参数</param>
        /// <param name="defaultValue">转换失败缺省值</param>
        /// <returns>Boolean</returns>
        public static Boolean? AsBoolOfNull(this String s, bool? defaultValue = null)
        {
            bool result;
            return Boolean.TryParse(s, out result) ? result : defaultValue;
        }
		/// <summary>
		/// Convert Y or N to Boolean
		/// </summary>
		/// <param name="s">Input "Y" Or "N"</param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static Boolean AsBoolWithYorN(this String s, bool defaultValue = false)
		{
			return string.IsNullOrEmpty(s) ? defaultValue : (s.ToUpper() == "Y" ? true : false);
		}

        /// <summary>
        /// 转换成 datetime
        /// </summary>
        /// <param name="s">要转换的参数</param>
        /// <param name="defaultValue">转换失败缺省值</param>
        /// <returns>DateTime</returns>
        public static DateTime AsDateTime(this String s, DateTime defaultValue = new DateTime()) {
            DateTime result;
            return DateTime.TryParse(s, out result) ? result : defaultValue;
        }

        public static DateTime AsDateTimeForConfig(this String s, DateTime defValue = new DateTime(), string defFormat = "")
        {
            if (string.IsNullOrEmpty(defFormat))
                defFormat = App.Framework.Web.ConfigHelper.DateTimeFormat;

            DateTime result;

            if (DateTime.TryParseExact(s, defFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
                return result;

            return defValue;
        }

        /// <summary>
        /// 转换成 dateTime
        /// </summary>
        /// <param name="s">要转换的参数</param>
        /// <param name="isSuccess">转换成是否成功</param>
        /// <returns>double</returns>
        public static DateTime AsDateTime(string s, out bool isSuccess)
        {
            DateTime result;
            isSuccess = DateTime.TryParse(s, out result);
            return result;
        }

        /// <summary>
        /// 扩展字符串转换成 DateTime? 类型
        /// 作者：TuJiang
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Nullable<DateTime> AsDateTimeOfNull(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            DateTime res;
            if (DateTime.TryParse(s, out res))
                return res;

            return null;
        }

		/// <summary>
		/// 首字母转大写
		/// </summary>
		/// <param name="strChange"></param>
		/// <returns></returns>
		public static string AsChangeFirstLetterToUpper(this string strChange)
		{
			try
			{
				if (!string.IsNullOrEmpty(strChange))
				{
					string tempFirst = strChange.Substring(0, 1);
					string tempElse = strChange.Substring(1, strChange.Length - 1);
					return (tempFirst.ToUpper() + tempElse.ToLower());
				}
				return string.Empty;
			}
			catch
			{
				return strChange;
			}
		}


        public static Nullable<DateTime> AsDateTimeOfNullForConfig(this String s, string defFormat = "")
        {
            if (string.IsNullOrEmpty(s))
                return null;

            if (string.IsNullOrEmpty(defFormat))
                defFormat = App.Framework.Web.ConfigHelper.DateTimeFormat;

            DateTime result;

            if (DateTime.TryParseExact(s, defFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
                return result;

            return null;
        }

        /// <summary>
        /// 转换成 Decimal
        /// </summary>
        /// <param name="s">要转换的参数</param>
        /// <param name="defaultValue">转换失败缺省值</param>
        /// <returns>Decimal</returns>
        public static Decimal AsDecimal(this String s, Decimal defaultValue = 0M) {
            Decimal result;
            return Decimal.TryParse(s, out result) ? result : defaultValue;
        }

		public static Nullable<decimal> AsDecimalOfNull(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return null;

			decimal res;
			if (Decimal.TryParse(s, out res))
				return res;

			return null;
		}


        /// <summary>
        /// 转换成 float
        /// </summary>
        /// <param name="s">要转换的参数</param>
        /// <param name="defaultValue">转换失败缺省值</param>
        /// <returns>float</returns>
        public static float AsFloat(this String s, float defaultValue = 0f) {
            float result;
            return float.TryParse(s, out result) ? result : defaultValue;
        }
        /// <summary>
        /// 转换成 Int32
        /// </summary>
        /// <param name="s">要转换的参数</param>
        /// <param name="defaultValue">转换失败缺省值</param>
        /// <returns>Int32</returns>
        public static Int32 AsInt(this String s, int defaultValue = 0) {
            int result = defaultValue;
            return int.TryParse(s, out result) ? result : defaultValue;
        }
        /// <summary>
        /// 转换成 long
        /// </summary>
        /// <param name="s">要转换的参数</param>
        /// <param name="defaultValue">转换失败缺省值</param>
        /// <returns>Int32</returns>
        public static long AsLong(this String s, long defaultValue = 0)
        {
            long result = defaultValue;
            return long.TryParse(s, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 扩展字符串转换成 long? 类型
        /// 作者：TuJiang
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Nullable<Int64> AsLongOfNull(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            long res;
            if (long.TryParse(s, out res))
                return res;
            return null;
        }

        /// <summary>
        /// 日期转换成 String
        /// </summary>
        /// <param name="s">要转换的参数</param>
        /// <param name="defaultValue">转换失败缺省值</param>
        /// <returns>string</returns>
        public static string DateToString(this DateTime d)
        {
            return d.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 可空类型转换
        /// </summary>
        /// <typeparam name="T">转换的可空类型</typeparam>
        /// <param name="s">字符串</param>
        /// <returns>转换的可空类型</returns>
        public static T AsNullable<T>(this String s) {
            if (string.IsNullOrEmpty(s)) {
                return default(T);
            }
            else {
                try {
                    return (T)Convert.ChangeType(s, typeof(T).GetGenericArguments()[0]);
                }
                catch {
                    return default(T);
                }
            }
        }

		/// <summary>
		/// 将1,3,5,7转换为2进制字符串1010101
		/// </summary>
		/// <param name="vaule"></param>
		/// <param name="formatLen">格式化成二进制字符串的长度</param>
		/// <returns></returns>
		public static string AsBitStr(string value, int formatLen)
		{
			if (string.IsNullOrEmpty(value))
				return value;

			double sum = 0;
			string[] arr = value.Split(',');
			List<string> list = arr.ToList();
			foreach (string v in list)
			{
				sum += Math.Pow(2, double.Parse(v) - 1);
			}

			string bitStr = Convert.ToString(Convert.ToInt64(sum), (int)2);
			char[] charList = bitStr.ToCharArray();
			Array.Reverse(charList);
			return FormatBitStr(new String(charList), formatLen);
		}

		/// <summary>
		/// 格式化二进制字符串，如果不够就补0
		/// </summary>
		/// <param name="value"></param>
		/// <param name="formatLen">格式化成二进制字符串的长度</param>
		/// <returns></returns>
		public static string FormatBitStr(string value, int formatLen)
		{
			for (int i = 1; i <= formatLen - value.Length; i++)
			{
				value += "0";
			}
			return value;
		}

		/// <summary>
		/// 将2进制字符串1010101的转换为1,3,5,7
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ConvertBitStrToStr(string value)
		{
			if (string.IsNullOrEmpty(value))
				return value;

			string returnValue = string.Empty;
			char[] arrList = value.ToCharArray();
			for (int i = 1; i <= arrList.Length; i++)
			{
				if (arrList[i - 1] == '1')
				{
					returnValue += (i.ToString() + ",");
				}
			}
			return returnValue.Substring(0, returnValue.Length - 1);
		}

    }
}
