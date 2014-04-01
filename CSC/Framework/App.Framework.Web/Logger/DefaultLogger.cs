//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：DefaultLogger.cs
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
    /// 默认的日志记录类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultLogger<T>
    {

        private ILogger<T> _loger;
        private IConverEntity<T> _converter;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="loger">记录日志提供者</param>
        /// <param name="converter">转转日志实体的提供者</param>
        public DefaultLogger(object loger, object converter)
        {
            _loger = (ILogger<T>)loger;
            _converter = (IConverEntity<T>)converter;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log">日志实体</param>
        public void RecordLog(LogEntity log,RequestContext request)
        {
            _loger.Record(_converter.ConvertToTCallBack(log), request);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log">自定义的日志实体</param>
        public void Record(T log, RequestContext request)
        {
            _loger.Record(log, request);
        }
    }
}
