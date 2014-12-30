using Eagle.Repository.SYS.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.UI.Skins
{
    public class SkinPackageController : BaseController
    {
        //
        // GET: /Admin/SkinPackage/
        [SessionExpiration]
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView("../UI/Skins/SkinPackages/_Edit");
            else
                return View("../UI/Skins/SkinPackages/Index");       
        }

    }
}
