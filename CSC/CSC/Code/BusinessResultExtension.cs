using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Framework.Data;

namespace CSC
{
    public static class BusinessResultExtension
    {
        public static string GetMessage(this App.Framework.BusinessResult result)
        {
            string msg = CSC.Resources.BusinessResultMessage.ResourceManager.GetString("MSG_" + Math.Abs(result.ResultType));
            if (!string.IsNullOrEmpty(msg))
                return msg;
            return result.ResultMessage;
        }

        public static string GetMessage(this DataCriteria result)
        {
            string msg = CSC.Resources.BusinessResultMessage.ResourceManager.GetString("MSG_" + Math.Abs(result.ResultType));
            if (!string.IsNullOrEmpty(msg))
                return msg;
            return result.ResultMessage;
        }
    }
}