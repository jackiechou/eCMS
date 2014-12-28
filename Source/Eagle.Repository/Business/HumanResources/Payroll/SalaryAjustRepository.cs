using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using Eagle.Model;

namespace Eagle.Repository
{
    public class SalaryAjustRepository
    {
        public EntityDataContext context { get; set; }

        public SalaryAjustRepository(EntityDataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool CheckExist(string code, out string errorMessage)
        {
            try
            {
                var result = this.context.LS_tblSalaryAdjust.FirstOrDefault(p => p.LSSalaryAdjustCode == code);
                errorMessage = String.Empty;
                return (result != null);
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public LS_tblSalaryAdjust Find(int id, out string errorMessage)
        {
            try
            {
                var result = this.context.LS_tblSalaryAdjust.Find(id);
                errorMessage = String.Empty;
                return result;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public SalaryAdjustModelView FindForEdit(int id, out string errorMessage)
        {
            try
            {
                var result = (from salary in this.context.LS_tblSalaryAdjust
                              where salary.LSSalaryAdjustID == id
                              select new SalaryAdjustModelView()
                              {
                                  LSSalaryAdjustID = salary.LSSalaryAdjustID,
                                  LSSalaryAdjustCode = salary.LSSalaryAdjustCode,
                                  Name = salary.Name,
                                  VNName = salary.VNName,
                                  AddSalary = salary.AddSalary,
                                  PIT = salary.PIT,
                                  Type = salary.Type,
                                  TypeAdd = salary.TypeAdd,
                                  MaxNonPIT = salary.MaxNonPIT,
                                  Rank = salary.Rank,
                                  Note = salary.Note,
                                  Used = salary.Used
                              }).FirstOrDefault();

                errorMessage = String.Empty;
                return result;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<SalaryAdjustModelView> FindAll(out string errorMessage)
        {
            try
            {
                var result = (from salary in this.context.LS_tblSalaryAdjust
                             select new SalaryAdjustModelView()
                             {
                                 LSSalaryAdjustID = salary.LSSalaryAdjustID,
                                 LSSalaryAdjustCode = salary.LSSalaryAdjustCode,
                                 Name = salary.Name,
                                 VNName = salary.VNName,
                                 AddSalary = salary.AddSalary,
                                 PIT = salary.PIT,
                                 Type = salary.Type,
                                 TypeAdd = salary.TypeAdd,
                                 MaxNonPIT = salary.MaxNonPIT,
                                 Rank = salary.Rank,
                                 Note = salary.Note,
                                 Used = salary.Used
                             }).ToList();

                errorMessage = String.Empty;
                return result;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Add(LS_tblSalaryAdjust model, out string errorMessage)
        {
            try
            {
                this.context.Entry(model).State = System.Data.Entity.EntityState.Added;
                this.context.SaveChanges();
                errorMessage = String.Empty;
                return true;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Update(LS_tblSalaryAdjust model, out string errorMessage)
        {
            try
            {
                this.context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                this.context.SaveChanges();
                errorMessage = String.Empty;
                return true;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                SalaryAjustRepository repository = new SalaryAjustRepository(this.context);
                LS_tblSalaryAdjust model = repository.Find(id, out errorMessage);
                if (model == null)
                {
                    return false;
                }
                this.context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                this.context.SaveChanges();
                errorMessage = String.Empty;
                return true;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }

        #region Bind DropdownList
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm, int LanguageId)
        {
            var tmp = context.LS_tblSalaryAdjust
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSSalaryAdjustID,
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
        #endregion
    }
}
