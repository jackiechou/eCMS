using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Model
{
    public class LoginViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AccountModel_UserName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AccountModel_Password")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RememberMe")]
        public bool RememberMe { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LanguageId")]
        public int LanguageId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LanguageCode")]
        public string LanguageCode { get; set; }

        public string DesiredUrl { get; set; }      
    }
}