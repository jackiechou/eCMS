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
    public class ScheduleDetailCreateViewModel
    {
        public int ScheduleDetailID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScheduleID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int ScheduleID { get; set; }

        public string ScheduleName { get; set; }

        public System.DateTime DateID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? Year { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSShiftID")]
        public int? LSShiftID { get; set; }
        public string strShiftName { get; set; }



  


        // hien thi lich va ngay gio
        public int TypeDate { get; set; }
        public int M { get; set; }
        public int Day { get; set; }
        public string strTypeDate { get; set; }
    }

    public class ScheduleFindViewModel : ScheduleDetailCreateViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSShiftID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? LSShiftIDtmp { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        public string FromDate { get; set; }


        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
        public string ToDate { get; set; }
    }
}
