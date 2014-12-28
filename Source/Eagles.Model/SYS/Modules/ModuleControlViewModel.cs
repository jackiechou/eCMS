using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Modules
{
    public class ModuleControlViewModel
    {
        public int ModuleControlId { get; set; }
        public int ModuleId { get; set; }
        public string ControlTitle { get; set; }
        public string ControlKey { get; set; }
        public string ControlSrc { get; set; }
        public int ControlType { get; set; }
        public string IconFile { get; set; }
        public int ViewOrder { get; set; }
    }
}
