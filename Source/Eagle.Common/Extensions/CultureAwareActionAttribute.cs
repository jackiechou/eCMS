using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eagle.Common.Extensions
{
    public class CultureAwareActionAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            // That's for displaying in the UI, the model binder doesn't use it
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");

            // That's the important one for the model binder
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
        }
    }
}
