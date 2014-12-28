using Eagle.Model;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Controllers
{
    public class DesktopLayoutController : BasicController
    {
        //
        // GET: /DesktopLayout/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadHeader()
        {
            LS_tblCompanyViewModel entity = LS_tblCompanyRepository.GetCompanyInfo(LanguageId);
            return PartialView("../Shared/_HeaderPartial", entity);
        }

        public ActionResult LoadTopMenu()
        {
            return PartialView("../Shared/_TopMenuPartial", new List<SYS_tblFunctionPermissionViewModel>());
        }

        public ActionResult LoadFooter()
        {
            LS_tblCompanyViewModel entity = LS_tblCompanyRepository.GetCompanyInfo(LanguageId);
            return PartialView("../Shared/_FooterPartial", entity);
        }
    }
}
