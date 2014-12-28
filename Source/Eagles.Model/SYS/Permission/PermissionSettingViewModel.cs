using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Permission
{
    public enum PermissionMode
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "View")]
        View = 1,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Create")]
        Create = 2,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Edit")]
        Edit = 3,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Details")]
        Details = 4,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Delete")]
        Delete = 5,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Validate")]
        Validate = 6,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InValidate")]
        InValidate = 7,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Print")]
        Print = 8,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Import")]
        Import = 9,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Export")]
        Export = 10,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Upload")]
        Upload = 11
    }

    public static class PermissionKey
    {
        public static Nullable<bool> View = false;
        public static Nullable<bool> Create = false;
        public static Nullable<bool> Edit = false;
        public static Nullable<bool> Details = false;
        public static Nullable<bool> Delete = false;
        public static Nullable<bool> Validate = false;
        public static Nullable<bool> InValidate = false;
        public static Nullable<bool> Print = false;
        public static Nullable<bool> Import = false;
        public static Nullable<bool> Export = false;
        public static Nullable<bool> Upload = false;
    }
    public class PermissionSettingViewModel
    {
        public Nullable<bool> View { get; set; }
        public Nullable<bool> Create { get; set; }
        public Nullable<bool> Details { get; set; }
        public Nullable<bool> Edit { get; set; }
        public Nullable<bool> Delete { get; set; }
        public Nullable<bool> Validate { get; set; }
        public Nullable<bool> InValidate { get; set; }
        public Nullable<bool> Print { get; set; }
        public Nullable<bool> Import { get; set; }
        public Nullable<bool> Export { get; set; }
        public Nullable<bool> Upload { get; set; }
    }
}
