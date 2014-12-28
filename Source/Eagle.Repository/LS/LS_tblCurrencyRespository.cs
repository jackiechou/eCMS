using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;
using Eagle.Core;
using Eagle.Model.LS;

namespace Eagle.Repository.LS
{
    public class LS_tblCurrencyRespository
    {
         public EntityDataContext context { get; set; }
         public LS_tblCurrencyRespository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblCurrencyViewModel> List()
        {
            try
            {
                return from p in context.LS_tblCurrency                     
                          select new LS_tblCurrencyViewModel()
                                {
                                    LSCurrencyID = p.LSCurrencyID,
                                    LSCurrencyCode = p.LSCurrencyCode,
                                    Name = p.Name,
                                    VNName = p.VNName,
                                    Rank = p.Rank,
                                    Used = p.Used,
                                    Note = p.Note
                                };
            }
            catch
            {
                return new List<LS_tblCurrencyViewModel>();
            }

            
        }

        public List<BankEntity> GetListForDropDownList()
        {
            try
            {
                return (context.LS_tblCurrency.Select(p => new BankEntity()
                {
                    Id = p.LSCurrencyID,
                    Name = p.Name
                })).ToList();
            }
            catch
            {
                return new List<BankEntity>();
            }
        }

        public List<LS_tblCurrencyViewModel> GetList()
        {
            try
            {
                return (from p in context.LS_tblCurrency
                       select new LS_tblCurrencyViewModel()
                       {
                           LSCurrencyID = p.LSCurrencyID,
                           LSCurrencyCode = p.LSCurrencyCode,
                           Name = p.Name,
                           VNName = p.VNName,
                           Rank = p.Rank,
                           Used = p.Used,
                           Note = p.Note
                       }).ToList();
            }
            catch
            {
                return new List<LS_tblCurrencyViewModel>();
            }
        }
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblCurrency.FirstOrDefault(p => p.LSCurrencyCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblCurrency model, out string errorMessage)
        {
            if (CheckExistCode(model.LSCurrencyCode))
            {
                errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            try
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                errorMessage = "";
            }
            catch(Exception ex)
            { 
                string strerr = ex.Message;
                errorMessage = strerr;
            }
            return true;
        }

        public LS_tblCurrency Find(int id)
        {
            return context.LS_tblCurrency.Find(id);
        }

        public bool Update(LS_tblCurrencyViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblCurrency modelUpdate = Find(model.LSCurrencyID);
                //modelUpdate.LSCurrencyCode = model.LSCurrencyCode;
                modelUpdate.Name = model.Name;
                modelUpdate.VNName = model.VNName;
                modelUpdate.Rank = model.Rank;
                modelUpdate.Used = model.Used;
                modelUpdate.Note = model.Note;
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

        public bool Update(LS_tblCurrency modelUpdate, out string errorMessage)
        {
            try
            {
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                errorMessage = ex.ToString();
                return false;
            }
        }
        public LS_tblCurrencyViewModel FindEditModel(int id)
        {
            LS_tblCurrencyViewModel ret = context.LS_tblCurrency
                                        .Where(p => p.LSCurrencyID == id)                                        
                                        .Select(p => new LS_tblCurrencyViewModel() {
                                            LSCurrencyID = p.LSCurrencyID,
                                            LSCurrencyCode = p.LSCurrencyCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }

        #region Bind DropdownList Bank     
        // dùng cho bind dropdownlist goi tu controller
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNum, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblCurrency
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSCurrencyID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion
    }
}
