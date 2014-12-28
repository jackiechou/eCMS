using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Pages
{
    public class PageBannerViewModel{
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageBannerId")]
        public int PageBannerId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerCode")]
        public System.Guid BannerCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageCode")]
        public System.Guid PageCode { get; set; }
    }
}
