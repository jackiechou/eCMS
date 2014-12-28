using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model.Mail;
using Eagle.Model;

namespace Eagle.Repository.Host
{
    public class HostSettings
    {     
        #region Host Accessor ================================================
        public static string HostCurrency
        {
            get
            {
                string setting = GetHostSetting("HostCurrency");
                if (string.IsNullOrEmpty(setting))
                {
                    setting = "USD";
                }
                return setting;
            }
        }
        public static string HostEmail
        {
            get { return GetHostSetting("HostEmail"); }
        }
        public static double HostFee
        {
            get { return GetHostSettingAsDouble("HostFee", 0); }
        }
        public static int HostPortalID
        {
            get { return GetHostSettingAsInteger("HostPortalId"); }
        }
        public static double HostSpace
        {
            get { return GetHostSettingAsDouble("HostSpace", 0); }
        }
        public static string HostTitle
        {
            get { return GetHostSetting("HostTitle"); }
        }

        public static string HostURL
        {
            get { return GetHostSetting("HostURL"); }
        }
        public static int PageQuota
        {
            get { return GetHostSettingAsInteger("PageQuota", 0); }
        }
        public static int PasswordExpiry
        {
            get { return GetHostSettingAsInteger("PasswordExpiry", 0); }
        }
        public static int PasswordExpiryReminder
        {
            get { return GetHostSettingAsInteger("PasswordExpiryReminder", 7); }
        }     
      
        public static bool UseFriendlyUrls
        {
            get { return GetHostSettingAsBoolean("UseFriendlyUrls", false); }
        }
        public static bool UseCustomErrorMessages
        {
            get { return GetHostSettingAsBoolean("UseCustomErrorMessages", false); }
        }
        public static int UserQuota
        {
            get { return GetHostSettingAsInteger("UserQuota", 0); }
        }
        public static int UsersOnlineTimeWindow
        {
            get { return GetHostSettingAsInteger("UsersOnlineTime", 15); }
        }
        public static int WebRequestTimeout
        {
            get { return GetHostSettingAsInteger("WebRequestTimeout", 10000); }
        }
        public static bool WhitespaceFilter
        {
            get { return GetHostSettingAsBoolean("WhitespaceFilter", false); }
        }

        public static int SMTP_AccountId
        {
            get { return GetHostSettingAsInteger("SMTP_AccountId"); }
        }

        public static string SMTPAuthentication
        {
            get { return GetHostSetting("SMTPAuthentication"); }
        }

        public static bool SMTPEnableSSL
        {
            get { return GetHostSettingAsBoolean("SMTPEnableSSL", false); }
        }

        public static string ExensionImage
        {
            get { return GetHostSetting("ExensionImage"); }
        }

        public static string ExtensionVideo
        {
            get { return GetHostSetting("ExtensionVideo"); }
        }

        public static string ExtensionFileUpload
        {
            get { return GetHostSetting("ExtensionFileUpload"); }
        }
        public static MailServerProviderAccountViewModel HostMailAccountInfo
        {
            get
            {
                int? SMTP_AccountId = HostSettings.SMTP_AccountId;
                MailServerProviderAccountViewModel account_entity = new MailServerProviderAccountViewModel();
                if (SMTP_AccountId != null && SMTP_AccountId > 0)
                    account_entity = MailAccountRespository.GetFullDetails(SMTP_AccountId);
                return account_entity;
            }
        }     

        public static string SMTPServer
        {
            get
            {
                string SMTPServer = HostMailAccountInfo.Outgoing_Mail_Server_Host + ":" + HostMailAccountInfo.Outgoing_Mail_Server_Port;
                return SMTPServer;
            }
        }

        public static string SMTPMEmail
        {
            get { return HostMailAccountInfo.Mail_Address; }
        }

        public static string SMTPUsername
        {
            get { return HostMailAccountInfo.Mail_Account; }
        }
        public static string SMTPPassword
        {
            get { return HostMailAccountInfo.Mail_Password; }
        }

        public static string Mail_Signature
        {
            get { return HostMailAccountInfo.Mail_Signature; }
        }
        #endregion Host Accessor ========================================================

        private static bool GetHostSettingAsBoolean(string key, bool defaultValue)
        {
            bool retValue = false;
            try
            {
                string setting = GetHostSetting(key);
                if (string.IsNullOrEmpty(setting))
                {
                    retValue = defaultValue;
                }
                else
                {
                    retValue = (setting.ToUpperInvariant().StartsWith("Y") || setting.ToUpperInvariant() == "TRUE");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return retValue;
        }
        private static double GetHostSettingAsDouble(string key, double defaultValue)
        {
            double retValue = 0;
            try
            {
                string setting = GetHostSetting(key);
                if (string.IsNullOrEmpty(setting))
                {
                    retValue = defaultValue;
                }
                else
                {
                    retValue = Convert.ToDouble(setting);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return retValue;
        }
        private static int GetHostSettingAsInteger(string key)
        {
            return GetHostSettingAsInteger(key, Null.NullInteger);
        }
        private static int GetHostSettingAsInteger(string key, int defaultValue)
        {
            int retValue = 0;
            try
            {
                string setting = GetHostSetting(key);
                if (string.IsNullOrEmpty(setting))
                {
                    retValue = defaultValue;
                }
                else
                {
                    retValue = Convert.ToInt32(setting);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return retValue;
        }
        private static string GetHostSettingAsString(string key, string defaultValue)
        {
            string retValue = defaultValue;
            try
            {
                string setting = GetHostSetting(key);
                if (!string.IsNullOrEmpty(setting))
                {
                    retValue = setting;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return retValue;
        }

        private static string GetHostSetting(string key)
        {
            string setting = null;
            GetHostSettingsDictionary().TryGetValue(key, out setting);
            return setting;
        }
    
        public static List<HostSetting> GetHostSettings()
        {
            List<HostSetting> lst = new List<HostSetting>();
            using (EntityDataContext context = new EntityDataContext())
            {
                lst = (from x in context.HostSettings select x).ToList();
            }
            return lst;
        }

        public static Dictionary<string, string> GetHostSettingsDictionary()
        {
            Dictionary<string, string> dicSettings = new Dictionary<string, string>();
            List<HostSetting> lst = GetHostSettings();
            if (lst.Count() > 0)
            {
                foreach (var item in lst)
                {
                    dicSettings.Add(item.SettingName, item.SettingValue);
                }
            }
            return dicSettings;
        }

        public static string GetSecureHostSetting(string key)
        {
            string setting = null;
            GetSecureHostSettingsDictionary().TryGetValue(key, out setting);
            return setting;
        }

        public static Dictionary<string, string> GetSecureHostSettingsDictionary()
        {
            Dictionary<string, string> dicSettings = new Dictionary<string, string>();
            List<HostSetting> lst = GetHostSettings();
            if (lst.Count() > 0)
            {
                foreach (var item in lst)
                {
                    if (item.SettingIsSecure == true)
                    {
                        dicSettings.Add(item.SettingName, item.SettingValue);
                    }
                }
            }
            return dicSettings;
        }


        public static bool IsExists(string SettingName)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.HostSettings.FirstOrDefault(p => p.SettingName.ToLower().Equals(SettingName.Trim().ToLower()));
                return (query != null);
            }
        }

        public static bool AddHostSetting(Guid ApplicationCode, string SettingName, string SettingValue, bool SettingIsSecure, int createdByUserID)
        {
            bool result = false;
            try
            {

                bool isDuplicate = IsExists(SettingName);
                if (isDuplicate == false)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        HostSetting model = new HostSetting();

                        model.SettingName = SettingName;
                        model.SettingValue = SettingValue;
                        model.ApplicationCode = ApplicationCode;
                        model.SettingIsSecure = SettingIsSecure;

                        int affectedRow = 0;
                        context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        affectedRow = context.SaveChanges();
                        if (affectedRow > 0)
                            result = true;
                    }
                }
                else
                    result = false;
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }

     }
}
