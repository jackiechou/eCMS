using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Report.HR.Position
{
    public class PositionReportModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSPositionID")]
        public Nullable<int> LSPositionID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSPositionCode")]
        public string LSPositionCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
        public string PositionName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Qty")]
        public int? Qty { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Percentage")]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? Percentage { get; set; }
    }
}
