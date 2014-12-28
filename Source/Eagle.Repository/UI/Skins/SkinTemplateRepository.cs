﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Eagle.Core;
using Eagle.Model.UI.Skins;

namespace CommonLibrary.Modules.Dashboard.Components.Skins
{
    public class SkinPackageTemplates
    {
        public static List<SkinPackageTemplate> GetListBySkinId(int SkinId)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var lst = (from sc in dbContext.SkinPackageTemplates
                            join s in dbContext.Skins
                            on sc.SkinPackageId equals s.SkinPackageId
                            where s.SkinId == SkinId
                           select sc).ToList();
                return lst;
            }
        }

        public static List<SkinPackageTemplate> GetListBySkinPackageId(int SkinPackageId)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = from sc in dbContext.SkinPackageTemplates
                            select sc;
                if (SkinPackageId > 0)
                    query = query.Where(x => x.SkinPackageId == SkinPackageId);
                return query.ToList();
            }
        }

        public static SkinPackageTemplate GetDetail(int TemplateId)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                SkinPackageTemplate query = dbContext.SkinPackageTemplates.Where(x => x.TemplateId == TemplateId).FirstOrDefault();
                return query;
            }
        }

        public static int Insert(int SkinPackageId, string TemplateName, string TemplateSrc)
        {
            int result = 0;
            List<SkinPackageTemplate> lst = GetListBySkinPackageId(SkinPackageId);
            bool CheckExists = false;
            foreach (var x in lst)
            {
                if (x.TemplateSrc == TemplateSrc)
                {
                    CheckExists = true;
                    break;
                }
            }
            if (CheckExists == false)
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    SkinPackageTemplate entity = new SkinPackageTemplate();
                    entity.SkinPackageId = SkinPackageId;
                    entity.TemplateName = TemplateName;
                    entity.TemplateSrc = TemplateSrc;
                    context.SkinPackageTemplates.Add(entity);
                    result = context.SaveChanges();
                }
            }
            else
                result = - 3;
            return result;
        }

        public static int Update(int TemplateId, int SkinPackageId, string TemplateName, string TemplateSrc, string LastModifiedByUserId)
        {
            System.Guid userid = new Guid(LastModifiedByUserId);
            int result = 0;
            List<SkinPackageTemplate> lst = GetListBySkinPackageId(SkinPackageId);
            bool CheckExists = false;
            foreach (var x in lst)
            {
                if (x.TemplateSrc == TemplateSrc)
                {
                    CheckExists = true;
                    break;
                }
            }
            if (CheckExists == false)
            {
                using (EntityDataContext dbContext = new EntityDataContext())
                {
                    SkinPackageTemplate entity = dbContext.SkinPackageTemplates.Single(x => x.TemplateId == TemplateId);
                    entity.SkinPackageId = SkinPackageId;
                    entity.TemplateName = TemplateName;
                    entity.TemplateSrc = TemplateSrc;
                    result = dbContext.SaveChanges();
                }
            }
            else
                result = -3;
            return result;
        }

        public static int Delete(int TemplateId)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                SkinPackageTemplate entity = dbContext.SkinPackageTemplates.Single(x => x.TemplateId == TemplateId);
                dbContext.SkinPackageTemplates.Remove(entity);
                int i = dbContext.SaveChanges();
                return i;
            }
        } 
    }
}