using System;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using App.Framework.Reflection;

namespace App.Framework.Web
{
	/// <summary>
	/// 枚举扩展方法
	/// </summary>
	public static class EnumExtensions
	{


		/// <summary>
		/// 获取字段说明信息
		/// </summary>
		public static string GetDescription(this Enum value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());

			if (field != null)
				return EnumHelper.GetDescription(field);

			return string.Empty;
		}

		/// <summary>
		/// 获得枚举值
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static object GetValue(this Enum value)
		{

			FieldInfo field = value.GetType().GetField(value.ToString());

			if (value != null)
				return field.GetValue(value);

			return string.Empty;
		}
	}



	/// <summary>
	/// 枚举辅助函数
	/// </summary>
	public class EnumHelper
	{
		/// <summary>
		/// 通过枚举值获取说明
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetDescriptionByKey<T>(string key) where T : struct
		{
			if (string.IsNullOrEmpty(key))
				return string.Empty;
			try
			{
				return ((Enum)Enum.Parse(typeof(T), key, true)).GetDescription();
			}
			catch
			{
				return key;
			}
		}

		/// <summary>
		/// 获得枚举说明
		/// </summary>
		public static Dictionary<Enum, string> GetDescriptions(Type t)
		{

			Dictionary<Enum, string> d = new Dictionary<Enum, string>();

			if (t.IsEnum == false)
				return d;

			var values = Enum.GetValues(t);

			foreach (var item in values)
				d.Add((Enum)item, ((Enum)item).GetDescription());

			return d;
		}

		/// <summary>
		/// 获得枚举-值和描述
		/// </summary>
		public static List<ListItemWidthType> GetListItemWidthType(Type t)
		{
			List<ListItemWidthType> listItem = new List<ListItemWidthType>();

			if (t.IsEnum == false)
				return null;

			var values = Enum.GetValues(t);

			foreach (var item in values)
			{
				listItem.Add(new ListItemWidthType() { Text = ((Enum)item).GetDescription(), Value = ((Enum)item).GetValue().ToString() });
			}
			return listItem;
		}

		/// <summary>
		/// 获得枚举-列表Text-Value相同
		/// </summary>
		public static List<ListItemWidthType> GetListItemWidthTypeDescription(Type t)
		{
			List<ListItemWidthType> listItem = new List<ListItemWidthType>();

			if (t.IsEnum == false)
				return null;

			var values = Enum.GetValues(t);

			foreach (var item in values)
			{
				listItem.Add(new ListItemWidthType() { Text = ((Enum)item).GetDescription(), Value = ((Enum)item).GetDescription() });
			}
			return listItem;
		}

		/// <summary>
		/// 通过枚举值获取说明
		/// </summary> 
		public static string GetDescriptionByValue(Type t, object value)
		{
			string result = string.Empty;
			if (value == null)
				return result;
			if (t.IsEnum)
			{
				var values = Enum.GetValues(t);

				foreach (var item in values)
				{
					if (item.ToString() == value.ToString())
					{
						result = ((Enum)item).GetDescription();
						break;
					}
				}
				return result;


				//if (!string.IsNullOrEmpty(enumName))
				//{
				//    FieldInfo field = t.GetField(enumName);

				//    result = EnumHelper.GetDescription(field);
				//}
			}

			return result;
		}

		/// <summary>
		/// 获取字段说明信息
		/// </summary>
		public static string GetDescription(FieldInfo field)
		{
            object[] attributes = field.GetCustomAttributes(typeof(EnumAttribute), false);

			string defaultAttribute = field.Name;

			if (attributes.Length > 0)
			{
				for (int i = 0; i < attributes.Length; i++)
				{
					EnumAttribute attr = attributes[i] as EnumAttribute;

					if (string.IsNullOrEmpty(attr.Culture))
						defaultAttribute = attr.Description;

					if (string.Compare(attr.Culture, Thread.CurrentThread.CurrentUICulture.Name, true) == 0)
						return attr.Description;
				}
			}

			return defaultAttribute;
		}

		/// <summary>
		/// 从资源文件中获取字段说明信息
		/// </summary>
		public static string GetResourceDescription(FieldInfo field, Type resourceType)
		{
			EnumAttribute enumInfo = ReflectionHelper.GetAttribute<EnumAttribute>(field);
			if (enumInfo == null)
			{
				return string.Empty;
			}
			return (string)resourceType.GetProperty(enumInfo.Description).GetValue(null, null);
		}
	}
}
