using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class InsuranceInformationSearchViewModel
    {
        public int? EmpID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SIBook")]
        public string SIBook { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "HIBook")]
        public string HIBook { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]      
        public DateTime? FromDate { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]   
        public DateTime? ToDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StartDateSI2")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
        public DateTime? StartDateSI { get; set; }

    }
}
