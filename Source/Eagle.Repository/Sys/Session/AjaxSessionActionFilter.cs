using Eagle.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Repository.SYS.Session
{
    public class AjaxSessionActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;
            var session = filterContext.HttpContext.Session;
            var url = new UrlHelper(filterContext.HttpContext.Request.RequestContext);
            //session[SettingKeys.DesiredUrl] = request.Url.AbsolutePath;
            session[SettingKeys.DesiredUrl] = request.Url.AbsoluteUri;

            if (session[SettingKeys.UserId] == null)
            {                
                if (request.IsAjaxRequest())
                {
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
                    //401 Unauthorized response
                    //403 Forbidden The request was a valid request, but the server is refusing to respond to it
                    //590 Unexpected time-out
                    response.StatusCode = 590;
                    response.Write("<script language='javascript'>window.location.reload();</script>");
                    //response.Redirect(url.Action("Login", "Admin", new { returnUrl = request.Url.AbsoluteUri}));
                }
                else
                {
                    filterContext.Result = new EmptyResult();
                    response.Redirect(url.Action("Login", "Admin", new { returnUrl = request.Url.AbsoluteUri}));
                }                
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
