using Eagle.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Repository.SYS.Session
{
    public class AjaxEmpAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;
            var session = filterContext.HttpContext.Session;

            if (session[SettingKeys.EmpId] == null)
            {
                if (request.IsAjaxRequest())
                {
                    response.StatusCode = 401; //custom status code, might as well be 401, dont know if that would violate any proncipal
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
                    SessionManager.DesiredUrl = request.Url.AbsolutePath.ToLower();
                    var url = new UrlHelper(request.RequestContext);
                    response.Redirect(url.Action("Login", "Admin", new { returnUrl = request.Url.AbsoluteUri }));
                }
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
