using System.Web.Mvc;
using App.Framework.Web;

namespace CSC.Controllers
{
    public class HomeController : Controller
    {
        public  ActionResult BlankJson()
        {
            return new { }.ToJsonEntity(true);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangeLanguage(string returnUrl, EnumLanguage language)
        {
            LocalizationManger.SetLanguage(language);
            return Redirect(returnUrl);
        }
    }
}
