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
    
    public partial class LS_tblMasterData
    {
        public LS_tblMasterData()
        {
            this.SYS_tblFunctionPermission = new HashSet<SYS_tblFunctionPermission>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string VNName { get; set; }
        public string Module { get; set; }
        public string TableName { get; set; }
        public Nullable<int> Rank { get; set; }
        public string Note { get; set; }
        public Nullable<int> Display { get; set; }
    
        public virtual ICollection<SYS_tblFunctionPermission> SYS_tblFunctionPermission { get; set; }
    }
}
