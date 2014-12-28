using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Pages
{
    public class PageViewModel : TreeGridViewModel
    {
        public Nullable<int> ApplicationId { get; set; }
        public Nullable<int> ContentItemId { get; set; }
        public Nullable<int> SkinId { get; set; }
        public Nullable<int> ScopeTypeId { get; set; }
        public string LanguageCode { get; set; }
        public int PageId { get; set; }
        public Nullable<System.Guid> PageCode { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> PageLevel { get; set; }
        public string Lineage { get; set; }
        public string ParentList { get; set; }
        public string PageName { get; set; }
        public string PageTitle { get; set; }
        public string PageUrl { get; set; }
        public string PagePath { get; set; }
        public int ListOrder { get; set; }
        public Nullable<bool> DisableLink { get; set; }
        public bool DisplayTitle { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<bool> IsExtenalLink { get; set; }
        public Nullable<bool> IsLeaf { get; set; }
        public Nullable<bool> IsMenu { get; set; }
        public bool IsSecured { get; set; }
        public bool IsVisible { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string PageHeadText { get; set; }
        public string PageFooterText { get; set; }
        public Nullable<int> Icon { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedOnDate")]
        public Nullable<System.Guid> CreatedByUserId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedOnDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> CreatedOnDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastModifiedByUserId")]
        public Nullable<System.Guid> LastModifiedByUserId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastModifiedOnDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> LastModifiedOnDate { get; set; }
    }
}
