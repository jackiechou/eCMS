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
    public class GradeRepository
    {
        public EntityDataContext context { get; set; }
        public GradeRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(string strValidate)
        {
            var code = context.PR_tblSalaryGrade.FirstOrDefault(p => p.SalaryGradeCode.Equals(strValidate));
            return (code != null);
        }
        public IEnumerable<GradeViewModels> List(int LanguageId)
        {

            try
            {
                var tmp = from p in context.PR_tblSalaryGrade
                          
                          select new GradeViewModels()
                          { 
                              SalaryGradeID = p.SalaryGradeID,
                              SalaryScaleID = p.SalaryScaleID,
                              SalaryGradeCode = p.SalaryGradeCode,
                              SalaryScaleEN = p.PR_tblSalaryScale.Name,
                              SalaryScaleVN = p.PR_tblSalaryScale.VNName,
                              Name = p.Name,
                              VNName = p.VNName,
                              Used = p.Used,
                              Rank = p.Rank,
                              Note = p.Note
                          };

                List<GradeViewModels> ret = new List<GradeViewModels>();
                foreach (var item in tmp)
                {
                    GradeViewModels s = new GradeViewModels()
                    {
                        SalaryGradeID = item.SalaryGradeID,
                        SalaryScaleID = item.SalaryScaleID,
                        SalaryGradeCode = item.SalaryGradeCode,
                        SalaryScale = ( LanguageId == 124 ? item.SalaryScaleEN : item.SalaryScaleVN),
                        Name = item.Name,
                        VNName = item.VNName,
                        Used = item.Used,
                        Rank = item.Rank,
                        Note = item.Note
                    };
                    ret.Add(s);
                }
                return ret;
            }
            catch
            {
                return new List<GradeViewModels>();
            }
        }
        public bool CheckExistEdit(string strValidate, int SalaryGradeID)
        {
            var code = context.PR_tblSalaryGrade
                        .FirstOrDefault(p => p.SalaryGradeCode.Equals(strValidate) && p.SalaryGradeID != SalaryGradeID);
            return (code != null);
        }
        public PR_tblSalaryGrade Find(int id)
        {
            return context.PR_tblSalaryGrade.Find(id);
        }
        public string Add(PR_tblSalaryGrade model)
        {
            try
            {
                if (CheckExist(model.SalaryGradeCode))
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
        public string Update(GradeViewModels model)
        {
            try
            {
                if (CheckExistEdit(model.SalaryGradeCode, model.SalaryGradeID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                PR_tblSalaryGrade modelUpdate = Find(model.SalaryGradeID);
                modelUpdate.SalaryGradeCode = model.SalaryGradeCode;
                modelUpdate.SalaryScaleID = model.SalaryScaleIDNull.Value;
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
        public GradeViewModels FindEdit(int id, int LanguageId)
        {
            try
            {
                GradeViewModels ret = context.PR_tblSalaryGrade
                                        .Where(p => p.SalaryGradeID == id)
                                        .Select(p => new GradeViewModels()
                                        {
                                            SalaryGradeID = p.SalaryGradeID,
                                            SalaryScaleID = p.SalaryScaleID,
                                            SalaryScaleIDNull = p.SalaryScaleID,
                                            SalaryGradeCode = p.SalaryGradeCode,
                                            //SalaryScale = LanguageId == 124 ? p.Name : p.VNName,
                                            SalaryScale = LanguageId == 124 ? p.PR_tblSalaryScale.Name : p.PR_tblSalaryScale.VNName,
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
                return new GradeViewModels();
            }

        }
        public bool Delete(int id)
        {
            try
            {
                PR_tblSalaryGrade modelUpdate = Find(id);
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
            var tmp = context.PR_tblSalaryGrade
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.SalaryGradeID,
                               name = c.Name + (c.Used == true ? "" : Eagle.Resource.LanguageResource.NotUsed),
                               text = c.VNName + (c.Used == true ? "" : Eagle.Resource.LanguageResource.NotUsed)
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
        private List<AutoCompleteViewModel> GetDropdownListByID(string searchTerm, int LanguageId, int SalaryScaleID)
        {
            var tmp = context.PR_tblSalaryGrade
                           .Where(c => (c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm)) && c.SalaryScaleID == SalaryScaleID)
                           .Select(c => new
                           {
                               id = c.SalaryGradeID,
                               name = c.Name + (c.Used == true ? "" : Eagle.Resource.LanguageResource.NotUsed),
                               text = c.VNName + (c.Used == true ? "" : Eagle.Resource.LanguageResource.NotUsed)
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
        public List<AutoCompleteViewModel> ListDropdownByID(string searchTerm, int pageSize, int pageNume, int LanguageId, int SalaryScaleID)
        {
            return GetDropdownListByID(searchTerm, LanguageId, SalaryScaleID).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }
        #endregion
    }
}
