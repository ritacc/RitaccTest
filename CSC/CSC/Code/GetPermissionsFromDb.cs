using System;
using System.Collections.Generic;
using System.Linq;
using App.Framework.Web.Permissions;
using App.Framework.Web.User;
using System.Xml.Linq;
using App.Framework.Web;
using CSC.Business;
using App.Framework;

namespace CSC
{
	public class Func
	{
		public string FunctionCode { get; set; }
		public bool IsCreate { get; set; }
		public bool IsUpdate { get; set; }
	}


	/// <summary>
	/// 权限获取类
	/// </summary>
	public class GetPermissionsFromDb : IGetPermissions
	{
		private static readonly IGetPermissions _Instance = new GetPermissionsFromDb();
		public bool Openallpermissions;

		/// <summary>
		/// 获取单例
		/// </summary>
		public static IGetPermissions Instance { get { return _Instance; } }

		private GetPermissionsFromDb() { }

		/// <summary>
		/// 从数据库获取权限
		/// </summary>
		/// <param name="userIdentityKey">用户标识</param>
		/// <returns></returns>
		public IList<PermissionsPoint> GetPermissionsPoint(string userIdentityKey)
		{
			#region 本段可返回全部权限
			if (Openallpermissions)
			{
				var powerCommerceResourceArr = Enum.GetValues(typeof(EnumPermission));
				return (from object e in powerCommerceResourceArr select new PermissionsPoint(((int)e).ToString(), e.ToString(), string.Empty)).ToList();
			}
			#endregion

			var search = new GetUserPermissionCriteria();
			var roleFunctionrList = BusinessPortal.Search<RoleFunc>(search);
			if (roleFunctionrList == null || roleFunctionrList.Count <= 0)
				return null;
			var funList = roleFunctionrList.Select(item => new Func
															   {
																   FunctionCode = item.FuncCode,
																   IsCreate = item.InsertableFlag,
																   IsUpdate = item.UpdatableFlag
															   }).ToList();

			var menuPermissions = from m in XElement.Load(ConfigHelper.PermissionConfigPath).Descendants("Permissions")
								  select m;


			var allFunctions = from m in menuPermissions
							   where funList.ToList(n => n.FunctionCode).Contains(m.Attribute("FunctionCode").Value)
							   select m;
			var allPers = allFunctions.Descendants("Permission");
			var creatgePers = from m in allPers
							  where m.Attribute("type").Value == "Create"
							  select m;
			var updatePers = from m in allPers
							 where m.Attribute("type").Value == "Update"
							 select m;
			var defaultPers = from m in allPers
							  where m.Attribute("type").Value == "Default"
							  select m;

			var enumKeys = (from item in creatgePers let funcCode = item.Attribute("Key").Value where funList.Any(m => item.Parent.Attribute("FunctionCode").Value == m.FunctionCode && m.IsCreate) select funcCode).ToList();
			enumKeys.AddRange(from item in updatePers let funcCode = item.Attribute("Key").Value where funList.Any(m => item.Parent.Attribute("FunctionCode").Value == m.FunctionCode && m.IsUpdate) select funcCode);

			enumKeys.AddRange(from item in defaultPers let funcCode = item.Attribute("Key").Value where funList.Any(m => item.Parent.Attribute("FunctionCode").Value == m.FunctionCode) select funcCode);

			var enumPowerCommerceResourceArr = Enum.GetValues(typeof(EnumPermission));

			var hasPowers = (from object e in enumPowerCommerceResourceArr where enumKeys.Contains(e.ToString()) select e).ToList();


			var hasPermissions = hasPowers.Select(e => new PermissionsPoint(((int)e).ToString(), e.ToString(), string.Empty)).ToList();
			return hasPermissions;

		}

		public IList<PermissionsPoint> GetPermissionsPoint(string userIdentity, UserData user)
		{
			return GetPermissionsPoint(userIdentity);
		}
	}


}