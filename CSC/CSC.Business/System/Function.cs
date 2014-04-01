using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework;
using App.Framework.Data;
using App.Framework.Security;
using System.ComponentModel.DataAnnotations;

namespace CSC.Business
{
	public class FunctionListModel
	{
		public BusinessList<Function> FunctionList
		{
			get;
			set;
		}

		public SearchFunctionCriteria SearchFunction
		{
			get;
			set;
		}
	}

	[DbCommand("spSearchFunction")]
	public class SearchFunctionCriteria : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SysCode
		{
			get { return SecurityPortal.ApplicationName; }
		}

		[DbParameter("ADMIN_FLAG")]
		public bool? AdminFlag
		{
			get;
			set;
		}

        [DbParameter("FunctionCode")]
        public string FunctionCode
        {
            get;
            set;
        }

        [DbParameter("FunctionDSC")]
        public string FunctionDSC
        {
            get;
            set;
        }
	}

	[DbCommand("spLoadFunction")]
	public class LoadFunctionCriteria : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SysCode
		{
			get { return SecurityPortal.ApplicationName; }
		}

		[DbParameter("FUNC_ID")]
		public long FuncId
		{
			get;
			set;
		}
	}

	[DbCommand("spSaveFunction")]
	public class SaveFunctionCriteria : DataCriteria
	{
		private Function m_Parent;

		public SaveFunctionCriteria(Function parent)
		{
			m_Parent = parent;
		}

		[DbParameter("FUNC_ID")]
		public long FuncID
		{
			get { return m_Parent.FuncId; }
			set { m_Parent.FuncId = value; }
		}

		[DbParameter("SYS_CODE")]
		public string SysCode
		{
			get { return App.Framework.Security.SecurityPortal.ApplicationName; }
		}

		[DbParameter("DSC")]
		public string Dsc
		{
			get { return m_Parent.Dsc; }
			set { m_Parent.Dsc = value; }
		}

		[DbParameter("ADMIN_FLAG")]
		public bool AdminFlag
		{
			get { return m_Parent.AdminFlag; }
			set { m_Parent.AdminFlag = value; }
		}

		[DbParameter("FUNC_TYPE")]
		public string FuncType
		{
			get { return m_Parent.FuncType; }
			set { m_Parent.FuncType = value; }
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

	public class Function : DataEntity
	{
		#region 功能表全信息
		[DbField("FUNC_ID")]
		public long FuncId
		{
			get;
			set;
		}

		[DbField("SYS_CODE")]
		public string SysCode
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
		
		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("DSC")]
		public string Dsc
		{
			get;
			set;
		}

		[DbField("EXECUTABLE")]
		public string Executable
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

		[DbField("WND_POS_X")]
		public long? WndPosX
		{
			get;
			set;
		}

		[DbField("WND_POS_Y")]
		public long? WndPosY
		{
			get;
			set;
		}

		[DbField("WND_HEIGHT")]
		public long? WndHeight
		{
			get;
			set;
		}

		[DbField("WND_WIDTH")]
		public long? WndWidth
		{
			get;
			set;
		}

		[DbField("BG_COLOR")]
		public string BgColor
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
			return new SaveFunctionCriteria(this);
		}
	}
}
