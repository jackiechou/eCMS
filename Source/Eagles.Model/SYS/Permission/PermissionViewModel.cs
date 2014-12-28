using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Permission
{
   

    public class PermissionViewModel
    {
        public int PermissionId { get; set; }
        public string PermissionCode { get; set; }
        public string PermissionKey { get; set; }
        public string PermissionName { get; set; }
        public int DisplayOrder { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsModified { get; set; }
    }
}