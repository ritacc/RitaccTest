//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：ListSlectRangeAttribute.cs
//文件功能：
//
//创建标识：鲜红
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace App.Framework.Web
{
    /// <summary>
    /// 用于验证列表的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class ListSlectRangeAttribute : ValidationAttribute//, IClientValidatable
    {
        private const string errFormat = "该项最少选择项为{0}最多选择项为{1}";

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ListSlectRangeAttribute()
        {
            MinSelected = 0;
            MaxSelected = -1;
        }

        /// <summary>
        /// 最少选择项
        /// </summary>
        public int MinSelected { get; set; }

        /// <summary>
        /// 最多选择项
        /// </summary>
        public int MaxSelected { get; set; }

        /// <summary>
        /// 是否通过验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return true;
        }


        /// <summary>
        /// 格式化错误信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(errFormat, MinSelected > 0 ? MinSelected.ToString() : "不限", MaxSelected > 0 ? MaxSelected.ToString() : "不限");
        }

        /// <summary>
        /// 获取客户端验证规则
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule
            {
                ValidationType = "list",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
            rule.ValidationParameters["min"] = MinSelected;
            rule.ValidationParameters["max"] = MaxSelected;

            yield return rule;
        }
    }
}