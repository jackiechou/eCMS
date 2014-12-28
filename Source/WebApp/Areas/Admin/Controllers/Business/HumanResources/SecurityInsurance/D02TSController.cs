using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class D02TSController : BaseController
    {
        public ActionResult Index()
        {
            return View("../Business/HumanResources/SecurityInsurance/D02TS/Index");
        }

    }
}
