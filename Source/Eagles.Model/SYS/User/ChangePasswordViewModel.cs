using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class ChangePasswordViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "UserName")]
        public string UserName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OldPassword")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string OldPassword { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NewPassword")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string NewPassword { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RetypeNewPassword")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string RetypeNewPassword { get; set; }
    }
}
