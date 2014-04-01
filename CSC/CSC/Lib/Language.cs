using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Framework.Web;
using CSC.Resources;

namespace CSC.Lib
{
    public static class Language
    {
        public static string GenerateLaugnageLink(this HtmlHelper helper)
        {
            return LocalizationManger.LanguageLink(helper, GlobalText.ChangeLanguage);
        }

    }
}