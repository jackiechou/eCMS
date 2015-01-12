using Eagle.Core;
using Eagle.Model;
using Eagle.Model.SYS.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Repository.SYS.Contents
{
    public class ContentItemRepository
    { 
        public EntityDataContext context { get; set; }

        public ContentItemRepository(EntityDataContext context)
        {
            this.context = context;
        }

        #region Tree ====================================================================================================
        public static List<ContentTreeModel> GetTreeList(int ContentTypeId, int ScopeTypeId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var lst = context.ContentItems.Where(m => m.ContentTypeId == ContentTypeId).OrderBy(m => m.ListOrder).ToList();
                if (ScopeTypeId == 1)
                    lst = lst.Where(c => c.ScopeTypeId == 1).ToList();
                else
                    lst = lst.Where(c => c.ScopeTypeId != 1).ToList();

                List<ContentTreeModel> list = lst.Select(m => new ContentTreeModel()
                {
                    id = m.ContentItemId,
                    key = m.ContentItemId,
                    parentId = m.ParentId,
                    name = m.ContentItemName,
                    text = m.ContentItemTitle,
                    title = m.ContentItemTitle,
                    tooltip = m.Content,
                    isParent = m.IsParent,
                    open = (m.ParentId == 0) ? true : false
                }).ToList();
                List<ContentTreeModel> recursiveObjects = RecursiveFillTree(list, null);
                return recursiveObjects;
            }
        }

        public static List<ContentTreeModel> RecursiveFillTree(List<ContentTreeModel> list, int? id)
        {
            List<ContentTreeModel> items = new List<ContentTreeModel>();
            List<ContentTreeModel> nodes = list.Where(m => m.parentId == id).Select(
               m => new ContentTreeModel
               {
                   id = m.id,
                   key = m.key,
                   parentId = m.parentId,
                   name = m.name,
                   text = m.text,
                   title = m.title,
                   tooltip = m.tooltip,
                   isParent = m.isParent,
                   open = m.open
               }).ToList();

            if (nodes.Count > 0)
            {
                foreach (var child in nodes)
                {
                    //child.children = RecursiveFillTree(list, child.id);
                    ContentTreeModel node = new ContentTreeModel()
                    {
                        id = child.id,
                        key = child.key,
                        parentId = child.parentId,
                        name = child.name,
                        text = child.text,
                        title = child.title,
                        tooltip = child.tooltip,
                        isParent = child.isParent,
                        open = child.open,
                        children = RecursiveFillTree(list, child.id)
                    };
                    items.Add(node);
                }
            }
            return items;
        }
        #endregion ==========================================================================================================================
    


        #region Content Items----------------------------------------------------------------------------------------------
        public static SelectList PopulateContentItemsByPageToDropDownList(string SelectedValue, bool IsShowSelectText = false)
        {           
            
            using (EntityDataContext context = new EntityDataContext())
            {
                List<SelectListItem> lst = new List<SelectListItem>();
                lst = (from p in context.ContentItems.AsEnumerable()
                       where p.ContentTypeId == (int)ContentTypeSetting.Page
                       select new SelectListItem
                       {
                           Text = p.ContentItemTitle,
                           Value = p.ContentItemId.ToString()
                       }).ToList();

                if (lst.Count == 0)
                    lst.Insert(0, new SelectListItem() { Value = "-1", Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.None) });

                if (IsShowSelectText)
                    lst.Insert(0, new SelectListItem() { Value = "-1", Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.Select) });

                SelectList selectlist = new SelectList(lst, "Value", "Text", SelectedValue);
                return selectlist;
            }
        }

        public static SelectList PopulateContentItemsByModuleToDropDownList(string SelectedValue, bool IsShowSelectText = false)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list = (from p in context.ContentItems.AsEnumerable()
                       where p.ContentTypeId == (int)ContentTypeSetting.Module
                       select new SelectListItem
                       {
                           Text = p.ContentItemTitle,
                           Value = p.ContentItemId.ToString()
                           
                       }).ToList();

                if (list.Count == 0)
                    list.Insert(0, new SelectListItem() { Value = "", Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.None) });
                
                if (IsShowSelectText)
                    list.Insert(0, new SelectListItem() { Value = "-1", Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.Select) });

                var selectlist = new SelectList(list, "Value", "Text", SelectedValue);
                return selectlist;
            }           
           
        }
        #endregion ------------------------------------------------------------------------------------------------------

        #region Bind DropdownList ===================================================================================
        // dùng cho bind dropdownlist
        public static List<AutoCompleteModel> GetContentDropDownListByPage(string searchTerm, int pageSize, int pageNum)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
                lst = context.ContentItems
                               .Where(c => c.Content.Contains(searchTerm) && c.ContentTypeId == (int)ContentTypeSetting.Page)
                               .Select(c => new AutoCompleteModel
                               {
                                   id = c.ContentItemId,
                                   name = c.ContentKey,
                                   text = c.Content
                               }).OrderBy(c => c.name)
                        .Skip(pageSize * (pageNum - 1))
                        .Take(pageSize)
                        .ToList();
                return lst;
            }
        }

        public static List<AutoCompleteModel> GetContentDropDownListByModule(string searchTerm, int pageSize, int pageNum)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
                lst = context.ContentItems
                               .Where(c => c.Content.Contains(searchTerm) && c.ContentTypeId == (int)ContentTypeSetting.Module)
                               .Select(c => new AutoCompleteModel
                               {
                                   id = c.ContentItemId,
                                   name = c.ContentKey,
                                   text = c.Content
                               }).OrderBy(c => c.name)
                        .Skip(pageSize * (pageNum - 1))
                        .Take(pageSize)
                        .ToList();
                return lst;
            }
        }
        #endregion ==================================================================================================
        public static bool IsIdExisted(int Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.ContentItems.FirstOrDefault(c => c.ContentItemId.Equals(Id));
                return (query != null);
            }
        }
        public static ContentItem Find(int Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.ContentItems.Find(Id);
            }
        }

        public static ContentItemViewModel Details(int ContentItemId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var entity = (from p in context.ContentItems
                       where p.ContentItemId == ContentItemId
                       select new ContentItemViewModel
                       {
                           PageId = p.PageId,
                           ModuleId = p.ModuleId,
                           ScopeTypeId = p.ScopeTypeId,
                           ContentTypeId = p.ContentTypeId,
                           ContentItemId = p.ContentItemId,
                           ContentItemName = p.ContentItemName,
                           ContentItemTitle = p.ContentItemTitle,
                           ContentKey = p.ContentKey,
                           Content = p.Content,
                           ParentId = p.ParentId,
                           Depth = p.Depth,
                           Lineage = p.Lineage,
                           ListOrder = p.ListOrder,
                           IsParent = p.IsParent,
                           IsActive = p.IsActive
                       }).FirstOrDefault();
                return entity;
            }
        }

        public static List<ContentItemViewModel> GetList(int ContentTypeId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var lst = (from c in context.ContentItems
                           join t in context.ContentTypes on c.ContentTypeId equals t.ContentTypeId into type
                           from t in type.DefaultIfEmpty()
                           join p in context.Pages on c.PageId equals p.PageId into pagelst
                           from pl in pagelst.DefaultIfEmpty()
                           join m in context.Modules on c.ModuleId equals m.ModuleId into modulelst
                           from ml in modulelst.DefaultIfEmpty()
                           where c.ContentTypeId == ContentTypeId
                           select new ContentItemViewModel  
                           {                       
                               PageId = c.PageId,
                               PageName = pl.PageName,
                               ModuleName = ml.ModuleName,
                               ModuleId = c.ModuleId,
                               ContentTypeId = c.ContentTypeId,
                               ContentItemId = c.ContentItemId,
                               Content =c.Content,
                               ContentKey = c.ContentKey,
                               IsActive = c.IsActive
                           }).OrderByDescending(c => c.ContentItemId).ToList();

                return lst;
            }
        }

        public static List<ContentItemViewModel> GetContentListByPage(int ScopeTypeId)
        {
            using(EntityDataContext context = new EntityDataContext()){
                List<ContentItemViewModel> lst = new List<ContentItemViewModel>();
                lst = (from p in context.ContentItems
                            where p.ContentTypeId == (int)ContentTypeSetting.Page
                            select new ContentItemViewModel
                            {
                                PageId = p.PageId,
                                ModuleId = p.ModuleId,
                                ContentTypeId = p.ContentTypeId,
                                ContentItemId = p.ContentItemId,
                                Content = p.Content,
                                ContentKey = p.ContentKey,
                                IsActive = p.IsActive
                            }).ToList();
                if (ScopeTypeId == 1)
                    lst = lst.Where(c => c.ScopeTypeId == 1).ToList();
                if (ScopeTypeId == 2 || ScopeTypeId == 3)
                    lst = lst.Where(c => c.ScopeTypeId == 2 || c.ScopeTypeId == 3).ToList();
                return lst;
            }
        }

        public static List<ContentItemViewModel> GetContentListByModule(int? ScopeTypeId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<ContentItemViewModel> lst = new List<ContentItemViewModel>();
                lst = (from p in context.ContentItems
                       where p.ContentTypeId == (int)ContentTypeSetting.Module
                       select new ContentItemViewModel
                       {
                           PageId = p.PageId,
                           ModuleId = p.ModuleId,
                           ContentTypeId = p.ContentTypeId,
                           ContentItemId = p.ContentItemId,
                           Content = p.Content,
                           ContentKey = p.ContentKey,
                           IsActive = p.IsActive
                       }).ToList();
                if (ScopeTypeId == 1)
                    lst = lst.Where(c => c.ScopeTypeId == 1).ToList();
                if (ScopeTypeId == 2 || ScopeTypeId == 3)
                    lst = lst.Where(c => c.ScopeTypeId == 2 || c.ScopeTypeId ==3).ToList();
                return lst;
            }
        }

      
        public static bool IsDataExisted(int ContentTypeId, string ContentKey, string Content)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.ContentItems.FirstOrDefault(c => c.ContentTypeId.Equals(ContentTypeId)
                    && c.ContentKey.Equals(ContentKey) && c.Content.ToUpper() == Content.ToUpper()
                    );
                return (query != null);
            }
        }

        public static bool Insert(ContentItemViewModel add_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDuplicate = IsDataExisted(add_model.ContentTypeId, add_model.ContentKey, add_model.Content);
                if (isDuplicate == false)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        ContentItem model = new ContentItem();
                        model.PageId = add_model.PageId;
                        model.ModuleId = add_model.ModuleId;
                        model.ContentTypeId = add_model.ContentTypeId;
                        model.ContentKey = add_model.ContentKey;
                        model.Content = add_model.Content;

                        int affectedRow = 0;
                        context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        affectedRow = context.SaveChanges();
                        if (affectedRow == 1)
                        {
                            add_model.ContentItemId = model.ContentItemId;
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

        public static bool InsertModuleContentItem(int ModuleId, string ModuleName, string Descriptiopn, out int? ContentItemId)
        {
            ContentItemId = 0;
            bool result = false;
            try
            {
                bool isDuplicate = IsDataExisted(2, ModuleName, Descriptiopn);
                if (isDuplicate == false)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        ContentItem model = new ContentItem();
                        model.ModuleId = ModuleId;
                        model.ContentTypeId = 2;
                        model.ContentKey = ModuleName;
                        model.Content = Descriptiopn;

                        int affectedRow = 0;
                        context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        affectedRow = context.SaveChanges();
                        if (affectedRow == 1)
                        {
                            ContentItemId = model.ContentItemId;
                            result = true;
                        }
                    }
                }              
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
            }
            return result;
        }

        public static bool InsertPageContentItem(int PageId, string PageName, string PageTitle, out int? ContentItemId)
        {
            ContentItemId = 0;
            bool result = false;
            try
            {
                bool isDuplicate = IsDataExisted(1, PageName, PageTitle);
                if (isDuplicate == false)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        ContentItem model = new ContentItem();
                        model.PageId = PageId;
                        model.ContentTypeId = 1;
                        model.ContentKey = PageName;
                        model.Content = PageTitle;

                        int affectedRow = 0;
                        context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        affectedRow = context.SaveChanges();
                        if (affectedRow == 1)
                        {
                            ContentItemId = model.ContentItemId;
                            result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
            }
            return result;
        }

        public static bool UpdateModuleContentItem(int ContentItemId, int ModuleId, string ModuleName, string ModuleTitle, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                ContentItem model = Find(ContentItemId);
                if (model != null)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        model.ContentItemId = ContentItemId;
                        model.ModuleId = ModuleId;
                        model.ContentTypeId = 2;
                        model.ContentKey = ModuleName;
                        model.Content = ModuleTitle;

                        context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        int affectedRows = context.SaveChanges();
                        if (affectedRows == 1)
                        {
                            Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                            result = true;
                        }
                    }
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

        public static bool UpdatePageContentItem(int ContentItemId, int PageId, string PageName, string PageTitle, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                ContentItem model = Find(ContentItemId);
                if (model != null)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        model.ContentItemId = ContentItemId;
                        model.PageId = PageId;
                        model.ContentTypeId = 1;
                        model.ContentKey = PageName;
                        model.Content = PageTitle;

                        context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        int affectedRows = context.SaveChanges();
                        if (affectedRows == 1)
                        {
                            Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                            result = true;
                        }
                    }
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



        public static bool Update(ContentItemViewModel edit_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                ContentItem model = Find(edit_model.ContentItemId);
                if (model != null)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        model.ContentItemId = edit_model.ContentItemId;
                        model.PageId = edit_model.PageId;
                        model.ModuleId = edit_model.ModuleId;
                        model.ContentTypeId = edit_model.ContentTypeId;
                        model.ContentKey = edit_model.ContentKey;
                        model.Content = edit_model.Content;

                        context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        int affectedRows = context.SaveChanges();
                        if (affectedRows == 1)
                        {
                            Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                            result = true;
                        }
                    }
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

        public static bool Delete(int id, out string message)
        {
            bool result = false;
            message = string.Empty;
            try
            {
                ContentItem model = Find(id);
                if (model != null)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        result = true;
                        message = Eagle.Resource.LanguageResource.DeleteSuccess;
                    }
                }
                else
                {
                    message = Eagle.Resource.LanguageResource.IDNoExistsErrorMessage;
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                message = Eagle.Resource.LanguageResource.DeleteFailure;
            }

            return result;
        }
    }
}
