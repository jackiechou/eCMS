using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class InterviewCriteriaRepository
    {
        public EntityDataContext context { get; set; }
        public InterviewCriteriaRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<InterviewCriteriaViewModel> GetAll(int LanguageId = 10001)
        {
            var result = from ic in context.REC_tblInterviewCriteria
                         join type in context.REC_tblInterviewCriteriaType on ic.InterviewCriteriaTypeID equals type.InterviewCriteriaTypeID
                         orderby new { typeRank = ic.REC_tblInterviewCriteriaType.Rank, rank = ic.Rank }
                         select new InterviewCriteriaViewModel()
                         {
                              InterviewCriteriaID = ic.InterviewCriteriaID,
                              Code = ic.Code,
                              InterviewCriteriaTypeID = ic.InterviewCriteriaTypeID,
                              Name = ic.Name,
                              Rank = ic.Rank,
                              Used = ic.Used,
                              Note = ic.Note,
                              InterviewCriteriaTypeName = type.Name
                         };
            return result.ToList();
        }

        public bool Add(REC_tblInterviewCriteria model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                //Kiểm tra xem Quy trình này đã được thiết lập chưa
                if (CodeIsExists(model.Code))
                {
                    ErrorMessage = Eagle.Resource.LanguageResource.CodeIsExists;
                    return false;
                }

                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        private bool CodeIsExists(string Code, int? InterviewCriteriaID = null)
        {
            if (InterviewCriteriaID == null)
            {
                var OnlineProcess = context
                            .REC_tblInterviewCriteria
                            .FirstOrDefault(p => p.Code == Code);
                return OnlineProcess != null;
            }
            else
            {
                var OnlineProcess = context
                            .REC_tblInterviewCriteria
                            .FirstOrDefault(p => p.Code == Code &&
                                p.InterviewCriteriaID != InterviewCriteriaID);
                return OnlineProcess != null;
            }
        }

        public REC_tblInterviewCriteria Find(int id)
        {
            return context.REC_tblInterviewCriteria.Find(id);
        }

        public InterviewCriteriaViewModel FindEdit(int id)
        {
            var result = (from ic in context.REC_tblInterviewCriteria
                          join type in context.REC_tblInterviewCriteriaType on ic.InterviewCriteriaTypeID equals type.InterviewCriteriaTypeID
                          where ic.InterviewCriteriaID == id
                          select new InterviewCriteriaViewModel() {
                              InterviewCriteriaID = ic.InterviewCriteriaID,
                              Code = ic.Code,
                              InterviewCriteriaTypeID = ic.InterviewCriteriaTypeID,
                              Name = ic.Name,
                              Rank = ic.Rank,
                              Used = ic.Used,
                              Note = ic.Note,
                              InterviewCriteriaTypeName = type.Name
                          }).FirstOrDefault();
            return result;
        }
        public bool Update(REC_tblInterviewCriteria model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                if (CodeIsExists(model.Code, model.InterviewCriteriaID))
                {
                    ErrorMessage = Eagle.Resource.LanguageResource.CodeIsExists;
                    return false;
                }
                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool Delete(int id, out string ErrorMessage)
        {
            try
            {
                var model = Find(id);
                if (model != null)
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
                ErrorMessage = "";
                return true;
            }
            catch( Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

        }

        #region Bind DropdownList 
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm)
        {
            var tmp = context.REC_tblInterviewCriteria
                           .Where(c => c.Name.Contains(searchTerm))
                           .OrderBy(c => c.Rank)
                           .Select(c => new
                           {
                               id = c.InterviewCriteriaID,
                               name = c.Name + (c.Used == true ? "" : Eagle.Resource.LanguageResource.NotUsed),
                               text = c.Name + (c.Used == true ? "" : Eagle.Resource.LanguageResource.NotUsed)
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
        // dùng cho bind dropdownlist goi tu controller
        public List<AutoCompleteViewModel> ListDropdown(string searchTerm, int pageSize, int pageNume)
        {
            return GetDropdownList(searchTerm).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }
        #endregion
    }
}
