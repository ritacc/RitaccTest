using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using App.Framework.Web.Permissions;
using CSC.Lib;
using System.Globalization;
using System.Threading;
using App.Framework.Web;
using CSC.Business;
using App.Framework;

namespace CSC
{
	// 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
	// 请访问 http://go.microsoft.com/?LinkId=9394801


	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{ 
			routes.IgnoreRoute("{filename}.html");
			routes.IgnoreRoute("{*alljs}", new { alljs = @".*\.html(/.*)?" });
            routes.IgnoreRoute("{*allhtm}", new { allhtm = @".*\.htm(/.*)?" });
       
			routes.MapRoute(
				"Default", // 路由名称
				"{controller}/{action}/{id}", // 带有参数的 URL
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
			);
		}


		void Session_Start(object sender, EventArgs e)
		{
			string sid = Session.SessionID;
			Session.Timeout = 60;
		}

		void Session_End(object sender, EventArgs e)
		{
			Application.Lock();

			Application.UnLock();
		} 


		protected void Application_AcquireRequestState(object sender, EventArgs e)
		{
			LocalizationManger.EnableMultiLanguage();
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);
			//绑定权限获取方式
			PermissionsProviderFactory.ProvidePermissions.PermissionsProvider = GetPermissionsFromDb.Instance;
			App.Framework.Web.Menu.MenuHelper.MenuGenerate = JsMenuClassGenerate.Instance;
			App.Framework.Web.Menu.MenuHelper.GetMenuCaptionCall = caption => Resources.Menu.ResourceManager.GetString(caption);
			App.Framework.BusinessPortal.CacheProvider = SessionCache.Instance;
			
		}
	}
}
