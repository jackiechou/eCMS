using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class DurationUserViewModel : TR_tblDurationUser
    {
        public string EmployeeName { get; set; }
        public int LSCompanyID { get; set; }
        public string CompanyName { get; set; }
        public int LSPositionID { get; set; }
        public string PositionName { get; set; }
        public string DurationInfo { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
    }
}
