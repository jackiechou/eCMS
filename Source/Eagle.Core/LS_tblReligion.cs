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
    
    public partial class LS_tblReligion
    {
        public LS_tblReligion()
        {
            this.REC_tblCandidate = new HashSet<REC_tblCandidate>();
        }
    
        public int LSReligionID { get; set; }
        public string LSReligionCode { get; set; }
        public string Name { get; set; }
        public string VNName { get; set; }
        public Nullable<short> Rank { get; set; }
        public string Note { get; set; }
        public Nullable<bool> Used { get; set; }
    
        public virtual ICollection<REC_tblCandidate> REC_tblCandidate { get; set; }
    }
}
