using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Report.HR.Qualification
{
    public class QualificationReportModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSQualificationID")]
        public int LSQualificationID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSQualificationCode")]
        public string LSQualificationCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
        public string QualificationName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Qty")]
        public Nullable<int> Qty { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Percentage")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> Percentage { get; set; }
    }
}
