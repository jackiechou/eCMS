using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using Eagle.Model;

namespace Eagle.Repository
{
    public class TrainingTypeRepository
    {        
        /// <summary>
        /// 
        /// </summary>
        public EntityDataContext context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public TrainingTypeRepository(EntityDataContext context)
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
                var result = this.context.LS_tblTrainingType.FirstOrDefault(p => p.LSTrainingTypeCode == code);
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
        public LS_tblTrainingType Find(int id, out string errorMessage)
        {
            try
            {
                LS_tblTrainingType result = this.context.LS_tblTrainingType.Find(id);
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
        public List<LS_tblTrainingType> GetAll(out string errorMessage)
        {
            try
            {
                List<LS_tblTrainingType> result = this.context.LS_tblTrainingType.ToList();
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
        public bool Add(LS_tblTrainingType model, out string errorMessage)
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
        public bool Update(LS_tblTrainingType model, out string errorMessage)
        {
            try
            {
                var result = this.Find(model.LSTrainingTypeID, out errorMessage);
                if (result == null)
                {
                    return false;
                }
                result.LSTrainingTypeCode = model.LSTrainingTypeCode;
                result.Name = model.Name;
                result.Rank = model.Rank;
                result.Note = model.Note;
                result.Used = model.Used;
                this.context.Entry(result).State = System.Data.Entity.EntityState.Modified;
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
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                var result = this.Find(id, out errorMessage);
                if (result == null)
                {
                    return false;
                }
                this.context.Entry(result).State = System.Data.Entity.EntityState.Deleted;
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

        #region Bind DropdownList Training Type
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm)
        {
            var tmp = context.LS_tblTrainingType
                           .Where(c => c.Name.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSTrainingTypeID,
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
