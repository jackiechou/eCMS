using Eagle.Common.Entities;
using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Repository.SYS
{
    public class ApplicationSettingRepository
    {
       
        public EntityDataContext context { get; set; }
        public ApplicationSettingRepository(EntityDataContext context)
        {
            this.context = context;
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

        //public string DefaultReportPageHeaderText
        //{
        //    get { return GetApplicationSetting("DefaultReportPageHeaderText", SettingModes.ApplicationCode, Null.NullString); }
        //}

        //public string DefaultReportPageFooterText
        //{
        //    get { return GetApplicationSetting("DefaultReportPageFooterText", SettingModes.ApplicationCode, Null.NullString); }
        //}

        //public string DefaultReportHeaderText
        //{
        //    get { return GetApplicationSetting("DefaultReportHeaderText", SettingModes.ApplicationCode, Null.NullString); }
        //}

        //public string DefaultReportLogo
        //{
        //    get { return GetApplicationSetting("DefaultReportLogo", SettingModes.ApplicationCode, Null.NullString); }
        //}

        public bool IsExists(int ApplicationSettingId)
        {
            var query = context.ApplicationSettings.Where(p => p.ApplicationSettingId.Equals(ApplicationSettingId)).FirstOrDefault();
            return (query != null);
        }

        public bool Insert(ApplicationSetting add_model, out string outMessage)
        {
            bool result = false;
            outMessage = string.Empty;
            try
            {
                bool isDuplicate = IsExists(add_model.ApplicationSettingId);
                if (isDuplicate == false)
                {
                    Eagle.Core.ApplicationSetting model = new Eagle.Core.ApplicationSetting();

                    model.ApplicationId = add_model.ApplicationId;
                    model.SettingName = add_model.SettingName;
                    model.SettingValue = add_model.SettingValue; 

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

        public static Eagle.Core.ApplicationSetting Find(int? ApplicationSettingId)
        {
            Eagle.Core.ApplicationSetting entity = new Eagle.Core.ApplicationSetting();
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    entity = (from p in context.ApplicationSettings
                              where p.ApplicationSettingId == ApplicationSettingId
                              select p).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return entity;
        }
        

        public bool Update(ApplicationSetting edit_model, out string outMessage)
        {
            bool result = false;
            outMessage = string.Empty;
            try
            {
                Eagle.Core.ApplicationSetting entity = Find(edit_model.ApplicationSettingId);
                entity.ApplicationId = edit_model.ApplicationId;
                entity.SettingName = edit_model.SettingName;
                entity.SettingValue = edit_model.SettingValue;

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
