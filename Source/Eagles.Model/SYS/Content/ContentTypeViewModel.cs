using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Content
{
    public class ContentTypeViewModel
    {
        public int ContentTypeId { get; set; }
        public string ContentTypeName { get; set; }
    }

    public enum ContentTypeSetting
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Page")]
        Page = 1,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Module")]
        Module = 2,
    }
}
