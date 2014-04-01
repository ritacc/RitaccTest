using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Framework.Web.Pager;

namespace CSC
{
    public static class QuickPagerHelper
    {
        public static MvcHtmlString QuickPager(this HtmlHelper helper)
        {
            return helper.Pager(Convert.ToInt32(helper.ViewData["RecordCount"]), Convert.ToInt32(helper.ViewData["PageSize"]), new App.Framework.Web.Pager.PagerSettings()
            {
                TagName = "ul",
                ClassName = "flip_t",
                ToolTipFormat = CSC.Resources.GlobalText.PagerToolTip,
                PrevPageText = CSC.Resources.GlobalText.PagerPrevious,
                NextPageText = CSC.Resources.GlobalText.PagerNext,
                FirstPageText = CSC.Resources.GlobalText.PagerFirst,
                LastPageText = CSC.Resources.GlobalText.PagerLast,
                BtnJumpText = CSC.Resources.GlobalText.PagerBtnJumpText,
                JumpToFormat =  CSC.Resources.GlobalText.PagerJumpToFormat,
                OverNumStr = CSC.Resources.GlobalText.PagerOverNumStr
            });
        }
    }
}