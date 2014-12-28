using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Model.SYS.Modules
{
    public class ModuleCategories
    {
        //@Html.DropDownListFor(x => x.ModuleTypeId, Eagle.Model.SYS.Modules.ModuleTypeViewModel.ModuleTypeList)
        public int ModuleTypeId { get; set; }
        public string ModuleTypeName { get; set; }
        public static IEnumerable<SelectListItem> ModuleCategorySelectList
        {
            get
            {
                return new SelectList(new[] 
                {
                    new SelectListItem{ Text = "Host", Value = "0" },
                    new SelectListItem{ Text = "Site", Value = "1" },      
                    new SelectListItem{ Text = "Desktop", Value = "2" }
                }, "ModuleTypeId", "ModuleTypeName");
            }
        }
    }
}
