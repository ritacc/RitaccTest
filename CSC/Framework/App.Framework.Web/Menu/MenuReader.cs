//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：MenuReader.cs
//文件功能：
//
//创建标识：鲜红 || 2011-04-06
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace App.Framework.Web.Menu
{
    /// <summary>
    /// XmlAttribute扩展
    /// </summary>
    public static class XmlAttributeHelper
    {
        /// <summary>
        /// 获取XmlAttribute的Bool值
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static bool TryGetBoolOrDefault(this XmlAttribute attr)
        {
            if (attr == null || string.IsNullOrEmpty(attr.Value)) return false;
            return Convert.ToBoolean(attr.Value);
        }

        /// <summary>
        /// 获取XmlAttribute的String值
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static string TryGetString(this XmlAttribute attr)
        {
            if (attr == null) return default(string);
            return attr.Value;
        }

        public static string TryGetString(this XAttribute attr)
        {
            if (attr == null) return default(string);
            return attr.Value;
        }
    }

    /// <summary>
    /// 从配置文件获取权限，菜单，模块信息
    /// </summary>
    public static class MenuReader
    {
        #region 私有属性
        private static string _MenuConfigPath = ConfigHelper.MenuConfigPath;
        private static string _PermissionConfigPath = ConfigHelper.PermissionConfigPath;
        private static bool _EnableMultiLanguage = true;
        private static Func<string, string> _GetCaptionCall;
        #endregion

        #region 私有方法
        private static Permission GetPermissionsByXmlAttr(XElement pnode)
        {
            return new Permission()
            {
                Key = pnode.Attribute("Key").TryGetString(),
                Description = pnode.Attribute("Description").TryGetString(),
            };
        }

        private static string TryGetCaption(string caption)
        {
            if (!_EnableMultiLanguage)
                return caption;
            string result = _GetCaptionCall(caption);
            if (string.IsNullOrEmpty(result))
                return caption;
            return result;
        }

        private static Module GetModuleByXmlAttr(XElement node)
        {
            string resource = node.Attribute("Resource").TryGetString();
            return new Module()
            {
                MoudleCaption = string.IsNullOrEmpty(resource) ? node.Attribute("Caption").TryGetString() : TryGetCaption(resource),
                Key = node.Attribute("Key").TryGetString(),
                Href = node.Attribute("Href").TryGetString(),
                CssClass = node.Attribute("CssClass").TryGetString()
            };
        }

        private static Menu GetMenuByXmlAttr(XElement menunode)
        {
            string resource = menunode.Attribute("Resource").TryGetString();
            return new Menu()
            {
                MenuTitle = string.IsNullOrEmpty(resource) ? menunode.Attribute("Caption").TryGetString() : TryGetCaption(resource),
                CssClass = menunode.Attribute("CssClass").TryGetString(),
                Href = WebHelper.GetContentUrl(menunode.Attribute("Href").TryGetString())
            };
        }

        /// <summary>
        /// 合并菜单和权限的XML文件
        /// </summary>
        /// <returns></returns>
        private static XElement MergeMenuAndPermission()
        {
            IEnumerable<XElement> menuPermissions = from m in XElement.Load(_PermissionConfigPath).Descendants("Permissions")
                                                    select m;

            var menuDoc = XElement.Load(_MenuConfigPath);
            IEnumerable<XElement> xmlobj = from m in menuDoc.Descendants("Menu")
                                           where menuPermissions.Contains(m, new ComparerMenuKey())
                                           select m;

            var menus = xmlobj.ToList();
            List<string> moduleKeys = new List<string>();

            foreach (XElement m in menus)
            {
                var pers = menuPermissions.Where(p => p.Attribute("MenuKey").Value == m.Attribute("Key").Value).Descendants();
                m.ReplaceNodes(pers);
            }
            return menuDoc;
        }

        /// <summary>
        /// 递归获取菜单
        /// </summary>
        /// <param name="pnode"></param>
        /// <param name="menu"></param>
        private static void GetMenu(XElement pnode, Menu menu)
        {
            foreach (var node in pnode.Elements())
            {
                if (string.Equals(node.Name.LocalName, "Menu", StringComparison.OrdinalIgnoreCase))
                {
                    Menu tmpmenu = GetMenuByXmlAttr(node);
                    menu.ChildNodes.Add(tmpmenu);
                    GetMenu(node, tmpmenu);
                }
                else if (string.Equals(node.Name.LocalName, "Permission", StringComparison.OrdinalIgnoreCase))
                {
                    menu.Permissions.Add(GetPermissionsByXmlAttr(node));
                }
            }
        }



        #endregion

        #region 公共方法
        /// <summary>
        /// 从XML文件里读取所有的权限，模块
        /// </summary>
        /// <returns></returns>
        public static List<Module> Read(Func<string, string> getCaptionCall)
        {
            _GetCaptionCall = getCaptionCall;

            var xDoc = MergeMenuAndPermission();
            List<Module> moduleList = new List<Module>();

            foreach (var node in xDoc.Elements())
            {
                Module module = GetModuleByXmlAttr(node);

                foreach (var menunode in node.Elements())
                {
                    Menu innerMenu = GetMenuByXmlAttr(menunode);
                    module.Menus.Add(innerMenu);
                    GetMenu(menunode, innerMenu);
                }

                module.BindParent();
                moduleList.Add(module);
            }


            return moduleList;
        }



        /// <summary>
        /// 反向获取当前用户权限对应的模块
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static List<Module> ReadMenuByPermissions(List<string> keys, Func<string, string> getCaptionCall)
        {
            List<Module> moduleList = Read(getCaptionCall);

            List<Permission> allPermissions = MenuHelper.GetAllPermissions(moduleList);
            List<Permission> currentUserHasPermissions = (from p in allPermissions
                                                          where keys.Contains(p.Key)
                                                          orderby p.Key ascending
                                                          select p).ToList();

            foreach (Permission p in currentUserHasPermissions)
                p.Visible = true;
            moduleList.ForEach(m =>
            {
                m.Href = WebHelper.GetContentUrl(m.Href);
            });
            return moduleList;

        }

        #endregion

    }

    public class ComparerMenuKey : IEqualityComparer<XElement>
    {

        public bool Equals(XElement x, XElement y)
        {
            return x.Attribute("MenuKey").TryGetString() == y.Attribute("Key").TryGetString();
        }

        public int GetHashCode(XElement obj)
        {
            return obj.GetHashCode();
        }
    }
}
