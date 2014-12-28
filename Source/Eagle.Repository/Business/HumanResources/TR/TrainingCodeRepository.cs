using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using Eagle.Model;

namespace Eagle.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingCodeRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public EntityDataContext context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public TrainingCodeRepository(EntityDataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trainingCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool CheckExist(string trainingCode, out string errorMessage)
        {
            try
            {
                var result = this.context.LS_tblTrainingCode.FirstOrDefault(p => p.Code == trainingCode);
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
        /// <returns></returns>
        public LS_tblTrainingCode Find(int id, out string errorMessage)
        {
            try
            {
                var result = this.context.LS_tblTrainingCode.Find(id);
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
        public List<LS_tblTrainingCode> GetAll(out string errorMessage)
        {
            try
            {
                List<LS_tblTrainingCode> result = this.context.LS_tblTrainingCode.ToList();
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
        /// <param name="errorMassage"></param>
        /// <returns></returns>
        public bool Add(LS_tblTrainingCode model, out string errorMassage)
        {           
            try
            {
                this.context.Entry(model).State = System.Data.Entity.EntityState.Added;
                this.context.SaveChanges();
                errorMassage = String.Empty;
                return true;
            }
            catch (Exception exp)
            {
                errorMassage = exp.Message;
                return false;
            }                        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Update(LS_tblTrainingCode model, out string errorMessage)
        {
            try
            {
                var result = this.Find(model.LSTrainingCodeID, out errorMessage);
                if (result == null)
                {
                    return false;
                }
                result.Code = model.Code;
                result.Name = model.Name;
                result.Note = model.Note;
                result.Rank = model.Rank;
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
        /// <param name="trainingCodeID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Delete(int trainingCodeID, out string errorMessage)
        {
            try
            {
                var result = this.Find(trainingCodeID, out errorMessage);
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

        #region Bind DropdownList Training Code
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm)
        {
            var tmp = context.LS_tblTrainingCode
                           .Where(c => c.Name.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSTrainingCodeID,
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
