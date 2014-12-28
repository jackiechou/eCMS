using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Pages
{
    public class PageModuleSettingViewModel
    {
        public int PageModuleSettingId { get; set; }
        public int PageModuleId { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
    }
}
