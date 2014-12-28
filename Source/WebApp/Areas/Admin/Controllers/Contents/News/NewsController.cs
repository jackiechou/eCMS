using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.Contents
{
    public class NewsController : BaseController
    {
        //
        // GET: /Admin/News/

        public ActionResult Index()
        {               
            ViewBag.Status = CommonRepository.GenerateThreeStatusModeList(Eagle.Resource.LanguageResource.All, true);
            return View();
        }

    }
}
