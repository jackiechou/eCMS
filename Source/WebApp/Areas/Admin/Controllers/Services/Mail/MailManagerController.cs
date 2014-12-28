using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.Services.Mail
{
    public class MailManagerController : BaseController
    {
        //
        // GET: /Admin/MailManager/

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView("../Services/Mail/_Reset");
            else
                return View("../Services/Mail/Index");
        }

    }
}
