using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Eagle.Model
{
    public class FunctionPermissionCreateViewModel
    {
        public int PermissionID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Group")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int GroupID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Module")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int ModuleID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Function")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public Nullable<int> FunctionID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Function")]

        public Nullable<int> MasterDataID { get; set; }
        public Nullable<bool> FView { get; set; }
        public Nullable<bool> FEdit { get; set; }
        public Nullable<bool> FDelete { get; set; }
        public Nullable<bool> FExport { get; set; }
    }
}
