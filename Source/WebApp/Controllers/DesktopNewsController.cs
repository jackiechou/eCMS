using Eagle.Model;
using Eagle.Model.Common;
using Eagle.Model.Contents;
using Eagle.Model.Contents.News;
using Eagle.Repository.Contents.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Eagle.WebApp.Controllers
{
    public class DesktopNewsController : BasicController
    {
        //
        // GET: /DesktopNews/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetListByCategoryCode(string CategoryCode, int Num)
        {
            List<NewsViewModel> lst = NewsRepository.GetListByFixedNumCode(CategoryCode, Convert.ToInt32(ThreeStatusString.Published), Num, LanguageCode);
            return PartialView("../News/_News",lst);
        }
    }
}
