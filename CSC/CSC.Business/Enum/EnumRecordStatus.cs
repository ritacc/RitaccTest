using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework.Web;

namespace CSC.Business
{
    public enum EnumRecordStatus
    {
		[Enum("Add")]
		ADD,
        [Enum("Edit")]
        EDIT
    }
}
