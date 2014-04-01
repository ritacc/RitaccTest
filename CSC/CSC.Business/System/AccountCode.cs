using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using App.Framework;
using App.Framework.Data;
using App.Framework.Security;

namespace CSC.Business
{
	 public class AccountCodeListModel
    {
        public BusinessList<AccountCodeModel> AccountCodeList
        {
            get;
            set;
        }

        public SearchAccountCodeCriteria SearchAccountCode
        {
            get;
            set;
        }
    }

    public class AccountCodeModel : DataEntity
    {        
        #region "Field from AccountCode table"
		
		[DbField("ACCT_ID")]
		public long AcctId { get; set; }
		[DbField("ACCT_DESC")]
		public string AcctDesc { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("ACCT_TYPE_ID")]
		public long? AcctTypeId { get; set; }
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

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("ACCT_SEG_1")]
		public string AcctSeg1 { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("ACCT_SEG_2")]
		public string AcctSeg2 { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("ACCT_SEG_3")]
		public string AcctSeg3 { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("ACCT_SEG_4")]
		public string AcctSeg4 { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("ACCT_SEG_5")]
		public string AcctSeg5 { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("ACCT_SEG_6")]
		public string AcctSeg6 { get; set; }


		[DbField("ACCT_TYPE_CODE")]
		public string ACCT_TYPE_CODE { get; set; }
		[DbField("ACCT_TYPE_DESC")]
		public string ACCT_TYPE_DESC { get; set; }

		[DbField("ACCT_SEG_1_Desc")]
		public string ACCT_SEG_1_Desc { get; set; }
		[DbField("ACCT_SEG_2_Desc")]
		public string ACCT_SEG_2_Desc { get; set; }
		[DbField("ACCT_SEG_3_Desc")]
		public string ACCT_SEG_3_Desc { get; set; }
		[DbField("ACCT_SEG_4_Desc")]
		public string ACCT_SEG_4_Desc { get; set; }
		[DbField("ACCT_SEG_5_Desc")]
		public string ACCT_SEG_5_Desc { get; set; }

		[DbField("ACCT_SEG_6_Desc")]
		public string ACCT_SEG_6_Desc { get; set; }
		#endregion

		#region "Other Field"
		[DbField("ACCOUNT_CODE")]
		public string ACCOUNT_CODE { get; set; }		

		[DbField("RecordStatus")]
		public string RecordStatus { get; set; }		

		[DbField("CREATED_BY_NAME")]
		public string CreatedByName { get; set; }

		[DbField("LAST_UPDATED_BY_NAME")]
		public string LastUpdatedByName { get; set; }
		#endregion

		public override DataCriteria CreateSaveCriteria()
		{
			return new SaveAccountCodeCriteria(this);
		}

		public override DataCriteria CreateDeleteCriteria()
		{
			return new DeleteAccountCodeCriteria(this);
		}
	}

	[DbCommand("spSearchAccountCode")]
	public class SearchAccountCodeCriteria : DataCriteria
	{
		[DbParameter("ACCT_TYPE_ID")]
		public long ACCT_TYPE_ID { get; set; }

		[DbParameter("ACCT_DESC")]
		public string ACCT_DESC { get; set; }
	}

	[DbCommand("spSaveAccountCode")]
	public class SaveAccountCodeCriteria : DataCriteria
	{

		 private AccountCodeModel m_Parent;
        public SaveAccountCodeCriteria(AccountCodeModel parent)
		{
			m_Parent = parent;
		}

		        [DbParameter("ACCT_ID")]
        public long AcctId
        {
            get { return m_Parent.AcctId; }
            set { m_Parent.AcctId = value; }
        }

        [DbParameter("ACCT_DESC")]
        public string AcctDesc
        {
            get { return m_Parent.AcctDesc; }
            set { m_Parent.AcctDesc = value; }
        }

        [DbParameter("ACCT_TYPE_ID")]
        public long? AcctTypeId
        {
            get { return m_Parent.AcctTypeId; }
            set { m_Parent.AcctTypeId = value; }
        }

		[DbParameter("BU_CODE")]
		public string BuCode
		{
			get { return App.Framework.Security.User.Current.BUCode; }
		}
        [DbParameter("ACCT_SEG_1")]
        public string AcctSeg1
        {
            get { return m_Parent.AcctSeg1; }
            set { m_Parent.AcctSeg1 = value; }
        }

        [DbParameter("ACCT_SEG_2")]
        public string AcctSeg2
        {
            get { return m_Parent.AcctSeg2; }
            set { m_Parent.AcctSeg2 = value; }
        }

        [DbParameter("ACCT_SEG_3")]
        public string AcctSeg3
        {
            get { return m_Parent.AcctSeg3; }
            set { m_Parent.AcctSeg3 = value; }
        }

        [DbParameter("ACCT_SEG_4")]
        public string AcctSeg4
        {
            get { return m_Parent.AcctSeg4; }
            set { m_Parent.AcctSeg4 = value; }
        }

        [DbParameter("ACCT_SEG_5")]
        public string AcctSeg5
        {
            get { return m_Parent.AcctSeg5; }
            set { m_Parent.AcctSeg5 = value; }
        }

        [DbParameter("ACCT_SEG_6")]
        public string AcctSeg6
        {
            get { return m_Parent.AcctSeg6; }
            set { m_Parent.AcctSeg6 = value; }
        }



		[DbParameter("CREATED_BY")]
		public long CreatedBy
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}

		[DbParameter("CREATION_DATE")]
		public DateTime CreationDate
		{
			get { return m_Parent.CreationDate; }
			set { m_Parent.CreationDate = value; }
		}

		[DbParameter("LAST_UPDATED_BY")]
		public long LastUpdatedBy
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}

		[DbParameter("LAST_UPDATE_DATE")]
		public DateTime LastUpdateDate
		{
			get { return m_Parent.LastUpdateDate; }
			set { m_Parent.LastUpdateDate = value; }
		}

		[DbParameter("RecordStatus")]
		public string RecordStatus
		{
			get { return m_Parent.RecordStatus; }
			set { m_Parent.RecordStatus = value; }
		}
	}

	[DbCommand("spDeleteAccountCode")]
	public class DeleteAccountCodeCriteria : DataCriteria
	{
		private AccountCodeModel m_Parent;

		public DeleteAccountCodeCriteria(AccountCodeModel parent)
		{
			m_Parent = parent;
		}

		[DbParameter("ACCT_ID")]
		public long AcctId
		{
			get { return m_Parent.AcctId; }
			set { m_Parent.AcctId = value; }
		}
	}

	[DbCommand("spLoadAccountCode")]
	public class LoadAccountCodeCriteria : DataCriteria
	{
		[DbParameter("ACCT_ID")]
		public long AcctId { get; set; }
	}

	public class AccountCodeTypeForDll:DataEntity
	{
		[DbField("ACCT_TYPE_ID")]
		public long AcctTypeId { get; set; }
		[DbField("ACCT_TYPE_CODE")]
		public string AcctTypeCode { get; set; }
		[DbField("ACCT_TYPE_DESC")]
		public string AcctTypeDesc { get; set; }

		public override DataCriteria CreateSaveCriteria()
		{
			return null;
		}

		public override DataCriteria CreateDeleteCriteria()
		{
			return null;
		}
	}

	[DbCommand("spSearchAccountType")]
	public class SearchAccountCodeTypeForDllCriteria : DataCriteria
	{

	}

	[DbCommand("spSearchAcctForPaymentMethod")]
	public class SearchAcctForPaymentMethodCriteria : DataCriteria
	{
	
	}

}

