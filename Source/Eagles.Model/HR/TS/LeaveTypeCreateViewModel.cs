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
    public class LeaveTypeCreateViewModel
    {
        public int LSLeaveTypeID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSLeaveTypeCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string LSLeaveTypeCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
        public string Note { get; set; }
    }
}
