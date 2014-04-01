using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using System.Web.UI;
using System.Web.Routing;
using App.Framework.Web.Filters;
using App.Framework.Web.User;
using App.Framework.Web.Permissions;
using App.Framework.Web;
using System.Web.Mvc;
using App.Framework.Data;
using App.Framework;
using System.IO;
using System.Text;
using App.Framework.Caching;
using System.Web.Caching;
using System.Collections;
using System.Text.RegularExpressions;

namespace CSC
{
	public class ToolHelper
	{
        public static string GetHtmlSelect(List<SelectListItem> selItems, string selVal, string styleStr)
        {
            if (selItems != null && selItems.Count > 0)
            {
                StringBuilder ss = new StringBuilder();
                ss.AppendFormat("<select{0}>", string.IsNullOrEmpty(styleStr) ? string.Empty : " style=\"" + styleStr + "\"");
                foreach (var item in selItems)
                {
                    if (item.Value.Equals(selVal))
                    {
                        ss.AppendFormat("<option selected=\"selected\" value=\"{0}\">{1}</option>", item.Value, item.Text);
                    }
                    else
                    {
                        ss.AppendFormat("<option value=\"{0}\">{1}</option>", item.Value, item.Text);
                    }
                }
                ss.Append("</select>");
                return ss.ToString();
            }
            return string.Empty;
        }
	}

	//public static class ValidTxtbox
	//{
	//    /// <summary>
	//    /// 验证两个txtbox范围
	//    /// </summary>
	//    /// <typeparam name="T">传入的类型</typeparam>
	//    /// <param name="txtLower">下限的txtbox</param>
	//    /// <param name="txtUpper">上限的txtbox</param>
	//    /// <param name="func">比较大小，小值放前面</param>
	//    /// <param name="controller">controller</param>
	//    /// <param name="txtname">字段的名称</param>
	//    /// <returns></returns>
	//    public static bool ValidTextLimit<T>(T txtLower, T txtUpper, Func<T, T, bool> func, Controller controller, string txtname)
	//    {
	//        if (!string.IsNullOrEmpty(Convert.ToString(txtUpper)))
	//        {
	//            if (func(txtUpper, (T)SD_ChanageType(0, typeof(T))))
	//            {
	//                MessageHelper.ShowMessage(controller, txtname + ":" + ServicePackage.txtLimitErr, isSucessed: false);
	//                return false;
	//            }
	//        }
	//        if (!string.IsNullOrEmpty(Convert.ToString(txtLower)))
	//        {
	//            if (func(txtLower, (T)SD_ChanageType(0, typeof(T))))
	//            {
	//                MessageHelper.ShowMessage(controller, txtname + ":" + ServicePackage.txtLimitErr, isSucessed: false);
	//                return false;
	//            }
	//        }
	//        if (!string.IsNullOrEmpty(Convert.ToString(txtUpper)) && !string.IsNullOrEmpty(Convert.ToString(txtLower)))
	//        {
	//            if (func(txtUpper, txtLower))
	//            {
	//                MessageHelper.ShowMessage(controller, txtname + ":" + ServicePackage.txtLimitErr, isSucessed: false);
	//                return false;
	//            }
	//        }
	//        return true;
	//    }

	//    private static object SD_ChanageType(object value, Type convertsionType)
	//    {
	//        //判断convertsionType类型是否为泛型，因为nullable是泛型类,
	//        if (convertsionType.IsGenericType && convertsionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))         //判断convertsionType是否为nullable泛型类
	//        {
	//            if (value == null || value.ToString().Length == 0)
	//            {
	//                return null;
	//            }
	//            //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
	//            System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(convertsionType);
	//            //将convertsionType转换为nullable对的基础基元类型
	//            convertsionType = nullableConverter.UnderlyingType;
	//        }
	//        return Convert.ChangeType(value, convertsionType);
	//    }
	//}

	public class ExportCvsByList
	{

		/// <summary>
		/// 导出CSV
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="criteria"></param>
		/// <param name="exportList"></param>
		/// <param name="resourceType"></param>
		/// <param name="filename"></param>
		public static void Export<T>(DataCriteria criteria, List<string> exportList, Type resourceType, string filename = "csv") where T : DataEntity
		{
			var blist = BusinessPortal.Search<T>(criteria);
			string filenPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
			int cvs = blist.ExportToCSV(filenPath, exportList, resourceType);

			System.Web.HttpContext context = global::System.Web.HttpContext.Current;
			context.Response.Clear();
			context.Response.ContentType = "text/plain";
			context.Response.ContentEncoding = System.Text.Encoding.UTF8;
			context.Response.Charset = "utf-8";
			context.Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".csv");

			byte[] fileData = null;
			FileStream fs = new FileStream(filenPath, FileMode.Open, FileAccess.Read);
			fileData = new byte[fs.Length];
			fs.Read(fileData, 0, fileData.Length);
			fs.Flush();
			fs.Close();
			MemoryStream ms = new MemoryStream(fileData);
			context.Response.BinaryWrite(ms.ToArray());
			context.Response.End();
		}
		private static int Comparison<C>(Comparison<C> c, C x, C y, SortDirectionEnum sortDirection)
		{
			if (c == null)
				return 0;
			var xCopy = x;
			var yCopy = y;
			if (sortDirection == SortDirectionEnum.Desc)
			{
				var tmp = xCopy;
				xCopy = yCopy;
				yCopy = tmp;
			}
			return c(xCopy, yCopy);
		}

		public static void ExportWithSort<T>(DataCriteria criteria, List<string> exportList, PageParams pageParams, Type resourceType, string filename = "csv", List<Comparison<T>> comparisonList = null) where T : DataEntity
		{
			var m_InnerList = BusinessPortal.Search<T>(criteria);

			if (comparisonList != null && comparisonList.Count > 0 && pageParams.SortField >= 0)
			{
				m_InnerList.InnerList.Sort((x, y) =>
				{
					var result = Comparison(comparisonList[pageParams.SortField], x, y, pageParams.sortDirection);
					if (pageParams.SecondSortFields != null)
					{
						int i = 0;
						while (result == 0 && i < pageParams.SecondSortFields.Count)
						{
							var comp = pageParams.SecondSortFields[i];
							if (comp.Key != pageParams.SortField)
							{
								result = Comparison(comparisonList[comp.Key], x, y, comp.Value);
							}
							i++;
						}
					}
					return result;
				});

			}

			string filenPath = Path.Combine(Path.GetTempPath(), new Guid().ToString());
			int cvs = m_InnerList.ExportToCSV(filenPath, exportList, resourceType);

			System.Web.HttpContext context = global::System.Web.HttpContext.Current;
			context.Response.Clear();
			context.Response.ContentType = "text/plain";
			context.Response.ContentEncoding = System.Text.Encoding.UTF8;
			context.Response.Charset = "utf-8";
			context.Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".csv");

			byte[] fileData = null;
			FileStream fs = new FileStream(filenPath, FileMode.Open, FileAccess.Read);
			fileData = new byte[fs.Length];
			fs.Read(fileData, 0, fileData.Length);
			fs.Flush();
			fs.Close();
			MemoryStream ms = new MemoryStream(fileData);
			context.Response.BinaryWrite(ms.ToArray());
			context.Response.End();
		}
	}

	public class EncryptHelper
	{
		public static string Encode(string str, string key)
		{
			DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
			provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
			provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			MemoryStream stream = new MemoryStream();
			CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
			stream2.Write(bytes, 0, bytes.Length);
			stream2.FlushFinalBlock();
			StringBuilder builder = new StringBuilder();
			foreach (byte num in stream.ToArray())
			{
				builder.AppendFormat("{0:X2}", num);
			}
			stream.Close();
			return builder.ToString();
		}
		public static string Decode(string str, string key)
		{

			DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
			provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
			provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));
			byte[] buffer = new byte[str.Length / 2];
			for (int i = 0; i < (str.Length / 2); i++)
			{
				int num2 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);
				buffer[i] = (byte)num2;
			}

			MemoryStream stream = new MemoryStream();
			CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
			stream2.Write(buffer, 0, buffer.Length);
			stream2.FlushFinalBlock();
			stream.Close();
			return Encoding.UTF8.GetString(stream.ToArray());

		}
	}

	public class Encryption64
	{
		public static string Decrypt(string stringToDecrypt, string sEncryptionKey)
		{

			byte[] key = { };
			byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 90 };
			byte[] inputByteArray = new byte[stringToDecrypt.Length];

			try
			{
				key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
				DESCryptoServiceProvider des = new DESCryptoServiceProvider();
				inputByteArray = Convert.FromBase64String(stringToDecrypt);

				MemoryStream ms = new MemoryStream();
				CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
				cs.Write(inputByteArray, 0, inputByteArray.Length);
				cs.FlushFinalBlock();

				Encoding encoding = Encoding.UTF8;
				return encoding.GetString(ms.ToArray());
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
		}

		public static string Encrypt(string stringToEncrypt, string sEncryptionKey)
		{

			byte[] key = { };
			byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 90 };
			byte[] inputByteArray; //Convert.ToByte(stringToEncrypt.Length)

			try
			{
				key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
				DESCryptoServiceProvider des = new DESCryptoServiceProvider();
				inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
				MemoryStream ms = new MemoryStream();
				CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
				cs.Write(inputByteArray, 0, inputByteArray.Length);
				cs.FlushFinalBlock();

				return Convert.ToBase64String(ms.ToArray());
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
		}
	}

	public class PySearch
	{
		private static Dictionary<string, string> _cacheDic = new Dictionary<string, string>();
		/// <summary>
		/// 获取简体中文字符串拼音首字母
		/// </summary>
		/// <param name="input">简体中文字符串</param>
		/// <returns>拼音首字母</returns>
		public static string getSpells(string input)
		{
			int len = input.Length;
			string reVal = "";
			for (int i = 0; i < len; i++)
			{
				reVal += getSpell(input.Substring(i, 1));
			}
			return reVal;
		}

		/// <summary>
		/// 获取一个简体中文字的拼音首字母
		/// </summary>
		/// <param name="cn">一个简体中文字</param>
		/// <returns>拼音首字母</returns>
		public static string getSpell(string cn)
		{
			byte[] arrCN = Encoding.Default.GetBytes(cn);
			if (arrCN.Length > 1)
			{
				int area = (short)arrCN[0];
				int pos = (short)arrCN[1];
				int code = (area << 8) + pos;
				int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
				for (int i = 0; i < 26; i++)
				{
					int max = 55290;
					if (i != 25) max = areacode[i + 1];
					if (areacode[i] <= code && code < max)
					{
						return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
					}
				}
				return "?";
			}
			else return cn;
		}
		/// <summary>
		/// 字符串按照首拼音去比较
		/// </summary>
		/// <param name="str1"></param>
		/// <param name="str2"></param>
		/// <returns></returns>
		public static int ComparePY(string str1, string str2)
		{
			str1 = getSpells(str1);
			str2 = getSpells(str2);
			return str1.CompareTo(str2);
		}

		/// <summary>
		/// 从Enum中获取对应的DSC描述的值
		/// </summary>
		/// <param name="str1"></param>
		/// <param name="str2"></param>
		/// <param name="EnumType"></param>
		/// <param name="DefaultCachePrex"></param>
		/// <returns></returns>
		public static int ComparePYbyCache<T>(string str1, string str2, string DefaultCachePrex = "LmEnum") where T : struct
		{
			str1 = GetEnumDsc<T>(DefaultCachePrex, str1);
			str2 = GetEnumDsc<T>(DefaultCachePrex, str2);
			return ComparePY(str1, str2);
		}

		private static string GetEnumDscWithDic<T>(string cachePrex, string name) where T : struct
		{
			string tmp = string.Empty;
			if (!_cacheDic.TryGetValue(cachePrex + name, out tmp))
			{
				tmp = EnumHelper.GetDescriptionByKey<T>(name);
				_cacheDic.Add(cachePrex + name, tmp);
			}
			return tmp;
		}

		private static string GetEnumDsc<T>(string cachePrex, string name) where T : struct
		{
			if (CacheHelper.Get(cachePrex + name) == null)
				CacheHelper.Insert(cachePrex + name, EnumHelper.GetDescriptionByKey<T>(name), 60 * 60);
			return CacheHelper.Get(cachePrex + name) as string;
		}
	}

	public static class HtmlHelper2
	{
		public static bool ShopChange(HtmlHelper helper)
		{
			bool isCsShopType = App.Framework.Security.User.Current.ShopType.Equals("CS", StringComparison.CurrentCultureIgnoreCase);
			if (isCsShopType)
			{
				bool flag = true;
				string controller = helper.ViewContext.RouteData.Values["controller"].ToString().ToLower();
				switch (controller)
				{
					case "stockadjustment": flag = false; break;
					case "purchaseordermaintenance": flag = false; break;
					case "purchaseorder": flag = false; break;
					case "postockarrivalcorrection": flag = false; break;
					case "poreturntovendor": flag = false; break;
					case "poreceipt": flag = false; break;
					case "purchaseapproval": flag = false; break;
				}
				return flag && isCsShopType;
			}
			return true;
			
		}

		public static MvcHtmlString ActionLinkWithPermission(this HtmlHelper helper, EnumPermission permission, string linkText, string id, string classname = null, object htmlAttributes = null, string url = null)
		{
			string key = permission.ToString();

			//bool isCsShopType = App.Framework.Security.User.Current.ShopType.Equals("GODOWN", StringComparison.CurrentCultureIgnoreCase);

			//((Route)RouteTable.Routes["Default"]).GetRouteData().

			if (UserExtension.GetCurrentUserHasPermissionsPoints().BlockIsNull().Any(m => m.Key == key) && ShopChange(helper))
			{
				TagBuilder builder = new System.Web.Mvc.TagBuilder("a");
				builder.GenerateId(id);

				RouteValueDictionary rvd = null;
				if (htmlAttributes != null)
                    rvd = new RouteValueDictionary(htmlAttributes);
				else
					rvd = new RouteValueDictionary();
				if (classname != null)
					rvd.Add("class", classname); //builder.AddCssClass()
				if (url != null)
					rvd.Add("href", url);
				builder.SetInnerText(linkText);
				builder.MergeAttributes(rvd);

				return MvcHtmlString.Create(builder.ToString());
			}
			return MvcHtmlString.Create(string.Empty);
		}

		public static MvcHtmlString Lov(string name, string val,string labelval , string url, string width, string height, string title, string btnSure, string btnCancel, string divContain,string editFunc ="null", string callFun = "null",string rootPath="")
		{			
			string ret = string.Format(@"GenerateLov('{0}','{1}','{2}','{3}',{4},{5},'{6}','{7}','{8}','{9}',{10},{11},'{12}');",name,val,labelval,url,width,height,title,btnSure,btnCancel,divContain,editFunc,callFun,rootPath);
			return MvcHtmlString.Create(ret);
		}


		public static string GenericSaveFileNameByGUID(string file)
		{
			if (string.IsNullOrEmpty(file) || file.LastIndexOf('.') == -1)
				return "";
			string Extend = file.Substring(file.LastIndexOf('.'));
			string filename = file.Substring(0, file.LastIndexOf('.'));
			return filename + "---" + Guid.NewGuid().ToString() + Extend;
		}

		public static MvcHtmlString DllAddOption(this HtmlHelper helper, string name, string id,
			IEnumerable<SelectListItem2> selectList, string classname = null, string optionLabel = null, object htmlAttributes = null)
		{
			TagBuilder builder = new System.Web.Mvc.TagBuilder("select");
			builder.GenerateId(id);

			RouteValueDictionary rvd = null;
			if (htmlAttributes != null)
				rvd = new RouteValueDictionary(htmlAttributes);
			else
				rvd = new RouteValueDictionary();
			if (classname != null)
				rvd.Add("class", classname); //builder.AddCssClass()
			rvd.Add("id", id);
			rvd.Add("name", name);
			builder.MergeAttributes(rvd);
			//builder.MergeAttributes(htmlAttributes);
			//builder.MergeAttributes<string, object>((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)); 
			//builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

			if (optionLabel != null)
			{
				builder.InnerHtml = string.Format("<option value=\"\">{0}</option>", optionLabel);
			}

			StringBuilder optionBuiler = new StringBuilder();
			string option = "<option {2} value='{1}'>{0}</option>";
			string optionSelect = "<option {2} value='{1}' selected='selected'>{0}</option>";
			foreach (SelectListItem2 item in selectList)
			{
				if (item.Selected)
					optionBuiler.AppendFormat(optionSelect, item.Text, item.Value, item.Additional);
				else
					optionBuiler.AppendFormat(option, item.Text, item.Value, item.Additional);
			}
			builder.InnerHtml += optionBuiler.ToString();			
			return MvcHtmlString.Create(builder.ToString());
		}

		public static List<SelectListItem2> ToSelectList2<T>(this List<T> list, Func<T, object> getText, Func<T, object> getValue, Func<T, bool> getSelect,Func<T,string> getAdditional)
		{
			if (list == null) return new List<SelectListItem2>();
			if (list == null) throw new ArgumentNullException("list");
			if (getText == null) throw new ArgumentNullException("getText");
			if (getValue == null) throw new ArgumentNullException("getValue");

			List<SelectListItem2> result = new List<SelectListItem2>();

			foreach (T t in list)
			{
				result.Add(new SelectListItem2()
				{
					Text = getText(t).ToString(),
					Value = getValue(t).ToString(),
					Selected = getSelect != null ? getSelect(t) : false,
					Additional = getAdditional!=null?getAdditional(t):""
				});
			}
			return result;
		}
	}
	public class SelectListItem2
	{
	
		public bool Selected { get; set; }
		
		public string Text { get; set; }

		public string Value { get; set; }
		/// <summary>
		/// 额外的值，通常用于添加到option的属性里
		/// </summary>
		public string Additional { get; set; }
	}
	public class CacheHelper : ICacheProvider
	{
		public static readonly int DayFactor = 17280;
		public static readonly int HourFactor = 720;
		public static readonly int MinuteFactor = 12;
		public static readonly double SecondFactor = 0.2;
		private static readonly Cache cache;
		private static int Factor = 5;
		private static readonly object lockobj = new object();

		private string SessionID
		{
			get
			{
				return HttpContext.Current.Session.SessionID;
			}
		}

		public void Add(string key, object obj)
		{
			Insert(key, obj, null);
		}

		public void Remove(string key)
		{
			RemoveByKey(key);
		}

		public T GetCache<T>(string key)
		{
			if (cache[key] == null)
				return default(T);
			else
			{
				return (T)cache[key];
			}
		}

		public T GetOrSetCache<T>(string key, Func<T> fun, int seconds = 120)
		{
			//T result = GetCache<T>(key);
			T result = default(T);
			if (cache[key] == null)
			{
				result = fun();
				Insert(key, result, null, seconds);
			}
			else
				result = (T)cache[key];
			return result;
		}

		//关联用户SessionID
		//public void Add<T>(string key, T obj, int seconds)
		//{           
		//    if (obj != null)
		//    {
		//        lock (lockobj)
		//        {
		//            Dictionary<string, T> tmpdic;
		//            if (cache[SessionID] == null)
		//                tmpdic = new Dictionary<string, T>();
		//            else
		//                tmpdic = (Dictionary<string, T>)cache[SessionID];
		//            if (tmpdic.ContainsKey(key))
		//                tmpdic[key] = obj;
		//            else
		//                tmpdic.Add(key, obj);
		//            cache.Insert(SessionID, tmpdic, null, DateTime.Now.AddSeconds(Factor * seconds), TimeSpan.Zero, CacheItemPriority.Normal, null);
		//        }
		//    }

		//}      

		public static void ResetFactor(int cacheFactor)
		{
			Factor = cacheFactor;
		}
		/// <summary> 
		/// Static initializer should ensure we only have to look up the current cache 
		/// instance once. 
		/// </summary> 
		static CacheHelper()
		{
			HttpContext context = HttpContext.Current;
			if (context != null)
			{
				cache = context.Cache;
			}
			else
			{
				cache = HttpRuntime.Cache;
			}
		}
		/// <summary> 
		/// 清空Cash对象 
		/// </summary> 
		public static void Clear()
		{
			IDictionaryEnumerator CacheEnum = cache.GetEnumerator();
			ArrayList al = new ArrayList();
			while (CacheEnum.MoveNext())
			{
				al.Add(CacheEnum.Key);
			}
			foreach (string key in al)
			{
				cache.Remove(key);
			}
		}
		/// <summary> 
		/// 根据正则表达式的模式移除Cache 
		/// </summary> 
		/// <param name="pattern">模式</param> 
		public static void RemoveByPattern(string pattern)
		{
			IDictionaryEnumerator CacheEnum = cache.GetEnumerator();
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
			while (CacheEnum.MoveNext())
			{
				if (regex.IsMatch(CacheEnum.Key.ToString()))
					cache.Remove(CacheEnum.Key.ToString());
			}
		}
		/// <summary> 
		/// 根据键值移除Cache 
		/// </summary> 
		/// <param name="key">键</param> 
		public static void RemoveByKey(string key)
		{
			if (cache[key] != null)
				cache.Remove(key);
		}
		/// <summary> 
		/// 把对象加载到Cache 
		/// </summary> 
		/// <param name="key">键</param> 
		/// <param name="obj">对象</param> 
		public static void Insert(string key, object obj)
		{
			Insert(key, obj, null, 1);
		}
		/// <summary> 
		/// 把对象加载到Cache,附加缓存依赖信息 
		/// </summary> 
		/// <param name="key">键</param> 
		/// <param name="obj">对象</param> 
		/// <param name="dep">缓存依赖</param> 
		public static void Insert(string key, object obj, CacheDependency dep)
		{
			Insert(key, obj, dep, MinuteFactor * 3);
		}
		/// <summary> 
		/// 把对象加载到Cache,附加过期时间信息 
		/// </summary> 
		/// <param name="key">键值</param> 
		/// <param name="obj">对象</param> 
		/// <param name="seconds">缓存时间(秒)</param> 
		public static void Insert(string key, object obj, int seconds)
		{
			Insert(key, obj, null, seconds);
		}
		/// <summary> 
		/// 把对象加载到Cache,附加过期时间信息和优先级 
		/// </summary> 
		/// <param name="key">键值</param> 
		/// <param name="obj">对象</param> 
		/// <param name="seconds">缓存时间(秒)</param> 
		/// <param name="priority">优先级</param> 
		public static void Insert(string key, object obj, int seconds, CacheItemPriority priority)
		{
			Insert(key, obj, null, seconds, priority);
		}
		/// <summary> 
		/// 把对象加载到Cache,附加缓存依赖和过期时间(多少秒后过期) 
		/// (默认优先级为Normal) 
		/// </summary> 
		/// <param name="key">键值</param> 
		/// <param name="obj">对象</param> 
		/// <param name="dep">缓存依赖</param> 
		/// <param name="seconds">缓存时间(秒)</param> 
		public static void Insert(string key, object obj, CacheDependency dep, int seconds)
		{
			Insert(key, obj, dep, seconds, CacheItemPriority.Normal);
		}
		/// <summary> 
		/// 把对象加载到Cache,附加缓存依赖和过期时间(多少秒后过期)及优先级 
		/// </summary> 
		/// <param name="key">键值</param> 
		/// <param name="obj">对象</param> 
		/// <param name="dep">缓存依赖</param> 
		/// <param name="seconds">缓存时间(秒)</param> 
		/// <param name="priority">优先级</param> 
		public static void Insert(string key, object obj, CacheDependency dep, int seconds, CacheItemPriority priority)
		{
			if (obj != null)
			{
				cache.Insert(key, obj, dep, DateTime.Now.AddSeconds(Factor * seconds), TimeSpan.Zero, priority, null);
			}
		}
		/// <summary> 
		/// 把对象加到缓存并忽略优先级 
		/// </summary> 
		/// <param name="key">键值</param> 
		/// <param name="obj">对象</param> 
		/// <param name="secondFactor">时间</param> 
		public static void MicroInsert(string key, object obj, int secondFactor)
		{
			if (obj != null)
			{
				cache.Insert(key, obj, null, DateTime.Now.AddSeconds(Factor * secondFactor), TimeSpan.Zero);
			}
		}
		/// <summary> 
		/// 把对象加到缓存,并把过期时间设为最大值 
		/// </summary> 
		/// <param name="key">键值</param> 
		/// <param name="obj">对象</param> 
		public static void Max(string key, object obj)
		{
			Max(key, obj, null);
		}
		/// <summary> 
		/// 把对象加到缓存,并把过期时间设为最大值,附加缓存依赖信息 
		/// </summary> 
		/// <param name="key">键值</param> 
		/// <param name="obj">对象</param> 
		/// <param name="dep">缓存依赖</param> 
		public static void Max(string key, object obj, CacheDependency dep)
		{
			if (obj != null)
			{
				cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
			}
		}
		/// <summary> 
		/// 插入持久性缓存 
		/// </summary> 
		/// <param name="key">键值</param> 
		/// <param name="obj">对象</param> 
		public static void Permanent(string key, object obj)
		{
			Permanent(key, obj, null);
		}
		/// <summary> 
		/// 插入持久性缓存,附加缓存依赖 
		/// </summary> 
		/// <param name="key">键值</param> 
		/// <param name="obj">对象</param> 
		/// <param name="dep">缓存依赖</param> 
		public static void Permanent(string key, object obj, CacheDependency dep)
		{
			if (obj != null)
			{
				cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
			}
		}
		/// <summary> 
		/// 根据键获取被缓存的对象 
		/// </summary> 
		/// <param name="key">键值</param> 
		/// <returns></returns> 
		public static object Get(string key)
		{
			return cache[key];
		}
		/// <summary> 
		/// Return int of seconds * SecondFactor 
		/// </summary> 
		public static int SecondFactorCalculate(int seconds)
		{
			// Insert method below takes integer seconds, so we have to round any fractional values 
			return Convert.ToInt32(Math.Round((double)seconds * SecondFactor));
		}
	}

	public class AuthorizationFilter2 : FilterAttribute, IAuthorizationFilter, IExceptionFilter, IActionFilter
	{
		private readonly IProvidePermissions _permissionsProvider = UserIdentityCollection.Instance;
		private readonly ExceptionFilter _exceptionFilter = new ExceptionFilter();
		private readonly IActionFilter _executionTimingFilter = new ExecutionTimingFilterAttribute();

		/// <summary>
		/// 
		/// </summary>
		public PermissionsPoint CurrentPermissionsPoint { get; private set; }
		public List<PermissionsPoint> CurrentPerPointList { get; private set; }
		/// <summary>
		/// 将int转换为枚举名称
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string ConvertToEnumName(int id, out string description)
		{

			description = null;
			return string.Empty;
		}
		/// <summary>
		/// 权限枚举
		/// </summary>
		public static Type PowerEnumType { get; set; }

		//public AuthorizationFilter2(int id) 
		//{
		//    string description = "";
		//    string key = ConvertToEnumName(id, out description);
		//    var per = new PermissionsPoint(id.ToString(), key, description);

		//    CurrentPermissionsPoint = per;
		//    _permissionsProvider.SetCurrentPermissions(UserIdentityFactory.Instance.UserIdentity, per);
		//    _permissionsProvider = UserIdentityCollection.Instance;
		//}


		public AuthorizationFilter2(int[] ids)
		{
			string description = "";
			string key = "";
			CurrentPerPointList = new List<PermissionsPoint>();
			foreach (int id in ids)
			{
				key = ConvertToEnumName(id, out description);
				var per = new PermissionsPoint(id.ToString(), key, description);
				CurrentPerPointList.Add(per);
				CurrentPermissionsPoint = per;
			}
			_permissionsProvider.SetCurrentPermissions(UserIdentityFactory.Instance.UserIdentity, CurrentPermissionsPoint);
			_permissionsProvider = UserIdentityCollection.Instance;
		}

		/// <summary>
		/// 认证逻辑方法
		/// </summary>
		/// <param name="filterContext"></param>
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			UserIdentityFactory.Instance.AutoRedirectToLoginPage(() =>
			{
				//验证权限
				_permissionsProvider.SetCurrentPermissions(UserIdentityFactory.Instance.UserIdentity, CurrentPermissionsPoint);

				UserIdentityAuthorization2 auth = new UserIdentityAuthorization2(_permissionsProvider);
				auth.Authenticate(UserIdentityFactory.Instance.UserIdentity,
					new UserData() { Token = UserIdentityFactory.Instance.Token, RoleId = UserIdentityFactory.Instance.RoleId, RoleName = UserIdentityFactory.Instance.RoleName }
					, CurrentPerPointList);
			}, () =>
			{
				filterContext.Result = new HttpUnauthorizedResult();
			});
		}
		/// <summary>
		/// 异常处理
		/// </summary>
		/// <param name="filterContext"></param>
		public virtual void OnException(ExceptionContext filterContext)
		{
			_exceptionFilter.OnException(filterContext);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filterContext"></param>
		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			_executionTimingFilter.OnActionExecuted(filterContext);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filterContext"></param>
		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			_executionTimingFilter.OnActionExecuting(filterContext);
		}
	}


	/// <summary>
	/// 负责权限认证的类 针对多权限点
	/// </summary>
	public class UserIdentityAuthorization2
	{
		private IProvidePermissions _PermissionsProvider;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="providePermissions">存储和获取权限的提供者</param>
		public UserIdentityAuthorization2(IProvidePermissions providePermissions)
		{
			_PermissionsProvider = providePermissions;
		}


		/// <summary>
		/// 验证用户是否具有当前操作的权限
		/// </summary>
		/// <param name="userIdentityKey">用户标识</param>
		/// <param name="currentAccessPermissions"></param>
		/// <param name="userData"></param>
		public void Authenticate(string userIdentityKey, App.Framework.Web.User.UserData userData, List<PermissionsPoint> currentAccessPermissions)
		{
			if (!UserHasPermission(userIdentityKey, userData, currentAccessPermissions))
				throw new AuthorizationException(CSC.Resources.GlobalText.NoPermssion);
		}


		/// <summary>
		/// 验证用户是否具有当前操作的权限
		/// </summary>
		/// <param name="userIdentityKey"></param>
		/// <param name="currentAccessPermissions"></param>
		/// <param name="userData"></param>
		/// <returns></returns>
		public bool UserHasPermission(string userIdentityKey, App.Framework.Web.User.UserData userData, List<PermissionsPoint> currentAccessPermissions)
		{
			IList<PermissionsPoint> pointList = _PermissionsProvider.GetUserHasPermissionsPoints(userIdentityKey, userData);
			if (pointList == null)
				return false;
			var l = pointList.ToList().ToList(m => m.PermissionsId);
			var s = l.ToString(",");

			foreach (PermissionsPoint p1 in currentAccessPermissions)
			{
				var searchResult = pointList.Where(p =>
					(!string.IsNullOrEmpty(p1.Key) && p.Key == p1.Key)
					|| (!string.IsNullOrEmpty(p1.PermissionsId) && p.PermissionsId == p1.PermissionsId));
				if (searchResult.Count() > 0)
					return true;
			}
			return false;
		}
	}

}