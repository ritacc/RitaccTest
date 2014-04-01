using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSC.Business;
using App.Framework.Web.Pager;
using App.Framework;
using App.Framework.Security;
using System.Data;
using App.Framework.Web.Filters;
using App.Framework.Web;
using App.Framework.Web.Permissions;

namespace CSC.Controllers
{
	public class ParameterController : Controller
	{
		[AuthorizationFilter((int)EnumPermission.Parameter_View)]
		public ActionResult Index(ParameterListModel model)
		{
			model = model ?? new ParameterListModel();
			model.Search = model.Search ?? new SearchParameterCriteria();
			SearchParameterCriteria search = new SearchParameterCriteria() 
			{
				SysCode = SecurityPortal.ApplicationName,
				ShopCode = App.Framework.Security.User.Current.ShopCode
			};

			model.List = search.List();

			//List<Comparison<Parameter>> compList = new List<Comparison<Parameter>>();
			//compList.Add((x, y) => x.Code.CompareTo(y.Code));
			//compList.Add((x, y) => x.ParaType.CompareTo(y.ParaType));

			//PageParams pageParms = Request.GetPageParams(); 
			//PageParams pageParms = PagerHelper.GetPageParams(Request, ViewData);
			//int recordCount;
			//model.List = BusinessPortal.Page<Parameter>(search, pageParms, out recordCount); 

			//PagerHelper.SetPageParmsToViewData(ViewData, recordCount, pageParms.PageSize);
			//PagerHelper.SetSortParamsToViewData(ViewData, pageParms);

			return View(model);
		}

		public JsonResult GetValue(string paraID, string code, string value, DateTime lastUpdateDate)
		{
			List<TmpParameter> list = null;

			if (Session["TEMP_PARAMETER"] != null)
			{
				list = (List<TmpParameter>)Session["TEMP_PARAMETER"];
			}
			else
			{
				list = new List<TmpParameter>();
			} 

			bool isExistItem = false;
			foreach (TmpParameter item in list)
			{
				if (item.Id.Equals(paraID))
				{
					list.Remove(item);
					list.Add(new TmpParameter(item.Id, code, value, lastUpdateDate));

					isExistItem = true;
					break;
				}
			}

			if (!isExistItem)
			{
				list.Add(new TmpParameter(paraID, code, value, lastUpdateDate));
			} 

			Session["TEMP_PARAMETER"] = list;

			return Json("Success");
		}

		[AuthorizationFilter((int)EnumPermission.Parameter_Edit)]
		public ActionResult Edit()
		{ 
			List<TmpParameter> list = null;

            //Modify by JLD on 2012-11-15 清除权限
            PermissionsProviderFactory.ProvidePermissions.ClearUserIdentity(App.Framework.Security.User.Current.UserIdentity);

			IDbTransaction tran = BusinessPortal.BeginTransaction();

			try
			{ 
				if (Session["TEMP_PARAMETER"] != null)
				{
					list = (List<TmpParameter>)Session["TEMP_PARAMETER"];
					BusinessResult result = new BusinessResult();
					foreach (TmpParameter tmp in list)
					{
						Parameter p = new Parameter();
						p.ParaID = Convert.ToInt64(tmp.Id);
						p.Code = tmp.Code;
						p.Value = tmp.Value;
						p.LastUpdateDate = tmp.LastUpdateDate;
						p.SysCode = SecurityPortal.ApplicationName;
						p.ShopCode = App.Framework.Security.User.Current.ShopCode;
						p.LastUpdatedBy = App.Framework.Security.User.Current.UserId;
						p.ActionType = "EDIT";

						result = BusinessPortal.Save(p,tran);
					}

					tran.Commit();
					Session["TEMP_PARAMETER"] = null;

					switch (result.ResultType)
					{
						case 0:
							result.ResultMessage = CSC.Resources.BusinessResultMessage.INF_SAVE_SUCCEED;
							break;
						case -1003:
							result.ResultMessage = CSC.Resources.BusinessResultMessage.MSG_1003;
							break;
						default:
							result.ResultMessage = CSC.Resources.BusinessResultMessage.INF_SAVE_FAILED;
							break;
					}
					if (result.ResultType == 0)
					{
						this.ShowMessage(result.ResultMessage, isSucessed: true);
                        return View("Index", new ParameterListModel());
					}
					else
					{
						this.ShowMessage(result.ResultMessage, isSucessed: false);
                        return View("Index", new ParameterListModel());
					}
				}
			}
			catch(Exception e)
			{ 
				tran.Rollback();

				throw e;
			}
			finally
			{
				 
			} 

			//return View("Index", new ParameterListModel());
            return RedirectToAction("Index");
		}
	}

	/// <summary>
	/// 保存临时参数Key-Value
	/// </summary>
	public class TmpParameter
	{ 
		public TmpParameter(string id, string code, string value, DateTime lastUpdateDate)
		{
			this.id = id;
			this.Code = code;
			this.value = value;
			this.LastUpdateDate = lastUpdateDate;
		}

		private string id;

		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		public string Code { get;set; }

		private string value;

		public string Value
		{
			get { return this.value; }
			set { this.value = value; }
		}

		public DateTime LastUpdateDate { get; set; }
	}
}
