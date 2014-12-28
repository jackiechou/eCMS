using Eagle.Core;
using Eagle.Model;
using Eagle.Model.Contents;
using Eagle.Model.Contents.Banners;
using Eagle.Repository.SYS.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Repository.Contents.Banners
{
    public class BannerPositionRepository
    {
        public static BannerPosition Find(int? Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.BannerPositions.Find(Id);
            }
        }

        public static bool IsIdExisted(int Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.BannerPositions.FirstOrDefault(c => c.BannerPositionId == Id);
                return (query != null);
            }
        }

        public static bool IsCodeExisted(string Code)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.BannerPositions.FirstOrDefault(p => p.BannerPositionCode.Equals(Code));
                return (query != null);
            }
        }

        public static bool IsDataExisted(string BannerPositionName)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.BannerPositions.FirstOrDefault(p => p.BannerPositionName.Equals(BannerPositionName));
                return (query != null);
            }
        }

        public static List<BannerPositionViewModel> GetActiveList()
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var lst = (from b in context.BannerPositions
                           select new BannerPositionViewModel
                           {
                               BannerPositionId = b.BannerPositionId,
                               BannerPositionCode = b.BannerPositionCode,
                               BannerPositionName = b.BannerPositionName,
                               BannerPositionStatus = Eagle.Model.Common.TwoStatusBoolean.Active,
                               BannerPositionListOrder = b.BannerPositionListOrder,
                               BannerPositionDescription = b.BannerPositionDescription                   
                           }).ToList();
                return lst;
            }
        }

        public static List<BannerPositionViewModel> GetList()
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var lst = (from b in context.BannerPositions
                            select new BannerPositionViewModel
                            {
                                BannerPositionId = b.BannerPositionId,
                                BannerPositionCode = b.BannerPositionCode,
                                BannerPositionName = b.BannerPositionName,
                                BannerPositionListOrder = b.BannerPositionListOrder,
                                BannerPositionDescription = b.BannerPositionDescription,
                                BannerPositionStatus = b.BannerPositionStatus
                            }).ToList();
                return lst;
            }
        }       

        public static BannerPositionViewModel Details(int BannerId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                BannerPositionViewModel entity = new BannerPositionViewModel();
                entity = (from b in context.BannerPositions
                          select new BannerPositionViewModel()
                          {
                              BannerPositionId = b.BannerPositionId,
                              BannerPositionCode = b.BannerPositionCode,
                              BannerPositionName = b.BannerPositionName,
                              BannerPositionListOrder = b.BannerPositionListOrder,
                              BannerPositionDescription = b.BannerPositionDescription,
                              BannerPositionStatus = b.BannerPositionStatus
                          }).FirstOrDefault();
                return entity;
            }
        }

        public static bool Insert(BannerPositionViewModel add_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDataDuplicate = IsDataExisted(add_model.BannerPositionName);
                if (isDataDuplicate == false)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        BannerPosition model = new BannerPosition();
                        model.BannerPositionName = add_model.BannerPositionName;
                        model.BannerPositionListOrder = add_model.BannerPositionListOrder;
                        model.BannerPositionDescription = add_model.BannerPositionDescription;
                        model.BannerPositionStatus = add_model.BannerPositionStatus;

                        int affectedRow = 0;
                        context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        affectedRow = context.SaveChanges();
                        if (affectedRow == 1)
                        {
                            add_model.BannerPositionId = model.BannerPositionId;
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

        public static bool Update(BannerPositionViewModel edit_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool IdExist = IsIdExisted(edit_model.BannerPositionId);
                if (IdExist == true)
                {
                    BannerPosition model = Find(edit_model.BannerPositionId);
                    if (model != null)
                    {
                        using (EntityDataContext context = new EntityDataContext())
                        {
                            model.BannerPositionName = edit_model.BannerPositionName;
                            model.BannerPositionListOrder = edit_model.BannerPositionListOrder;
                            model.BannerPositionDescription = edit_model.BannerPositionDescription;
                            model.BannerPositionStatus = edit_model.BannerPositionStatus;

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
        
        public static bool Delete(int? Id, out string Message)
        {
            bool result = false;
            Message = string.Empty;
            try
            {
                BannerPosition model = Find(Id);
                if (model != null)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        result = true;
                        Message = Eagle.Resource.LanguageResource.DeleteSuccess;
                    }
                }
                else
                {
                    Message = Eagle.Resource.LanguageResource.IdExisted;
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.DeleteFailure;
            }

            return result;
        }
    }
}
