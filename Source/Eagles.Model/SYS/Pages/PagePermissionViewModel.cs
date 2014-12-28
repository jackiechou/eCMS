using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Pages
{
    public class PagePermissionViewModel
    {
        public int PagePermissionId { get; set; }
        public System.Guid PageCode { get; set; }
        public System.Guid PermissionCode { get; set; }
        public Nullable<System.Guid> RoleCode { get; set; }
        public Nullable<System.Guid> UserCode { get; set; }
        public Nullable<bool> AllowAccess { get; set; }
    }
}
