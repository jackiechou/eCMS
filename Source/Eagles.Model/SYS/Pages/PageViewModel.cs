using Eagle.Model.SYS.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Pages
{

    public class PageViewModel : TreeGridViewModel
    {
         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplicationId")]
        public Nullable<int> ApplicationId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContentItemId")]
        public Nullable<int> ContentItemId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SkinId")]
        public Nullable<int> SkinId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TemplateId")]
         public Nullable<int> TemplateId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScopeTypeId")]
        public Nullable<int> ScopeTypeId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LanguageCode")]
        public string LanguageCode { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageId")]
        public int PageId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageGroupId")]
        public int PageGroupId { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageCode")]
        public Nullable<System.Guid> PageCode { get; set; }      

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageName")]
        public string PageName { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageAlias")]
         public string PageAlias { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageTitle")]
        public string PageTitle { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageUrl")]
        public string PageUrl { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PagePath")]
        public string PagePath { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ListOrder")]
        public int ListOrder { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DisableLink")]
        public Nullable<bool> DisableLink { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DisplayTitle")]
        public bool DisplayTitle { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
        public string Description { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Keywords")]
        public string Keywords { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsDeleted")]
        public bool IsDeleted { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsExtenalLink")]
        public Nullable<bool> IsExtenalLink { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsLeaf")]
        public Nullable<bool> IsLeaf { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsMenu")]
        public Nullable<bool> IsMenu { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsSecured")]
        public bool IsSecured { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsVisible")]
        public bool IsVisible { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StartDate")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EndDate")]
        public Nullable<System.DateTime> EndDate { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageHeadText")]
        public string PageHeadText { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageFooterText")]
        public string PageFooterText { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Icon")]
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
        public List<RolePermissionViewModel> RolePermissionList { get; set; }
    }
}
