using System;
using System.Web.Mvc;
using App.Framework.Web;
using App.Framework.Web.Filters;
using App.Framework;
using App.Framework.Web.Pager;
namespace CSC.Code
{
    public class XController : Controller
    {
        public ActionResult ViewList(object model, Func<PageParams, int> setPageParams, string viewName = null)
        {
            ActionResultType resulttype = MessageHelper.GetActionResultTypeByRequest(ControllerContext.HttpContext.Request);
            if (resulttype == ActionResultType.Json || Request.IsAjaxRequest())
            {
                return model.ToJsonEntity();
            }

            var pageParms = PagerHelper.GetPageParams(Request, ViewData);
            int recordCount = setPageParams(pageParms);
            PagerHelper.SetPageParmsToViewData(ViewData, recordCount, pageParms.PageSize);
            PagerHelper.SetSortParamsToViewData(ViewData, pageParms);
			if (!string.IsNullOrEmpty(viewName))
			{
				return base.View(viewName, model);
			}
	        return base.View(model);
        }

        public ActionResult ViewList(Func<PageParams, int> setPageParams, Func<ActionResult> result, Func<ActionResult> jsonResult = null)
        {
            if (jsonResult != null)
            {
                ActionResultType resulttype = MessageHelper.GetActionResultTypeByRequest(ControllerContext.HttpContext.Request);
                if (resulttype == ActionResultType.Json || Request.IsAjaxRequest())
                {
                    return jsonResult();
                }
            }
            var pageParms = PagerHelper.GetPageParams(Request, ViewData);
            int recordCount = setPageParams(pageParms);
            PagerHelper.SetPageParmsToViewData(ViewData, recordCount, pageParms.PageSize);
            PagerHelper.SetSortParamsToViewData(ViewData, pageParms);
            return result();
        }
    }
}