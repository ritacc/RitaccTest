//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：Util.cs
//文件功能：分页中需要用到的获取按钮URL
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
using System.Web.Mvc;
using System.ComponentModel;
using System.Web.Routing;

namespace App.Framework.Web.Pager
{
    /// <summary>
    /// 生成分页代码
    /// </summary>
    internal class PagerBuilder
    {
        /// <summary>
        /// 生成分页按钮
        /// </summary>
        public IPageAble ProcPager { get; set; }
        /// <summary>
        /// 生成HTML
        /// </summary>
        public IGeneratePagerHtmlAble PagerHtmlGenerate { get; set; }

        /// <summary>
        /// 默认构造
        /// </summary>
        public PagerBuilder()
        {
            ProcPager = new NormalPageProc();
            PagerHtmlGenerate = new NormalPagerHtmlGenerate();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="procPager"></param>
        /// <param name="pagerHtmlGenerate"></param>
        public PagerBuilder(IPageAble procPager, IGeneratePagerHtmlAble pagerHtmlGenerate)
        {
            ProcPager = procPager;
            PagerHtmlGenerate = pagerHtmlGenerate;
        }


        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="recordCount">记录总数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="settings">生成设置</param>
        /// <param name="urlCallback">生成设置</param>
        /// <returns></returns>
        public MvcHtmlString Build(int recordCount, int pageIndex, int pageSize, PagerSettings settings, Func<RequestContext, string, object, string> urlCallback =null )
        {
            settings.RecordCount = recordCount;
            settings.PageSize = pageSize;
            if (recordCount <= 0) return MvcHtmlString.Create("");
            int pageCount = (recordCount + pageSize - 1) / pageSize;
            if (pageCount <= 1 && !settings.ShowWherOnlyOnePages) return MvcHtmlString.Create("");
            if (pageIndex <= 0)
            {
                string pageindexStr =settings.HtmlRequestContext ==null ? "1" : settings.HtmlRequestContext.HttpContext.Request[settings.PageParameterName];
                Int32.TryParse(pageindexStr, out pageIndex);
            }
            if (pageIndex <= 0) pageIndex = 1;

            settings.CurrentPageIndex = pageIndex;
            settings.PageCount = pageCount;

            ProcPager.PagerSetting = settings;
            PagerHtmlGenerate.PagerSetting = settings;
            PagerHtmlGenerate.UrlCallback = urlCallback ?? Util.GetUrl;

            IList<PageButton> pageBtnList = new List<PageButton>();
            if (pageCount <= 1) pageBtnList = ProcPager.GetBtnLessThanOnePage();
            else if (pageIndex == 1) pageBtnList = ProcPager.GetBtnWhenFrist();
            else if (pageIndex == pageCount) pageBtnList = ProcPager.GetBtnWhenLast();
            else pageBtnList = ProcPager.GetBtnWhenDefault();

            return PagerHtmlGenerate.Generate(pageBtnList);
        }

       

    }


}