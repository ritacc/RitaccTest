using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web.Script.Serialization;

namespace App.Framework.Web
{
	public static class JSONExtension
	{
		public static string ToJSON2(this object obj)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Serialize(obj);
		}

		public static string ToJSON(this object obj)
		{
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
			using (MemoryStream ms = new MemoryStream())
			{
				serializer.WriteObject(ms, obj);
				return Encoding.UTF8.GetString(ms.ToArray());
			}
		}

		public static T ParseJSON<T>(this string str)
		{
			T obj = Activator.CreateInstance<T>();
			using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(str)))
			{
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
				return (T)serializer.ReadObject(ms);
			}
		}
	}
}
