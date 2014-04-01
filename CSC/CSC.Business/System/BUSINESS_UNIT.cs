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
	public class BUSINESS_UNIT : DataEntity
	{

		[DbField("BU_CODE")]
		public string BU_CODE { get; set; }

		[DbField("BU_NAME")]
		public string BU_NAME { get; set; }

		[DbField("LOGO_FILE")]
		public string LOGO_FILE { get; set; }
		

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
