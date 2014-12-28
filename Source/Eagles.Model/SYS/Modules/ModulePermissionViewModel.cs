using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Modules
{
    public class ModulePermissionViewModel
    {
        public int ModulePermissionId { get; set; }
        public Nullable<System.Guid> ModuleCode { get; set; }
        public Nullable<System.Guid> PermissionCode { get; set; }
        public Nullable<System.Guid> RoleCode { get; set; }
        public Nullable<System.Guid> UserCode { get; set; }
        public Nullable<bool> AllowAccess { get; set; }
    }  
   
}
