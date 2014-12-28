using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Permission
{
    public class FolderPermissionViewModel
    {
        public int FolderPermissionId { get; set; }
        public System.Guid FolderCode { get; set; }
        public System.Guid PermissionCode { get; set; }
        public System.Guid RoleCode { get; set; }
        public System.Guid UserCode { get; set; }
        public bool AllowAccess { get; set; }
    }
}
