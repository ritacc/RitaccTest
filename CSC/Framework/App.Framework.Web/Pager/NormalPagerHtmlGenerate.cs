//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：NormalPagerHtmlGenerate.cs
//文件功能：能够根据分页按钮生成HTML的默认实现
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
using System.Text.RegularExpressions;

namespace App.Framework.Web.Pager
{
    /// <summary>
    /// 普通分页HTML代码生成类
    /// </summary>
    internal class NormalPagerHtmlGenerate : IGeneratePagerHtmlAble
    {

        private Func<RequestContext, string, object, string> _GetUrlCallback;


        /// <summary>
        /// 分页设置
        /// </summary>
        public PagerSettings PagerSetting { get; set; }




        private string WrapPageButton(PageButton btn)
        {
            string result = string.Empty;
            if (btn.Disabled)
                return String.Format("<li><a disabled=\"disabled\">{0}</a></li>", btn.Text);
            result = String.Format("<li><a href='{0}'>{1}</a></li>", _GetUrlCallback(PagerSetting.HtmlRequestContext, PagerSetting.PageParameterName, btn.PageIndex), btn.Text);
            return result;
        }

        private string WrapChangePageSizeButton(int pageSize)
        {
            string result = string.Empty;
            if (PagerSetting.PageSize == pageSize)
                return String.Format("<a disabled=\"disabled\">{0}</a>", pageSize);
            string url =_GetUrlCallback(PagerSetting.HtmlRequestContext, "pageSize", pageSize);

            Regex reg = new Regex(PagerSetting.PageParameterName + "=\\d*"); //换页大小的话，就回到第一页
            url = reg.Replace(url, PagerSetting.PageParameterName + "=1");
            result = String.Format("<a href='{0}'>{1}</a>", url, pageSize);

            return result;
        }

        /// <summary>
        /// 生成HTML代码
        /// </summary>
        /// <param name="pageButtons">按钮列表</param>
        /// <returns></returns>
        public MvcHtmlString Generate(IList<PageButton> pageButtons)
        {
            TagBuilder tagBuilder = new TagBuilder(PagerSetting.TagName);
            tagBuilder.GenerateId(PagerSetting.TagID);
            if (!string.IsNullOrEmpty(PagerSetting.ClassName))
                tagBuilder.AddCssClass(PagerSetting.ClassName);
            StringBuilder sb = new StringBuilder();
            foreach (PageButton btn in pageButtons)
            {
                if (!btn.Hide)
                    sb.Append(WrapPageButton(btn));
            }



            tagBuilder.InnerHtml = sb.ToString();
            string clickEvent = _GetUrlCallback(PagerSetting.HtmlRequestContext, PagerSetting.PageParameterName, 0);
            if (!PagerSetting.HideGoToButton)
            {
                string textGoTo = @"<input id='txtgoto' style='width:24px;' maxLength=""9"" type=""text"" class=""goto"">";
                string gotoPage =
    @"<li class=""alone"">
{0}
<input type='button' class='sure' value='{1}' 
onclick="" var pageindex = $('#txtgoto').val(); 
if (/[^\d]/.test(pageindex) || pageindex <= 0 || pageindex > {2}){{
    alert('{3}');
    return false;
}}

var locationhref = 'location.href=\'{4}\'';  
locationhref = locationhref.replace(/pageindex=/,'pageindex=' + pageindex);  
eval(locationhref);   
"" height=""21"" width=""62"" >
</li>";

                gotoPage = string.Format(gotoPage, string.Format(PagerSetting.JumpToFormat, textGoTo), PagerSetting.BtnJumpText, PagerSetting.PageCount, string.Format(PagerSetting.OverNumStr, PagerSetting.PageCount), clickEvent.Replace(PagerSetting.PageParameterName + "=0", PagerSetting.PageParameterName + "="));
                tagBuilder.InnerHtml += gotoPage;
            }
            string pagerHtml = tagBuilder.ToString(TagRenderMode.Normal);


            string changePageSizeStr = WrapChangePageSizeButton(10) + WrapChangePageSizeButton(20) + WrapChangePageSizeButton(50) + WrapChangePageSizeButton(100);

            string pagerToolTip = string.Format(PagerSetting.ToolTipFormat, PagerSetting.RecordCount, PagerSetting.PageCount, changePageSizeStr, PagerSetting.CurrentPageIndex);
            TagBuilder toolTip = new TagBuilder("span");
            toolTip.AddCssClass("fLeft");
            toolTip.InnerHtml = pagerToolTip;

            return MvcHtmlString.Create(pagerHtml + toolTip.ToString());
        }

        /// <summary>
        /// 设置URL回调
        /// </summary>
        public Func<RequestContext, string, object, string> UrlCallback
        {
            set
            {
                _GetUrlCallback = value;
            }
        }
    }

}
