using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS
{
    public class MasterDataPermissionViewModel
    {
        public int PermissionID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Group")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int GroupID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Module")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int ModuleID { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MasterDataID")]
        public Nullable<int> MasterDataID { get; set; }

        public string MasterDataName { get; set; }

        public Nullable<bool> FView { get; set; }
        public Nullable<bool> FEdit { get; set; }
        public Nullable<bool> FDelete { get; set; }
        public Nullable<bool> FExport { get; set; }
    }
}
