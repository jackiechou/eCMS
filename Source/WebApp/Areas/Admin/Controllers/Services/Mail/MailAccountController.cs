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

        public ActionResult Index()
        {
            return View();
        }

    }
}
