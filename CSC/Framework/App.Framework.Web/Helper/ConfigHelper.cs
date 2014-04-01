using System;
using System.Configuration;

namespace App.Framework.Web
{
    /// <summary>
    /// 配置公共方法类
    /// </summary>
    public static class ConfigHelper
    {

        public static bool EnableReportingService
        {
            get
            {
                return ConfigurationManager.AppSettings["EnableReportingService"] == "1";
            }
        }


        public static string DateTimeFormat
        {
            get
            {
                return ConfigurationManager.AppSettings["DateTimeFormat"];
            }
        }

        public static string DateAndHHmmFormat
        {
            get { return ConfigurationManager.AppSettings["DateAndHHmmFormat"]; }
        }

        public static string DateAndTimeFormat
        {
            get
            {
                return ConfigurationManager.AppSettings["DateAndTimeFormat"];
            }
        }


        /// <summary>
        /// 公共分页记录数
        /// </summary>
        public static int AdminShowPageSize
        {
            get
            {
                var pagesize = ConfigurationManager.AppSettings["PageSize"];
                if (pagesize == null)
                    throw new InvalidCastException("请先配置PageSize");
                return Convert.ToInt32(pagesize);
            }
        }

        /// <summary>
        /// 菜单配置文件地址
        /// </summary>
        public static string MenuConfigPath
        {
            get
            {
                return System.Web.HttpContext.Current.Server.MapPath("~\\menuConfig.xml");
            }
        }

        /// <summary>
        /// 权限配置文件地址
        /// </summary>
        public static string PermissionConfigPath
        {
            get
            {
                return System.Web.HttpContext.Current.Server.MapPath("~\\Permissions.xml");
            }
        }




    }
}
