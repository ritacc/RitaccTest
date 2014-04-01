using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSC.Business;
using App.Framework.Web.Pager;
using App.Framework.Web;
using App.Framework;
using App.Framework.Security;
using CSC.Code;
using System.Data;
using App.Framework.Web.Filters;

namespace CSC.Controllers
{
	/// <summary>
	/// The controller of Role
	/// </summary>
	public class RoleController : XController
	{
		#region "Index"
		[AuthorizationFilter((int)EnumPermission.Role_View)]
		public ActionResult Index()
		{
			return View();
		}
		#endregion

		#region "List"
		[AuthorizationFilter((int)EnumPermission.Role_View)]
		public ActionResult List(RoleListModel model)
		{
			model = model ?? new RoleListModel();
			model.RoleSearch = model.RoleSearch ?? new SearchRoleCriteria();
			var search = new SearchRoleCriteria()
			{
				//RoleDescFrom = model.RoleSearch.RoleDescFrom,
				//RoleDescTo = model.RoleSearch.RoleDescTo,
				ROLE_CODE = model.RoleSearch.ROLE_CODE,
				RoleType = model.RoleSearch.RoleType,
				AdminFlag = model.RoleSearch.AdminFlag
			};


			List<Comparison<Role>> compList = new List<Comparison<Role>>();
			compList.Add((x, y) => x.RoleCode.CompareTo(y.RoleCode));
			//commented by jason in 20121219 for TSYF02010#06.doc
			//compList.Add((x, y) => x.RoleDsc.CompareTo(y.RoleDsc));
			//end commented by jason in 20121219 for TSYF02010#06.doc
			//compList.Add((x, y) => x.RoleType.CompareTo(y.RoleType));
			//commented by jason in 20121219 for TSYF02010#06.doc
			//compList.Add((x, y) => x.SystemScope.CompareTo(y.SystemScope));
			//end commented by jason in 20121219 for TSYF02010#06.doc
			//compList.Add((x, y) => x.AdminFlag.CompareTo(y.AdminFlag));
			compList.Add((x, y) => x.CreationDate.CompareTo(y.CreationDate));
			//commented by jason in 20121219 for TSYF02010#06.doc
			//compList.Add((x, y) => x.FrozenFlag.CompareTo(y.FrozenFlag));
			//compList.Add((x, y) => (x.FrozenDate.HasValue ? x.FrozenDate.Value.Format() : "").CompareTo(y.FrozenDate.HasValue ? y.FrozenDate.Value.Format() : ""));
			//commented by jason in 20121219 for TSYF02010#06.doc

			model.RoleList = SessionCache.Instance.GetOrSetCache<BusinessList<Role>>("roleSearch", () => BusinessPortal.Search<Role>(search), !this.IsSortingOrPageing());

			PagerHelper.SetSortParamsToViewData(ViewData, new PageParams() { SortField = 0, sortDirection = SortDirectionEnum.Asc });

			return ViewList(model, (pageparams) =>
			{
				model.RoleForDropDownList = BusinessPortal.Search<Role>(new SearchRoleForDDLCriteria());
				int recordCount;
				model.RoleList = model.RoleList.Page(pageparams, out recordCount, compList);
				return recordCount;
			});
		}
		#endregion

		#region "Edit"
		[AuthorizationFilter((int)EnumPermission.Role_View)]
		public ActionResult View(int? roleId)
		{
			return DoEdit(roleId, (model) => View("~/Views/Role/Edit.aspx", model));
		}

		[AuthorizationFilter((int)EnumPermission.Role_Add)]
		public ActionResult Add(int? roleId)
		{
			return DoEdit(roleId, (model) => View("~/Views/Role/Edit.aspx",model));
		}

		[AuthorizationFilter((int)EnumPermission.Role_Edit)]
		public ActionResult Edit(int? roleId)
		{
			return DoEdit(roleId, (model) => View(model));
		}

		public ActionResult DoEdit(int? roleId, Func<object,ActionResult> View)
		{
			var roleSearch = new SearchRoleCriteria()
			{
				RoleID = roleId.HasValue ? roleId.Value : -1
			};

			RoleListModel viewModel = new RoleListModel()
			{
				RoleModel = BusinessPortal.Load<Role>(roleSearch)
			};

			return View(viewModel);
		}
		
		[HttpPost]
		[AuthorizationFilter((int)EnumPermission.Role_Add)]
		public ActionResult Add(RoleListModel vm)
		{
			return DoEdit(vm, () => View("~/Views/Role/Edit.aspx"));
		}

		[HttpPost]
		[AuthorizationFilter((int)EnumPermission.Role_Edit)]
		public ActionResult Edit(RoleListModel vm)
		{
			return DoEdit(vm,()=>View());
		}

		public ActionResult DoEdit(RoleListModel vm,Func<ActionResult> View)
		{
			if (vm.RoleModel == null)
				return View();
			try
			{
				BusinessResult result = BusinessPortal.Save(vm.RoleModel);


				if (result.ResultType == 0)
					this.ShowMessage(Resources.BusinessResultMessage.INF_SAVE_SUCCEED, isSucessed: true);
				else
					this.ShowMessage(result.GetMessage(), isSucessed: false);


				return View();
			}
			catch (Exception ex)
			{
				this.ShowMessage(ex.Message, isSucessed: false);
				return View();
			}
		}

		#endregion

		#region "Role Func"
		[AuthorizationFilter((int)EnumPermission.Role_View)]
		public ActionResult RoleFunc(int? roleId, bool? adminFlag)
		{
			var roleFuncSearch = new SearchRoleFuncCriteria()
			{
				RoleID = roleId.HasValue ? roleId.Value : -1,
				AdminFlag = adminFlag.HasValue ? adminFlag.Value : false
			};

			var roleSearch = new SearchRoleCriteria()
			{
				RoleID = roleId.HasValue ? roleId.Value : -1
			};

			RoleListModel viewModel = new RoleListModel()
			{
				RoleModel = BusinessPortal.Load<Role>(roleSearch)
			};
			if (viewModel.RoleModel != null)
			{
				viewModel.RoleModel.RoleFuncList = BusinessPortal.Search<RoleFunc>(roleFuncSearch);
			}
			return View(viewModel);
		}

		//save role func
		[HttpPost]
		[AuthorizationFilter((int)EnumPermission.Role_Edit)]
		public ActionResult RoleFunc(RoleListModel vm)
		{
			if (string.IsNullOrEmpty(Request["hRoleId"]))
				return View(vm);

			IDbTransaction tran = BusinessPortal.BeginTransaction();
			try
			{
				//清空所有的角色-功能
				int roleId = Request["hRoleId"].ParseInt();
				var batchCriteria = new UpdateBatchRoleFuncCriteria()
				{
					RoleID = roleId
				};
				BusinessResult result;
				result = BusinessPortal.Execute(batchCriteria, tran);

				//重新修改功能
				var inertList = Request["chkRoleFuncInsert"].ToListByRequest(v => Convert.ToInt32(v));
				var updateList = Request["chkRoleFuncUpdate"].ToListByRequest(v => Convert.ToInt32(v));
				var activeList = Request["chkRoleFuncActive"].ToListByRequest(v => Convert.ToInt32(v));

				UpdateRoleFuncCriteria roleFuncCriteria;
				bool insertFlag, updateFlag;
				if (activeList != null) {
					foreach (var active in activeList)
					{
						roleFuncCriteria = new UpdateRoleFuncCriteria();
						roleFuncCriteria.RoleID = roleId;
						roleFuncCriteria.FuncID = active;

						insertFlag = false;
						if (inertList != null) {
							foreach (var insert in inertList)
							{
								if (insert == active)
								{
									insertFlag = true;
									break;
								}
							}
						}
						roleFuncCriteria.InsertableFlag = insertFlag;

						updateFlag = false;
						if (updateList != null) {
							foreach (var update in updateList)
							{
								if (update == active)
								{
									updateFlag = true;
									break;
								}
							}
						}
						roleFuncCriteria.UpdatableFlag = updateFlag;

						result = BusinessPortal.Execute(roleFuncCriteria, tran);
					}
				}
				tran.Commit();

				//清空页面所有已经存在的权限
				App.Framework.Web.Permissions.UserIdentityCollection.Instance.Clear();

				if (result.ResultType == 0)
					this.ShowMessage(Resources.BusinessResultMessage.INF_SAVE_SUCCEED, isSucessed: true);
				else
					this.ShowMessage(Resources.BusinessResultMessage.INF_SAVE_FAILED, isSucessed: false);
				return RoleFunc(roleId,Convert.ToBoolean(Request["chkAdminFlag"]));
			}
			catch (Exception ex)
			{
				tran.Rollback();
				this.ShowMessage(ex.Message, isSucessed: false);
				return RoleFunc(Request["hRoleId"].ParseInt(), Convert.ToBoolean(Request["chkAdminFlag"]));
			}
		}
		#endregion

		#region "User Role"
		[AuthorizationFilter((int)EnumPermission.Role_View)]
		public ActionResult UserRole(int? roleId)
		{
			var userRoleSearch = new SearchUserRoleCriteria()
			{
				RoleID = roleId.HasValue ? roleId.Value : -1
			};

			var roleSearch = new SearchRoleCriteria()
			{
				RoleID = roleId.HasValue ? roleId.Value : -1
			};

			RoleListModel viewModel = new RoleListModel()
			{
				RoleModel = BusinessPortal.Load<Role>(roleSearch)
			};
			if (viewModel.RoleModel != null)
			{
				viewModel.RoleModel.UserRoleList = BusinessPortal.Search<UserRole>(userRoleSearch);
			}

			//added by jason in 20121219 for TSYF02010#06.doc
			List<Comparison<UserRole>> compList = new List<Comparison<UserRole>>();
			compList.Add((x, y) => x.ShopCode.CompareTo(y.ShopCode));
			compList.Add((x, y) => x.UserType.CompareTo(y.UserType));
			compList.Add((x, y) => x.UserCode.CompareTo(y.UserCode));

			return ViewList(viewModel, (pageparams) =>
			{
				int recordCount;
				viewModel.RoleModel.UserRoleList = viewModel.RoleModel.UserRoleList.Page(pageparams, out recordCount, compList);
				return recordCount;
			});
			//end added by jason in 20121219 for TSYF02010#06.doc

			//commented by jason in 20121219 for TSYF02010#06.doc
			//return View(viewModel);
			//end commented by jason in 20121219 for TSYF02010#06.doc
		}
		#endregion

		#region ModifyFrozenFlag
		[AuthorizationFilter((int)EnumPermission.Role_Edit)]
		public ActionResult Frozen(string roleID, bool frozenFlag)
		{
			if (string.IsNullOrEmpty(roleID))
			{
				throw new Exception();
			}

			try
			{
				Role role = new Role()
				{
					RoleID = int.Parse(roleID),
					FrozenFlag = frozenFlag
				};


				BusinessResult result = BusinessPortal.Delete(role);

				if (result.ResultType == 0)
				{
					if (frozenFlag)
					{
						result.ResultMessage = Resources.GlobalText.FrozenSuccess;
					}
					else
					{
						result.ResultMessage = Resources.GlobalText.CancelFrozenSuccess;
					}
				}
				else 
				{
					if (frozenFlag)
					{
						result.ResultMessage = Resources.GlobalText.FrozenNoSuccess;
					}
					else
					{
						result.ResultMessage = Resources.GlobalText.CancelFrozenNoSuccess;
					}
				}

				App.Framework.Web.Permissions.UserIdentityCollection.Instance.Clear();
                return this.ShowMessageResult(result.ResultMessage, isSucessed: true, btnSureClickScript: "window.parent.loadFrame(window.parent.$(\"#tabList\"), \"List\", true);");
				
			}
			catch (Exception ex)
			{
				this.ShowMessage(ex.Message, isSucessed: false);
				return this.Message(ex.Message);
			}
		}

		#endregion

		#region delete

		[AuthorizationFilter((int)EnumPermission.Role_Delete)]
		public ActionResult Delete()
		{
			try
			{
				if (string.IsNullOrEmpty(Request.QueryString["id"].ToString()))
				{
					throw new Exception();
				}

				DeleteRoleCriteria delete = new DeleteRoleCriteria()
				{
					RoleId = long.Parse(Request.QueryString["id"].ToString())
				};


				BusinessResult result = BusinessPortal.Execute(delete);


				if (result.ResultType == 0)
				{
					return this.ShowMessageResult(CSC.Resources.BusinessResultMessage.INF_DELETE_SUCCEED, isSucessed: true, btnSureClickScript: "window.parent.location.reload();");
				}
				else if (result.ResultType == -9999)
				{
					return this.ShowMessageResult(CSC.Resources.BusinessResultMessage.INF_DELETE_FAILED, isSucessed: false);
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

		public ActionResult ShopList()
		{
			//commented by Jason in 20120308 for SYSTEM05#13.doc
			//SearchShopForLovCriteria searchModel = new SearchShopForLovCriteria();
			//BusinessList<Shop> list = BusinessPortal.Search<Shop>(searchModel);

			//Added by Jason in 20120308 for SYSTEM05#13.doc
			SearchShopByUserID searchModel = new SearchShopByUserID()
			{
				UserID = App.Framework.Security.User.Current.UserId
			};
			BusinessList<Shop> list = BusinessPortal.Search<Shop>(searchModel);

			return View("Shop", new ShopViewModel()
			{
				ShopList = list
			});
		}
	}
}
