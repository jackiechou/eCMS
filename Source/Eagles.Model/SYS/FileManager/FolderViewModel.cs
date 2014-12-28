using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Model.SYS
{
    public class FolderViewModel
    {
        [Key]
        public int FolderId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FolderKey")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string FolderKey { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FolderPath")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        public string FolderPath { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedByUserId")]
        public Nullable<int> CreatedByUserId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedOnDate")]
        public Nullable<DateTime> CreatedOnDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastModifiedByUserId")]
        public Nullable<int> LastModifiedByUserId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastModifiedOnDate")]
        public Nullable<DateTime> LastModifiedOnDate { get; set; }
    }
}
