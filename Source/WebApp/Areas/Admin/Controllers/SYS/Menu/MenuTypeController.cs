using Eagle.Model.SYS.Menu;
using Eagle.Repository.Sys.Menus;
using Eagle.Repository.SYS.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.SYS.Menu
{
    public class MenuTypeController : BaseController
    {
        //
        // GET: /Admin/MenuType/

        public ActionResult Index()
        {
            return View();
        }

        [SessionExpiration]
        public ActionResult Create()
        {
            MenuTypeViewModel model = new MenuTypeViewModel();
            return PartialView("../SYS/MenuTypes/_Create", model);
        }

        [SessionExpiration]
        public List<MenuTypeViewModel> PopulateMenuTypeList()
        {
            return MenuTypeRepository.GetActiveList(ScopeTypeId, LanguageCode);
        }



    }
}
