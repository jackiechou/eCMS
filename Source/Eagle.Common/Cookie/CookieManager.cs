using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Eagle.Common.Cookie
{
    /// <summary>
    /// A generic Cookie class that manages an individual Cookie by localizing the
    /// cookie management into a single class. This means the Cookie's name and
    /// and timing is abstracted.
    /// 
    /// The GetId() method is the key method here which retrieves a Cookie Id.
    /// If the cookie exists it returns the value, otherwise it generates a new
    /// Id and creates the cookie with the specs of the class and
    /// 
    /// It's recommended you store this class as a static member off another
    /// object to have
    /// </summary>
    public class CookieManager : WebClient
    {
        /// <summary>
        /// The name of the Cookie that is used. This value should always be set or 
        /// overridden via the constructor.
        /// <seealso>Class wwCookie</seealso>
        /// </summary>
        public static string CookieName = CookieVariables.Cookie_Name;

        /// <summary>
        /// The timeout of a persistent cookie.
        /// <seealso>Class wwCookie</seealso>
        /// </summary>
        public static int CookieTimeoutInMonths = CookieVariables.Cookie_TimeoutInMonths;

        public CookieManager()
        {
        }

        public CookieManager(string NewCookieName)
        {
            CookieName = NewCookieName;
        }

        /// <summary>
        /// Writes the cookie into the response stream with the value passed. The value
        /// is always the UserId.
        /// <seealso>Class WebStoreCookie</seealso>
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="NonPersistent"></param>
        /// <returns>Void</returns>
        public static void WriteCookie(string Value, bool NonPersistent)
        {
            HttpCookie Cookie = new HttpCookie(CookieName, Value);

            SetCookiePath(Cookie, null);

            if (!NonPersistent)
                Cookie.Expires = DateTime.Now.AddMonths(CookieTimeoutInMonths);

            HttpContext.Current.Response.Cookies.Add(Cookie);
        }

        /// <summary>
        /// Writes the cookie into the response stream with the value passed. The value
        /// is always the UserId.
        /// <seealso>Class WebStoreCookie</seealso>
        /// </summary>
        /// <param name="Value"></param>
        /// <returns>Void</returns>
        public static void WriteCookie(string Value)
        {
            WriteCookie(Value, false);
        }


        /// <summary>
        /// Removes the cookie by clearing it out and expiring it immediately.
        /// <seealso>Class WebStoreCookie</seealso>
        /// </summary>
        /// <param name=""></param>
        /// <returns>Void</returns>
        public static void Remove()
        {
            HttpCookie Cookie = HttpContext.Current.Request.Cookies[CookieName];

            if (Cookie != null)
            {
                SetCookiePath(Cookie, null);
                Cookie.Expires = DateTime.Now.AddHours(-2);
                HttpContext.Current.Response.Cookies.Add(Cookie);
            }
        }

        private void DeleteAllCookie()
        {
            string[] theCookies = System.IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Cookies));
            foreach (string currentFile in theCookies)
            {
                try
                {

                    System.IO.File.Delete(currentFile);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }

        /// <summary>
        /// This is the key method of this class that retrieves the value of the 
        /// cookie. This method is meant as retrieving an ID value. If the value 
        /// doesn't exist it is created and the cookie set and the value returned. If 
        /// the Cookie exists the value is retrieved and returned.
        /// <seealso>Class wwCookie</seealso>
        /// </summary>
        /// <param name=""></param>
        /// <returns>String</returns>
        public static string GetId()
        {
            string UserId;

            // Check to see if we have a cookie we can use
            HttpCookie Cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (Cookie == null)
                UserId = null;
            else
                UserId = (string)Cookie.Value;

            if (UserId == null)
            {
                // Generate a new ID
                UserId = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                WriteCookie(UserId);
            }
            return UserId;
        }

        /// <summary>
        /// Method that generates the ID stored in the cookie. You can override
        /// this method in a subclass to handle custom or specific Id creation.
        /// </summary>
        /// <returns></returns>
        protected virtual string GenerateId()
        {
            return Guid.NewGuid().ToString().GetHashCode().ToString("x");
        }

        /// <summary>
        /// Determines whether the cookie exists
        /// <seealso>Class wwCookie</seealso>
        /// </summary>
        /// <param name=""></param>
        /// <returns>Boolean</returns>
        public static bool CookieExist()
        {
            // Check to see if we have a cookie we can use
            HttpCookie Cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (Cookie == null)
                return false;

            return true;
        }

        /// <summary>
        /// Sets the Cookie Path
        /// </summary>
        /// <param name="Cookie"></param>
        private static void SetCookiePath(HttpCookie Cookie, string Path)
        {
            if (Path == null)
            {
                Path = HttpContext.Current.Request.ApplicationPath;
                if (Path != "/")
                    Cookie.Path = Path + "/";
                else
                    Cookie.Path = "/";
            }
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);

            if (request is HttpWebRequest)
            {
                CookieContainer m_container = new CookieContainer();
                (request as HttpWebRequest).CookieContainer = m_container;
            }
            return request;
        }
    }
}
