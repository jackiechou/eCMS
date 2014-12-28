using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Eagle.Model.Contents.News
{
    public class NewsCategoryViewModel
    {
        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CategoryId")]
        public int CategoryId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CategoryCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string CategoryCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CategoryName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string CategoryName { get; set; }

        public string Alias { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ParentId")]
        public int? ParentId { get; set; }
         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Depth")]
        public int? Depth { get; set; }
         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Lineage")]
        public string Lineage { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Attachment")]
        public int? CategoryImage { get; set; }

        public HttpPostedFileBase FileUpload { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Url")]
        public string NavigateUrl { get; set; }

        public int? ListOrder { get; set; }

        public int? Status { get; set; }

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

        public string IPLog { get; set; }

        public string IPLastUpdated { get; set; }
    }
}