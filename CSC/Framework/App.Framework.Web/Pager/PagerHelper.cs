//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：PagerHelper.cs
//文件功能：分页中需要用到HtmlHelper类扩展
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
using System.Web;

namespace App.Framework.Web.Pager
{
    /// <summary>
    /// 分页中需要用到HtmlHelper类扩展
    /// </summary>
    public static class PagerHelper
    {
        /// <summary>
        /// 生成分页代码
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="recordCount">总页数</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, int recordCount, int PageSize)
        {
            return Pager(helper, recordCount, -1, PageSize, new PagerSettings());
        }

        /// <summary>
        /// 生成分页代码
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="recordCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, int recordCount, int PageSize, PagerSettings settings)
        {
            return Pager(helper, recordCount, -1, PageSize, settings);
        }


        /// <summary>
        /// 生成分页代码
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="recordCount">总页数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="settings">设置项</param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, int recordCount, int pageIndex, int PageSize, PagerSettings settings)
        {
            PagerBuilder pb = new PagerBuilder();
            settings.HtmlRequestContext = helper.ViewContext.RequestContext;
            return pb.Build(recordCount, pageIndex, PageSize, settings);
        }

        /// <summary>
        /// 生成ajax的分页
        /// </summary>
        /// <param name="recordCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static MvcHtmlString AjaxPager(int recordCount, int pageIndex, int PageSize, PagerSettings settings)
        {
            PagerBuilder pb = new PagerBuilder();
            settings.HideGoToButton = true;
            settings.HtmlRequestContext = null;
            return pb.Build(recordCount, pageIndex, PageSize, settings, (context, reques, pageindex) => "javascript:page(" + pageindex + ");");
        }


        /// <summary>
        /// 获取分页(排序)参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static PageParams GetPageParams(HttpRequestBase request, ViewDataDictionary viewData)
        {
            int pageSize = request["PageSize"].ParseInt();
            int pageIndex = request["PageIndex"].ParseInt();
            int? sortField = request["SortField"] == null ? null : (int?)request["SortField"].ParseInt();
            string sortFieldName = request["SortFieldName"];
            SortDirectionEnum? sortDirection = request["SortDirection"] == null ? null : (SortDirectionEnum?)request["SortDirection"].ParseInt();

            PageParams viewDataPageParams = viewData.GetPageParams();
            //if (pageSize != viewDataPageParams.PageSize)
            //    pageIndex = 1; //改变了分页大小后自动跳到第一页
            PageParams pageParams = new PageParams()
            {
                PageSize = pageSize <= 0 ? viewDataPageParams.PageSize : pageSize,
                PageIndex = pageIndex <= 0 ? viewDataPageParams.PageIndex : pageIndex,
                SortField = sortField == null ? viewDataPageParams.SortField : sortField.Value,
                SortFiledName = sortFieldName,
                sortDirection = sortDirection == null ? viewDataPageParams.sortDirection : sortDirection.Value
            };

            return pageParams;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static PageParams GetPageParams(this HttpRequestBase request)
        {
            int pageSize = request["PageSize"].ParseInt();
            int pageIndex = request["PageIndex"].ParseInt();
            int sortField = request["SortField"].ParseInt();
            string sortFieldName = request["SortFiledName"];

            SortDirectionEnum? sortDirection = (SortDirectionEnum?)request["SortDirection"].ParseInt();


            PageParams pageParams = new PageParams()
            {
                PageSize = pageSize <= 0 ? 50 : pageSize,
                PageIndex = pageIndex <= 0 ? 1 : pageIndex,
                SortField = sortField < 0 ? 0 : sortField,
                SortFiledName = sortFieldName,
                sortDirection = sortDirection == null ? SortDirectionEnum.Asc : sortDirection.Value
            };

            return pageParams;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewData"></param>
        /// <returns></returns>
        public static PageParams GetPageParams(this ViewDataDictionary viewData)
        {
            int pageSize = viewData["PageSize"] == null ? 0 : Convert.ToInt32(viewData["PageSize"]);
            int pageIndex = viewData["PageIndex"] == null ? 0 : Convert.ToInt32(viewData["PageIndex"]); ;
            int sortField = viewData["SortField"] == null ? 0 : Convert.ToInt32(viewData["SortField"]);
            string sortFieldName = viewData["SortFieldName"] == null ? string.Empty : viewData["SortFieldName"].ToString();
            SortDirectionEnum? sortDirection = viewData["SortDirection"] == null ? null : (SortDirectionEnum?)viewData["SortDirection"].ToString().ParseInt();

            PageParams pageParams = new PageParams()
            {
                PageSize = pageSize <= 0 ? 50 : pageSize,
                PageIndex = pageIndex <= 0 ? 1 : pageIndex,
                SortField = sortField,// < 0 ? 0 : sortField,
                sortDirection = sortDirection == null ? SortDirectionEnum.Asc : sortDirection.Value
            };
            return pageParams;

        }



        /// <summary>
        /// 设置分页参数到
        /// </summary>
        /// <param name="viewData"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageSize"></param>
        public static void SetPageParmsToViewData(this ViewDataDictionary viewData, int recordCount, int pageSize)
        {
            viewData["RecordCount"] = recordCount;
            viewData["PageSize"] = pageSize;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewData"></param>
        /// <param name="pageParams"></param>
        public static void SetSortParamsToViewData(this ViewDataDictionary viewData, PageParams pageParams)
        {
            viewData["SortField"] = pageParams.SortField;
            viewData["SortDirection"] = (int)pageParams.sortDirection;
            viewData["PageIndex"] = pageParams.PageIndex;
            viewData["PageSize"] = pageParams.PageSize;
            viewData["SortFiledName"] = pageParams.SortFiledName;
        }
    }
}



