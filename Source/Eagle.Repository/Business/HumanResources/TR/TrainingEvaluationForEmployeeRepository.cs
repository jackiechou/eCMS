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
    public class TrainingEvaluationForEmployeeRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public const int TRAININGREQUEST_FUNCTIONID = 476; 

        /// <summary>
        /// 
        /// </summary>
        public EntityDataContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        public TrainingEvaluationForEmployeeRepository(EntityDataContext Context)
        {
            this.Context = Context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingRequestViewModel> GetListOfTrainingRequestForEvaluation(AccountViewModel account, int ModuleID, TrainingRequestSearchViewModel model, out string errorMessage)
        {
            try
            {
                var listOfUser = this.Context.SYS_spfrmDataPermission(account.UserName, ModuleID).ToList();
                var found = new List<TrainingRequestViewModel>();
                if (!model.FromDate.HasValue)
                {
                    model.FromDate = DateTime.Now;
                }
                if (!model.ToDate.HasValue)
                {
                    model.ToDate = DateTime.Now;
                }
                var company = Context.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID).FirstOrDefault();
                string Lineage = String.Empty;
                if (company != null)
                {
                    Lineage = company.Lineage;
                }
                found = (from request in this.Context.TR_tblTrainingRequest
                         join duration in this.Context.TR_tblTrainingRequestDuration on request.TrainingRequestID equals duration.TrainingRequestID into RequestDuration
                         from duration in RequestDuration.DefaultIfEmpty()

                         join du in this.Context.TR_tblDurationUser on duration.TrainingDurationID equals du.TrainingDurationID into DurationUser
                         from du in DurationUser.DefaultIfEmpty()

                         join par in this.Context.HR_tblEmp on du.EmpID equals par.EmpID into DurationEmployee
                         from par in DurationEmployee.DefaultIfEmpty()

                         join code in this.Context.LS_tblTrainingCode on request.LSTrainingCodeID equals code.LSTrainingCodeID into RequestCode
                         from code in RequestCode.DefaultIfEmpty()

                         join course in this.Context.LS_tblTrainingCourse on request.LSTrainingCourseID equals course.LSTrainingCourseID into RequestCourse
                         from course in RequestCourse.DefaultIfEmpty()

                         join user in this.Context.HR_tblEmp on request.EmpIDCreate equals user.EmpID into PlanUser
                         from user in PlanUser.DefaultIfEmpty()

                         join comz in this.Context.LS_tblCompany on request.LSCompanyID equals comz.LSCompanyID into RequestCom
                         from com in RequestCom.DefaultIfEmpty()

                         join template in this.Context.TR_tblEvaluationTemplate on request.EvaluationTemplateID equals template.EvaluationTemplateID into RequestTemplate
                         from template in RequestTemplate.DefaultIfEmpty()

                         join trainingEvaluation in this.Context.TR_tblTrainingEvaluation on request.TrainingRequestID equals trainingEvaluation.TrainingRequestID into RequestTrainingEvaluation
                         from trainingEvaluation in RequestTrainingEvaluation.DefaultIfEmpty()

                         where (request.LSTrainingCodeID == model.LSTrainingCodeID || (!model.LSTrainingCodeID.HasValue)) &&
                         (request.LSTrainingCourseID == model.LSTrainingCourseID || (!model.LSTrainingCourseID.HasValue)) &&
                         (com.Lineage.StartsWith(Lineage) || String.IsNullOrEmpty(Lineage)) &&
                         (
                            (request.TR_tblTrainingRequestDuration.Where(p => p.FromDate <= model.FromDate && model.FromDate <= p.ToDate && p.FromDate <= model.ToDate && model.ToDate <= p.ToDate).FirstOrDefault() != null) ||
                            (request.TR_tblTrainingRequestDuration.Where(p => model.FromDate <= p.FromDate && model.FromDate <= p.ToDate && p.FromDate <= model.ToDate && p.ToDate <= model.ToDate).FirstOrDefault() != null) ||
                            (request.TR_tblTrainingRequestDuration.Where(p => model.FromDate <= p.FromDate && model.FromDate <= p.ToDate && p.FromDate <= model.ToDate && model.ToDate <= p.ToDate).FirstOrDefault() != null) ||
                            (request.TR_tblTrainingRequestDuration.Where(p => p.FromDate <= model.FromDate && model.FromDate <= p.ToDate && p.FromDate <= model.ToDate && p.ToDate <= model.ToDate).FirstOrDefault() != null) ||
                            (!model.FromDate.HasValue) || (!model.ToDate.HasValue)
                         ) &&
                         (request.Status == model.Status || model.Status == -1 || (!model.Status.HasValue))
                             // Data Permission 
                         && (listOfUser.Contains(par.EmpID))
                         && (request.EmpIDCreate == account.EmpId || (request.EmpIDCreate != account.EmpId && request.Status != 0))
                         && (par.EmpCode.Contains(model.EmpCode) || (String.IsNullOrEmpty(model.EmpCode)))
                         && ((par.LastName + " " + par.FirstName).ToUpper().Contains(model.EmployeeName.ToUpper()) || String.IsNullOrEmpty(model.EmployeeName))
                         orderby par.FirstName ascending, course.Name ascending

                         select new TrainingRequestViewModel()
                         {                             
                             EmployeeEvaluationID = par.EmpID, 
                             EmployeeEvaluation = par.LastName + " " + par.FirstName,
                             FromDate = duration.FromDate,
                             ToDate = duration.ToDate,
                             TrainingCode = code.Code,
                             TrainingCodeName = code.Name,
                             TrainingCourseName = course.Name,
                             EvaluationTemplateName = template.EvaluationTemplateName,
                             TrainingEvaluationID = trainingEvaluation.TrainingEvaluationID,
                             // Information of TrainingRequest
                             TrainingRequestID = request.TrainingRequestID,
                             EmpIDCreate = request.EmpIDCreate,
                             LSCompanyID = request.LSCompanyID,
                             LSTrainingCodeID = request.LSTrainingCodeID,
                             LSTrainingCategoryID = request.LSTrainingCategoryID,
                             LSTrainingCourseID = request.LSTrainingCourseID,
                             LSProviderID = request.LSProviderID,
                             LearningObjective = request.LearningObjective,
                             LSTrainingTypeID = request.LSTrainingTypeID,
                             LSTrainingFormID = request.LSTrainingFormID,
                             LSTrainingLocationID = request.LSTrainingLocationID,
                             NumOfStaff = request.NumOfStaff,
                             TrainingType = request.TrainingType,
                             TrainingPlanID = request.TrainingPlanID,
                             Status = request.Status,
                             TotalCost = request.TotalCost,
                             EvaluationRequired = request.EvaluationRequired,
                             EvaluationTemplateID = request.EvaluationTemplateID,
                             CourseCompleteStatus = request.CourseCompleteStatus,
                             Comment = request.Comment,
                             Company = com.Name,                             

                             RequestUserName = user.LastName + " " + user.FirstName

                         }).ToList();

                foreach (var item in found)
                {
                    item.IsMaxLevelApproval = this.IsMaxLevelApproval(item.TrainingRequestID, out errorMessage);
                    item.IsEvaluated = this.CheckingTrainingEvaluationByEmployee(item.TrainingRequestID, item.EmployeeEvaluationID, out errorMessage);
                    if (item.ToDate <= DateTime.Now)
                    {
                        item.AllowEvaluationByDuraionEnded = true;
                    }                    
                }
                errorMessage = String.Empty;
                if (model.StatusOfEvaluation == 0)
                {
                    return found;
                }
                if (model.StatusOfEvaluation == 1)
                {
                    return found.Where(p => p.IsEvaluated == true).ToList();
                }
                return found.Where(p => p.IsEvaluated == false).ToList();
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
        /// <param name="EmployeeID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingRequestViewModel> GetListOfTrainingRequest(AccountViewModel account, int ModuleID, TrainingRequestSearchViewModel model, int EmployeeID, out string errorMessage)
        {
            try
            {
                var listOfUser = this.Context.SYS_spfrmDataPermission(account.UserName, ModuleID).ToList();
                var listOfFound = new List<TrainingRequestViewModel>();
                var listOfTrainingRequestID = (from employee in this.Context.TR_tblDurationUser
                                              join duration in this.Context.TR_tblTrainingRequestDuration on employee.TrainingDurationID equals duration.TrainingDurationID into EmployeeDuration
                                              from duration in EmployeeDuration.DefaultIfEmpty()

                                              join request in this.Context.TR_tblTrainingRequest on duration.TrainingRequestID equals request.TrainingRequestID into DurationResquest
                                              from request in DurationResquest.DefaultIfEmpty()

                                              join user in this.Context.HR_tblEmp on request.EmpIDCreate equals user.EmpID into PlanUser
                                              from user in PlanUser.DefaultIfEmpty()
                                             
                                              where (request.LSTrainingCodeID == model.LSTrainingCodeID || !model.LSTrainingCodeID.HasValue) &&
                                              (request.LSTrainingCourseID == model.LSTrainingCourseID || !model.LSTrainingCourseID.HasValue) &&
                                              (
                                                (duration.FromDate <= model.FromDate && (model.FromDate <= duration.ToDate) && (duration.ToDate <= model.ToDate)) ||
                                                (model.ToDate <= duration.ToDate && (model.FromDate <= duration.FromDate && duration.FromDate <= model.ToDate)) ||
                                                (model.FromDate <= duration.FromDate && duration.ToDate <= model.ToDate) ||
                                                (duration.FromDate <= model.FromDate && model.ToDate <= duration.ToDate)
                                              )
                                              && (request.Status == model.Status || model.Status == -1 || (!model.Status.HasValue))
                                              // Data Permission
                                              && (listOfUser.Contains(request.EmpIDCreate))
                                              && (request.EmpIDCreate == account.EmpId || (request.EmpIDCreate != account.EmpId && request.Status != 0))
                                              && (user.EmpCode.Contains(model.EmpCode) || (String.IsNullOrEmpty(model.EmpCode)))
                                              && ((user.LastName + " " + user.FirstName).ToUpper().Contains(model.EmployeeName.ToUpper()) || String.IsNullOrEmpty(model.EmployeeName))

                                              group duration by duration.TrainingRequestID into TrainingRequest
                                              orderby TrainingRequest.Key descending
                                              select TrainingRequest.Key).ToList();
                
                if (listOfTrainingRequestID != null)
                {
                    foreach (var item in listOfTrainingRequestID)
                    {
                        var repository = new TrainingRequestRepository(this.Context);
                        var found = repository.FindForEdit(item, out errorMessage);
                        if (found != null)
                        {
                            found.IsMaxLevelApproval = this.IsMaxLevelApproval(item, out errorMessage);
                            found.IsEvaluated = this.CheckingTrainingEvaluationByEmployee(item, EmployeeID, out errorMessage);
                            var listOfTrainingDuration = repository.GetListOfTrainingRequestDuration(item, out errorMessage);
                            if (listOfTrainingDuration != null)
                            {
                                var EndDateDuration = DateTime.Now;
                                foreach (var duration in listOfTrainingDuration)
                                {
                                    found.DurationInfoForTrainingEvaluation += String.Format("({0} - {1})", duration.FromDate.Value.ToString("dd/MM/yyyy"), duration.ToDate.Value.ToString("dd/MM/yyyy"));
                                    if (duration.ToDate > EndDateDuration)
                                    {
                                        EndDateDuration = duration.ToDate.Value;
                                    }
                                }
                                if (EndDateDuration == DateTime.Now)
                                {
                                    found.AllowEvaluationByDuraionEnded = true;
                                }
                            }                            
                            listOfFound.Add(found);
                        }                                                
                    }
                }                                
                errorMessage = String.Empty;

                return listOfFound;
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
        /// <param name="TrainingRequestID"></param>
        /// <param name="EmpID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool CheckingTrainingEvaluationByEmployee(int TrainingRequestID, int EmpID, out string errorMessage)
        {
            try
            {

                var checking = this.Context.TR_tblTrainingEvaluation.Where(p => p.TrainingRequestID == TrainingRequestID && p.EvaluationEmpID == EmpID).FirstOrDefault();
                errorMessage = String.Empty;

                return (checking != null ? true : false);
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrainingRequest"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool IsFullOfConditionsForCourseComplete(int TrainingRequestID, out string errorMessage)
        {
            try
            {
                var listOfEmpID = from employee in this.Context.TR_tblDurationUser
                                              join duration in this.Context.TR_tblTrainingRequestDuration.Where(p => p.TrainingRequestID == TrainingRequestID) on employee.TrainingDurationID equals duration.TrainingDurationID into EmployeeDuration                                              
                                              from duration in EmployeeDuration.DefaultIfEmpty()
                                              group employee by employee.EmpID into RequestEmployee
                                              orderby RequestEmployee.Key
                                              select RequestEmployee.Key;
                if (listOfEmpID != null)
                {
                    var checking = true;
                    foreach (var empID in listOfEmpID)
                    {
                        if (!this.CheckingTrainingEvaluationByEmployee(TrainingRequestID, empID, out errorMessage))
                        {
                            checking = false;
                            break;
                        }
                    }
                    errorMessage = String.Empty;

                    return checking;
                }
                errorMessage = String.Empty;
                
                return false;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrainingRequestID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool IsMaxLevelApproval(int TrainingRequestID, out string errorMessage)
        {
            try
            {
                var processOnlineInfo = this.Context.LS_tblOnlineProcess.Where(p => p.FunctionID == TRAININGREQUEST_FUNCTIONID).FirstOrDefault();
                int maxLevelApprove = 3;
                if (processOnlineInfo == null)
                {
                    throw new NullReferenceException();
                }
                maxLevelApprove = processOnlineInfo.NoOfLevel;                
                var found = this.Context.TR_tblTrainingRequest.Where(p => p.TrainingRequestID == TrainingRequestID).FirstOrDefault();
                if (found == null)
                {
                    errorMessage = Eagle.Resource.LanguageResource.ObjectNotExistDataForUpdatDelete;
                    throw new NullReferenceException();
                }
                if (found.LevelApprove == maxLevelApprove && found.Status == maxLevelApprove * 2)
                {
                    errorMessage = String.Empty;
                    return true;
                }
                errorMessage = String.Empty;
                return false;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                throw exp;
            }
        }
    }
}
