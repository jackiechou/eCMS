using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Pages
{
    public class PageSettingViewModel
    {
        public int PageSettingId { get; set; }
        public Nullable<System.Guid> PageCode { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
    }
}
