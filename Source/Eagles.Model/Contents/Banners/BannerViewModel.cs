using Eagle.Model.SYS.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Model.Contents.Banners
{
    public class BannerSearchViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerName")]
        public string BannerName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerZoneId")]
        public int BannerZoneId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerStatus")]
        public string BannerStatus { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Advertiser")]
        public string Advertiser { get; set; }

    }

    public class BannerViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerId")]
        public int BannerId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerCode")]
        public System.Guid BannerCode { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerPositionId")]
        public int BannerPositionId { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerTypeId")]
         public int BannerTypeId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerPositionName")]
        public string BannerPositionName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LanguageCode")]
        public string LanguageCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerTitle")]
        public string BannerTitle { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerContent")]
        public string BannerContent { get; set; }        
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AltText")]
        public string AltText { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Attachment")]
        public Nullable<int> FileId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LinkToImage")]
        public string LinkToImage { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
        public string Description { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Advertiser")]
        public string Advertiser { get; set; }
        

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Tags")]
        public string Tags { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ListOrder")]
        public Nullable<int> ListOrder { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ClickThroughs")]
        public Nullable<int> ClickThroughs { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Width")]
        public Nullable<int> Width { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Height")]
        public Nullable<int> Height { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StartDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EndDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> EndDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Target")]
        public string Target { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public string Status { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedOnDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> CreatedOnDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastModifiedOnDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> LastModifiedOnDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedByUserId")]
        public Nullable<Guid> CreatedByUserId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastModifiedByUserId")]
        public Nullable<Guid> LastModifiedByUserId { get; set; }

        public List<PageBannerViewModel> PageBannerList { get; set; }

        public List<int?> SelectedPageIds { get; set; }

        public SelectList PageList { get; set; }
    }


    public class BannerPageViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerPageId")]
        public Guid BannerPageId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BannerCode")]
        public Guid BannerCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageCode")]
        public Guid PageCode { get; set; }

    }
}
