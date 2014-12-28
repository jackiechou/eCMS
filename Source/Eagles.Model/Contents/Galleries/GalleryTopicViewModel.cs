using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Contents.Galleries
{
    public class GalleryTopicViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TopicId")]
        public int TopicId { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TopicName")]
        public string TopicName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Alias")]
        public string Alias { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ParentId")]
        public int ParentId { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Depth")]
        public string Depth { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Lineage")]
        public string Lineage { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SortKey")]
        public Nullable<int> SortKey { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
        public string Description { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PostedDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> PostedDate { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastUpdatedDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
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
