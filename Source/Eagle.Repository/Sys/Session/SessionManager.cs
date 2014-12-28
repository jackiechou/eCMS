using Eagle.Common.Settings;
using Eagle.Model.HR.Personnel.Employee;
using Eagle.Model.SYS.Application;
using Eagle.Model.SYS.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Eagle.Repository.SYS.Session
{
   

    public static class SessionManager
    {

        public static String DomainUrl
        {
            get
            {
               return HttpContext.Current.Session[SettingKeys.DomainUrl].ToString();
            }
            set
            {
                HttpContext.Current.Session[SettingKeys.DomainUrl] = value;
            }
        }

        public static String DesiredUrl
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session[SettingKeys.DesiredUrl] != null)
                    {
                        return HttpContext.Current.Session[SettingKeys.DesiredUrl].ToString();
                    }
                }

                return String.Empty;
            }
            set
            {
                HttpContext.Current.Session[SettingKeys.DesiredUrl] = value;
            }
        }

        public static int UserId
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session[SettingKeys.UserId]);
            }
            set
            {
                HttpContext.Current.Session[SettingKeys.UserId] = value;
            }
        }

        public static string UserName
        {
            get
            {
                return HttpContext.Current.Session[SettingKeys.UserName].ToString();
            }
            set
            {
                HttpContext.Current.Session[SettingKeys.UserName] = value;
            }
        }

        public static UserInfo User
        {
            get
            {
                return ((UserInfo)HttpContext.Current.Session[SettingKeys.AccountInfo]);
            }
            set
            {
                HttpContext.Current.Session[SettingKeys.AccountInfo] = value;
            }
        }
        
        public static int? EmpId
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session[SettingKeys.EmpId]);
            }
            set
            {
                HttpContext.Current.Session[SettingKeys.EmpId] = value;
            }
        }

        public static EmployeeInfo Employee
        {
            get
            {
                return (EmployeeInfo)HttpContext.Current.Session[SettingKeys.EmpInfo];
            }
            set
            {
                HttpContext.Current.Session[SettingKeys.EmpInfo] = value;
            }
        }

        public static UserInfo Account
        {
            get
            {
                if (HttpContext.Current.Session[SettingKeys.AccountInfo] != null)
                {
                    return ((UserInfo)HttpContext.Current.Session[SettingKeys.AccountInfo]);
                }
                else
                {
                    return null;
                }
            }
        }

        public static ApplicationEntity Application
        {
            get
            {
                return (ApplicationEntity)HttpContext.Current.Session[SettingKeys.ApplicationInfo];
            }
            set
            {
                HttpContext.Current.Session[SettingKeys.ApplicationInfo] = value;
            }
        }

        public static int PortalId
        {
            get
            {
                return (int)HttpContext.Current.Session[SettingKeys.ApplicationId];
            }
            set
            {
                HttpContext.Current.Session[SettingKeys.ApplicationId] = value;
            }
        }

        public static string DefaultLanguage
        {
            get
            {
                return HttpContext.Current.Session[SettingKeys.DefaultLanguage].ToString();
            }
            set
            {
                HttpContext.Current.Session[SettingKeys.DefaultLanguage] = value;
            }
        }
        
    }
}
