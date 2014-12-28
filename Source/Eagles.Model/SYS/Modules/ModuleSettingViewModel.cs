using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Modules
{
    public class ModuleSettingViewModel
    {
        public int ModuleSettingId { get; set; }
        public Nullable<System.Guid> ModuleCode { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
    }
}
