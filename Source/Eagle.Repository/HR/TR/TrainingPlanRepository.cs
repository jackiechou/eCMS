using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using Eagle.Core;
using Eagle.Model;
using System.Data;
using Eagle.Common.Utilities;

namespace Eagle.Repository
{    
    /// <summary>
    /// 
    /// </summary>
    public class TrainingPlanRepository
    {
        public EntityDataContext context { get; set; }

        public TrainingPlanRepository(EntityDataContext Conext)
        {
            this.context = Conext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public TR_tblPlan Find(int id, out string errorMessage)
        {
            try
            {
                var result = this.context.TR_tblPlan.Find(id);
                if (result == null)
                {
                    errorMessage = "";
                }
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
        /// <param name="trainingPlanID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public TrainingPlanViewModel FindForEdit(int trainingPlanID, out string errorMessage)
        {
            try
            {
                var result = (from plan in this.context.TR_tblPlan
                              join code in this.context.LS_tblTrainingCode on plan.LSTrainingCodeID equals code.LSTrainingCodeID into PlanCode
                              from code in PlanCode.DefaultIfEmpty()

                              join course in this.context.LS_tblTrainingCourse on plan.LSTrainingCourseID equals course.LSTrainingCourseID into PlanCourse
                              from course in PlanCourse.DefaultIfEmpty()

                              join category in this.context.LS_tblTrainingCategory on plan.LSTrainingCategoryID equals category.LSTrainingCategoryID into PlanCategory
                              from category in PlanCategory.DefaultIfEmpty()

                              join provider in this.context.LS_tblTrainingProvider on plan.LSProviderID equals provider.LSTrainingProviderID into PlanProvider
                              from provider in PlanProvider.DefaultIfEmpty()

                              join com in this.context.LS_tblCompany on plan.LSCompanyID equals com.LSCompanyID into PlanCompany
                              from com in PlanCompany.DefaultIfEmpty()

                              join type in this.context.LS_tblTrainingType on plan.LSTrainingTypeID equals type.LSTrainingTypeID into PlanType
                              from type in PlanType.DefaultIfEmpty()

                              join form in this.context.LS_tblTrainingForm on plan.LSTrainingFormID equals form.LSTrainingFormID into PlanForm
                              from form in PlanForm.DefaultIfEmpty()

                              join location in this.context.LS_tblTrainingLocation on plan.LSTrainingLocationID equals location.LSTrainingLocationID into PlanLocation
                              from location in PlanLocation.DefaultIfEmpty()
                              
                              where plan.TrainingPlanID == trainingPlanID
                              orderby plan.TrainingPlanID descending

                              select new TrainingPlanViewModel()
                              {
                                  TrainingPlanID = plan.TrainingPlanID,
                                  EmpIDCreate = plan.EmpIDCreate,
                                  LSCompanyID = plan.LSCompanyID,
                                  LSTrainingCodeID = plan.LSTrainingCodeID,
                                  LSTrainingCourseID = plan.LSTrainingCourseID,
                                  LSProviderID = plan.LSProviderID,
                                  LearningObjective = plan.LearningObjective,
                                  LSTrainingTypeID = plan.LSTrainingTypeID,
                                  LSTrainingFormID = plan.LSTrainingFormID,
                                  LSTrainingLocationID = plan.LSTrainingLocationID,
                                  LSTrainingCategoryID = plan.LSTrainingCategoryID,
                                  PlanDays = plan.PlanDays,
                                  PlanYear = plan.PlanYear,
                                  NumOfStaff = plan.NumOfStaff,
                                  Status = plan.Status,
                                  Creater = plan.Creater,
                                  CreateTime = plan.CreateTime,
                                  Comment = plan.Comment,
                                  Note = plan.Note,
                                  LevelApprove = plan.LevelApprove,
                                  DateLevel1 = plan.DateLevel1,
                                  EmpIDLevel1 = plan.EmpIDLevel1,
                                  ReasonLevel1 = plan.ReasonLevel1,
                                  DateLevel2 = plan.DateLevel2,
                                  EmpIDLevel2 = plan.EmpIDLevel2,
                                  ReasonLevel2 = plan.ReasonLevel2,
                                  DateLevel3 = plan.DateLevel3,
                                  EmpIDLevel3 = plan.EmpIDLevel3,
                                  ReasonLevel3 = plan.ReasonLevel3,
                                  
                                  TrainingCodeName = code.Name,                                  
                                  TrainingCourseName = course.Name,                                                                    
                                  TrainingCategoryName = category.Name,                                  
                                  TrainingProviderName = provider.Name,                                                                    
                                  TrainingFormName = form.Name,                                                                    
                                  TrainingLocationName = location.Name,                                  
                                  TrainingTypeName = type.Name,                                  
                                  CompanyName = com.Name,                                  
                                  LSTrainingCodeIDAllowNull = plan.LSTrainingCodeID,
                                  LSTrainingCourseIDAllowNull = plan.LSTrainingCourseID,
                                  LSTrainingCategoryIDAllowNull = plan.LSTrainingCategoryID,
                                  LSTrainingTypeIDAllowNull = plan.LSTrainingTypeID,
                                  NumOfStaffAllNull = plan.NumOfStaff,
                                  isFirstComment = plan.isFirstComment,
                                  CurrentComment = plan.CurrentComment,

                                  PlanMonth = plan.PlanMonth,
                                  PlanHours = plan.PlanHours,                                                                    
                                 
                              }).FirstOrDefault();
                result.MonthYear = String.Format("{0}/{1}", result.PlanMonth.ToString().PadLeft(2, '0'), result.PlanYear);

                var listOfItems = (from planExpense in this.context.TR_tblTrainingPlanExpense
                                  join expense in this.context.LS_tblTrainingExpense on planExpense.LSTrainingExpenseID equals expense.LSTrainingExpenseID into PlanExpense
                                  from expense in PlanExpense.DefaultIfEmpty()

                                  join currency in this.context.LS_tblCurrency on planExpense.LSCurrencyID equals currency.LSCurrencyID into PlanCurrency
                                  from currency in PlanCurrency.DefaultIfEmpty()
                                  where planExpense.TrainingPlanID == result.TrainingPlanID
                                  select new TrainingPlanExpenseModelView()
                                  {
                                      TrainingPlanExpenseID = planExpense.TrainingPlanExpenseID,
                                      LSTrainingExpenseID = planExpense.LSTrainingExpenseID,
                                      LSCurrencyID = planExpense.LSCurrencyID,
                                      Cost = planExpense.Cost,
                                      TrainingExpenseName = expense.Name,
                                      TrainingCurrencyName = currency.Name
                                  }).ToList();
                result.ListOfTrainingExpense = listOfItems;

                result.TR_tblPlanComment = this.context.TR_tblPlanComment.Where(p => p.TraningPlanID == result.TrainingPlanID).ToList();
                if (result.Status >= 1)
                {
                    result.Comment = String.Empty;
                }
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
        public List<TR_sprptTrainingPlanExpense_Result> GetListOfTrainingPlanExpenseForReport(TR_sprptTrainingPlanExpense_Result model, out string errorMessage)
        {
            try
            {                
                var listOfTrainingPlanExpense = this.context.TR_sprptTrainingPlanExpense(model.LSTrainingCodeID, model.LSTrainingCourseID).ToList();
                if (listOfTrainingPlanExpense != null)
                {
                    var i = 1;
                    foreach (var item in listOfTrainingPlanExpense)
                    {
                        item.DurationInfo = this.GetDurationInfoOfTrainingRequest(item.TrainingRequestID);
                        item.Seq = i.ToString();
                        item.ExpenseOfTrainingPlanInfo = item.ExpenseOfTrainingPlan.HasValue ? item.ExpenseOfTrainingPlan.Value.ToString("#,###") : String.Empty;
                        item.ExpenseOfTrainingRequestInfo = item.ExpenseOfTrainingRequest.HasValue ? item.ExpenseOfTrainingRequest.Value.ToString("#,###") : String.Empty;
                        i++;
                    }
                }
                errorMessage = String.Empty;

                return listOfTrainingPlanExpense;
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
        /// <returns></returns>
        private string GetDurationInfoOfTrainingRequest(int TrainingRequestID)
        {
            try
            {
                var result = String.Empty;
                var listOfDuration = this.context.TR_tblTrainingRequestDuration.Where(p => p.TrainingRequestID == TrainingRequestID).ToList();
                if (listOfDuration == null)
                {
                    throw new ArgumentNullException();
                }
                var tmp = String.Empty;
                if (listOfDuration.Count > 1)
                {
                    tmp = ";";
                }
                foreach (var item in listOfDuration)
                {
                    result += String.Format("({0} - {1}){2}", DateTimeUtils.ToString(item.FromDate.Value, "dd/MM/yyyy"), DateTimeUtils.ToString(item.ToDate.Value, "dd/MM/yyyy"), tmp);
                }

                return result;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="EmployeeEvaluation"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TR_sprptTrainingPlanEvaluation_Result> GetListOfTrainingPlanEvaluationForReport(TrainingPlanEvaluationResultViewModel model, out string errorMessage)
        {
            try
            {                
                if (String.IsNullOrEmpty(model.FullName))
                {
                    model.FullName = String.Empty;
                }
                if (!model.EvaluationRequired.HasValue)
                {
                    model.EvaluationRequired = false;
                }
                if (model.LSTrainingCodeID == null)
                {
                    model.LSTrainingCodeID = 0;
                }
                if (model.LSTrainingCourseID == null)
                {
                    model.LSTrainingCourseID = 0;
                }
                var listOfTrainingPlanEvaluation = this.context.TR_sprptTrainingPlanEvaluation(model.FullName, model.LSTrainingCodeID, model.LSTrainingCourseID, model.EvaluationRequiredValue, model.EmployeeEvaluationValue).ToList();
                if (listOfTrainingPlanEvaluation != null)
                {
                    var i = 1;
                    foreach (var item in listOfTrainingPlanEvaluation)
                    {
                        item.Seq = i.ToString();
                        if (item.TrainingRequestID.HasValue)
                        {
                            item.DurationInfo = this.GetDurationInfoOfTrainingRequest(item.TrainingRequestID.Value);
                        }
                        i++;
                    }
                }
                errorMessage = String.Empty;

                return listOfTrainingPlanEvaluation;
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
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TR_sprptTrainingPlan_Result> GetListOfTrainingPlanForReport(int? Year, int? Month, out string errorMessage)
        {
            try
            {
                var i = 1;
                var listOfTrainingPlan = (from plan in this.context.TR_sprptTrainingPlan(Month, Year)
                                          select new TR_sprptTrainingPlan_Result
                                         {                                                                                          
                                             TrainingCode = plan.TrainingCode,
                                             TrainingCategory = plan.TrainingCategory,
                                             TrainingCourse = plan.TrainingCourse,
                                             TrainingLocation = plan.TrainingLocation,
                                             PlanHours = plan.PlanHours,
                                             PlanDays = plan.PlanDays,
                                             Cost = plan.Cost,     
                                             CostInfo = (plan.Cost.HasValue ? plan.Cost.Value.ToString("#,###") : String.Empty),
                                             NumOfStaff = plan.NumOfStaff,
                                             Seq = (i++).ToString()
                                         }).ToList();

                errorMessage = String.Empty;
                return listOfTrainingPlan;
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
        /// <param name="trainingPlanID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingPlanExpenseModelView> GetListOfTrainingPlanExpense(int trainingPlanID, out string errorMessage)
        {
            try
            {
                var result = (from planExpense in this.context.TR_tblTrainingPlanExpense
                             join plan in this.context.TR_tblPlan on planExpense.TrainingPlanID equals plan.TrainingPlanID into TrainingPlanExpense
                             from plan in TrainingPlanExpense.DefaultIfEmpty()

                             join currency in this.context.LS_tblCurrency on planExpense.LSCurrencyID equals currency.LSCurrencyID into TrainingPlanCurrency
                             from currency in TrainingPlanCurrency.DefaultIfEmpty()

                             join expense in this.context.LS_tblTrainingExpense on planExpense.LSTrainingExpenseID equals expense.LSTrainingExpenseID into PlanExpense
                             from expense in PlanExpense.DefaultIfEmpty()

                             where planExpense.TrainingPlanID == trainingPlanID
                             select new TrainingPlanExpenseModelView
                             {
                                 TrainingPlanExpenseID = planExpense.TrainingPlanExpenseID,
                                 LSTrainingExpenseID = planExpense.LSTrainingExpenseID,
                                 TrainingPlanID = planExpense.TrainingPlanID,
                                 LSCurrencyID = planExpense.LSCurrencyID,
                                 Cost = planExpense.Cost,
                                 TrainingCurrencyName = currency.Name,
                                 TrainingExpenseName = expense.Name
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
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingPlanViewModel> GetAll(out string errorMessage)
        {
            try
            {
                var result = (from plan in this.context.TR_tblPlan
                             join code in this.context.LS_tblTrainingCode on plan.LSTrainingCodeID equals code.LSTrainingCodeID into PlanCode
                             from code in PlanCode.DefaultIfEmpty()

                             join course in this.context.LS_tblTrainingCourse on plan.LSTrainingCourseID equals course.LSTrainingCourseID into PlanCourse
                             from course in PlanCourse.DefaultIfEmpty()

                             join category in this.context.LS_tblTrainingCategory on plan.LSTrainingCategoryID equals category.LSTrainingCategoryID into PlanCategory
                             from category in PlanCategory.DefaultIfEmpty()

                             join provider in this.context.LS_tblTrainingProvider on plan.LSProviderID equals provider.LSTrainingProviderID into PlanProvider
                             from provider in PlanProvider.DefaultIfEmpty()

                             join com in this.context.LS_tblCompany on plan.LSCompanyID equals com.LSCompanyID into PlanCompany
                             from com in PlanCompany.DefaultIfEmpty()

                             join type in this.context.LS_tblTrainingType on plan.LSTrainingTypeID equals type.LSTrainingTypeID into PlanType
                             from type in PlanType.DefaultIfEmpty()

                             join form in this.context.LS_tblTrainingForm on plan.LSTrainingFormID equals form.LSTrainingFormID into PlanForm
                             from form in PlanForm.DefaultIfEmpty()

                             join location in this.context.LS_tblTrainingLocation on plan.LSTrainingLocationID equals location.LSTrainingLocationID into PlanLocation
                             from location in PlanType.DefaultIfEmpty()

                             select new TrainingPlanViewModel() 
                             {
                                 LSCompanyID = plan.LSCompanyID,

                                 LSTrainingCourseID = plan.LSTrainingCourseID,
                                 LSTrainingCodeID = plan.LSTrainingCodeID,
                                 LSTrainingCategoryID = plan.LSTrainingCategoryID,
                                 LSProviderID = plan.LSProviderID,
                                 LearningObjective = plan.LearningObjective,

                                 LSTrainingTypeID = plan.LSTrainingTypeID,
                                 LSTrainingFormID = plan.LSTrainingFormID,
                                 LSTrainingLocationID = plan.LSTrainingLocationID,

                                 PlanDays = plan.PlanDays,
                                 PlanMonth = plan.PlanMonth,
                                 PlanHours = plan.PlanHours,
                                 PlanYear = plan.PlanYear,
                                 NumOfStaff = plan.NumOfStaff,
                                 Status = plan.Status,
                                 Note = plan.Note,
                                 LevelApprove = plan.LevelApprove,

                                 DateLevel1 = plan.DateLevel1,
                                 EmpIDLevel1 = plan.EmpIDLevel1,
                                 ReasonLevel1 = plan.ReasonLevel1,

                                 DateLevel2 = plan.DateLevel2,
                                 EmpIDLevel2 = plan.EmpIDLevel2,
                                 ReasonLevel2 = plan.ReasonLevel2,

                                 DateLevel3 = plan.DateLevel3,
                                 EmpIDLevel3 = plan.EmpIDLevel3,
                                 ReasonLevel3 = plan.ReasonLevel3,
                                 
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
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingPlanViewModel> GetTrainingPlanList(AccountViewModel account, int ModuleID, TrainingPlanSearchViewModel model, out string errorMessage)
        {
            try
            {
                var listOfUser = context.SYS_spfrmDataPermission(account.UserName, ModuleID).ToList();
                var company = this.context.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID).FirstOrDefault();
                string Lineage = String.Empty;
                if (company != null)
                {
                    Lineage = company.Lineage;
                }
                var result = (from plan in this.context.TR_tblPlan
                              join code in this.context.LS_tblTrainingCode on plan.LSTrainingCodeID equals code.LSTrainingCodeID into PlanCode
                              from code in PlanCode.DefaultIfEmpty()

                              join course in this.context.LS_tblTrainingCourse on plan.LSTrainingCourseID equals course.LSTrainingCourseID into PlanCourse
                              from course in PlanCourse.DefaultIfEmpty()

                              join category in this.context.LS_tblTrainingCategory on plan.LSTrainingCategoryID equals category.LSTrainingCategoryID into PlanCategory
                              from category in PlanCategory.DefaultIfEmpty()

                              join provider in this.context.LS_tblTrainingProvider on plan.LSProviderID equals provider.LSTrainingProviderID into PlanProvider
                              from provider in PlanProvider.DefaultIfEmpty()

                              join com in this.context.LS_tblCompany on plan.LSCompanyID equals com.LSCompanyID into PlanCompany
                              from com in PlanCompany.DefaultIfEmpty()

                              join type in this.context.LS_tblTrainingType on plan.LSTrainingTypeID equals type.LSTrainingTypeID into PlanType
                              from type in PlanType.DefaultIfEmpty()

                              join form in this.context.LS_tblTrainingForm on plan.LSTrainingFormID equals form.LSTrainingFormID into PlanForm
                              from form in PlanForm.DefaultIfEmpty()

                              join location in this.context.LS_tblTrainingLocation on plan.LSTrainingLocationID equals location.LSTrainingLocationID into PlanLocation
                              from location in PlanType.DefaultIfEmpty()

                              join user in this.context.HR_tblEmp on plan.EmpIDCreate equals user.EmpID into PlanUser
                              from user in PlanUser.DefaultIfEmpty()

                              where (plan.LSTrainingCodeID == model.LSTrainingCodeID || !model.LSTrainingCodeID.HasValue) 
                              && (plan.LSTrainingCourseID == model.LSTrainingCourseID || !model.LSTrainingCourseID.HasValue)
                              && (com.Lineage.StartsWith(Lineage) || String.IsNullOrEmpty(Lineage))                         
                              && (plan.PlanMonth == model.PlanMonth || !model.PlanMonth.HasValue)
                              && (plan.PlanYear == model.PlanYear || !model.PlanYear.HasValue)
                              && (listOfUser.Contains(plan.EmpIDCreate) && (plan.EmpIDCreate == account.EmpId || (plan.EmpIDCreate != account.EmpId && plan.Status != 0)))
                              && (plan.Status == model.Status || model.Status == -1) 
                              && (user.EmpCode.Contains(model.EmpCode) || (String.IsNullOrEmpty(model.EmpCode)))
                              && ((user.LastName + " " + user.FirstName).ToUpper().Contains(model.EmployeeName.ToUpper()) || String.IsNullOrEmpty(model.EmployeeName))
      
                              orderby plan.TrainingPlanID descending

                              select new TrainingPlanViewModel()
                              {
                                  TrainingPlanID = plan.TrainingPlanID,
                                  EmpIDCreate = plan.EmpIDCreate,
                                  LSCompanyID = plan.LSCompanyID,                                  
                                  LSTrainingCodeID = plan.LSTrainingCodeID,
                                  LSTrainingCourseID = plan.LSTrainingCourseID,
                                  LSProviderID = plan.LSProviderID,
                                  LearningObjective = plan.LearningObjective,
                                  LSTrainingTypeID = plan.LSTrainingTypeID,
                                  LSTrainingFormID = plan.LSTrainingFormID,
                                  LSTrainingLocationID = plan.LSTrainingLocationID,
                                  LSTrainingCategoryID = plan.LSTrainingCategoryID,
                                  PlanDays = plan.PlanDays,
                                  PlanMonth = plan.PlanMonth,
                                  PlanHours = plan.PlanHours,
                                  PlanYear = plan.PlanYear,
                                  NumOfStaff = plan.NumOfStaff,
                                  Status = plan.Status,
                                  Creater = plan.Creater,
                                  CreateTime = plan.CreateTime,
                                  Note = plan.Note,
                                  LevelApprove = plan.LevelApprove,
                                  DateLevel1 = plan.DateLevel1,
                                  EmpIDLevel1 = plan.EmpIDLevel1,
                                  ReasonLevel1 = plan.ReasonLevel1,
                                  DateLevel2 = plan.DateLevel2,
                                  EmpIDLevel2 = plan.EmpIDLevel2,
                                  ReasonLevel2 = plan.ReasonLevel2,
                                  DateLevel3 = plan.DateLevel3,
                                  EmpIDLevel3 = plan.EmpIDLevel3,
                                  ReasonLevel3 = plan.ReasonLevel3,

                                  TrainingCodeName = code.Name,
                                  TrainingCode = code.Code,
                                  TrainingCourseName = course.Name,
                                  TrainingCategoryName = category.Name,
                                  TrainingProviderName = provider.Name,
                                  TrainingFormName = form.Name,
                                  TrainingLocationName = location.Name,
                                  TrainingTypeName = type.Name,
                                  CompanyName = com.Name,
                                  LSTrainingCodeIDAllowNull = plan.LSTrainingCodeID,
                                  LSTrainingCourseIDAllowNull = plan.LSTrainingCourseID,
                                  LSTrainingCategoryIDAllowNull = plan.LSTrainingCategoryID,
                                  LSTrainingTypeIDAllowNull = plan.LSTrainingTypeID,
                                  NumOfStaffAllNull = plan.NumOfStaff,

                                  PlanUserName = user.LastName + " " + user.FirstName

                              }).ToList();
                
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        item.MonthYear = String.Format("{0}/{1}", item.PlanMonth.ToString().PadLeft(2,'0'), item.PlanYear);
                    }
                }
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
        public bool Add(TR_tblPlan model, out string errorMessage)
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
        /// <param name="listOfPlanExpense"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Update(TR_tblPlan model, out string errorMessage)
        {
            try
            {
                if (model.TR_tblPlanComment != null)
                {
                    foreach (var item in model.TR_tblPlanComment)
                    {
                        if (item.CommentID == 0)
                        {
                            this.context.Entry(item).State = System.Data.Entity.EntityState.Added;
                        }                        
                    }
                }
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
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                var listOfItems = this.context.TR_tblTrainingPlanExpense.Where(p => p.TrainingPlanID == id).ToList();
                foreach (var item in listOfItems)
                {
                    this.context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
                var repository = new TrainingExpenseRepository(this.context);                
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trainingPlanID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public string GetLearningObjective(int trainingPlanID, out string errorMessage)
        {
            try
            {
                var result = this.context.LS_tblTrainingCourse.Single(p => p.LSTrainingCourseID == trainingPlanID).LearningObjectives;
                errorMessage = String.Empty;
                return result;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return String.Empty;
            }
        }
               
    }
}
