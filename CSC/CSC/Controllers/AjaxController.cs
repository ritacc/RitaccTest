using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using App.Framework.Web;
using App.Framework.Web.Filters;
using System.IO;
using System.Web.Routing;
namespace CSC.Controllers
{
	public class AjaxController : Controller
	{

		public ActionResult GetCurrentUserHasPermission(string url)
		{
			try
			{
				if (url.ToLower().IndexOf("workplace") >= 0)
				{
					throw new NotImplementedException();
				}
				string controller = string.Empty;
				string action = string.Empty;
				HttpRequest hr = new HttpRequest("", url, "");
				TextWriter stringWriter = new StringWriter();
				HttpResponse hrs = new HttpResponse(stringWriter);
				HttpContext hc = new HttpContext(hr, hrs);
				HttpContextWrapper hcw = new HttpContextWrapper(hc);

				foreach (Route r in System.Web.Routing.RouteTable.Routes)
				{
					RouteData rt = r.GetRouteData(hcw);
					if (rt != null)
					{
						controller = rt.Values["Controller"].ToString();
						action = rt.Values["Action"].ToString();
						break;
					}
				}

				controller += "Controller";
				Type type = Type.GetType("CSC.Controllers." + controller);
				bool hasP = true;
				var methods = type.GetMethods();
				for (int i = 0; i < methods.Length; i++)
				{
					var me = methods[i];
					if (me.Name == action && me.IsPublic)
					{
						var attr = me.GetCustomAttributes(typeof(AuthorizationFilter), true);
						if (attr.Length > 0)
						{
							var au = (AuthorizationFilter)attr[0];
							int pId = int.Parse(au.CurrentPermissionsPoint.PermissionsId);
							hasP = hasP && QuickInvoke.GetCurrentUserHasPermission(pId);
						}
					}
				}

				return hasP.ToJsonEntity();
			}
			catch
			{
				return true.ToJsonEntity();
			}
		}





		// 根据 Resource 资源名称, 得到 Json 对象
		// className: 资源文件类名
		public JsonResult ResourceText(string className)
		{
			Dictionary<string, string> dictJson = new Dictionary<string, string>();
			string assembly = string.Format("CSC.Resources.{0}", className);
			Type type = Type.GetType(assembly, false);
			if (type != null)
			{
				foreach (PropertyInfo pi in type.GetProperties())
				{
					if (pi.PropertyType != typeof(string))
					{
						continue;
					}
					dictJson.Add(pi.Name, pi.GetValue(null, null).ToString());
				}
			}
			return this.Json(dictJson, JsonRequestBehavior.AllowGet);
		}

		public static MvcHtmlString GetResourceText(string className)
		{
			Dictionary<string, string> dictJson = new Dictionary<string, string>();
			string assembly = string.Format("CSC.Resources.{0}", className);
			Type type = Type.GetType(assembly, false);
			if (type != null)
			{
				foreach (PropertyInfo pi in type.GetProperties())
				{
					if (pi.PropertyType != typeof(string))
					{
						continue;
					}
					dictJson.Add(pi.Name, pi.GetValue(null, null).ToString());
				}
			}
			return MvcHtmlString.Create(dictJson.ToJSON2());
		}

	}
}
