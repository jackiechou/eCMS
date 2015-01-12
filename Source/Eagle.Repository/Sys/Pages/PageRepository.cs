using Eagle.Common;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model;
using Eagle.Repository.Common;
using Eagle.Model.Common;
using Eagle.Model.SYS.Pages;
using System.Web.Mvc;
using System.Data.Objects.SqlClient;
using Eagle.Common.Utilities;

namespace Eagle.Repository.SYS
{
    public class PageRepository
    {
        #region Variable and Construction====================================================================================================
        public EntityDataContext context { get; set; }
        public PageRepository(EntityDataContext context)
        {
            this.context = context;           
        }
        #endregion ==========================================================================================================================

      
        #region Load Data====================================================================================================================

        public static SelectList PopulatePageSelectList(int ScopeTypeId, bool? IsSecured, string SelectedValue = null, bool IsShowSelectText = false)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.Pages.Where(p => p.ScopeTypeId == ScopeTypeId);
                if (IsSecured != null && IsSecured == true)
                    query = query.Where(p=> p.IsSecured == true);

                List<SelectListItem> list = query.AsEnumerable().Select(p => new SelectListItem()
                {
                    Text = p.PageTitle,
                    Value = p.PageId.ToString()
                }).ToList();                

                if (IsShowSelectText)
                    list.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.NoneSpecified), Value = "" });
                return new SelectList(list, "Value", "Text", SelectedValue);
            }
        }

        public static SelectList PopulatePageSelectList(bool IsSecured, string SelectedValue, bool IsShowSelectText = false)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list = (from p in context.Pages.AsEnumerable()
                        where p.IsSecured == IsSecured
                        select new SelectListItem
                        {
                            Text = p.PageTitle,
                            Value = p.PageId.ToString()

                        }).ToList();

                if (list.Count == 0)
                    list.Insert(0, new SelectListItem() { Value = "", Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.None) });

                if (IsShowSelectText)
                    list.Insert(0, new SelectListItem() { Value = "-1", Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.Select) });

                var selectlist = new SelectList(list, "Value", "Text", SelectedValue);
                return selectlist;
            }
        }
        public static SelectList PopulateActivePageSelectList(int ScopeTypeId, string LanguageCode, string SelectedValue = null, bool IsShowSelectText = false)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<SelectListItem> list = (context.Pages.AsEnumerable().Where(p => p.ScopeTypeId == ScopeTypeId && p.IsVisible == true && p.LanguageCode == LanguageCode).Select(p => new SelectListItem()
                {
                    Text = p.PageTitle,
                    Value = p.PageId.ToString()
                })).ToList();

                if (IsShowSelectText)
                    list.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.NoneSpecified), Value = "" });
                return new SelectList(list, "Value", "Text", SelectedValue);
            }
        }
        public static SelectList PopulateSelecuredListByScopeTypeIdAndStatus(int ScopeTypeId, string SelectedValue, bool IsShowSelectText = false)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<SelectListItem> list = (context.Pages.AsEnumerable().Where(p => p.IsSecured == true && p.ScopeTypeId == ScopeTypeId).Select(p => new SelectListItem()
                {
                    Text = p.PageTitle,
                    Value = p.PageId.ToString()
                })).ToList();

                if (IsShowSelectText)
                    list.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.All), Value = "" });
                return new SelectList(list, "Value", "Text", SelectedValue);
            }

        }
        public static List<PageViewModel> GetListByKeywordsScopeTypeIdAndStatus(string Keywords, int ScopeTypeId, bool? Status)
        {
            using (EntityDataContext context = new EntityDataContext())
            {

                string strCommand = @"EXEC Cms.Pages_GetListByScopeTypeIdAndStatus @Keywords={0},@ScopeTypeId = {1}, @IsVisible = {2}";
                List<PageViewModel> lst = context.Database.SqlQuery<PageViewModel>(strCommand, Keywords, ScopeTypeId, Status).ToList();
                return lst;
            }
        }
        public static List<PageViewModel> GetListByScopeTypeIdAndIsSecured(int ScopeTypeId, bool? IsSecured)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<PageViewModel> lst = new List<PageViewModel>();
                lst = (from p in context.Pages
                       where (p.IsSecured == IsSecured || IsSecured == null)
                       orderby p.ListOrder ascending
                       select new PageViewModel
                       {
                           ApplicationId = p.ApplicationId,
                           ContentItemId = p.ContentItemId,
                           TemplateId = p.TemplateId,
                           ScopeTypeId = p.ScopeTypeId,
                           LanguageCode = p.LanguageCode,
                           PageId = p.PageId,
                           PageCode = p.PageCode,
                           PageName = p.PageName,
                           PageTitle = p.PageTitle,
                           PageUrl = p.PageUrl,
                           PagePath = p.PagePath,
                           ListOrder = p.ListOrder,
                           DisableLink = p.DisableLink,
                           DisplayTitle = p.DisplayTitle,
                           Description = p.Description,
                           Keywords = p.Keywords,
                           IsDeleted = p.IsDeleted,
                           IsExtenalLink = p.IsExtenalLink,
                           IsMenu = p.IsMenu,
                           IsSecured = p.IsSecured,
                           IsVisible = p.IsVisible,
                           StartDate = p.StartDate,
                           EndDate = p.EndDate,
                           PageHeadText = p.PageHeadText,
                           PageFooterText = p.PageFooterText,
                           Icon = p.Icon,
                           CreatedByUserId = p.CreatedByUserId,
                           CreatedOnDate = p.CreatedOnDate,
                           LastModifiedByUserId = p.LastModifiedByUserId,
                           LastModifiedOnDate = p.LastModifiedOnDate
                       }).ToList();
                if (ScopeTypeId == 2 || ScopeTypeId == 3)
                    lst = lst.Where(p => p.ScopeTypeId == ScopeTypeId).ToList();
                return lst;
            }
        }
        public static List<PageViewModel> GetListByScopeTypeId(int ScopeTypeId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<PageViewModel> lst = new List<PageViewModel>();
                lst = (from p in context.Pages
                       select new PageViewModel
                       {
                           ApplicationId = p.ApplicationId,
                           ContentItemId = p.ContentItemId,
                           TemplateId = p.TemplateId,
                           ScopeTypeId = p.ScopeTypeId,
                           LanguageCode = p.LanguageCode,
                           PageId = p.PageId,
                           PageCode = p.PageCode,
                           PageName = p.PageName,
                           PageTitle = p.PageTitle,
                           PageUrl = p.PageUrl,
                           PagePath = p.PagePath,
                           ListOrder = p.ListOrder,
                           DisableLink = p.DisableLink,
                           DisplayTitle = p.DisplayTitle,
                           Description = p.Description,
                           Keywords = p.Keywords,
                           IsDeleted = p.IsDeleted,
                           IsExtenalLink = p.IsExtenalLink,
                           IsMenu = p.IsMenu,
                           IsSecured = p.IsSecured,
                           IsVisible = p.IsVisible,
                           StartDate = p.StartDate,
                           EndDate = p.EndDate,
                           PageHeadText = p.PageHeadText,
                           PageFooterText = p.PageFooterText,
                           Icon = p.Icon,
                           CreatedByUserId = p.CreatedByUserId,
                           CreatedOnDate = p.CreatedOnDate,
                           LastModifiedByUserId = p.LastModifiedByUserId,
                           LastModifiedOnDate = p.LastModifiedOnDate
                       }).ToList();
                if (ScopeTypeId == 2 || ScopeTypeId == 3)
                    lst = lst.Where(p => p.ScopeTypeId == ScopeTypeId).ToList();
                return lst;
            }
        }  

        public static Page Find(int Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.Pages.Find(Id);
            }
        }

        public static PageViewModel GetDetails(int Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var entity = (from p in context.Pages
                              join c in context.ContentItems on p.PageId equals c.PageId into PageContentList
                              from pc in PageContentList.DefaultIfEmpty()
                              where p.PageId == Id
                              select new PageViewModel
                              {
                                  ApplicationId = p.ApplicationId,
                                  ContentItemId = pc.ContentItemId,
                                  TemplateId = p.TemplateId,
                                  ScopeTypeId = p.ScopeTypeId,
                                  LanguageCode = p.LanguageCode,
                                  PageId = p.PageId,
                                  PageCode = p.PageCode,
                                  PageName = p.PageName,
                                  PageTitle = p.PageTitle,
                                  PageUrl = p.PageUrl,
                                  PagePath = p.PagePath,
                                  ListOrder = p.ListOrder,
                                  DisableLink = p.DisableLink,
                                  DisplayTitle = p.DisplayTitle,
                                  Description = p.Description,
                                  Keywords = p.Keywords,
                                  IsDeleted = p.IsDeleted,
                                  IsExtenalLink = p.IsExtenalLink,
                                  IsMenu = p.IsMenu,
                                  IsSecured = p.IsSecured,
                                  IsVisible = p.IsVisible,
                                  StartDate = p.StartDate,
                                  EndDate = p.EndDate,
                                  PageHeadText = p.PageHeadText,
                                  PageFooterText = p.PageFooterText,
                                  Icon = p.Icon,
                                  CreatedByUserId = p.CreatedByUserId,
                                  CreatedOnDate = p.CreatedOnDate,
                                  LastModifiedByUserId = p.LastModifiedByUserId,
                                  LastModifiedOnDate = p.LastModifiedOnDate
                              }).FirstOrDefault();
                return entity;
            }
        }
        public static List<PageViewModel> GetSecuredList(bool IsSecured, bool? Status)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<PageViewModel> lst = new List<PageViewModel>();
                lst = (from p in context.Pages
                       where p.IsSecured == IsSecured && p.IsVisible == Status
                       select new PageViewModel
                       {
                           ApplicationId = p.ApplicationId,
                           ContentItemId = p.ContentItemId,
                           TemplateId = p.TemplateId,
                           ScopeTypeId = p.ScopeTypeId,
                           LanguageCode = p.LanguageCode,
                           PageId = p.PageId,
                           PageCode = p.PageCode,                         
                           PageName = p.PageName,
                           PageTitle = p.PageTitle,
                           PageUrl = p.PageUrl,
                           PagePath = p.PagePath,
                           ListOrder = p.ListOrder,
                           DisableLink = p.DisableLink,
                           DisplayTitle = p.DisplayTitle,
                           Description = p.Description,
                           Keywords = p.Keywords,
                           IsDeleted = p.IsDeleted,
                           IsExtenalLink = p.IsExtenalLink,
                           IsMenu = p.IsMenu,
                           IsSecured = p.IsSecured,
                           IsVisible = p.IsVisible,
                           StartDate = p.StartDate,
                           EndDate = p.EndDate,
                           PageHeadText = p.PageHeadText,
                           PageFooterText = p.PageFooterText,
                           Icon = p.Icon,
                           CreatedByUserId = p.CreatedByUserId,
                           CreatedOnDate = p.CreatedOnDate,
                           LastModifiedByUserId = p.LastModifiedByUserId,
                           LastModifiedOnDate = p.LastModifiedOnDate
                       }).ToList();
                return lst;
            }
        }
        public static List<PageViewModel> GetAll(PageViewModel obj, string strLanguage)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                string strCommand = @"EXEC Cms.Pages_GetAll @PageTitle = {0},@IsVisible = {2}, @Lang = {3}";
                return context.Database.SqlQuery<PageViewModel>(strCommand, obj.PageTitle,obj.IsVisible, strLanguage).ToList();
            }
        }

        #endregion===========================================================================================================================

        #region Submit Data =================================================================================================================

        public static bool Insert(PageViewModel model)
        {
            bool flag = false; 
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    string strCommand = @" EXEC Cms.Pages_Insert @PageGroupId = {6}, @PageName = {0}, @PageTitle = {1}, @PageUrl = {2}, @ListOrder = {3}, @Icon = {4}, @IsVisible = {5}";
                    int result = context.Database.SqlQuery<int>(strCommand, model.PageGroupId, model.PageName, model.PageTitle, model.PageUrl, model.ListOrder, model.Icon, MathUtils.ParseBooleanToIntReturnNull(model.IsVisible)).FirstOrDefault();
                    if (result > 0)
                        flag = true;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            return flag;
        }

        public static bool Update(PageViewModel model, int Write, int Edit)
        {
            bool flag = false;
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    string strCommand = @" EXEC Cms.Pages_Update @PageId = {0}, @PageGroupId = {1},@PageName = {2}, @PageTitle = {3}, @PageUrl = {4}, @ListOrder = {5}, @Icon = {6}, @IsVisible = {7}";
                    int result = context.Database.SqlQuery<int>(strCommand, model.PageId, model.PageGroupId, model.PageName, model.PageTitle, model.PageUrl, model.ListOrder, model.Icon, MathUtils.ParseBooleanToIntReturnNull(model.IsVisible)).FirstOrDefault();
                    if (result > 0)
                        flag = true;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            return flag;
        }
        
        public static bool UpdateStatus(int Id, bool Status, out string message)
        {
           bool result = false;
            message = string.Empty;
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    Page model = Find(Id);
                    if (model != null)
                    {
                        model.IsVisible = Status;
                        context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        result = true;
                        message = Eagle.Resource.LanguageResource.UpdateSuccess;
                    }
                    else
                    {
                        message = Eagle.Resource.LanguageResource.IdExisted;
                        result = false;
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    result = false;
                    message = Eagle.Resource.LanguageResource.UpdateFailure;
                }
            }
            return result;
        }

        public static bool UpdateListOrder(int Id, int ListOrder, out string message)
        {
            bool result = false;
            message = string.Empty;

            try
            {
                Page entity = Find(Id);
                if (entity != null)
                {                        
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        entity.ListOrder = ListOrder;
                        context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        int i = context.SaveChanges();
                        if (i == 1)
                        {
                            result = true;
                            message = Eagle.Resource.LanguageResource.UpdateSuccess;
                        }
                        else
                        {
                            result = false;
                            message = Eagle.Resource.LanguageResource.UpdateFailed;
                        }  
                    }  
                }else
                    message = Eagle.Resource.LanguageResource.IDNoExistsErrorMessage;                  
                
            }
            catch (Exception ex)
            {
                message = ex.Message;
                result = false;
            }
            return result;
        }
        
        #endregion================================================================================
    }
}
