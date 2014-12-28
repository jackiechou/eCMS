using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.Common
{
    public class MethodPITModel
    {
        //<%= Html.DropDownListFor(x => x.MethodPITId, Model.MethodPITName) %>
        public int MethodPITId { get; set; }
        public string MethodPITName { get; set; }
    }

    public class MethodPITModelList
    {
        public List<MethodPITModel> MethodPITList { get; set; }
        public MethodPITModelList(int LanguageId)
        {
            if (LanguageId == 124)
            {
                MethodPITList = new List<MethodPITModel>()
                {
                    new MethodPITModel(){ MethodPITId = 1, MethodPITName = "According to progressive" },
                    new MethodPITModel(){ MethodPITId = 2, MethodPITName = "According to percent of salary" }
                };
            }
            else
            {
                MethodPITList = new List<MethodPITModel>()
                {
                    new MethodPITModel(){ MethodPITId = 1, MethodPITName = "Tính theo lũy tiến" },
                    new MethodPITModel(){ MethodPITId = 2, MethodPITName = "Tính theo % lương" }
                };
            }
        }

        //public static List<SelectListItem> MethodPITSelectList
        //{
        //    get
        //    {
        //        List<SelectListItem> lst = new List<SelectListItem>();

        //        foreach (MethodPITModel item in Enum.GetValues(typeof(MethodPITModel)))
        //        {
        //            lst.Add(new SelectListItem { Text = item.MethodPITName.ToString(), Value = item.MethodPITId.ToString(), Selected = false });
        //        }

        //        return lst;
        //    }
        //}
    }
}
