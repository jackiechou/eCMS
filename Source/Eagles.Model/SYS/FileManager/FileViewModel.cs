using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Eagle.Model.SYS
{
    public class FileControlModel
    {
        public string FolderKey { get; set; }

        public string FileIds { get; set; }

        public int? KeyId { get; set; }

        public string UpdateFileIdsServiceUrl { get; set; }    
    }

    public class FileModel
    {
        public string ItemId { get; set; }
        public string ItemTag { get; set; }
        public string FileIds { get; set; }
        public string FolderKey { get; set; }
        public IList<FileUploadModel> FileUploadList { get; set; }
    }

    public class FileUploadModel
    {
        public int? FileId { get; set; }

        public string ContentType { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileTitle")]
        public string FileTitle { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileName")]
        public string FileName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileDescription")]
        public string FileDescription { get; set; }       

        public string Extension { get; set; }

        public byte[] FileContent { get; set; }

        public string FileUrl { get; set; }

        public Nullable<int> Width { get; set; }

        public Nullable<int> Height { get; set; }     

        public Nullable<int> Size { get; set; }

        public Nullable<int> FolderId { get; set; }

        public string FolderKey { get; set; }

        public string FolderPath { get; set; }

        public HttpPostedFileBase FileUploadName { get; set; }

        //Modified FILE MANAGER 
        public int ItemId { get; set; }
        public string ItemTag { get; set; }
        public string FileIds { get; set; }
    }

    public class FileViewModel
    {
        [Key]
        public int FileId { get; set; }      

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileName")]
        public string FileName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileTitle")]
        public string FileTitle { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileContent")]
        public byte[] FileContent { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileDescription")]
        public string FileDescription { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Extension")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        public string Extension { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContentType")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        public string ContentType { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FolderId")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int FolderId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FolderKey")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string FolderKey { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FolderPath")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        public string FolderPath { get; set; }

        public string FileUrl { get; set; }        

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Size")]
        public Nullable<int> Size { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Width")]
        public Nullable<int> Width { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Height")]
        public Nullable<int> Height { get; set; }       

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedByUserId")]
        public Nullable<int> CreatedByUserId { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedOnDate")]
        public Nullable<DateTime> CreatedOnDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastModifiedByUserId")]
        public Nullable<int> LastModifiedByUserId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastModifiedOnDate")]
        public Nullable<DateTime> LastModifiedOnDate { get; set; }
        #region elFinder.Net.Web.ViewModels =====================================================
        public string Folder { get; set; }
        public string SubFolder { get; set; }
        #endregion ==============================================================================
    }
}
