using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Model
{
    //public class CommentViewModel
    //{
    //    //Mail 
    //    public string path;
    //    public string PTypeReport;
    //    public string FromEmail;
    //    public string ToEmail;
    //    public string CCEmail;
    //    public string Subject;
    //    public string Body;

    //    /// <summary>
    //    /// Common
    //    /// </summary>

    //    public long? Invoice { get; set; }
    //    public string Title { get; set; }
    //    public int? DialogConfirm { get; set; }
    //    public int? InvoiceStatus { get; set; }
    //    public int Status { get; set; }
    //    public int PType { get; set; }
    //    public bool IsGetData { get; set; }
    //    public bool isReminderScreen { get; set; }
    //    public int? Check_Status { get; set; }
    //    public string msgDelete { get; set; }

    //    public int? BranchInvoice { get; set; }
    //    public int? BranchRequest { get; set; }
    //    public int? RoleIdRequest { get; set; }
    //    public string UserRequest { get; set; }
    //    public int? InvoiceDate { get; set; }

    //    public string Comment { get; set; }
    //    public long? TrackId { get; set; }
    //    public string MsgTab3 { get; set; }

    //    List<CommentDetailViewModel> _lstDetail;
    //    public List<CommentDetailViewModel> lstDetail
    //    {
    //        get
    //        {
    //            if (_lstDetail == null) _lstDetail = new List<CommentDetailViewModel>();
    //            return _lstDetail;
    //        }
    //        set { _lstDetail = value; }
    //    }
    //}
    //public class CommentDetailViewModel
    //{
    //    public long Invoice { get; set; }
    //    public string StandardTime { get; set; }
    //    public string UserCreate { get; set; }
    //    public string Comment { get; set; }
    //    public string CodeName { get; set; }

    //    public string FullDateUK { get; set; }
    //    public string UserRequest { get; set; }
    //    public string UserExecute { get; set; }
    //    public string DesVn { get; set; }
    //    public string NameStatus { get; set; }
    //    public int BranchId { get; set; }
    //    public long TrackId { get; set; }
    //}

    public static class LanguageType
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Vietnamese")]
        public static string Vietnamese = "vi-VN";

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "English")]
        public static string English = "en-US";      
    }

    public static class ScopeTypeStatuse
    {
        public static int Host = 1;
        public static int Admin = 2;
        public static int Site = 3;
    }

    public enum ScopeType : int
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Host")]
        Host = 1,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Admin")]
        Admin = 2,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Site")]
        Site = 3
    }

    public enum IsSecured
    { 
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsDesktop")]
        IsDesktop = 0,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsAdmin")]
        IsAdmin = 1
    }

    public class DropdownListItem
    {
        public string key { get; set; }
        public string value { get; set; }
        public string desc { get; set; }
    }

    public class HashMap
    {
        public int? Key { get; set; }
        public string Value { get; set; }
    }

    public class MenuDropdownListViewModel
    {
        public int ViewMenuId { get; set; }
        public string ViewMenuName { get; set; }
    }

    // Tree JQgrid
    public class TreeGridViewModel
    {
        public string tree_level { get; set; }
        public string tree_parent { get; set; }
        public string tree_isLeaf { get; set; }
        public string tree_expanded { get; set; }
        public string tree_loaded { get; set; }
    }
    
    public class SiteMapModel
    {
        public int Level { get; set; }
        public string Page_Url { get; set; }
        public string Page_Name { get; set; }
        public string Icon { get; set; }
    }

    public class CommonViewModel
    {
        

    }
       
    public class SelectListItemCustom : SelectListItem
    {
        public IDictionary<string, object> itemsHtmlAttributes { get; set; }
    }

    public class LayOutViewModel
    {
        public long ID { get; set; }
        public string PivotName { get; set; }
        public string UserName { get; set; }
        public string URL { get; set; }
        public string LayOutString { get; set; }

        public string LayoutReport { get; set; }
        public int? Tab { get; set; }
    }

    public enum Color
    {
        Red,
        Green,
        Blue,
        BrightRed,
        BrightGreen,
        BrightBlue,
    }
    public enum WeekDay
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    public class WeeklyEvent
    {
        public string Title { get; set; }
        public WeekDay Day { get; set; }
        public WeekDay? AnotherDay { get; set; }
    }
}