using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSC
{
    public class DbException:Exception
    {
        public DbException(string msg) : base(msg) { }
    }
}