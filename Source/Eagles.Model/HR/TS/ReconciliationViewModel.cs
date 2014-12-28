using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class ReconciliationViewModel
    {
        public int ReconciliationID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int Year { get; set; }

        public int LSCompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNameVN { get; set; }
        public int EmpID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public Nullable<bool> SearchStatus { get; set; }

        public Nullable<decimal> ALPrevious { get; set; }
        public Nullable<decimal> ALTakenPrevious { get; set; }
        public Nullable<decimal> ALBalancePrevious { get; set; }
        public Nullable<decimal> LeaveForward { get; set; }
        public Nullable<decimal> LeaveForwardTaken { get; set; }
        public bool chkCheck { get; set; }


    }
}
