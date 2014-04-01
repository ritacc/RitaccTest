using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Framework.Data;

namespace CSC
{
    public static class DataCriteriaExtension
    {
        public static string ToValueString(this DataCriteria criteria)
        {
            string result = string.Empty;
            foreach (var item in criteria.GetType().GetProperties())
            {
                result += "_" + item.GetValue(criteria, null);
            }

            return result;
        }

    }
}