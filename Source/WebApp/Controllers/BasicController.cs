using Eagle.Core;
using Eagle.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Controllers
{
    public class BasicController : Controller
    {
        // entity
        protected EntityDataContext db = new EntityDataContext();

        // override dispose 
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        // LanguageId => nó nấy session trả về mã ngôn ngữ hiệu tại
        protected string domainUrl { get { return ConfigurationManager.AppSettings["DOMAIN_URL"].ToString(); } }
        public BasicController()
        {
            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            System.Globalization.DateTimeFormatInfo dateformat = new System.Globalization.DateTimeFormatInfo();
            dateformat.ShortDatePattern = "MM/dd/yyyy";
            culture.DateTimeFormat = dateformat;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
        }
        protected const int defaultPageSize = 10;
        private int defaultLanguageId = 10001;
        private string defaultLanguageCode = "en-US";
        protected int LanguageId
        {
            get
            {
                if (base.Session["LanguageId"] == null)
                {
                    base.Session["LanguageId"] = defaultLanguageId;
                    return defaultLanguageId;
                }
                else
                {
                    defaultLanguageId = Convert.ToInt32(base.Session["LanguageId"]);
                    return defaultLanguageId;
                }
            }
        }

        protected string LanguageCode
        {
            get
            {
                if (base.Session["LanguageCode"] == null)
                {
                    Session["LanguageCode"] = defaultLanguageCode;
                    return defaultLanguageCode;
                }
                else
                {
                    defaultLanguageCode = base.Session["LanguageCode"].ToString();
                    return defaultLanguageCode;
                }
            }
        }
                        
        public void ShowMessageBox(string MessageType, string Message)
        {
            string CssClass = string.Empty;
            if (MessageType == "warning")
                CssClass = "alert alert-warning";
            else if (MessageType == "error")
                CssClass = "alert alert-error";
            else if (MessageType == "success")
                CssClass = "alert alert-success";
            else
                CssClass = "alert alert-warning";

            ViewBag.DisplayErrorMessage = true;
            ViewBag.PopupTitle = MessageType.ToUpper();
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = CssClass;
            ViewBag.Message = Message;
        }

        public SelectList LoadMonthList(string SelectedValue)
        {
            SelectList month_lst = new SelectList(new[] { new { Text = Eagle.Resource.LanguageResource.Select, Value = (string)SelectedValue } }.Concat(
                 Enumerable.Range(1, 12).Select(r => new { 
                     Text = ((LanguageId == 124) ? DateTimeFormatInfo.CurrentInfo.GetMonthName(r):  string.Format("{0} {1}", Eagle.Resource.LanguageResource.Month, r)),
                     Value = r.ToString()
                 })), "Value", "Text", SelectedValue);
           return month_lst;
        }


        protected override void Execute(System.Web.Routing.RequestContext requestContext)
        {
            //var themeName =
            var themeName = ConfigurationManager.AppSettings["DefaultThemeName"];
            var themeSrc = ConfigurationManager.AppSettings["DefaultThemeSource"];

            // Add code here to set the Theme based on your database or some other storage
            requestContext.HttpContext.Items["themeName"] = themeName;

            // Allow the Theme to be overriden via the querystring
            // If a Theme Name is Passed in the querystring then use it and override the previously set Theme Name
            // http://localhost/Default.aspx?theme=Red
            var previewTheme = requestContext.HttpContext.Request.QueryString["theme"];
            if (!string.IsNullOrEmpty(previewTheme))
            {
                requestContext.HttpContext.Items["themeName"] = previewTheme;
            }

            base.Execute(requestContext);
        }
    }
    
}
