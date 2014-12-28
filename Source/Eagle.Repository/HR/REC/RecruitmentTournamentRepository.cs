using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class RecruitmentTournamentRepository
    {
        public EntityDataContext context { get; set; }
        public RecruitmentTournamentRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<LS_tblRecruitmentTournament> GetAll(int LanguageId = 10001)
        {
            return context.LS_tblRecruitmentTournament.OrderBy(p => p.Rank).ToList();
        }

        public bool Add(LS_tblRecruitmentTournament model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                //Kiểm tra xem Quy trình này đã được thiết lập chưa
                if (CodeIsExists(model.LSRecruitmentTournamentCode))
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

        private bool CodeIsExists(string LSRecruitmentTournamentCode, int? LSRecruitmentTournamentID = null)
        {
            if (LSRecruitmentTournamentID == null)
            {
                var OnlineProcess = context
                            .LS_tblRecruitmentTournament
                            .FirstOrDefault(p => p.LSRecruitmentTournamentCode == LSRecruitmentTournamentCode);
                return OnlineProcess != null;
            }
            else
            {
                var OnlineProcess = context
                            .LS_tblRecruitmentTournament
                            .FirstOrDefault(p => p.LSRecruitmentTournamentCode == LSRecruitmentTournamentCode &&
                                p.LSRecruitmentTournamentID != LSRecruitmentTournamentID);
                return OnlineProcess != null;
            }
        }

        public LS_tblRecruitmentTournament Find(int id)
        {
            return context.LS_tblRecruitmentTournament.Find(id);
        }

        public bool Update(LS_tblRecruitmentTournament model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                if (CodeIsExists(model.LSRecruitmentTournamentCode, model.LSRecruitmentTournamentID))
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
            var tmp = context.LS_tblRecruitmentTournament
                           .Where(c => c.Name.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSRecruitmentTournamentID,
                               name = c.Name + (c.Used == true ?"":Eagle.Resource.LanguageResource.NotUsed),
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
