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
    
    public partial class LS_tblMultiLevel
    {
        public int LSID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string Lineage { get; set; }
        public string LSCode { get; set; }
        public string Name { get; set; }
        public string VNName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public Nullable<int> Rank { get; set; }
        public Nullable<bool> Used { get; set; }
        public string TaxCode { get; set; }
        public string Creater { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Editor { get; set; }
        public Nullable<System.DateTime> EditTime { get; set; }
    }
}
