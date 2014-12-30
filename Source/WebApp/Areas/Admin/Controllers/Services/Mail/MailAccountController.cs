using Eagle.Repository.SYS.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.Services.Mail
{
    public class MailAccountController : BaseController
    {
        //
        // GET: /Admin/MailAccount/
        [SessionExpiration]
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView("../Services/Mail/MailAccount/_Reset");
            else
                return View("../Services/Mail/MailAccount/Index");
        }

    }
}
