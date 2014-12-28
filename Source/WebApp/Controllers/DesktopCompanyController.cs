using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Core;
using Eagle.Model;
using Eagle.Repository;

namespace Eagle.WebApp.Controllers
{
    public class DesktopCompanyController : BasicController
    {
        //
        // GET: /DesktopCompany/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCompanyInfo(string ContainerPage)
        {
            LS_tblCompanyViewModel entity = LS_tblCompanyRepository.GetCompanyInfo(LanguageId);
            string viewName = string.Empty;
            switch(ContainerPage){
                case "Header":
                    viewName = "../Shared/_HeaderPartial";
                    break;
                case "Footer":
                    viewName = "../Shared/_FooterPartial";
                    break;
                case "Contact":
                    viewName = "../Contact/_CompanyForContactPage";
                    break;
            }
            return PartialView(viewName, entity);
        }

    }
}
