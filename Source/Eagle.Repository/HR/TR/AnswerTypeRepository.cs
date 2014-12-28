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
    public class AnswerTypeRepository
    {
        public EntityDataContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AnswerTypeRepository(EntityDataContext Context)
        {
            this.Context = Context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LSAnswerTypeID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public AnswerTypeViewModel FindForEdit(int LSAnswerTypeID, out string errorMessage)
        {
            try
            {
                var found = (from answer in this.Context.LS_tblAnswerType
                             where answer.LSAnswerTypeID == LSAnswerTypeID
                             select new AnswerTypeViewModel()
                             {
                                 LSAnswerTypeID = answer.LSAnswerTypeID,
                                 AnswerTypeCode = answer.AnswerTypeCode,
                                 Name = answer.Name,
                                 Rank = answer.Rank,
                                 Used = answer.Used,
                                 Note = answer.Note

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
        /// <param name="Code"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool IsExisted(string Code, out string errorMessage)
        {
            try
            {
                var found = this.Context.LS_tblAnswerType.Where(p => p.AnswerTypeCode == Code).FirstOrDefault();
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
        /// <param name="errorMessger"></param>
        /// <returns></returns>
        public List<AnswerTypeViewModel> GetListOfAnswerType(out string errorMessger)
        {
            try
            {
                var found = (from answer in this.Context.LS_tblAnswerType
                            select new AnswerTypeViewModel()
                            {
                                LSAnswerTypeID = answer.LSAnswerTypeID,
                                AnswerTypeCode = answer.AnswerTypeCode,
                                Name = answer.Name,
                                Rank = answer.Rank,
                                Used = answer.Used,
                                Note = answer.Note

                            }).ToList();
                errorMessger = String.Empty;

                return found;
            }
            catch (Exception exp)
            {
                errorMessger = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Save(LS_tblAnswerType model, out string errorMessage)
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
        public bool Update(LS_tblAnswerType model, out string errorMessage)
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
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                var model = this.Context.LS_tblAnswerType.Where(p => p.LSAnswerTypeID == id).FirstOrDefault();
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
            var tmp = this.Context.LS_tblAnswerType
                           .Where(c => c.Name.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSAnswerTypeID,
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
