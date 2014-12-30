using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Menu
{
    public class MenuTreeModel
    {
        public int id { get; set; }

        public int? parentid { get; set; }

        public string text { get; set; }

        public Guid value { get; set; }

        public List<MenuTreeModel> children { get; set; }

        public MenuTreeModel()
        {
            children = new List<MenuTreeModel>();
        }

    }

    public class MenuModel
    {
        public int MenuId { set; get; }
        public int? ParentId { set; get; }
        public Guid MenuCode { set; get; }
        public string MenuName { set; get; }
        public IList<MenuModel> MenuList { set; get; }
        public MenuModel()
        {
            MenuList = new List<MenuModel>();
        }
    }
    public class MenuViewModel
    {       

        //Original ===============================================================================
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PageId")]
        public Nullable<int> PageId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MenuTypeId")]
        public int MenuTypeId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScopeTypeId")]
        public int ScopeTypeId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MenuId")]
        public int MenuId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MenuCode")]
        public System.Guid MenuCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ParentId")]
        public int? ParentId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Depth")]
        public int Depth { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ListOrder")]
        public int ListOrder { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MenuName")]
        public string MenuName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MenuTitle")]
        public string MenuTitle { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MenuAlias")]
        public string MenuAlias { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Target")]
        public string Target { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IconFile")]
        public Nullable<int> IconFile { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IconClass")]
        public string IconClass { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Color")]
        public string Color { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CssClass")]
        public string CssClass { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public int MenuStatus { get; set; }

        public bool HasChild { get; set; }

        //Modified ====================================================================================
        public string IconUrl { get; set; }
        public Nullable<bool> IsExtenalLink { get; set; }
        public string PageUrl { get; set; }
        public string PagePath { get; set; }
        public List<MenuViewModel> MenuChildren { get; set; }
        public List<MenuTypeViewModel> MenuTypeList { get; set; }
    }
}
