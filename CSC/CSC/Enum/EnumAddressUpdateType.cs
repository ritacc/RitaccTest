using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Framework.Web;

namespace CSC
{
	public enum EnumAddressUpdateType
	{
		[Enum("InvoiceOnly")]
		InvoiceOnly,
		[Enum("ProductAndInvoice")]
		ProductAndInvoice
	}
}