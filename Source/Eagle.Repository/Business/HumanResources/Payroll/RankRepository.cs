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
    public class RankRepository
    {
        public EntityDataContext context { get; set; }
        public RankRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(string strValidate)
        {
            var code = context.PR_tblSalaryRank.FirstOrDefault(p => p.SalaryRankCode.Equals(strValidate));
            return (code != null);
        }
        public IEnumerable<RankViewModels> List(int LanguageId)
        {

            try
            {
                var tmp = from p in context.PR_tblSalaryRank

                          select new RankViewModels()
                          { 
                              SalaryGradeID = p.SalaryGradeID,
                              SalaryRankID = p.SalaryRankID,
                              SalaryRankCode = p.SalaryRankCode,
                              SalaryGradeEN = p.PR_tblSalaryGrade.Name,
                              SalaryGradeVN = p.PR_tblSalaryGrade.VNName,
                              Coef = p.Coef,
                              Name = p.Name,
                              VNName = p.VNName,
                              Used = p.Used,
                              Rank = p.Rank,
                              Note = p.Note
                          };

                List<RankViewModels> ret = new List<RankViewModels>();
                foreach (var item in tmp)
                {
                    RankViewModels s = new RankViewModels()
                    {
                        SalaryGradeID = item.SalaryGradeID,
                        SalaryRankID = item.SalaryRankID,
                        SalaryRankCode = item.SalaryRankCode,
                        SalaryGrade = ( LanguageId == 124 ? item.SalaryGradeEN : item.SalaryGradeVN),
                        Coef = item.Coef,
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
                return new List<RankViewModels>();
            }
        }
        public bool CheckExistEdit(string strValidate, int SalaryRankID)
        {
            var code = context.PR_tblSalaryRank
                        .FirstOrDefault(p => p.SalaryRankCode.Equals(strValidate) && p.SalaryRankID != SalaryRankID);
            return (code != null);
        }
        public PR_tblSalaryRank Find(int id)
        {
            return context.PR_tblSalaryRank.Find(id);
        }
        public string Add(PR_tblSalaryRank model)
        {
            try
            {
                if (CheckExist(model.SalaryRankCode))
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
        public string Update(RankViewModels model)
        {
            try
            {
                if (CheckExistEdit(model.SalaryRankCode, model.SalaryRankID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                PR_tblSalaryRank modelUpdate = Find(model.SalaryRankID);
                modelUpdate.SalaryRankCode = model.SalaryRankCode;
                modelUpdate.SalaryGradeID = model.SalaryGradeIDNull.Value;
                modelUpdate.Name = model.Name;
                modelUpdate.VNName = model.VNName;
                modelUpdate.Coef = model.Coef;
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
        public RankViewModels FindEdit(int id, int LanguageId)
        {
            try
            {
                RankViewModels ret = context.PR_tblSalaryRank
                                        .Where(p => p.SalaryRankID == id)
                                        .Select(p => new RankViewModels()
                                        {
                                            SalaryGradeID = p.SalaryGradeID,
                                            SalaryRankID = p.SalaryRankID,
                                            SalaryGradeIDNull = p.SalaryGradeID,
                                            SalaryRankCode = p.SalaryRankCode,
                                            SalaryGrade = LanguageId == 124 ? p.PR_tblSalaryGrade.Name : p.PR_tblSalaryGrade.VNName,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Coef = p.Coef,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new RankViewModels();
            }

        }
        public bool Delete(int id)
        {
            try
            {
                PR_tblSalaryRank modelUpdate = Find(id);
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
            var tmp = context.PR_tblSalaryRank
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.SalaryRankID,
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
        private List<AutoCompleteViewModel> GetDropdownListByID(string searchTerm, int LanguageId, int SalaryGradeID)
        {
            var tmp = context.PR_tblSalaryRank
                           .Where(c => (c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm)) && c.SalaryGradeID == SalaryGradeID)
                           .Select(c => new
                           {
                               id = c.SalaryRankID,
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
        // dùng cho bind dropdownlist
        public List<AutoCompleteViewModel> ListDropdownByID(string searchTerm, int pageSize, int pageNume, int LanguageId, int SalaryGradeID)
        {
            return GetDropdownListByID(searchTerm, LanguageId,SalaryGradeID).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }
        #endregion
    }
}
