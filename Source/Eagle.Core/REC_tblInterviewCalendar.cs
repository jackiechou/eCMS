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
    
    public partial class REC_tblInterviewCalendar
    {
        public int InterviewCalendarID { get; set; }
        public int ProjectTournamentID { get; set; }
        public int CandidateID { get; set; }
        public Nullable<System.DateTime> InterviewDate { get; set; }
        public Nullable<System.DateTime> InterviewTime { get; set; }
    
        public virtual REC_tblCandidate REC_tblCandidate { get; set; }
        public virtual REC_tblProjectTournament REC_tblProjectTournament { get; set; }
    }
}
