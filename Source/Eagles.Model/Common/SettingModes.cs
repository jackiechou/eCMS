using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Model
{
    #region MENU ========================================================================================
    public static class HostMenuMode
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TopHostMenu")]
        public static int TopHostMenu = 1;

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BottomHostMenu")]
        public static int BottomHostMenu = 2;

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MainHostMenu")]
        public static int MainHostMenu = 3;

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LeftHostMenu")]
        public static int LeftHostMenu = 4;

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RightHostMenu")]
        public static int RightHostMenu = 5;
    }

    public static class AdminMenuMode
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TopAdminMenu")]
        public static int TopAdminMenu = 6;

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BottomAdminMenu")]
        public static int BottomAdminMenu = 7;

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MainAdminMenu")]
        public static int MainAdminMenu = 8;

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LeftAdminMenu")]
        public static int LeftAdminMenu = 9;

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RightAdminMenu")]
        public static int RightAdminMenu = 10;
    }

    public static class DesktopMenuMode
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TopDesktopMenu")]
        public static int TopDesktopMenu = 11;

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BottomDesktopMenu")]
        public static int BottomDesktopMenu = 12;

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MainDesktopMenu")]
        public static int MainDesktopMenu = 13;

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LeftDesktopMenu")]
        public static int LeftDesktopMenu = 14;

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RightDesktopMenu")]
        public static int RightDesktopMenu = 15;
    }
    #endregion MENU ======================================================================================
    
   
    #region SETTING MODES ===================================================================================
    public static class SettingModes
    {
       
        
    }

     #endregion SETTING MODES ================================================================================
}
