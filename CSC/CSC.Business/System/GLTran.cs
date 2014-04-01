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
	public class GlTranModel : DataEntity
	{
		#region "Field from GlTran table"

		[DbField("GL_TRAN_ID")]
		public long GlTranId { get; set; }
		[DbField("TRAN_DATE")]
		public DateTime TranDate { get; set; }
		[DbField("TRAN_TYPE")]
		public string TranType { get; set; }
		[DbField("SOURCE_TYPE")]
		public string SourceType { get; set; }
		[DbField("DOC_NO")]
		public string DocNo { get; set; }
		[DbField("DOC_TYPE")]
		public string DocType { get; set; }
		[DbField("COMPANY_CODE")]
		public string CompanyCode { get; set; }
		[DbField("COST_CENTRE")]
		public string CostCentre { get; set; }
		[DbField("NATURAL_ACCOUNT")]
		public string NaturalAccount { get; set; }
		[DbField("COUNTER_PARTY")]
		public string CounterParty { get; set; }
		[DbField("PRODUCT_LINE")]
		public string ProductLine { get; set; }
		[DbField("SALES_DESTINATION")]
		public string SalesDestination { get; set; }
		[DbField("ENTERED_DR")]
		public decimal EnteredDr { get; set; }
		[DbField("ENTERED_CR")]
		public decimal EnteredCr { get; set; }
		[DbField("GL_DATE")]
		public DateTime GlDate { get; set; }
		[DbField("REFERENCE")]
		public string Reference { get; set; }
		[DbField("SOURCE_REF_ID")]
		public long? SourceRefId { get; set; }
		[DbField("GODOWN_ID")]
		public long? GodownId { get; set; }
		[DbField("ITEM")]
		public string Item { get; set; }
		[DbField("TRAN_QTY")]
		public decimal? TranQty { get; set; }
		[DbField("UOM_ID")]
		public long? UomId { get; set; }
		[DbField("DESCRIPTION")]
		public string Description { get; set; }
		[DbField("CURRENCY_CODE")]
		public string CurrencyCode { get; set; }
		[DbField("CONVERSION_RATE")]
		public decimal ConversionRate { get; set; }
		[DbField("CREATED_BY")]
		public long CreatedBy { get; set; }
		[DbField("CREATION_DATE")]
		public DateTime CreationDate { get; set; }
		[DbField("LAST_UPDATED_BY")]
		public long LastUpdatedBy { get; set; }
		[DbField("LAST_UPDATE_DATE")]
		public DateTime LastUpdateDate { get; set; }
		[DbField("UNIT_COST")]
		public decimal? UnitCost { get; set; }
		[DbField("SUPPLIER_CODE")]
		public string SupplierCode { get; set; }
		[DbField("OF_IND")]
		public string OfInd { get; set; }


		#endregion

		#region "Other Field"
		[DbField("RecordStatus")]
		public string RecordStatus { get; set; }

		[DbField("CREATED_BY_NAME")]
		public string CreatedByName { get; set; }

		[DbField("LAST_UPDATED_BY_NAME")]
		public string LastUpdatedByName { get; set; }
		#endregion

		public override DataCriteria CreateSaveCriteria()
		{
			return null;
		}

		public override DataCriteria CreateDeleteCriteria()
		{
			return null;
		}

		public string GetWriteStr()
		{
			StringBuilder sb = new StringBuilder();
			if (OfInd == "APH")
			{
				sb.Append(StringHelper.GetStrByLeft("SWF", 10));//Bu Code
				sb.Append(StringHelper.GetStrByLeft("DEV", 10));//Bu Source
				sb.Append(StringHelper.GetStrByLeft(this.SupplierCode, 20));//Vendor Code
				sb.Append(StringHelper.GetStrByLeft(this.DocNo, 20));//Invoice Number
				sb.Append(StringHelper.GetStrByDate(this.TranDate));//Invoice Date
				sb.Append(StringHelper.getStrTypeDecimal(this.EnteredDr,10,2));//Invoice total amount
				sb.Append(StringHelper.GetStrByDate(this.GlDate));//GL Date
				sb.Append(StringHelper.GetStrByLeft(this.Reference, 50));//Invoice Description
				sb.Append(StringHelper.GetStrByLeft(this.CurrencyCode, 5));//Currency Code
				sb.Append(StringHelper.getStrTypeDecimal(this.ConversionRate,6,8));//Convertion rate
				sb.Append(StringHelper.GetStrByLeft("", 300));//Filler

			}
			else if (OfInd == "APD")
			{
				sb.Append(StringHelper.GetStrByLeft("SWF", 10));//Bu Code
				sb.Append(StringHelper.GetStrByLeft("DEV", 10));//Bu Source
				sb.Append(StringHelper.GetStrByLeft(this.SupplierCode, 20));//Vendor Code
				sb.Append(StringHelper.GetStrByLeft(this.DocNo, 20));//Invoice Number
				sb.Append(StringHelper.GetStrByLeft(this.CompanyCode, 20));//Company Code
				sb.Append(StringHelper.GetStrByLeft(this.CostCentre, 20));//Cost Centre
				sb.Append(StringHelper.GetStrByLeft(this.NaturalAccount, 20));//Natural Account
				sb.Append(StringHelper.GetStrByLeft(this.CounterParty, 20));//Counter Party
				sb.Append(StringHelper.GetStrByLeft(this.ProductLine, 50));//Product Line
				sb.Append(StringHelper.GetStrByLeft(this.SalesDestination, 20));//Sales Destination
				//Distribution amount
				if (this.EnteredCr > 0)
				{
					sb.Append(StringHelper.getStrTypeDecimal(this.EnteredDr, 10, 2));
				}
				else
				{
					sb.Append(StringHelper.getStrTypeDecimal(this.EnteredDr, 10, 2));
				}
				sb.Append(StringHelper.GetStrByLeft(this.Reference, 50));//Invoice Description
				sb.Append(StringHelper.GetStrByLeft("", 300));//Filler
			}
			else if (OfInd == "GLE" || OfInd == "GLT")
			{
				sb.Append(StringHelper.GetStrByLeft("DF", 10));//Prefix
				sb.Append(StringHelper.GetStrByLeft("PROVISIONS", 10));//Source
				sb.Append(StringHelper.GetStrByLeft(this.CompanyCode, 20));//Company Code
				sb.Append(StringHelper.GetStrByLeft(this.CostCentre, 20));//Cost Centre
				sb.Append(StringHelper.GetStrByLeft(this.NaturalAccount, 20));//Natural Account
				sb.Append(StringHelper.GetStrByLeft(this.CounterParty, 20));//Counter Party
				sb.Append(StringHelper.GetStrByLeft(this.ProductLine, 50));//Product Line
				sb.Append(StringHelper.GetStrByLeft(this.SalesDestination, 20));//Sales Destination
				sb.Append(StringHelper.getStrTypeDecimal(this.EnteredDr,10, 2));//Entered_Dr
				sb.Append(StringHelper.getStrTypeDecimal(this.EnteredDr, 10, 2));//Entered_Cr
				sb.Append(StringHelper.GetStrByDate(this.GlDate));//GL Date
				sb.Append(StringHelper.GetStrByLeft(this.Reference, 50));//Reference
				sb.Append(StringHelper.GetStrByLeft("", 200));//Filler
				sb.Append(StringHelper.GetStrByLeft(this.CurrencyCode, 5));//Currency Code
				sb.Append(StringHelper.getStrTypeDecimal(this.ConversionRate,6, 8));//Convertion rate
			}
			return sb.ToString();
		}
	}

	public class StringHelper
	{
		public static string getStrTypeDecimal(decimal data, int iLeft, int iRight)
		{
			string strLeft = string.Empty;
			string strRight = string.Empty; ;

			string str = data.ToString();

			string[] arr = str.Split('.');
			if (arr.Length == 2)
			{
				strLeft = arr[0];
				strRight = arr[1];
			}
			else
			{
					strLeft ="0";
				strRight = "0";
			}
			string result = string.Empty;
			if (strLeft.Length <= iLeft)
			{
				result = strLeft.PadLeft(iLeft, ' ');
			}
			else
			{
				result = strLeft.Substring(0, iLeft);
			}
			if (iRight > 0)
			{
				result += ".";
				if (strRight.Length <= iRight)
				{
					result += strRight.PadLeft(iRight, '0');
				}
				else
				{
					result += strRight.Substring(0, iRight);
				}
			}
			return result;
		}
		public static string GetStrByLeft(string Source, int len)
		{
			string result = "";
			if (Source.Length <= len)
			{
				result = Source.PadRight(len, ' ');
			}
			else
			{
				result = Source.Substring(0, len);
			}
			return result;
		}
		//public static string GetStrByRight(string Source, int len)
		//{
		//    string result = "";
		//    if (Source.Length <= len)
		//    {
		//        result = Source.PadRight(len, ' ');
		//    }
		//    else
		//    {
		//        result = Source.Substring(0, len);
		//    }
		//    return result;
		//}

		public static string GetStrByDate(DateTime? Source)
		{
			if (Source.HasValue)
			{
				return Source.Value.ToString("ddmmyy");
			}
			else
			{
				return "".PadRight(6);
			}
		}
	}
	
	[DbCommand("spSearchGlTran")]
	public class SearchGlTranCriteria : DataCriteria
	{
		[DbParameter("GL_DATE")]
		public DateTime GL_DATE { get; set; }
	}


}

