using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework;
using App.Framework.Data;
using System.ComponentModel.DataAnnotations;

namespace CSC.Business
{
	public class HolidayListModel
	{
		public WorkingDay WorkingDay
		{
			get;
			set;
		}

		public BusinessList<Holiday> HolidayList
		{
			get;
			set;
		}

		public SearchHolidayCriteria SearchHoliday
		{
			get;
			set;
		}
	}

	[DbCommand("spSearchHoliday")]
	public class SearchHolidayCriteria : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SysCode
		{
			get
			{
				return App.Framework.Security.SecurityPortal.ApplicationName;
			}
		}

		[DbParameter("SHOP_CODE")]
		public string ShopCode
		{
			get
			{
				return App.Framework.Security.User.Current.ShopCode;
			}
		}

		[DbParameter("HOLIDAY_YEAR")]
		public string HolidayYear
		{
			get;
			set;
		}
	}

	[DbCommand("spLoadWorkingDay")]
	public class LoadWorkingDayCriteria : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SysCode
		{
			get
			{
				return App.Framework.Security.SecurityPortal.ApplicationName;
			}
		}

		[DbParameter("SHOP_CODE")]
		public string ShopCode
		{
			get
			{
				return App.Framework.Security.User.Current.ShopCode;
			}
		}
	}

	[DbCommand("spSaveWorkingDay")]
	public class SaveWorkingDayCriteria: DataCriteria
	{
	    private WorkingDay m_Parent;

		public SaveWorkingDayCriteria(WorkingDay parent)
	    {
	        m_Parent = parent;
	    }

	    [DbParameter("SYS_CODE")]
	    public string SysCode
	    {
	        get { return App.Framework.Security.SecurityPortal.ApplicationName; }
	    }

	    [DbParameter("SHOP_CODE")]
	    public string ShopCode
	    {
	        get { return App.Framework.Security.User.Current.ShopCode; }
	    }

	    [DbParameter("MON_WK_DAY_FLAG")]
	    public bool MonWkDayFlag
	    {
	        get { return m_Parent.MonWkDayFlag; }
	        set { m_Parent.MonWkDayFlag = value; }
	    }

	    [DbParameter("TUE_WK_DAY_FLAG")]
	    public bool TueWkDayFlag
	    {
	        get { return m_Parent.TueWkDayFlag; }
	        set { m_Parent.TueWkDayFlag = value; }
	    }

	    [DbParameter("WED_WK_DAY_FLAG")]
	    public bool WedWkDayFlag
	    {
	        get { return m_Parent.WedWkDayFlag; }
	        set { m_Parent.WedWkDayFlag = value; }
	    }

	    [DbParameter("THU_WK_DAY_FLAG")]
	    public bool ThuWkDayFlag
	    {
	        get { return m_Parent.ThuWkDayFlag; }
	        set { m_Parent.ThuWkDayFlag = value; }
	    }

	    [DbParameter("FRI_WK_DAY_FLAG")]
	    public bool FriWkDayFlag
	    {
	        get { return m_Parent.FriWkDayFlag; }
	        set { m_Parent.FriWkDayFlag = value; }
	    }

	    [DbParameter("SAT_WK_DAY_FLAG")]
	    public bool SatWkDayFlag
	    {
	        get { return m_Parent.SatWkDayFlag; }
	        set { m_Parent.SatWkDayFlag = value; }
	    }

	    [DbParameter("SUN_WK_DAY_FLAG")]
	    public bool SunWkDayFlag
	    {
	        get { return m_Parent.SunWkDayFlag; }
	        set { m_Parent.SunWkDayFlag = value; }
	    }

	    [DbParameter("LAST_UPDATE_DATE")]
	    public DateTime LastUpdateDate
	    {
	        get { return m_Parent.LastUpdateDate; }
	        set { m_Parent.LastUpdateDate = value; }
	    }

	    [DbParameter("LAST_UPDATED_BY")]
	    public long LastUpdatedBy
	    {
	        get { return m_Parent.LastUpdatedBy; }
	        set { m_Parent.LastUpdatedBy = value; }
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
	}

	[DbCommand("spLoadHoliday")]
	public class LoadHolidayCriteria : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SysCode
		{
			get
			{
				return App.Framework.Security.SecurityPortal.ApplicationName;
			}
		}

		[DbParameter("SHOP_CODE")]
		public string ShopCode
		{
			get
			{
				return App.Framework.Security.User.Current.ShopCode;
			}
		}

		[DbParameter("EFF_DATE")]
		public DateTime EffDate
		{
			get;
			set;
		}
	}


	[DbCommand("spSaveHoliday")]
	public class SaveHolidayCriteria : DataCriteria
	{
		private Holiday m_Parent;

		public SaveHolidayCriteria(Holiday parent)
		{
			m_Parent = parent;
		}

		[DbParameter("SYS_CODE")]
		public string SysCode
		{
			get { return App.Framework.Security.SecurityPortal.ApplicationName; }
		}

		[DbParameter("SHOP_CODE")]
		public string ShopCode
		{
			get { return App.Framework.Security.User.Current.ShopCode; }
		}

		[DbParameter("EFF_DATE")]
		public DateTime EffDate
		{
			get { return m_Parent.EffDate; }
			set { m_Parent.EffDate = value; }
		}

		[DbParameter("DSC")]
		public string Dsc
		{
			get { return m_Parent.Dsc; }
			set { m_Parent.Dsc = value; }
		}

		[DbParameter("ACTIVE_FLAG")]
		public bool ActiveFlag
		{
			get { return m_Parent.ActiveFlag; }
			set { m_Parent.ActiveFlag = value; }
		}

		[DbParameter("LAST_UPDATE_DATE")]
		public DateTime LastUpdateDate
		{
			get { return m_Parent.LastUpdateDate; }
			set { m_Parent.LastUpdateDate = value; }
		}

		[DbParameter("LAST_UPDATED_BY")]
		public long LastUpdatedBy
		{
			get { return m_Parent.LastUpdatedBy; }
			set { m_Parent.LastUpdatedBy = value; }
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
	}

	[DbCommand("spDeleteHoliday")]
	public class DeleteHolidayCriteria : DataCriteria
	{
		private Holiday m_Parent;
		public DeleteHolidayCriteria(Holiday parent)
		{
			m_Parent = parent;
		}

		[DbParameter("SYS_CODE")]
		public string SysCode
		{
			get { return App.Framework.Security.SecurityPortal.ApplicationName; }
		}

		[DbParameter("SHOP_CODE")]
		public string ShopCode
		{
			get { return App.Framework.Security.User.Current.ShopCode; }
		}

		[DbParameter("EFF_DATE")]
		public DateTime EffDate
		{
			get { return m_Parent.EffDate; }
			set { m_Parent.EffDate = value; }
		}
	}



	public class WorkingDay : DataEntity
	{
		#region 店表全部字段
		[DbField("SYS_CODE")]
		public string SysCode
		{
			get;
			set;
		}

		[DbField("CODE")]
		public string Code
		{
			get;
			set;
		}

		[DbField("NAME")]
		public string Name
		{
			get;
			set;
		}

		[DbField("ADDR_LINE1")]
		public string AddrLine1
		{
			get;
			set;
		}

		[DbField("ADDR_LINE2")]
		public string AddrLine2
		{
			get;
			set;
		}

		[DbField("PHONE_NO1")]
		public string PhoneNo1
		{
			get;
			set;
		}

		[DbField("PHONE_NO2")]
		public string PhoneNo2
		{
			get;
			set;
		}

		[DbField("FAX")]
		public string Fax
		{
			get;
			set;
		}

		[DbField("EMAIL")]
		public string Email
		{
			get;
			set;
		}

		[DbField("WEB_URL")]
		public string WebUrl
		{
			get;
			set;
		}

		[DbField("PROV")]
		public string Prov
		{
			get;
			set;
		}

		[DbField("CITY")]
		public string City
		{
			get;
			set;
		}

		[DbField("AREA")]
		public string Area
		{
			get;
			set;
		}

		[DbField("POSTAL_CODE")]
		public string PostalCode
		{
			get;
			set;
		}

		[DbField("PHONE_AREA_CODE")]
		public string PhoneAreaCode
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

		[DbField("CREATION_DATE")]
		public DateTime CreationDate
		{
			get;
			set;
		}

		[DbField("CREATED_BY")]
		public long CreatedBy
		{
			get;
			set;
		}

		[DbField("LAST_UPDATE_LOGIN")]
		public long? LastUpdateLogin
		{
			get;
			set;
		}

		[DbField("MON_WK_DAY_FLAG")]
		public bool MonWkDayFlag
		{
			get;
			set;
		}

		[DbField("TUE_WK_DAY_FLAG")]
		public bool TueWkDayFlag
		{
			get;
			set;
		}

		[DbField("WED_WK_DAY_FLAG")]
		public bool WedWkDayFlag
		{
			get;
			set;
		}

		[DbField("THU_WK_DAY_FLAG")]
		public bool ThuWkDayFlag
		{
			get;
			set;
		}

		[DbField("FRI_WK_DAY_FLAG")]
		public bool FriWkDayFlag
		{
			get;
			set;
		}

		[DbField("SAT_WK_DAY_FLAG")]
		public bool SatWkDayFlag
		{
			get;
			set;
		}

		[DbField("SUN_WK_DAY_FLAG")]
		public bool SunWkDayFlag
		{
			get;
			set;
		}

		[DbField("APPT_DAILY_QUOTA")]
		public long? ApptDailyQuota
		{
			get;
			set;
		}

		[DbField("FULL_NAME")]
		public string FullName
		{
			get;
			set;
		}

		[DbField("ROWID")]
		public Guid RowId
		{
			get;
			set;
		}
		#endregion

		#region 其他附加信息
		[DbField("RecordStatus")]
		public string RecordStatus
		{
			get;
			set;
		}
		#endregion

		public override DataCriteria CreateDeleteCriteria()
		{
			throw new NotImplementedException();
		}

		public override DataCriteria CreateSaveCriteria()
		{
			return new SaveWorkingDayCriteria(this);
		}
	}

	public class Holiday : DataEntity
	{
		#region 假期表的全部记录
		[DbField("SYS_CODE")]
		public string SysCode
		{
			get;
			set;
		}

		[DbField("SHOP_CODE")]
		public string ShopCode
		{
			get;
			set;
		}

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("EFF_DATE")]
		public DateTime EffDate
		{
			get;
			set;
		}

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("DSC")]
		public string Dsc
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

		[DbField("CREATION_DATE")]
		public DateTime CreationDate
		{
			get;
			set;
		}

		[DbField("CREATED_BY")]
		public long CreatedBy
		{
			get;
			set;
		}

		[DbField("LAST_UPDATE_LOGIN")]
		public long LastUpdateLogin
		{
			get;
			set;
		}

		[DbField("ACTIVE_FLAG")]
		public bool ActiveFlag
		{
			get;
			set;
		}
		#endregion

		#region 其他附加信息
		[DbField("RecordStatus")]
		public string RecordStatus
		{
			get;
			set;
		}
		#endregion

		public override DataCriteria CreateDeleteCriteria()
		{
			return new DeleteHolidayCriteria(this);
		}

		public override DataCriteria CreateSaveCriteria()
		{
			return new SaveHolidayCriteria(this);
		}
	}
}
