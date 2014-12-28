using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class GeneralInformation_InsNoticeDataViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Type")]
        public string TypeName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AriseDate")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
        public DateTime AriseDate  { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ToDate { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OldSalaryCoef")]
        [DisplayFormat(DataFormatString = "{0:#,##0.0}")]
        public decimal? OldSalaryCoef { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OldSalary")]
        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public decimal? OldSalary { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NewSalaryCoef")]
        [DisplayFormat(DataFormatString = "{0:#,##0.0}")]
        public decimal? NewSalaryCoef { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NewSalary")]
        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public decimal? NewSalary { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Percent")]
        [DisplayFormat(DataFormatString = "{0:#,##0.0}")]
        public decimal? Percent { get; set; }
    }
}
