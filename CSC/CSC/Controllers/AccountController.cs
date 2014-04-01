using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Framework.Web.User;
using CRM.Business;
using CSC;
using App.Framework.Security;
using CSC.Business.Entity.User;
using System.Web.Security;
using App.Framework.Web.Permissions;
using App.Framework;
using App.Framework.Web;
using CSC.Business;
using App.Framework.Web.Filters;
using System.Configuration;
using CSC.Code;
using CSC.Resources;


namespace CSC.Controllers
{
	public class AccountController : Controller
	{
		public ActionResult Index()
		{ 
			return View();
		}


		public ActionResult Login()
		{
		  
			LoginModel model = new LoginModel();
			 
			


			return View(model);
		}

		[HttpPost]
        
		public ActionResult Login(string username, string password, string shop, string returnUrl)
		{
			try
			{

                var user = UserExtension.Instance.GetUserInfo(username, shop) as App.Framework.Security.User;

			    if (user != null)
			        SecurityPortal.ValidateUser(username,user.UserDescription, password, shop, UserExtension.Instance);
                else
                    throw new SecurityExceptionToUser("用户不存在");
			    //BusinessPortal.Execute(new SaveAudit()
                //                           {
                //                               ACTION_TYPE = AcionType.LOGIN.ToString(),
                //                               SHOP_CODE = shop,
                //                               USER_ID = UserExtension.Instance.GetUserInfo(username, shop).UserIdentity.AsInt()

                //                           });
				GetUserByUserName u = new GetUserByUserName()
				{
					UserName = username
				};

				

				GetUserLoginIp loginIP = new GetUserLoginIp()
				{
					USER_ID = user.UserId
				};
				BusinessPortal.Execute(loginIP);
				var enableFlag = "false";//System.Configuration.ConfigurationManager.AppSettings["EnableUserLoginLock"];
				if (enableFlag.Equals("true", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(loginIP.LOGIN_IP) && loginIP.LOGIN_IP != Request.UserHostAddress)
				{
					FormsAuthentication.SignOut();
					throw new SecurityExceptionToUser(string.Format(CSC.Resources.Account.UserLoginedIPFormat, loginIP.LOGIN_IP));
				}

				

				UserIP userIp = new UserIP()
				{
					USER_ID = user.UserId,
					IP = Request.UserHostAddress
				};
				System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"(\d{1,3}.){3}\d{1,3}");
				if (reg.IsMatch(userIp.IP))
					BusinessPortal.Execute(userIp);

				//Initial logon password change
				if (SecurityPortal.InitialLogonChangePassword)
				{
					if (UserExtension.Instance.CheckInitialLogin(user.UserId))
					{
						var rolteValues = App.Framework.Web.Pager.Util.GetRouteValueDictionary(HttpContext, null);

						return RedirectToAction("InitialPwdChange", rolteValues);
					}
				}


				if (string.IsNullOrEmpty(shop))
				{
					var rolteValues = App.Framework.Web.Pager.Util.GetRouteValueDictionary(HttpContext, null);

					return RedirectToAction("LogInToShop", rolteValues);
				}


				if (string.IsNullOrEmpty(returnUrl))
					return Redirect("~/");
				return Redirect(returnUrl);
			}
			catch (SecurityExceptionToUser ex)
			{
				ModelState.AddModelError("err", ex.Message);
			}

			return Login();

		}

		public ActionResult LogInToShop()
		{
			if (!App.Framework.Security.User.IsAuthenticated)
				return RedirectToAction("Login");

			//Initial logon password change
			if (SecurityPortal.InitialLogonChangePassword)
			{
				if (UserExtension.Instance.CheckInitialLogin(App.Framework.Security.User.Current.UserId))
				{
					var rolteValues = App.Framework.Web.Pager.Util.GetRouteValueDictionary(HttpContext, null);

					return RedirectToAction("InitialPwdChange", rolteValues);
				}
			}
			

			var model = new LoginToShopModel();

			 model.ShopList = BusinessPortal.Search<Shop>(new SearchShopByUserID() { UserID = App.Framework.Security.User.Current.UserId });
			 var user = BusinessPortal.Load<Business.Entity.User.User>(new GetUser() { UserID = App.Framework.Security.User.Current.UserId });
			 DateTime? pwdExpiryDate = user.PwdExpiryDate;
			if (pwdExpiryDate.HasValue)
			{
				var str = ConfigurationManager.AppSettings["PwdWarningDays"];
				int warningDays = 0;
				if (!string.IsNullOrEmpty(str))
				{
					if (!int.TryParse(str, out warningDays))
					{
						warningDays = 0;
					}
				}
				if (pwdExpiryDate.Value <= DateTime.Today.AddDays(warningDays))
				{
					model.PwdWarningMesssage = string.Format(CSC.Resources.Account.PasswordWarning, pwdExpiryDate.Value.Format());
				}
			}

			//if exists a shop only, not redirect shop page.
			if (string.IsNullOrEmpty(model.PwdWarningMesssage) && model.ShopList.Count == 1)
			{
				string returnUrl = null; //不跳回原来的页面
				if (!App.Framework.Security.User.IsAuthenticated)
					return RedirectToAction("Login");
				App.Framework.Security.User CurUser = App.Framework.Security.User.Current;
				CurUser.ShopCode = model.ShopList[0].Code;
				PermissionsProviderFactory.ProvidePermissions.ClearUserIdentity(CurUser.UserIdentity);
				UserExtension.Instance.SaveUserState(CurUser);
				if (string.IsNullOrEmpty(returnUrl) == true)
					returnUrl = "~/Home/Index";
				return Redirect(returnUrl);
			}

			//.Value
			return View(model);
		}

		[HttpPost]
		public ActionResult LogInToShop(string shop, string returnUrl)
		{
			returnUrl = null; //不跳回原来的页面
			if (!App.Framework.Security.User.IsAuthenticated)
				return RedirectToAction("Login");
			App.Framework.Security.User user = App.Framework.Security.User.Current;
			if (string.IsNullOrEmpty(shop))
                return this.Message(Account.PleaseSelectShop);
			user.ShopCode = shop;
			PermissionsProviderFactory.ProvidePermissions.ClearUserIdentity(user.UserIdentity);
			UserExtension.Instance.SaveUserState(user);
			if (string.IsNullOrEmpty(returnUrl) == true)
				returnUrl = "~/Home/Index";
			return Redirect(returnUrl);
		}

		public ActionResult SignOut(string returnUrl)
		{
			if (string.IsNullOrEmpty(returnUrl) == true)
				returnUrl = "~/Account/Login";

			
			//取消身份验证
			FormsAuthentication.SignOut();

			UserIP uIp = new UserIP()
			{
				USER_ID = App.Framework.Security.User.Current.UserId,
				IP = null
			};


			BusinessPortal.Execute(uIp);

			Session.Abandon();
			Session.Clear();
			//清除用户权限缓存
			PermissionsProviderFactory.ProvidePermissions.ClearUserIdentity(HttpContext.User.Identity.Name);

			
			return Redirect(returnUrl);
		}
		
		#region ModifyUserLoginPwdAndAuthPwd

		public ActionResult PasswordChange(long? userId)
		{
			ModifyPasswordModel model = new ModifyPasswordModel();
			if (userId != null && userId !=0)
		    {
		        var user = BusinessPortal.Load<CSC.Business.Entity.User.User>(new GetUser {UserID = userId.Value});
		        model.UserName = user.UserName;
		        model.UserCode = user.UserCode;
		    }else
		    {
		        model.UserName = App.Framework.Security.User.Current.UserName;
                model.UserCode = App.Framework.Security.User.Current.UserCode;
		    }
		    return View(model);
		}

		#region 当前用户修改自己的密码

		[HttpPost]
		public ActionResult PasswordChange(string newLoginPwd, string newConfirmLoginPwd, string newAuthPwd, string newConfirmAuthPwd)
		{

            if (!ViewData.ModelState.IsValid)
            {
                return View("PasswordChange");
            }

			//密码框都没有填写--则代表不修改任何密码
			if (string.IsNullOrEmpty(newLoginPwd) && string.IsNullOrEmpty(newConfirmLoginPwd) && string.IsNullOrEmpty(newAuthPwd) && string.IsNullOrEmpty(newConfirmAuthPwd))
			{
				ViewData.ModelState.AddModelError("NewConfirmLoginPwd", "无密码修改！");
				ViewData.ModelState.AddModelError("NewConfirmAuthPwd", "无密码修改！");


				return View();
			}

			if (newLoginPwd != newConfirmLoginPwd)
			{
				ViewData.ModelState.AddModelError("NewConfirmLoginPwd", "新密码和确认新密码不一致！");

				return View();
			}

			if (newAuthPwd != newConfirmAuthPwd)
			{
				ViewData.ModelState.AddModelError("NewConfirmAuthPwd", "新密码和确认新密码不一致！");

				return View();
			}

			SecurityPortal.ModifyUserLoginPwdAndAuthPwd(App.Framework.Security.User.Current.UserId, newLoginPwd, newAuthPwd, UserExtension.Instance);

			return View();
		}

		[HttpPost]
		[AuthorizationFilter((int)EnumPermission.Own_ChangePassword)]
		public ActionResult ModifyLoginPwd(CSC.Business.Entity.User.ModifyPasswordModel model)
		{
            if (!ViewData.ModelState.IsValid)
            {
                return View("PasswordChange", model);
            }

			try
			{
				SecurityPortal.ModifyUserLoginPwd(App.Framework.Security.User.Current.UserId, model.NewLoginPwd, UserExtension.Instance);

				this.ShowMessage(CSC.Resources.Account.ModifyLoginPwdSuccess, isSucessed: true);


				model.IsCurrentUser = true;
			}
			catch(Exception ex)
			{
				this.ShowMessage(ex.Message, isSucessed: false);
			}

			return View("PasswordChange", model);
		}

		[HttpPost]
		[AuthorizationFilter((int)EnumPermission.Own_ChangePassword)]
		public ActionResult ModifyAuthPwd(CSC.Business.Entity.User.ModifyPasswordModel model)
		{
			SecurityPortal.ModifyUserAuthPwd(App.Framework.Security.User.Current.UserId, model.NewAuthPwd, UserExtension.Instance);

			this.ShowMessage(CSC.Resources.Account.ModifyAuthPwdSuccess, isSucessed: true);

			model = new ModifyPasswordModel();
			model.IsCurrentUser = true;

			return View("PasswordChange", model);
		}

		#endregion

		#region 具有权限的用户修改他人密码

		[HttpPost]
		[AuthorizationFilter((int)EnumPermission.User_ChangePassword)]
		public ActionResult ModifyUserLoginPwd(CSC.Business.Entity.User.ModifyPasswordModel model)
		{
            

            if (!ViewData.ModelState.IsValid)
            {
                return View("PasswordChange", model);
            }

			try 
			{
				int isModify = model.IsModifyUserPwd(Convert.ToInt32(model.UserID));

				switch (isModify)
				{
					case 0:
						SecurityPortal.ModifyUserLoginPwd(Convert.ToInt64(model.UserID), model.NewLoginPwd, UserExtension.Instance);
						this.ShowMessage(CSC.Resources.Account.ModifyLoginPwdSuccess, isSucessed: true);
						break;
					case -1:
						this.ShowMessage(CSC.Resources.Account.CancelModifyLogPwd, isSucessed: true);
						break;
					case -2:
						this.ShowMessage(CSC.Resources.Account.UserNoExists_Log, isSucessed: true);
						break;
				}


				model.IsCurrentUser = false;
			}
			catch (Exception ex)
			{
				this.ShowMessage(ex.Message, isSucessed: false);
			}
			

			return View("PasswordChange", model);
		}

		[HttpPost]
		[AuthorizationFilter((int)EnumPermission.User_ChangePassword)]
		public ActionResult ModifyUserAuthPwd(CSC.Business.Entity.User.ModifyPasswordModel model)
		{
			int isModify = model.IsModifyUserPwd(Convert.ToInt32(model.UserID));

			switch (isModify)
			{
				case 0:
					SecurityPortal.ModifyUserAuthPwd(Convert.ToInt64(model.UserID), model.NewAuthPwd, UserExtension.Instance);
					this.ShowMessage(CSC.Resources.Account.ModifyAuthPwdSuccess, isSucessed: true);
					break;
				case -1:
					this.ShowMessage(CSC.Resources.Account.CancelModifyAuthPwd, isSucessed: true);
					break;
				case -2:
					this.ShowMessage(CSC.Resources.Account.UserNoExists_Auth, isSucessed: true);
					break;
			}

			model = new ModifyPasswordModel();
			model.IsCurrentUser = false;

			return View("PasswordChange", model);
		}

		#endregion

		#endregion

		#region 修改初始密码

		public ActionResult InitialPwdChange()
		{
			ModifyPasswordModel model = new ModifyPasswordModel();
			model.UserName = App.Framework.Security.User.Current.UserName;
			model.UserCode = App.Framework.Security.User.Current.UserCode;
			return View(model);
		}

		[HttpPost]
		public ActionResult InitialPwdChange(CSC.Business.Entity.User.ModifyPasswordModel model)
		{
			if (!ViewData.ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				SecurityPortal.ModifyUserLoginPwd(App.Framework.Security.User.Current.UserId, model.NewLoginPwd, UserExtension.Instance);

				this.ShowMessage(CSC.Resources.Account.ModifyLoginPwdSuccess, isSucessed: true);
			}
			catch (Exception ex)
			{
				this.ShowMessage(ex.Message, isSucessed: false);
			}

			return View(model);
		}

		#endregion
	}

	  
}
