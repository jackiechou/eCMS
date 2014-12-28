using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Contents.Galleries
{
    public class GalleryFileViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CollectionId")]
        public Nullable<int> CollectionId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileId")]
        public int FileId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ListOrder")]
        public Nullable<int> ListOrder { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Tags")]
        public string Tags { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedOnDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> CreatedOnDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastModifiedDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> LastModifiedDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedByUserId")]
        public Nullable<Guid> CreatedByUserId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastModifiedByUserId")]
        public Nullable<Guid> LastModifiedByUserId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IPLog")]
        public string IPLog { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IPLastUpdate")]
        public string IPLastUpdate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public string Status { get; set; }
    }
}
