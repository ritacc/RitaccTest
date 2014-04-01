using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSC.Business;
using App.Framework.Web.Pager;
using App.Framework;
using App.Framework.Web.Filters;
using App.Framework.Web;
using App.Framework.Web.Json;
using CSC.Code;
using CSC.Resources;

namespace CSC.Controllers
{
	public class FunctionController : XController
    {
        [AuthorizationFilter((int)EnumPermission.Function_View)]
        public ActionResult IndexDefault(FunctionListModel model)
        {
            model = model ?? new FunctionListModel();

            return View("Index",model);
        }

		[AuthorizationFilter((int)EnumPermission.Function_View)]
		public ActionResult Index(FunctionListModel model)
		{
			model = model ?? new FunctionListModel();

			//功能列表
			model.SearchFunction = model.SearchFunction ?? new SearchFunctionCriteria();
			var searchFunction = new SearchFunctionCriteria()
			{
				AdminFlag = model.SearchFunction.AdminFlag,
                FunctionCode = model.SearchFunction.FunctionCode,
                FunctionDSC = model.SearchFunction.FunctionDSC
			};
			
			//排序分页
			List<Comparison<Function>> compList = new List<Comparison<Function>>();
			compList.Add((x, y) => x.FuncCode.CompareTo(y.FuncCode));
			compList.Add((x, y) => x.Dsc.CompareTo(y.Dsc));
			//compList.Add((x, y) => x.Executable.CompareTo(y.Executable));
			//compList.Add((x, y) => x.SystemScope.CompareTo(y.SystemScope));
			compList.Add((x, y) => x.FuncType.CompareTo(y.FuncType));


			//返回列表内容并创建缓存
			model.FunctionList = SessionCache.Instance.GetOrSetCache<BusinessList<Function>>("FunctionList", () => BusinessPortal.Search<Function>(searchFunction), !this.IsSortingOrPageing());
			//设置默认排序
			PagerHelper.SetSortParamsToViewData(ViewData, new PageParams() { SortField = 0, sortDirection = SortDirectionEnum.Asc });
			//返回列表
			return ViewList(model, (pageparams) =>
			{
				int recordCount;
				model.FunctionList = model.FunctionList.Page(pageparams, out recordCount, compList);
				return recordCount;
			});
		}

        #region Edit 功能暂时不用

        [AuthorizationFilter((int)EnumPermission.Function_Edit)]
		public ActionResult Edit()
		{
            //List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "FORM", Value = "FORM", Selected = true });
            //items.Add(new SelectListItem { Text = "BTN", Value = "BTN" });
            //items.Add(new SelectListItem { Text = "RPT", Value = "RPT" });
            //this.ViewData["FuncTypeItems"] = items;

            var loadFunction = new LoadFunctionCriteria()
            {
                FuncId = int.Parse(Request.QueryString["FuncId"].ToString())
            };
            var function = BusinessPortal.Load<Function>(loadFunction);
            return View(function); 
		}

		[AuthorizationFilter((int)EnumPermission.Function_Edit)]
		[HttpPost]
		public ActionResult Edit(Function model)
		{
            try
            {
                //List<SelectListItem> items = new List<SelectListItem>();
                //items.Add(new SelectListItem { Text = "FORM", Value = "FORM", Selected = true });
                //items.Add(new SelectListItem { Text = "BTN", Value = "BTN" });
                //items.Add(new SelectListItem { Text = "RPT", Value = "RPT" });
                //this.ViewData["FuncTypeItems"] = items;

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                model.RecordStatus = EnumRecordStatus.EDIT.ToString();
                BusinessResult result = BusinessPortal.Save(model);
                if (result.ResultType == 0)
                {
                    this.ShowMessage(BusinessResultMessage.INF_SAVE_SUCCEED, isSucessed: true);
                }
                else if (result.ResultType == -9999)
                {
                    this.ShowMessage(BusinessResultMessage.INF_SAVE_FAILED, isSucessed: false);
                }
                else
                {
                    this.ShowMessage(result.GetMessage(), isSucessed: false);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, isSucessed: false);
                return View(model);
            }
		}

        #endregion

        public JsonResult GetFunction(long FuncId) {
            var search = new LoadFunctionCriteria() {
                 FuncId = FuncId
            };
            var model = BusinessPortal.Load<Function>(search);
            return Json(
                new {
                    FuncDesc = model.Dsc
                }, JsonRequestBehavior.AllowGet);

        }
    }
}
