using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class C66ASearchViewModel
    {

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Month")]
        public DateTime? MonthYear { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Stage")]
        public int Stage { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public int? LSCompanyID { get; set; }

        public string SIMonth { get {
            if (MonthYear == null)
            {
                return null;
            }else
            {
                return MonthYear.Value.ToString("MM/yyyy");
            }
        } }
    }
}
