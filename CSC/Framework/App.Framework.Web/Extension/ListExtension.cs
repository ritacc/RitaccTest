//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：ListExtension.cs
//文件功能：
//
//创建标识：鲜红 || 2011-04-25
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace App.Framework.Web
{
	/// <summary>
	/// List扩展方法类
	/// </summary>
	public static class ListExtention
	{



		/// <summary>
		/// 转化一个DataTable
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static DataTable ToDataTable<T>(this IEnumerable<T> list)
		{
			//创建属性的集合
			List<PropertyInfo> pList = new List<PropertyInfo>();
			//获得反射的入口
			Type type = typeof(T);
			DataTable dt = new DataTable();
			//把所有的public属性加入到集合 并添加DataTable的列
			Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, typeof(string) /*p.PropertyType*/); });
			foreach (var item in list)
			{
				//创建一个DataRow实例
				DataRow row = dt.NewRow();
				//给row 赋值
				pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
				//加入到DataTable
				dt.Rows.Add(row);
			}
			return dt;
		}

		public static List<SelectListItem> ToSelectList<T>(this List<T> list)
		{
			return list.ToList<T, SelectListItem>(null);
		}


		/// <summary>
		/// 将List T 转换为List SelectListItem 
		/// </summary>
		/// <typeparam name="T">原来的集合类型</typeparam>
		/// <param name="list">集合实例</param>
		/// <param name="call">转换的方法委托</param>
		/// <returns></returns>
		public static List<SelectListItem> ToSelectList<T>(this List<T> list, Func<T, SelectListItem> call)
		{
			return list.ToList<T, SelectListItem>(call);
		}

		/// <summary>
		/// 将List T 转换为List SelectListItem 
		/// </summary>
		/// <typeparam name="T">原来的集合类型</typeparam>
		/// <param name="list">集合实例</param>
		/// <param name="getText">文本</param>
		/// <param name="getValue">值</param>
		/// <returns></returns>
		public static List<SelectListItem> ToSelectList<T>(this List<T> list, Func<T, object> getText, Func<T, object> getValue)
		{
			return list.ToSelectList(getText, getValue, null);
		}

		/// <summary>
		/// 将List T 转换为List SelectListItem
		/// </summary>
		/// <typeparam name="T">原来的集合类型</typeparam>
		/// <param name="list">集合实例</param>
		/// <param name="getText">文本</param>
		/// <param name="getValue">值</param>
		/// <param name="getSelect">值</param>
		/// <returns>SelectListItem集合</returns>
		public static List<SelectListItem> ToSelectList<T>(this List<T> list, Func<T, object> getText, Func<T, object> getValue, Func<T, bool> getSelect)
		{
			if (list == null) return new List<SelectListItem>();
			if (list == null) throw new ArgumentNullException("list");
			if (getText == null) throw new ArgumentNullException("getText");
			if (getValue == null) throw new ArgumentNullException("getValue");

			List<SelectListItem> result = new List<SelectListItem>();

			foreach (T t in list)
			{
				result.Add(new SelectListItem()
				{
					Text = getText(t).ToString(),
					Value = getValue(t).ToString(),
					Selected = getSelect != null ? getSelect(t) : false
				});
			}
			return result;
		}



		#region test Add external property to listItem
		/// <summary>
		/// 将List T 转换为List SelectListItem 
		/// </summary>
		/// <typeparam name="T">原来的集合类型</typeparam>
		/// <param name="list">集合实例</param>
		/// <param name="getText">文本</param>
		/// <param name="getValue">值</param>
		/// <returns></returns>
		public static List<ListItemWidthType> ToSelectListWithType<T>(this List<T> list, Func<T, object> getText, Func<T, object> getValue, Func<T, object> getType)
		{
			return list.ToSelectListWidtType(getText, getValue, getType, null);
		}

		/// <summary>
		/// 将List T 转换为List SelectListItem
		/// </summary>
		/// <typeparam name="T">原来的集合类型</typeparam>
		/// <param name="list">集合实例</param>
		/// <param name="getText">文本</param>
		/// <param name="getValue">值</param>
		/// <param name="getSelect">值</param>
		/// <returns>SelectListItem集合</returns>
		public static List<ListItemWidthType> ToSelectListWidtType<T>(this List<T> list, Func<T, object> getText, Func<T, object> getValue, Func<T, object> getType, Func<T, bool> getSelect)
		{
			if (list == null) throw new ArgumentNullException("list");
			if (getText == null) throw new ArgumentNullException("getText");
			if (getValue == null) throw new ArgumentNullException("getValue");
			if (getType == null) throw new ArgumentNullException("getType");

			List<ListItemWidthType> result = new List<ListItemWidthType>();

			foreach (T t in list)
			{
				result.Add(new ListItemWidthType()
				{
					Text = getText(t) == null ? "" : getText(t).ToString(),
					Value = getValue(t)==null? "" : getValue(t).ToString(),
					InterValue = getType(t).ToString(),
					Selected = getSelect != null ? getSelect(t) : false
				});
			}
			return result;
		}

		public static List<ListItemWidthType> ToSelectListWidtType<T>(this List<T> list, Func<T, object> getText, Func<T, object> getValue, Func<T, bool> getSelect=null)
		{
			if (list == null) throw new ArgumentNullException("list");
			if (getText == null) throw new ArgumentNullException("getText");
			if (getValue == null) throw new ArgumentNullException("getValue");

			List<ListItemWidthType> result = new List<ListItemWidthType>();

			foreach (T t in list)
			{
				result.Add(new ListItemWidthType()
				{
					Text = getText(t).ToString(),
					Value = getValue(t).ToString(),
					InterValue =null,
					Selected = getSelect != null ? getSelect(t) : false
				});
			}
			return result;
		}

		public static List<ListItemWidthTypeTag> ToSelectListWidtTypeTag<T>(this List<T> list,
			Func<T, object> getText, Func<T, object> getValue, Func<T, object> getDesc, Func<T, object> getTag, Func<T, bool> getSelect)
		{
			if (list == null) throw new ArgumentNullException("list");
			if (getText == null) throw new ArgumentNullException("getText");
			if (getValue == null) throw new ArgumentNullException("getValue");
			if (getDesc == null) throw new ArgumentNullException("getDesc");
			if (getTag == null) throw new ArgumentNullException("getTag");

			List<ListItemWidthTypeTag> result = new List<ListItemWidthTypeTag>();

			foreach (T t in list)
			{
				result.Add(new ListItemWidthTypeTag()
				{
					Text =			getText(t).ToString(),
					Value =			getValue(t).ToString(),
					InterValue =	getDesc== null ? "" : getDesc(t).ToString(),
					Tag = getTag(t) != null ? getTag(t).ToString() : "",
					Selected =		getSelect != null ? getSelect(t) : false
				});
			}
			return result;
		}
		#endregion
		/// <summary>
		///  返回列表中满足条件的第一个元素；如果列表为空或未找到这样的元素，则返回默认值。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static T FirstOrDefaultAllowNull<T>(this List<T> list)
		{
			return list == null || list.Count <= 0 ? default(T) : list[0];
		}


		/// <summary>
		/// 将一个List集合进行二次转换
		/// </summary>
		/// <typeparam name="T">原来的集合类型</typeparam>
		/// <typeparam name="TResult">要转换成的集合类型</typeparam>
		/// <param name="list">集合实例</param>
		/// <param name="call">转换的方法委托</param>
		/// <returns>要转换成的集合类型</returns>
		public static List<TResult> ToList<T, TResult>(this List<T> list, Func<T, TResult> call)
		{
			return list.ConvertAll(new Converter<T, TResult>(call));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="list"></param>
		/// <param name="call"></param>
		/// <returns></returns>
		public static List<TResult> ToListOrDefault<T, TResult>(this List<T> list, Func<T, TResult> call)
		{
			if (list != null)
				return ToList(list, call);
			return null;
		}

		/// <summary>
		/// 将List转换为String
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="split"></param>
		/// <returns></returns>
		public static string ToString<T>(this List<T> list, string split)
		{
			string result = string.Empty;
			if (list == null || list.Count <= 0)
				return result;
			foreach (T t in list)
			{
				result += t.ToString() + split;
			}
			result = result.Substring(0, result.Length - 1);
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="list"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static TResult GetValueOrDefault<TResult>(this List<TResult> list, int index)
		{
			if (list.Count < index + 1)
				return default(TResult);
			return list[index];
		}

		public static void Each<T>(this List<T> arr, Action<int, T> action)
		{
			if (null == arr) return;
			for (int i = 0, len = arr.Count; i < len; i++)
			{
				action(i, arr[i]);
			}
		}

		public static void Each<T>(this List<T> arr, Func<int, T, bool> func)
		{
			if (null == arr) return;
			for (int i = 0, len = arr.Count; i < len; i++)
			{
				if (!func(i, arr[i]))
					break;
			}
		}


		public static void Each(this Array arr, Action<int, object> func)
		{
			if (null == arr) return;
			int i = 0;
			foreach (var item in arr)
			{
				func(i, item);
				i++;
			}
		}
	}

	public class ListItemWidthType : SelectListItem
	{
		public string InterValue { get; set; }
	}

	public class ListItemWidthTypeTag : ListItemWidthType
	{
		public string Tag { get; set; }
	}
}
