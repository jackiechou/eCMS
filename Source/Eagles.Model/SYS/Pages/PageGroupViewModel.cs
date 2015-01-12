using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Pages
{
    
    public class PageGroupViewModel
    {
       

        

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageGroupId")]
        public int PageGroupId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageGroupCode")]
        public string PageGroupCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageGroupName")]
        public string PageGroupName { get; set; }

       

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsActive")]
        public bool? IsActive { get; set; }
    }
}