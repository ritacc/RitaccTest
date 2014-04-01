//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：HtmlHelperExtention.cs
//文件功能：
//
//创建标识：鲜红 || 2011-03-29
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace App.Framework.Web
{
    /// <summary>
    /// HtmlHelper扩展
    /// </summary>
    public static partial class HtmlHelperExtention
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selectList"></param>
        /// <param name="checkBoxName"></param>
        /// <param name="splitTagName"></param>
        /// <param name="inputType"></param>
        /// <returns></returns>
        public static MvcHtmlString InputList(this HtmlHelper helper, IEnumerable<SelectListItem> selectList, string checkBoxName, string splitTagName, InputType inputType)
        {
            return InputList(helper, null, selectList, checkBoxName, splitTagName, inputType);
        }

        /// <summary>
        /// 生成CheckBox列表
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="checkBoxName">name属性</param>
        /// <param name="splitTagName">每个SelectList外层</param>
        /// <param name="inputType">inputType</param>
        /// <param name="selectList">selectList</param>
        /// <param name="metadata">metadata</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString InputList(this HtmlHelper helper, ModelMetadata metadata, IEnumerable<SelectListItem> selectList, string checkBoxName, string splitTagName, InputType inputType)
        {
            if (helper == null) throw new ArgumentNullException("helper");
            if (selectList == null) throw new ArgumentNullException("selectList");
            if (string.IsNullOrEmpty(checkBoxName)) throw new ArgumentNullException("checkBoxName");

            StringBuilder sb = new StringBuilder();
            int idIndex = 0;
            TagBuilder tagBuilder = new TagBuilder("span");
            foreach (SelectListItem item in selectList)
            {
                TagBuilder splitTagBuilder = null;
                if (!string.IsNullOrEmpty(splitTagName))
                    splitTagBuilder = new TagBuilder(splitTagName);
                TagBuilder checkTagBuilder = new TagBuilder("input");
                checkTagBuilder.Attributes["type"] = inputType.ToString();
                checkTagBuilder.Attributes["name"] = checkBoxName;
                checkTagBuilder.Attributes["value"] = item.Value;

                string checkBoxId = checkBoxName + "_id_" + idIndex;
                checkTagBuilder.Attributes["id"] = checkBoxId;
                if (item.Selected)
                    checkTagBuilder.Attributes["checked"] = "checked";
                TagBuilder labelTagBuilder = new TagBuilder("label") { InnerHtml = helper.Encode(item.Text) };

                labelTagBuilder.Attributes["for"] = checkBoxId;
                string checkHtml = checkTagBuilder.ToString() + labelTagBuilder.ToString();
                if (splitTagBuilder != null)
                {
                    splitTagBuilder.InnerHtml += checkHtml;
                    sb.AppendLine(splitTagBuilder.ToString());
                }
                else
                    sb.AppendLine(checkHtml);

                idIndex++;
            }

            TagBuilder hiddenTagBuilder = new TagBuilder("input");
            hiddenTagBuilder.Attributes["type"] = "hidden";
            hiddenTagBuilder.MergeAttribute("name", "hidden" + checkBoxName);
            hiddenTagBuilder.MergeAttribute("id", "hidden" + checkBoxName);
            //hiddenTagBuilder.MergeAttributes<string, object>(helper.GetUnobtrusiveValidationAttributes(checkBoxName, metadata));
            tagBuilder.InnerHtml = hiddenTagBuilder + sb.ToString();
            return MvcHtmlString.Create(tagBuilder.ToString());
        }
        /// <summary>
        /// 生成CheckBox列表
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="selectList"></param>
        /// <param name="checkBoxName">name属性</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, IEnumerable<SelectListItem> selectList, string checkBoxName)
        {
            return helper.InputList(selectList, checkBoxName, null, InputType.CheckBox);
        }

        /// <summary>
        /// Input列表
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="splitTag"></param>
        /// <param name="inputType"></param>
        /// <returns></returns>
        public static MvcHtmlString InputListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string splitTag, InputType inputType)
        {
            ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            List<SelectListItem> list = ((List<SelectListItem>)modelMetadata.Model);
            return htmlHelper.InputList(modelMetadata, list, modelMetadata.PropertyName, splitTag, inputType);
        }

        /// <summary>
        /// CheckBox 列表
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.InputListFor(expression, "span", InputType.CheckBox);
        }

        /// <summary>
        /// CheckBox 列表
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="splitTag"></param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string splitTag)
        {
            return htmlHelper.InputListFor(expression, splitTag, InputType.CheckBox);
        }

        /// <summary>
        ///  生成CheckBox列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selectList"></param>
        /// <param name="checkBoxName"></param>
        /// <param name="splitTagName">每项分隔符的Tag名称</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, IEnumerable<SelectListItem> selectList, string checkBoxName, string splitTagName)
        {
            return InputList(helper, selectList, checkBoxName, splitTagName, InputType.CheckBox);
        }


        /// <summary>
        /// Radio列表
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString RadioBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.InputListFor(expression, "span", InputType.Radio);
        }



        /// <summary>
        /// 生成RadioButton列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selectList"></param>
        /// <param name="checkBoxName"></param>
        /// <param name="splitTag"></param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonList(this HtmlHelper helper, IEnumerable<SelectListItem> selectList, string checkBoxName, string splitTag)
        {
            return InputList(helper, selectList, checkBoxName, splitTag, InputType.Radio);
        }

        /// <summary>
        /// 生成RadioButton列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selectList"></param>
        /// <param name="checkBoxName"></param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonList(this HtmlHelper helper, IEnumerable<SelectListItem> selectList, string checkBoxName,Predicate<SelectListItem> checkCall = null)
        {
            foreach (var item in selectList)
                if (checkCall != null && checkCall(item))
                    item.Selected = true;
            return InputList(helper, selectList, checkBoxName, null, InputType.Radio);
        }
    }
}
