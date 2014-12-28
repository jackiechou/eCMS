using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Model.Common
{

    #region TWO STATUS ===================================================================================
    public enum TwoStatusInt
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InActive")]
        InActive = 0,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Active")]
        Active = 1
    }

    public static class TwoStatusString
    {
        public static string InActive = "0";
        public static string Active = "1";
    }

    public static class TwoStatusBoolean
    {
        public static bool InActive = false;
        public static bool Active = true;
    }

    public class TwoStatusModel
    {
        //<%= Html.DropDownListFor(x => x.StatusId, Model.StatusList) %>
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public static IEnumerable<SelectListItem> StatusList
        {
            get
            {
                return new SelectList(new[] 
                {
                    new SelectListItem{ Text = "Inactive", Value = "0" },
                    new SelectListItem{ Text = "Active", Value = "1" }
                }, "StatusId", "StatusName");
            }
        }
    }

    #endregion ===========================================================================================

    #region THREE STATUS =======================================================================================
    public enum ThreeStatusMode : int
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InActive")]
        InActive = 0,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Active")]
        Active = 1,
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Published")]
        Published = 2
    }

    public static class ThreeStatusString
    {
        public static string InActive = "0";
        public static string Active = "1";
        public static string Published = "2";
    }  


    public class ThreeStatusModel
    {
        //<%= Html.DropDownListFor(x => x.StatusId, Model.StatusList) %>
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public static IEnumerable<SelectListItem> StatusList
        {
            get
            {
                return new SelectList(new[] 
                {
                    new SelectListItem{ Text = "None", Value = "" },
                    new SelectListItem{ Text = "Inactive", Value = "0" },
                    new SelectListItem{ Text = "Active", Value = "1" },      
                    new SelectListItem{ Text = "Published", Value = "2" }
                }, "StatusId", "StatusName");
            }
        }
    }
    #endregion STATUS =======================================================================================
}
