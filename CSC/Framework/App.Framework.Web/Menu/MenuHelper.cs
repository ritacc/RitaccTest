//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：MenuHelper.cs
//文件功能：
//
//创建标识：鲜红 || 2011-03-31
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using App.Framework.Web.Permissions;
using App.Framework.Web.User;

namespace App.Framework.Web.Menu
{
    /// <summary>
    /// 扩展HtmlHelper
    /// </summary>
    public static class MenuHelper
    {
        public static IGenerateMenu MenuGenerate = new MenuGenerate();
        public static Func<string, string> GetMenuCaptionCall { get; set; }

        #region 私有方法
        /// <summary>
        /// 根据当前权限获取菜单
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        internal static Module GetModuleByMenu(Menu m)
        {

            if (m.Parent != null)
                return m.Parent;
            if (m.ParentMenu != null)
                return GetModuleByMenu(m.ParentMenu);
            return null;
        }

        /// <summary>
        /// 获取权限点
        /// </summary>
        /// <param name="m"></param>
        /// <param name="pList"></param>
        private static void GetPermissions(Menu m, List<Permission> pList)
        {
            if (m.ChildNodes != null)
                foreach (Menu menu in m.ChildNodes)
                    GetPermissions(menu, pList);
            pList.AddRange(m.Permissions);

        }

        /// <summary>
        /// 获取所有权限点
        /// </summary>
        /// <param name="mList"></param>
        /// <returns></returns>
        internal static List<Permission> GetAllPermissions(List<Module> mList)
        {
            List<Permission> allMenus = new List<Permission>();
            foreach (Module m in mList)
                foreach (Menu menu in m.Menus)
                    GetPermissions(menu, allMenus);
            return allMenus;
        }

        /// <summary>
        /// 设置菜单是否显示
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="mList"></param>
        public static void SetMenuVisible(List<string> keys, List<Module> mList)
        {
            List<Permission> allMenus = GetAllPermissions(mList);
            var pers = from p in allMenus
                       where keys.Contains(p.Key)
                       select p;
            List<Permission> permissions = pers.ToList();
            foreach (Permission p in permissions) p.Visible = true;

        }

        /// <summary>
        /// 获取用户缓存过的权限
        /// </summary>
        /// <param name="userIdentKey"></param>
        /// <returns></returns>
        private static List<Permission> GetUserSavedPermissions(string userIdentKey,User.UserData data)
        {
            List<Permission> pList = new List<Permission>();
            IList<PermissionsPoint> perList = PermissionsProviderFactory.ProvidePermissions.GetUserHasPermissionsPoints(userIdentKey,data);
            if (perList == null) return null;
            foreach (PermissionsPoint p in perList)
            {
                pList.Add(new Permission()
                {
                    Key = p.Key,
                    Description = p.Description
                });
            }
            return pList.OrderBy(p => p.Key).ToList();
        }

        /// <summary>
        /// 获取用户模块
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        private static List<Module> GetUserModules(HtmlHelper helper)
        {
            List<Permission> perList = GetUserSavedPermissions(UserIdentityFactory.Instance.UserIdentity, new UserData() { RoleId = UserIdentityFactory.Instance.RoleId, RoleName = UserIdentityFactory.Instance.RoleName,Token = UserIdentityFactory.Instance.Token });
            if (perList == null || perList.Count <=0) return null;
            List<string> keys = new List<string>();
            foreach (Permission p in perList)
                keys.Add(p.Key);
            List<Module> userModules = MenuReader.ReadMenuByPermissions(keys, GetMenuCaptionCall);

            //设置当前模块
            Permission currentPermissions = GetCurrentPermissions(userModules);
            if (currentPermissions != null)
                currentPermissions.Parent.IsCurrent = true;
            return userModules;
        }

        /// <summary>
        /// 获得当前权限点
        /// </summary>
        /// <param name="userModules"></param>
        /// <returns></returns>
        private static Permission GetCurrentPermissions(List<Module> userModules)
        {
            var currentPermissions = UserIdentityCollection.Instance.GetCurrentPermissions(UserIdentityFactory.Instance.UserIdentity);
            if (currentPermissions == null) return null;
            string currentKey = currentPermissions.Key;
            var allPermissions = GetAllPermissions(userModules);
            var currentPer = from p in allPermissions
                             where p.Key == currentKey
                             select p;
            return currentPer.FirstOrDefault();

        }

        #endregion

        #region 生成模块和菜单，辅助HtmlHelper
        /// <summary>
        /// 生成模块HTML代码
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string Moudle(this HtmlHelper helper)
        {
            if (!UserIdentityFactory.Instance.IsAuthenticated)
                return string.Empty;
            return MenuGenerate.GenerateMoudle(GetUserModules(helper));
        }
        /// <summary>
        /// 生成菜单HTML代码
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString Menu(this HtmlHelper helper)
        {
            if (!UserIdentityFactory.Instance.IsAuthenticated)
                return MvcHtmlString.Empty;
            List<Module> mList = GetUserModules(helper);
            if (mList == null || mList.Count <= 0)
                return MvcHtmlString.Create(string.Empty);
            return MvcHtmlString.Create(MenuGenerate.GenerateMenu(Module.Current));
        }
        #endregion
    }
}
