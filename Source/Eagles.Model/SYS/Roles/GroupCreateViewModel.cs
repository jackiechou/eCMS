using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Eagle.Model
{
    public class GroupCreateViewModel
    {
        public int GroupID { get; set; }

        public string InitialGroupCode { get { return GroupCode; } }

        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "GroupCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Remote("ValidationGroupCode", "Validation", AdditionalFields = "InitialGroupCode")]
        public string GroupCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "GroupName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string GroupName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
        public string Note { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
        public Nullable<bool> Used { get; set; }

      

    }
}
