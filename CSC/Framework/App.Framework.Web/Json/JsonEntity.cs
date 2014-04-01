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

namespace App.Framework.Web.Json
{
    /// <summary>
    /// Json实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonEntity<T>
    {
		/// <summary>
		/// Json
		/// </summary>
		/// <param name="t"></param>
		/// <param name="success"></param>
        public  JsonEntity(T t,bool success)
        {
            Result = t;
            if (typeof(T) == typeof(string))
                Message = t as string;
            Success = success;
        }

		/// <summary>
		/// 构造
		/// </summary>
        public JsonEntity()
        { 
            
        }

		public bool Logged { get; set; }
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回JsonResult
        /// </summary>
        /// <returns></returns>
        public JsonResult ToJson()
        {
            JsonResult result = new JsonResult();
            result.Data = this;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
    }


}
