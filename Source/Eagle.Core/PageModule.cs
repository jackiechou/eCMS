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
    using System.Collections.Generic;
    
    public partial class PageModule
    {
        public int PageModuleId { get; set; }
        public int PageId { get; set; }
        public int ModuleId { get; set; }
        public string Pane { get; set; }
        public string Alignment { get; set; }
        public string Color { get; set; }
        public string Border { get; set; }
        public string InsertedPosition { get; set; }
        public Nullable<int> Icon { get; set; }
        public Nullable<int> ModuleOrder { get; set; }
        public Nullable<bool> IsVisible { get; set; }
    }
}
