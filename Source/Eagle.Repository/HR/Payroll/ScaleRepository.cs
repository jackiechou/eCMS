using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Web.Routing;
using Eagle.Common.Helpers;
using System.Data.Entity.Validation;
using Eagle.Model;
using Eagle.Core;
namespace Eagle.Repository
{
    public class ScaleRepository
    {
        public EntityDataContext context { get; set; }
        public ScaleRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(string strValidate)
        {
            var code = context.PR_tblSalaryScale.FirstOrDefault(p => p.SalaryScaleCode.Equals(strValidate));
            return (code != null);
        }
        public IEnumerable<ScaleCreateViewModels> List()
        {

            try
            {
                var tmp = from p in context.PR_tblSalaryScale
                          select new ScaleCreateViewModels()
                          { 
                              SalaryScaleID = p.SalaryScaleID,
                              SalaryScaleCode = p.SalaryScaleCode,
                              Name = p.Name,
                              VNName = p.VNName,
                              Used = p.Used,
                              Rank = p.Rank,
                              Note = p.Note
                          };

                List<ScaleCreateViewModels> ret = new List<ScaleCreateViewModels>();
                foreach (var item in tmp)
                {
                    ScaleCreateViewModels s = new ScaleCreateViewModels()
                    {
                        SalaryScaleID = item.SalaryScaleID,
                        SalaryScaleCode = item.SalaryScaleCode,
                        VNName = item.VNName,
                        Used = item.Used,
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
                return new List<ScaleCreateViewModels>();
            }
        }
        /// <summary>
        /// Kiem tra duplicate code  khi edit cho truong hop cho chinh sua Code
        /// </summary>
        /// <param name="strValidate"></param>
        /// <returns></returns>
        public bool CheckExistEdit(string strValidate, int SalaryScaleID)
        {
            var code = context.PR_tblSalaryScale
                        .FirstOrDefault(p => p.SalaryScaleCode.Equals(strValidate) && p.SalaryScaleID != SalaryScaleID);
            return (code != null);
        }
        public PR_tblSalaryScale Find(int id)
        {
            return context.PR_tblSalaryScale.Find(id);
        }
        public string Add(PR_tblSalaryScale model)
        {
            try
            {
                if (CheckExist(model.SalaryScaleCode))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return "success";
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public string Update(ScaleCreateViewModels model)
        {

            try
            {
                if (CheckExistEdit(model.SalaryScaleCode, model.SalaryScaleID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                PR_tblSalaryScale modelUpdate = Find(model.SalaryScaleID);
                modelUpdate.SalaryScaleCode = model.SalaryScaleCode;
                modelUpdate.Name = model.Name;
                modelUpdate.VNName = model.VNName;
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
        public ScaleCreateViewModels FindEdit(int id)
        {
            try
            {
                ScaleCreateViewModels ret = context.PR_tblSalaryScale
                                        .Where(p => p.SalaryScaleID == id)
                                        .Select(p => new ScaleCreateViewModels()
                                        {
                                            SalaryScaleID = p.SalaryScaleID,
                                            SalaryScaleCode = p.SalaryScaleCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new ScaleCreateViewModels();
            }

        }
        public bool Delete(int id)
        {
            try
            {
                PR_tblSalaryScale modelUpdate = Find(id);
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
            var tmp = context.PR_tblSalaryScale
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.SalaryScaleID,
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
