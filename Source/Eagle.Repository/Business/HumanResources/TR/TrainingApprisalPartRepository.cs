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
    public class TrainingApprisalPartRepository
    {
        public EntityDataContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        public TrainingApprisalPartRepository(EntityDataContext Context)
        {
            this.Context = Context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public TrainingApprisalPartViewModel FindForEdit(int id, out string errorMessage)
        {
            try
            {
                var found = (from part in this.Context.LS_tblTrainingApprisalPart
                            where part.LSTrainingApprisalPartID == id
                            select new TrainingApprisalPartViewModel()
                            {
                                LSTrainingApprisalPartID = part.LSTrainingApprisalPartID,
                                LSTrainingApprisalPartCode = part.LSTrainingApprisalPartCode,
                                Name = part.Name,
                                Rank = part.Rank,
                                Used = part.Used,
                                Note = part.Note

                            }).FirstOrDefault();
                errorMessage = String.Empty;

                return found;
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
        /// <param name="code"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool IsExisted(string code, out string errorMessage)
        {
            try
            {
                var found = this.Context.LS_tblTrainingApprisalPart.Where(p => p.LSTrainingApprisalPartCode == code).FirstOrDefault();
                errorMessage = String.Empty;

                return (found != null);
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
        /// <param name="LSAnswerTypeID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingApprisalPartViewModel> GetOfTrainingApprisalPart(out string errorMessage)
        {
            try
            {
                var found = (from part in this.Context.LS_tblTrainingApprisalPart
                             select new TrainingApprisalPartViewModel()
                             {
                                 LSTrainingApprisalPartID = part.LSTrainingApprisalPartID,
                                 LSTrainingApprisalPartCode = part.LSTrainingApprisalPartCode,
                                 Name = part.Name,
                                 Rank = part.Rank,
                                 Used = part.Used
                             }).ToList();
                errorMessage = String.Empty;

                return found;
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
        public bool Save(LS_tblTrainingApprisalPart model, out string errorMessage)
        {
            try
            {
                this.Context.Entry(model).State = System.Data.Entity.EntityState.Added;
                this.Context.SaveChanges();
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
        public bool Update(LS_tblTrainingApprisalPart model, out string errorMessage)
        {
            try
            {                            
                this.Context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                this.Context.SaveChanges();
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
                var model = this.Context.LS_tblTrainingApprisalPart.Where(p => p.LSTrainingApprisalPartID == id).FirstOrDefault();
                if (model == null)
                {
                    errorMessage = Eagle.Resource.LanguageResource.TrainingAnwserTypeNotExistDataForUpdateOrDelete;
                    return false;
                }
                this.Context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                this.Context.SaveChanges();
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
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm)
        {
            var tmp = this.Context.LS_tblTrainingApprisalPart
                           .Where(c => c.Name.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSTrainingApprisalPartID,
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNume"></param>
        /// <returns></returns>
        public List<AutoCompleteViewModel> ListDropdown(string searchTerm, int pageSize, int pageNume)
        {
            return GetDropdownList(searchTerm).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }
    }
}
