using Eagle.Model.SYS.Pages;
using Eagle.Model.SYS.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Modules
{
    public class ModuleTreeModel
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
        public List<ModuleTreeModel> children { get; set; }

        public ModuleTreeModel()
        {
            children = new List<ModuleTreeModel>();
        }
    }
    public class ModuleViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplicationId")]
        public int ApplicationId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContentItemId")]
        public Nullable<int> ContentItemId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScopeTypeId")]
        public int? ScopeTypeId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModuleId")]
        public int ModuleId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModuleCode")]
        public System.Guid ModuleCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModuleKey")]
        public string ModuleKey { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModuleTitle")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string ModuleTitle { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModuleName")]
        public string ModuleName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AllPages")]
        public bool AllPages { get; set; }

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



        //Modified --------------------------------------------------------------------------

        public virtual List<PageModuleViewModel> PageModules { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageId")]
        public Nullable<int> PageId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModuleOrder")]
        public Nullable<int> ModuleOrder { get; set; }

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
        public int Icon { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IconClass")]
        public int IconClass { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsVisible")]
        public Nullable<bool> IsVisible { get; set; }
        public List<RolePermissionViewModel> RolePermissionList { get; set; }
    }
}
