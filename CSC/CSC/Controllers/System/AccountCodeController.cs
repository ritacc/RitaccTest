using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Framework;
using App.Framework.Web;
using App.Framework.Web.Filters;
using CSC.Code;
using CSC.Business;
using CSC.Resources;

namespace CSC.Controllers
{
	public class AccountCodeController : XController
	{
		public ActionResult Index()
		{
			return View();
		}
		#region AccountCode

		
		[AuthorizationFilter((int)EnumPermission.CSCP03080_View)]
		public ActionResult AccountCodeList(AccountCodeListModel model)
		{
			model = model ?? new AccountCodeListModel();
			model.SearchAccountCode = model.SearchAccountCode ?? new SearchAccountCodeCriteria();
			var searchAccountCode = new SearchAccountCodeCriteria()
			{
				ACCT_TYPE_ID = model.SearchAccountCode.ACCT_TYPE_ID,
				ACCT_DESC = model.SearchAccountCode.ACCT_DESC
			};

			//
			List<Comparison<AccountCodeModel>> compList = new List<Comparison<AccountCodeModel>>();
			compList.Add((x, y) => x.ACCT_TYPE_CODE.CompareTo(y.ACCT_TYPE_CODE));
			compList.Add((x, y) => x.AcctDesc.CompareTo(y.AcctDesc));
			

			//
			model.AccountCodeList = SessionCache.Instance.GetOrSetCache<BusinessList<AccountCodeModel>>("AccountCodeList",
				() => BusinessPortal.Search<AccountCodeModel>(searchAccountCode), !this.IsSortingOrPageing());

			return ViewList(model, (pageparams) =>
			{
				int recordCount;
				pageparams.SecondSortFields = new List<KeyValuePair<int, SortDirectionEnum>>() { };
				pageparams.SecondSortFields.Add(new KeyValuePair<int, SortDirectionEnum>(0, SortDirectionEnum.Asc));

				model.AccountCodeList = model.AccountCodeList.Page(pageparams, out recordCount, compList);
				return recordCount;
			});
		}

		#endregion

		#region "Create Page"

		[AuthorizationFilter((int)EnumPermission.CSCP03080_Add)]
		public ActionResult AccountCodeCreate()
		{
			return View();
		}

		[AuthorizationFilter((int)EnumPermission.CSCP03080_Add)]
		[HttpPost]
		public ActionResult AccountCodeCreate(AccountCodeModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(model);
				}

				model.RecordStatus = EnumRecordStatus.ADD.ToString();
				BusinessResult result = BusinessPortal.Save(model);
				if (result.ResultType == 0)
				{
					SessionCache.Instance.Remove("AccountCodeList");
					this.ShowMessageEx(BusinessResultMessage.INF_SAVE_SUCCEED, isSucessed: true);
				}
				else if (result.ResultType == -9999)
				{
					if (result.GetMessage().IndexOf("UK_ACCT_CODE") > 0)
					{
						this.ShowMessageEx("RECORD CONTAINS DUPLICATE DATA", isSucessed: false);

					}
					else
					{
						this.ShowMessageEx(BusinessResultMessage.INF_SAVE_FAILED, isSucessed: false);
					}
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

		#endregion

		#region "Edit Page"

		[AuthorizationFilter((int)EnumPermission.CSCP03080_Edit)]
		public ActionResult AccountCodeEdit()
		{
			var loadBrandCode = new LoadAccountCodeCriteria()
			{
				AcctId = long.Parse(Request.QueryString["AcctId"])
			};
			var model = BusinessPortal.Load<AccountCodeModel>(loadBrandCode);

			return View(model);
		}

		[AuthorizationFilter((int)EnumPermission.CSCP03080_Edit)]
		[HttpPost]
		public ActionResult AccountCodeEdit(AccountCodeModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(model);
				}

				model.RecordStatus = EnumRecordStatus.EDIT.ToString();
				BusinessResult result = BusinessPortal.Save(model);
				if (result.ResultType == 0)
				{
					SessionCache.Instance.Remove("AccountCodeList");
					this.ShowMessageEx(BusinessResultMessage.INF_SAVE_SUCCEED, isSucessed: true);
				}
				else if (result.ResultType == -9999)
				{
					if (result.GetMessage().IndexOf("UK_ACCT_CODE") > 0)
					{
						this.ShowMessageEx("RECORD CONTAINS DUPLICATE DATA", isSucessed: false);

					}
					else
					{
						this.ShowMessageEx(BusinessResultMessage.INF_SAVE_FAILED, isSucessed: false);
					}
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

		#endregion

		#region "Delete Function"

		[AuthorizationFilter((int)EnumPermission.CSCP03080_Delete)]
		public ActionResult AccountCodeDelete()
		{
			try
			{
				if (string.IsNullOrEmpty(Request.QueryString["AcctId"].ToString()))
				{
					throw new Exception();
				}
				var brandCodeModel = new AccountCodeModel()
				{
					AcctId = long.Parse(Request.QueryString["AcctId"].ToString())
				};

				BusinessResult result = BusinessPortal.Delete(brandCodeModel);
				if (result.ResultType == 0)
				{
					SessionCache.Instance.Remove("AccountCodeList");
					return this.ShowMessageResult(BusinessResultMessage.INF_DELETE_SUCCEED, isSucessed: true, btnSureClickScript: "window.parent.location.reload();");
				}
				else if (result.ResultType == -9999)
				{
					return this.ShowMessageResult(BusinessResultMessage.INF_DELETE_FAILED, isSucessed: false);
				}
				else
				{
					return this.ShowMessageResult(result.GetMessage(), isSucessed: false);
				}
			}
			catch (Exception ex)
			{
				this.ShowMessage(ex.Message, isSucessed: false);
				return this.Message(ex.Message);
			}
		}

		#endregion

		
	}
}
