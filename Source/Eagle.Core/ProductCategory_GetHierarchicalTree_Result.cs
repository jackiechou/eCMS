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
    
    public partial class ProductCategory_GetHierarchicalTree_Result
    {
        public int Category_Id { get; set; }
        public string Category_Code { get; set; }
        public string Category_Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Depth { get; set; }
        public string Lineage { get; set; }
        public int Parent_Id { get; set; }
    }
}
