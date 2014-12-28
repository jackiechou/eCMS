using Eagle.Repository.UI.Skins;
using Eagle.WebApp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Eagle.WebApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            // Replace the Default WebFormViewEngine with our custom WebFormThemeViewEngine
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeViewEngine());

            // ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            //  ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());
        }
        void Session_Start(object sender, EventArgs e)
        {
            //Session[SettingKeys.EmpCode] = string.Empty;
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                CultureInfo ci = (CultureInfo)Session["CurrentLanguage"];

                if (ci == null)
                {
                    ci = new CultureInfo("vi-VN");
                    Session["CurrentLanguage"] = ci;
                    Session["LanguageId"] = 124;
                }

                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
            //else
            //Response.RedirectToRoutePermanent("Admin_Login");

        }
    }
}