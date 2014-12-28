using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class RecruitmentFeeRepository
    {
        public EntityDataContext context { get; set; }
        public RecruitmentFeeRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<LS_tblRecruitmentFee> GetAll(int LanguageId = 10001)
        {
            return context.LS_tblRecruitmentFee.OrderBy(p => p.Rank).ToList();
        }

        public bool Add(LS_tblRecruitmentFee model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                //Kiểm tra xem Quy trình này đã được thiết lập chưa
                if (CodeIsExists(model.LSRecruitmentFeeCode))
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

        private bool CodeIsExists(string LSRecruitmentFeeCode, int? LSRecruitmentFeeID = null)
        {
            if (LSRecruitmentFeeID == null)
            {
                var OnlineProcess = context
                            .LS_tblRecruitmentFee
                            .FirstOrDefault(p => p.LSRecruitmentFeeCode == LSRecruitmentFeeCode);
                return OnlineProcess != null;
            }
            else
            {
                var OnlineProcess = context
                            .LS_tblRecruitmentFee
                            .FirstOrDefault(p => p.LSRecruitmentFeeCode == LSRecruitmentFeeCode &&
                                p.LSRecruitmentFeeID != LSRecruitmentFeeID);
                return OnlineProcess != null;
            }
        }

        public LS_tblRecruitmentFee Find(int id)
        {
            return context.LS_tblRecruitmentFee.Find(id);
        }

        public bool Update(LS_tblRecruitmentFee model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                if (CodeIsExists(model.LSRecruitmentFeeCode, model.LSRecruitmentFeeID))
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
            var tmp = context.LS_tblRecruitmentFee
                           .Where(c => c.Name.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSRecruitmentFeeID,
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
