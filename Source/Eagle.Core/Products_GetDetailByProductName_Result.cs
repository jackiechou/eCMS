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
    
    public partial class Products_GetDetailByProductName_Result
    {
        public int Product_Id { get; set; }
        public System.Guid Product_No { get; set; }
        public string Category_Code { get; set; }
        public string Product_Name { get; set; }
        public string Alias { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public Nullable<short> UnitsOnOrder { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string PhotoFileName { get; set; }
        public string ThumbnailPhotoFileName { get; set; }
        public string Specification { get; set; }
        public Nullable<int> ReorderLevel { get; set; }
        public Nullable<byte> Discontinued { get; set; }
        public Nullable<System.DateTime> CreatedOnDate { get; set; }
        public Nullable<System.DateTime> LastModifiedOnDate { get; set; }
        public string Category_Name { get; set; }
    }
}
