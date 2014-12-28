using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    public class TimeKeepingReportViewModel : timesheet_sprptTimeKeepingReport_detail_Result
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCode")]
        public string EmployeeCode { get; set; }

        public Nullable<DateTime> FromDate { get; set; }

        public Nullable<DateTime> ToDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string FromDateInfo { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string ToDateInfo { get; set; }
    }
}
