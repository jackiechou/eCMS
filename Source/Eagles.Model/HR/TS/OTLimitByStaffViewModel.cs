using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Core;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Eagle.Common.Utilities;
namespace Eagle.Model
{
    public class OTLimitByStaffListViewModel : Timesheet_tblOTLimitByStaff
    {
        public bool chkCheck { get; set; }
        public string strPositonName { get; set; }
        public string EmpCode { get; set; }
        public string FullName { get; set; }
    }
    public class OTLimitByStaffCreateViewModel 
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public Nullable<int> SearchLSCompanyID { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public Nullable<bool> SearchActive1 { get; set; }
    }
}
