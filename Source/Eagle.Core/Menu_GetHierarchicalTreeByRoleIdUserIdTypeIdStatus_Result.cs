//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eagle.Core
{
    using System;
    
    public partial class Menu_GetHierarchicalTreeByRoleIdUserIdTypeIdStatus_Result
    {
        public int MenuId { get; set; }
        public int MenuTypeId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int Depth { get; set; }
        public int ListOrder { get; set; }
        public string MenuName { get; set; }
        public string MenuAlias { get; set; }
        public Nullable<int> PageId { get; set; }
        public string Target { get; set; }
        public Nullable<int> IconFile { get; set; }
        public string IconClass { get; set; }
        public string CssClass { get; set; }
        public string Description { get; set; }
        public int MenuStatus { get; set; }
        public int ScopeTypeId { get; set; }
        public string PagePath { get; set; }
        public string KeyWords { get; set; }
    }
}
