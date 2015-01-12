using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Permission
{
    public static class PermissionCodeStatus
    {
        public static string SYSTEM_PAGE = "SYSTEM_PAGE";
        public static string SYSTEM_MENU = "SYSTEM_MENU";
        public static string SYSTEM_MODULE = "SYSTEM_MODULE";
        public static string SYSTEM_FUNCTION = "SYSTEM_FUNCTION";
    }

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
