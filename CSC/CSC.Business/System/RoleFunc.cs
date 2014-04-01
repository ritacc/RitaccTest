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
	public class RoleFunc: DataEntity
	{
		[DbField("FUNC_ID")]
		public long FuncId
		{
			get;
			set;
		}

		[DbField("FUNC_CODE")]
		public string FuncCode
		{
			get;
			set;
		}

		[DbField("DSC")]
		public string Dsc
		{
			get;
			set;
		}

		[DbField("SYSTEM_SCOPE")]
		public string SystemScope
		{
			get;
			set;
		}

		[DbField("ADMIN_FLAG")]
		public bool AdminFlag
		{
			get;
			set;
		}

		[DbField("FUNC_TYPE")]
		public string FuncType
		{
			get;
			set;
		}

		[DbField("LAST_UPDATE_DATE")]
		public DateTime LastUpdateDate
		{
			get;
			set;
		}

		[DbField("LAST_UPDATED_BY")]
		public long LastUpdatedBy
		{
			get;
			set;
		}

		[DbField("ROLE_ID")]
		public long RoleID { get; set; }

		[DbField("ACTIVE_FLAG")]
		public bool ActiveFlag { get; set; }

		[DbField("INSERTABLE_FLAG")]
		public bool InsertableFlag { get; set; }

		[DbField("UPDATABLE_FLAG")]
		public bool UpdatableFlag { get; set; }

		public override DataCriteria CreateDeleteCriteria()
		{
			throw new NotImplementedException();
		}

		public override DataCriteria CreateSaveCriteria()
		{
			throw new NotImplementedException();
		}
	}

	[DbCommand("spSearchRoleFunc")]
	public class SearchRoleFuncCriteria : DataCriteria
	{
		/// <summary>
		/// 系统编号
		/// </summary>
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("ROLE_ID")]
		public long RoleID { get; set; }

		[DbParameter("ADMIN_FLAG")]
		public bool AdminFlag { get; set; }

	}

	[DbCommand("spUpdateBatchRoleFunc")]
	public class UpdateBatchRoleFuncCriteria : DataCriteria
	{
		[DbParameter("ROLE_ID")]
		public long RoleID { get; set; }
	}

	[DbCommand("spUpdateRoleFunc")]
	public class UpdateRoleFuncCriteria : DataCriteria
	{
		[DbParameter("FUNC_ID")]
		public long FuncID { get; set; }

		[DbParameter("ROLE_ID")]
		public long RoleID { get; set; }

		[DbParameter("INSERTABLE_FLAG")]
		public bool InsertableFlag { get; set; }

		[DbParameter("UPDATABLE_FLAG")]
		public bool UpdatableFlag { get; set; }

		[DbParameter("CURRENT_USER_ID")]
		public long CurrentUserID
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
	}

	/// <summary>
	/// 根据用户ID，SHOPCODE获取权限集合
	/// </summary>
	[DbCommand("spGetUserPermission")]
	public class GetUserPermissionCriteria : DataCriteria
	{
		/// <summary>
		/// 系统编号
		/// </summary>
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("USER_ID")]
		public long UserId { get { return App.Framework.Security.User.Current.UserId; } }

		[DbParameter("SHOP_CODE")]
		public string ShopCode { get { return App.Framework.Security.User.Current.ShopCode; } }

	}

	[DbCommand("spSearchRoleFuncByRole")]
	public class SearchRoleFuncByRoleCriteria : DataCriteria
	{
		/// <summary>
		/// 系统编号
		/// </summary>
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("ROLE_ID")]
		public long RoleID { get; set; }
	}

	/// <summary>
	/// 功能类型
	/// </summary>
	/// <remarks>
	/// FORM->页面
	/// BTN->BUTTON
	/// PWD->PASSWORD
	/// RPT->REPORT
	/// </remarks>
	public enum FunctionTypes { PWD, BTN, RPT, FORM }
}
