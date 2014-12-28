using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eagle.Repository
{
    public enum Gender
    {
        Male = 0,
        Female = 1,
        NoneSpecified = -1
    }

    public static class GenderClass
    {
       
        public static List<SelectListItem> GenderSelectList
        {
            get
            {
                List<SelectListItem> genders = new List<SelectListItem>();

                foreach (Gender gender in Enum.GetValues(typeof(Gender)))
                {
                    genders.Add(new SelectListItem { Text = gender.ToString(), Value = gender.ToString("D"), Selected = false });
                }

                return genders;
            }
        }
        public static Dictionary<int?, string> GetGenderList()
        {
            Dictionary<int?, string> ret = new Dictionary<int?, string>();            
            ret.Add(0, Eagle.Resource.LanguageResource.Male);
            ret.Add(1, Eagle.Resource.LanguageResource.Female);
            ret.Add(-1, Eagle.Resource.LanguageResource.NonSpecified);
            return ret;
        }

        //public static List<SelectListItem> GetList(int? selectedValue, string optionLabel, bool HasOptionLabel = false)
        //{
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    if(HasOptionLabel)            
        //        items.Add(new SelectListItem { Selected = true, Value = "-1", Text = string.Format("-- {0} --", optionLabel) });

        //    if (string.IsNullOrEmpty(optionLabel))
        //        optionLabel = Eagle.Resource.LanguageResource.NoneSpecified;

        //    Dictionary<int?, string> dict = new Dictionary<int?, string>();
        //    dict.Add(0, Eagle.Resource.LanguageResource.Male);
        //    dict.Add(1, Eagle.Resource.LanguageResource.Female);

        //    foreach (var item in dict)
        //    {
        //        items.Add(new SelectListItem
        //        {
        //            Text = item.Value,
        //            Value = item.Key.ToString(),
        //            Selected = selectedValue == item.Key ? true : false
        //        });
        //    }
        //    return items.OrderBy(l => l.Text).ToList();
        //}

        //List<Org>() parentOrganisations = // fetch here
        //ViewBag.Organisations = parentOrganisations.ToSelectList(org => org.ID.ToString(),org => org.OrganisationName, "-1", "-- None -- ", true);
        //public static List<SelectListItem> ToSelectList<T>(List<T> Items, Func<T, string> getKey, Func<T, string> getValue, string selectedValue, string optionLabel, bool HasOptionLabel = false)
        //{
        //    List<SelectListItem> items = new List<SelectListItem>();

        //    if (HasOptionLabel)
        //        items.Add(new SelectListItem { Selected = true, Value = "-1", Text = string.Format("-- {0} --", optionLabel) });

        //    foreach (var item in Items)
        //    {
        //        items.Add(new SelectListItem
        //        {
        //            Text = getKey(item),
        //            Value = getValue(item),
        //            Selected = selectedValue == getValue(item) ? true : false
        //        });
        //    }

        //    return items
        //        .OrderBy(l => l.Text)
        //        .ToList();
        //}
    }
}
