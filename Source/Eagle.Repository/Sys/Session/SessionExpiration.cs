using Eagle.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Repository.SYS.Session
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SessionExpiration : ActionFilterAttribute, IActionFilter
    {
        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    throw new NotImplementedException();
        //}
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;
            var server = filterContext.HttpContext.Server;
            var session = filterContext.HttpContext.Session;
            var url = new UrlHelper(request.RequestContext);
            //session[SettingKeys.DesiredUrl] = request.Url.AbsolutePath.ToLower();
            session[SettingKeys.DesiredUrl] = request.Url.AbsoluteUri.ToLower();

            if (session[SettingKeys.UserId] != null)
            {
                // check if a new session id was generated
                if (session.IsNewSession)
                {
                    // If it says it is a new session, but an existing cookie exists, then it must have timed out
                    string sessionCookie = request.Headers["userInfo"];
                    if ((null != sessionCookie) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        response.Redirect("/Admin/Login?desiredUrl=" + request.Url.AbsoluteUri);
                    }
                }              
            }
            else
            {
                if (request.IsAjaxRequest())
                {                   
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
                    response.Redirect("/Admin/Login?desiredUrl=" + request.Url.AbsoluteUri);    
                    //response.Write("<script language='javascript'>window.location.reload();</script>");
                    //response.Write("<script language='javascript'>window.location.href=/Admin/Login?desiredUrl = "+ request.Url.AbsoluteUri+";</script>");             
                }
                else
                {
                    filterContext.Result = new EmptyResult();
                    if (!response.IsRequestBeingRedirected)
                    {
                        response.Buffer = true;
                        response.Flush();
                        HttpContext.Current.RewritePath("/Admin/Login?desiredUrl=" + request.Url.AbsoluteUri);
                    }                                 
                }
            }
            base.OnActionExecuting(filterContext);
        }
        //public virtual ActionResult OnSessionExpiryRedirectResult(ActionExecutingContext filterContext)
        //{
        //    AdminController admin_controller = new AdminController();
        //    var actionResult = (RedirectToRouteResult)admin_controller.Index();
        //    return actionResult;
        //}
    }
}
