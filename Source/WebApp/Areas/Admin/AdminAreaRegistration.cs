using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Eagle.WebApp.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        string area = "Admin";
        //string defaultController = "Admin";
        //string defaultAction = "login";
        //string defaultId;
        //IRouteConstraint routeConstraints;
        //string[] nameSpaces = { "Eagle.WebApp.Areas.Admin.Controllers" };

        public override string AreaName
        {
            get
            {
                return this.area;
            }
        }     

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute("Default_Login",
            //       "login",
            //         new { controller = "User", action = "Login", desiredUrl = UrlParameter.Optional },
            //        new[] { "Eagle.WebApp.Areas.Admin.Controllers" });

            //context.MapRoute(
            //        this.area + "_Login",
            //        this.area + "/login/{desiredUrl}",
            //        new { controller = "User", action = "Login", desiredUrl = UrlParameter.Optional },
            //       new[] { "Eagle.WebApp.Areas.Admin.Controllers.SYS.Users" });

            //context.MapRoute(this.area + "_PermissionDenied", this.area + "/Common/PermissionDenied", new { controller = "Common", action = "PermissionDenied" }, new[] { "Eagle.WebApp.Areas.Admin.Controllers" });
            //context.MapRoute(this.area + "_PageNotFound", this.area + "/Common/PageNotFound", new { controller = "Common", action = "PageNotFound" }, new[] { "Eagle.WebApp.Areas.Admin.Controllers" });


            context.MapRoute(
                     this.area + "_Login",
                     this.area + "/login/{desiredUrl}",
                     new { controller = "User", action = "Login", desiredUrl = UrlParameter.Optional }
                     );

            context.MapRoute(
              this.area + "_Home",
              this.area + "/{controller}/{action}/{id}",
              new { area = this.area, controller = "Home", action = "Index", id = UrlParameter.Optional }
          );

            context.MapRoute(
               this.area + "_Default",
               this.area + "/{controller}/{action}/{menuid}/{pageid}",
               new { area = this.area, controller = "Home", action = "Index", menuid = UrlParameter.Optional, pageid = UrlParameter.Optional }
           );

            context.MapRoute(
                this.area + "_PermissionDenied", 
                this.area + "/PermissionDenied", 
                new { controller = "Common", action = "PermissionDenied" });
            context.MapRoute(
                this.area + "_PageNotFound", 
                this.area + "/PageNotFound", 
                new { controller = "Common", action = "PageNotFound" });


          //  context.MapRoute(
          //    this.area + "_HR",
          //    this.area + "/HR/{controller}/{action}/{id}",
          //    new { area = "Admin/HR", controller = "Home", action = "Index", id = UrlParameter.Optional }              
          //);
        }
    }
}
