using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.Report.TS
{
    public class YearlyOTReportModel
    {
        public Nullable<int> LSCompanyID { get; set; }
        public string LSCompanyName { get; set; }
        public Nullable<int> EmpID { get; set; }
        public string EmpCode { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }

        public int Year { get; set; }

        public decimal TotalHours { get; set; }
        public decimal DayOT { get; set; }
        public decimal NightOT { get; set; }
        public decimal HolidaysOT { get; set; }
    }
}
