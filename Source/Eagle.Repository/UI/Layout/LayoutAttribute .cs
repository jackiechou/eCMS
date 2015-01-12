using CommonLibrary.Modules.Dashboard.Components.Skins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Repository.UI.Layout
{
    public class LayoutAttribute: ActionFilterAttribute
    {
        private readonly string _masterName;

        public LayoutAttribute(string masterName)
        {
            _masterName = masterName;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var result = filterContext.Result as ViewResult;
            var request = filterContext.HttpContext.Request;
            if (request.IsAjaxRequest())
                result.MasterName = null;
            else
            {               
                if (result != null)
                    result.MasterName = _masterName;               
            }
           
        }
    }
}
