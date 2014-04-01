using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework.Data;
using App.Framework;
using System.ComponentModel.DataAnnotations;
using App.Framework.Security;
using System.Data;

namespace CSC.Business
{
	public class UserModel : DataEntity
	{
		[DbField("USER_ID")]
		public long UserId { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("USER_CODE")]
		public string UserCode { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("USER_NAME")]
		public string UserName { get; set; }
		
		[DbField("USER_TYPE")]
		public string UserType { get; set; }
		
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
		public DateTime PwdExpiryDate { get; set; }

		[DbField("LAST_UPDATE_DATE")]
		public DateTime LastUpdateDate { get; set; }

		[DbField("LAST_UPDATED_BY")]
		public long LastUpdatedBy { get; set; }

		[DbField("CREATION_DATE")]
		public DateTime CreationDate { get; set; }

		[DbField("CREATED_BY")]
		public long CreatedBy { get; set; }

		[DbField("LAST_UPDATE_LOGIN")]
		public long LastUpdateLogin { get; set; }
		
		[DbField("REPORT_SERVER_CODE")]
		public string ReportServerCode { get; set; }

		[DbField("CHAR_REPORT_SERVER_CODE")]
		public string CharReportServerCode { get; set; }

		[DbField("SHOP_COUNT")]
		public int ShopCount { get; set; }

        [DbField("ASSIGNMENT_COUNT")]
        public int ASSIGNMENT_COUNT { get; set; }
		public override DataCriteria CreateDeleteCriteria()
		{
			return new SuspendUserCriteria(this);
		}

		public override DataCriteria CreateSaveCriteria()
		{
			return new SaveUserCriteria(this);
		}
	}

	[DbCommand("spDeleteUser")]
	public class DeleteUserCriteria : DataCriteria
	{
		[DbParameter("USER_ID")]
		public long UserId { get; set; }
	}

	[DbCommand("spSearchUser")]
	public class SearchUserCriteria : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("USER_ID")]
		public long UserId { get; set; }

        [DbParameter("USER_CODE")]
        public string USER_CODE { get; set; }

		[DbParameter("ROLE_ID")]
		public long RoleID { get; set; }

		[DbParameter("USER_NAME")]
		public string UserName { get; set; }

		//added by jason in 20121219 for TSYF02010#06.doc
		//public bool Suspend { get; set; }

		[DbParameter("SUSPEND_FLAG")]
		public string SuspendFlag { get; set; }
		//added by jason in 20121219 for TSYF02010#06.doc

        [DbParameter("SUSPEND")]
        public string Suspend { get; set; }

		[DbParameter("USER_TYPE")]
		public string UserType { get { return App.Framework.Security.User.Current.UserType; } }

		[DbParameter("SHOP_CODE")]
		public string ShopCode { get { return App.Framework.Security.User.Current.ShopCode; } }
	}

	[DbCommand("spSearchUserForDDL")]
	public class SearchUserForDDLCriteria : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("USER_TYPE")]
		public string UserType { get { return App.Framework.Security.User.Current.UserType; } }

		[DbParameter("SHOP_CODE")]
		public string ShopCode { get { return App.Framework.Security.User.Current.ShopCode; } }
	}

	[DbCommand("spUpdateUserForzen")]
	public class CancelFrozenUserCriteria : DataCriteria
	{
		[DbParameter("USER_ID")]
		public long UserId { get; set; }

		[DbParameter("LAST_UPDATED_BY")]
		public long LastUpdatedBy
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
	}

	[DbCommand("spUpdateUserSuspend")]
	public class SuspendUserCriteria : DataCriteria
	{
		public UserModel parent;

		public SuspendUserCriteria(UserModel parent)
		{
			this.parent = parent;
		}

		[DbParameter("USER_ID")]
		public long UserID
		{
			get { return parent.UserId; }
			set { parent.UserId = value; }
		}

		[DbParameter("SUSPEND_FLAG")]
		public bool SuspendFlag
		{
			get { return parent.SuspendFlag; }
			set { parent.SuspendFlag = value; }
		}

		[DbParameter("LAST_UPDATED_BY")]
		public long LastUpdatedBy
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
	}

	[DbCommand("spSaveUser")]
	public class SaveUserCriteria : DataCriteria
	{
		private UserModel parent;

		public SaveUserCriteria(UserModel parent)
		{
			this.parent = parent;
		}

		[DbParameter("USER_ID",ParameterDirection.InputOutput)]
		public long UserID
		{
			get { return parent.UserId; }
			set { parent.UserId = value; }
		}

		[DbParameter("USER_CODE")]
		public string UserCode
		{
			get { return parent.UserCode; }
			set { parent.UserCode = value; }
		}

		[DbParameter("USER_NAME")]
		public string UserName
		{
			get { return parent.UserName; }
			set { parent.UserName = value; }
		}

		[DbParameter("USER_TYPE")]
		public string UserType
		{
			get { return parent.UserType; }
			set { parent.UserType = value; }
		}

		[DbParameter("SYSTEM_SCOPE")]
		public string SystemScope
		{
			get { return SecurityPortal.ApplicationName; }
		}

		[DbParameter("LOGIN_PWD")]
		public string LoginPwd
		{
			get {
				return  DefaultEncrypt.Instance.Encrypt(parent.UserCode);
				//return CipherHelper.Hash(parent.UserCode, CipherHelper.HashFormat.SHA1); 
			}
			set { parent.LoginPwd = value; }
		}

		[DbParameter("PWD_EXPIRY_DAYS")]
		public long PwdExriryDays
		{
			get { return SecurityPortal.PasswordEffectiveDays; }
		}

		[DbParameter("LAST_UPDATE_DATE")]
		public DateTime LastUpdateDate
		{
			get { return parent.LastUpdateDate; }
			set { parent.LastUpdateDate = value; }
		}

		[DbParameter("LAST_UPDATED_BY")]
		public long LastUpdatedBy
		{
			get { return parent.LastUpdatedBy; }
			set { parent.LastUpdatedBy = value; }
		}

		[DbParameter("CURRENT_USER_ID")]
		public long CurrentUserID
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}

		[DbParameter("CURRENT_USER_TYPE")]
		public string CurrentUserType
		{
			get { return App.Framework.Security.User.Current.UserType; }
		}
	}

	public class UserViewModel
	{
		public UserModel UserModel { get; set; }

		public SearchUserCriteria UserSearch { get; set; }

		//根据SHOPCODE筛选对应的ROLE
		public UserRoleShopInUserCriteria ShopRoleSearch { get; set; }

		public BusinessList<UserModel> UserList { get; set; }

		public BusinessList<UserRole> UserRoleList { get; set; }

		public BusinessList<UserShop> UserShopList { get; set; }

		public BusinessList<UserRole> UserRoleShopList { get; set; }

		public BusinessList<UserModel> UserForDropDownList { get; set; }

		public BusinessList<Role> RoleForDropDownList { get; set; }

		public BusinessList<Shop> ShopForDropDownList { get; set; }
	}
}
