using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class StandardAnnualLeaveCreateViewModel
    {
        public int StandardALID { get; set; }
        public int EmpID { get; set; }
        public decimal StandardALDays { get; set; }
        public bool chkCheck { get; set; }
        //
        private  string _strStartDate;
        public  string strStartDate {
            get {
                _strStartDate = String.Format("{0:dd/MM/yyyy}", StartDate);
                return _strStartDate;
            }
            set
            {
                _strStartDate = value;
            }
        }
        public DateTime? StartDate { get; set; }

        public string strPositonName { get; set; }
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
