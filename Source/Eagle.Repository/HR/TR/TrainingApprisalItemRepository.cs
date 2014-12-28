using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using Eagle.Model;

namespace Eagle.Repository
{
    public class TrainingApprisalItemRepository
    {
        public EntityDataContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        public TrainingApprisalItemRepository(EntityDataContext Context)
        {
            this.Context = Context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingApprisalItemViewModel> GetListOfTemplateItem(int id, out string errorMessage)
        {
            try
            {
                var found = (from item in this.Context.LS_tblTrainingApprisalItem
                             join answer in this.Context.LS_tblAnswerType on item.LSAnswerTypeID equals answer.LSAnswerTypeID into ItemAnswer
                             from answer in ItemAnswer.DefaultIfEmpty()

                             join part in this.Context.LS_tblTrainingApprisalPart on item.LSTrainingApprisalPartID equals part.LSTrainingApprisalPartID into ItemPart
                             from part in ItemPart.DefaultIfEmpty()

                             where part.LSTrainingApprisalPartID == id
                             select new TrainingApprisalItemViewModel()
                             {
                                 LSTrainingApprisalItemID = item.LSTrainingApprisalItemID,
                                 LSTrainingApprisalPartID = item.LSTrainingApprisalPartID,
                                 LSAnswerTypeID = item.LSAnswerTypeID,
                                 LSTrainingApprisalItemCode = item.LSTrainingApprisalItemCode,
                                 Name = item.Name,
                                 AnswerTypeName = answer.Name,
                                 TrainingApprisalPartName = part.Name,
                                 Rank = item.Rank,
                                 Used = item.Used,
                                 Note = item.Note

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
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public TrainingApprisalItemViewModel FindForEdit(int id, out string errorMessage)
        {
            try
            {
                var found = (from item in this.Context.LS_tblTrainingApprisalItem
                             join answer in this.Context.LS_tblAnswerType on item.LSAnswerTypeID equals answer.LSAnswerTypeID into ItemAnswer
                             from answer in ItemAnswer.DefaultIfEmpty()

                             join part in this.Context.LS_tblTrainingApprisalPart on item.LSTrainingApprisalPartID equals part.LSTrainingApprisalPartID into ItemPart
                             from part in ItemPart.DefaultIfEmpty()

                            where item.LSTrainingApprisalItemID == id
                            select new TrainingApprisalItemViewModel()
                            {
                                LSTrainingApprisalItemID = item.LSTrainingApprisalItemID,
                                LSTrainingApprisalPartID = item.LSTrainingApprisalPartID,
                                LSAnswerTypeID = item.LSAnswerTypeID,
                                LSTrainingApprisalItemCode = item.LSTrainingApprisalItemCode,
                                Name = item.Name,
                                AnswerTypeName = answer.Name,
                                TrainingApprisalPartName = part.Name,
                                Rank = item.Rank,
                                Used = item.Used,
                                Note = item.Note,
                                IsMultiAnswer = item.IsMultiAnswer

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
                var found = this.Context.LS_tblTrainingApprisalItem.Where(p => p.LSTrainingApprisalItemCode == code).FirstOrDefault();
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
        public List<TrainingApprisalItemViewModel> GetOfTrainingApprisalItem(out string errorMessage)
        {
            try
            {
                var found = (from item in this.Context.LS_tblTrainingApprisalItem
                             join answer in this.Context.LS_tblAnswerType on item.LSAnswerTypeID equals answer.LSAnswerTypeID into ItemAnswer
                             from answer in ItemAnswer.DefaultIfEmpty()

                             join part in this.Context.LS_tblTrainingApprisalPart on item.LSTrainingApprisalPartID equals part.LSTrainingApprisalPartID into ItemPart
                             from part in ItemPart.DefaultIfEmpty()
                             select new TrainingApprisalItemViewModel()
                             {
                                 LSTrainingApprisalItemID = item.LSTrainingApprisalItemID,
                                 LSTrainingApprisalPartID = item.LSTrainingApprisalPartID,
                                 LSAnswerTypeID = item.LSAnswerTypeID,
                                 LSTrainingApprisalItemCode = item.LSTrainingApprisalItemCode,
                                 Name = item.Name,
                                 AnswerTypeName = answer.Name,
                                 TrainingApprisalPartName = part.Name,
                                 Rank = item.Rank,
                                 Used = item.Used,
                                 Note = item.Note,
                                 IsMultiAnswer = item.IsMultiAnswer
                                 
                             }).ToList();
                errorMessage = String.Empty;

                if (found != null)
                {
                    foreach (var item in found)
                    {
                        item.IsAllowDelete = this.IsAllowDelete(item.LSTrainingApprisalItemID, out errorMessage);
                    }
                }
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
        /// <param name="TrainingApprisalItemID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool IsAllowDelete(int TrainingApprisalItemID, out string errorMessage)
        {
            try
            {
                errorMessage = String.Empty;                
                var objTemplateItem = this.Context.TR_tblTemplateItem.Where(p => p.LSTrainingApprisalItemID == TrainingApprisalItemID).FirstOrDefault();
                if (objTemplateItem != null)
                {
                    return false;
                }
                var objTrainingEvaluationDetail = this.Context.TR_tblTrainingEvaluationDetail.Where(p => p.LSTrainingApprisalItemID == TrainingApprisalItemID).FirstOrDefault();
                if (objTrainingEvaluationDetail != null)
                {
                    return false;
                }                
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
        public bool Save(LS_tblTrainingApprisalItem model, out string errorMessage)
        {
            try
            {
                this.Context.Entry(model).State = System.Data.Entity.EntityState.Added;
                foreach (var item in model.LS_tblTrainingAnswerType)
                {
                    if (item.LSTrainingAnswerTypeID == 0)
                    {
                        item.LSTrainingApprisalItemID = model.LSTrainingApprisalItemID;
                        this.Context.Entry(item).State = System.Data.Entity.EntityState.Added;
                    }
                }
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
        public bool Update(LS_tblTrainingApprisalItem model, out string errorMessage)
        {
            try
            {
                var objUpdate = this.Context.LS_tblTrainingApprisalItem.Where(p => p.LSTrainingApprisalItemID == model.LSTrainingApprisalItemID).FirstOrDefault();
                if (objUpdate == null)
                {
                    errorMessage = Eagle.Resource.LanguageResource.ObjectNotExistDataForUpdatDelete;
                    return false;
                }

                objUpdate.LSTrainingApprisalItemCode = model.LSTrainingApprisalItemCode;
                objUpdate.Name = model.Name;
                objUpdate.Note = model.Note;
                objUpdate.Rank = model.Rank;
                objUpdate.Used = model.Used;
                objUpdate.IsMultiAnswer = model.IsMultiAnswer;
                objUpdate.LSTrainingApprisalPartID = model.LSTrainingApprisalPartID;
                objUpdate.LSAnswerTypeID = model.LSAnswerTypeID;
                this.Context.Entry(objUpdate).State = System.Data.Entity.EntityState.Modified;

                foreach (var item in model.LS_tblTrainingAnswerType)
                {
                    if (item.LSTrainingAnswerTypeID == 0)
                    {
                        item.LSTrainingApprisalItemID = model.LSTrainingApprisalItemID;
                        this.Context.Entry(item).State = System.Data.Entity.EntityState.Added;
                    }
                }
                var listOfTrainingAnswerFromDB = this.Context.LS_tblTrainingAnswerType.Where(p => p.LSTrainingApprisalItemID == model.LSTrainingApprisalItemID).ToList();
                foreach (var item in listOfTrainingAnswerFromDB)
                {
                    var checking = model.LS_tblTrainingAnswerType.Where(p => p.LSTrainingAnswerTypeID == item.LSTrainingAnswerTypeID).FirstOrDefault();
                    if (checking == null)
                    {
                        this.Context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                    else
                    {
                        this.Context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                }               
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
                var listOfTrainingAnswer = this.Context.LS_tblTrainingAnswerType.Where(p => p.LSTrainingApprisalItemID == id).ToList();
                foreach (var item in listOfTrainingAnswer)
                {
                    this.Context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
                var model = this.Context.LS_tblTrainingApprisalItem.Where(p => p.LSTrainingApprisalItemID == id).FirstOrDefault();
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
