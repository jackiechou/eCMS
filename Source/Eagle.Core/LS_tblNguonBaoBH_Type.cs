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
    
    public partial class LS_tblNguonBaoBH_Type
    {
        public LS_tblNguonBaoBH_Type()
        {
            this.LS_tblNguonBaoBH = new HashSet<LS_tblNguonBaoBH>();
        }
    
        public string NguonBaoBH_TypeID { get; set; }
        public string Name { get; set; }
        public string VNName { get; set; }
        public string Note { get; set; }
        public Nullable<bool> Used { get; set; }
    
        public virtual ICollection<LS_tblNguonBaoBH> LS_tblNguonBaoBH { get; set; }
    }
}
