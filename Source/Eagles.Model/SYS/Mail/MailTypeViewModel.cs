using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Mail
{
    public class MailTypeViewModel
    {
         [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TypeId")]       
        public int TypeId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PortalId")]
         [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int PortalId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ParentId")]
         [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int ParentId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CultureCode")]
         [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string CultureCode { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
         [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
        public string Description { get; set; }


         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SortKey")]
         [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]     
        public int SortKey { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Depth")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int Depth { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PostedDate")]
        public Nullable<System.DateTime> PostedDate { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastUpdatedDate")]
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public bool Status { get; set; }
    }
}
