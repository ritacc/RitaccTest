using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework.Data;
using System.ComponentModel.DataAnnotations;
using App.Framework;
using App.Framework.Security;
using System.Data;

namespace CSC.Business.Entity.User
{
	[DbCommand("spForzenUser")]
	public class ForzenUser : DataCriteria
	{
		[DbParameter("USER_ID")]
		public long? USER_ID { get; set; }
	}

	public class LoginModel
	{
		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		public string UserName
		{
			get;
			set;
		}

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		public string Password
		{
			get;
			set;
		}

		public string Shop
		{
			get;
			set;
		}

		public BusinessList<Shop> ShopList { get; set; }
	}

	public class ModifyPasswordModel : DataEntity
	{
		[DbField("USER_ID")]
		public long UserID { get; set; }

		[DbField("LOGIN_PWD")]
		//[StringLength(12, ErrorMessage = "密码位数为3到12位", MinimumLength = 3)]
		public string NewLoginPwd { get; set; }

		[DbField("LOGIN_PWD")]
		//[StringLength(12, ErrorMessage = "密码位数为3到12位", MinimumLength = 3)]
		public string NewConfirmLoginPwd { get; set; }

		[DbField("AUTH_PWD")]
		public string NewAuthPwd { get; set; }

		[DbField("AUTH_PWD")]
		public string NewConfirmAuthPwd { get; set; }

		[DbField("PWD_EXPIRY_DATE")]
		public long PwdExpiryDate { get; set; }

		[DbField("LAST_UPDATED_BY")]
		public long LastUpdatedBy { get; set; }


		public string UserName { get; set; }
		public string UserCode { get; set; }

		public bool IsCurrentUser { get; set; }

		public override DataCriteria CreateDeleteCriteria()
		{
			return new ModifyUserAuthPwd(this);
		}

		public override DataCriteria CreateSaveCriteria()
		{
			return new ModifyUserLoginPwd(this);

			//return new ModifyUserLoginPwdAndAuthPwd(this);
		}

		/// <summary>
		/// 判断当前登录的用户能否修改参数userId的密码
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public int IsModifyUserPwd(int userId)
		{
			if (App.Framework.Security.User.Current.UserType == CSC.Business.UserTypes.NS.ToString())
			{
				User user = BusinessPortal.Load<User>(new GetUser() { UserID = userId });
				//公司用户不能修改系统管理员的密码
				if (user.UserType == CSC.Business.UserTypes.SA.ToString())
				{
					return -1;
				}

				BusinessList<Shop> shopList = BusinessPortal.Search<Shop>(new SearchShopByUserID() { UserID = userId });
				bool existFlag = false;
				foreach (Shop sh in shopList)
				{
					if (sh.Code == App.Framework.Security.User.Current.ShopCode)
					{
						existFlag = true;
					}
				}
				//如果userId不在当前店里面，不能修改用户密码
				if (!existFlag)
				{
					return -2;
				}
			}
			return 0;
		}
	}

	public class User : DataEntity
	{
		[DbField("USER_ID")]
		public long UserId { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("USER_CODE")]
		public string UserCode { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("USER_NAME")]
		public string UserName { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("USER_TYPE")]
		public string UserType { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("SYSTEM_SCOPE")]
		public string SystemScope { get; set; }

		[DbField("FROZEN_FLAG")]
		public bool FrozenFlag { get; set; }

		[DbField("FROZEN_DATE")]
		public DateTime? FrozenDate { get; set; }

		[DbField("SUSPEND_FLAG")]
		public bool SuspendFlag { get; set; }

		[DbField("SUSPEND_DATE")]
		public DateTime? SuspendDate { get; set; }

		[DbField("LOGIN_PWD")]
		public string LoginPwd { get; set; }

		[DbField("AUTH_PWD")]
		public string AuthPwd { get; set; }

		[DbField("PWD_EXPIRY_DATE")]
		public DateTime? PwdExpiryDate { get; set; }

		[DbField("LAST_UPDATE_DATE")]
		public DateTime LastUpdateDate { get; set; }

		[DbField("LAST_UPDATED_BY")]
		public long LastUpdatedBy { get; set; }

		[DbField("CREATION_DATE")]
		public DateTime CreationDate { get; set; }

		[DbField("CREATED_BY")]
		public long CreatedBy { get; set; }

		[DbField("LAST_UPDATE_LOGIN")]
		public long? LastUpdateLogin { get; set; }

		[DbField("REPORT_SERVER_CODE")]
		public string ReportServerCode { get; set; }

		[DbField("CHAR_REPORT_SERVER_CODE")]
		public string CharREportServerCode { get; set; }

		[DbField("ROWID")]
		public Guid RowId { get; set; }

		[DbField("SHOP_COUNT")]
		public int ShopCount { get; set; }

		[DbField("ModifyFSType")]
		public string ModifyFSType { get; set; }

		public override DataCriteria CreateDeleteCriteria()
		{
			throw new NotImplementedException();
		}

		public override DataCriteria CreateSaveCriteria()
		{
			throw new NotImplementedException();
		}
	}

	[DbCommand("spAuthUser")]
	public class AuthUser : DataCriteria
	{
		[DbParameter("UserName")]
		public string UserName { get; set; }

		[DbParameter("LoginPwd")]
		public string LoginPwd { get; set; }

		[DbParameter("SystemScope")]
		public string SystemScope { get; set; }
	}

	[DbCommand("spGetUserPassword")]
	public class GetUserPassword : DataCriteria
	{
		[DbParameter("UserName")]
		public string UserName { get; set; }
		[DbParameter("ShopCode")]
		public string ShopCode { get; set; }
	}

	[DbCommand("spGetUserByUserName")]
	public class GetUserByUserName : DataCriteria
	{
		[DbParameter("UserName")]
		public string UserName { get; set; }
		[DbParameter("SYSTEM_SCOPE")]
		public string SystemScope { get { return SecurityPortal.ApplicationName; } }
		[DbParameter("ShopCode")]
		public string ShopCode { get; set; }
	}

	[DbCommand("spGetUserByUserID")]
	public class GetUser : DataCriteria
	{
		[DbParameter("UserID")]
		public long UserID { get; set; }

		public User Load()
		{
			return BusinessPortal.Load<User>(this);
		}
	}

	[DbCommand("spModifyUserPassword")]
	public class ModifyUserPassword : DataCriteria
	{
		[DbParameter("UserId")]
		public long UserId { get; set; }

		[DbParameter("NewPassword")]
		public string NewPassword { get; set; }

		[DbParameter("PwdExpiryDate")]
		public uint PwdExpiryDate { get; set; }

	}

	[DbCommand("spModifyUserLoginPwdAndAuthPwd")]
	public class ModifyUserLoginPwdAndAuthPwd : DataCriteria
	{
		private ModifyPasswordModel user;

		public ModifyUserLoginPwdAndAuthPwd(ModifyPasswordModel user)
		{
			this.user = user;
		}

		[DbParameter("UserID")]
		public long UserID
		{
			get { return user.UserID; }
			set { user.UserID = value; }
		}

		[DbParameter("NewLoginPwd")]
		public string NewLoginPwd
		{
			get { return user.NewLoginPwd; }
			set { user.NewLoginPwd = value; }
		}

		[DbParameter("NewAuthPwd")]
		public string NewAuthPwd
		{
			get { return user.NewAuthPwd; }
			set { user.NewAuthPwd = value; }
		}

		[DbParameter("LastUpdatedBy")]
		public long LastUpdatedBy
		{
			get { return user.LastUpdatedBy; }
			set { user.LastUpdatedBy = value; }
		}
	}

	[DbCommand("spModifyUserLoginPwd")]
	public class ModifyUserLoginPwd : DataCriteria
	{
		private ModifyPasswordModel user;

		public ModifyUserLoginPwd(ModifyPasswordModel user)
		{
			this.user = user;
		}

		[DbParameter("UserID")]
		public long UserID
		{
			get { return user.UserID; }
			set { user.UserID = value; }
		}

		[DbParameter("NewLoginPwd")]
		public string NewLoginPwd
		{
			get { return user.NewLoginPwd; }
			set { user.NewLoginPwd = value; }
		}

		[DbParameter("PwdExpiryDate")]
		public long PwdExpiryDate
		{
			get { return user.PwdExpiryDate; }
			set { user.PwdExpiryDate = value; }
		}

		[DbParameter("LastUpdatedBy")]
		public long LastUpdatedBy
		{
			get { return user.LastUpdatedBy; }
			set { user.LastUpdatedBy = value; }
		}
	}

	[DbCommand("spModifyUserAuthPwd")]
	public class ModifyUserAuthPwd : DataCriteria
	{
		private ModifyPasswordModel user;

		public ModifyUserAuthPwd(ModifyPasswordModel user)
		{
			this.user = user;
		}

		[DbParameter("UserID")]
		public long UserID
		{
			get { return user.UserID; }
			set { user.UserID = value; }
		}

		[DbParameter("NewAuthPwd")]
		public string NewAuthPwd
		{
			get { return user.NewAuthPwd; }
			set { user.NewAuthPwd = value; }
		}

		[DbParameter("LastUpdatedBy")]
		public long LastUpdatedBy
		{
			get { return user.LastUpdatedBy; }
			set { user.LastUpdatedBy = value; }
		}
	}

	[DbCommand("spCheckLastPwd")]
	public class CheckLastPwdCriteria : DataCriteria
	{
		[DbParameter("UserId")]
		public long UserId { get; set; }

		[DbParameter("NewPassword")]
		public string NewPassword { get; set; }

		[DbParameter("ChangePwdCnt ")]
		public long ChangePwdCnt { get; set; }
		
	}

	[DbCommand("spCheckInitialLogin")]
	public class CheckInitialLoginCriteria : DataCriteria
	{
		[DbParameter("UserId")]
		public long UserId { get; set; }
	}


	public class DeleteCriteria : DataCriteria
	{

	}

	#region ListModel

	public class ListModel
	{
		public BusinessList<User> List { get; set; }
	}

	#endregion
}
