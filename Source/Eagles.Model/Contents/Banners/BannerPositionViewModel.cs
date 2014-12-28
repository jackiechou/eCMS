using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Contents.Banners
{
    public class BannerPositionViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PositionId")]
        public int BannerPositionId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PositionCode")]
        public System.Guid BannerPositionCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PositionName")]
        public string BannerPositionName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PositionStatus")]
        public bool BannerPositionStatus { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ListOrder")]
        public Nullable<int> BannerPositionListOrder { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
        public string BannerPositionDescription { get; set; }


    }
}
