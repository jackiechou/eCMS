using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Application
{
    public class ApplicationEntity
    {
        public static int ApplicationId { get; set; }
        public static string ApplicationCode { get; set; }
        public static string ApplicationName { get; set; }
        public static string DefaultLanguage { get; set; }
        public static string HomeDirectory { get; set; }
        public static string Currency { get; set; }
        public static string TimeZoneOffset { get; set; }
        public static string Url { get; set; }
        public static string LogoFile { get; set; }
        public static string BackgroundFile { get; set; }
        public static string KeyWords { get; set; }
        public static string CopyRight { get; set; }
        public static string FooterText { get; set; }
        public static string Description { get; set; }
        public static int HostSpace { get; set; }
        public static double HostFee { get; set; }
        public static Nullable<System.DateTime> ExpiryDate { get; set; }
        public static Nullable<int> RegisteredUserId { get; set; }
        public static Nullable<int> CreatedByUserId { get; set; }
        public static Nullable<int> LastModifiedByUserId { get; set; }
        public static Nullable<System.DateTime> CreatedOnDate { get; set; }
        public static Nullable<System.DateTime> LastModifiedOnDate { get; set; }
    }

    public class ApplicationInfo
    {
        public int ApplicationId { get; set; }
        public Guid ApplicationCode { get; set; }
        public string ApplicationName { get; set; }
        public string DefaultLanguage { get; set; }
        public string HomeDirectory { get; set; }
        public string Currency { get; set; }
        public string TimeZoneOffset { get; set; }
        public string Url { get; set; }
        public string LogoFile { get; set; }
        public string BackgroundFile { get; set; }
        public string KeyWords { get; set; }
        public string CopyRight { get; set; }
        public string FooterText { get; set; }
        public string Description { get; set; }
        public int HostSpace { get; set; }
        public double HostFee { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<int> RegisteredUserId { get; set; }
        public Nullable<int> CreatedByUserId { get; set; }
        public Nullable<int> LastModifiedByUserId { get; set; }
        public Nullable<System.DateTime> CreatedOnDate { get; set; }
        public Nullable<System.DateTime> LastModifiedOnDate { get; set; }
    }

    public class ApplicationLanguageViewModel
    {
        public int ApplicationLanguageId { get; set; }
        public System.Guid ApplicationCode { get; set; }
        public string LanguageCode { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }

    public class ApplicationModuleViewModel
    {
        public int ApplicationModuleId { get; set; }
        public System.Guid ApplicationCode { get; set; }
        public System.Guid ModuleCode { get; set; }
    }

    public partial class ApplicationPageViewModel
    {
        public int ApplicationPageId { get; set; }
        public System.Guid ApplicationCode { get; set; }
        public System.Guid ModuleCode { get; set; }
    }
}
