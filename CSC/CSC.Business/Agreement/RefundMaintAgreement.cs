using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework.Data;
using App.Framework.Security;
using App.Framework;
using System.ComponentModel.DataAnnotations;

namespace CSC.Business
{
	public class RefundMaintAgreementListModel 
	{
		public BusinessList<RefundMaintAgreementModel> RefundMaintAgreementList { get; set; }

		public SearchRefundMaintAgreementCriteria SearchRefundMaintAgreement { get; set; }
	}

	public class RefundMaintAgreementModel:DataEntity
	{
		[DbField("CUST_PROD_ID")]
		public long CustProdId { get; set; }

		[DbField("CUST_PROD_NO")]
		public string CustProdNo { get; set; }

		[DbField("CUST_PROD_GRP_NO")]
		public string CustProdGrpNo { get; set; }

		[DbField("CUST_NAME")]
		public string CustName { get; set; }

		[DbField("CUST_NAME2")]
		public string CustName2 { get; set; }

		public string CustNameForSort
		{
			get
			{
				var retVar = "";
				if (!string.IsNullOrWhiteSpace(this.CustName))
				{
					retVar = retVar + this.CustName;
					if (!string.IsNullOrWhiteSpace(this.CustName2))
					{
						retVar = retVar + "/";
					}
				}

				if (!string.IsNullOrWhiteSpace(this.CustName2))
				{
					retVar = retVar + this.CustName2;
				}

				return retVar;
			}
		}

		[DbField("PHONE1")]
		public string Phone1 { get; set; }

		[DbField("PHONE1_TYPE_ID")]
		public long? Phone1TypeId { get; set; }

		[DbField("PHONE2")]
		public string Phone2 { get; set; }

		[DbField("PHONE2_TYPE_ID")]
		public long? Phone2TypeId { get; set; }

		[DbField("PHONE3")]
		public string Phone3 { get; set; }

		[DbField("PHONE3_TYPE_ID")]
		public long? Phone3TypeId { get; set; }

		[DbField("FAX")]
		public string Fax { get; set; }

		public string PhoneForSort
		{
			get
			{
				var retVar = "";
				if (!string.IsNullOrWhiteSpace(this.Phone1))
				{
					retVar = retVar + this.Phone1;
					if (!string.IsNullOrWhiteSpace(this.Phone2) || !string.IsNullOrWhiteSpace(this.Phone3))
					{
						retVar = retVar + ",";
					}
				}

				if (!string.IsNullOrWhiteSpace(this.Phone2))
				{
					retVar = retVar + this.Phone2;
					if (!string.IsNullOrWhiteSpace(this.Phone3))
					{
						retVar = retVar + ",";
					}
				}

				if (!string.IsNullOrWhiteSpace(this.Phone3))
				{
					retVar = retVar + this.Phone3;
				}
				return retVar;
			}
		}

		[DbField("CUST_CLASS_ID")]
		public long? CustClassId { get; set; }

		[DbField("CUST_CLASS_CODE")]
		public string CustClassCode { get; set; }

		[DbField("CUST_CLASS_DESC")]
		public string CustClassDesc { get; set; }

		[DbField("BRAND_ID")]
		public long BrandId { get; set; }

		[DbField("BRAND_CODE")]
		public string BrandCode { get; set; }

		[DbField("BRAND_DESC")]
		public string BrandDesc { get; set; }

		[DbField("MODEL_ID")]
		public long ModelId { get; set; }

		[DbField("MODEL_CODE")]
		public string ModelCode { get; set; }

		[DbField("MODEL_DESC")]
		public string ModelDesc { get; set; }

		[DbField("CUST_PROD_MAINT_ID")]
		public long CustProdMaintId { get; set; }

		[DbField("POLICY_NO")]
		public string PolicyNo { get; set; }

		[DbField("RENEW_MAINT_START_DATE")]
		public DateTime? RenewMaintStartDate { get; set; }

		[DbField("RENEW_MAINT_END_DATE")]
		public DateTime? RenewMaintEndDate { get; set; }

		[DbField("PAYMENT_NO")]
		public string PaymentNo { get; set; }

		[DbField("REFUND_PAYMENT_NO")]
		public string RefundPaymentNo { get; set; }

		[DbField("RENEW_OPTION")]
		public string RenewOption { get; set; }

		[DbField("RENEW_ANNUAL_AMT")]
		public decimal? RenewAnnualAmt { get; set; }

		[DbField("RENEW_GROUP_ANNUAL_AMT")]
		public decimal? RenewGroupAnnualAmt { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("REFUND_DATE")]
		public DateTime? RefundDate { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("REFUND_AMT")]
		public decimal? RefundAmt { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
		[DbField("REVISED_EXPIRY_DATE")]
		public DateTime? RevisedExpiryDate { get; set; }

		[DbField("AR_PAYMENT_ID")]
		public long? ArPaymentId { get; set; }

		[DbField("HAS_REFUND")]
		public bool HasRefund { get; set; }

		[DbField("CREATED_BY")]
		public long CreatedBy { get; set; }

		[DbField("CREATION_DATE")]
		public DateTime CreationDate { get; set; }

		[DbField("LAST_UPDATED_BY")]
		public long LastUpdatedBy { get; set; }

		[DbField("LAST_UPDATE_DATE")]
		public DateTime LastUpdateDate { get; set; }

		public override DataCriteria CreateSaveCriteria()
		{
			throw new NotImplementedException();
		}

		public override DataCriteria CreateDeleteCriteria()
		{
			throw new NotImplementedException();
		}
	}

	[DbCommand("spSearchRefundMaintAgreement")]
	public class SearchRefundMaintAgreementCriteria : DataCriteria
	{
		[DbParameter("PHONE1")]
		public string Phone1 { get; set; }

		[DbParameter("PHONE2")]
		public string Phone2 { get; set; }

		[DbParameter("PHONE3")]
		public string Phone3 { get; set; }

		[DbParameter("CUST_NAME")]
		public string CustName { get; set; }

		[DbParameter("DIST_CODE")]
		public string DistCode { get; set; }

		[DbParameter("ROAD_ESTATE_CODE")]
		public string RoadEstateCode { get; set; }

		[DbParameter("ROAD_NO")]
		public string RoadNo { get; set; }

		[DbParameter("BUILDING_CODE")]
		public string BuildingCode { get; set; }

		[DbParameter("FLOOR_NO")]
		public string FloorNo { get; set; }

		[DbParameter("MODEL_TYPE_CODE")]
		public string ModelTypeCode { get; set; }

		[DbParameter("MODEL_CODE")]
		public string ModelCode { get; set; }

		[DbParameter("BRAND_CODE")]
		public string BrandCode { get; set; }

		[DbParameter("SYS_CODE")]
		public string SysCode
		{
			get { return SecurityPortal.ApplicationName; }
		}

		[DbParameter("SHOP_CODE")]
		public string ShopCode
		{
			get { return App.Framework.Security.User.Current.ShopCode; }
		}

		[DbParameter("BU_CODE")]
		public string BuCode
		{
			get { return App.Framework.Security.User.Current.BUCode; }
		}
	}

	[DbCommand("spLoadRefundMaintAgreement")]
	public class LoadRefundMaintAgreementCriteria : DataCriteria 
	{
		[DbParameter("CUST_PROD_ID")]
		public long CustProdId { get; set; }

		[DbParameter("SYS_CODE")]
		public string SysCode
		{
			get { return SecurityPortal.ApplicationName; }
		}

		[DbParameter("SHOP_CODE")]
		public string ShopCode
		{
			get { return App.Framework.Security.User.Current.ShopCode; }
		}

		[DbParameter("BU_CODE")]
		public string BuCode
		{
			get { return App.Framework.Security.User.Current.BUCode; }
		}
	}

	[DbCommand("spSaveRefundMaintAgreement")]
	public class SaveRefundMaintAgreementCriteria : DataCriteria
	{
		[DbParameter("CUST_PROD_ID")]
		public long CustProdId { get; set; }

		[DbParameter("CUST_PROD_MAINT_ID")]
		public long CustProdMaintId { get; set; }

		[DbParameter("REFUND_DATE")]
		public DateTime RefundDate { get; set; }

		[DbParameter("REFUND_AMT")]
		public decimal RefundAmt { get; set; }

		[DbParameter("REVISED_EXPIRY_DATE")]
		public DateTime RevisedExpiryDate { get; set; }

		[DbParameter("BU_CODE")]
		public string BuCode
		{
			get { return App.Framework.Security.User.Current.BUCode; }
		}

		[DbParameter("CURRENT_USER_ID")]
		public long CurrentUserId
		{
			get { return App.Framework.Security.User.Current.UserId; }
		}
	}
}
