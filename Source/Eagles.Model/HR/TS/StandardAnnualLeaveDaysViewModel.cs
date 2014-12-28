using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Eagle.Core;
namespace Eagle.Model
{
    public class StandardAnnualLeaveDaysViewModel
    {
        public int StandardALID { get; set; }
        public int EmpID { get; set; }
        public decimal StandardALDays { get; set; }
        //
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public int LSCompanyID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public Nullable<bool> SearchActive1 { get; set; }
    }

    //public class StandardAnnualLeaveAdjustViewModel : Timesheet_tblAnnualLeaveAdjust
    //{
    //    public string strDateAdjust
    //    {
    //        get
    //        {
    //            return String.Format("{0:dd/MM/yyyy}", AdjustDate);
    //        }
    //    }

    //}
}
