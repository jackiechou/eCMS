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
    public class TrainingAnswerTypeRepository
    {
        public EntityDataContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        public TrainingAnswerTypeRepository(EntityDataContext Context)
        {
            this.Context = Context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrainingApprisalItemID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingAnswerTypeViewModel> GetListOfTrainingAnswerTypeForTrainingItem(int? TrainingApprisalItemID, out string errorMessage)
        {
            try
            {
                var found = (from trainingAnswer in this.Context.LS_tblTrainingAnswerType
                             join item in this.Context.LS_tblTrainingApprisalItem on trainingAnswer.LSTrainingApprisalItemID equals item.LSTrainingApprisalItemID into ItemAnswer
                             from item in ItemAnswer.DefaultIfEmpty()
                             where (trainingAnswer.LSTrainingApprisalItemID == TrainingApprisalItemID)

                             select new TrainingAnswerTypeViewModel()
                             {
                                 LSTrainingAnswerTypeID = trainingAnswer.LSTrainingAnswerTypeID,
                                 LSTrainingAnswerTypeCode = trainingAnswer.LSTrainingAnswerTypeCode,
                                 LSTrainingApprisalItemID = trainingAnswer.LSTrainingApprisalItemID,
                                 Name = trainingAnswer.Name,
                                 Rank = trainingAnswer.Rank,                                 
                                 Used = trainingAnswer.Used

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
        /// <param name="LSAnswerTypeID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingAnswerTypeViewModel> GetListOfTrainingAnswerType(int? LSTrainingApprisalItemID, out string errorMessage)
        {
            try
            {
                var found = (from trainingAnswer in this.Context.LS_tblTrainingAnswerType
                             where (trainingAnswer.LSTrainingApprisalItemID == LSTrainingApprisalItemID || LSTrainingApprisalItemID.HasValue == false)
                             select new TrainingAnswerTypeViewModel()
                             {
                                 LSTrainingAnswerTypeID = trainingAnswer.LSTrainingAnswerTypeID,
                                 LSTrainingAnswerTypeCode = trainingAnswer.LSTrainingAnswerTypeCode,                                 
                                 LSTrainingApprisalItemID = trainingAnswer.LSTrainingApprisalItemID,
                                 Name = trainingAnswer.Name,
                                 Rank = trainingAnswer.Rank,                                 
                                 Used = trainingAnswer.Used

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
        /// <param name="TrainingAnswerTypeID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public TrainingAnswerTypeViewModel FindForEdit(int TrainingAnswerTypeID, out string errorMessage)
        {
            try
            {
                var found = (from trainingAnswer in this.Context.LS_tblTrainingAnswerType                            
                             where trainingAnswer.LSTrainingAnswerTypeID == TrainingAnswerTypeID
                             select new TrainingAnswerTypeViewModel()
                             {
                                 LSTrainingAnswerTypeID = trainingAnswer.LSTrainingAnswerTypeID,
                                 LSTrainingAnswerTypeCode = trainingAnswer.LSTrainingAnswerTypeCode,      
                                 LSTrainingApprisalItemID = trainingAnswer.LSTrainingApprisalItemID,
                                 Name = trainingAnswer.Name,
                                 Rank = trainingAnswer.Rank,
                                 Used = trainingAnswer.Used,
                                 Note = trainingAnswer.Note

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
                var found = this.Context.LS_tblTrainingAnswerType.Where(p => p.LSTrainingAnswerTypeCode == Code).FirstOrDefault();
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
        /// <param name="mode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Save(LS_tblTrainingAnswerType model, out string errorMessage)
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
        /// <param name="mode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Update(LS_tblTrainingAnswerType model, out string errorMessage)
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
        /// <param name="mode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                var model = this.Context.LS_tblTrainingAnswerType.Where(p => p.LSTrainingAnswerTypeID == id).FirstOrDefault();
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
    }
}
