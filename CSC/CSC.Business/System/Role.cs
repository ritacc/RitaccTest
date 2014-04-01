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
	#region Role

	public class Role:DataEntity
	{
		[DbField("ROLE_ID")]
		public long RoleID { get; set; }
		
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("ROLE_CODE")]
		public string RoleCode { get; set; }

		//[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("ROLE_TYPE")]
		public string RoleType { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("ROLE_DSC")]
		public string RoleDsc { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("SYSTEM_SCOPE")]
		public string SystemScope { get; set; }

		[DbField("ADMIN_FLAG")]
		public bool AdminFlag { get; set; }

		[DbField("FROZEN_FLAG")]
		public bool FrozenFlag { get; set; }

		[DbField("FROZEN_DATE")]
		public DateTime? FrozenDate { get; set; }

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

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("ROLE_SDSC")]
		public string RoleSdsc { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("SHOP_CODE")]
		public string ShopCode { get; set; }
		
		[DbField("SHOP_NAME")]
		public string ShopName { get; set; }

		[DbField("USER_TYPE")]
		public string UserType { get; set; }

		[DbField("BU_CODE")]
		public string BU_CODE { get; set; }

		public override DataCriteria CreateDeleteCriteria()
		{
			return new FrozenRoleCriteria(this);
		}

		public override DataCriteria CreateSaveCriteria()
		{
			return new SaveRoleCriteria(this);
		}

		public BusinessList<UserRole> UserRoleList { get; set; }

		public BusinessList<RoleFunc> RoleFuncList { get; set; }
	}

	#endregion

	#region delete

	[DbCommand("spDeleteRole")]
	public class DeleteRoleCriteria : DataCriteria
	{
		[DbParameter("ROLE_ID")]
		public long RoleId { get; set; }
	}

	#endregion

	#region SearchCriteria

	[DbCommand("spSearchRole")]
	public class SearchRoleCriteria:DataCriteria
	{
		/// <summary>
		/// 系统编号
		/// </summary>
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("ROLE_ID")]
		public long RoleID { get; set; }

		//[DbParameter("ROLE_DSC_FROM")]
		//public string RoleDescFrom { get; set; }

		//[DbParameter("ROLE_DSC_TO")]
		//public string RoleDescTo { get; set; }

		[DbParameter("ROLE_CODE")]
		public string ROLE_CODE { get; set; }

		[DbParameter("ROLE_TYPE")]
		public string RoleType { get; set; }

		[DbParameter("ADMIN_FLAG")]
		public bool? AdminFlag { get; set; }

		[DbParameter("USER_TYPE")]
		public string UserType { get { return App.Framework.Security.User.Current.UserType; } }

		[DbParameter("SHOP_CODE")]
		public string ShopCode { get { return App.Framework.Security.User.Current.ShopCode; } }
	}

	[DbCommand("spSearchRoleForDDL")]
	public class SearchRoleForDDLCriteria : DataCriteria
	{
		/// <summary>
		/// 系统编号
		/// </summary>
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("USER_TYPE")]
		public string UserType { get { return App.Framework.Security.User.Current.UserType; } }

		[DbParameter("SHOP_CODE")]
		public string ShopCode { get { return App.Framework.Security.User.Current.ShopCode; } }
		
	}

	//[DbCommand("spSearchRoleByUserID")]
	//public class SearchRoleByUserID : DataCriteria
	//{
	//    [DbParameter("UserID")]
	//    public long UserID { get; set; }
	//}

	//[DbCommand("spSearchUserRolesByUserID")]
	//public class SearchUserRolesByUserID : DataCriteria
	//{
	//    [DbParameter("UserID")]
	//    public long UserID { get; set; }
	//}

	#endregion

	#region SaveCriteria

	[DbCommand("spSaveRole")]
	public class SaveRoleCriteria : DataCriteria
	{
		public Role parent;

		public SaveRoleCriteria(Role parent)
		{
			this.parent = parent;
		}

		[DbParameter("ROLE_ID")]
		public long RoleID
		{
			get { return parent.RoleID; }
			set { parent.RoleID = value; }
		}

		[DbParameter("SYS_CODE")]
		public string SysCode
		{
			get { return parent.SystemCode; }
		}

		[DbParameter("ROLE_CODE")]
		public string RoleCode
		{
			get { return parent.RoleCode; }
			set { parent.RoleCode = value; }
		}

		[DbParameter("ROLE_TYPE")]
		public string RoleType
		{
			get { return parent.RoleType; }
			set { parent.RoleType = value; }
		}

		[DbParameter("ROLE_DSC")]
		public string RoleDsc
		{
			get { return parent.RoleDsc; }
			set { parent.RoleDsc = value; }
		}

		//[DbParameter("SYSTEM_SCOPE")]
		//public string SystemScope
		//{
		//    get { return parent.SystemScope; }
		//    set { parent.SystemScope = value; }
		//}

		[DbParameter("ADMIN_FLAG")]
		public bool AdminFlag
		{
			get { return parent.AdminFlag; }
			set { parent.AdminFlag = value; }
		}

		[DbParameter("ROLE_SDSC")]
		public string RoleSdsc
		{
			get { return parent.RoleSdsc; }
			set { parent.RoleSdsc = value; }
		}

		[DbParameter("SHOP_CODE")]
		public string ShopCode
		{
			get { return parent.ShopCode; }
			set { parent.ShopCode = value; }
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

		[DbParameter("USER_TYPE")]
		public string UserType
		{
			get { return App.Framework.Security.User.Current.UserType; }
		}

		[DbParameter("BU_CODE")]
		public string BuCode
		{
			get { return App.Framework.Security.User.Current.BUCode; }
		}
	}

	#endregion

	#region Frozen & Cancel Frozen

	[DbCommand("spUpdateRoleForzen")]
	public class FrozenRoleCriteria : DataCriteria
	{
		public Role parent;

		public FrozenRoleCriteria(Role parent)
		{
			this.parent = parent;
		}

		[DbParameter("ROLE_ID")]
		public long RoleID
		{
			get { return parent.RoleID; }
			set { parent.RoleID = value; }
		}

		[DbParameter("FROZEN_FLAG")]
		public bool FrozenFlag
		{
			get { return parent.FrozenFlag; }
			set { parent.FrozenFlag = value; }
		}

		[DbParameter("LAST_UPDATED_BY")]
		public long LastUpdatedBy
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
	}

	#endregion

	#region ListModel

	public class RoleListModel
	{
		public Role RoleModel { get; set; }

		public SearchRoleCriteria RoleSearch { get; set; }

		public BusinessList<Role> RoleList { get; set; }

		public BusinessList<Role> RoleForDropDownList { get; set; }

		
	}

	#endregion

	#region enum

	/// <summary>
	/// 角色类型
	/// </summary>
	/// <remarks>
	/// SY系统层面
	/// SH=>店层面
	/// </remarks>
	public enum RoleTypes { SY, SH }

	#endregion

	
}
