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
    
    public partial class LS_tblTrainingProvider
    {
        public LS_tblTrainingProvider()
        {
            this.LS_tblTrainingCourse = new HashSet<LS_tblTrainingCourse>();
            this.TR_tblPlan = new HashSet<TR_tblPlan>();
            this.TR_tblTrainingRequest = new HashSet<TR_tblTrainingRequest>();
        }
    
        public int LSTrainingProviderID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string ContactPerson { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public Nullable<int> Rank { get; set; }
        public bool Used { get; set; }
        public string Note { get; set; }
    
        public virtual ICollection<LS_tblTrainingCourse> LS_tblTrainingCourse { get; set; }
        public virtual ICollection<TR_tblPlan> TR_tblPlan { get; set; }
        public virtual ICollection<TR_tblTrainingRequest> TR_tblTrainingRequest { get; set; }
    }
}
