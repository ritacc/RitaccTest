using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace CSC
{
	public class SerializeHelper
	{
		#region JSON 方式序列化

		/// <summary>
		/// JSON 方式序列化
		/// </summary>
		/// <param name="obj">序列化对象</param>
		/// <returns>字符串</returns>
		public static string JSONSerialize(object obj)
		{
			return new JavaScriptSerializer().Serialize(obj);
		}

		/// <summary>
		/// JSON 方式反序列化
		/// </summary>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="json">字符串</param>
		/// <returns>对象</returns>
		public static T JSONDeserialize<T>(string json)
		{
			return new JavaScriptSerializer().Deserialize<T>(json);
		}


		/// <summary>
		/// JSON 方式反序列化
		/// 日期反序列化后的结果,需调用 ToLocalTime()才能得到本地时间
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="json"></param>
		/// <param name="s"></param>
		/// <returns></returns>
		public static T JSONDeserializeReplacDate<T>(string json)
		{
			Regex reg = new Regex("\\/Date\\(\\d{0,}\\)\\/");
			MatchEvaluator matchEvaluator = new MatchEvaluator(Replac);
			json = reg.Replace(json, matchEvaluator);
			return new JavaScriptSerializer().Deserialize<T>(json);
		}

		/// <summary>
		/// 替换
		/// </summary>
		/// <param name="m"></param>
		/// <returns></returns>
		private static string Replac(Match m)
		{
			string s = "\\" + m.Value.TrimEnd('/') + "\\" + "/";
			return s;
		}

		#endregion
	}
}