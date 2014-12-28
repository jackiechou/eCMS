using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class RecruitmentSourceRepository
    {
        public EntityDataContext context { get; set; }
        public RecruitmentSourceRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<LS_tblRecruitmentSource> GetAll(int LanguageId = 10001)
        {
            return context.LS_tblRecruitmentSource.OrderBy(p => p.Rank).ToList();
        }

        public bool Add(LS_tblRecruitmentSource model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                //Kiểm tra xem Quy trình này đã được thiết lập chưa
                if (CodeIsExists(model.LSRecruitmentSourceCode))
                {
                    ErrorMessage = Eagle.Resource.LanguageResource.CodeIsExists;
                    return false;
                }

                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return true;
            }
            catch
            {
                ErrorMessage = Eagle.Resource.LanguageResource.SystemError;
                return false;
            }
        }

        private bool CodeIsExists(string LSRecruitmentSourceCode, int? LSRecruitmentSourceID = null)
        {
            if (LSRecruitmentSourceID == null)
            {
                var OnlineProcess = context
                            .LS_tblRecruitmentSource
                            .FirstOrDefault(p => p.LSRecruitmentSourceCode == LSRecruitmentSourceCode);
                return OnlineProcess != null;
            }
            else
            {
                var OnlineProcess = context
                            .LS_tblRecruitmentSource
                            .FirstOrDefault(p => p.LSRecruitmentSourceCode == LSRecruitmentSourceCode &&
                                p.LSRecruitmentSourceID != LSRecruitmentSourceID);
                return OnlineProcess != null;
            }
        }

        public LS_tblRecruitmentSource Find(int id)
        {
            return context.LS_tblRecruitmentSource.Find(id);
        }

        public bool Update(LS_tblRecruitmentSource model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                if (CodeIsExists(model.LSRecruitmentSourceCode, model.LSRecruitmentSourceID))
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
            var tmp = context.LS_tblRecruitmentSource
                           .Where(c => c.Name.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSRecruitmentSourceID,
                               name = c.Name,
                               text = c.Name
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
