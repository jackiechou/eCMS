using Eagle.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Repository.SYS.Session
{
    public class AjaxAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;
            var session = filterContext.HttpContext.Session;

            if (session[SettingKeys.UserId] == null)
            {
                if (request.IsAjaxRequest())
                {
                    //401 Unauthorized response
                    //403 Forbidden The request was a valid request, but the server is refusing to respond to it
                    //590 Unexpected time-out
                    response.StatusCode = 401;
                    //filterContext.Result = new EmptyResult();
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            // put whatever data you want which will be sent to the client
                            message = "timeout"
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    //base.HandleUnauthorizedRequest(filterContext);
                    //filterContext.Result = new RedirectResult("~/Admin/GoToLogin");
                    //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Login" } });
                    SessionManager.DesiredUrl = request.Url.AbsolutePath.ToLower();
                    var url = new UrlHelper(request.RequestContext);
                    response.Redirect(url.Action("Login", "Admin", new { returnUrl = request.Url.AbsoluteUri }));
                }
            }
            base.HandleUnauthorizedRequest(filterContext);
        }
    }

    //How to use with js
    //$.get('@Url.Action("SomeAction")', function (result) {
    //    if (result.message) {
    //        alert(result.message);
    //    } else {
    //        // do whatever you were doing before with the results
    //    }
    //});
}
