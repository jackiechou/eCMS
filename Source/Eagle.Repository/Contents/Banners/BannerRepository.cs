using Eagle.Core;
using Eagle.Model.Contents.Banners;
using Eagle.Repository.SYS.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Repository.Contents.Banners
{
    public class BannerRepository
    {
        public EntityDataContext context { get; set; }

        public BannerRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public static Banner Find(int? Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.Banners.Find(Id);
            }
        }

        public static bool IsIdExisted(int BannerId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.Banners.FirstOrDefault(c => c.BannerId == BannerId);
                return (query != null);
            }
        }
        
        public static bool IsCodeExisted(string Code)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.Banners.FirstOrDefault(p => p.BannerCode.Equals(Code));
                return (query != null);
            }
        }

        public static List<int?> GetSelectedPageListByBannerId(int BannerId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return (from b in context.PageBanners where b.BannerId == BannerId select b.PageId).ToList();
            }
        }

        public static List<BannerViewModel> GetList(int? BannerTypeId, int? BannerPositionId, string LanguageCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<BannerViewModel> lst = new List<BannerViewModel>();
                lst = (from c in context.Banners
                       join t in context.BannerTypes on c.BannerTypeId equals t.BannerTypeId into type_lst
                       from type in type_lst.DefaultIfEmpty()
                       join p in context.BannerPositions on c.BannerPositionId equals p.BannerPositionId into position_lst
                       from position in position_lst.DefaultIfEmpty()
                       select new BannerViewModel()
                       {
                           BannerId = c.BannerId,
                           BannerCode = c.BannerCode,
                           BannerPositionId = c.BannerPositionId,
                           BannerTypeId = c.BannerTypeId,
                           LanguageCode = c.LanguageCode,
                           BannerTitle = c.BannerTitle,
                           AltText = c.AltText,
                           BannerContent = c.BannerContent,
                           Advertiser = c.Advertiser,
                           FileId = c.FileId,
                           LinkToImage = c.LinkToImage,
                           Target = c.Target,
                           Description = c.Description,
                           Tags = c.Tags,
                           ListOrder = c.ListOrder,
                           ClickThroughs = c.ClickThroughs,
                           Width = c.Width,
                           Height = c.Height,
                           StartDate = c.StartDate,
                           EndDate = c.EndDate,
                           Status = c.Status,
                           CreatedByUserId = c.CreatedByUserId,
                           CreatedOnDate = c.CreatedOnDate,
                           LastModifiedByUserId = c.LastModifiedByUserId,
                           LastModifiedOnDate = c.LastModifiedOnDate
                       }).OrderByDescending(c => c.ListOrder).ToList();

                if (BannerTypeId != null && BannerTypeId > 0)
                    lst.Where(b => b.BannerTypeId == BannerTypeId);
                if (BannerPositionId != null && BannerPositionId > 0)
                    lst.Where(b => b.BannerPositionId == BannerPositionId);
                if (!(string.IsNullOrEmpty(LanguageCode)))
                    lst.Where(b => b.LanguageCode == LanguageCode);
                return lst;
            }
        }
              
        public static BannerViewModel Details(int BannerId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                BannerViewModel entity = new BannerViewModel();
                entity = (from c in context.Banners
                          join t in context.BannerTypes on c.BannerTypeId equals t.BannerTypeId into type_lst
                          from type in type_lst.DefaultIfEmpty()
                          join p in context.BannerPositions on c.BannerPositionId equals p.BannerPositionId into position_lst
                          from position in position_lst.DefaultIfEmpty()
                          select new BannerViewModel()
                          {
                              BannerId = c.BannerId,
                              BannerCode = c.BannerCode,
                              BannerPositionId = c.BannerPositionId,
                              BannerTypeId = c.BannerTypeId,
                              LanguageCode = c.LanguageCode,
                              BannerTitle = c.BannerTitle,
                              AltText = c.AltText,
                              BannerContent = c.BannerContent,
                              Advertiser = c.Advertiser,
                              FileId = c.FileId,
                              LinkToImage = c.LinkToImage,
                              Target = c.Target,
                              Description = c.Description,
                              Tags = c.Tags,
                              ListOrder = c.ListOrder,
                              ClickThroughs = c.ClickThroughs,
                              Width = c.Width,
                              Height = c.Height,
                              StartDate = c.StartDate,
                              EndDate = c.EndDate,
                              Status = c.Status,
                              CreatedByUserId = c.CreatedByUserId,
                              CreatedOnDate = c.CreatedOnDate,
                              LastModifiedByUserId = c.LastModifiedByUserId,
                              LastModifiedOnDate = c.LastModifiedOnDate
                          }).FirstOrDefault();
                return entity;
            }
        }

        public static bool Insert(BannerViewModel add_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDataDuplicate = IsIdExisted(add_model.BannerId);
                if (isDataDuplicate == false)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        Banner model = new Banner();
                        model.BannerPositionId = add_model.BannerPositionId;
                        model.BannerTypeId = add_model.BannerTypeId;
                        model.LanguageCode = add_model.LanguageCode;
                        model.BannerTitle = add_model.BannerTitle;
                        model.AltText = add_model.AltText;
                        model.BannerContent = add_model.BannerContent;
                        model.Advertiser = add_model.Advertiser;
                        model.FileId = add_model.FileId;
                        model.LinkToImage = add_model.LinkToImage;
                        model.Target = add_model.Target;
                        model.Description = add_model.Description;
                        model.Tags = add_model.Tags;
                        model.ListOrder = add_model.ListOrder;
                        model.ClickThroughs = add_model.ClickThroughs;
                        model.Width = add_model.Width;
                        model.Height = add_model.Height;
                        model.StartDate = add_model.StartDate;
                        model.EndDate = add_model.EndDate;
                        model.Status = add_model.Status;
                        model.CreatedByUserId = add_model.CreatedByUserId;
                        model.CreatedOnDate = add_model.CreatedOnDate;
                        model.LastModifiedByUserId = add_model.LastModifiedByUserId;
                        model.LastModifiedOnDate = add_model.LastModifiedOnDate;

                        int affectedRow = 0;
                        context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        affectedRow = context.SaveChanges();
                        if (affectedRow == 1)
                        {
                            add_model.BannerId = model.BannerId;
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

        public static bool Update(BannerViewModel edit_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool EmpIDExist = IsIdExisted(edit_model.BannerId);
                 if (EmpIDExist == true)
                 {
                     Banner model = Find(edit_model.BannerId);
                     if (model != null)
                     {
                         using (EntityDataContext context = new EntityDataContext())
                         {
                             model.BannerPositionId = edit_model.BannerPositionId;
                             model.BannerTypeId = edit_model.BannerTypeId;
                             model.LanguageCode = edit_model.LanguageCode;
                             model.BannerTitle = edit_model.BannerTitle;
                             model.AltText = edit_model.AltText;
                             model.BannerContent = edit_model.BannerContent;
                             model.Advertiser = edit_model.Advertiser;
                             model.FileId = edit_model.FileId;
                             model.LinkToImage = edit_model.LinkToImage;
                             model.Target = edit_model.Target;
                             model.Description = edit_model.Description;
                             model.Tags = edit_model.Tags;
                             model.ListOrder = edit_model.ListOrder;
                             model.ClickThroughs = edit_model.ClickThroughs;
                             model.Width = edit_model.Width;
                             model.Height = edit_model.Height;
                             model.StartDate = edit_model.StartDate;
                             model.EndDate = edit_model.EndDate;
                             model.Status = edit_model.Status;
                             model.CreatedByUserId = edit_model.CreatedByUserId;
                             model.CreatedOnDate = edit_model.CreatedOnDate;
                             model.LastModifiedByUserId = edit_model.LastModifiedByUserId;
                             model.LastModifiedOnDate = edit_model.LastModifiedOnDate;

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
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return result;
        }

        public static bool Delete(int? id, out string message)
        {
            bool result = false;
            message = string.Empty;
            try
            {
                Banner model = Find(id);
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
