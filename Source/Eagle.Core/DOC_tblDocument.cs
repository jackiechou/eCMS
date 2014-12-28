//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eagle.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class DOC_tblDocument
    {
        public DOC_tblDocument()
        {
            this.REC_tblCandidate = new HashSet<REC_tblCandidate>();
        }
    
        public int DocumentId { get; set; }
        public Nullable<int> LSReferenceId { get; set; }
        public string FileTitle { get; set; }
        public string FileNameVirtual { get; set; }
        public string FileNamePhysical { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public int FolderId { get; set; }
        public byte[] FileContent { get; set; }
        public string FileDescription { get; set; }
        public string Keywords { get; set; }
        public Nullable<int> Size { get; set; }
        public Nullable<int> CreatedByUserId { get; set; }
        public Nullable<System.DateTime> CreatedOnDate { get; set; }
        public Nullable<int> LastModifiedByUserId { get; set; }
        public Nullable<System.DateTime> LastModifiedOnDate { get; set; }
    
        public virtual DOC_tblDocumentReference DOC_tblDocumentReference { get; set; }
        public virtual ICollection<REC_tblCandidate> REC_tblCandidate { get; set; }
    }
}