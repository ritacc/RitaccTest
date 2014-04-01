//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：Util.cs
//文件功能：分页中需要用到的获取按钮URL
//
//创建标识：鲜红 || 2011-03-24
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;

namespace App.Framework.Web.Pager
{
    /// <summary>
    /// 分页中需要用到的获取按钮URL
    /// </summary>
    public static class Util
    {

        /// <summary>
        /// 获取当前上下文中的URL
        /// </summary>
        /// <param name="context"></param>
        /// <param name="pName"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static string GetUrl(RequestContext context, string pName, object pValue)
        {
            return GetUrl(context, new NameValueCollection() { {pName,pValue.ToString()} });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="setQueryList"></param>
        /// <returns></returns>
        public static string GetUrl(RequestContext context, NameValueCollection setQueryList)
        {
            if (context == null) throw new ArgumentNullException("context");
            string url = "";
            var routeValues = GetRouteValueDictionary(context.HttpContext, null);
            url = new UrlHelper(context).Action(routeValues["action"].ToString(), routeValues["controller"].ToString());
            url += "?";
            NameValueCollection queryList = context.HttpContext.Request.QueryString;

            //循环获取参数
            NameValueCollection newQueryList = new NameValueCollection();
            foreach (string key in queryList.Keys)
            {
                if (setQueryList[key] == null)
                    setQueryList.Add(key, queryList[key]);
            }

            foreach (string key in setQueryList.Keys)
            {
                string value = setQueryList[key];
                if (!string.IsNullOrEmpty(value))
                    url += key + "=" + value + "&";
            }
            url = url.Substring(0, url.Length - 1);
            return url;
        }


        /// <summary>
        /// 从context或者从路由表中获取值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static RouteValueDictionary GetRouteValueDictionary(HttpContextBase context, RouteValueDictionary routeValues)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (routeValues == null) routeValues = new RouteValueDictionary();

            var rq = context.Request.QueryString;

            RouteValueDictionary routeData = RouteTable.Routes.GetRouteData(context).Values;

            foreach (string key in rq)
            {
                if (key == null) continue;

                routeData[key] = rq[key];
            }

            foreach (string key in routeValues.Keys)
            {
                if (key == null) continue;

                routeData[key] = routeValues[key];
            }

            return routeData;
        }
    }
}
