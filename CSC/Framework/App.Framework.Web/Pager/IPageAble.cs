//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：IPageAble.cs
//文件功能：分页接口
//
//创建标识：鲜红 || 2011-03-24
//
//修改标识：
//修改描述：
//**********************************************************
using System.Collections.Generic;

namespace App.Framework.Web.Pager
{
    /// <summary>
    /// 分页接口
    /// </summary>
    public interface IPageAble
    {
        /// <summary>
        /// 一个按钮或者少于一个按钮的时候
        /// </summary>
        /// <returns></returns>
        IList<PageButton> GetBtnLessThanOnePage();
        /// <summary>
        /// 第一页时
        /// </summary>
        /// <returns></returns>
        IList<PageButton> GetBtnWhenFrist();
        /// <summary>
        /// 最后一页时
        /// </summary>
        /// <returns></returns>
        IList<PageButton> GetBtnWhenLast();
        /// <summary>
        /// 默认情况
        /// </summary>
        /// <returns></returns>
        IList<PageButton> GetBtnWhenDefault();
        /// <summary>
        /// 分页设置
        /// </summary>
        PagerSettings PagerSetting { get; set; }
    }
}
