using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSC.Business;

namespace CSC.Controllers
{
    public class AuditController : Controller
    {
		public ActionResult Index()
        {
			var model = new Audit()
			{
				CreatedBy = Request.QueryString["CreatedBy"],
				CreationDate = Convert.ToDateTime(Request.QueryString["CreationDate"]),
				LastUpdatedBy =  Request.QueryString["LastUpdatedBy"],
				LastUpdateDate = Convert.ToDateTime(Request.QueryString["LastUpdateDate"])
			};
			return View(model);
        }
    }
}
