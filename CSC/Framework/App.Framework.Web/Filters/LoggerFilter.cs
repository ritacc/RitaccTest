//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：LoggerFilter.cs
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
using System.Web.Mvc;
using System.Security.Principal;
using System.Reflection;
using App.Framework.Web.Logger;

namespace App.Framework.Web.Filters
{
    /// <summary>
    /// 日志记录过滤器
    /// </summary>
    public class LoggerFilter : FilterAttribute, IActionFilter
    {
      
        private readonly Dictionary<Type, object> _defaultLoggerDic = new Dictionary<Type, object>();
        private readonly object _logger;
        private readonly Type _loggerType;
        private readonly LogEntity _logEntity;
        private readonly Type _entityType;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entityType">日志实体类型</param>
        /// <param name="loggerType">记录日志实类型,需继承自App.Framework.Web.Logger.ILogger</param>
        /// <param name="level"></param>
        public LoggerFilter(Type entityType, Type loggerType, int permissionsId = -1, int level=0,int eventType =0)
        {
            _entityType = entityType;
            _logger = Activator.CreateInstance(loggerType);
            _loggerType = loggerType;
            _logEntity = new LogEntity()
            {
                Level = level,
                EventType = eventType,
                PermissionsId = permissionsId
            };
        }

       
        /// <summary>
        /// 日志记录逻辑
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _logEntity.LogTime = DateTime.Now;
            IPrincipal user = filterContext.Controller.ControllerContext.HttpContext.User;
            _logEntity.Opreater = user.Identity.Name;
            _logEntity.IP = filterContext.Controller.ControllerContext.HttpContext.Request.UserHostAddress;

            object defaultLogger = null;
            //Activator.CreateInstance(typeof (IConverEntity<>).MakeGenericType(_entityType),;
            if (!_defaultLoggerDic.ContainsKey(_loggerType))
                defaultLogger = Activator.CreateInstance(typeof(DefaultLogger<>).MakeGenericType(_entityType), new object[] { _logger, _logger });
            else
                defaultLogger = _defaultLoggerDic[_loggerType];
            
            Type type = defaultLogger.GetType();
            MethodInfo mi = type.GetMethod("RecordLog");
            mi.Invoke(defaultLogger, new object[] { _logEntity, filterContext.RequestContext });
        }


        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
             
        }
    }
}
