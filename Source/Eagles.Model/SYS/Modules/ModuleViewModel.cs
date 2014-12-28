using Eagle.Model.SYS.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Modules
{
    public class ModuleViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplicationId")]
        public int ApplicationId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContentItemId")]
        public Nullable<int> ContentItemId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScopeTypeId")]
        public int ScopeTypeId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModuleId")]
        public int ModuleId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageId")]
        public Nullable<int> PageId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModuleCode")]
        public System.Guid ModuleCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModuleKey")]
        public string ModuleKey { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModuleTitle")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string ModuleTitle { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModuleName")]
        public string ModuleName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AllTabs")]
        public bool AllTabs { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsAdmin")]
        public bool IsAdmin { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InheritViewPermissions")]
        public bool InheritViewPermissions { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Header")]
        public string Header { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Footer")]
        public string Footer { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StartDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EndDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> EndDate { get; set; }      

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Visibility")]
        public Nullable<bool> Visibility { get; set; }


        public virtual List<PageModuleViewModel> PageModules { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Pane")]
        public string Pane { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Alignment")]
        public string Alignment { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Color")]
        public string Color { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Border")]
        public string Border { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InsertedPosition")]
        public string InsertedPosition { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Icon")]

        public Nullable<int> Icon { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModuleOrder")]
        public Nullable<int> ModuleOrder { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsVisible")]
        public Nullable<bool> IsVisible { get; set; }

    }
}
