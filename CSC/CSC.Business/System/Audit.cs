using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework.Data;

namespace CSC.Business
{
	public class Audit:DataEntity
	{
		[DbField("CREATED_BY")]
		public string CreatedBy { get; set; }

		[DbField("CREATION_DATE")]
		public DateTime CreationDate { get; set; }

		[DbField("LAST_UPDATED_BY")]
		public string LastUpdatedBy { get; set; }

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

	//public class AuditForDb : DataEntity
	//{
	//    [DbField("CREATED_BY")]
	//    public long? CREATED_BY { get; set; }

	//    [DbField("CREATION_DATE")]
	//    public DateTime CREATION_DATE { get; set; }

	//    [DbField("LAST_UPDATED_BY")]
	//    public string LAST_UPDATED_BY { get; set; }

	//    [DbField("LAST_UPDATE_DATE")]
	//    public DateTime LAST_UPDATE_DATE { get; set; }

	//    public override DataCriteria CreateSaveCriteria()
	//    {
	//        throw new NotImplementedException();
	//    }

	//    public override DataCriteria CreateDeleteCriteria()
	//    {
	//        throw new NotImplementedException();
	//    }
	//}


}
