using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Eagle.Model;
using Eagle.Core;
using Eagle.Model.Common;
using Eagle.Common.Settings;
using System.Data;
using Eagle.Common.Data;
namespace Eagle.Repository
{
    public class LS_tblCompanyRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblCompanyRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<CompanyViewModel> GetAll()
        {
            return context.LS_tblCompany.Select(p => new CompanyViewModel()
            {
                LSCompanyID = p.LSCompanyID,
                LSCompanyCode = p.LSCompanyCode,
                LSCompanyDefineID = p.LSCompanyDefineID,
                Name = p.Name,
                Parent = p.Parent,
                Used = p.Used
            }).ToList();
        }

        public CompanyViewModel FindEdit(int id)
        {
            CompanyViewModel ret = new CompanyViewModel();
            var model = context.LS_tblCompany.Find(id);
            AutoMapper.Mapper.CreateMap<LS_tblCompany, CompanyViewModel>();
            AutoMapper.Mapper.Map(model, ret);
            return ret;
        }

        public bool LSCompanyCodeExists(string LSCompanyCode, string InitialLSCompanyCode = null)
        {
            if (string.IsNullOrEmpty(InitialLSCompanyCode))
            {
                var OnlineProcess = context.LS_tblCompany.Where(p => p.LSCompanyCode == LSCompanyCode).FirstOrDefault();
                return OnlineProcess != null;
            }
            else
            {
                var OnlineProcess = context.LS_tblCompany.Where(p => p.LSCompanyCode == LSCompanyCode && p.LSCompanyCode != InitialLSCompanyCode).FirstOrDefault();
                return OnlineProcess != null;
            }
        }

        public List<CompanyViewModel> GetByParent(int? ParentID, int LanguageId = 10001)
        {
            var result = from p in context.LS_tblCompany
                         where (ParentID == null ? p.Parent == null : p.Parent == ParentID)
                         select new CompanyViewModel()
                         {
                             LSCompanyID = p.LSCompanyID,
                             Name = (LanguageId == 124) ? p.Name : p.VNName
                         };
            return result.ToList();
        }

        public IEnumerable<LS_tblCompanyViewModel> ListCompany()
        {
            try
            {
                return context.LS_tblCompany
                          //.Where(p => p.Used.Value == true)
                            .Select(p => new LS_tblCompanyViewModel()
                            {
                                LSCompanyID = p.LSCompanyID,
                                LSCompanyCode = p.LSCompanyCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                FileId = p.FileId,
                                Rank = p.Rank,
                                Used = p.Used,
                                Phone =p.Phone,
                                Fax = p.Fax,
                                Address = p.Address,
                                TaxCode = p.TaxCode,
                                Creater = p.Creater,
                                CreateTime = p.CreateTime,
                                Editor = p.Editor,
                                EditTime= p.EditTime
                            });
            }
            catch
            {
                return new List<LS_tblCompanyViewModel>();
            }
        }

        public static LS_tblCompanyViewModel GetCompanyInfo(int LanguageId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var entity = context.LS_tblCompany.Select(p => new LS_tblCompanyViewModel()
                {
                    LSCompanyID = p.LSCompanyID,
                    LSCompanyCode = p.LSCompanyCode,
                    LSCompanyName = (LanguageId == 124) ? p.Name : p.VNName,
                    Name = p.Name,
                    VNName = p.VNName,
                    Fax = p.Fax,
                    Phone = p.Phone,
                    TaxCode = p.TaxCode,
                    Address = p.Address,
                    FileId = p.FileId,
                    Rank = p.Rank,
                    Used = p.Used
                }).DefaultIfEmpty().First();
                return entity;
            }
        }


        public List<CompanyEntity> GetListForDropDownList()
        {
            try
            {
                return context.LS_tblCompany.Select(p => new CompanyEntity()
                            {
                                Id = p.LSCompanyID,                              
                                Name = p.Name,
                            }).OrderBy(p=>p.Name).ToList();
            }
            catch
            {
                return new List<CompanyEntity>();
            }
        }

        public List<LS_tblCompanyViewModel> GetList()
        {
            try
            {
                return context.LS_tblCompany.Select(p => new LS_tblCompanyViewModel()
                            {
                                LSCompanyID = p.LSCompanyID,
                                LSCompanyCode = p.LSCompanyCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                FileId = p.FileId
                            }).OrderBy(p=>p.Name).ToList();
            }
            catch
            {
                return new List<LS_tblCompanyViewModel>();
            }
        }
        
        public static LS_tblCompanyViewModel Details(int? LSCompanyID, int LanguageId)
        {
            LS_tblCompanyViewModel entity = new LS_tblCompanyViewModel();
            using (EntityDataContext context = new EntityDataContext())
            {
                if (LSCompanyID != null && LSCompanyID > 0)
                {
                    var query = (context.LS_tblCompany
                                    .Select(p => new LS_tblCompanyViewModel()
                                    {
                                        LSCompanyID = p.LSCompanyID,
                                        LSCompanyCode = p.LSCompanyCode,
                                        LSCompanyName = (LanguageId == 124)?p.Name:p.VNName,
                                        Name = p.Name,
                                        VNName = p.VNName,
                                        FileId = p.FileId,
                                        Rank = p.Rank,
                                        Used = p.Used,
                                    })).ToList();
                    entity = query.FirstOrDefault();
                }
            }
            return entity;
        }


        public LS_tblCompanyViewModel GetDetails(int? LSCompanyID)
        {
            LS_tblCompanyViewModel entity = null;
            if (LSCompanyID != null && LSCompanyID > 0)
            {
                var query = (context.LS_tblCompany
                                .Select(p => new LS_tblCompanyViewModel()
                                {
                                    LSCompanyID = p.LSCompanyID,
                                    LSCompanyCode = p.LSCompanyCode,
                                    Name = p.Name,
                                    VNName = p.VNName,
                                    FileId = p.FileId,
                                    Rank = p.Rank,
                                    Used = p.Used,
                                })).ToList();
                entity = query.FirstOrDefault();
            }
            return entity;
        }
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblCompany.FirstOrDefault(p => p.LSCompanyCode.Equals(strCode));
            return (code != null);
        }

        public string AddCompany(LS_tblCompany model)
        {
            if (CheckExistCode(model.LSCompanyCode))
            {
                string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                return errorMessage;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return "success";
        }
        public LS_tblCompany Find(int? id)
        {
            return context.LS_tblCompany.Find(id);
        }
        public LS_tblCompanyViewModel FindEditModel(int id)
        {
            LS_tblCompanyViewModel ret = context.LS_tblCompany
                                        .Where(p => p.LSCompanyID == id)
                                        .Select(p => new LS_tblCompanyViewModel()
                                        {
                                            LSCompanyID = p.LSCompanyID,
                                            LSCompanyCode = p.LSCompanyCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Address = p.Address,
                                            Phone = p.Phone,
                                            TaxCode = p.TaxCode,
                                            Fax = p.Fax,
                                            FileId = p.FileId,
                                            Rank = p.Rank,
                                            Used = p.Used
                                        }).FirstOrDefault();
            return ret;
        }

        public bool Add(LS_tblCompany modelAdd, out string errorMessage)
        {
            try
            {
                context.Entry(modelAdd).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                try
                {
                    //update 
                    string Lineage = "";
                    if (modelAdd.Parent != null && modelAdd.Parent != 0)
                    {
                        LS_tblCompany parentModel = Find(modelAdd.Parent);
                        Lineage = parentModel.Lineage;
                    }
                    Lineage += modelAdd.LSCompanyID.ToString() + ".";
                    modelAdd.Lineage = Lineage;
                    context.SaveChanges();
                }
                catch
                {
                    context.Entry(modelAdd).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    throw new Exception("Lỗi hệ thống! Không thể cập nhật Lineage");
                }

                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Update(LS_tblCompanyViewModel companyModel, out string errorMessage)
        {
            try
            {
                LS_tblCompany modelUpdate = Find(companyModel.LSCompanyID);
               // modelUpdate.LSCompanyCode = model.LSCompanyCode;
                modelUpdate.TaxCode = companyModel.TaxCode;
                modelUpdate.Name = companyModel.Name;
                modelUpdate.VNName = companyModel.VNName;
                modelUpdate.Rank = companyModel.Rank;
                modelUpdate.Used = companyModel.Used;
                modelUpdate.Address = companyModel.Address;
                modelUpdate.Phone = companyModel.Phone;
                modelUpdate.Fax = companyModel.Fax;
                modelUpdate.FileId = companyModel.FileId;
                modelUpdate.Editor = companyModel.Editor;
                modelUpdate.EditTime = companyModel.EditTime;
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                errorMessage = "";
                return true;
            }
            catch
            {
                errorMessage = "Error";
                return false;
            }
        }
        
        public List<LS_tblCompanyViewModel> GetAllCompany(int LanguageId)
        {
            if (LanguageId == 124)
            {
                return context.LS_tblCompany.Select(p => new LS_tblCompanyViewModel() { LSCompanyID = p.LSCompanyID, Name = p.Name }).ToList();
            }else
            {
                return context.LS_tblCompany.Select(p => new LS_tblCompanyViewModel() { LSCompanyID = p.LSCompanyID, Name = p.VNName }).ToList();
            }
        }

        #region HIEARACHY List ==========================================================================================================
        public static List<int> GetTreeIdListByNodeId(int? Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<int> children = new List<int>();
                if (Id != null && Id > 0)
                {
                    var lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == Id).Select(p => p.Lineage).FirstOrDefault();
                    children = context.LS_tblCompany.Where(p => p.Lineage.Contains(lineage)).Select(p => p.LSCompanyID).ToList();                    
                }
                return children;
            }
        }
        
        public List<TreeNode> GetTreeListByParentNode(int ParentId)
        {
            //var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
            //var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();

            using (EntityDataContext context = new EntityDataContext())
            {
                List<TreeNode> list = context.LS_tblCompany.OrderBy(p => p.Name).Select(p => new TreeNode()
                {
                    id = p.LSCompanyID,
                    text = p.Name,
                    parentid = p.Parent
                }).ToList();
                List<TreeNode> recursiveObjects = RecursiveFillTree(list, ParentId);
                return recursiveObjects;
            }
        }

        public List<LS_tblCompany> GetParentNodes()
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<LS_tblCompany> list = context.LS_tblCompany.Where(p => p.Parent == null || p.Parent == 0).OrderBy(p => p.Name).ToList();
                return list;
            }
        }

        public static List<LS_tblCompany> GetParentList()
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<LS_tblCompany> list = context.LS_tblCompany.Where(p => p.Parent == null || p.Parent == 0).OrderBy(p => p.Name).ToList();
                return list;
            }
        }

        public static List<TreeNode> GetChildren(int? ParentID, int? LanguageId)
        {
            List<TreeNode> recursiveObjects = new List<TreeNode>();
            if (ParentID != null && ParentID != 0)
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    List<TreeNode> list = context.LS_tblCompany.OrderBy(p => p.Name).Select(p => new TreeNode()
                    {
                        id = p.LSCompanyID,
                        text = (LanguageId == 124) ? p.Name : p.VNName,
                        parentid = p.Parent
                    }).ToList();
                    recursiveObjects = RecursiveFillTree(list, ParentID);                    
                }
            }
            return recursiveObjects;
        }


        public static List<TreeNode> PopulateHierachicalDropDownList(int? LanguageId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<TreeNode> list = context.LS_tblCompany.OrderBy(p=>p.Name).Select(p => new TreeNode() { 
                    id = p.LSCompanyID, 
                    text =(LanguageId == 124) ? p.Name : p.VNName, 
                    parentid = p.Parent }).ToList();                
                List<TreeNode> recursiveObjects = RecursiveFillTree(list, null);
                return recursiveObjects;
            }
        }

        private static List<TreeNode> RecursiveFillTree(List<TreeNode> elements, int? parentid)
        {          
            List<TreeNode> items = new List<TreeNode>();
            List<TreeNode> children = elements.Where(p => p.parentid == ((parentid==0)?null:parentid)).Select(
               p => new TreeNode
               {
                   id = p.id,
                   text = p.text,
                   parentid = p.parentid,
                   depth = p.depth
               }).ToList();

            if (children.Count > 0)
            {
                foreach (var child in children)
                {
                    TreeNode node = new TreeNode()
                    {
                        id = child.id,
                        parentid = child.parentid,
                        text = child.text,
                        depth = child.depth,
                        children = RecursiveFillTree(elements, child.id)
                    };
                    items.Add(node);
                }
            }
            return items;
        }
        #endregion =============================================================================================================================





        #region Bind DropdownList ========================================================================
        private List<AutoCompleteViewModel> GetListCompany(string searchTerm, int? LanguageId)
        {
            var tmp = context.LS_tblCompany
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSCompanyID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           });
            List<AutoCompleteViewModel> ret = new List<AutoCompleteViewModel>();
            foreach (var item in tmp)
            {
                AutoCompleteViewModel p = new AutoCompleteViewModel()
                {
                    id = item.id.ToString(),
                    name = item.name,
                    text = item.text
                };
                ret.Add(p);
            }
            return ret;
        }

        // dùng cho bind dropdownlist
        public List<AutoCompleteViewModel> ListDropdown(string searchTerm, int pageSize, int pageNume, int? LanguageId)
        {
            return GetListCompany(searchTerm, LanguageId).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }
        #endregion ======================================================================================

    }
}
