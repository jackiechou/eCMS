using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Roles
{
    public class RoleModulePermissionViewModel
    {
        public Nullable<System.Guid> ModuleCode { get; set; }
        public Nullable<System.Guid> PermissionCode { get; set; }
        public string RoleName { get; set; }
        public Nullable<System.Guid> RoleCode { get; set; }
        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Search { get; set; }
        public bool Print { get; set; }
        public bool Export { get; set; }
    }
}
