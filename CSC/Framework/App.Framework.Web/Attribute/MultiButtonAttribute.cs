//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：MultiButtonAttribute.cs
//文件功能：实现多个按钮提交
//
//创建标识：
//
//修改标识：
//修改描述：
//**********************************************************

using System;
using System.Web.Mvc;
using System.Linq;
using System.Reflection;

namespace App.Framework.Web
{
    /// <summary>
    /// 实现多个按钮提交
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class MultiButtonAttribute : ActionNameSelectorAttribute
    {
        /// <summary>
        /// 按钮name属性值
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// name属性值后置value
        /// </summary>
        public string Argument { get; set; }

        /// <summary>
        /// 重写属性验证
        /// </summary>
        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            var key = ButtonKeyFrom(controllerContext);
            var keyIsValid = IsValid(key);

            if (keyIsValid)
            {
                UpdateValueProviderIn(controllerContext, ValueFrom(key));
            }

            return keyIsValid;
        }

        /// <summary>
        /// 获取按钮key(Name属性)
        /// </summary>
        /// <param name="controllerContext">Controller上下文</param>
        /// <returns>key</returns>
        private string ButtonKeyFrom(ControllerContext controllerContext)
        {
            var keys = controllerContext.HttpContext.Request.Params.AllKeys;
            return keys.FirstOrDefault(KeyStartsWithButtonName);
        }

        /// <summary>
        /// 判断是否为空
        /// </summary>
        private static bool IsValid(string key)
        {
            return key != null;
        }

        /// <summary>
        /// 获取按钮Name中的后置参数值
        /// </summary>
        private static string ValueFrom(string key)
        {
            var parts = key.Split(":".ToCharArray());
            return parts.Length < 2 ? null : parts[1];
        }

        /// <summary>
        /// 设置Name后置参数值
        /// </summary>
        private void UpdateValueProviderIn(ControllerContext controllerContext, string value)
        {
            if (string.IsNullOrEmpty(this.Argument)) return;
            controllerContext.RouteData.Values[this.Argument] = value;
            //controllerContext.Controller.ValueProvider[Argument] = new ValueProviderResult(value, value, null);
        }

        /// <summary>
        /// 判断按钮是否以指定的名称开始(忽略大小写)
        /// </summary>
        private bool KeyStartsWithButtonName(string key)
        {
            return key.StartsWith(this.Name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
