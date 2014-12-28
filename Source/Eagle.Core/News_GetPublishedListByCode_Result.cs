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
    
    public partial class News_GetPublishedListByCode_Result
    {
        public int ApplicationId { get; set; }
        public int NewsId { get; set; }
        public System.Guid NewsCode { get; set; }
        public string LanguageCode { get; set; }
        public string CategoryCode { get; set; }
        public string Title { get; set; }
        public string Headline { get; set; }
        public string Alias { get; set; }
        public string Summary { get; set; }
        public Nullable<int> FrontImage { get; set; }
        public Nullable<int> MainImage { get; set; }
        public string MainText { get; set; }
        public string NavigateUrl { get; set; }
        public string Authors { get; set; }
        public Nullable<int> ListOrder { get; set; }
        public string Tags { get; set; }
        public string Source { get; set; }
        public Nullable<int> TotalRates { get; set; }
        public Nullable<int> TotalViews { get; set; }
        public Nullable<bool> IsPublishedToFeed { get; set; }
        public Nullable<bool> IsCommentShowed { get; set; }
        public Nullable<bool> IsRatingShowed { get; set; }
        public int Status { get; set; }
        public string PostedDate { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public Nullable<int> CreatedByUserId { get; set; }
        public Nullable<int> LastModifiedByUserId { get; set; }
        public string IPLog { get; set; }
        public string IPLastUpdated { get; set; }
        public string CategoryName { get; set; }
    }
}
