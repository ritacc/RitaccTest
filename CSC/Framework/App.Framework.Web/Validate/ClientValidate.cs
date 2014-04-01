//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：ClientValidate.cs
//文件功能：
//
//创建标识：鲜红 || 2011-05-09
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace App.Framework.Web
{
    /// <summary>
    /// HtmlHelper辅助类，增加验证客户端的快捷方式
    /// </summary>
    public static class ValidateHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="htmlTagName"></param>
        /// <returns></returns>
        public static MvcHtmlString ValidateMessage(this HtmlHelper helper, string htmlTagName)
        {
            return MvcHtmlString.Create(string.Format("<span data-valmsg-replace=\"true\" data-valmsg-for=\"{0}\" class=\"field-validation-valid\"></span>",htmlTagName));
        }

        /// <summary>
        /// 生成Html控件，并生成验证属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="inputType"></param>
        /// <param name="name"></param>
        /// <param name="display"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static Validate Validate(this HtmlHelper obj, InputType inputType, string name, string display, object htmlAttributes)
        {
            return obj.Validate(inputType, name, name, display, htmlAttributes);
        }

        /// <summary>
        /// 生成Html控件，并生成验证属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="inputType"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="display"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static Validate Validate(this HtmlHelper obj, InputType inputType, string name, string id, string display, object htmlAttributes)
        {
            return new Validate(inputType, name, id, display,null);
        }

        /// <summary>
        /// 生成Html控件，并生成验证属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="display"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static Validate TextValidate(this HtmlHelper obj, string name, string display, object htmlAttributes)
        {
            return obj.Validate(InputType.Text, name, name, display, htmlAttributes);
        }
        /// <summary>
        /// 生成Html控件，并生成验证属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="display"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static Validate TextValidate(this HtmlHelper obj, string name, string id, string display, object htmlAttributes)
        {
            return obj.Validate(InputType.Text, name, id, display, htmlAttributes);
        }

    }

    /// <summary>
    /// 限制字符长度
    /// </summary>
    public class LengthValidate : Validate
    {
        /// <summary>
        /// 
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public LengthValidate(int min, int max)
        {
            Min = min;
            Max = max;
            ErrorMessage = string.Format("{0}长度为{1}-{2}之间", Root.Display, Min, Max);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagBuilder"></param>
        internal override void ToMvcHtml(System.Web.Mvc.TagBuilder tagBuilder)
        {
            tagBuilder.MergeAttribute("data-val-length-min", Min.ToString());
            tagBuilder.MergeAttribute("data-val-length-max", Max.ToString());
            tagBuilder.MergeAttribute("data-val-length", ErrorMessage);
        }
    }

    /// <summary>
    /// 必填
    /// </summary>
    public class RequiredValidate : Validate
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RequiredValidate()
        {
            ErrorMessage = string.Format("{0}不能为空", Root.Display);
        }

        internal override void ToMvcHtml(TagBuilder tagBuilder)
        {
            tagBuilder.MergeAttribute("data-val-required", ErrorMessage);
        }
    }

    /// <summary>
    /// 正则
    /// </summary>
    public class RegexValidate : Validate
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string RegexString { get; set; }
        internal override void ToMvcHtml(TagBuilder tagBuilder)
        {
            tagBuilder.MergeAttribute("data-val-regex-pattern", RegexString);
            tagBuilder.MergeAttribute("data-val-regex", ErrorMessage);
        }
    }

    /// <summary>
    /// 生成客户端验证代码类
    /// </summary>
    public class Validate
    {
        private string _HtmlId { get; set; }
        private InputType _InputType { get; set; }
        private IDictionary<string, object> _HtmlAttributes;
        private string _Name;


        /// <summary>
        /// 上一个
        /// </summary>
        public Validate Prev { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 根节点
        /// </summary>
        public static Validate Root { get; set; }
        /// <summary>
        /// 显示的字段名称
        /// </summary>
        public string Display { get; set; }


        /// <summary>
        /// 生成Html控件，并生成验证属性
        /// </summary>
        /// <param name="inputType"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="display"></param>
        /// <param name="htmlAttributes"></param>
        public Validate(InputType inputType, string name, string id, string display, IDictionary<string, object> htmlAttributes)
        {
            _InputType = inputType;
            _HtmlId = id;
            Display = display;
            _HtmlAttributes = htmlAttributes;
            Root = this;
            _Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        protected Validate() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagBuilder"></param>
        internal virtual void ToMvcHtml(TagBuilder tagBuilder)
        {
            tagBuilder.MergeAttribute("type", _InputType.ToString().ToLower());
            tagBuilder.MergeAttribute("id", _HtmlId);
            tagBuilder.MergeAttribute("name", _Name);
            tagBuilder.MergeAttribute("data-val", "true");
            tagBuilder.MergeAttributes<string, object>(_HtmlAttributes);
        }

        /// <summary>
        /// 完成所有验证并输出HTML代码
        /// </summary>
        /// <returns></returns>
        public MvcHtmlString Complete()
        {
            TagBuilder tagBuilder = new TagBuilder("input");
            GetHtml(this, tagBuilder);
            return MvcHtmlString.Create(tagBuilder.ToString());
        }

        private void GetHtml(Validate cur, TagBuilder tagBuilder)
        {
            cur.ToMvcHtml(tagBuilder);
            if (cur.Prev != null)
                GetHtml(cur.Prev, tagBuilder);
        }

        /// <summary>
        /// 限定字符长度
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Validate Length(int min, int max)
        {
            LengthValidate v = new LengthValidate(min, max);
            v.Prev = this;
            return v;
        }

        /// <summary>
        /// 必须
        /// </summary>
        /// <returns></returns>
        public Validate Required()
        {
            RequiredValidate v = new RequiredValidate();
            v.Prev = this;
            return v;
        }

        /// <summary>
        /// 正则
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public Validate Regex(string reg,string errMsg)
        {
            RegexValidate v = new RegexValidate();
            v.RegexString = reg;
            v.ErrorMessage = errMsg;
            v.Prev = this;
            return v;
        }
    }
}
