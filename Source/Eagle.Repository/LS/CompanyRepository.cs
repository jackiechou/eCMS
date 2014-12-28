using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class CompanyRepository
    {
        public EntityDataContext context { get; set; }
        public CompanyRepository(EntityDataContext context)
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

        public LS_tblCompany Find(int? id)
        {
            return context.LS_tblCompany.Find(id);
        }
        public CompanyViewModel FindEdit(int id)
        {
            CompanyViewModel ret = new CompanyViewModel();
            var model = context.LS_tblCompany.Find(id);
            AutoMapper.Mapper.CreateMap<LS_tblCompany, CompanyViewModel>();
            AutoMapper.Mapper.Map(model, ret);
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

        public bool Update(CompanyViewModel model, out string errorMessage)
        {
            try
            {
                var modelEdit = Find(model.LSCompanyID);
                modelEdit.LSCompanyCode = model.LSCompanyCode;
                modelEdit.TaxCode = model.TaxCode;
                modelEdit.Name = model.Name;
                modelEdit.VNName = model.VNName;
                modelEdit.Phone = model.Phone;
                modelEdit.Fax = model.Fax;
                modelEdit.Address = model.Address;
                modelEdit.Used = model.Used;
                modelEdit.Editor = model.Editor;
                modelEdit.EditTime = DateTime.Now;
                context.Entry(modelEdit).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }


        public bool LSCompanyCodeExists(string LSCompanyCode, string InitialLSCompanyCode = null )
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
                         select new CompanyViewModel() { 
                              LSCompanyID = p.LSCompanyID,
                              Name = (LanguageId == 124) ? p.Name : p.VNName
                         };
            return result.ToList();
        }
    }
}
