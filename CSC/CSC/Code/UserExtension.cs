using System;
using System.Collections.Generic;
using App.Framework.Web.User;
using CRM.Business;
using CSC.Business.Entity.User;
using App.Framework;
using App.Framework.Security;
using System.Web.Script.Serialization;
using App.Framework.Web.Permissions;
using CSC.Business;

namespace CSC
{
	public class UserExtension : IUserProfile
	{

		private static readonly UserExtension _instance = new UserExtension();
		static readonly JavaScriptSerializer JavaScriptSerializer = new JavaScriptSerializer();
		private UserExtension() { }
		public static UserExtension Instance
		{
			get
			{
				return _instance;
			}
		}

		private readonly Dictionary<long, int> _userPasswordErrorTimes = new Dictionary<long, int>();

		public static IList<PermissionsPoint> GetCurrentUserHasPermissionsPoints()
		{
			return UserIdentityCollection.Instance.GetUserHasPermissionsPoints(App.Framework.Security.User.Current.UserIdentity, null);
		}

		public static void SetAuth(string userIndentityKey, string userdescription, string userName, string shopCode)
		{
			var gubun = new GetUserByUserName
			{
				UserName = userName,
				ShopCode = shopCode
			};
			var user = BusinessPortal.Load<Business.Entity.User.User>(gubun);
			var shop = BusinessPortal.Load<Shop>(new LoadShopCriteria { SysCode = SecurityPortal.ApplicationName, Code = shopCode });
			var str = JavaScriptSerializer.Serialize(new App.Framework.Security.User
			{
				UserId = Convert.ToInt64(userIndentityKey),
				UserName = userName,
				UserType = user.UserType,
				ShopName = shop == null ? string.Empty : shop.Name,
				BUCode = shop == null ? string.Empty : shop.BU_CODE,
				ShopType = shop == null ? string.Empty : shop.SHOP_TYPE,
				ShopCode = shopCode,
				UserCode = userName,
				UserDescription = userdescription,
				IsSysAdmin = true,
				IsShopAdmin = true
			});
			UserIdentityFactory.Instance.SetAuth(userIndentityKey, userName, null, null, null, str);
		}

		public bool UserIsExists(string userName, string shopCode)
		{
			var gubun = new GetUserByUserName()
			{
				UserName = userName,
				ShopCode = shopCode

			};
			var user = BusinessPortal.Load<Business.Entity.User.User>(gubun);
			return user != null;
		}

		public string GetUserPassword(string userIndentify, string shopCode)
		{
			var gup = new GetUserPassword()
			{
				UserName = userIndentify,
				ShopCode = shopCode
			};
			var user = BusinessPortal.Load<Business.Entity.User.User>(gup);
			return user.LoginPwd;
		}

		public void ModifyUserPassword(string userIndentify, string password, uint pwdExpiryDate)
		{
			var mup = new ModifyUserPassword()
			{
				UserId = Convert.ToInt32(userIndentify),
				NewPassword = password,
				PwdExpiryDate = pwdExpiryDate
			};
			BusinessPortal.Load<Business.Entity.User.User>(mup);
		}

		public void ModifyUserLoginPwdAndAuthPwd(long userID, string newLoginPwd, string newAuthPwd)
		{
			var model = new ModifyPasswordModel()
			{
				UserID = userID,
				LastUpdatedBy = userID
			};

			if (string.IsNullOrEmpty(newLoginPwd) && string.IsNullOrEmpty(newAuthPwd))
			{
				model = new ModifyPasswordModel()
				{
					UserID = userID
				};
			}
			else if (string.IsNullOrEmpty(newLoginPwd))
			{
				model = new ModifyPasswordModel()
				{
					UserID = userID,
					NewAuthPwd = newAuthPwd
				};
			}
			else if (string.IsNullOrEmpty(newAuthPwd))
			{
				model = new ModifyPasswordModel()
				{
					UserID = userID,
					NewLoginPwd = newAuthPwd,
				};
			}
			else
			{
				model = new ModifyPasswordModel()
				{
					UserID = userID,
					NewLoginPwd = newLoginPwd,
					NewAuthPwd = newAuthPwd
				};
			}

			BusinessPortal.Save(model);
		}

		public void ModifyUserLoginPwd(long userID, string newLoginPwd)
		{
			ModifyPasswordModel model = new ModifyPasswordModel()
			{
				UserID = userID,
				LastUpdatedBy = App.Framework.Security.User.Current.UserId
			};

			if (!string.IsNullOrEmpty(newLoginPwd))
			{
				model = new ModifyPasswordModel()
				{
					UserID = userID,
					LastUpdatedBy = App.Framework.Security.User.Current.UserId,
					NewLoginPwd = newLoginPwd,
					PwdExpiryDate = SecurityPortal.PasswordEffectiveDays
				};
			}

			BusinessPortal.Save(model);
		}

		public void ModifyUserAuthPwd(long userID, string newAuthPwd)
		{
			ModifyPasswordModel model = new ModifyPasswordModel()
			{
				UserID = userID,
				LastUpdatedBy = App.Framework.Security.User.Current.UserId
			};

			if (!string.IsNullOrEmpty(newAuthPwd))
			{
				model = new ModifyPasswordModel()
				{
					UserID = userID,
					LastUpdatedBy = App.Framework.Security.User.Current.UserId,
					NewAuthPwd = newAuthPwd
				};
			}

			//BusinessPortal.Save(model);
			BusinessPortal.Delete(model);
		}

		public void SaveUserState(UserBase user)
		{
			App.Framework.Security.User ue = user as App.Framework.Security.User;
			SetAuth(ue.UserId.ToString(), ue.UserDescription, user.UserName, user.ShopCode);
		}

		public int GetErrorPasswordTimes(long userId)
		{
			if (_userPasswordErrorTimes.ContainsKey(userId))
				return _userPasswordErrorTimes[userId];
			return 0;
		}


		public void SaveErrorPasswordTimes(long userId)
		{
			if (_userPasswordErrorTimes.ContainsKey(userId))
				_userPasswordErrorTimes[userId]++;
			else
				_userPasswordErrorTimes[userId] = 1;

		}



		public UserBase GetUserInfo(string userName, string shopCode)
		{
			GetUserByUserName gubun = new GetUserByUserName()
			{
				UserName = userName,
				ShopCode = shopCode
			};
			CSC.Business.Entity.User.User user = BusinessPortal.Load<CSC.Business.Entity.User.User>(gubun);

			if (user == null)
				//throw new SecurityExceptionToUser("用户不存在");
				throw new SecurityExceptionToUser("Username is invalid");

			if (user.FrozenFlag || user.SuspendFlag)
			{
				throw new SecurityExceptionToUser(CSC.Resources.Account.UserIsLocked);
			}

			return new App.Framework.Security.User()
			{
				UserName = user.UserCode,
				UserCode = user.UserCode,
				UserId = user.UserId,
				UserIdentity = user.UserId.ToString(),
				UserDescription = user.UserName,
				PasswordExpiryDate = user.PwdExpiryDate
			};
		}

		public void Freeze(long userId)
		{
			ForzenUser fUser = new ForzenUser() { USER_ID = userId };
			BusinessPortal.Execute(fUser);
		}

		public void ClearUserErrorPasswordTimes(long userId)
		{
			_userPasswordErrorTimes.Remove(userId);
		}

		public DateTime? GetPasswordEffectiveDays(string userName, string shopCode)
		{
			var user = GetUserInfo(userName, shopCode);
			return ((App.Framework.Security.User)user).PasswordExpiryDate;
		}

		public void ResetPasswordEffectiveDays(string userName)
		{
			throw new NotImplementedException();
		}

		public bool CheckLastPwd(long userId, string newPwd, uint lastUseCount) 
		{
			if (lastUseCount == 0) 
			{
				return true;
			}

			
			CheckLastPwdCriteria model = new CheckLastPwdCriteria()
			{
				UserId = userId,
				NewPassword = newPwd,
				ChangePwdCnt = lastUseCount
			};

			BusinessPortal.Execute(model);

			if (model.ResultType == 1)
			{
				return true;
			}
			else 
			{
				return false;
			}
		}

		public bool CheckInitialLogin(long userId)
		{
			CheckInitialLoginCriteria model = new CheckInitialLoginCriteria()
			{
				UserId = userId
			};

			BusinessPortal.Execute(model);

			if (model.ResultType == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
