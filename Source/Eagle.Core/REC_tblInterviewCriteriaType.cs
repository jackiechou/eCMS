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
    
    public partial class REC_tblInterviewCriteriaType
    {
        public REC_tblInterviewCriteriaType()
        {
            this.REC_tblInterviewCriteria = new HashSet<REC_tblInterviewCriteria>();
        }
    
        public int InterviewCriteriaTypeID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> Rank { get; set; }
        public Nullable<bool> Used { get; set; }
        public string Note { get; set; }
    
        public virtual ICollection<REC_tblInterviewCriteria> REC_tblInterviewCriteria { get; set; }
    }
}