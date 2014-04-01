using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Security
{
    public class SecurityExceptionToUser:Exception
    {
        public SecurityExceptionToUser(string msg) : base(msg) { }
    }
}
