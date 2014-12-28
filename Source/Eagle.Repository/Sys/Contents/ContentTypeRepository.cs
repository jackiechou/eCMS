using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Repository.Sys.Contents
{
    public class ContentTypeRepository
    {
        public static SelectList PopulateContentTypesToDropDownList(string SelectedValue, bool IsShowSelectText = false)
        {

            using (EntityDataContext context = new EntityDataContext())
            {
                List<SelectListItem> lst = new List<SelectListItem>();
                lst = (from p in context.ContentTypes.AsEnumerable()
                       select new SelectListItem
                       {
                           Text = p.ContentTypeName,
                           Value = p.ContentTypeId.ToString()
                       }).ToList();

                if (lst.Count == 0)
                    lst.Insert(0, new SelectListItem() { Value = "-1", Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.None) });

                if (IsShowSelectText)
                    lst.Insert(0, new SelectListItem() { Value = "-1", Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.Select) });

                SelectList selectlist = new SelectList(lst, "Value", "Text", SelectedValue);
                return selectlist;
            }
        }
    }
}
