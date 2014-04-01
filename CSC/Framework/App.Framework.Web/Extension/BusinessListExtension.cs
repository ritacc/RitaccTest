using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace App.Framework.Web
{
    public static class BusinessListExtension
    {
        public static List<SelectListItem> ToSelectList<T>(this BusinessList<T> list, Func<T, SelectListItem> call) where T : App.Framework.Data.DataEntity
        {
            return list.ToList().ToList<T, SelectListItem>(call);
        }

        public static List<SelectListItem> ToSelectList<T>(this BusinessList<T> list, Func<T, object> getText, Func<T, object> getValue) where T : App.Framework.Data.DataEntity
        {
            return list.ToSelectList(getText, getValue, null);
        }

        public static List<SelectListItem> ToSelectList<T>(this BusinessList<T> list, Func<T, object> getText, Func<T, object> getValue, Func<T, bool> getSelect) where T : App.Framework.Data.DataEntity
        {
            return list.ToList().ToSelectList(getText, getValue, getSelect);
        }
    }
}
