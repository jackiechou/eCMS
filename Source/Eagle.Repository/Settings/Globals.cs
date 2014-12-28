using Eagle.Common.Entities;
using Eagle.Common.Settings;
using Eagle.Model.SYS.Application;
using Eagle.Repository.SYS.Application;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Eagle.Repository.Settings
{
    public static class Globals
    {
        //=========================================27/05/2013=========================================
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        public static int ConnectionTimeout = 216000;
        public static int CommandTimeout = 900000;
        public static int SessionTimeout = 216000;

        private static string _ApplicationPath = HttpContext.Current.Request.ApplicationPath;
        private static string _ApplicationMapPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 1).Replace("/", "\\");
        //string[] validFileTypes = { ".bmp", ".gif", ".png", ".jpg", ".jpeg", ".doc", ".xls" };
        private static string[] _ValidImageTypes = { ".bmp", ".gif", ".png", ".jpg", ".jpeg", ".tiff", ".swf" };
        private static string[] _ValidVideoTypes = { ".flv", ".mpg", ".avi", ".3gp", ".mov", ".mp4" };

        //private static string _HostPath = Globals.ApplicationPath + "/portals/_default/";
        private static string _HostPath = Globals.ApplicationPath;
        private static string _HostMapPath = HttpContext.Current.Server.MapPath(Globals.HostPath);

        public static int _ApplicationId = Convert.ToInt32(HttpContext.Current.Session["ApplicationId"].ToString());
        //public static string _RoleId = HttpContext.Current.Session["RoleId"].ToString();        
        //public static string _IsSuperUser = HttpContext.Current.Session["IsSuperUser"].ToString();
        ////public static string _UpdatePassword = HttpContext.Current.Session["UpdatePassword"].ToString();
        ////public static string _IsDeleted = HttpContext.Current.Session["IsDeleted"].ToString();        //
        ////public static string _HomeDirectory = HttpContext.Current.Session["HomeDirectory"].ToString();

        public static string[] ValidImageTypes
        {
            get { return _ValidImageTypes; }
            set { _ValidImageTypes = value; }
        }

        public static string[] ValidVideoTypes
        {
            get { return _ValidVideoTypes; }
            set { _ValidVideoTypes = value; }
        }

        public static string ApplicationPath
        {
            get { return _ApplicationPath; }
            set { _ApplicationPath = value; }
        }
        public static string ApplicationMapPath
        {
            get { return _ApplicationMapPath; }
            set { _ApplicationMapPath = value; }
        }

        public static string HostMapPath
        {
            get { return _HostMapPath; }
            set { _HostMapPath = value; }
        }
        public static string HostPath
        {
            get { return _HostPath; }
            set { _HostPath = value; }
        }
     

        //public static string RoleId
        //{
        //    get { return _RoleId; }
        //    set { _RoleId = value; }
        //}

        public static int ApplicationId
        {
            get { return _ApplicationId; }
            set { _ApplicationId = value; }
        }

        //public static string IsSuperUser
        //{
        //    get { return _IsSuperUser; }
        //    set { _IsSuperUser = value; }
        //}

     

        //public static string HomeDirectory
        //{
        //    get { return PortalInfo._HomeDirectory; }
        //    set { PortalInfo._HomeDirectory = value; }
        //}

        public static string DefaultLanguage
        {
            get { return ApplicationEntity.DefaultLanguage; }
            set { ApplicationEntity.DefaultLanguage = value; }
        }

        /// <summary>
        /// Return The Name of the Logged in User by ApplicationId
        /// </summary>
        /// <param name="ApplicationId">ApplicationId</param>
        /// <returns>Logged In UserName</returns>

        public static string GetUser(int ApplicationId)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName + "_" + GetApplicationName + "_" + ApplicationId];
            string user = string.Empty;
            if (authCookie != null && authCookie.Value != string.Empty)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if (ticket != null)
                {
                    user = ticket.Name;
                }
                else
                {
                    user = "anonymoususer";
                }
            }
            else
            {
                user = "anonymoususer";
            }
            return user;
        }

        public static FormsAuthenticationTicket GetUserTicket(int ApplicationId)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName + "_" + ApplicationId];
            if (authCookie != null && authCookie.Value != string.Empty)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                return ticket;
            }
            else
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "anonymoususer", DateTime.Now,
                                                                            DateTime.Now.AddMinutes(30),
                                                                              true,
                                                                              ApplicationId.ToString(),
                                                                              FormsAuthentication.FormsCookiePath);
                return ticket;
            }
        }

        public static string CloakText(string PersonalInfo)
        {
            if (PersonalInfo != null)
            {
                StringBuilder sb = new StringBuilder();
                char[] chars = PersonalInfo.ToCharArray();
                foreach (char chr in chars)
                {
                    sb.Append(((int)chr).ToString());
                }
                if (sb.Length > 0)
                    sb.Remove(sb.Length - 1, 1);
                StringBuilder sbScript = new StringBuilder();
                sbScript.Append(Environment.NewLine + "<script type=\"text/javascript\">" + Environment.NewLine);
                sbScript.Append("//<![CDATA[" + Environment.NewLine);
                sbScript.Append("   document.write(String.fromCharCode(" + sb.ToString() + "))" + Environment.NewLine);
                sbScript.Append("//]]>" + Environment.NewLine);
                sbScript.Append("</script>" + Environment.NewLine);
                return sbScript.ToString();
            }
            else
            {
                return null;
            }
        }

        public static string GetApplicationName
        {
            get
            {
                return (HttpContext.Current.Request.ApplicationPath == "/" ? "" : HttpContext.Current.Request.ApplicationPath);
            }
        }

        /// <summary>
        /// To Check Whether Eagle Is Installed Or Not
        /// </summary>
        /// <returns>True If It Is Install</returns>
        public static bool IsInstalled()
        {
            bool isInstalled = false;
            string IsInstalled = Config.GetSetting("IsInstalled").ToString();
            string InstallationDate = Config.GetSetting("InstallationDate").ToString();
            if ((IsInstalled != "" && IsInstalled != "false") && InstallationDate != "")
            {
                isInstalled = true;
            }
            return isInstalled;
        }
    }
}
