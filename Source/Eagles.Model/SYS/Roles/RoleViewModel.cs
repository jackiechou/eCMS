using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Model.SYS.Roles
{
    public class RoleViewModel
    {
        public int ApplicationId { get; set; }

        public int? RoleGroupId { get; set; }

        public int RoleId { get; set; }

        public Guid? RoleCode { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RoleName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string RoleName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LoweredRoleName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string LoweredRoleName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
        public string Description { get; set; }
    }
}
