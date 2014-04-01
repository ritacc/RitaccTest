//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：ExceptionFilter.cs
//文件功能：
//
//创建标识：鲜红 || 2011-04-16
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using App.Framework.Web.User;
using System.ServiceModel;
using System.Configuration;

namespace App.Framework.Web.Filters
{
	/// <summary>
	/// ActionResult类型
	/// </summary>
	public enum ActionResultType
	{
		/// <summary>
		/// 
		/// </summary>
		Normal,

		/// <summary>
		/// 
		/// </summary>
		Json
	}

	/// <summary>
	/// 异常拦截器
	/// </summary>
	public class ExceptionFilter : IExceptionFilter
	{
		private bool EnableExceptionFilter
		{
			get
			{
				string str = ConfigurationManager.AppSettings["EnableExceptionFilter"];
				if (string.IsNullOrEmpty(str))
					return false;
				return Convert.ToBoolean(str);
			}
		}
		/// <summary>
		/// 异常处理
		/// </summary>
		/// <param name="filterContext"></param>
		public void OnException(ExceptionContext filterContext)
		{
			ActionResultType resulttype = MessageHelper.GetActionResultTypeByRequest(filterContext.HttpContext.Request);
			filterContext.ExceptionHandled = true;
			string errMsg = filterContext.Exception.Message;// +":" + filterContext.Exception.StackTrace;

			if (!filterContext.HttpContext.Request.IsAjaxRequest() && resulttype != Filters.ActionResultType.Json)
			{
				if (filterContext.Exception is Permissions.AuthorizationException)
				{
					var exception = filterContext.Exception as App.Framework.Web.Permissions.AuthorizationException;
					if (!exception.IsAuthenticated) //如果未登录，则跳转到登录页
					{
						UserIdentityFactory.Instance.RedirectToLoginPage();
						return;
					}
				}
				if (!EnableExceptionFilter) //如果在非ajax方法请求时又未启用异常处理，则直接返回
				{
					throw filterContext.Exception;
				}
				filterContext.Controller.ViewData["ErrMsg"] = errMsg;
				filterContext.Result = new ViewResult()
				{
					ViewName = "Error",
					ViewData = filterContext.Controller.ViewData
				};
			}
			else
			{
				filterContext.Result = errMsg.ToJsonEntity(false, errMsg, false);
			}
		}
	}
}
