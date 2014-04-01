using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework.Data;

namespace CSC.Business
{
	public class CompanyModel:DataEntity
	{
		#region "Field from COMPANY table"

		[DbField("COMP_ID")]
		public long CompId { get; set; }

		[DbField("COMP_CODE")]
		public string CompCode { get; set; }

		[DbField("COMP_DESC")]
		public string CompDesc { get; set; }

		[DbField("BU_CODE")]
		public string BuCode { get; set; }

		[DbField("CREATED_BY")]
		public long CreatedBy { get; set; }

		[DbField("CREATION_DATE")]
		public DateTime CreationDate { get; set; }

		[DbField("LAST_UPDATED_BY")]
		public long LastUpdatedBy { get; set; }

		[DbField("LAST_UPDATE_DATE")]
		public DateTime LastUpdateDate { get; set; }

		#endregion

		public override DataCriteria CreateSaveCriteria()
		{
			throw new NotImplementedException();
		}

		public override DataCriteria CreateDeleteCriteria()
		{
			throw new NotImplementedException();
		}
	}

	[DbCommand("spSearchCompanyForDDL")]
	public class SearchCompanyForDDLCriteria : DataCriteria
	{
		[DbParameter("BU_CODE")]
		public string BuCode
		{
			get { return App.Framework.Security.User.Current.BUCode; }
		}
	}

	[DbCommand("spLoadCompany")]
	public class LoadCompanyCriteria : DataCriteria
	{
		[DbParameter("COMP_ID")]
		public long CompId { get; set; }
	}
}
