﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Eagle.Common.Common.Utilities
{
    /// <summary>
    /// A generic Web Cookie handler class that can be used for a single 'UserId' in an
    /// application. This class manages all aspects of retrieving and setting of a cookie
    /// consistently. Typically all you'll need to do is call the GetId() method which 
    /// both returns existing cookies and ensures that the cookie gets set.
    /// 
    /// All methods of this class are static which is the reason why only a single Cookie
    /// can be managed at a time. The idea is that you can use this single cookie as an
    /// application global Cookie to track a user and then retrieve additional storage 
    /// information from other locations (like a database or session).
    /// </summary>
    public class CookieUtils
    {
        public static string CookieName = "COOKIE_NAME";
        public static int CookieTimeoutInMonths = 24;
        public CookieUtils(string NewCookieName)
        {
            CookieName = NewCookieName;
        }

        /// <summary>
        /// Writes the cookie into the response stream with the value passed. The value
        ///  is always the UserId.
        /// <seealso>Class WebStoreCookie</seealso>
        /// </summary>
        /// <param name="String Value">
        /// Writes a value into the specified cookie.
        /// </param>
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
        ///  is always the UserId.
        /// <seealso>Class WebStoreCookie</seealso>
        /// </summary>
        /// <param name="String Value">
        /// Writes a value into the specified cookie.
        /// </param>
        /// <returns>Void</returns>
        public static void WriteCookie(string Value)
        {
            WriteCookie(Value, false);
        }


        /// <summary>
        /// Removes the cookie by clearing it out and expiring it immediately.
        /// <seealso>Class WebStoreCookie</seealso>
        /// </summary>
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

        /// <summary>
        /// Retrieves the user's Cookie. If the Cookie doesn't exist a new one is generated
        /// by hashing a new GUID value and writing the Cookie into the Response.
        /// <returns>Customers UserId</returns>
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
        /// </summary>
        /// <returns></returns>
        public static bool CookieExist()
        {
            // Check to see if we have a cookie we can use
            HttpCookie loCookie = HttpContext.Current.Request.Cookies[CookieName];
            if (loCookie == null)
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

    }

    
}
