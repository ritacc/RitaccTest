using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework.Data;
using App.Framework;
using App.Framework.Security;
namespace CSC.Business
{

	[DbCommand("spGetSellingPrice")]
	public class UnitPriceDataCriteria : DataCriteria
	{

	    [DbParameter("SYS_CODE")]
	    public string SYSCODE
	    {
			get { return SecurityPortal.ApplicationName; }
	    }
	    [DbParameter("SHOP_CODE")]
	    public string SHOP_CODE
	    {
			get { return App.Framework.Security.User.Current.ShopCode; }
	    }

	    [DbParameter("BU_CODE")]
	    public string BuCode
	    {
	        get { return App.Framework.Security.User.Current.BUCode; }
	    }

		[DbParameter("CUST_PROD_ID")]
		public long? CustProdid{get;set;}

		[DbParameter("CLIENT_ID")]
		public long? ClientID { get; set; }

		[DbParameter("BRAND_ID")]
		public long? BrandId { get; set; }

		[DbParameter("MODEL_ID")]
		public long? ModelId { get; set; }

		[DbParameter("PARTS_ID")]
		public long? PartsID { get; set; }

		[DbParameter("DATE")]
		public DateTime? TransactionDate { get; set; }

		[DbParameter("JOB_SALES_MEMO_IND")]
		public string JobSalesMemoInd { get; set; }

		[DbParameter("ORDER_TYPE")]
		public string OrderType { get; set; }

		[DbParameter("PRICE_TYPE")]
		public string PriceType { get; set; }

		[DbParameter("ENQ_MODEL_TYPE_ID")]
		public long? ENQ_MODEL_TYPE_ID { get; set; }

		[DbParameter("ENQ_PROD_CLS_ID")]
		public long? ENQ_PROD_CLS_ID { get; set; }
	}


	public class UnitPriceResult : DataEntity
	{
	    [DbField("RESULT")]
	    public Decimal Result { get; set; }

	    public override DataCriteria CreateDeleteCriteria()
	    {
	        throw new NotImplementedException();
	    }

	    public override DataCriteria CreateSaveCriteria()
	    {
	        throw new NotImplementedException();
	    }
	}
}
