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
    public class TrainingEvaluationRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public EntityDataContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public TrainingEvaluationRepository(EntityDataContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingEvaluationViewModel> GetListOfTrainingEvaluation(TrainingEvaluationViewModel model, int EmpID, out string errorMessage)
        {
            try
            {
                var found = (from evaluation in this.Context.TR_tblTrainingEvaluation
                             join template in this.Context.TR_tblEvaluationTemplate on evaluation.EvaluationTemplateID equals template.EvaluationTemplateID into EvaluationTemplate
                             from template in EvaluationTemplate.DefaultIfEmpty()

                             join request in this.Context.TR_tblTrainingRequest on evaluation.TrainingRequestID equals request.TrainingRequestID into EvaluationRequest
                             from request in EvaluationRequest.DefaultIfEmpty()

                             join code in this.Context.LS_tblTrainingCode on request.LSTrainingCodeID equals code.LSTrainingCodeID into RequestCode
                             from code in RequestCode.DefaultIfEmpty()

                             join course in this.Context.LS_tblTrainingCourse on request.LSTrainingCourseID equals course.LSTrainingCourseID into RequestCourse
                             from course in RequestCourse.DefaultIfEmpty()

                             join emp in this.Context.HR_tblEmp on evaluation.EvaluationEmpID equals emp.EmpID into EvaluationEmployee
                             from emp in EvaluationEmployee.DefaultIfEmpty()

                             where (evaluation.EvaluationTemplateID == model.EvaluationTemplateID || model.EvaluationTemplateID == 0) /*&& (evaluation.EvaluationEmpID == EmpID) */
                             orderby evaluation.TrainingEvaluationID descending
                             select new TrainingEvaluationViewModel
                             {
                                 TrainingEvaluationID = evaluation.TrainingEvaluationID,
                                 TrainingRequestID = request.TrainingRequestID,                                 
                                 EvaluationEmpID = evaluation.EvaluationEmpID,
                                 EvaluationTemplateID = evaluation.EvaluationTemplateID,
                                 EvaluationTemplateName = template.EvaluationTemplateName,
                                 EvaluationEmpName = emp.LastName + " " + emp.FirstName,
                                 TrainingCode = code.Code,
                                 TrainingCourseName = course.Name
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
        /// <param name="EvaluationTempalteID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public TrainingEvaluationViewModel GetTrainingEvaluationFromTemplate(int EvaluationTemplateID, out string errorMessage)
        {
            try
            {
                var found = new TrainingEvaluationViewModel();
                found.ListOfTrainingEvaluationPart = (from detail in this.Context.TR_tblEvaluationTemplateDetail
                                                      join part in this.Context.LS_tblTrainingApprisalPart on detail.LSTrainingApprisalPartID equals part.LSTrainingApprisalPartID into DetailPart
                                                      from part in DetailPart.DefaultIfEmpty()

                                                      where detail.EvaluationTemplateID == EvaluationTemplateID
                                                      select new TrainingEvaluationPartViewModel
                                                      {
                                                          TrainingEvaluationID = found.TrainingEvaluationID,
                                                          LSTrainingApprisalPartID = detail.LSTrainingApprisalPartID,
                                                          Note = detail.Note,
                                                          TrainingApprisalPartName = part.Name
                                                      }).ToList();

                foreach (var item in found.ListOfTrainingEvaluationPart)
                {
                    var listOfDetail = (from template in this.Context.TR_tblTemplateItem
                                        join apprisalItem in this.Context.LS_tblTrainingApprisalItem on template.LSTrainingApprisalItemID equals apprisalItem.LSTrainingApprisalItemID into TemplateItem
                                        from apprisalItem in TemplateItem.DefaultIfEmpty()
                                        where template.EvaluationTemplateID == EvaluationTemplateID && template.LSTrainingApprisalPartID == item.LSTrainingApprisalPartID
                                        select new TrainingEvaluationDetailViewModel
                                        {
                                            TrainingEvaluationID = found.TrainingEvaluationID,
                                            LSTrainingApprisalPartID = item.LSTrainingApprisalPartID,
                                            LSTrainingApprisalItemID = apprisalItem.LSTrainingApprisalItemID,
                                            TrainingApprisalItemName = apprisalItem.Name,
                                            Comment = apprisalItem.Note,
                                            IsMultiAnswers = apprisalItem.IsMultiAnswer.HasValue ? apprisalItem.IsMultiAnswer.Value : false

                                        }).ToList();
                    item.ListOfTrainingEvaluationDetail = listOfDetail;
                    foreach (var detail in item.ListOfTrainingEvaluationDetail)
                    {
                        var listOfTrainingAnswer = (from answer in this.Context.LS_tblTrainingAnswerType
                                                    where answer.LSTrainingApprisalItemID == detail.LSTrainingApprisalItemID
                                                    select new TrainingAnswerTypeViewModel
                                                    {
                                                        LSTrainingAnswerTypeID = answer.LSTrainingAnswerTypeID,
                                                        LSTrainingAnswerTypeCode = answer.LSTrainingAnswerTypeCode,
                                                        LSTrainingApprisalItemID = answer.LSTrainingApprisalItemID.Value,
                                                        Name = answer.Name,
                                                        Note = answer.Note,
                                                        Rank = answer.Rank,
                                                        Used = answer.Used,
                                                        Checked = false,
                                                        IsAnswer = false
                                                    }).ToList();

                        detail.ListOfTrainingAnswer = listOfTrainingAnswer;                        
                    }
                }                

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
        /// <param name="TrainingEvaluationID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public TrainingEvaluationViewModel FindForEdit(int TrainingEvaluationID, out string errorMessage)
        {            
            try
            {
                var found = (from evaluation in this.Context.TR_tblTrainingEvaluation
                            join request in this.Context.TR_tblTrainingRequest on evaluation.TrainingRequestID equals request.TrainingRequestID into EvaluationRequest
                            from request in EvaluationRequest.DefaultIfEmpty()

                            join template in this.Context.TR_tblEvaluationTemplate on request.EvaluationTemplateID equals template.EvaluationTemplateID into EvaluationTemplate
                            from template in EvaluationTemplate.DefaultIfEmpty()

                            join emp in this.Context.HR_tblEmp on evaluation.EvaluationEmpID equals emp.EmpID into EvaluationEmployee
                            from emp in EvaluationEmployee.DefaultIfEmpty()

                            join code in this.Context.LS_tblTrainingCode on request.LSTrainingCodeID equals code.LSTrainingCodeID into RequestCode
                            from code in RequestCode.DefaultIfEmpty()

                            join course in this.Context.LS_tblTrainingCourse on request.LSTrainingCourseID equals course.LSTrainingCourseID into RequestCourse
                            from course in RequestCourse.DefaultIfEmpty()

                            where evaluation.TrainingEvaluationID == TrainingEvaluationID
                            select new TrainingEvaluationViewModel
                            {
                                TrainingEvaluationID = evaluation.TrainingEvaluationID,
                                TrainingRequestID = request.TrainingRequestID,
                                EvaluationTemplateID = evaluation.EvaluationTemplateID,
                                EvaluationEmpID = evaluation.EvaluationEmpID,

                                EvaluationTemplateName  = template.EvaluationTemplateName,
                                EvaluationEmpName = emp.LastName + " " + emp.FirstName,
                                IsComplete = evaluation.IsComplete,
                                TrainingCode = code.Code,
                                TrainingCourseName = course.Name
                            }).FirstOrDefault();

                var listOfTrainingEvaluationPart = (from evaluationPart in this.Context.TR_tblTrainingEvaluationPart
                                                    join part in this.Context.LS_tblTrainingApprisalPart on evaluationPart.LSTrainingApprisalPartID equals part.LSTrainingApprisalPartID into EvaluationPart
                                                    from part in EvaluationPart.DefaultIfEmpty()
                                                    where evaluationPart.TrainingEvaluationID == TrainingEvaluationID
                                                    select new TrainingEvaluationPartViewModel
                                                    {
                                                        TrainingEvaluationID = evaluationPart.TrainingEvaluationID,
                                                        LSTrainingApprisalPartID = evaluationPart.LSTrainingApprisalPartID,
                                                        Note = evaluationPart.Note,
                                                        TrainingApprisalPartName = part.Name
                                                    }).ToList();
                found.ListOfTrainingEvaluationPart = listOfTrainingEvaluationPart;

                foreach (var evaluation in found.ListOfTrainingEvaluationPart)
                {
                    var listOfDetail = (from item in this.Context.LS_tblTrainingApprisalItem.Where(p => p.LSTrainingApprisalPartID == evaluation.LSTrainingApprisalPartID)
                                        join detail in this.Context.TR_tblTrainingEvaluationDetail.Where(p => p.TrainingEvaluationID == evaluation.TrainingEvaluationID && p.LSTrainingApprisalPartID == evaluation.LSTrainingApprisalPartID) on item.LSTrainingApprisalItemID equals detail.LSTrainingApprisalItemID into ItemDetail
                                        from detail in ItemDetail.DefaultIfEmpty()                                 
                                        where (detail.TrainingEvaluationID == evaluation.TrainingEvaluationID && detail.LSTrainingApprisalPartID == evaluation.LSTrainingApprisalPartID)
                                        group detail by new { detail.TrainingEvaluationID, detail.LSTrainingApprisalPartID, detail.LSTrainingApprisalItemID } into TrainingDetail
                                        select new TrainingEvaluationDetailViewModel
                                        {                                            
                                            TrainingEvaluationID = TrainingDetail.Key.TrainingEvaluationID,
                                            LSTrainingApprisalPartID = TrainingDetail.Key.LSTrainingApprisalPartID,
                                            LSTrainingApprisalItemID = TrainingDetail.Key.LSTrainingApprisalItemID                                            
                                        }).ToList();
                    foreach (var tmp in listOfDetail)
                    {
                        var item = this.Context.LS_tblTrainingApprisalItem.Where(p => p.LSTrainingApprisalItemID == tmp.LSTrainingApprisalItemID).FirstOrDefault();
                        if (item != null)
                        {
                            tmp.TrainingApprisalItemName = item.Name;
                        }
                    }

                    evaluation.ListOfTrainingEvaluationDetail = listOfDetail;
                    foreach (var detail in evaluation.ListOfTrainingEvaluationDetail)
                    {
                        
                        var listOfTrainingAnswer = (from answer in this.Context.LS_tblTrainingAnswerType.Where(p => p.LSTrainingApprisalItemID == detail.LSTrainingApprisalItemID)
                                                   join ph in this.Context.TR_tblTrainingEvaluationDetail.Where(p => p.TrainingEvaluationID == detail.TrainingEvaluationID && p.LSTrainingApprisalPartID == detail.LSTrainingApprisalPartID && p.LSTrainingApprisalItemID == detail.LSTrainingApprisalItemID)
                                                   on answer.LSTrainingAnswerTypeID equals ph.LSTrainingAnswerTypeID into AnswerDetail
                                                   from ph in AnswerDetail.DefaultIfEmpty()
                                                   
                                                   select new TrainingAnswerTypeViewModel
                                                   {
                                                       TrainingEvaluationDetailID = (ph != null ? ph.TrainingEvaluationDetailID : 0),
                                                       LSTrainingAnswerTypeID = answer.LSTrainingAnswerTypeID,
                                                       LSTrainingAnswerTypeCode = answer.LSTrainingAnswerTypeCode,
                                                       LSTrainingApprisalItemID = answer.LSTrainingApprisalItemID.Value,
                                                       Name = answer.Name,
                                                       Note = answer.Note,
                                                       Rank = answer.Rank,
                                                       Used = answer.Used,
                                                       Checked = (ph != null ? true : false),
                                                       IsAnswer = (ph != null ? true : false)
                                                   }).ToList();

                        detail.ListOfTrainingAnswer = listOfTrainingAnswer;
                    }
                }                           
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
        /// <param name="listOfTrainingEvaluationDetail"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Save(TrainingEvaluationViewModel model, List<TrainingEvaluationDetailViewModel> listOfTrainingEvaluationDetail, out string errorMessage)
        {
            try
            {
                var evaluation = new TR_tblTrainingEvaluation();
                var detailEvaluation = new List<TR_tblTrainingEvaluationDetail>();                                
                var resultOfMapping = this.MappingForAdding(model, listOfTrainingEvaluationDetail, ref evaluation, ref detailEvaluation, out errorMessage);
                if (resultOfMapping == false)
                {
                    errorMessage = "System error";
                    return false;
                }

                foreach (var item in detailEvaluation)
                {
                    if (item.TrainingEvaluationDetailID == 0)
                    {
                        this.Context.Entry(item).State = System.Data.Entity.EntityState.Added;
                    }
                }

                this.Context.Entry(evaluation).State = System.Data.Entity.EntityState.Added;
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
        /// <param name="listOfTrainingEvaluationDetail"></param>
        /// <param name="evaluation"></param>
        /// <param name="detailEvaluation"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool MappingForAdding(TrainingEvaluationViewModel model, List<TrainingEvaluationDetailViewModel> listOfTrainingEvaluationDetail, 
            ref TR_tblTrainingEvaluation evaluation, ref List<TR_tblTrainingEvaluationDetail> detailEvaluation, out string errorMessage)
        {
            try
            {
                AutoMapper.Mapper.CreateMap<TrainingEvaluationViewModel, TR_tblTrainingEvaluation>();
                evaluation = AutoMapper.Mapper.Map<TR_tblTrainingEvaluation>(model);

                evaluation.TR_tblTrainingEvaluationPart = (from part in model.ListOfTrainingEvaluationPart
                                                           select new TR_tblTrainingEvaluationPart
                                                           {
                                                               LSTrainingApprisalPartID = part.LSTrainingApprisalPartID,
                                                               TrainingEvaluationID = part.TrainingEvaluationID
                                                           }).ToList();

                detailEvaluation.Clear();
                var listOfFound = (from detail in listOfTrainingEvaluationDetail
                                   select new TR_tblTrainingEvaluationDetail
                                   {
                                       TrainingEvaluationDetailID = 0,
                                       TrainingEvaluationID = detail.TrainingEvaluationID,
                                       LSTrainingApprisalPartID = detail.LSTrainingApprisalPartID,
                                       LSTrainingApprisalItemID = detail.LSTrainingApprisalItemID,
                                       LSTrainingAnswerTypeID = detail.LSTrainingAnswerTypeID,
                                       IsAnswer = true
                                   }).ToList();

                detailEvaluation = listOfFound;          
                
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
        /// <param name="listOfTrainingEvaluationDetail"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Update(TrainingEvaluationViewModel model, List<TrainingEvaluationDetailViewModel> listOfTrainingEvaluationDetail, out string errorMessage)
        {
            try
            {
                var found = this.Context.TR_tblTrainingEvaluation.Where(p => p.TrainingEvaluationID == model.TrainingEvaluationID).FirstOrDefault();
                if (found == null)
                {
                    errorMessage = Eagle.Resource.LanguageResource.TrainingAnwserTypeNotExistDataForUpdateOrDelete;
                    return false;
                }
                found.TrainingRequestID = model.TrainingRequestID;
                found.EvaluationTemplateID = model.EvaluationTemplateID;                
                found.IsComplete = model.IsComplete;
                found.UpdateDate = model.UpdateDate;
                found.Updater = model.Updater;
                var listOfDetail = this.Context.TR_tblTrainingEvaluationDetail.Where(p => p.TrainingEvaluationID == found.TrainingEvaluationID).ToList();
                if (listOfDetail == null)
                {
                    errorMessage = Eagle.Resource.LanguageResource.TrainingAnwserTypeNotExistDataForUpdateOrDelete;
                    return false;
                }
                foreach (var item in listOfDetail)
                {
                    var checking = listOfTrainingEvaluationDetail.Where(
                        p => p.TrainingEvaluationID == item.TrainingEvaluationID && 
                            p.LSTrainingApprisalPartID == item.LSTrainingApprisalPartID && 
                            p.LSTrainingApprisalItemID == item.LSTrainingApprisalItemID &&
                            p.LSTrainingAnswerTypeID == item.LSTrainingAnswerTypeID).FirstOrDefault();
                    if (checking == null)
                    {
                        this.Context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                    else if (checking.TrainingEvaluationDetailID > 0)
                    {
                        this.Context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                foreach (var item in listOfTrainingEvaluationDetail)
                {
                    if (item.TrainingEvaluationDetailID == 0)
                    {
                        AutoMapper.Mapper.CreateMap<TrainingEvaluationDetailViewModel, TR_tblTrainingEvaluationDetail>();
                        var objMapping = AutoMapper.Mapper.Map<TR_tblTrainingEvaluationDetail>(item);
                        this.Context.Entry(objMapping).State = System.Data.Entity.EntityState.Added;
                    }
                }
                this.Context.Entry(found).State = System.Data.Entity.EntityState.Modified;
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
        /// <param name="TrainingEvaluationID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Delete(int TrainingEvaluationID, out string errorMessage)
        {
            try
            {
                var listOfDetail = this.Context.TR_tblTrainingEvaluationDetail.Where(p => p.TrainingEvaluationID == TrainingEvaluationID).ToList();
                foreach (var item in listOfDetail)
                {
                    this.Context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
                var listOfPart = this.Context.TR_tblTrainingEvaluationPart.Where(p => p.TrainingEvaluationID == TrainingEvaluationID).ToList();
                foreach (var item in listOfPart)
                {
                    this.Context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
                var found = this.Context.TR_tblTrainingEvaluation.Single(p => p.TrainingEvaluationID == TrainingEvaluationID);                
                if (found == null)
                {
                    errorMessage = Eagle.Resource.LanguageResource.TrainingAnwserTypeNotExistDataForUpdateOrDelete;
                    return false;
                }
                this.Context.Entry(found).State = System.Data.Entity.EntityState.Deleted;
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

        #region Bind DropdownList Training Category

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm)
        {
            var tmp = this.Context.TR_tblEvaluationTemplate
                           .Where(c => c.EvaluationTemplateName.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.EvaluationTemplateID,
                               name = c.EvaluationTemplateName,
                               text = c.EvaluationTemplateName
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
        #endregion
    }
}
