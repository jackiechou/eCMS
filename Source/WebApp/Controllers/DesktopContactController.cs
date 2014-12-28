using Eagle.Model;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Controllers
{
    public class DesktopContactController : BasicController
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View("../Contact/Index");
        }

        public ActionResult GetContactInfo()
        {
            LS_tblCompanyViewModel entity = LS_tblCompanyRepository.GetCompanyInfo(LanguageId);
            return PartialView("../Contact/_ContactInfo", entity);
        }

        //public ActionResult GetListByCategoryCode(string CategoryCode, int Num)
        //{
        //    List<NewsViewModel> lst = NewsRepository.GetListByFixedNumCode(CategoryCode, StatusKey.Published, Num, LanguageId);
        //    return PartialView("../Contents/News/_NewsPartial", lst);
        //}

    }
}
