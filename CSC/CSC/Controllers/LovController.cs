using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSC.Controllers
{
    public class LovController : Controller
    {

        public ActionResult LOVPost()
        {

            string viewName = Request.QueryString["v"];
            if (string.IsNullOrEmpty(viewName))
            {
                throw new Exception("错误的视图名称");
            }

            switch (viewName.ToLower())
            {
                case "test":
                    return View(viewName);
            }
            return null;

        }
        
    }
}
