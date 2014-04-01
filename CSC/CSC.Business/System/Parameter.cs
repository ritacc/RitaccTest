using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework.Data;
using App.Framework;

namespace CSC.Business
{
	[DbCommand("spGetParameter")]
	public class GetParameter : DataCriteria
	{
		[DbParameter("SYS_CODE")]
		public string SYS_CODE
		{
			get
			{
				return App.Framework.Security.SecurityPortal.ApplicationName;
			}
		}
		[DbParameter("SHOP_CODE")]
		public string SHOP_CODE
		{
			get
			{
				return App.Framework.Security.User.Current.ShopCode;
			}
		}

		[DbParameter("BU_CODE")]
		public string BuCode
		{
			get { return App.Framework.Security.User.Current.BUCode; }
		}

		[DbParameter("CODE")]
		public string CODE { get; set; }
	}


	public class ParameterResult : DataEntity
	{
		[DbField("RESULT")]
		public string Result { get; set; }

		public override DataCriteria CreateDeleteCriteria()
		{
			throw new NotImplementedException();
		}

		public override DataCriteria CreateSaveCriteria()
		{
			throw new NotImplementedException();
		}
	}

	public class ParameterResultLong : DataEntity
	{
		[DbField("RESULT")]
		public long? Result { get; set; }

		public override DataCriteria CreateDeleteCriteria()
		{
			throw new NotImplementedException();
		}

		public override DataCriteria CreateSaveCriteria()
		{
			throw new NotImplementedException();
		}
	}

	public class Parameter : DataEntity
	{
		[DbField("PARA_ID")]
		public long ParaID { get; set; }

		[DbField("SYS_CODE")]
		public string SysCode;

		[DbField("CODE")]
		public string Code { get; set; }

		[DbField("MODULE")]
		public string Module { get; set; }

		[DbField("DSC")]
		public string Dsc { get; set; }

		[DbField("REMARKS")]
		public string Remarks { get; set; }

		[DbField("PARA_TYPE")]
		public string ParaType { get; set; }

		[DbField("VALUE")]
		public string Value { get; set; }

		[DbField("LAST_UPDATE_DATE")]
		public DateTime? LastUpdateDate { get; set; }

		[DbField("LAST_UPDATED_BY")]
		public long LastUpdatedBy { get; set; }

		[DbField("CREATION_DATE")]
		public DateTime? CreationDate { get; set; }

		[DbField("CREATED_BY")]
		public long CreatedBy { get; set; }

		[DbField("LAST_UP0DATE_LOGIN")]
		public long LastUpdateLogin { get; set; }

		#region 店参数关联

		[DbField("SHOP_CODE")]
		public string ShopCode { get; set; }



		#endregion

		public string ActionType { get; set; }

		public override DataCriteria CreateDeleteCriteria()
		{
			throw new NotImplementedException();
		}

		public override DataCriteria CreateSaveCriteria()
		{
			throw new NotImplementedException();
		}
	}

	[DbCommand("spSearchParameter")]
	public class SearchParameterCriteria : DataCriteria
	{
		[DbParameter("SysCode")]
		public string SysCode { get; set; }

		[DbParameter("ShopCode")]
		public string ShopCode { get; set; }

		public Parameter Load()
		{
			return BusinessPortal.Load<Parameter>(this);
		}

		public BusinessList<Parameter> List()
		{
			return BusinessPortal.Search<Parameter>(this);
		}
	}


	[DbCommand("spLoadParameter")]
	public class LoadParameterCriteria : DataCriteria
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

		[DbParameter("CODE")]
		public string Code
		{
			get;
			set;
		}
	}

	[DbCommand("spSaveParameter")]
	public class SaveParameterCriteria : DataCriteria
	{
		public Parameter parent;

		public SaveParameterCriteria(Parameter parent)
		{
			this.parent = parent;
		}

		[DbParameter("SYS_CODE")]
		public string SysCode
		{
			get { return parent.SysCode; }
			set { parent.SysCode = value; }
		}

		[DbParameter("SHOP_CODE")]
		public string ShopCode
		{
			get { return parent.ShopCode; }
			set { parent.ShopCode = value; }
		}

		[DbParameter("PARA_ID")]
		public long ParaID
		{
			get { return parent.ParaID; }
			set { parent.ParaID = value; }
		}

		[DbParameter("PARAM_CODE")]
		public string Code
		{
			get { return parent.Code; }
			set { parent.Code = value; }
		}

		[DbParameter("VALUE")]
		public string Value
		{
			get { return parent.Value; }
			set { parent.Value = value; }
		}

		[DbParameter("LAST_UPDATE_DATE")]
		public DateTime? LastUpdateDate
		{
			get { return parent.LastUpdateDate; }
			set { parent.LastUpdateDate = value; }
		}

		[DbParameter("CUR_USER")]
		public long CurrentUser
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}

		[DbParameter("ActionType")]
		public string ActionType
		{
			get { return parent.ActionType; }
			set { parent.ActionType = value; }
		}
	}

	public class ParameterListModel
	{
		public SearchParameterCriteria Search { get; set; }

		public BusinessList<Parameter> List { get; set; }
	}
}
