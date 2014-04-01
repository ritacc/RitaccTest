//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：Util.cs
//文件功能：能够根据分页按钮生成HTML接口
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
using System.Web.Routing;
using System.Web.Mvc;

namespace App.Framework.Web.Pager
{
    /// <summary>
    /// 能够根据分页按钮生成HTML接口
    /// </summary>
    public interface IGeneratePagerHtmlAble
    {
        /// <summary>
        /// 分页设置
        /// </summary>
        PagerSettings PagerSetting { get; set; }

        /// <summary>
        /// 生成分页的HTML代码
        /// </summary>
        /// <param name="pageButtons"></param>
        /// <returns></returns>
        MvcHtmlString Generate(IList<PageButton> pageButtons);

    
        /// <summary>
        /// 取得URL的回调
        /// </summary>
        Func<RequestContext, string, object, string> UrlCallback { set; }
    }
}
