using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Content
{
    public class ContentTreeModel
    {
        public int id { get; set; }
        public int key { get; set; }
        public int? parentId { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string tooltip { get; set; }
        public bool? isParent { get; set; }
        public bool? open { get; set; }
        public List<ContentTreeModel> children { get; set; }

        public ContentTreeModel()
        {
            children = new List<ContentTreeModel>();
        }
    }

    public class ContentItemViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageId")]
        public Nullable<int> PageId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModuleId")]
        public Nullable<int> ModuleId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContentTypeId")]
        public int ContentTypeId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContentItemId")]
        public int ContentItemId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContentItemName")]
         public string ContentItemName { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContentItemTitle")]
         public string ContentItemTitle { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Content")]
        public string Content { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContentKey")]
        public string ContentKey { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsActive")]
        public bool? IsActive { get; set; }

        public string PageName { get; set; }
        public string ModuleName { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplicationId")]
        public int ApplicationId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LanguageCode")]
        public string LanguageCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScopeTypeId")]
        public int? ScopeTypeId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ParentId")]
        public int? ParentId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Depth")]
        public int? Depth { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Lineage")]
        public string Lineage { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsParent")]
        public bool? IsParent { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ListOrder")]
        public int? ListOrder { get; set; }
    }
}


