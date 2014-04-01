//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：JsonHelper.cs
//文件功能：Json辅助
//
//创建标识：鲜红 || 2011-04-25
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using App.Framework.Web.Json;

namespace App.Framework.Web
{
    /// <summary>
    /// Json辅助
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 返回JsonEntity
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static JsonResult ToJsonEntity(this object obj)
        {
			return obj.ToJsonEntity<object>();
        }

        /// <summary>
        /// 返回JsonEntity
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="success"></param>
        /// <returns></returns>
        public static JsonResult ToJsonEntity(this object obj, bool success)
        {
			return obj.ToJsonEntity<object>(success);
        }

        /// <summary>
        /// 返回JsonEntity
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static JsonResult ToJsonEntity(this object obj, bool success, string msg)
        {
			return obj.ToJsonEntity<object>(success, msg);
        }

        /// <summary>
        /// 返回JsonEntity
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static JsonResult ToJsonEntity<T>(this T obj)
        {
			return obj.ToJsonEntity<T>(true,null,true);
        }

        /// <summary>
        /// 返回JsonEntity
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="success">对象</param>
        /// <returns></returns>
        public static JsonResult ToJsonEntity<T>(this T obj, bool success)
        {
			return obj.ToJsonEntity<T>(success, null);
        }


        /// <summary>
        /// 返回JsonEntity
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="success">是否成功</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static JsonResult ToJsonEntity<T>(this T obj, bool success, string msg)
        {
			return obj.ToJsonEntity<T>(success,msg,true);
        }

		/// <summary>
		/// JsonEntity
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="success"></param>
		/// <param name="msg"></param>
		/// <param name="logged"></param>
		/// <returns></returns>
		public static JsonResult ToJsonEntity<T>(this T obj, bool success, string msg,bool logged)
		{
			if (obj == null) return null;
			return new JsonEntity<T>()
			{
				Result = obj,
				Success = success,
				Message = msg,
				Logged = logged
			}.ToJson();
		}
    }
}
