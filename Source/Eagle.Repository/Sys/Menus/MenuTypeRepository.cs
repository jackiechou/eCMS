using Eagle.Core;
using Eagle.Model.SYS.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Repository.Sys.Menus
{
    public class MenuTypeRepository
    { 
        public EntityDataContext context { get; set; }

        public MenuTypeRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public static MenuType Find(int Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.MenuTypes.Find(Id);
            }
        }

        public static SelectList PopulateActiveMenuTypeSelectList(int ScopeTypeId, string LanguageCode, string SelectedValue, bool IsShowSelectText = false)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<MenuTypeViewModel> lst = GetActiveList(ScopeTypeId, LanguageCode);
            if (lst.Count == 0)
                list.Insert(0, new SelectListItem() { Value = "", Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.None) });
            else
            {
                list = lst.Select(m => new SelectListItem()
                {
                    Text = m.MenuTypeName,
                    Value = m.MenuTypeId.ToString(),
                }).ToList();
                if (IsShowSelectText)
                    list.Insert(0, new SelectListItem() { Value = "", Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.Select) });
            }            
            var selectList = new SelectList(list, "Value", "Text", SelectedValue);
            return selectList;
        }

        public static List<MenuTypeViewModel> GetActiveList(int ScopeTypeId, string LanguageCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<MenuTypeViewModel> lst = (from p in context.MenuTypes
                       where p.ScopeTypeId == ScopeTypeId && p.LanguageCode == LanguageCode && p.MenuTypeStatus == true
                       select new MenuTypeViewModel
                       {
                           ApplicationId = p.ApplicationId,
                           ScopeTypeId = p.ScopeTypeId,
                           LanguageCode = p.LanguageCode,
                           MenuTypeId = p.MenuTypeId,
                           MenuTypeCode = p.MenuTypeCode,
                           MenuTypeName = p.MenuTypeName,
                           Description = p.Description,
                           ListOrder = p.ListOrder,
                           IsAdmin = p.IsAdmin,
                           MenuTypeStatus = p.MenuTypeStatus,
                           PostedDate = p.PostedDate,
                           LastUpdatedDate = p.LastUpdatedDate
                       }).ToList();
                if(lst.Count == 0)
                    lst = new List<MenuTypeViewModel>();
                return lst;
            }
        }

        public static List<MenuTypeViewModel> GetList(int ScopeTypeId, bool? Status, string LanguageCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<MenuTypeViewModel> lst = new List<MenuTypeViewModel>();
                lst = (from p in context.MenuTypes
                       where p.ScopeTypeId == ScopeTypeId
                       select new MenuTypeViewModel
                       {
                           ApplicationId = p.ApplicationId,
                           ScopeTypeId = p.ScopeTypeId,
                           LanguageCode = p.LanguageCode,
                           MenuTypeId = p.MenuTypeId,
                           MenuTypeCode = p.MenuTypeCode,
                           MenuTypeName = p.MenuTypeName,
                           Description = p.Description,
                           ListOrder = p.ListOrder,
                           IsAdmin = p.IsAdmin,
                           MenuTypeStatus = p.MenuTypeStatus,
                           PostedDate = p.PostedDate,
                           LastUpdatedDate = p.LastUpdatedDate
                       }).ToList();

                if (LanguageCode != null && LanguageCode !=string.Empty)
                    lst = lst.Where(p => p.LanguageCode == LanguageCode).ToList();
                if (Status != null)
                    lst = lst.Where(p => p.MenuTypeStatus == Status).ToList();
                return lst;
            }
        }

        public static MenuTypeViewModel GetDetails(int Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    MenuTypeViewModel entity = (from p in context.MenuTypes
                              where p.MenuTypeId == Id
                              select new MenuTypeViewModel
                              {
                                  ApplicationId = p.ApplicationId,
                                  ScopeTypeId = p.ScopeTypeId,
                                  LanguageCode = p.LanguageCode,
                                  MenuTypeId = p.MenuTypeId,
                                  MenuTypeCode = p.MenuTypeCode,
                                  MenuTypeName = p.MenuTypeName,
                                  Description = p.Description,
                                  ListOrder = p.ListOrder,
                                  IsAdmin = p.IsAdmin,
                                  MenuTypeStatus = p.MenuTypeStatus,
                                  PostedDate = p.PostedDate,
                                  LastUpdatedDate = p.LastUpdatedDate
                              }).FirstOrDefault();

                    return entity;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return new MenuTypeViewModel();
                }
            }
        }

        public static bool IsIdExisted(int Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.MenuTypes.FirstOrDefault(c => c.MenuTypeId.Equals(Id));
                return (query != null);
            }
        }

        public bool IsDataExisted(string MenuTypeName, string LanguageCode, int ScopeTypeId)
        {
            var query = context.MenuTypes.FirstOrDefault(c => c.MenuTypeName.ToUpper().Equals(MenuTypeName.ToUpper())
                && c.LanguageCode == LanguageCode && c.ScopeTypeId == ScopeTypeId);
            return (query != null);
        }

        public bool Add(MenuTypeViewModel add_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDuplicate = IsDataExisted(add_model.MenuTypeName, add_model.LanguageCode, add_model.ScopeTypeId);
                if (isDuplicate == false)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        MenuType model = new MenuType();
                        model.ApplicationId = add_model.ApplicationId;
                        model.ScopeTypeId = add_model.ScopeTypeId;
                        model.LanguageCode = add_model.LanguageCode;
                        //model.MenuTypeCode = add_model.MenuTypeCode;
                        model.MenuTypeName = add_model.MenuTypeName;
                        model.Description = add_model.Description;
                        model.ListOrder = add_model.ListOrder;
                        model.IsAdmin = add_model.IsAdmin;
                        model.MenuTypeStatus = add_model.MenuTypeStatus;
                        model.PostedDate = add_model.PostedDate;
                        model.LastUpdatedDate = add_model.LastUpdatedDate;

                        int affectedRow = 0;
                        context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        affectedRow = context.SaveChanges();
                        if (affectedRow == 1)
                        {
                            add_model.MenuTypeId = model.MenuTypeId;
                            Message = Eagle.Resource.LanguageResource.CreateSuccess;
                            result = true;
                        }
                    }
                }
                else
                {
                    result = false;
                    Message = Eagle.Resource.LanguageResource.DuplicateError;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return result;
        }

        public bool Update(MenuTypeViewModel edit_model, out string Message)
        {
            Message = string.Empty;
            bool Result = false;
            try
            {
                MenuType model = Find(edit_model.MenuTypeId);
                if (model != null)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        model.ApplicationId = edit_model.ApplicationId;
                        model.ScopeTypeId = edit_model.ScopeTypeId;
                        model.LanguageCode = edit_model.LanguageCode;
                        //model.MenuTypeCode = edit_model.MenuTypeCode;
                        model.MenuTypeName = edit_model.MenuTypeName;
                        model.Description = edit_model.Description;
                        model.ListOrder = edit_model.ListOrder;
                        model.IsAdmin = edit_model.IsAdmin;
                        model.MenuTypeStatus = edit_model.MenuTypeStatus;
                        model.PostedDate = edit_model.PostedDate;
                        model.LastUpdatedDate = edit_model.LastUpdatedDate;

                        context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        int affectedRows = context.SaveChanges();
                        if (affectedRows == 1)
                        {
                            Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                            Result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Result = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return Result;
        }

        public static bool Delete(int Id, out string Message)
        {
            bool Result = false;
            Message = string.Empty;
            try
            {
                MenuType model = Find(Id);
                if (model != null)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        Result = true;
                        Message = Eagle.Resource.LanguageResource.DeleteSuccess;
                    }
                }
                else
                {
                    Message = Eagle.Resource.LanguageResource.IdExisted;
                    Result = false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Result = false;
                Message = Eagle.Resource.LanguageResource.DeleteFailure;
            }

            return Result;
        }
    }
}
