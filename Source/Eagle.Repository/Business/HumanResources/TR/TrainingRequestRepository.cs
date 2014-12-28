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
    public class TrainingRequestRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public EntityDataContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        public TrainingRequestRepository(EntityDataContext Context)
        {
            this.Context = Context;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingRequestViewModel> GetListOfTrainingRequest(AccountViewModel account, int ModuleID, TrainingRequestSearchViewModel model, out string errorMessage)
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

                         join code in this.Context.LS_tblTrainingCode on request.LSTrainingCodeID equals code.LSTrainingCodeID into RequestCode
                         from code in RequestCode.DefaultIfEmpty()

                         join course in this.Context.LS_tblTrainingCourse on request.LSTrainingCourseID equals course.LSTrainingCourseID into RequestCourse
                         from course in RequestCourse.DefaultIfEmpty()

                         join user in this.Context.HR_tblEmp on request.EmpIDCreate equals user.EmpID into PlanUser
                         from user in PlanUser.DefaultIfEmpty()

                         join comz in this.Context.LS_tblCompany on request.LSCompanyID equals comz.LSCompanyID into RequestCom
                         from com in RequestCom.DefaultIfEmpty()

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
                         && (listOfUser.Contains(request.EmpIDCreate))
                         && (request.EmpIDCreate == account.EmpId || (request.EmpIDCreate != account.EmpId && request.Status != 0))                         
                         && (user.EmpCode.Contains(model.EmpCode) || (String.IsNullOrEmpty(model.EmpCode)))
                         && ((user.LastName + " " + user.FirstName).ToUpper().Contains(model.EmployeeName.ToUpper()) || String.IsNullOrEmpty(model.EmployeeName))
                         orderby request.TrainingRequestID descending

                         select new TrainingRequestViewModel()
                         {
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
                             TrainingCode = code.Code,
                             TrainingCodeName = code.Name,
                             TrainingCourseName = course.Name,

                             RequestUserName = user.LastName + " " + user.FirstName

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
        /// <param name="TrainingPlanID"></param>
        /// <returns></returns>
        public List<TrainingRequestDurationViewModel> GetListOfTrainingRequestDuration(int TrainingRequestID, out string errorMessage)
        {
            try
            {
                var found = (from duration in this.Context.TR_tblTrainingRequestDuration                                                          
                            where duration.TrainingRequestID == TrainingRequestID
                            select new TrainingRequestDurationViewModel()
                            {   
                                TrainingDurationID = duration.TrainingDurationID,
                                TrainingRequestID = duration.TrainingRequestID,
                                FromDate = duration.FromDate,
                                ToDate = duration.ToDate,
                                TotalDays = duration.TotalDays,
                                TotalDaysAllowNull = duration.TotalDays,
                                TotalHours = duration.TotalHours,                                
                            }).ToList();

                foreach (var item in found)
                {
                    if (item.FromDate.HasValue)
                    {
                        item.FromDateInfo = item.FromDate.Value.ToString("dd/MM/yyyy");
                    }
                    if (item.ToDate.HasValue)
                    {
                        item.ToDateInfo = item.ToDate.Value.ToString("dd/MM/yyyy");
                    }
                }                                       
            
                // Setting of key
                var i = 0;
                foreach (var item in found)
                {
                    item.Key = i++;
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
        /// <param name="TrainingDurationID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingRequestDurationUserViewModel> GetListOfTrainingRequestDurationUser(int? TrainingRequestID, out string errorMessage)
        {
            try
            {
                var found = (from durationUser in this.Context.TR_tblDurationUser
                             join duration in this.Context.TR_tblTrainingRequestDuration on durationUser.TrainingDurationID equals duration.TrainingDurationID into DurationUser
                             from duration in DurationUser.DefaultIfEmpty()

                             join emp in this.Context.HR_tblEmp on durationUser.EmpID equals emp.EmpID into UserEmployee
                             from emp in UserEmployee.DefaultIfEmpty()                             
                                                          
                             join com in this.Context.LS_tblCompany on emp.LSCompanyID equals com.LSCompanyID into EmployeeCompany
                             from com in EmployeeCompany.DefaultIfEmpty()

                             join pos in this.Context.LS_tblPosition on emp.LSPositionID equals pos.LSPositionID into EmployeePosition
                             from pos in EmployeePosition.DefaultIfEmpty()

                             where (duration.TrainingRequestID == TrainingRequestID || TrainingRequestID.HasValue == false)
                             select new TrainingRequestDurationUserViewModel()
                             {
                                 TrainingDurationID = durationUser.TrainingDurationID,           
                                 EmpID = durationUser.EmpID,
                                 FromDate = duration.FromDate.Value,
                                 ToDate = duration.ToDate.Value,
                                 TotalDays = duration.TotalDays,
                                 TotalHours = duration.TotalHours,
                                 EmployeeName = emp.LastName + " " + emp.FirstName,
                                 CompanyName = com.Name,
                                 PositionName = pos.Name
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
        /// <param name="TrainingRequestID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingRequestCostViewModel> GetListOfTrainingRequestCost(int TrainingRequestID, out string errorMessage)
        {
            try
            {
                var found = (from cost in this.Context.TR_tblTrainingRequestCost
                             join request in this.Context.TR_tblTrainingRequest on cost.TrainingRequestID equals request.TrainingRequestID into CostRequest
                             from request in CostRequest.DefaultIfEmpty()

                             join expense in this.Context.LS_tblTrainingExpense on cost.LSTrainingExpenseID equals expense.LSTrainingExpenseID into CostExpense
                             from expense in CostExpense.DefaultIfEmpty()

                             join currency in this.Context.LS_tblCurrency on cost.LSCurrencyID equals currency.LSCurrencyID into CostCurrency
                             from currency in CostCurrency.DefaultIfEmpty()


                             where cost.TrainingRequestID == TrainingRequestID
                             select new TrainingRequestCostViewModel()
                             {
                                TrainingRequestID = cost.TrainingRequestID,
                                LSTrainingExpenseID = cost.LSTrainingExpenseID,
                                ActualCost = cost.ActualCost,
                                Cost = cost.Cost,
                                LSCurrencyID = cost.LSTrainingExpenseID,

                                TrainingExpenseName = expense.Name,
                                TrainingCurrencyName = currency.Name
                                
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
        /// <param name="TrainingRequestID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public TR_tblTrainingRequest Find(int TrainingRequestID, out string errorMessage)
        {
            try
            {
                var found = this.Context.TR_tblTrainingRequest.Find(TrainingRequestID);
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
        public TrainingRequestViewModel FindForEdit(int id, out string errorMessage)
        {
            try
            {
                var found = (from request in this.Context.TR_tblTrainingRequest
                             join duration in this.Context.TR_tblTrainingRequestDuration on request.TrainingRequestID equals duration.TrainingRequestID into RequestDuration
                             from duration in RequestDuration.DefaultIfEmpty()

                             join cost in this.Context.TR_tblTrainingRequestCost on request.TrainingRequestID equals cost.TrainingRequestID into RequestCost
                             from cost in RequestCost.DefaultIfEmpty()

                             join durationUser in this.Context.TR_tblDurationUser on duration.TrainingDurationID equals durationUser.TrainingDurationID into Duration_DurationUser
                             from durationUser in Duration_DurationUser.DefaultIfEmpty()

                             join employee in this.Context.HR_tblEmp on durationUser.EmpID equals employee.EmpID into DurationUser_Employee
                             from employee in DurationUser_Employee.DefaultIfEmpty()

                             join code in this.Context.LS_tblTrainingCode on request.LSTrainingCodeID equals code.LSTrainingCodeID into RequestCode
                             from code in RequestCode.DefaultIfEmpty()

                             join category in this.Context.LS_tblTrainingCategory on request.LSTrainingCategoryID equals category.LSTrainingCategoryID into RequestCategory
                             from category in RequestCategory.DefaultIfEmpty()

                             join course in this.Context.LS_tblTrainingCourse on request.LSTrainingCourseID equals course.LSTrainingCourseID into RequestCourse
                             from course in RequestCourse.DefaultIfEmpty()

                             join provider in this.Context.LS_tblTrainingProvider on request.LSProviderID equals provider.LSTrainingProviderID into RequestProvider
                             from provider in RequestProvider.DefaultIfEmpty()

                             join type in this.Context.LS_tblTrainingType on request.LSTrainingTypeID equals type.LSTrainingTypeID into RequestType
                             from type in RequestType.DefaultIfEmpty()

                             join form in this.Context.LS_tblTrainingForm on request.LSTrainingFormID equals form.LSTrainingFormID into RequestForm
                             from form in RequestForm.DefaultIfEmpty()

                             join location in this.Context.LS_tblTrainingLocation on request.LSTrainingLocationID equals location.LSTrainingLocationID into RequestLocation
                             from location in RequestLocation.DefaultIfEmpty()

                             join evaluation in this.Context.TR_tblEvaluationTemplate on request.EvaluationTemplateID equals evaluation.EvaluationTemplateID into RequestTemplate
                             from evaluation in RequestTemplate.DefaultIfEmpty() 

                             join trainingEvaluation in this.Context.TR_tblTrainingEvaluation on request.TrainingRequestID equals trainingEvaluation.TrainingRequestID into RequestTrainingEvaluation
                             from trainingEvaluation in RequestTrainingEvaluation.DefaultIfEmpty()

                             where request.TrainingRequestID == id

                             select new TrainingRequestViewModel()
                             {
                                 EmployeeEvaluation = employee.LastName + " " + employee.FirstName,
                                 // Information of TrainingRequest
                                 TrainingRequestID = request.TrainingRequestID,    
                                 LevelApprove = request.LevelApprove,
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
                                 Status = request.Status,
                                 TotalCost = request.TotalCost,
                                 EvaluationRequired = request.EvaluationRequired,
                                 EvaluationTemplateID = request.EvaluationTemplateID,
                                 EvaluationTemplateName = evaluation.EvaluationTemplateName,

                                 CourseCompleteStatus = request.CourseCompleteStatus,                                 
                                 Comment = request.Comment,
                                 Company = request.CurrentComment,
                                 TrainingPlanID = request.TrainingPlanID,

                                 LSTrainingCodeIDAllowNull = request.LSTrainingCodeID,
                                 TrainingCode = code.Code,
                                 TrainingCodeName = code.Name,
                                 LSTrainingCourseIDAllowNull = request.LSTrainingCourseID,
                                 TrainingCourseName = course.Name,
                                 LSTrainingCategoryIDAllowNull = request.LSTrainingCategoryID,
                                 TrainingCategoryName = category.Name,
                                 LSTrainingTypeIDAllowNull = request.LSTrainingTypeID,
                                 TrainingTypeName = type.Name,
                                 TrainingLocationName = location.Name,
                                 TrainingProviderName = provider.Name,
                                 TrainingFormName = form.Name,

                                 TrainingEvaluationID = trainingEvaluation.TrainingEvaluationID
                                 

                             }).FirstOrDefault();                
                found.TR_tblTrainingRequestComment = this.Context.TR_tblTrainingRequestComment.Where(p => p.TraningRequestID == found.TrainingRequestID).ToList();
                if (found.Status >= 1)
                {
                    found.Comment = String.Empty;
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
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Add(TR_tblTrainingRequest model, out string errorMessage)
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
        public bool Update(TR_tblTrainingRequest model, out string errorMessage)
        {
            try
            {
                if (model.TR_tblTrainingRequestComment != null)
                {
                    foreach (var item in model.TR_tblTrainingRequestComment)
                    {
                        if (item.CommentID == 0)
                        {
                            this.Context.Entry(item).State = System.Data.Entity.EntityState.Added;
                        }
                    }
                }
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
        public bool Approval(TR_tblTrainingRequest model, out string errorMessage)
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
                // Delete TR_tblTrainingRequestCost
                var listOfTrainingCost = this.Context.TR_tblTrainingRequestCost.Where(p => p.TrainingRequestID == id).ToList();
                foreach (var item in listOfTrainingCost)
                {
                    this.Context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }

                // Delete TR_tblTrainingRequestDuration
                var listOfTrainingDuration = this.Context.TR_tblTrainingRequestDuration.Where(p => p.TrainingRequestID == id).ToList();
                foreach (var item in listOfTrainingDuration)
                {
                    // Delete TR_tblDurationUser
                    var listOfDurationUser = this.Context.TR_tblDurationUser.Where(p => p.TrainingDurationID == item.TrainingDurationID).ToList();
                    foreach (var itemOfDurationUser in listOfDurationUser)
                    {
                        this.Context.Entry(itemOfDurationUser).State = System.Data.Entity.EntityState.Deleted;
                    }
                    this.Context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }

                // Delete TrainingRequest
                var objTrainingRequest = this.Context.TR_tblTrainingRequest.Where(p => p.TrainingRequestID == id).FirstOrDefault();
                if (objTrainingRequest != null)
                {
                    this.Context.Entry(objTrainingRequest).State = System.Data.Entity.EntityState.Deleted;
                }
                // Update data
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
        public bool DeleteDetailItem(int id, out string errorMessage)
        {
            try
            {
                // Delete TR_tblTrainingRequestCost
                var listOfTrainingCost = this.Context.TR_tblTrainingRequestCost.Where(p => p.TrainingRequestID == id).ToList();
                foreach (var item in listOfTrainingCost)
                {
                    this.Context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }

                // Delete TR_tblTrainingRequestDuration
                var listOfTrainingDuration = this.Context.TR_tblTrainingRequestDuration.Where(p => p.TrainingRequestID == id).ToList();
                foreach (var item in listOfTrainingDuration)
                {
                    // Delete TR_tblDurationUser
                    var listOfDurationUser = this.Context.TR_tblDurationUser.Where(p => p.TrainingDurationID == item.TrainingDurationID).ToList();
                    foreach (var itemOfDurationUser in listOfDurationUser)
                    {
                        this.Context.Entry(itemOfDurationUser).State = System.Data.Entity.EntityState.Deleted;
                    }
                    this.Context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
                
                // Update data
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

