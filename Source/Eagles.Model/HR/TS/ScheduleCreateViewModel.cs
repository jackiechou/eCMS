using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class ScheduleCreateViewModel
    {
        public int ScheduleID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScheduleCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string ScheduleCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScheduleName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string ScheduleName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
        public bool Used { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
        public string Note { get; set; }
    }
}
