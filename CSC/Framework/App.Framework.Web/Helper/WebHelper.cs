using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace App.Framework.Web
{
    /// <summary>
    /// Web页面表现 公共方法类
    /// </summary>
    public static class WebHelper
    {
       
        #region 写登录 Cookie
        /// <summary>
        /// 写登录 Cookie 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lockCurrentDomain"></param>
        public static void SetUserLoginCookie(string name, bool lockCurrentDomain)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(name, true, Convert.ToInt32(FormsAuthentication.Timeout.TotalMinutes));
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName);

            string domain = FormsAuthentication.CookieDomain;
            string encrypt = FormsAuthentication.Encrypt(ticket);

            if (lockCurrentDomain) domain = HttpContext.Current.Request.Url.DnsSafeHost;

            cookie.Domain = domain;
            cookie.Expires = DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes);
            cookie.Value = encrypt;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        #endregion

        #region 添加自定义 Cookie
        /// <summary>
        /// 添加自定义 Cookie 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="lockCurrentDomain"></param>
        public static void AddCustomCookie(string name, string value, bool lockCurrentDomain)
        {
            HttpCookie cookie = new HttpCookie(name);

            string domain = FormsAuthentication.CookieDomain;

            if (lockCurrentDomain) domain = HttpContext.Current.Request.Url.DnsSafeHost;
			cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Domain = domain;
			cookie.Expires = DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes);
            cookie.Value = value;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        #endregion

        #region 删除自定义 Cookie
        /// <summary>
        /// 删除自定义 Cookie 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lockCurrentDomain"></param>
        public static void DeleteCustomCookie(string name, bool lockCurrentDomain)
        {
            HttpCookie cookie = new HttpCookie(name);

            string domain = FormsAuthentication.CookieDomain;

            if (lockCurrentDomain) domain = HttpContext.Current.Request.Url.DnsSafeHost;

            cookie.Domain = domain;
            cookie.Expires = DateTime.Now.AddDays(-10);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        #endregion

        #region  根据枚举获取下拉列表项
        /// <summary>
        /// 根据枚举获取下拉列表项 
        /// </summary>
        /// <param name="type"></param>
        public static List<SelectListItem> GetItemByEnum(Type type)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            FieldInfo[] fields = type.GetFields();

            //循环加入集合
            /* 因获取的 fields[0] 值始终为"value__"，所以必须排除 */
            for (int i = 1; i < fields.Length; i++)         
            {
                SelectListItem item = new SelectListItem();
                item.Value = ((int)fields[i].GetValue(type)).ToString();
                item.Text = EnumHelper.GetDescription(fields[i]);

                items.Add(item);
            }

            return items;
        }
        #endregion

        #region 根据集合获取下拉列表项
        /// <summary>
        /// 将对象集合转为下拉列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <param name="text"></param>
        public static List<SelectListItem> GetItemValueByList<T>(List<T> list, string value, string text)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            value = value.Replace("[", string.Empty).Replace("]", string.Empty);
            text = text.Replace("[", string.Empty).Replace("]", string.Empty);

            //循环加入集合
            foreach (T t in list)
            {
                SelectListItem item = new SelectListItem();
                item.Value = t.GetType().GetProperty(value).GetValue(t, null).ToString();
                item.Text = t.GetType().GetProperty(text).GetValue(t, null).ToString();

                items.Add(item);
            }

            return items;
        }
        #endregion

        #region 页面交互脚本输出
        /// <summary>
        /// 输出头部
        /// </summary>
        private static void Header()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write("<script language=\"javascript\">");
        }

        /// <summary>
        /// 输出底部
        /// </summary>
        /// <param name="message"></param>
        private static void Footer(string message)
        {
            HttpContext.Current.Response.Write("</script>");
            NoScript(message);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 不支持客户端脚本时输出普通信息
        /// </summary>
        /// <param name="message"></param>
        private static void NoScript(string message)
        {
            if (string.IsNullOrEmpty(message)) { message = "Javascript Error..."; }

            HttpContext.Current.Response.Write("\r\n\r\n");
            HttpContext.Current.Response.Write("<noscript>");
            HttpContext.Current.Response.Write(message);
            HttpContext.Current.Response.Write("</noscript>");
        }

        /// <summary>
        /// 弹出窗口
        /// </summary>
        /// <param name="message"></param>
        public static void Alert(string message)
        {
            WebHelper.Header();
            HttpContext.Current.Response.Write("window.alert('" + CharacterHelper.StrReplace(message) + "');");
            WebHelper.Footer(message);
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        public static void Refresh()
        {
            WebHelper.Header();
            HttpContext.Current.Response.Write("self.location.href = self.location.href;");
            WebHelper.Footer(null);
        }

        /// <summary>
        /// 弹出窗口并刷新页面
        /// </summary>
        /// <param name="message"></param>
        public static void AlertAndRefresh(string message)
        {
            WebHelper.Header();
            HttpContext.Current.Response.Write("window.alert('" + CharacterHelper.StrReplace(message) + "'); self.location.href = self.location.href;");
            WebHelper.Footer(message);
        }

        /// <summary>
        /// 弹出窗口并后退
        /// </summary>
        /// <param name="message"></param>
        public static void AlertAndBack(string message)
        {
            WebHelper.Header();
            HttpContext.Current.Response.Write("window.alert('" + CharacterHelper.StrReplace(message) + "'); self.history.go(-1);");
            WebHelper.Footer(message);
        }

        /// <summary>
        /// 弹出窗口并重定向
        /// </summary>
        /// <param name="message"></param>
        /// <param name="url"></param>
        public static void AlertAndRedirect(string message, string url)
        {
            WebHelper.Header();
            HttpContext.Current.Response.Write("window.alert('" + CharacterHelper.StrReplace(message) + "'); self.location.href = '" + url + "';");
            WebHelper.Footer(message);
        }

        /// <summary>
        /// 弹出窗口并重定向
        /// </summary>
        /// <param name="message"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        public static void AlertAndRedirect(string message, string controller, string action)
        {
            WebHelper.Header();
            HttpContext.Current.Response.Write("window.alert('" + CharacterHelper.StrReplace(message) + "'); self.location.href = '/" + controller + "/" + action + "';");
            WebHelper.Footer(message);
        }

        /// <summary>
        /// 弹出窗口然后关闭
        /// </summary>
        /// <param name="message"></param>
        public static void AlertAndClose(string message)
        {
            WebHelper.Header();
            HttpContext.Current.Response.Write("window.alert('" + CharacterHelper.StrReplace(message) + "'); window.opener = null; window.close();");
            WebHelper.Footer(message);
        }

        /// <summary>
        /// 执行任意 Javascript 脚本
        /// </summary>
        /// <param name="message"></param>
        public static void ExecuteScript(string message)
        {
            WebHelper.Header();
            HttpContext.Current.Response.Write(message);
            WebHelper.Footer(null);
        } 
        #endregion

        public static string GetContentUrl(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            return System.Web.Mvc.UrlHelper.GenerateContentUrl(path, System.Web.HttpContext.Current.Request.RequestContext.HttpContext);
        }
    }
}