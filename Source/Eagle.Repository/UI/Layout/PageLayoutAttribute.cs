using CommonLibrary.Modules.Dashboard.Components.Skins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Repository.UI.Layout
{
    public class PageLayoutAttribute: ActionFilterAttribute
    {
        private string _masterName = LayoutType.MainLayout;
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var result = filterContext.Result as ViewResult;
            var request = filterContext.HttpContext.Request;
            var route = filterContext.RouteData;
            if (request.IsAjaxRequest())
                result.MasterName = null;
            else
            {
                if (result != null)
                {
                    if (route.Values["pageid"] != null)
                    {
                        int pageId = Convert.ToInt32(route.Values["pageid"].ToString());
                        _masterName = SkinTemplateRepository.GetTemplateSrcByPageId(pageId);
                    }
                    result.MasterName = _masterName;      
                }         
            }
           
        }
    }
}
