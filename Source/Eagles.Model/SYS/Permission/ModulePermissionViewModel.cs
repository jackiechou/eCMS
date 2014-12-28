using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Permission
{
    public class ModulePermissionViewModel
    {
        public int ModuleId { get; set; }
        public bool View { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
    }
}
