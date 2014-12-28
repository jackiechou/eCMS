using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Repository.Extensions
{
    //   @Html.Controller();
    //@Html.Action();
    //@Html.Id();
    public static class HtmlRequestHelper
    {
        public static string Id(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("id"))
                return (string)routeValues["id"];
            else if (HttpContext.Current.Request.QueryString.AllKeys.Contains("id"))
                return HttpContext.Current.Request.QueryString["id"];

            return string.Empty;
        }

        public static string Controller(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }

        public static string Action(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
                return (string)routeValues["action"];

            return string.Empty;
        }

        //<link href=<%= Html.GetThemedStyleSheet() %> rel="stylesheet" type="text/css" />
        public static string GetThemedStyleSheet(this HtmlHelper html)
        {
            HttpContext context = HttpContext.Current;
            if (context == null)
            {
                throw new InvalidOperationException("Http Context cannot be null.");
            }

            string defaultStyleSheet = context.Server.MapPath("~/Themes/Default/Content/css/style.css");

            string domain = string.Format("{0}://{1}{2}{3}", context.Request.Url.Scheme,context.Request.Url.Host,context.Request.Url.Port == 80 ? string.Empty : ":" + context.Request.Url.Port, context.Request.ApplicationPath);
            string cacheKey = String.Format(CultureInfo.InvariantCulture, "theme_for_{0}", domain);
            string theme = (string)context.Cache[cacheKey];
            if (String.IsNullOrEmpty(theme) || theme == "Default")
            {
                return defaultStyleSheet;
            }

            string styleSheet = context.Server.MapPath(String.Format(CultureInfo.InvariantCulture,
                "~/Themes/{0}/Content/Site.css", theme));
            if (!File.Exists(styleSheet))
            {
                styleSheet = defaultStyleSheet;
            }
            return String.Format(CultureInfo.InvariantCulture, "'{0}'", styleSheet);
        }
    }
}
