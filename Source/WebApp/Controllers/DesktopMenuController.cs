using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Controllers
{
    public class DesktopMenuController : Controller
    {
        //
        // GET: /DesktopMenu/

        public ActionResult Index()
        {
            return View();
        }
        //[ChildActionOnly]
        //public ActionResult Menu()
        //{
        //    List<PageViewModel> allPage = (List<PageViewModel>)Session[SettingKeys.Pages];
        //    //allPage = allPage.Where(o => PageIDs.Contains(o.PageId)).ToList();
        //    ViewBag.allPage = allPage;

        //    // Tim nhung node cha truoc
        //    foreach (var item in allPage.Where(p => p.ParentId == null))
        //    {
        //        strResult += "<li class=''>";
        //        strResult += "<a href='" + item.PageUrl.ToLower() + "' class='sf-with-ul'><span class='pagename'>" + item.PageName + "</span></a>";
        //        // Truong hop ton tai con
        //        if (item.tree_isLeaf == "false")
        //        {
        //            strResult += LoopPage(item.PageId, allPage);
        //        }

        //        strResult += "</li>";
        //    }
       // }
    }
}
