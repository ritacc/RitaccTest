using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework.Data;
using App.Framework;
using System.ComponentModel.DataAnnotations;
using App.Framework.Security;

namespace CSC.Business
{
	#region UserRole

	public class UserRole:DataEntity
	{
		[DbField("ROLE_ID")]
		public long RoleID { get; set; }

		[DbField("USER_ID")]
		public long UserID { get; set; }

		[DbField("ACTIVE_FLAG")]
		public bool ActiveFlag { get; set; }

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

		[DbField("USER_TYPE")]
		public string UserType { get; set; }

		[DbField("USER_CODE")]
		public string UserCode { get; set; }

		[DbField("USER_NAME")]
		public string UserName { get; set; }

		[DbField("ROLE_CODE")]
		public string RoleCode { get; set; }

		[DbField("ROLE_DSC")]
		public string RoleDsc { get; set; }

		[DbField("ROLE_SDSC")]
		public string RoleSDsc { get; set; }

		[DbField("ADMIN_FLAG")]
		public bool AdminFlag { get; set; }

		[DbField("ROLE_TYPE")]
		public string RoleType { get; set; }

		[DbField("SHOP_CODE")]
		public string ShopCode { get; set; }

		[DbField("SHOP_NAME")]
		public string ShopName { get; set; }

		[DbField("SYSTEM_SCOPE")]
		public string SystemScope { get; set; }

		[DbField("BU_CODE")]
		public string BU_CODE { get; set; }

		public override DataCriteria CreateDeleteCriteria()
		{
			throw new NotImplementedException();
		}

		public override DataCriteria CreateSaveCriteria()
		{
			throw new NotImplementedException();
		} 
	}

	#endregion
	
	[DbCommand("spSearchUserRole")]
	public class SearchUserRoleCriteria : DataCriteria
	{
		/// <summary>
		/// 系统编号
		/// </summary>
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("ROLE_ID")]
		public long RoleID { get; set; }

	}

	[DbCommand("spSearchRoleInUser")]
	public class UserRoleInUserCriteria : DataCriteria
	{
		/// <summary>
		/// 系统编号
		/// </summary>
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("USER_ID")]
		public long UserId { get; set; }

		[DbParameter("USER_TYPE")]
		public string UserType { get { return App.Framework.Security.User.Current.UserType; } }

		[DbParameter("SHOP_CODE")]
		public string ShopCode { get { return App.Framework.Security.User.Current.ShopCode; } }

		#region added by jason in 20120305 for bug SYSTEM05#09.doc(第二点)
		[DbParameter("CURRENT_USER_ID")]
		public long CurrentUserID
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
		#endregion end added by jason in 20120305 for bug SYSTEM05#09.doc(第二点)
	}

	//根据选择的店去筛选role
	[DbCommand("spSearchUserRoleShopByShop")]
	public class UserRoleShopInUserCriteria : DataCriteria
	{
		/// <summary>
		/// 系统编号
		/// </summary>
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("USER_ID")]
		public long UserId { get; set; }

		[DbParameter("USER_TYPE")]
		public string UserType { get { return App.Framework.Security.User.Current.UserType; } }

		[DbParameter("SHOP_CODE")]
		public string ShopCode { get; set; }

		[DbParameter("CURRENT_SHOP_CODE")]
		public string CurrentShopCode { get { return App.Framework.Security.User.Current.ShopCode; } }

		#region added by jason in 20120305 for bug SYSTEM05#09.doc(第二点)
		[DbParameter("CURRENT_USER_ID")]
		public long CurrentUserID
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
		#endregion end added by jason in 20120305  for bug SYSTEM05#09.doc(第二点)
	}

	//查找USER对应的ROLE
	[DbCommand("spSearchUserRoleByUser")]
	public class SearchUserRoleByUserCriteria : DataCriteria
	{
		[DbParameter("USER_ID")]
		public long UserID
		{
			get;
			set;
		}

		[DbParameter("ROLE_TYPE")]
		public string RoleType
		{
			get;
			set;
		}
	}

	//写入单条SY_USER_ROLE
	[DbCommand("spAddUserRole")]
	public class AddUserRoleCriteria : DataCriteria
	{
		[DbParameter("ROLE_ID")]
		public long RoleID { get; set; }

		[DbParameter("USER_ID")]
		public long UserID { get; set; }

		[DbParameter("CURRENT_USER_ID")]
		public long CurrentUserID
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
	}

	//写入单条SY_USER_ROLE_shop
	[DbCommand("spAddUserRoleShop")]
	public class AddUserRoleShopCriteria : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("SHOP_CODE")]
		public string ShopCode { get; set; }

		[DbParameter("ROLE_ID")]
		public long RoleID { get; set; }

		[DbParameter("USER_ID")]
		public long UserID { get; set; }

		[DbParameter("CURRENT_USER_ID")]
		public long CurrentUserID
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
	}

	//将User下面对应的SY_USER_ROLE删除
	[DbCommand("spDeleteUserRole")]
	public class DeleteUserRoleCriteria : DataCriteria
	{
		[DbParameter("USER_ID")]
		public long UserID { get; set; }
	}

	//将User下面对应的SY_USER_ROLE_shop删除
	[DbCommand("spDeleteUserRoleShop")]
	public class DeleteUserRoleShopCriteria : DataCriteria
	{
		[DbParameter("USER_ID")]
		public long UserID { get; set; }

		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("CURRENT_USER_ID")]
		public long CurrentUserID
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
	}

	//将User下面shop对应的SY_USER_ROLE_shop删除
	[DbCommand("spDeleteUserRoleShopByShop")]
	public class DeleteUserRoleShopByShopCriteria : DataCriteria
	{
		[DbParameter("USER_ID")]
		public long UserID { get; set; }

		[DbParameter("SHOP_CODE")]
		public string ShopCode { get; set; }

		[DbParameter("ROLE_TYPE")]
		public string RoleType
		{
			get;
			set;
		}
	}

	//删除不在USER下面的ROLE
	[DbCommand("spDeleteUserRoleNotInUser")]
	public class DeleteUserRoleNotInUserCriteria : DataCriteria
	{
		[DbParameter("USER_ID")]
		public long UserID { get; set; }
	}

	/// <summary>
	/// 用户类型
	/// </summary>
	/// <remarks>
	/// NS->公司用户
	/// SA->系统管理员
	/// </remarks>
	public enum UserTypes { NS, SA }
	
	
}
