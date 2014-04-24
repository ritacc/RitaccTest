using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using App.Framework.Web.Permissions;
using System.Web.Script.Serialization;

namespace App.Framework.Web.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// RoleId
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// RoleName
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// OtherInfo
        /// </summary>
        public string OtherInfo { get; set; }
    }

    /// <summary>
    /// 用户身份Cookies实现
    /// </summary>
    partial class UserIdentityFormCookies : IGetUserIdentity
    {
        /// <summary>
        /// UserIdentityFormCookies
        /// </summary>
        private UserIdentityFormCookies() { }

        /// <summary>
        /// UserIdentityFormCookies
        /// </summary>
        private static readonly UserIdentityFormCookies _Instance = new UserIdentityFormCookies();

        /// <summary>
        /// JavaScriptSerializer
        /// </summary>
        private JavaScriptSerializer _JavaScriptSerializer = new JavaScriptSerializer();

        #region private method
        /// <summary>
        /// GetUserData
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        private string GetUserData(string cookieName)
        {
            HttpCookie curCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (curCookie == null || string.IsNullOrEmpty(curCookie.Value)) return null;

            FormsAuthenticationTicket tickets = FormsAuthentication.Decrypt(curCookie.Value);
            if (string.IsNullOrEmpty(tickets.UserData)) return string.Empty;

            UserData ud = _JavaScriptSerializer.Deserialize<UserData>(tickets.UserData);

            switch (cookieName.ToLower())
            {
                case "username":
                    return ud.UserName;

                case "token":
                    return ud.Token;

                case "otherinfo":
                    return ud.OtherInfo;

                case "roleid":
                    return ud.RoleId;

                case "rolename":
                    return ud.RoleName;
            }

            return string.Empty;
        }

        /// <summary>
        /// SetAuthCookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lockCurrentDomain"></param>
        /// <param name="userData"></param>
        private void SetAuthCookie(string name, bool lockCurrentDomain, string userData)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, name, DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), true, userData);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName);

            string domain = FormsAuthentication.CookieDomain;
            string encrypt = FormsAuthentication.Encrypt(ticket);

            if (lockCurrentDomain) domain = HttpContext.Current.Request.Url.DnsSafeHost;

            cookie.Domain = domain;
            cookie.Expires = ticket.Expiration;
            cookie.Value = encrypt;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        #endregion

        /// <summary>
        /// 获取单例
        /// </summary>
        public static UserIdentityFormCookies Instance
        {
            get
            {
                return _Instance;
            }
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get { return GetUserData("RoleId"); } }

        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get { return GetUserData("RoleName"); } }


        public string UserDescription { get { return GetUserData("UserDescription"); } }

        /// <summary>
        /// 用户身份标识
        /// </summary>
        public string UserIdentity
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        /// <summary>
        /// 是否认证通过
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get
            {
                return GetUserData("UserName");
            }

        }

        /// <summary>
        /// 其它信息
        /// </summary>
        public string OtherInfo
        {
            get
            {
                return GetUserData("OtherInfo");
            }

        }

        /// <summary>
        /// 验证签名
        /// </summary>
        public string Token
        {
            get
            {
                return GetUserData("Token");
            }
        }

        /// <summary>
        /// 设置登录成功
        /// </summary>
        /// <param name="userIndentityKey"></param>
        /// <param name="userName"></param>
        /// <param name="token"></param>
        /// <param name="otherInfo"></param>
        public void SetAuth(string userIndentityKey, string userName, string token, string roleId, string roleName, string otherInfo)
        {
            UserData ud = new UserData()
            {
                UserName = userName,
                Token = token,
                OtherInfo = otherInfo,
                RoleId = roleId,
                RoleName = roleName
            };
            string serializeStr = _JavaScriptSerializer.Serialize(ud);
            SetAuthCookie(userIndentityKey, false, serializeStr);
        }

        /// <summary>
        /// 跳转自登录页面
        /// </summary>
        public void RedirectToLoginPage()
        {
            string loginPage = FormsAuthentication.LoginUrl;
            if (!loginPage.StartsWith("~"))
                HttpContext.Current.Response.Redirect(System.Web.Mvc.UrlHelper.GenerateContentUrl("~" + FormsAuthentication.LoginUrl, HttpContext.Current.Request.RequestContext.HttpContext) + "?ReturnUrl1=" + HttpContext.Current.Request.RawUrl);
            else
                FormsAuthentication.RedirectToLoginPage();
        }




        /// <summary>
        /// 判断用户是否登录，若未登录则自动跳转,如果已经登录则执行action
        /// </summary>
        public void AutoRedirectToLoginPage(Action successAction, Action failureAction)
        {
            if (!IsAuthenticated || string.IsNullOrEmpty(UserIdentity))
            {
                //RedirectToLoginPage();
                failureAction();
                //return;
                throw new AuthorizationException(Resources.Messages.UserHasNotLogged, false);
            }

            successAction();
        }
    }
}
