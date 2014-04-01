using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework.Web.User;
using System.Web.Script.Serialization;

namespace App.Framework.Security
{

	public class User : App.Framework.Security.UserBase
	{

		private static JavaScriptSerializer _JavaScriptSerializer = new JavaScriptSerializer();


		public static bool IsAuthenticated
		{
			get
			{
				return UserIdentityFactory.Instance.IsAuthenticated;
			}
		}

		public static User Current
		{
			get
			{
				if (!UserIdentityFactory.Instance.IsAuthenticated)
				{
					throw new App.Framework.Web.Permissions.AuthorizationException("user not logged in", false);

					//return new User();
					//UserIdentityFactory.Instance.RedirectToLoginPage();
					//return null;
				}
				string info = UserIdentityFactory.Instance.OtherInfo;
				var obj = _JavaScriptSerializer.Deserialize<User>(info);

				User ue = new User()
				{
					UserId = Convert.ToInt64(UserIdentityFactory.Instance.UserIdentity),
					UserCode = obj.UserCode,
					UserName = UserIdentityFactory.Instance.UserName,
					UserDescription = obj.UserDescription,
					ShopCode = obj.ShopCode,
					BUCode=obj.BUCode,
					ShopType = obj.ShopType,
					SysCode = obj.SysCode,
					ShopName = obj.ShopName,
					IsSysAdmin = obj.IsSysAdmin,
					IsShopAdmin = obj.IsShopAdmin,
					UserIdentity = UserIdentityFactory.Instance.UserIdentity,
					PasswordExpiryDate = obj.PasswordExpiryDate,
					UserType = obj.UserType
				};
				if (string.IsNullOrEmpty(ue.ShopCode) && System.Web.HttpContext.Current.Request.Url.ToString().IndexOf("LogInToShop") < 0)
				{
					//System.Web.HttpContext.Current.Response.Redirect("~/Account/LogInToShop");
				}
				return ue;
			}
		}

		public long UserId
		{
			get;
			set;
		}

		public string UserCode
		{
			get;
			set;
		}

		public string ShopName
		{
			get;
			set;
		}

		public string BUCode
		{
			get;
			set;
		}

		public string ShopType
		{
			get;
			set;
		}

		public string SysCode
		{
			get;
			set;
		}

		public DateTime? PasswordExpiryDate
		{
			get;
			set;
		}

		public bool IsShopAdmin
		{
			get;
			set;
		}

		public bool IsSysAdmin
		{
			get;
			set;
		}

		public string UserType
		{
			get;
			set;
		}

		public bool FrozenFlag
		{
			get;
			set;
		}
	}
}
