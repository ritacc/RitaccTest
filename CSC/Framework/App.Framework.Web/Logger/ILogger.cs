//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：ILogger.cs
//文件功能：
//
//创建标识：鲜红 || 2011-03-25
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace App.Framework.Web.Logger
{
    /// <summary>
    /// 日志记录接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILogger<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        void Record(T log, RequestContext request);
    }
}
