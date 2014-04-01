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
using System.Web.Routing;

namespace App.Framework.Web.Pager
{
    /// <summary>
    /// 分页设置
    /// </summary>
    public class PagerSettings
    {

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PagerSettings()
        {
            NumericPagerCount = 10;
            MorePageText = "...";
            FirstPageText = "首页";
            PrevPageText = "上一页";
            NextPageText = "下一页";
            LastPageText = "末页";

            TagName = "div";
            TagID = "_MvcPagerx_";
            PageParameterName = "pageindex";

            JumpToFormat = "转到第{0}页";
            BtnJumpText = "确定";
            OverNumStr = "请输入1到{0}的数字";
        }

        public string JumpToFormat
        {
            get;
            set;
        }

        
        public string OverNumStr
        {
            get;
            set;
        }

        public string BtnJumpText
        { 
            get;
            set;
        }


        public string ToolTipFormat
        {
            get;
            set;
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount { get; set; }
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        internal int PageCount { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPageIndex { get; set; }
        /// <summary>
        /// 请求上下文
        /// </summary>
        internal RequestContext HtmlRequestContext { get; set; }
        /// <summary>
        /// 数字分页总数
        /// </summary>
        public int NumericPagerCount { get; set; }
        /// <summary>
        /// 当前页数字格式化字符串
        /// </summary>
        public string CurrentPageNumberFormatString { get; set; }
        /// <summary>
        /// 数字字格式化字符串
        /// </summary>
        public string PageNumberFormatString { get; set; }
        /// <summary>
        /// 是否显示更多
        /// </summary>
        public bool ShowMorePagerItems { get; set; }
        /// <summary>
        /// 显示更多的文本
        /// </summary>
        public string MorePageText { get; set; }
        /// <summary>
        /// 第一页文本
        /// </summary>
        public string FirstPageText { get; set; }
        /// <summary>
        /// 前一页文本
        /// </summary>
        public string PrevPageText { get; set; }
        /// <summary>
        /// 下一页文本
        /// </summary>
        public string NextPageText { get; set; }
        /// <summary>
        /// 最后一页文本
        /// </summary>
        public string LastPageText { get; set; }

        /// <summary>
        /// 主容器标签名
        /// </summary>
        public string TagName { get; set; }
        /// <summary>
        /// 主容器标签名CSS样式名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 主容器标签ID
        /// </summary>
        public string TagID { get; set; }
        /// <summary>
        /// 分页在浏览器中带的参数（名称）
        /// </summary>
        public string PageParameterName { get; set; }

        /// <summary>
        /// 在只有一页时是否显示
        /// </summary>
        private bool _ShowWherOnlyOnePages = true;
        public bool ShowWherOnlyOnePages
        {
            get { return _ShowWherOnlyOnePages; }
            set { _ShowWherOnlyOnePages = value; }
        }

        /// <summary>
        /// 是否显示跳转按钮
        /// </summary>
        public bool HideGoToButton { get; set; }


    }
}
