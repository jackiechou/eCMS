using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Permission
{
    public class MenuPermissionViewModel : TreeGridViewModel
    {
        public int MenuTypeId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuTitle { get; set; }
        public string MenuAlias { get; set; }
        public Guid PageCode { get; set; }
        public string PageUrl { get; set; }
        public bool View { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        
    }
}
