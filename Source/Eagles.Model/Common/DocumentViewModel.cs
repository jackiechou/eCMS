using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Eagle.Model
{
    public class DocumentViewModel : DOC_tblDocument
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "UploadFile")]
        public HttpPostedFileBase UploadFile { get; set; }
        public string ReferenceType { get; set; }
        public string UserUpload { get; set; }
        public string UserModified { get; set; }
        public string FilePath { get; set; }
        public string FileErrorMessage { get; set; }
    }
}
