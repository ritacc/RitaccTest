//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：PageButton.cs
//文件功能：分页的按钮类
//
//创建标识：鲜红 || 2011-03-24
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Web.Pager
{
    /// <summary>
    /// 分页按钮
    /// </summary>
    public class PageButton
    {
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// 按钮类型
        /// </summary>
        public PageButtonType ButtonType { get; set; }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool Hide { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PageButton()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pageIndex"></param>
        /// <param name="disabled"></param>
        /// <param name="buttonType"></param>
        public PageButton(string text, int pageIndex, bool disabled, PageButtonType buttonType)
        {
            Text = text;
            PageIndex = pageIndex;
            Disabled = disabled;
            ButtonType = buttonType;
        }
    }
}
