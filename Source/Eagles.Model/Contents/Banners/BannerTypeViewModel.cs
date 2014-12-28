using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Contents.Banners
{
    public class BannerTypeViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerTypeId")]
        public int BannerTypeId { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerTypeName")]
        public string BannerTypeName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerTypeDescription")]
        public string BannerTypeDescription { get; set; }
    }
}
