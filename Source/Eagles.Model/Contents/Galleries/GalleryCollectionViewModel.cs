using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Contents.Galleries
{
    public class GalleryCollectionViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TopicId")]
        public int TopicId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CollectionId")]
        public int CollectionId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Title")]
        public string Title { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IconFile")]
        public string IconFile { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ListOrder")]
        public Nullable<int> ListOrder { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
        public string Description { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Tags")]
        public string Tags { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Url")]
        public string Url { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedOnDate")]
        public Nullable<System.DateTime> CreatedOnDate { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastModifieddDate")]
        public Nullable<System.DateTime> LastModifieddDate { get; set; }

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
