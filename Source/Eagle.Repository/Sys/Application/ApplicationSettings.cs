using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Eagle.Repository.SYS.Application
{
    public class ApplicationSettings
    {
        public int ApplicationId { get; set; }
        public int ApplicationSettingId { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
        public Nullable<int> CreatedByUserId { get; set; }
        public Nullable<int> LastModifiedByUserId { get; set; }
        public Nullable<System.DateTime> CreatedOnDate { get; set; }
        public Nullable<System.DateTime> LastModifiedOnDate { get; set; }
    }
}
