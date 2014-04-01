//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：PageButtonType.cs
//文件功能：分页中按钮枚举
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
    /// 分页中按钮枚举
    /// </summary>
    public enum PageButtonType
    {
        /// <summary>
        /// 第一页按钮
        /// </summary>
        FirstPageButton,
        /// <summary>
        /// 上一页按钮
        /// </summary>
        PrevPageButton,
        /// <summary>
        /// 下一页按钮
        /// </summary>
        NextPageButton,
        /// <summary>
        /// 最后一页按钮
        /// </summary>
        LastPageButton,
        /// <summary>
        /// 更多按钮
        /// </summary>
        MorePageButton,
        /// <summary>
        /// 数字页按钮
        /// </summary>
        NumericPageButton
    }
}
