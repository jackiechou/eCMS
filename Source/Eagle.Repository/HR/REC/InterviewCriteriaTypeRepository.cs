using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class InterviewCriteriaTypeRepository
    {
        public EntityDataContext context { get; set; }
        public InterviewCriteriaTypeRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<REC_tblInterviewCriteriaType> GetAll(int LanguageId = 10001)
        {
            return context.REC_tblInterviewCriteriaType.OrderBy(p => p.Rank).ToList();
        }

        public bool Add(REC_tblInterviewCriteriaType model, out string ErrorMessage)
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

        private bool CodeIsExists(string Code, int? InterviewCriteriaTypeID = null)
        {
            if (InterviewCriteriaTypeID == null)
            {
                var OnlineProcess = context
                            .REC_tblInterviewCriteriaType
                            .FirstOrDefault(p => p.Code == Code);
                return OnlineProcess != null;
            }
            else
            {
                var OnlineProcess = context
                            .REC_tblInterviewCriteriaType
                            .FirstOrDefault(p => p.Code == Code &&
                                p.InterviewCriteriaTypeID != InterviewCriteriaTypeID);
                return OnlineProcess != null;
            }
        }

        public REC_tblInterviewCriteriaType Find(int id)
        {
            return context.REC_tblInterviewCriteriaType.Find(id);
        }

        public bool Update(REC_tblInterviewCriteriaType model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                if (CodeIsExists(model.Code, model.InterviewCriteriaTypeID))
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
            var tmp = context.REC_tblInterviewCriteriaType
                           .Where(c => c.Name.Contains(searchTerm))
                           .OrderBy(c => c.Rank)
                           .Select(c => new
                           {
                               id = c.InterviewCriteriaTypeID,
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
