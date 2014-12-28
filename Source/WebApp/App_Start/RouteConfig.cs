using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Eagle.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
#if DEBUG
            routes.IgnoreRoute("{*browserlink}", new { browserlink = @".*/arterySignalR/ping" });
#endif

            //// elFinder's connector route       
            routes.MapRoute(null, "connector", new { controller = "Files", action = "Index", folder = UrlParameter.Optional, subFolder = UrlParameter.Optional });
            routes.MapRoute(null, "Thumbnails/{tmb}", new { controller = "Files", action = "Thumbs", tmb = UrlParameter.Optional });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { "Eagle.WebApp.Controllers" });
            //routes.MapRoute("Admin_Login", "Admin/User/Login/{desiredUrl}", new { controller = "User", action = "Login", desiredUrl = UrlParameter.Optional }, new[] { "Eagle.WebApp.Areas.Admin.Controllers.SYS.Users" });
           //// routes.MapRoute("Admin_Default", "Admin/{controller}/{action}/{FunctionID}/{ModuleID}", new { controller = "User", action = "Index", FunctionID = UrlParameter.Optional, ModuleID = UrlParameter.Optional }, new[] { "Eagle.WebApp.Areas.Admin.Controllers" });
           // routes.MapRoute("Admin_Default", "Admin/{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { "Eagle.WebApp.Areas.Admin.Controllers" });
            //routes.MapRoute("Admin_PermissionDenied", "Admin/Common/PermissionDenied", new { controller = "Common", action = "PermissionDenied" }, new[] { "Eagle.WebApp.Areas.Admin.Controllers" });
            //routes.MapRoute("Admin_PageNotFound", "Admin/Common/PageNotFound", new { controller = "Common", action = "PageNotFound" }, new[] { "Eagle.WebApp.Areas.Admin.Controllers" });


            //routes.MapRoute("Host_Default", "Host/{controller}/{action}/{id}", new { controller = "Admin", action = "Index", id = UrlParameter.Optional });

            //routes.MapRoute("Site_Default", "Site/{controller}/{action}/{id}", new { controller = "Site", action = "Index", id = UrlParameter.Optional }, new[] { "Eagle.WebApp.Areas.Admin.Controllers" });
        }
    }
}