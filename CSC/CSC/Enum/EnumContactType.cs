using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Framework.Web;

namespace CSC
{
	public enum EnumContactType
	{
		[Enum("Email")]
		Email,
		[Enum("Letter")]
		Letter
	}
}