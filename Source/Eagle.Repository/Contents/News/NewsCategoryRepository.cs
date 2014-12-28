using Eagle.Common.Utilities;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Eagle.Model.Common;
using Eagle.Model.Contents;
using Eagle.Model;
using Eagle.Repository.SYS.FileManager;
using Eagle.Model.Contents.News;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Eagle.Repository.Contents.News
{
    public class NewsCategoryRepository
    {
        public EntityDataContext context { get; set; }

        public NewsCategoryRepository(EntityDataContext context)
        {
            this.context = context;
        }


        public static NewsCategory Find(int? id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.NewsCategories.Find(id);
            }
        }
        public static string GenerateCategoryCode(int num)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                System.Nullable<Int32> Max_ID = (from u in context.NewsCategories select u.CategoryId).DefaultIfEmpty(0).Max() + 1;
                string _MaxID = Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
                return _MaxID;
            }
        }

        public static string GenerateCategoryCode(int num, string sid)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                System.Nullable<Int32> Max_ID = (from u in context.NewsCategories select u.CategoryId).DefaultIfEmpty(0).Max() + 1;
                string _MaxID = Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
                return string.Format("{0}-{1}", num, sid.Substring(0, 5).ToUpper());
            }
        }


        public static bool IsDataExisted(string CategoryCode, string CategoryName)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.NewsCategories.FirstOrDefault(c => c.CategoryCode.ToUpper().Equals(CategoryCode.ToUpper())
                    && (c.CategoryName.ToUpper().Equals(CategoryName.ToUpper())));
                return (query != null);
            }
        }

        public static bool IsCodeExisted(string CategoryCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.NewsCategories.FirstOrDefault(c => c.CategoryCode.ToUpper().Equals(CategoryCode.ToUpper()));
                return (query != null);
            }
        }

        public static bool IsIDExisted(int ID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.NewsCategories.FirstOrDefault(c => c.CategoryId.Equals(ID));
                return (query != null);
            }
        }


        #region =============================================================================================================================
        public static List<TreeNode> PopulateHierachicalDropDownList(string LanguageCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<TreeNode> list = context.NewsCategories.OrderBy(p => p.ListOrder).Where(p => p.LanguageCode == LanguageCode).Select(p => new TreeNode()
                {
                    id = p.CategoryId,
                    text = p.CategoryName,
                    parentid = p.ParentId
                }).ToList();
                List<TreeNode> recursiveObjects = CommonRepository.RecursiveFillTreeNodes(list, null);
                return recursiveObjects;
            }
        }
        public List<NewsCategory> GetParentNodes()
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<NewsCategory> list = context.NewsCategories.Where(p => p.ParentId == null || p.ParentId == 0).OrderBy(p => p.ListOrder).ToList();
                return list;
            }
        }
        public static List<TreeNode> GetChildren(int? ParentID, string LanguageCode)
        {
            List<TreeNode> recursiveObjects = new List<TreeNode>();
            if (ParentID != null && ParentID != 0)
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    List<TreeNode> list = context.NewsCategories.OrderBy(p => p.ListOrder).Select(p => new TreeNode()
                    {
                        id = p.CategoryId,
                        text = p.CategoryName,
                        parentid = p.ParentId
                    }).ToList();
                    recursiveObjects = CommonRepository.RecursiveFillTreeNodes(list, ParentID);
                }
            }
            return recursiveObjects;
        }
        public static List<int> GetTreeIdListByNodeId(int? Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<int> children = new List<int>();
                if (Id != null && Id > 0)
                {
                    var lineage = context.NewsCategories.Where(p => p.CategoryId == Id).Select(p => p.Lineage).FirstOrDefault();
                    children = context.NewsCategories.Where(p => p.Lineage.Contains(lineage)).Select(p => p.CategoryId).ToList();
                }
                return children;
            }
        }
        public List<TreeNode> GetTreeListByParentNode(int ParentId)
        {
            //var Lineage = context.NewsCategories.Where(p => p.CategoryId == CategoryId).Select(p => p.Lineage).FirstOrDefault();
            //var allChildenCompany = context.NewsCategories.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.CategoryId).ToList();

            using (EntityDataContext context = new EntityDataContext())
            {
                List<TreeNode> list = context.NewsCategories.OrderBy(p => p.ListOrder).Select(p => new TreeNode()
                {
                    id = p.CategoryId,
                    text = p.CategoryName,
                    parentid = p.ParentId
                }).ToList();
                List<TreeNode> recursiveObjects = CommonRepository.RecursiveFillTreeNodes(list, ParentId);
                return recursiveObjects;
            }
        }


        public static List<TreeEntity> GetTreeList(string LanguageCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<TreeEntity> list = context.NewsCategories.OrderBy(p => p.ListOrder).Where(p => p.LanguageCode == LanguageCode).Select(p => new TreeEntity()
                {
                    key = p.CategoryId,
                    title = p.CategoryName,
                    parentid = p.ParentId,
                    depth = p.Depth,
                    folder = (p.Depth > 1) ? true : false,
                    lazy = (p.Depth > 1) ? true : false,
                    expanded = (p.Depth > 1) ? true : false,
                    tooltip = p.Description
                }).ToList();
                List<TreeEntity> recursiveObjects = CommonRepository.RecursiveFillTreeEntities(list, null);
                return recursiveObjects;
            }
        }

      
        #endregion =============================================================================================================================

        public static NewsCategoryViewModel GetDetails(int CategoryId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = (from x in context.NewsCategories
                             where x.CategoryId == CategoryId
                             select new NewsCategoryViewModel
                             {
                                 CategoryId = x.CategoryId,
                                 CategoryCode = x.CategoryCode,
                                 CategoryName = x.CategoryName,
                                 Alias = x.Alias,
                                 ParentId = x.ParentId,
                                 Depth = x.Depth,
                                 Lineage = x.Lineage,
                                 CategoryImage = x.CategoryImage,
                                 Description = x.Description,
                                 NavigateUrl = x.NavigateUrl,
                                 ListOrder = x.ListOrder,
                                 Status = x.Status,
                                 LanguageCode = x.LanguageCode,
                                 CreatedOnDate = x.CreatedOnDate,
                                 LastModifiedOnDate = x.LastModifiedOnDate,
                                 CreatedByUserId = x.CreatedByUserId,
                                 LastModifiedByUserId = x.LastModifiedByUserId,
                                 IPLog = x.IPLog,
                                 IPLastUpdated = x.IPLastUpdated
                             }).SingleOrDefault();
                return query;
            }
        }

        public static NewsCategoryViewModel GetDetailsByCategoryCode(string CategoryCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = (from x in context.NewsCategories
                             where x.CategoryCode.Trim() == CategoryCode
                             select new NewsCategoryViewModel
                             {
                                 CategoryId = x.CategoryId,
                                 CategoryCode = x.CategoryCode,
                                 CategoryName = x.CategoryName,
                                 Alias = x.Alias,
                                 ParentId = x.ParentId,
                                 Depth = x.Depth,
                                 Lineage = x.Lineage,
                                 CategoryImage = x.CategoryImage,
                                 Description = x.Description,
                                 NavigateUrl = x.NavigateUrl,
                                 ListOrder = x.ListOrder,
                                 Status = x.Status,
                                 LanguageCode = x.LanguageCode,
                                 CreatedOnDate = x.CreatedOnDate,
                                 LastModifiedOnDate = x.LastModifiedOnDate,
                                 CreatedByUserId = x.CreatedByUserId,
                                 LastModifiedByUserId = x.LastModifiedByUserId,
                                 IPLog = x.IPLog,
                                 IPLastUpdated = x.IPLastUpdated
                             }).SingleOrDefault();
                return query;
            }
        }

        public static List<NewsCategoryViewModel> GetList(int ApplicationId, string LanguageCode, int? Status)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<NewsCategoryViewModel> lst = new List<NewsCategoryViewModel>();
                lst = (from x in context.NewsCategories
                       where (Status == null || x.Status == Status)
                       && x.LanguageCode == LanguageCode && x.ApplicationId == ApplicationId
                       select new NewsCategoryViewModel
                       {
                           CategoryId = x.CategoryId,
                           CategoryCode = x.CategoryCode,
                           CategoryName = x.CategoryName,
                           Alias = x.Alias,
                           ParentId = x.ParentId,
                           Depth = x.Depth,
                           Lineage = x.Lineage,
                           CategoryImage = x.CategoryImage,
                           Description = x.Description,
                           NavigateUrl = x.NavigateUrl,
                           ListOrder = x.ListOrder,
                           Status = x.Status,
                           LanguageCode = x.LanguageCode,
                           CreatedOnDate = x.CreatedOnDate,
                           LastModifiedOnDate = x.LastModifiedOnDate,
                           CreatedByUserId = x.CreatedByUserId,
                           LastModifiedByUserId = x.LastModifiedByUserId,
                           IPLog = x.IPLog,
                           IPLastUpdated = x.IPLastUpdated
                       }).ToList();
                return lst;
            }
        }

        public static NewsCategory Find(int CategoryId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = (from x in context.NewsCategories
                             where x.CategoryId == CategoryId
                             select x).SingleOrDefault();
                return query;
            }
        }

        public static NewsCategory FindByCode(string CategoryCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = (from x in context.NewsCategories
                             where x.CategoryCode.Trim() == CategoryCode
                             select x).SingleOrDefault();
                return query;
            }
        }

        public static List<NewsCategoryViewModel> GetAllParentNodesOfSelectedNodeStatus(string LanguageCode, Guid CategoryCode, int Status)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.NewsCategories_GetAllParentNodesOfSelectedNodeStatus(LanguageCode, CategoryCode, Status).Select(x => new NewsCategoryViewModel()
                {
                    CategoryId = (int)x.CategoryId,
                    CategoryCode = x.CategoryCode,
                    CategoryName = x.CategoryName,
                    Alias = x.Alias,
                    ParentId = x.ParentId,
                    Depth = x.Depth,
                    Lineage = x.Lineage,
                    CategoryImage = x.CategoryImage,
                    Description = x.Description,
                    NavigateUrl = x.NavigateUrl,
                    ListOrder = x.ListOrder,
                    Status = x.Status,
                    LanguageCode = x.LanguageCode,
                    CreatedOnDate = DateTimeUtils.ToDateTime(x.CreatedOnDate, "dd/MM/yyyy"),
                    LastModifiedOnDate = DateTimeUtils.ToDateTime(x.LastModifiedOnDate, "dd/MM/yyyy"),
                    CreatedByUserId = x.CreatedByUserId,
                    LastModifiedByUserId = x.LastModifiedByUserId,
                    IPLog = x.IPLog,
                    IPLastUpdated = x.IPLastUpdated
                }).AsQueryable();

                return query.ToList();
            }
        }

        public static List<NewsCategoryViewModel> GetAllChildrenNodesOfSelectedNodeStatus(string LanguageCode, Guid CategoryCode, int Status)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.NewsCategories_GetAllChildrenNodesOfSelectedNodeStatus(LanguageCode, CategoryCode, Status).Select(x => new NewsCategoryViewModel()
                {
                    CategoryId = (int)x.CategoryId,
                    CategoryCode = x.CategoryCode,
                    CategoryName = x.CategoryName,
                    Alias = x.Alias,
                    ParentId = x.ParentId,
                    Depth = x.Depth,
                    Lineage = x.Lineage,
                    CategoryImage = x.CategoryImage,
                    Description = x.Description,
                    NavigateUrl = x.NavigateUrl,
                    ListOrder = x.ListOrder,
                    Status = x.Status,
                    LanguageCode = x.LanguageCode,
                    CreatedOnDate = DateTimeUtils.ToDateTime(x.CreatedOnDate, "dd/MM/yyyy"),
                    LastModifiedOnDate = DateTimeUtils.ToDateTime(x.LastModifiedOnDate, "dd/MM/yyyy"),
                    CreatedByUserId = x.CreatedByUserId,
                    LastModifiedByUserId = x.LastModifiedByUserId,
                    IPLog = x.IPLog,
                    IPLastUpdated = x.IPLastUpdated
                }).AsQueryable();

                return query.ToList();
            }
        }

        public static bool Insert(NewsCategoryViewModel add_model, out string Message)
        {
            Message = string.Empty;
            bool flag = false;
            int result = -1;

            try
            {
                bool isDuplicate = IsDataExisted(add_model.CategoryCode, add_model.CategoryName);
                if (isDuplicate == false)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        HttpPostedFileBase FileUpload = add_model.FileUpload;
                        if (FileUpload != null)
                            add_model.CategoryImage = FileRepository.Insert(FileUpload, "UPLOAD_NEWS_CATEGORY_IMAGE_DIR", null, null, add_model.CreatedByUserId);
                       
                        add_model.IPLog = NetworkUtils.GetIP4Address();
                        add_model.Alias = StringUtils.convertTitle2Alias(add_model.CategoryName);

                        var retVal = new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        object[] parameters = {                             
                            new SqlParameter("@CreatedByUserId",add_model.CreatedByUserId),
                            new SqlParameter("@IPLog",add_model.IPLog),           
                            new SqlParameter("@ApplicationId",add_model.ApplicationId),
                            new SqlParameter("@LanguageCode",add_model.LanguageCode),
                            new SqlParameter("@CategoryName",add_model.CategoryName),
                            new SqlParameter("@Alias",add_model.Alias),
                            new SqlParameter("@ParentId",add_model.ParentId),
                            new SqlParameter("@CategoryImage",add_model.CategoryImage),
                            new SqlParameter("@Description",add_model.Description),
                            new SqlParameter("@NavigateUrl",add_model.NavigateUrl),
                            new SqlParameter("@Status",add_model.Status),
                            retVal
                        };

                        string sql = @"EXEC dbo.NewsCategories_Insert @CreatedByUserId,@IPLog,@ApplicationId,@LanguageCode,@CategoryName,@Alias,@ParentId,@CategoryImage,@Description,@NavigateUrl,@Status, @o_return OUTPUT";
                        int affectedRow = context.Database.ExecuteSqlCommand(sql, parameters);

                        if (!string.IsNullOrEmpty(retVal.Value.ToString()))
                        {
                            result = Convert.ToInt32(retVal.Value);   
                            if(result > 0)
                            {
                                Message = Eagle.Resource.LanguageResource.CreateSuccess;
                                flag = true;
                            }
                        }
                    }
                }
                else
                {
                    flag = false;
                    Message = Eagle.Resource.LanguageResource.DuplicateError;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                flag = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return flag;
        }

        public static bool Update(NewsCategoryViewModel edit_model, out string Message)
        {
            Message = string.Empty;
            bool flag = false;
            int result = -1;
            //try
            //{

            NewsCategory model = Find(edit_model.CategoryId);
            if (model != null)
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    int? oFileId = null;
                    HttpPostedFileBase FileUpload = edit_model.FileUpload;
                    if (FileUpload != null)
                        FileRepository.Update(FileUpload, "UPLOAD_NEWS_CATEGORY_IMAGE_DIR", null, null, edit_model.CategoryImage, edit_model.LastModifiedByUserId, out oFileId);

                    edit_model.CategoryImage = oFileId;
                    edit_model.IPLastUpdated = NetworkUtils.GetIP4Address();
                    edit_model.Alias = StringUtils.convertTitle2Alias(edit_model.CategoryName);

                    var retVal = new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    object[] parameters = {                             
                            new SqlParameter("@LastModifiedByUserId",edit_model.LastModifiedByUserId),
                            new SqlParameter("@IPLastUpdated",edit_model.IPLastUpdated),           
                            new SqlParameter("@ApplicationId",edit_model.ApplicationId),
                            new SqlParameter("@LanguageCode",edit_model.LanguageCode),
                            new SqlParameter("@CategoryId",edit_model.CategoryId),
                            new SqlParameter("@CategoryName",edit_model.CategoryName),
                            new SqlParameter("@Alias",edit_model.Alias),
                            new SqlParameter("@ParentId",edit_model.ParentId),
                            new SqlParameter("@CategoryImage",edit_model.CategoryImage),
                            new SqlParameter("@Description",edit_model.Description),
                            new SqlParameter("@NavigateUrl",edit_model.NavigateUrl),
                            new SqlParameter("@Status",edit_model.Status),
                            retVal
                        };

                    string sql = @"EXEC dbo.NewsCategories_Update @LastModifiedByUserId,@IPLastUpdated,@ApplicationId,@LanguageCode,@CategoryId,@CategoryName,@Alias,@ParentId,@CategoryImage,@Description,@NavigateUrl,@Status, @o_return OUTPUT";
                    int affectedRow = context.Database.ExecuteSqlCommand(sql, parameters);

                    if (!string.IsNullOrEmpty(retVal.Value.ToString()))
                    {
                        result = Convert.ToInt32(retVal.Value);
                        if (result > 0)
                        {
                            Message = Eagle.Resource.LanguageResource.CreateSuccess;
                            flag = true;
                        }
                    }
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    ex.ToString();
            //    flag = false;
            //    Message = Eagle.Resource.LanguageResource.SystemError;
            //}
            return flag;
        }

        public static bool Delete(int? id, out string message)
        {
            bool result = false;
            message = string.Empty;
            try
            {
                NewsCategory model = Find(id);
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
