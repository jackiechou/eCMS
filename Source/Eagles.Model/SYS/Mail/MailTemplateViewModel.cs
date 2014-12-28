using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Mail
{
    public class MailTemplateViewModel
    {
        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailTemplateId")]
        public int Mail_Template_Id { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailTemplateName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Mail_Template_Name { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailTemplateContent")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Mail_Template_Content { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailTemplateDiscontinued")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public Nullable<bool> Mail_Template_Discontinued { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailTypeId")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public Nullable<int> Mail_Type_Id { get; set; }
        public string Mail_Type_Name { get; set; }
    }
}
