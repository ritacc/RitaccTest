using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Framework;
using App.Framework.Web.Pager;
using App.Framework.Web.Filters;
using Resources;
using App.Framework.Web.Json;
using App.Framework.Web;
using CSC.Code;
using CSC.Business;
using CSC.Resources;
using App.Framework.Security;

namespace CSC.Controllers
{
	public class ShopController : XController
	{

		public ActionResult Index()
		{
			return View();
		}


		[AuthorizationFilter((int)EnumPermission.Shop_View)]
		public ActionResult ShopList(ShopListModel model)
		{
			model = model ?? new ShopListModel();
			model.Search = model.Search ?? new SearchShopCriteria();

			var search = new SearchShopCriteria()
			{
				UserId = App.Framework.Security.User.Current.UserId,
				SysCode = SecurityPortal.ApplicationName,
				Code = model.Search.Code
			};
			List<Comparison<Shop>> compList = new List<Comparison<Shop>>();
			compList.Add((x, y) => x.Code.CompareTo(y.Code));
			compList.Add((x, y) => x.Name.CompareTo(y.Name));
			//compList.Add((x, y) => x.FullName.CompareTo(y.FullName));
			model.List = SessionCache.Instance.GetOrSetCache<BusinessList<Shop>>("shop", () => BusinessPortal.Search<Shop>(search), !this.IsSortingOrPageing());
			PagerHelper.SetSortParamsToViewData(ViewData, new PageParams() { SortField = 0, sortDirection = SortDirectionEnum.Asc });

			return ViewList(model, (pageparams) =>
			{
				int recordCount;
				model.List = model.List.Page(pageparams, out recordCount, compList);
				return recordCount;
			});
		}

        [AuthorizationFilter((int)EnumPermission.Shop_View)]
        public ActionResult detail(string Code)
        {
            var search = new spLoadShopViewCriteria()
            {
                SysCode = SecurityPortal.ApplicationName,
                Code = Code
            };
            Shop entity = BusinessPortal.Load<Shop>(search);
            return View(entity);
        }



		[AuthorizationFilter((int)EnumPermission.Shop_Edit)]
		public ActionResult Edit(string Code)
		{
			var search = new LoadShopCriteria()
			{
				SysCode = SecurityPortal.ApplicationName,
				Code = Code
			};
			Shop entity = BusinessPortal.Load<Shop>(search);
			return View(entity);
		}

		[HttpPost]
		[AuthorizationFilter((int)EnumPermission.Shop_Edit)]
		public ActionResult Edit(Shop model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(model);
				}
				model.RecordStatus = EnumRecordStatus.EDIT.ToString();
				model.SysCode = SecurityPortal.ApplicationName;
				BusinessResult result = BusinessPortal.Save(model);
				if (result.ResultType == 0)
				{
					this.ShowMessage(CSC.Resources.BusinessResultMessage.INF_SAVE_SUCCEED, isSucessed: true);
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

		[AuthorizationFilter((int)EnumPermission.Shop_Edit)]
		public ActionResult SetDefaultGodown()
		{
			DefualtGodownModel model = new DefualtGodownModel();
			model.Code = Request.QueryString["CODE"];

			//ParameterResultLong result = BusinessPortal.Load<ParameterResultLong>(new GetDefaultGodownByWH()
			//{
			//    WH_CODE = model.Code
			//});
			long? val=QuickInvoke.GetDefultGodown(model.Code );
			if(val.HasValue)
			{
				model.GodownID = val.Value;  //result.Result.Value;
			 }

			if (model.GodownID == -1)
				model.GodownID = null;

			return View(model);
		}



		[AuthorizationFilter((int)EnumPermission.Shop_Edit)]
		public ActionResult SetPassWord(string Code)
		{
			var search = new LoadShopCriteria()
			{
				SysCode = SecurityPortal.ApplicationName,
				Code = Code
			};
			Shop entity = BusinessPortal.Load<Shop>(search);
			return View(entity);
		}

		[HttpPost]
		[AuthorizationFilter((int)EnumPermission.Shop_Edit)]
		public ActionResult SetPassWord(string AUTH_PWD, string Code)
		{
			string msg = string.Empty;
			bool success = false;
			QuickInvoke.InovkeWithTryCatch(() =>
			{
				var a = QuickInvoke.Encrypt(AUTH_PWD);
				var Criteria = new EditPwdCriteria() { AUTH_PWD = QuickInvoke.Encrypt(AUTH_PWD), Code = Code };
				QuickInvoke.DbOpreation2(() => BusinessPortal.Execute(Criteria));
				success = true;
				msg = BusinessResultMessage.INF_SAVE_SUCCEED;
			}, (errmsg) =>
			{
				msg = BusinessResultMessage.INF_SAVE_FAILED;
				this.ShowMessageEx(errmsg, isSucessed: false);
			});
			if (success)
				this.ShowMessageEx(msg, isSucessed: true);
			return View();
		}



		[HttpPost]
		[AuthorizationFilter((int)EnumPermission.Shop_Edit)]
		public ActionResult SetDefaultGodown(DefualtGodownModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(model);
				}
				
				BusinessResult result = BusinessPortal.Save(model);

				if (result.ResultType == 0)
				{
					SessionCache.Instance.Remove("SalesReturnList");
					this.ShowMessageEx(BusinessResultMessage.INF_SAVE_SUCCEED, isSucessed: true);
				}
				else if (result.ResultType == -9999)
				{
					this.ShowMessageEx(BusinessResultMessage.INF_SAVE_FAILED, isSucessed: false);
				}
				else
				{
					this.ShowMessageEx(result.GetMessage(), isSucessed: false);
				}

				return View(model);
			}
			catch (Exception ex)
			{
				this.ShowMessage(ex.Message, isSucessed: false);
				return View(model);
			}
		}

		[HttpGet]
		public JsonResult CheckShopAuthPwd(string pwd)
		{

			var search = new LoadShopCriteria()
			{
				SysCode = SecurityPortal.ApplicationName,
				Code = App.Framework.Security.User.Current.ShopCode
			};
			Shop entity = BusinessPortal.Load<Shop>(search);


			if (QuickInvoke.Encrypt(pwd) == entity.AUTH_PWD)
			{
				return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
			}
			else
			{
				return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
			}
		}


	}
}
