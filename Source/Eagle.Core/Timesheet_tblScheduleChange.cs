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
    
    public partial class Timesheet_tblScheduleChange
    {
        public int ChangeScheduleID { get; set; }
        public int ScheduleID_To { get; set; }
        public int ScheduleID_From { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public int EmpID { get; set; }
        public string Note { get; set; }
    
        public virtual TimeSheet_tblSchedule TimeSheet_tblSchedule { get; set; }
        public virtual TimeSheet_tblSchedule TimeSheet_tblSchedule1 { get; set; }
    }
}
