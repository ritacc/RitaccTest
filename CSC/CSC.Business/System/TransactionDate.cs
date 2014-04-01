using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework.Data;
using App.Framework;

namespace CSC.Business
{
	[DbCommand("spGetTransactionDate")]
	public class GetTransactionDate : DataCriteria
	{
		
		[DbParameter("BU_CODE")]
		public string BuCode
		{
			get { return App.Framework.Security.User.Current.BUCode; }
		}

	}


	public class TransactionDateResult : DataEntity
	{
		[DbField("RESULT")]
		public DateTime Result { get; set; }

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

