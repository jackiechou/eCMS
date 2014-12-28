using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public abstract class ThemeControllerBase : Controller
{
    protected override void Execute(System.Web.Routing.RequestContext requestContext)
    {
        var themeName = ConfigurationManager.AppSettings["DefaultThemeName"];
        var themeSrc = ConfigurationManager.AppSettings["DefaultThemeSource"];

        if (requestContext.HttpContext.Items[themeName] == null)
        {
            //first time load
            requestContext.HttpContext.Items[themeName] = requestContext.HttpContext.Request.Cookies.Get("theme").Value;
        }
        else
        {
            requestContext.HttpContext.Items[themeName] = themeSrc;

            var previewTheme = requestContext.RouteData.GetRequiredString("theme");

            if (!string.IsNullOrEmpty(previewTheme))
            {
                requestContext.HttpContext.Items[themeName] = previewTheme;
            }
        }

        base.Execute(requestContext);
    }
}