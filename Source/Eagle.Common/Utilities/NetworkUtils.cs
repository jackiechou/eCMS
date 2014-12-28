using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;

namespace Eagle.Common.Utilities
{
    public static class NetworkUtils
    {       
        /// <summary>
        /// Retrieves a base domain name from a full domain name.
        /// For example: www.west-wind.com produces west-wind.com
        /// </summary>
        /// <param name="domainName">Dns Domain name as a string</param>
        /// <returns></returns>
        public static string GetBaseDomain(string domainName)
        {
            var tokens = domainName.Split('.');

            // only split 3 urls like www.west-wind.com
            if (tokens == null || tokens.Length != 3)
                return domainName;

            var tok = new List<string>(tokens);
            var remove = tokens.Length - 2;
            tok.RemoveRange(0, remove);

            return tok[0] + "." + tok[1]; ;
        }

        /// <summary>
        /// Returns the base domain from a domain name
        /// Example: http://www.west-wind.com returns west-wind.com
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetBaseDomain(this Uri uri)
        {
            if (uri.HostNameType == UriHostNameType.Dns)
                return GetBaseDomain(uri.DnsSafeHost);

            return uri.Host;
        }

        //string strHostName = Dns.GetHostName();      
        //string scheme = Request.Url.Scheme; // will get http, https, etc.
        //string host = Request.Url.Host; // will get www.mywebsite.com
        //string port = Request.Url.Port; // will get the port
        //string path = Request.Url.AbsolutePath; // should get the /pages/page1.aspx part, can't remember if it only get pages/page1.aspx
        //public static string GetHost()
        //{
        //    return "http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
        //}

        public static string GetIP4Address()
        {
            string IP4Address = String.Empty;

            foreach (System.Net.IPAddress IPA in System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }

        //Get Lan Connected IP address method
        public static string GetLanIPAddress()
        {
            string hostName = Dns.GetHostName(); //Get the Host Name
            IPHostEntry ipHostEntries = Dns.GetHostEntry(hostName); //Get The Ip Host Entry
            IPAddress[] arrIpAddress = ipHostEntries.AddressList;
            //string localIp = ipHostEntry.AddressList[1].ToString();
            string localIp = arrIpAddress[arrIpAddress.Length - 1].ToString();
            return localIp;
        }

        public static string GetRemoteIPAddress()
        {
            string remoteIp = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (String.IsNullOrEmpty(remoteIp))
            {
                remoteIp = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return remoteIp;
        }

        public static string GetIPFromDomainName(string domain_name)
        {
            System.Net.WebClient myWebClient = new System.Net.WebClient();
            System.IO.Stream myStream = myWebClient.OpenRead(domain_name);
            System.IO.StreamReader myStreamReader = new System.IO.StreamReader(myStream);
            string ip = myStreamReader.ReadToEnd();
            return ip;
        }

        public static string GetAddress(AddressType AddressFormat)
        {
            string Host = Dns.GetHostName();  
            string IPAddress = string.Empty;
            System.Net.Sockets.AddressFamily addrFamily = System.Net.Sockets.AddressFamily.InterNetwork;
            switch (AddressFormat)
            {
                case AddressType.IPv4:
                    addrFamily = System.Net.Sockets.AddressFamily.InterNetwork;
                    break;
                case AddressType.IPv6:
                    addrFamily = System.Net.Sockets.AddressFamily.InterNetworkV6;
                    break;
            }
            IPHostEntry IPE = Dns.GetHostEntry(Host);
            if (Host != IPE.HostName)
            {
                IPE = Dns.GetHostEntry(IPE.HostName);
            }
            foreach (IPAddress IPA in IPE.AddressList)
            {
                if (IPA.AddressFamily == addrFamily)
                {
                    return IPA.ToString();
                }
            }
            return string.Empty;
        }    

        public static string GetAddress(string Host, AddressType AddressFormat)
        {
            string IPAddress = string.Empty;
            System.Net.Sockets.AddressFamily addrFamily = System.Net.Sockets.AddressFamily.InterNetwork;
            switch (AddressFormat)
            {
                case AddressType.IPv4:
                    addrFamily = System.Net.Sockets.AddressFamily.InterNetwork;
                    break;
                case AddressType.IPv6:
                    addrFamily = System.Net.Sockets.AddressFamily.InterNetworkV6;
                    break;
            }
            IPHostEntry IPE = Dns.GetHostEntry(Host);
            if (Host != IPE.HostName)
            {
                IPE = Dns.GetHostEntry(IPE.HostName);
            }
            foreach (IPAddress IPA in IPE.AddressList)
            {
                if (IPA.AddressFamily == addrFamily)
                {
                    return IPA.ToString();
                }
            }
            return string.Empty;
        }
    }
    public enum AddressType
    {
        IPv4,
        IPv6
    }
}
