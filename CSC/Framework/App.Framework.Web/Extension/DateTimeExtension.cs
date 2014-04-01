using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Web
{
    public static class DateTimeExtension
    {
		//public static string Format(this DateTime date)
		//{
		//    return date.ToString(ConfigHelper.DateTimeFormat);
		//}

		/// <summary>
		/// 格式化成日期或者日期+时间
		/// </summary>
		/// <param name="date"></param>
		/// <param name="timeFlag">是否显示时间部分</param>
		/// <returns></returns>
		public static string Format(this DateTime date,bool timeFlag=false)
		{
			if (timeFlag)
			{
				return date.ToString(ConfigHelper.DateAndTimeFormat);
			}
			else 
			{
				return date.ToString(ConfigHelper.DateTimeFormat);
			}
		}


		public static string Format(this DateTime? date, bool timeFlag = false) {
			if (!date.HasValue)
				return string.Empty;
			return date.Value.Format(timeFlag);
		}

        public static string FormatHHmm(this DateTime date)
        {
            return date.ToString(ConfigHelper.DateAndHHmmFormat);
        }

        public static string FormatHHmm(this DateTime? date)
        {
            if (!date.HasValue)
                return string.Empty;

            return date.Value.ToString(ConfigHelper.DateAndHHmmFormat);
        }
    }
}
