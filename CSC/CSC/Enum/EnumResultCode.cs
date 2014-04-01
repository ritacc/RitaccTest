using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Framework.Web;

namespace CSC
{
	public enum EnumResultCode
	{
		[Enum("COMPLETE")]
		COMPLETE,
		[Enum("ID/OD")]
		ID_OD,
		[Enum("RE-BOOK")]
		RE_BOOK,
		[Enum("FOLLOWUP")]
		FOLLOWUP,
		[Enum("NOFOLLOWUP")]
		NOFOLLOWUP
	}
}