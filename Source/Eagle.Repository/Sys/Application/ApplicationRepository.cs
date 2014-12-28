using Eagle.Common.Entities;
using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Model.SYS.Application;
using Eagle.Repository.SYS.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Repository.SYS
{
    public class ApplicationRepository
    {  
        public EntityDataContext context { get; set; }
        public ApplicationRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public static string GetActiveApplicationLanguage(int ApplicationId)
        {
            // get Language
            string Language = "vi-VN";
            string tmpLanguage = GetApplicationDefaultLanguage(ApplicationId);
            if (!String.IsNullOrEmpty(tmpLanguage))
            {
                Language = tmpLanguage;
            }
            return Language;
        }

        public static string GetApplicationDefaultLanguage(int ApplicationId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                string CultureCode = (from p in context.Applications
                                      where p.ApplicationId == ApplicationId
                                      select p.DefaultLanguage).First();
                return CultureCode;
            }
        }

        public ApplicationInfo GetApplication(int ApplicationId)
        {
            string defaultLanguage = GetActiveApplicationLanguage(ApplicationId);
            return GetApplication(ApplicationId, defaultLanguage);
        }

        public ApplicationInfo GetApplication(int ApplicationId, string LanguageCode)
        {
            using (EntityDataContext db = new EntityDataContext())
            {
                var query = (from p in db.Applications
                             join l in db.ApplicationLanguages on p.ApplicationId equals (l.ApplicationId)
                             where p.ApplicationId == ApplicationId && l.LanguageCode == LanguageCode
                             select new ApplicationInfo
                             {                                
                                 ApplicationId = p.ApplicationId,
                                 ApplicationName = p.ApplicationName,
                                 ExpiryDate = p.ExpiryDate,
                                 Currency = p.Currency,
                                 DefaultLanguage = p.DefaultLanguage,
                                 TimeZoneOffset = p.TimeZoneOffset,
                                 HomeDirectory = p.HomeDirectory,
                                 Url = p.Url,
                                 LogoFile = p.LogoFile,
                                 BackgroundFile = p.BackgroundFile,
                                 KeyWords = p.KeyWords,
                                 CopyRight = p.CopyRight,
                                 FooterText = p.FooterText,
                                 Description = p.Description
                             }).SingleOrDefault();
                return query;
            }
        }

        public static string GetApplicationSetting(string settingName, int ApplicationId, string defaultValue)
        {
            string retValue = string.Empty;          
            using (EntityDataContext db = new EntityDataContext())
            {
                string setting = db.ApplicationSettings.Where(x => x.ApplicationId == ApplicationId && x.SettingName == settingName).Select(x => x.SettingValue).FirstOrDefault();                  
                if (string.IsNullOrEmpty(setting))
                    retValue = defaultValue;
                else
                    retValue = setting;
            }
            return retValue;
        }

        public bool IsExists(Guid ApplicationCode)
        {
            var query = context.Applications.Where(p => p.ApplicationCode.Equals(ApplicationCode)).FirstOrDefault();
            return (query != null);
        }

        public bool Insert(ApplicationInfo add_model, out string outMessage)
        {
            bool result = false;
            outMessage = string.Empty;
            try
            {
                bool isDuplicate = IsExists(add_model.ApplicationCode);
                if (isDuplicate == false)
                {
                    Eagle.Core.Application model = new Eagle.Core.Application();

                    model.ApplicationName = add_model.ApplicationName;
                    model.ExpiryDate = add_model.ExpiryDate;
                    model.Currency = add_model.Currency;
                    model.DefaultLanguage = add_model.DefaultLanguage;
                    model.TimeZoneOffset = add_model.TimeZoneOffset;
                    model.HomeDirectory = add_model.HomeDirectory;
                    model.Url = add_model.Url;
                    model.LogoFile = add_model.LogoFile;
                    model.BackgroundFile = add_model.BackgroundFile;
                    model.KeyWords = add_model.KeyWords;
                    model.CopyRight = add_model.CopyRight;
                    model.Description = add_model.Description;
                    model.FooterText = add_model.FooterText;

                    int affectedRow = 0;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if (affectedRow > 0)
                    {
                        result = true;
                        outMessage = model.ApplicationId.ToString();
                    }
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }

        public static Eagle.Core.Application Find(Guid ApplicationCode)
        {
            Eagle.Core.Application entity = new Eagle.Core.Application();
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    entity = (from p in context.Applications
                              where p.ApplicationCode == ApplicationCode
                              select p).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return entity;
        }

        public bool Update(ApplicationInfo edit_model, out string outMessage)
        {
            bool result = false;
            outMessage = string.Empty;
            try
            {
                Eagle.Core.Application entity = Find(edit_model.ApplicationCode);
                entity.ExpiryDate = edit_model.ExpiryDate;
                entity.Currency = edit_model.Currency;
                entity.DefaultLanguage = edit_model.DefaultLanguage;
                entity.TimeZoneOffset = edit_model.TimeZoneOffset;
                entity.HomeDirectory = edit_model.HomeDirectory;
                entity.Url = edit_model.Url;
                entity.LogoFile = edit_model.LogoFile;
                entity.BackgroundFile = edit_model.BackgroundFile;
                entity.KeyWords = edit_model.KeyWords;
                entity.CopyRight = edit_model.CopyRight;
                entity.Description = edit_model.Description;
                entity.FooterText = edit_model.FooterText;

                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                int affectedRow = context.SaveChanges();
                if (affectedRow > 0)
                {
                    result = true;
                    outMessage = entity.ApplicationId.ToString();
                }
            }
            catch (Exception ex)
            {
                outMessage = ex.Message;
            }
            return result;
        }
    }
}
