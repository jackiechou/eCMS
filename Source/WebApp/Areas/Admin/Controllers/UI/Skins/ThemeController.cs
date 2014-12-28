using CommonLibrary.UI.Skins;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.UI.Skins
{
    public class ThemeController : BaseController
    {
        //
        // GET: /Admin/Theme/

        [HttpPost]
        public ActionResult Index(string SelectedTheme)
        {
            var ThemeName = ConfigurationManager.AppSettings["ThemeName"];

            ControllerContext.RequestContext.HttpContext.Items[ThemeName] = SelectedTheme;
            var themeCookie = new HttpCookie("theme", SelectedTheme);
            HttpContext.Response.Cookies.Add(themeCookie);

            //const string controller = "Home";
            //const string action = "Index";

            //ViewContext.RequestContext.HttpContext.Items[ThemeName].ToString()
            ViewBag.Theme = SkinRepository.PopulateActiveSkinSelectList(ApplicationId, SelectedTheme, false);
            //return Redirect(string.Format("/{0}/{1}/{2}", controller, action, SelectedTheme));
            return View("../UI/Skins/Themes/Index");
        }
    }
}
