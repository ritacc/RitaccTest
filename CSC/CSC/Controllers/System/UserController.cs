using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSC.Business;
using App.Framework.Web.Pager;
using App.Framework.Web;
using App.Framework;
using CSC.Code;
using App.Framework.Security;
using System.Data;
using App.Framework.Web.Filters;


namespace CSC.Controllers
{
	/// <summary>
	/// The controller of Role
	/// </summary>
	public class UserController : XController
	{
		#region "Index"
		[AuthorizationFilter((int)EnumPermission.User_View)]
		public ActionResult Index()
		{
			return View();
		}
		#endregion

		#region "List"
		[AuthorizationFilter((int)EnumPermission.User_View)]
		public ActionResult List(UserViewModel model)
		{
			model = model ?? new UserViewModel();
			model.UserSearch = model.UserSearch ?? new SearchUserCriteria();
			var search = new SearchUserCriteria()
			{
				UserId = model.UserSearch.UserId,
                USER_CODE = model.UserSearch.USER_CODE,
				RoleID = model.UserSearch.RoleID,
				UserName = model.UserSearch.UserName,
				//SuspendFlag = model.UserSearch.Suspend ? "Y" : "N"
                Suspend = model.UserSearch.Suspend
			};


			List<Comparison<UserModel>> compList = new List<Comparison<UserModel>>();
			compList.Add((x, y) => x.UserCode.CompareTo(y.UserCode));
			//commented by jason in 20121219 for TSYF02010#06.doc
			//compList.Add((x, y) => x.UserName.CompareTo(y.UserName));
			//end commented by jason in 20121219 for TSYF02010#06.doc
			compList.Add((x, y) => x.UserType.CompareTo(y.UserType));
			compList.Add((x, y) => x.CreationDate.CompareTo(y.CreationDate));
			compList.Add((x, y) => x.ShopCount.CompareTo(y.ShopCount));
			compList.Add((x, y) => x.SystemScope.CompareTo(y.SystemScope));
			//commented by jason in 20121219 for TSYF02010#06.doc
			//compList.Add((x, y) => x.SuspendFlag.CompareTo(y.SuspendFlag));
			//compList.Add((x, y) => (x.SuspendDate.HasValue ? x.SuspendDate.Value.Format() : "").CompareTo(y.SuspendDate.HasValue ? y.SuspendDate.Value.Format() : ""));
			//compList.Add((x, y) => x.FrozenFlag.CompareTo(y.FrozenFlag));
			//compList.Add((x, y) => (x.FrozenDate.HasValue ? x.FrozenDate.Value.Format() : "").CompareTo(y.FrozenDate.HasValue ? y.FrozenDate.Value.Format() : ""));
			//end commented by jason in 20121219 for TSYF02010#06.doc

			//model.UserList = BusinessPortal.Search<UserModel>(search);

			model.UserList = SessionCache.Instance.GetOrSetCache<BusinessList<UserModel>>("userSearch", () => BusinessPortal.Search<UserModel>(search), !this.IsSortingOrPageing());

			PagerHelper.SetSortParamsToViewData(ViewData, new PageParams() { SortField = 0, sortDirection = SortDirectionEnum.Asc });

			return ViewList(model, (pageparams) =>
			{
				model.RoleForDropDownList = BusinessPortal.Search<Role>(new SearchRoleForDDLCriteria());
				model.UserForDropDownList = BusinessPortal.Search<UserModel>(new SearchUserForDDLCriteria());
				int recordCount;
				model.UserList = model.UserList.Page(pageparams, out recordCount, compList);
				return recordCount;
			});
		}
		#endregion

		#region "Edit"
		[AuthorizationFilter((int)EnumPermission.User_View)]
		public ActionResult View(long? userId)
		{
			return DoEdit(userId, (model) => View("~/Views/User/Edit.aspx", model));
		}

		[AuthorizationFilter((int)EnumPermission.User_Add)]
		public ActionResult Add(long? userId)
		{
			return DoEdit(userId, (model) => View("~/Views/User/Edit.aspx", model));
		}

		[AuthorizationFilter((int)EnumPermission.User_Edit)]
		public ActionResult Edit(long? userId)
		{
			return DoEdit(userId, (model) => View(model));
		}

		#region added by Jason in 20120913 for remove user role list in add/edit page
		public ActionResult DoEdit(long? userId, Func<object, ActionResult> View)
		{
			var userSearch = new SearchUserCriteria()
			{
				UserId = userId.HasValue ? userId.Value : -1
			};

			var userShopCriteria = new UserShopInUserCriteria()
			{
				UserId = userId.HasValue ? userId.Value : -1
			};

			UserViewModel viewModel = new UserViewModel()
			{
				UserModel = BusinessPortal.Load<UserModel>(userSearch),
				UserShopList = BusinessPortal.Search<UserShop>(userShopCriteria)
			};
			return View(viewModel);
		}
		#endregion

		#region commented by Jason in 20120913 for remove user role list in add/edit page
		//public ActionResult DoEdit(long? userId, Func<object, ActionResult> View)
		//{
		//    var userSearch = new SearchUserCriteria()
		//    {
		//        UserId = userId.HasValue ? userId.Value : -1
		//    };

		//    var userShopCriteria = new UserShopInUserCriteria()
		//    {
		//        UserId = userId.HasValue ? userId.Value : -1
		//    };

		//    var userRoleCriteria = new UserRoleInUserCriteria()
		//    {
		//        UserId = userId.HasValue ? userId.Value : -1
		//    };

		//    UserViewModel viewModel = new UserViewModel()
		//    {
		//        UserModel = BusinessPortal.Load<UserModel>(userSearch),
		//        UserShopList = BusinessPortal.Search<UserShop>(userShopCriteria),
		//        UserRoleList = BusinessPortal.Search<UserRole>(userRoleCriteria)
		//    };
		//    return View(viewModel);
		//}
		#endregion

		[HttpPost]
		[AuthorizationFilter((int)EnumPermission.User_Add)]
		public ActionResult Add(UserViewModel vm)
		{
			//return DoEdit(vm, () => View("~/Views/User/Edit.aspx"));
			SessionCache.Instance.Remove("userSearch");
			return DoEdit(vm);
		}

		[HttpPost]
		[AuthorizationFilter((int)EnumPermission.User_Edit)]
		public ActionResult Edit(UserViewModel vm)
		{
			return DoEdit(vm);
		}

		#region added by Jason in 20120913 for remove user role list in add/edit page
		[HttpPost]
		public ActionResult DoEdit(UserViewModel vm)
		{
			bool isEdit = (vm.UserModel.UserId > 0);
			if (vm.UserModel == null) {
				if (isEdit)
				{
					return Edit(vm.UserModel.UserId);
				}
				else
				{
					return Add(vm.UserModel.UserId);
				}
			}
			IDbTransaction tran = BusinessPortal.BeginTransaction();

		    try
		    {
				//保存用户基本信息
				BusinessResult result = BusinessPortal.Save(vm.UserModel, tran);

				//新增或者编辑，保存之后的USERID
				long userId = vm.UserModel.UserId;

				var shopList = Request["chkUserShopActive"].ToListByRequest(v => v.ToString());
				var shopListAnti = Request["chkUserShopActive-Anti"].ToListByRequest(v => v.ToString());
				
				//保存成功之后，再保存SHOP,ROLE
				if (result.ResultType == 0)//新增
				{
					if (!isEdit)
					{
						if (shopList != null)
						{
							foreach (var shopCode in shopList)
							{
								//新增user_shop
								result = BusinessPortal.Execute(new AddUserShopCriteria() { ShopCode = shopCode, UserID = userId }, tran);
							}
						}
					}
					else //编辑
					{		
						//查找已经存在的店
						BusinessList<Shop> exsitShopList = BusinessPortal.Search<Shop>(new SearchShopByUserID() { UserID = userId });

						bool existFlag;
						//选择的店列表
						if (shopList != null)
						{
							foreach (var shopCode in shopList)
							{
								existFlag = false;
								foreach (Shop exsitShop in exsitShopList)
								{
									if (shopCode == exsitShop.Code)
									{
										existFlag = true;
										break;
									}
								}
								//如果数据库不存在此店，则新增
								if (!existFlag)
								{
									//新增user_shop
									result = BusinessPortal.Execute(new AddUserShopCriteria() { ShopCode = shopCode, UserID = userId }, tran);
								}
							}
						}
						//未被选择的店列表
						if (shopListAnti != null)
						{
							foreach (var shopCode in shopListAnti)
							{
								existFlag = false;
								foreach (Shop exsitShop in exsitShopList)
								{
									if (shopCode == exsitShop.Code)
									{
										existFlag = true;
										break;
									}
								}
								//如果数据库不存在此店，则新增
								if (existFlag)
								{
									//清空当前店对应的sy_user_role_shop
									result = BusinessPortal.Execute(new DeleteUserRoleShopByShopCriteria() { UserID = userId, ShopCode = shopCode }, tran);
									result = BusinessPortal.Execute(new DeleteUserShopByShopCriteria() { UserID = userId, ShopCode = shopCode }, tran);
									result = BusinessPortal.Execute(new DeleteUserRoleNotInUserCriteria() { UserID = userId }, tran);
								}
							}
						}
					}
					//清空选择用户的权限
					App.Framework.Web.Permissions.UserIdentityCollection.Instance.ClearUserIdentity(userId.ToString());
				}

                if (result.ResultType != 0)
                {
                    tran.Rollback();
                    this.ShowMessage(result.GetMessage(), isSucessed: false);

                    if (isEdit)
                    {
                        return Edit(vm.UserModel.UserId);
                    }
                    else
                    {
                        return Add(vm.UserModel.UserId);
                    }
                }

				//added by jason in 20120308 for SYSTEM05#13.doc NO.1
				GetUserShopCountCriteria GetUserShopCount = new GetUserShopCountCriteria()
				{
					UserID = userId
				};
				BusinessPortal.Execute(GetUserShopCount, tran);
				if (GetUserShopCount.UserShopCount <= 0)
				{
					tran.Rollback();
					this.ShowMessage(CSC.Resources.GlobalText.MustSelectShop, isSucessed: false);
				}
				else
				{
				//end added by jason in 20120308 for SYSTEM05#13.doc NO.1
					tran.Commit();

					if (result.ResultType == 0)
					{
						this.ShowMessage(Resources.BusinessResultMessage.INF_SAVE_SUCCEED, isSucessed: true);
					}
					else
					{
						this.ShowMessage(result.GetMessage(), isSucessed: false);
					}
				}
				//return Edit(vm.UserModel.UserId);
				if (isEdit)
				{
					return Edit(vm.UserModel.UserId);
				}
				else
				{
					return Add(vm.UserModel.UserId);
				}
		    }
		    catch (Exception ex)
		    {
				tran.Rollback();
		        this.ShowMessage(ex.Message, isSucessed: false);
				//return Edit(vm.UserModel.UserId);
				if (isEdit)
				{
					return Edit(vm.UserModel.UserId);
				}
				else
				{
					return Add(vm.UserModel.UserId);
				}
		    }

		}
		#endregion

		#region commented by Jason in 20120913 for remove user role list in add/edit page
		//[HttpPost]
		//public ActionResult DoEdit(UserViewModel vm)
		//{
		//    bool isEdit = (vm.UserModel.UserId > 0);
		//    if (vm.UserModel == null)
		//    {
		//        if (isEdit)
		//        {
		//            return Edit(vm.UserModel.UserId);
		//        }
		//        else
		//        {
		//            return Add(vm.UserModel.UserId);
		//        }
		//    }
		//    IDbTransaction tran = BusinessPortal.BeginTransaction();

		//    try
		//    {
		//        //保存用户基本信息
		//        BusinessResult result = BusinessPortal.Save(vm.UserModel, tran);

		//        //新增或者编辑，保存之后的USERID
		//        long userId = vm.UserModel.UserId;

		//        var shopList = Request["chkUserShopActive"].ToListByRequest(v => v.ToString());
		//        var roleList = Request["chkUserRoleActive"].ToListByRequest(v => Convert.ToInt32(v));

		//        //保存成功之后，再保存SHOP,ROLE
		//        if (result.ResultType == 0)
		//        {
		//            //新增
		//            if (!isEdit)
		//            {
		//                //当前用户为公司用户时，新增用户
		//                if (App.Framework.Security.User.Current.UserType == UserTypes.NS.ToString())
		//                {
		//                    //新增user_shop

		//                    result = BusinessPortal.Execute(new AddUserShopCriteria() { ShopCode = App.Framework.Security.User.Current.ShopCode, UserID = userId }, tran);
		//                    if (roleList != null)
		//                    {
		//                        foreach (var roleId in roleList)
		//                        {
		//                            //新增user_role
		//                            result = BusinessPortal.Execute(new AddUserRoleCriteria() { RoleID = roleId, UserID = userId }, tran);
		//                            //新增user_role_shop
		//                            result = BusinessPortal.Execute(new AddUserRoleShopCriteria() { ShopCode = App.Framework.Security.User.Current.ShopCode, RoleID = roleId, UserID = userId }, tran);
		//                        }
		//                    }
		//                }
		//                else
		//                { //当前用户为系统管理员时，新增用户

		//                    if (roleList != null)
		//                    {
		//                        foreach (var roleId in roleList)
		//                        {
		//                            //新增user_role
		//                            result = BusinessPortal.Execute(new AddUserRoleCriteria() { RoleID = roleId, UserID = userId }, tran);
		//                        }
		//                    }

		//                    if (shopList != null)
		//                    {
		//                        foreach (var shopCode in shopList)
		//                        {
		//                            //新增user_shop
		//                            result = BusinessPortal.Execute(new AddUserShopCriteria() { ShopCode = shopCode, UserID = userId }, tran);


		//                            if (roleList != null)
		//                            {
		//                                foreach (var roleId in roleList)
		//                                {
		//                                    //新增user_role_shop
		//                                    result = BusinessPortal.Execute(new AddUserRoleShopCriteria() { ShopCode = shopCode, RoleID = roleId, UserID = userId }, tran);
		//                                }
		//                            }
		//                        }
		//                    }
		//                }
		//            }
		//            else
		//            {//编辑 
		//                //当前用户为公司用户时，编辑用户
		//                if (App.Framework.Security.User.Current.UserType == UserTypes.NS.ToString())
		//                {
		//                    //先清空当前店对应的sy_user_role_shop
		//                    result = BusinessPortal.Execute(new DeleteUserRoleShopByShopCriteria()
		//                    {
		//                        UserID = userId
		//                        ,
		//                        ShopCode = App.Framework.Security.User.Current.ShopCode
		//                    }, tran);

		//                    BusinessList<UserRole> userRoleList = BusinessPortal.Search<UserRole>(new SearchUserRoleByUserCriteria()
		//                    {
		//                        UserID = userId
		//                    });
		//                    bool existFlag;
		//                    if (roleList != null)
		//                    {
		//                        foreach (var roleId in roleList)
		//                        {
		//                            existFlag = false;
		//                            if (userRoleList != null)
		//                            {
		//                                foreach (UserRole role in userRoleList)
		//                                {
		//                                    if (roleId == role.RoleID)
		//                                    {
		//                                        existFlag = true;
		//                                        break;
		//                                    }
		//                                }
		//                            }
		//                            //页面新增的ROLE不在数据库中，则新增
		//                            if (!existFlag)
		//                            {
		//                                //新增user_role
		//                                result = BusinessPortal.Execute(new AddUserRoleCriteria() { RoleID = roleId, UserID = userId }, tran);
		//                            }
		//                        }
		//                    }

		//                    //if (roleList != null) //commented by jason in 20120308 for SYSTEM05#13.doc NO.1
		//                    if (shopList != null && roleList != null) //added by jason in 20120308 for SYSTEM05#13.doc NO.1
		//                    {
		//                        foreach (var roleId in roleList)
		//                        {
		//                            //新增user_role_shop
		//                            result = BusinessPortal.Execute(new AddUserRoleShopCriteria() { ShopCode = App.Framework.Security.User.Current.ShopCode, RoleID = roleId, UserID = userId }, tran);
		//                        }
		//                    }

		//                    //清除多余的ROLE
		//                    BusinessPortal.Execute(new DeleteUserRoleNotInUserCriteria() { UserID = userId }, tran);

		//                    //added by jason in 20120308 for SYSTEM05#13.doc NO.1
		//                    //清除多余的店
		//                    BusinessPortal.Execute(new DeleteUserShopNotInUserCriteria() { UserID = userId }, tran);
		//                    //end added by jason in 20120308 for SYSTEM05#13.doc NO.1
		//                }
		//                else
		//                {
		//                    //当前用户为系统管理员时，编辑用户
		//                    //先清空sy_user_role_shop，登录用户所属店之外的数据留住
		//                    BusinessPortal.Execute(new DeleteUserRoleShopCriteria() { UserID = userId }, tran);
		//                    //清空sy_user_role
		//                    BusinessPortal.Execute(new DeleteUserRoleCriteria() { UserID = userId }, tran);

		//                    //查找已经存在的店
		//                    BusinessList<Shop> exsitShopList = BusinessPortal.Search<Shop>(new SearchShopByUserID() { UserID = userId });

		//                    if (roleList != null)
		//                    {
		//                        foreach (var roleId in roleList)
		//                        {
		//                            //新增user_role
		//                            result = BusinessPortal.Execute(new AddUserRoleCriteria() { RoleID = roleId, UserID = userId }, tran);
		//                        }
		//                    }

		//                    if (shopList != null)
		//                    {
		//                        bool existFlag;
		//                        foreach (var shopCode in shopList)
		//                        {
		//                            existFlag = false;
		//                            foreach (Shop exsitShop in exsitShopList)
		//                            {
		//                                if (shopCode == exsitShop.Code)
		//                                {
		//                                    existFlag = true;
		//                                    break;
		//                                }
		//                            }
		//                            //如果数据库不存在此店，则新增
		//                            if (!existFlag)
		//                            {
		//                                //新增user_shop
		//                                result = BusinessPortal.Execute(new AddUserShopCriteria() { ShopCode = shopCode, UserID = userId }, tran);
		//                            }

		//                            if (roleList != null)
		//                            {
		//                                foreach (var roleId in roleList)
		//                                {
		//                                    //新增user_role_shop
		//                                    result = BusinessPortal.Execute(new AddUserRoleShopCriteria() { ShopCode = shopCode, RoleID = roleId, UserID = userId }, tran);
		//                                }
		//                            }
		//                        }
		//                    }

		//                    //清除多余的ROLE
		//                    BusinessPortal.Execute(new DeleteUserRoleNotInUserCriteria() { UserID = userId }, tran);
		//                    //清除多余的店
		//                    BusinessPortal.Execute(new DeleteUserShopNotInUserCriteria() { UserID = userId }, tran);
		//                }
		//                //清空选择用户的权限
		//                App.Framework.Web.Permissions.UserIdentityCollection.Instance.ClearUserIdentity(userId.ToString());
		//            }
		//        }
		//        //added by jason in 20120308 for SYSTEM05#13.doc NO.1
		//        GetUserShopCountCriteria GetUserShopCount = new GetUserShopCountCriteria()
		//        {
		//            UserID = userId
		//        };
		//        BusinessPortal.Execute(GetUserShopCount, tran);
		//        if (GetUserShopCount.UserShopCount <= 0)
		//        {
		//            tran.Rollback();
		//            this.ShowMessage(CSC.Resources.GlobalText.MustSelectShop, isSucessed: false);
		//        }
		//        else
		//        {
		//            //end added by jason in 20120308 for SYSTEM05#13.doc NO.1
		//            tran.Commit();

		//            if (result.ResultType == 0)
		//            {
		//                this.ShowMessage(Resources.BusinessResultMessage.INF_SAVE_SUCCEED, isSucessed: true);
		//            }
		//            else
		//            {
		//                this.ShowMessage(result.GetMessage(), isSucessed: false);
		//            }
		//        }
		//        //return Edit(vm.UserModel.UserId);
		//        if (isEdit)
		//        {
		//            return Edit(vm.UserModel.UserId);
		//        }
		//        else
		//        {
		//            return Add(vm.UserModel.UserId);
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        tran.Rollback();
		//        this.ShowMessage(ex.Message, isSucessed: false);
		//        //return Edit(vm.UserModel.UserId);
		//        if (isEdit)
		//        {
		//            return Edit(vm.UserModel.UserId);
		//        }
		//        else
		//        {
		//            return Add(vm.UserModel.UserId);
		//        }
		//    }

		//}
		#endregion
		#endregion

		#region "UserRole"
		[AuthorizationFilter((int)EnumPermission.User_View)]
		public ActionResult UserRole(long? userId, UserViewModel model)
		{
			model = model ?? new UserViewModel();
			model.ShopRoleSearch = model.ShopRoleSearch ?? new UserRoleShopInUserCriteria();
			

			//string shop = Request["ShopRoleSearch.ShopCode"];
			var userSearch = new SearchUserCriteria()
			{
				UserId = userId.HasValue ? userId.Value : -1
			};

			string shopCode = string.Empty;
			//如果当前用户是公司用户，ShopCode=当前店,否则=选择的店
			if(App.Framework.Security.User.Current.UserType == CSC.Business.UserTypes.NS.ToString()){
				shopCode = App.Framework.Security.User.Current.ShopCode;
			}
			else{
				//第一次进入此页面时，model.ShopRoleSearch.ShopCode == null。默认选择第DDL里面的第一个shop
				/*if (model.ShopRoleSearch.ShopCode == null)
				{
					BusinessList<Shop> shopList = BusinessPortal.Search<Shop>(new SearchShopByUserIDInUserCriteria() { UserId = userId.HasValue ? userId.Value : -1 });
					if (shopList != null && shopList.Count > 0) 
					{
						shopCode = shopList[0].Code;
					}
				}
				else {*/
					shopCode = model.ShopRoleSearch.ShopCode;
				//}
			}
			var search = new UserRoleShopInUserCriteria()
			{
				UserId = userId.Value,
				ShopCode = shopCode
			};
		    var userRoleShoplist = string.IsNullOrEmpty(search.ShopCode)
		                               ? new BusinessList<UserRole>()
		                               : BusinessPortal.Search<UserRole>(search);

			UserViewModel viewModel = new UserViewModel()
			{
				UserModel = BusinessPortal.Load<UserModel>(userSearch),
				//只加载登录用户权限范围店铺
				ShopForDropDownList = BusinessPortal.Search<Shop>(new SearchShopByUserIDInUserCriteria() { UserId = userId.HasValue ? userId.Value : -1 }),
				UserRoleShopList = userRoleShoplist
			};
			return View(viewModel);
		}

		//save user role
		[HttpPost]
		[AuthorizationFilter((int)EnumPermission.User_Edit)]
		public ActionResult UserRole(UserViewModel vm)
		{
			int userId = Request["hUserId"].ParseInt();

			IDbTransaction tran = BusinessPortal.BeginTransaction();
			try
			{
				var roleList = Request["chkUserRoleActive"].ToListByRequest(v => Convert.ToInt32(v));

				var shopCode = string.Empty;
				//如果当前用户为公司用户，
				BusinessResult result;
				if (App.Framework.Security.User.Current.UserType == UserTypes.NS.ToString())
				{
					shopCode = App.Framework.Security.User.Current.ShopCode;
					//先清空当前店对应的sy_user_role_shop
					result = BusinessPortal.Execute(new DeleteUserRoleShopByShopCriteria() { UserID = userId, ShopCode = shopCode }, tran);
				}
				else { 
					//获取DDL选择的数据
					if (string.IsNullOrEmpty(Request["hShopCode"]))
					{
						BusinessList<Shop> shopList = BusinessPortal.Search<Shop>(new SearchShopByUserIDInUserCriteria() { UserId = userId });
						if (shopList != null && shopList.Count > 0)
						{
							shopCode = shopList[0].Code;
						}
						
					}
					else {
						shopCode = Request["hShopCode"].ToString();
					}
					//先清空当前店对应的sy_user_role_shop
					result = BusinessPortal.Execute(new DeleteUserRoleShopByShopCriteria() { UserID = userId, ShopCode = shopCode }, tran);
				}

				BusinessList<UserRole> userRoleList = BusinessPortal.Search<UserRole>(new SearchUserRoleByUserCriteria() { UserID = userId });
				bool existFlag;
				if (roleList != null)
				{
					foreach (var roleId in roleList)
					{
						existFlag = false;
						if (userRoleList != null)
						{
							foreach (UserRole role in userRoleList)
							{
								if (roleId == role.RoleID)
								{
									existFlag = true;
									break;
								}
							}
						}
						//页面新增的ROLE不在数据库中，则新增
						if (!existFlag)
						{
							//新增user_role
							result = BusinessPortal.Execute(new AddUserRoleCriteria() { RoleID = roleId, UserID = userId }, tran);
						}
					}
				}

				if (roleList != null)
				{
					foreach (var roleId in roleList)
					{
						//新增user_role_shop
						result = BusinessPortal.Execute(new AddUserRoleShopCriteria() { ShopCode = shopCode, RoleID = roleId, UserID = userId }, tran);
					}
				}

				//清除多余的ROLE
				result = BusinessPortal.Execute(new DeleteUserRoleNotInUserCriteria() { UserID = userId }, tran);

				//清空选择用户的权限
				App.Framework.Web.Permissions.UserIdentityCollection.Instance.ClearUserIdentity(userId.ToString());

				tran.Commit();

				if (result.ResultType == 0)
					this.ShowMessage(Resources.BusinessResultMessage.INF_SAVE_SUCCEED, isSucessed: true);
				else
					this.ShowMessage(Resources.BusinessResultMessage.INF_SAVE_FAILED, isSucessed: false);
				return UserRole(userId, vm);
			}
			catch (Exception ex)
			{
				tran.Rollback();
				this.ShowMessage(ex.Message, isSucessed: false);
				return UserRole(userId, vm);
			}

		}

		//弹出功能列表
		[AuthorizationFilter((int)EnumPermission.User_View)]
		public ActionResult RoleFunc(int? roleId)
		{
			var roleFuncSearch = new SearchRoleFuncByRoleCriteria()
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
				viewModel.RoleModel.RoleFuncList = BusinessPortal.Search<RoleFunc>(roleFuncSearch);
			}
			return View(viewModel);
		}

		#endregion

		#region "Cancel Frozen"
		[AuthorizationFilter((int)EnumPermission.User_Edit)]
		public ActionResult CancelFrozen(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new Exception();
			}

			try
			{
				CancelFrozenUserCriteria cancelFrozen = new CancelFrozenUserCriteria()
				{
					UserId = int.Parse(userId)
				};


				BusinessResult result = BusinessPortal.Execute(cancelFrozen);

				if (result.ResultType == 0)
				{
					result.ResultMessage = Resources.GlobalText.CancelLockSuccess;
				}
				else
				{
					result.ResultMessage = Resources.GlobalText.CancelLockNoSuccess;
				}

				//清空选择用户的权限
				App.Framework.Web.Permissions.UserIdentityCollection.Instance.ClearUserIdentity(userId.ToString());
				return this.ShowMessageResult(result.ResultMessage, isSucessed: true, btnSureClickScript: "window.parent.loadFrame(window.parent.$(\"#tabList\"), \"List\", true);");
				
			}
			catch (Exception ex)
			{
				this.ShowMessage(ex.Message, isSucessed: false);
				return this.Message(ex.Message);
			}
		}
		#endregion

		#region "Suspend,Cancel Suspend"
		[AuthorizationFilter((int)EnumPermission.User_Edit)]
		public ActionResult Suspend(string userId, bool suspendFlag)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new Exception();
			}

			try
			{
				UserModel cancelFrozen = new UserModel()
				{
					UserId = int.Parse(userId),
					SuspendFlag = suspendFlag
				};


				BusinessResult result = BusinessPortal.Delete(cancelFrozen);

				if (result.ResultType == 0)
				{
					if (suspendFlag)
					{
						result.ResultMessage = Resources.GlobalText.SuspendSuccess;
					}
					else
					{
						result.ResultMessage = Resources.GlobalText.CancelSuspendSuccess;
					}
				}
				else
				{
					if (suspendFlag)
					{
						result.ResultMessage = Resources.GlobalText.SuspendNoSuccess;
					}
					else
					{
						result.ResultMessage = Resources.GlobalText.CancelSuspendNoSuccess;
					}
				}

				//清空选择用户的权限
				App.Framework.Web.Permissions.UserIdentityCollection.Instance.ClearUserIdentity(userId.ToString());
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

		[AuthorizationFilter((int)EnumPermission.User_Delete)]
		public ActionResult Delete()
		{
			try
			{
				if (string.IsNullOrEmpty(Request.QueryString["id"].ToString()))
				{
					throw new Exception();
				}

				DeleteUserCriteria delete = new DeleteUserCriteria()
				{
					UserId = long.Parse(Request.QueryString["id"].ToString()) 
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

	}
}
