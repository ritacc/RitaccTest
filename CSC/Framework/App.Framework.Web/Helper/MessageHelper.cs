//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：MessageHelper.cs
//文件功能：调用用户消息页给出提示
//
//创建标识：鲜红 || 2011-04-11
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using App.Framework.Web.Filters;
using System.Web;
using System.Web.UI;
using App.Framework.Web.Json;

namespace App.Framework.Web
{



	/// <summary>
	/// 调用用户消息页给出提示
	/// </summary>
	public static class MessageHelper
	{
		private static readonly string MSGSTR = "message";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
		public static MvcHtmlString Message(this HtmlHelper helper)
		{
			var msg = helper.ViewContext.Controller.ViewData[MSGSTR];
			if (msg != null)
				return MvcHtmlString.Create(msg.ToString());
			else
				return null;
		}

        
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="isSucessed"></param>
        /// <param name="redirectUrl"></param>
        /// <param name="btnSureClickScript"></param>
        /// Commented by jason in 20120416 for MessageBox no default title
		/// public static void ShowMessage(this Controller controller, string msg, string title = "消息", bool isSucessed = true, string redirectUrl = "#", string btnSureClickScript = "")
		/// end Commented by jason in 20120416 for MessageBox no default title
		public static void ShowMessage(this Controller controller, string msg, string title = "", bool isSucessed = true, string redirectUrl = "#", string btnSureClickScript = "")
		{
            controller.ViewData[MSGSTR] = string.Format("if(window.btnSureClick == undefined) window.btnSureClick = function(sucessed, redirectUrl){{{0}}};showMessage('{1}',{2},{3},'{4}');", btnSureClickScript, title, msg.ToJSON(), isSucessed.ToString().ToLower(), redirectUrl);
		}

		/// addedd by jason in 20120416 for MessageBox no default title
		/// <summary>
		/// Show message on current page
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="msg"></param>
		/// <param name="title"></param>
		/// <param name="isSucessed"></param>
		/// <param name="redirectUrl"></param>
		/// <param name="btnSureClickScript"></param>
		public static void ShowMessageEx(this Controller controller, string msg, bool isSucessed = true, string redirectUrl = "#", string btnSureClickScript = "")
		{
			controller.ViewData[MSGSTR] = string.Format("if(window.btnSureClick == undefined) window.btnSureClick = function(sucessed, redirectUrl){{{0}}};showMessageEx({1},{2},'{3}');", btnSureClickScript,  msg.ToJSON(), isSucessed.ToString().ToLower(), redirectUrl);
		}

        /// <summary>
        /// 显示是否
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="msg"></param>
        /// <param name="title"></param>
		/// Commented by jason in 20120416 for MessageBox no default title
		/// public static void ShowConfirm(this Controller controller, string msg, string title = "确定")
		/// end Commented by jason in 20120416 for MessageBox no default title
        public static void ShowConfirm(this Controller controller, string msg, string title = "")
        {
            controller.ViewData[MSGSTR] = string.Format("showConfirm('{0}','{1}');",  title, msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="isSucessed"></param>
        /// <param name="redirectUrl"></param>
        /// <param name="btnSureClickScript"></param>
        /// <returns></returns>
		/// Commented by jason in 20120416 for MessageBox no default title
		/// public static ActionResult ShowMessageResult(this Controller controller, string msg, string title = "消息",bool isSucessed = true, string redirectUrl = "#", string btnSureClickScript = "")
		/// end Commented by jason in 20120416 for MessageBox no default title
        public static ActionResult ShowMessageResult(this Controller controller, string msg, string title = "",bool isSucessed = true, string redirectUrl = "#", string btnSureClickScript = "")
        {
            ShowMessage(controller, msg, title, isSucessed, redirectUrl, btnSureClickScript);
            return controller.Message();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static ActionResult Message(this Controller controller)
        {
            return Message(controller, null, null, true);
        }

		/// <summary>
		/// 调用用户消息页给出提示
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="msg">提示消息</param>
		/// <returns>ActionResult</returns>
		public static ActionResult Message(this Controller controller, string msg)
		{
            controller.ShowMessage(msg);
			return Message(controller, null, null, true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public static ActionResult Error(this Controller controller, string msg)
		{
			return Message(controller, msg, null, false);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="successCallback"></param>
		/// <returns></returns>
		public static ActionResult MessageOrError(this Controller controller, Func<ActionResult> successCallback)
		{
			if (controller.ModelState.IsValid)
				return successCallback();
			return controller.Error();
		}

		/// <summary>
		/// 直接从ModelState里取出错误输出
		/// </summary>
		/// <param name="controller"></param>
		/// <returns></returns>
		public static ActionResult Error(this Controller controller)
		{
			string err = string.Empty;
			foreach (ModelState state2 in controller.ModelState.Values)
			{
				foreach (ModelError error in state2.Errors)
				{
					err += error.ErrorMessage + "\r\n";
				}
			}
			return Message(controller, err, null, false);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="msg"></param>
		/// <param name="urlDic"></param>
		/// <returns></returns>
		public static ActionResult Message(this Controller controller, string msg, Dictionary<string, string> urlDic)
		{
			return Message(controller, msg, urlDic, true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="msg"></param>
		/// <param name="urlDic"></param>
		/// <returns></returns>
		public static ActionResult Error(this Controller controller, string msg, Dictionary<string, string> urlDic)
		{
			return Message(controller, msg, urlDic, false);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static ActionResultType GetActionResultTypeByRequest(HttpRequestBase request)
		{
			string resulttype = request["resulttype"] ?? string.Empty;
			if (request.IsAjaxRequest() || string.Equals(resulttype, ActionResultType.Json.ToString(), StringComparison.OrdinalIgnoreCase))
				return ActionResultType.Json;
			else
				return ActionResultType.Normal;
		}

		/// <summary>
		/// 调用用户消息页给出提示
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="msg">提示消息</param>
		/// <param name="urlDic">Key为Url，Value为显示链接名称</param>
		/// <param name="success">成功?</param>
		/// <returns>ActionResult</returns>
		public static ActionResult Message(this Controller controller, string msg, Dictionary<string, string> urlDic, bool success)
		{
			ActionResultType atype = GetActionResultTypeByRequest(controller.HttpContext.Request);
			if (atype == ActionResultType.Json)
			{
				return msg.ToJsonEntity(success, msg);
			}
			var result = new ViewResult()
			{
				ViewName = "Message",
				ViewData = new ViewDataDictionary() { new KeyValuePair<string, object>("Message", msg) }
			};
			if (urlDic != null)
				result.ViewData["LinkDic"] = urlDic;
			result.ViewData["Success"] = success;
			return result;
		}
	}
}
