using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CSC.Resources;
using System.Linq.Expressions;
using System.Threading;
using System.Web.Routing;
using App.Framework.Web;
namespace CSC
{
    public enum HtmlDisablStyle
    {
        Disabled,
        Hide
    }

    /// <summary>
    /// HTML扩展
    /// </summary>
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString DropDownListForHourMinute<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttribues = null, string type = "M")
        {
            List<SelectListItem> list = new List<SelectListItem>();

            if (type == "H") //小时
            {
                for (int i = 0; i <= 23; i++)
                {
                    SelectListItem item = new SelectListItem() { Text = i >= 10 ? i.ToString() : "0" + i, Value = i >= 10 ? i.ToString() : "0" + i };
                    list.Add(item);
                }
            }
            else //分钟
            {
                for (int i = 0; i <= 59; i++)
                {
                    SelectListItem item = new SelectListItem() { Text = i >= 10 ? i.ToString() : "0" + i, Value = i >= 10 ? i.ToString() : "0" + i };
                    list.Add(item);
                }
            }

            list.Insert(0, new SelectListItem() { Text = "", Value = "" });

            return html.DropDownListFor(expression, list, htmlAttribues);
        }

        public static string GetText<TModel>(this ViewPage<TModel> html, string textCN, string textEN)
        {
            if (Thread.CurrentThread.CurrentUICulture.Name.Equals("zh-cn", StringComparison.OrdinalIgnoreCase))
                return textCN;
            else
                return textEN;
        }

        public static string GetText(this HtmlHelper html, string textCN, string textEN)
        {
            if (Thread.CurrentThread.CurrentUICulture.Name.Equals("zh-cn", StringComparison.OrdinalIgnoreCase))
                return textCN;
            else
                return textEN;
        }


        public static MvcHtmlString LabelX(this HtmlHelper html, string textCN, string textEN, string labelFor = null)
        {
            TagBuilder tag = new TagBuilder("label");
            if (!string.IsNullOrEmpty(labelFor))
            {
                tag.MergeAttribute("for", labelFor);
            }
            if (Thread.CurrentThread.CurrentUICulture.Name.Equals("zh-cn", StringComparison.OrdinalIgnoreCase))
                tag.InnerHtml = textCN;
            else
                tag.InnerHtml = textEN;
            return MvcHtmlString.Create(tag.ToString());
        }
        /// <summary>
        /// CheckBox CUSTOM
        /// </summary>
        /// <param name="html"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="chked"></param>
        /// <param name="htmlAttribues"></param>
        /// <returns></returns>
        public static MvcHtmlString CheckBox(this HtmlHelper html, string name, string value, bool chked, object htmlAttribues = null)
        {
            TagBuilder tag = new TagBuilder("input");
            tag.MergeAttribute("type", "checkbox");
            tag.MergeAttribute("name", name);
            tag.MergeAttribute("value", value);
            tag.MergeAttribute("id", name.Replace(".","_"));
            if (chked)
                tag.MergeAttribute("checked", "checked");
            var htmlVd = new RouteValueDictionary(htmlAttribues);
            tag.MergeAttributes<string, object>(htmlVd);

            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString ButtonX(this HtmlHelper html, string text, string name = null, object htmlAttribues = null)
        {
            TagBuilder tag = new TagBuilder("button");
            //tag.MergeAttribute("type", "button");
            if (string.IsNullOrEmpty(name))
                tag.MergeAttribute("name", name);
            tag.MergeAttribute("id", name);
            //tag.MergeAttribute("value", text);
            tag.InnerHtml = text;
            var htmlVd = new RouteValueDictionary(htmlAttribues);
            tag.MergeAttributes<string, object>(htmlVd);
            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString ButtonL(this HtmlHelper html, string text, string name = null, object htmlAttribues = null)
        {
            TagBuilder tag = new TagBuilder("div");
            //tag.MergeAttribute("type", "button");
            if (string.IsNullOrEmpty(name))
                tag.MergeAttribute("name", name);
            tag.MergeAttribute("id", name);
            //tag.MergeAttribute("value", text);
            tag.InnerHtml = text;
            var htmlVd = new RouteValueDictionary(htmlAttribues);
            tag.MergeAttributes<string, object>(htmlVd);
            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString SelectYesOrNo(this HtmlHelper html, string name, bool? value, object htmlAttribues = null)
        {
            return html.DropDownList(name,
                new List<SelectListItem>() 
                { 
                    { new SelectListItem() { Text = string.Empty, Value = null, Selected = value==null } }, 
                    { new SelectListItem() { Text = GlobalText.SelectYesItemText, Value = "true", Selected = value!=null && value.Value } }, 
                    { new SelectListItem() { Text = GlobalText.SelectNoItemText, Value = "false", Selected = value!=null && !value.Value } }
                }, htmlAttribues
            );

        }


        public static MvcHtmlString SelectYesOrNo(this HtmlHelper html, string name, string value, string yesValue, string noValue, object htmlAttribues = null)
        {
            return html.DropDownList(name,
                new List<SelectListItem>() 
                { 
                    { new SelectListItem() { Text = string.Empty, Value = null, Selected = value==null } }, 
                    { new SelectListItem() { Text = GlobalText.SelectYesItemText, Value = yesValue, Selected = yesValue ==value  } }, 
                    { new SelectListItem() { Text = GlobalText.SelectNoItemText, Value = noValue, Selected =  noValue ==value } }
                }, htmlAttribues
            );

        }

        public static MvcHtmlString SelectYesOrNo2(this HtmlHelper html, string name, string value, string yesValue, string noValue, object htmlAttribues = null)
        {
            return html.DropDownList(name,
                new List<SelectListItem>() 
                { 
                    { new SelectListItem() { Text = string.Empty, Value = null, Selected = value==null } }, 
                    { new SelectListItem() { Text = "YES", Value = yesValue, Selected = yesValue ==value  } }, 
                    { new SelectListItem() { Text = "NO", Value = noValue, Selected =  noValue ==value } }
                }, htmlAttribues
            );

        }

        /// <summary>
        /// 权限按钮（有权限则button可用(显示)，没有则不可用（不显示））
        /// </summary>
        /// <param name="html"></param>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="p"></param>
        /// <param name="style"></param>
        /// <param name="htmlAttribues"></param>
        /// <returns></returns>
        public static MvcHtmlString PermissionButton(this HtmlHelper html, string name, string text, EnumPermission p, HtmlDisablStyle style = HtmlDisablStyle.Disabled, object htmlAttribues = null)
        {
            var htmlVd = new RouteValueDictionary(htmlAttribues);
            if (QuickInvoke.GetCurrentUserHasPermission(p))
            {
                if (style == HtmlDisablStyle.Disabled)
                    htmlVd.Add("disabled", "disabled");
                else if (style == HtmlDisablStyle.Hide)
                    htmlVd.Add("style", "display:none;");
            }
            return html.ButtonX(text, name, htmlVd);
        }


        /// <summary>
        /// 根据权限生成可用或禁用的A标签
        /// </summary>
        /// <param name="html"></param>
        /// <param name="linkText">文本</param>
        /// <param name="actionName">Action名称</param>
        /// <param name="controlName">控制器名称</param>
        /// <param name="routeValue">路由参数对象</param>
        /// <param name="p">权限</param>
        /// <param name="style">禁用还是隐藏</param>
        /// <param name="htmlAttribues">html特性</param>
        /// <returns></returns>
        public static MvcHtmlString PermissionLink(this HtmlHelper html, string linkText, string actionName,
            string controlName, object routeValue, EnumPermission p,
            HtmlDisablStyle style = HtmlDisablStyle.Disabled, object htmlAttribues = null)
        {
            var htmlVd = new RouteValueDictionary(htmlAttribues);
            if (QuickInvoke.GetCurrentUserHasPermission(p))
            {
                if (style == HtmlDisablStyle.Disabled)
                    htmlVd.Add("disabled", "disabled");
                else if (style == HtmlDisablStyle.Hide)
                    htmlVd.Add("style", "display:none;");
            }
            return html.ActionLink(linkText, actionName, controlName, routeValue, htmlVd);
        }

        /// <summary>
        /// 根据权限生成可用或禁用的A标签
        /// </summary>
        /// <param name="html"></param>
        /// <param name="linkText">文本</param>
        /// <param name="url">URL</param>
        /// <param name="routeValue">路由参数对象</param>
        /// <param name="p">权限</param>
        /// <param name="style">禁用还是隐藏</param>
        /// <param name="htmlAttribues">html</param>
        /// <returns></returns>
        public static MvcHtmlString PermissionLink(this HtmlHelper html, string linkText, string url, object routeValue,
            EnumPermission p, HtmlDisablStyle style = HtmlDisablStyle.Disabled, object htmlAttribues = null)
        {
            var htmlVd = new RouteValueDictionary(htmlAttribues);
            if (QuickInvoke.GetCurrentUserHasPermission(p))
            {
                if (style == HtmlDisablStyle.Disabled)
                    htmlVd.Add("disabled", "disabled");
                else if (style == HtmlDisablStyle.Hide)
                    htmlVd.Add("style", "display:none;");
            }
            TagBuilder tg = new TagBuilder("a");
            tg.InnerHtml = linkText;
            tg.MergeAttribute("href", url);
            tg.MergeAttributes(htmlVd);
            return MvcHtmlString.Create(tg.ToString());
        }

        public static MvcHtmlString SetCalendarFormat(this HtmlHelper html)
        {
            string str = @"
    <script type=""text/javascript"">
        window.DataFormat = '{0}';
        window.Language = '{1}';
		window.TranDate='{2}';
    </script>
";
            return MvcHtmlString.Create(string.Format(str
				, App.Framework.Web.ConfigHelper.DateTimeFormat
				, Thread.CurrentThread.CurrentUICulture.Name == "zh-CN" ? "cn" : "en"
				,QuickInvoke.GetTransactionDate().Format()));
        }

        public static MvcHtmlString SelectYesOrNoFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttribues = null)
        {

            object val = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, html.ViewData).Model;
            bool? value = val == null ? null : (bool?)Convert.ToBoolean(val);

            return html.DropDownListFor(expression,
                new List<SelectListItem>() 
                { 
                    { new SelectListItem() { Text = string.Empty, Value = null, Selected = value==null } }, 
                    { new SelectListItem() { Text = GlobalText.SelectYesItemText, Value = "true", Selected = value!=null && value.Value } }, 
                    { new SelectListItem() { Text = GlobalText.SelectNoItemText, Value = "false", Selected = value!=null && !value.Value } }
                }, htmlAttribues
            );
        }


        public static MvcHtmlString LabelHiddenFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, int width =150,object htmlAttribues = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            object val = metadata.Model;
            if (val == null) val = string.Empty;
            TagBuilder tag = new TagBuilder("label");
            tag.MergeAttribute("class", "label_readonly");
            tag.MergeAttribute("style", "width: " + width + "px;");
            tag.MergeAttributes(new RouteValueDictionary(htmlAttribues));
            tag.InnerHtml = val.ToString();
            TagBuilder tagHidden = new TagBuilder("input");
            tagHidden.MergeAttribute("name", metadata.PropertyName);
            tagHidden.MergeAttribute("type", "hidden");
            tagHidden.MergeAttribute("value", val.ToString());
            return MvcHtmlString.Create(string.Format("{0}{1}", tag.ToString(), tagHidden.ToString()));
        }


    }
}
