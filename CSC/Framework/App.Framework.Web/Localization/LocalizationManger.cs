using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Diagnostics;

namespace App.Framework.Web
{
    public class ConverToEnumByString<T> where T : struct
    {
        public static T Convert(string str)
        {
            T result;
            Enum.TryParse<T>(str,out result);
            return result;
        }
    }

    /// <summary>
    /// 本地化支持
    /// </summary>
    public static class LocalizationManger
    {
        private static readonly string LANGUAGE_COOKIENAME = "DCH_Lauguage";
        //private static readonly EnumLanguage DefaultLanguage = EnumLanguage.Zh_CN;
		private static readonly EnumLanguage DefaultLanguage = EnumLanguage.En_US;
        private static EnumLanguage GetCurrentLanguage()
        {
            string langName = DefaultLanguage.ToString();
            HttpRequest request = HttpContext.Current.Request;
            if (request.Cookies != null)
            {
                 
                HttpCookie hc = request.Cookies[LANGUAGE_COOKIENAME];
                if (hc != null)
                {
                    langName = hc.Value;
                }
            }
            return ConverToEnumByString<EnumLanguage>.Convert(langName);
        }
        /// <summary>
        /// 启用多语言
        /// </summary>
        public static void EnableMultiLanguage()
        {
            string currentLanguage = GetCurrentLanguage().ToString().Replace("_","-");
            CultureInfo cultureInfo = new CultureInfo(currentLanguage);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);

			//自定义，日期格式
			if (string.IsNullOrEmpty(currentLanguage))
				currentLanguage = "zh-CN";

			System.Globalization.CultureInfo culinfo = new System.Globalization.CultureInfo(currentLanguage);
			System.Globalization.DateTimeFormatInfo dinfo = (System.Globalization.DateTimeFormatInfo)System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.Clone(); //这个是只读的,所以先复制一份
			dinfo.ShortDatePattern = App.Framework.Web.ConfigHelper.DateTimeFormat;
			culinfo.DateTimeFormat = dinfo;
			System.Threading.Thread.CurrentThread.CurrentCulture = culinfo;
        }

        /// <summary>
        /// 生成切换语言选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="languageText"></param>
        /// <returns></returns>
        public static string LanguageLink(this HtmlHelper helper, string languageText)
        {

            EnumLanguage showLanguage = GetCurrentLanguage() == EnumLanguage.En_US ? EnumLanguage.Zh_CN : EnumLanguage.En_US;
            TagBuilder tb = new TagBuilder("a");
            tb.InnerHtml = languageText;
            tb.MergeAttribute("href",
                new UrlHelper(helper.ViewContext.RequestContext).Content(string.Format("~/Home/ChangeLanguage?language={0}&returnUrl={1}",showLanguage.ToString(),helper.ViewContext.HttpContext.Request.RawUrl ))) ;
            return tb.ToString(TagRenderMode.Normal);
        }

        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="language"></param>
        public static void SetLanguage(EnumLanguage language)
        {
            App.Framework.Web.WebHelper.AddCustomCookie(LANGUAGE_COOKIENAME, language.ToString(), false);
        }
    }
}
