using System.Web.Mvc;
using App.Framework.Web;

namespace CSC
{
    public static class ControlerExtension
    {
        /// <summary>
        /// 重新加载一个Iframe，在每个window中搜索包裹此Iframe的容器
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <param name="containerId"></param>
        /// <param name="newUrl"></param>
        /// <param name="isSuccessed"></param>
        /// <returns></returns>
        public static ActionResult ReLoadFrameMessageResult(this Controller controller, string message, string containerId, string newUrl = null, bool isSuccessed = true)
        {
            return controller.ShowMessageResult(message, isSucessed: isSuccessed, btnSureClickScript: string.Format("window.parent.loadFrame(getAllWinElementById('{0}'), {1}, true);", containerId, newUrl ?? "false"));
        }

        /// <summary>
        /// 刷新父窗口并关闭所有dialog
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <param name="tabId"></param>
        /// <param name="newUrl"></param>
        /// <param name="isSuccessed"></param>
        /// <returns></returns>
        public static ActionResult RefreshParentAndClearDialogMessageResult(this Controller controller, string message, string tabId, string newUrl = null, bool isSuccessed = true)
        {
            return controller.ShowMessageResult(message, isSucessed: isSuccessed, btnSureClickScript: string.Format("window.parent.loadFrame(window.parent.$(\"#{0}\"), {1}, true);window.parent.$(\".div_shade_dialog\").remove();window.parent.$(\".div_container_dialog\").remove(); ", tabId, string.IsNullOrEmpty(newUrl) ? "false" : "\"" + newUrl + "\""));
        }


        /// <summary>
        /// 显示消息并且刷新父窗体
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <param name="tabId"></param>
        /// <param name="newUrl"> </param>
        /// <param name="isSuccessed"> </param>
        /// <returns></returns>
        public static ActionResult RefreshParentMessageResult(this Controller controller, string message, string tabId, string newUrl = null, bool isSuccessed = true)
        {
            return controller.ShowMessageResult(message, isSucessed: isSuccessed, btnSureClickScript: string.Format("window.parent.loadFrame(window.parent.$(\"#{0}\"), {1}, true);", tabId, string.IsNullOrEmpty(newUrl) ? "false" : "\"" + newUrl + "\""));
        }


        /// <summary>
        /// 刷新Dialog内容
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <param name="containerId"></param>
        /// <param name="isSuccessed"></param>
        /// <returns></returns>
        public static ActionResult RefreshDialogContent(this Controller controller, string message, string containerId, bool isSuccessed = true)
        {
            return controller.ShowMessageResult(message, isSucessed: isSuccessed, btnSureClickScript: "window.parent.refreshFrameById('" + containerId + "');");
        }

        /// <summary>
        /// 弹出消息，刷新指定的frame
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <param name="tabId"></param>
        /// <param name="isSuccessed"></param>
        /// <returns></returns>
        public static ActionResult RefreshMessageResult(this Controller controller, string message, string tabId, bool isSuccessed = true)
        {
            return controller.ShowMessageResult(message, isSucessed: isSuccessed, btnSureClickScript: string.Format("loadFrame($(\"#{0}\"), false, true);", tabId));
        }

        /// <summary>
        /// 弹出消息，重新载入父页面
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <param name="isSuccessed"></param>
        /// <returns></returns>
        public static ActionResult ReLoadParentMessageResult(this Controller controller, string message, bool isSuccessed = true)
        {
            return controller.ShowMessageResult(message, isSucessed: isSuccessed, btnSureClickScript: "window.parent.location.reload()");
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <param name="tabId"></param>
        /// <param name="isSuccessed"></param>
        /// <returns></returns>
        public static ActionResult CloseDialogMessageResult(this Controller controller, string message, string tabId, bool isSuccessed = true)
        {
            return controller.ShowMessageResult(message, isSucessed: isSuccessed, btnSureClickScript: "closeDialogById('" + tabId + "');");
        }

        /// <summary>
        /// 页面是否在进行排序或者分布操作
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static bool IsSortingOrPageing(this Controller controller)
        {
            var request = controller.HttpContext.Request;
            return (request["pageIndex"] != null || request["pageSize"] != null || request["sortField"] != null) && request["reload"] == null;
        }
    }
}