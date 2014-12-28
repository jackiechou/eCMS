using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Menu
{
    public class MenuTypeViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplicationId")]
        public int ApplicationId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScopeTypeId")]
        public int ScopeTypeId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LanguageCode")]
        public string LanguageCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MenuTypeId")]
        public int MenuTypeId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MenuTypeCode")]
        public System.Guid MenuTypeCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MenuTypeName")]
        public string MenuTypeName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ListOrder")]
        public int ListOrder { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsAdmin")]
        public bool IsAdmin { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public bool MenuTypeStatus { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PostedDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> PostedDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastUpdatedDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }


        //Modified
       
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScopeTypeId")]
        [Range(1, 3, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "PleaseChooseType" )]
        [Required(AllowEmptyStrings = false,ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public ScopeType SelectedScopeType { get; set; }
    }
}
