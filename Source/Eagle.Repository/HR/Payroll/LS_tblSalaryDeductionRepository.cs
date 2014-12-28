using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Core;

using System.Web.Routing;
using Eagle.Common.Helpers;
using System.Data.Entity.Validation;
using Eagle.Model;

namespace Eagle.Repository
{
    public class LS_tblSalaryDeductionRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblSalaryDeductionRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(string strValidate)
        {
            var code = context.LS_tblSalaryDeduction.FirstOrDefault(p => p.LSDeductSalaryCode.Equals(strValidate));
            return (code != null);
        }
        public List<SalaryDeductionViewModels> List()
        {
            try
            {
                var tmp = from p in context.LS_tblSalaryDeduction
                          select new SalaryDeductionViewModels()
                          { 
                              LSDeductSalaryID = p.LSDeductSalaryID,
                              LSDeductSalaryCode = p.LSDeductSalaryCode,
                              Name = p.Name,
                              VNName = p.VNName,
                              BeforeTax = p.BeforeTax,
                              Used = p.Used,
                              Rank = p.Rank,
                              Note = p.Note
                          };

                List<SalaryDeductionViewModels> ret = new List<SalaryDeductionViewModels>();
                foreach (var item in tmp)
                {
                    SalaryDeductionViewModels s = new SalaryDeductionViewModels()
                    {
                        LSDeductSalaryID = item.LSDeductSalaryID,
                        LSDeductSalaryCode = item.LSDeductSalaryCode,
                        VNName = item.VNName,
                        Used = item.Used,
                        BeforeTax = item.BeforeTax,
                        Rank = item.Rank,
                        Name = item.Name,
                        Note = item.Note
                    };
                    ret.Add(s);
                }
                return ret;
            }
            catch
            {
                return new List<SalaryDeductionViewModels>();
            }
        }
        /// <summary>
        /// Kiem tra duplicate code  khi edit cho truong hop cho chinh sua Code
        /// </summary>
        /// <param name="strValidate"></param>
        /// <returns></returns>
        public bool CheckExistEdit(string strValidate, int LSDeductSalaryID)
        {
            var code = context.LS_tblSalaryDeduction
                        .FirstOrDefault(p => p.LSDeductSalaryCode.Equals(strValidate) && p.LSDeductSalaryID != LSDeductSalaryID);
            return (code != null);
        }
        public LS_tblSalaryDeduction Find(int id)
        {
            return context.LS_tblSalaryDeduction.Find(id);
        }
        public string Add(LS_tblSalaryDeduction model)
        {
            try
            {
                if (CheckExist(model.LSDeductSalaryCode))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return "success";
            }
            catch //(Exception e)
            {
                return "error";
            }
        }
        public string Update(SalaryDeductionViewModels model)
        {

            try
            {
                if (CheckExistEdit(model.LSDeductSalaryCode, model.LSDeductSalaryID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                LS_tblSalaryDeduction modelUpdate = Find(model.LSDeductSalaryID);
                modelUpdate.LSDeductSalaryCode = model.LSDeductSalaryCode;
                modelUpdate.Name = model.Name;
                modelUpdate.VNName = model.VNName;
                modelUpdate.BeforeTax = model.BeforeTax;
                modelUpdate.Rank = model.Rank;
                modelUpdate.Used = model.Used;
                modelUpdate.Note = model.Note;
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return strErr;
            }
        }
        public SalaryDeductionViewModels FindEdit(int id)
        {
            try
            {
                SalaryDeductionViewModels ret = context.LS_tblSalaryDeduction
                                        .Where(p => p.LSDeductSalaryID == id)
                                        .Select(p => new SalaryDeductionViewModels()
                                        {
                                            LSDeductSalaryID = p.LSDeductSalaryID,
                                            LSDeductSalaryCode = p.LSDeductSalaryCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            BeforeTax = p.BeforeTax,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new SalaryDeductionViewModels();
            }

        }
        public bool Delete(int id)
        {
            try
            {
                LS_tblSalaryDeduction modelUpdate = Find(id);
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return false;
            }

        }
        #region Bind DropdownList
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm, int LanguageId)
        {
            var tmp = context.LS_tblSalaryDeduction
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSDeductSalaryID,
                               name = LanguageId == 124 ? c.Name : c.VNName,
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
        public List<AutoCompleteViewModel> ListDropdown(string searchTerm, int pageSize, int pageNume, int LanguageId)
        {
            return GetDropdownList(searchTerm, LanguageId).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }
        #endregion
    }
}
