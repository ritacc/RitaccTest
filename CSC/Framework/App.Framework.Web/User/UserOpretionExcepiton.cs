using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Web
{
    public class UserOpretionExcepiton:Exception
    {
        public UserOpretionExcepiton(string msg) : base(msg) { }
    }
}
