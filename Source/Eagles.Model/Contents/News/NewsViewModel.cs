using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Contents.News
{
    public class NewsViewModel
    {
        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NewsId")]
        public int NewsId { get; set; }

        public string CategoryId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CategoryCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string CategoryAlias { get; set; }
        public int? CategoryImage { get; set; }
        public string CategoryIconUrl { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Headline")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Headline { get; set; }

        public string Alias { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Summary")]
        public string Summary { get; set; }

        public string Authors { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Url")]
        public string NavigateUrl { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Attachment")]
        public int? FrontImage { get; set; }
        public string FrontImageUrl { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Attachment")]
        public int? MainImage { get; set; }
        public string MainImageUrl { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MainText")]
        public string MainText { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Source")]
        public string Source { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Tags")]
        public string Tags { get; set; }
         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ListOrder")]
         public int? ListOrder { get; set; }
          [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TotalRates")]
        public int? TotalRates { get; set; }
          [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TotalViews")]
        public int? TotalViews { get; set; }

        public int Status { get; set; }
         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LanguageCode")]
        public string LanguageCode { get; set; }

        public int ApplicationId { get; set; }


        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedOnDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> CreatedOnDate { get; set; }

        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastModifiedOnDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> LastModifiedOnDate { get; set; }

        public Nullable<int> CreatedByUserId { get; set; }

        public Nullable<int> LastModifiedByUserId { get; set; }
          [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IPLog")]
        public string IPLog { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IPLastUpdated")]
        public string IPLastUpdated { get; set; }
    }
}
