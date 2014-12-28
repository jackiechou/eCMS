using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Core.MetaData
{

    [MetadataTypeAttribute(typeof(DOC_tblDocument.MetaData))]
    public partial class DOC_tblDocument
    {
        internal sealed class MetaData
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DocumentNo")]
            public int DocumentId { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ReferenceType")]
            public Nullable<int> LSReferenceId { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Title")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string FileTitle { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileName")]
            public string FileNameVirtual { get; set; }
            public string FileNamePhysical { get; set; }
            public string Extension { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContentType")]
            public string ContentType { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Folder")]
            public int FolderId { get; set; }
            public byte[] FileContent { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
            public string FileDescription { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Keywords")]
            public string Keywords { get; set; }
            public Nullable<int> Size { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "UploadedBy")]
            public Nullable<int> CreatedByUserId { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "UploadedTime")]
            public Nullable<System.DateTime> CreatedOnDate { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModifiedBy")]
            public Nullable<int> LastModifiedByUserId { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModifiedTime")]
            [DisplayFormat(DataFormatString="{0:dd/MM/yyyy hh:mm}")]
            public Nullable<System.DateTime> LastModifiedOnDate { get; set; }
        }

    }
}
