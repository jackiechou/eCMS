using Eagle.Core;
using Eagle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Repository.Sys.Languages
{
    public class LanguageRepository
    {
        public EntityDataContext context { get; set; }

        public LanguageRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public List<Language> GetAllWithoutDefault()
        {
            return context.Languages.Where(p => p.LanguageCode != LanguageType.Vietnamese).ToList();
        }

        public List<Language> GetAll()
        {
            return context.Languages.ToList();
        }

        public List<LanguageViewModel> GetList()
        {
            return context.Languages.Select(x => new LanguageViewModel
            {
                LanguageId = x.LanguageId,
                LanguageCode = x.LanguageCode,
                LanguageName = x.LanguageName
            }).ToList();
        }


        public Language GetDetails(int LanguageId)
        {
            return context.Languages.Where(x => x.LanguageId == LanguageId).SingleOrDefault();
        }

        public Language GetDetailsByCode(string LanguageCode)
        {
            return context.Languages.Where(x => x.LanguageCode == LanguageCode).SingleOrDefault();
        }
        public static SelectList PopulateLanguages(string SelectedValue, bool IsShowSelectText = false)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Vietnamese, Value = "vi-VN" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.English, Value = "en-Us" });
            if (IsShowSelectText)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.Select), Value = "" });
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }

        public static SelectList PopulateActiveLanguages(string SelectedValue, bool IsShowSelectText = false)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<SelectListItem> lst = context.Languages.Where(l => l.LanguageStatus == true).Select(l=>new SelectListItem
                {
                    Text = l.LanguageName,
                    Value = l.LanguageCode
                }).ToList();
                lst.Add(new SelectListItem { Text = Resource.LanguageResource.Vietnamese, Value = "vi-VN" });
                lst.Add(new SelectListItem { Text = Resource.LanguageResource.English, Value = "en-Us" });
                if (IsShowSelectText)
                    lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.Select), Value = "" });
                return new SelectList(lst, "Value", "Text", SelectedValue);
            }
        }

        
    }
}
