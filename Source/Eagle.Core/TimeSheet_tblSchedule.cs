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
    
    public partial class TimeSheet_tblSchedule
    {
        public TimeSheet_tblSchedule()
        {
            this.TimeSheet_tblAssignEmpSchedule = new HashSet<TimeSheet_tblAssignEmpSchedule>();
            this.Timesheet_tblScheduleChange = new HashSet<Timesheet_tblScheduleChange>();
            this.Timesheet_tblScheduleChange1 = new HashSet<Timesheet_tblScheduleChange>();
            this.TimeSheet_tblScheduleDetail = new HashSet<TimeSheet_tblScheduleDetail>();
        }
    
        public int ScheduleID { get; set; }
        public string ScheduleCode { get; set; }
        public string ScheduleName { get; set; }
        public bool Used { get; set; }
        public string Note { get; set; }
    
        public virtual ICollection<TimeSheet_tblAssignEmpSchedule> TimeSheet_tblAssignEmpSchedule { get; set; }
        public virtual ICollection<Timesheet_tblScheduleChange> Timesheet_tblScheduleChange { get; set; }
        public virtual ICollection<Timesheet_tblScheduleChange> Timesheet_tblScheduleChange1 { get; set; }
        public virtual ICollection<TimeSheet_tblScheduleDetail> TimeSheet_tblScheduleDetail { get; set; }
    }
}
