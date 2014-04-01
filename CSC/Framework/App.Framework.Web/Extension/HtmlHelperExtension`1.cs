using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI;
using System.Text;
using System.Collections.Specialized;
using App.Framework.Web.Pager;

namespace App.Framework.Web
{
    /// <summary>
    ///扩展 Html 控件
    /// </summary>
    public static class HtmlControls
    {

        /// <summary>
        /// 按钮
        /// </summary>
        /// <param id="helper">HtmlHelper</param>
        /// <param id="value">按钮文字</param>
        /// <param id="id">id</param>
        /// <param name="url">正确时返回路径</param>
        public static void Submit(this HtmlHelper helper, string value, string id = null, string url = null)
        {
            HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);
            TagBuilder tbButton = new TagBuilder("input");

            tbButton.MergeAttribute("type", "submit");
            if (!string.IsNullOrEmpty(value))
            {
                tbButton.MergeAttribute("value", value);
            }
            if (!string.IsNullOrEmpty(id))
            {
                tbButton.MergeAttribute("id", id);
            }
            if (!string.IsNullOrEmpty(id))
            {
                tbButton.MergeAttribute("name", id);
            }
            if (!string.IsNullOrEmpty(url))
            {
                tbButton.MergeAttribute("url", url);
            }

            writer.InnerWriter.Write(tbButton.ToString(TagRenderMode.SelfClosing));
        }
        /// <summary>
        /// 按钮
        /// </summary>
        /// <param id="helper">HtmlHelper</param>
        /// <param id="value">按钮文字</param>
        /// <param id="id">id</param>
        /// <param name="url">路径</param>
        public static void Button(this HtmlHelper helper, string value, string id = null, string url = null)
        {
            helper.PowerButton(value, url, false, id);
        }
        /// <summary>
        /// 权限按钮
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value">按钮文字</param>
        /// <param name="url">路径</param>
        /// <param name="isDisabled">是否禁用</param>
        /// <param name="id">id</param>
        public static void PowerButton(this HtmlHelper helper, string value, string url, bool isDisabled, string id = null)
        {
            HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);
            TagBuilder tbButton = new TagBuilder("input");

            tbButton.MergeAttribute("type", "button");
            if (!string.IsNullOrEmpty(value))
            {
                tbButton.MergeAttribute("value", value);
            }
            if (!string.IsNullOrEmpty(id))
            {
                tbButton.MergeAttribute("id", id);
            }
            if (!string.IsNullOrEmpty(id))
            {
                tbButton.MergeAttribute("name", id);
            }
            if (!string.IsNullOrEmpty(url))
            {
                tbButton.MergeAttribute("url", url);
            }
            if (isDisabled)
            {
                tbButton.MergeAttribute("disabled", "disabled");
            }

            writer.InnerWriter.Write(tbButton.ToString(TagRenderMode.SelfClosing));
        }
        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param id="helper">HtmlHelper</param>
        /// <param id="value">按钮文字</param>
        /// <param id="id">id</param>
        /// <param id="url">返回路径</param>
        public static void ReturnButton(this HtmlHelper helper, string value, string id, string url)
        {
            helper.PowerButton(value, url, false, id);
        }


        public static void EnableSortScript(this HtmlHelper helper)
        {
            var viewData = helper.ViewData;
            string str = @"window.SortField = '{0}';
                window.SortDirection = '{1}';";
            HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);
            writer.Write(string.Format(str, viewData["sortField"], viewData["sortDirection"]));
        }

        /// <summary>
        ///排序
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="linkText"></param>
        /// <param name="actionName"></param>
        /// <param name="sortFieldIndex"></param>
        /// <param name="controllerName"></param>
        /// <param name="htmlAttributes"></param>
        public static void Sort(this HtmlHelper helper, string linkText, string actionName, int sortFieldIndex,string sortFieldName=null, string controllerName = null, object htmlAttributes = null)
        {
            var vd = helper.ViewData;
            var viewData = helper.ViewData;

            NameValueCollection sortNameValues = new NameValueCollection();
            sortNameValues.Add("PageIndex", viewData["PageIndex"] == null ? "1" : viewData["PageIndex"].ToString());
            sortNameValues.Add("PageSize", viewData["PageSize"] == null ? "20" : viewData["PageSize"].ToString());
            sortNameValues.Add("SortField", sortFieldIndex.ToString());
            sortNameValues.Add("SortFieldName", sortFieldName);
            sortNameValues.Add("SortDirection", (viewData["SortDirection"] != null && viewData["SortDirection"].ToString() == "1") && (viewData["SortField"].ToString() == sortFieldIndex.ToString()) ? "-1" : "1");
            string url = App.Framework.Web.Pager.Util.GetUrl(helper.ViewContext.RequestContext, sortNameValues);

            HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);
            writer.InnerWriter.Write(string.Format("<a href='{0}'>{1}</a>", url, linkText));
        }




        /// <summary>
        /// 扩展自定义 ActionLink
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="linkText">链接文字</param>
        /// <param name="actionName">action</param>
        /// <param name="action">Action 表达式, QueryString 参数</param>
        /// <param name="htmlAttributes">html 属性</param>
        /// <returns>MvcHtmlString</returns>
        public static void ActionLinkExtend(this HtmlHelper helper, string linkText, string actionName, Action<Dictionary<string, object>> action, object htmlAttributes, string controllerName = null)
        {
            HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);
            RouteValueDictionary routeValues = null;
            if (action != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                action(dict);
                routeValues = new RouteValueDictionary(dict);
            }

            RouteValueDictionary htmlValues = new RouteValueDictionary(htmlAttributes);
            writer.InnerWriter.Write(LinkExtensions.ActionLink(helper, linkText, actionName, controllerName, routeValues, htmlValues));
        }

        /// <summary>
        /// 生成Label和记录Label值的Hidden
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString LabelAndHiddenFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            MvcHtmlString label = html.LabelFor(expression);
            MvcHtmlString hidden = html.HiddenFor(expression);
            return MvcHtmlString.Create(label.ToString() + hidden.ToString());
        }


        /// <summary>
        /// 列表项索引
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString GridItemIndex(this HtmlHelper html)
        {
            if (html.ViewData["ItemIndex"] == null)
                html.ViewData["ItemIndex"] = 0;
            var pageParams = PagerHelper.GetPageParams(html.ViewContext.RequestContext.HttpContext.Request, html.ViewData);
            var index = Convert.ToInt32(html.ViewData["ItemIndex"]) + 1;
            html.ViewData["ItemIndex"] = index;
            return MvcHtmlString.Create(((pageParams.PageIndex - 1) * pageParams.PageSize + index).ToString());

        }
    }


}