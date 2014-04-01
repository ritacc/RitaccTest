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
	#region UserRole

	public class UserShop : DataEntity
	{
		[DbField("SYS_CODE")]
		public string SysCode { get; set; }

		[DbField("USER_ID")]
		public long UserID { get; set; }

		[DbField("CODE")]
		public string Code { get; set; }

		[DbField("NAME")]
		public string Name { get; set; }

		[DbField("FULL_NAME")]
		public string FullName { get; set; }

		[DbField("ACTIVE_FLAG")]
		public bool ActiveFlag { get; set; }

		[DbField("ASSIGNMENT_FLAG")]
		public string ASSIGNMENT_FLAG { get; set; }

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

	
	[DbCommand("spSearchShopInUser")]
	public class UserShopInUserCriteria : DataCriteria
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

		[DbParameter("CURRENT_USER_ID")]
		public long CurrentUserID
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}

	}

	//写入单条SY_USER_shop
	[DbCommand("spAddUserShop")]
	public class AddUserShopCriteria : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("SHOP_CODE")]
		public string ShopCode { get; set; }

		[DbParameter("USER_ID")]
		public long UserID { get; set; }

		[DbParameter("CURRENT_USER_ID")]
		public long CurrentUserID
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
	}

	[DbCommand("spGetUserShopCount")]
	public class GetUserShopCountCriteria : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SystemCode
		{
			get 
			{
				return SecurityPortal.ApplicationName;
			}
		}

		[DbParameter("USER_ID")]
		public long UserID
		{
			get;
			set;
		}

		[DbParameter("USER_SHOP_COUNT", ParameterDirection.Output)]
		public int UserShopCount
		{
			get;
			set;
		}
	}

	//将User下面对应的SY_USER_shop删除
	[DbCommand("spDeleteUserShop")]
	public class DeleteUserShopCriteria : DataCriteria
	{
		[DbParameter("USER_ID")]
		public long UserID { get; set; }
	}

	//删除不在USER下面的shop
	[DbCommand("spDeleteUserShopNotInUser")]
	public class DeleteUserShopNotInUserCriteria : DataCriteria
	{
		[DbParameter("USER_ID")]
		public long UserID { get; set; }
	}

	#region added by Jason in 20120913 for remove user role list in add/edit page
	[DbCommand("spDeleteUserShopByShop")]
	public class DeleteUserShopByShopCriteria : DataCriteria
	{
		[DbParameter("USER_ID")]
		public long UserID { get; set; }

		[DbParameter("SHOP_CODE")]
		public string ShopCode { get; set; }
	}
	#endregion
}
