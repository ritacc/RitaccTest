using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework;
using App.Framework.Data;
using System.ComponentModel.DataAnnotations;
using App.Framework.Security;

namespace CSC.Business
{

	public class ShopListModel
	{

		public BusinessList<Shop> List { get; set; }

		public SearchShopCriteria Search { get; set; }

	}

	public class ShopViewModel
	{
		public Shop ShopModel { get; set; }

		public SearchShopCriteria SearchShopCriteria { get; set; }

		public BusinessList<Shop> ShopList { get; set; }
	}

    public class LoginToShopModel
    {
        public BusinessList<Shop> ShopList { get; set; }
        public string PwdWarningMesssage { get; set; }
    }

    [DbCommand("spSearchShopFromTechnician")]
    public class SearchShopFromTechnician : DataCriteria
    {
        [DbParameter("SYS_CODE")]
        public string SYS_CODE { get { return App.Framework.Security.SecurityPortal.ApplicationName; } }

        [DbParameter("SHOP_TYPE")]
        public string SHOP_TYPE { get { return "CS"; } }

        [DbParameter("BU_CODE")]
        public string BU_CODE { get { return App.Framework.Security.User.Current.BUCode; } }
    }

	[DbCommand("spSaveShop")]
	public class SaveShopCriteria : DataCriteria
	{
		private Shop m_Parent;

		public SaveShopCriteria(Shop parent)
		{
			m_Parent = parent;
		}

		[DbParameter("SYS_CODE")]
		public string SysCode
		{
			get { return m_Parent.SysCode; }
			set { m_Parent.SysCode = value; }
		}

		[DbParameter("CODE")]
		public string Code
		{
			get { return m_Parent.Code; }
			set { m_Parent.Code = value; }
		}

		[DbParameter("NAME")]
		public string Name
		{
			get { return m_Parent.Name; }
			set { m_Parent.Name = value; }
		}

		[DbParameter("ADDR_LINE1")]
		public string AddrLine1
		{
			get { return m_Parent.AddrLine1; }
			set { m_Parent.AddrLine1 = value; }
		}

		[DbParameter("ADDR_LINE2")]
		public string AddrLine2
		{
			get { return m_Parent.AddrLine2; }
			set { m_Parent.AddrLine2 = value; }
		}

		[DbParameter("PHONE_NO1")]
		public string PhoneNo1
		{
			get { return m_Parent.PhoneNo1; }
			set { m_Parent.PhoneNo1 = value; }
		}

		[DbParameter("PHONE_NO2")]
		public string PhoneNo2
		{
			get { return m_Parent.PhoneNo2; }
			set { m_Parent.PhoneNo2 = value; }
		}

		[DbParameter("FAX")]
		public string Fax
		{
			get { return m_Parent.Fax; }
			set { m_Parent.Fax = value; }
		}

		[DbParameter("EMAIL")]
		public string Email
		{
			get { return m_Parent.Email; }
			set { m_Parent.Email = value; }
		}

		[DbParameter("WEB_URL")]
		public string WebUrl
		{
			get { return m_Parent.WebUrl; }
			set { m_Parent.WebUrl = value; }
		}

		[DbParameter("PROV")]
		public string Prov
		{
			get { return m_Parent.Prov; }
			set { m_Parent.Prov = value; }
		}

		[DbParameter("CITY")]
		public string City
		{
			get { return m_Parent.City; }
			set { m_Parent.City = value; }
		}

		[DbParameter("AREA")]
		public string Area
		{
			get { return m_Parent.Area; }
			set { m_Parent.Area = value; }
		}

		[DbParameter("POSTAL_CODE")]
		public string PostalCode
		{
			get { return m_Parent.PostalCode; }
			set { m_Parent.PostalCode = value; }
		}

		[DbParameter("PHONE_AREA_CODE")]
		public string PhoneAreaCode
		{
			get { return m_Parent.PhoneAreaCode; }
			set { m_Parent.PhoneAreaCode = value; }
		}

		[DbParameter("LAST_UPDATED_BY")]
		public long? LastUpdatedBy
		{
			get { return m_Parent.LastUpdatedBy; }
			set { m_Parent.LastUpdatedBy = value; }
		}

		[DbParameter("LAST_UPDATE_DATE")]
		public DateTime LastUpdateDate
		{
			get { return m_Parent.LastUpdateDate; }
			set { m_Parent.LastUpdateDate = value; }
		}

		[DbParameter("FULL_NAME")]
		public string FullName
		{
			get { return m_Parent.FullName; }
			set { m_Parent.FullName = value; }
		}

		[DbParameter("ROWID")]
		public Guid RowID
		{
			get { return m_Parent.RowID; }
			set { m_Parent.RowID = value; }
		}

		[DbParameter("CURRENT_USER_ID")]
		public long CurrentUserID
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}

		[DbParameter("RecordStatus")]
		public string RecordStatus
		{
			get { return m_Parent.RecordStatus; }
			set { m_Parent.RecordStatus = value; }
		}

		[DbParameter("BU_CODE")]
		public string BU_CODE
		{
			get { return m_Parent.BU_CODE; }
			set { m_Parent.BU_CODE = value; }
		}

		[DbParameter("SHOP_TYPE")]
		public string SHOP_TYPE
		{
			get { return m_Parent.SHOP_TYPE; }
			set { m_Parent.SHOP_TYPE = value; }
		}
	}

	[DbCommand("spLoadShop")]
	public class LoadShopCriteria : DataCriteria
	{

		[DbParameter("SYS_CODE")]
		public string SysCode { get; set; }

		[DbParameter("CODE")]
		public string Code { get; set; }
	}
	[DbCommand("spLoadShop")]
	public class LoadShopORCriteria : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SysCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("CODE")]
		public string Code { get; set; }
	}

    [DbCommand("spLoadShopView")]
    public class spLoadShopViewCriteria : DataCriteria
    {

        [DbParameter("SYS_CODE")]
        public string SysCode { get; set; }

        [DbParameter("CODE")]
        public string Code { get; set; }
    }

	[DbCommand("spSearchShop")]
	public class SearchShopCriteria : DataCriteria
	{

		[DbParameter("USER_ID")]
		public long UserId { get; set; }
		//get {
		//    if (App.Framework.Security.User.Current == null) {
		//        return -1;
		//    } else {
		//        return App.Framework.Security.User.Current.UserId;
		//    }
		//} 

		[DbParameter("SYS_CODE")]
		public string SysCode { get; set; }
		//    get {
		//        return SecurityPortal.ApplicationName;
		//    }
		//}
		[DbParameter("CODE")]
		public string Code { get; set; }

		public string ShopCode { get; set; }
	}



	[DbCommand("spSearchShopForLov")]
	public class SearchShopForLovCriteria : DataCriteria
	{

		[DbParameter("SYS_CODE")]
		public string SYS_CODE { get { return SecurityPortal.ApplicationName; } }
	}

	[DbCommand("spSearchShopByUserID")]
	public class SearchShopByUserID : DataCriteria
	{
		[DbParameter("UserID")]
		public long UserID { get; set; }

		public Shop Load()
		{
			return BusinessPortal.Load<Shop>(this);
		}

		public BusinessList<Shop> List()
		{
			return BusinessPortal.Search<Shop>(this);
		}
	}

	[DbCommand("spShearchShopBySysCodeAndBuCode")]
	public class SearchWarehouseCriteria : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("BU_CODE")]
		public string BuCode { get { return App.Framework.Security.User.Current.BUCode; } }

		[DbParameter("USER_ID")]
		public long USER_ID
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
	}

	//[DbCommand("spShearchShopGodown")]
	//public class ShearchShopGodownCriteria : DataCriteria
	//{
	//    [DbParameter("SYS_CODE")]
	//    public string SystemCode { get { return SecurityPortal.ApplicationName; } }

	//    [DbParameter("BU_CODE")]
	//    public string BuCode { get { return App.Framework.Security.User.Current.BUCode; } }
	//}

	[DbCommand("spSearchShopByUserIDInUser")]
	public class SearchShopByUserIDInUserCriteria : DataCriteria
	{
		/// <summary>
		/// 系统编号
		/// </summary>
		[DbParameter("SYS_CODE")]
		public string SystemCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("USER_ID")]
		public long UserId { get; set; }

		[DbParameter("CURRENT_USER_ID")]
		public long CurrentUserID
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
	}

	[DbCommand("spSearchShopForDDL")]
	public class SearchShopCriteriaForDDL
	{
		[DbParameter("USER_ID")]
		public long UserID { get; set; }
		//public long UserId {
		//    get {
		//        return App.Framework.Security.User.Current.UserId;
		//    }
		//}
	}

	[DbCommand("spSearchBUForDDL")]
	public class SearchBUCriteria : DataCriteria
	{

	}



	#region For DLL
	public class ShopCodeForDllDataEntity : DataEntity
	{
		[DbField("CODE")]
		public string Code { get; set; }

		[DbField("SHOP_TYPE")]
		public string SHOP_TYPE { get; set; }

		public override DataCriteria CreateDeleteCriteria()
		{
			return null;
		}
		public override DataCriteria CreateSaveCriteria()
		{
			return null;
		}
	}
	[DbCommand("spSearchShopCodeForDDL")]
	public class SearchShopCodeForDDLCriteria : DataCriteria
	{
		[DbParameter("CODE")]
		public string Code { get; set; }

		[DbParameter("SHOP_TYPE")]
		public string SHOP_TYPE { get; set; }

		[DbParameter("USER_ID")]
		public long? USER_ID { get { return App.Framework.Security.User.Current.UserId; } }

		[DbParameter("SYS_CODE")]
		public string SYS_CODE { get { return SecurityPortal.ApplicationName; } }
	}
	#endregion

	public class Shop : DataEntity
	{

		[DbField("SYS_CODE")]
		public string SysCode { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("CODE")]
		public string Code { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("NAME")]
		public string Name { get; set; }

		[DbField("DEFUALT_GODOWN")]
		public string DEFUALT_GODOWN { get; set; }

		[DbField("ADDR_LINE1")]
		public string AddrLine1 { get; set; }

		[DbField("ADDR_LINE2")]
		public string AddrLine2 { get; set; }

		[DbField("PHONE_NO1")]
		[RegularExpression("((\\d{11})|^((\\d{7,8})|(\\d{4}|\\d{3})-(\\d{7,8})|(\\d{4}|\\d{3})-(\\d{7,8})-(\\d{4}|\\d{3}|\\d{2}|\\d{1})|(\\d{7,8})-(\\d{4}|\\d{3}|\\d{2}|\\d{1}))$)", ErrorMessage = "Phone format error, numbers and dashes only.")]
		public string PhoneNo1 { get; set; }

		[DbField("PHONE_NO2")]
		[RegularExpression("((\\d{11})|^((\\d{7,8})|(\\d{4}|\\d{3})-(\\d{7,8})|(\\d{4}|\\d{3})-(\\d{7,8})-(\\d{4}|\\d{3}|\\d{2}|\\d{1})|(\\d{7,8})-(\\d{4}|\\d{3}|\\d{2}|\\d{1}))$)", ErrorMessage = "Phone format error, numbers and dashes only.")]
		public string PhoneNo2 { get; set; }

		[DbField("FAX")]
		[RegularExpression("((\\d{11})|^((\\d{7,8})|(\\d{4}|\\d{3})-(\\d{7,8})|(\\d{4}|\\d{3})-(\\d{7,8})-(\\d{4}|\\d{3}|\\d{2}|\\d{1})|(\\d{7,8})-(\\d{4}|\\d{3}|\\d{2}|\\d{1}))$)", ErrorMessage = "Fax format error.")]
		public string Fax { get; set; }

		[DbField("EMAIL")]
		[RegularExpression("^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((.[a-zA-Z0-9_-]{2,3}){1,2})$", ErrorMessage = "Email format error.")]
		public string Email { get; set; }

		[RegularExpression("^([a-zA-z]+://)?[^\\s]*$", ErrorMessage = "Website URL format error.")]
		[DbField("WEB_URL")]
		public string WebUrl { get; set; }

		[DbField("PROV")]
		public string Prov { get; set; }

		[DbField("CITY")]
		public string City { get; set; }

		[DbField("AREA")]
		public string Area { get; set; }

		[DbField("POSTAL_CODE")]
		[RegularExpression("^[0-9]{1,10}$", ErrorMessage = "ZIP format error.")]
		public string PostalCode { get; set; }

		[DbField("PHONE_AREA_CODE")]
		[RegularExpression("^[0-9]{1,4}$", ErrorMessage = "Phone area code format error.")]
		public string PhoneAreaCode { get; set; }

		[DbField("LAST_UPDATE_DATE")]
		public DateTime LastUpdateDate { get; set; }

		[DbField("LAST_UPDATED_BY")]
		public long? LastUpdatedBy { get; set; }

		[DbField("CREATION_DATE")]
		public DateTime CreationDate { get; set; }

		[DbField("CREATED_BY")]
		public long? CreatedBy { get; set; }

		[DbField("LAST_UPDATE_LOGIN")]
		public long? LastUpdateLogin { get; set; }

		[DbField("MON_WK_DAY_FLAG")]
		public string MonWkDayFlag { get; set; }

		[DbField("TUE_WK_DAY_FLAG")]
		public string TueWkDayFlag { get; set; }

		[DbField("WED_WK_DAY_FLAG")]
		public string WedWkDayFlag { get; set; }

		[DbField("THU_WK_DAY_FLAG")]
		public string ThuWkDayFlag { get; set; }

		[DbField("FRI_WK_DAY_FLAG")]
		public string FriWkDayFlag { get; set; }

		[DbField("STA_WK_DAY_FLAG")]
		public string SatWkDayFlag { get; set; }

		[DbField("SUN_WK_DAY_FLAG")]
		public string SunWkDayFlag { get; set; }

		[DbField("APPT_DAILY_QUOTA")]
		public long? ApptDailyQuota { get; set; }

		[DbField("FULL_NAME")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		public string FullName { get; set; }

		//[DbField("ROWID")]
		public Guid RowID { get; set; }


        [DbField("PROVNAME")]
        public string ProvName { get; set; }

        [DbField("CITYNAME")]
        public string CityName { get; set; }

        [DbField("AREANAME")]
        public string AreaName { get; set; }

		[DbField("BU_CODE")]
		public string BU_CODE { get; set; }

		[DbField("SHOP_TYPE")]
		public string SHOP_TYPE { get; set; }

		[DbField("AUTH_PWD")]
		public string AUTH_PWD { get; set; }

		[DbField("RecordStatus")]
		public string RecordStatus { get; set; }

		public override DataCriteria CreateDeleteCriteria()
		{
			throw new NotImplementedException();
		}

		public override DataCriteria CreateSaveCriteria()
		{
			return new SaveShopCriteria(this);
		}
	}

	[DbCommand("spEditPassword")]
	public class EditPwdCriteria : DataCriteria
	{

		[DbParameter("AUTH_PWD")]
		public string AUTH_PWD { get; set; }

		[DbParameter("SYS_CODE")]
		public string SysCode { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("CODE")]
		public string Code { get; set; }

		[DbParameter("BU_CODE")]
		public string BU_CODE { get { return App.Framework.Security.User.Current.BUCode; } }

		[DbParameter("LAST_UPDATED_BY")]
		public long? LAST_UPDATED_BY
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
	}



	public class DefualtGodownModel : DataEntity
	{
		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("CODE")]
		public string Code { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("GodownID")]
		public long? GodownID { get; set; }

		
		public override DataCriteria CreateSaveCriteria()
		{
			return new SaveDefaultGodown(this);
		}

		public override DataCriteria CreateDeleteCriteria()
		{
			throw new NotImplementedException();
		}
	}

	[DbCommand("spSaveDefaultGodown")]
	public class SaveDefaultGodown : DataCriteria
	{
		private DefualtGodownModel m_Parent;

		[DbParameter("GodownID")]
		public long? GodownID { 
			get { return m_Parent.GodownID; }
			set { m_Parent.GodownID = value; }
		}

		[DbParameter("CODE")]
		public string Code
		{
			get { return m_Parent.Code; }
			set { m_Parent.Code = value; }
		}

		public SaveDefaultGodown(DefualtGodownModel mobj)
		{
			m_Parent=mobj;
		}
	}


	[DbCommand("spGetDefaultGodown")]
	public class GetDefaultGodownByWH : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SYS_CODE { get { return SecurityPortal.ApplicationName; } }

		[DbParameter("BU_CODE")]
		public string BU_CODE { get { return App.Framework.Security.User.Current.BUCode; } }

		[DbParameter("WH_CODE")]
		public string WH_CODE { get; set; }
	}


	/// <summary>
	/// shop type
	/// </summary>
	/// <remarks>
	/// CS
	/// GODOWN
	/// </remarks>
	public enum ShopType { CS, GODOWN }
}
