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
    
    public partial class Gallery_Topics
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public string Alias { get; set; }
        public int ParentId { get; set; }
        public string Depth { get; set; }
        public string Lineage { get; set; }
        public Nullable<int> SortKey { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> PostedDate { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> UserLog { get; set; }
        public Nullable<int> UserLastUpdate { get; set; }
        public string IPLog { get; set; }
        public string IPLastUpdate { get; set; }
        public string Status { get; set; }
    }
}
