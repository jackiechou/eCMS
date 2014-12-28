using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Eagle.Core;
using Eagle.Repository;
using Eagle.Repository.HR;
using Eagle.Model;
using AutoMapper;
using Eagle.Common.Helpers;
using Eagle.Common.Utilities;
using System.Collections;
using Eagle.Repository.Mail;
using Eagle.Model.HR;
using Eagle.Common.Settings;
using Eagle.Repository.SYS.Users;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingRequestController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        public const int TRAININGREQUEST_FUNCTIONID = 476;        

        #region Listing, Adding, Deleting of Duration

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrainingRequestID"></param>
        /// <returns></returns>
        public ActionResult _ListOfDuration(int? TrainingRequestID)
        {
            var listOfDuration = this.TempData["Duration"] as List<TrainingRequestDurationViewModel>;
            if (TrainingRequestID.HasValue && TrainingRequestID.Value > 0)
            {
                // For updating of TrainingRequest
                var errorMessage = String.Empty;
                var repository = new TrainingRequestRepository(this.db);
                listOfDuration = repository.GetListOfTrainingRequestDuration(TrainingRequestID.Value, out errorMessage);
                this.TempData["Duration"] = listOfDuration;

                return this.PartialView("../TR/TrainingRequest/_ListOfDuration", listOfDuration);
            }            
            // For adding a new row of TrainingRequestDuration
            var IsStart = false;
            if (listOfDuration == null)
            {
                listOfDuration = new List<TrainingRequestDurationViewModel>();
                IsStart = true;
            }
            if (listOfDuration.Count == 0 && IsStart)
            {
                listOfDuration.Add(new TrainingRequestDurationViewModel()
                {
                    Key = 0
                    //FromDate = DateTime.Now,
                    //ToDate = DateTime.Now
                });                
            }
            
            return this.PartialView("../TR/TrainingRequest/_ListOfDuration", listOfDuration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listOfDuration"></param>
        /// <returns></returns>
        public ActionResult AddRowOfDuration(List<TrainingRequestDurationViewModel> listOfDuration)
        {            
            if (listOfDuration == null)
            {
                listOfDuration = new List<TrainingRequestDurationViewModel>();
            }
            var duration = new TrainingRequestDurationViewModel();
            duration.Key = listOfDuration.Count;
            listOfDuration.Add(duration);                       

            return this.PartialView("../TR/TrainingRequest/_ListOfDuration", listOfDuration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listOfDuration"></param>
        /// <returns></returns>
        public ActionResult DeleteRowOfDuration(int id, string FromDateInfo, string ToDateInfo, List<TrainingRequestDurationViewModel> listOfDuration)
        {
            var listOfDurationUser = this.TempData["DurationUser"] as List<TrainingRequestDurationUserViewModel>;
            if (listOfDurationUser == null)
            {
                listOfDurationUser = new List<TrainingRequestDurationUserViewModel>();
            }            
            if (listOfDuration == null)
            {
                listOfDuration = new List<TrainingRequestDurationViewModel>();
            }
            foreach (var item in listOfDuration)
            {
                if (item.Key == id)
                {
                    // Deleting durationUser
                    if (DateTimeUtils.IsDateTime(item.FromDateInfo, "dd/MM/yyyy") && DateTimeUtils.IsDateTime(item.ToDateInfo, "dd/MM/yyyy"))
                    {
                        item.FromDate = DateTime.ParseExact(item.FromDateInfo, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
                        item.ToDate = DateTime.ParseExact(item.ToDateInfo, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
                        listOfDurationUser.RemoveAll(p => p.FromDate == item.FromDate && p.ToDate == item.ToDate);
                    }                    
                    
                    listOfDuration.Remove(item);
                    break;
                }
            }
            int key = 0;
            foreach (var item in listOfDuration)
            {
                item.Key = key++;
            }
            this.TempData["Duration"] = listOfDuration;
            this.TempData["DurationUser"] = listOfDurationUser;

            return this.PartialView("../TR/TrainingRequest/_ListOfDuration", listOfDuration);
        }        

        #endregion       

        #region Listing, Adding, Deleting of DurationUser

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrainingRequestID"></param>
        /// <returns></returns>
        public ActionResult _ListOfDurationUser(int? TrainingRequestID)
        {
            var listOfDurationUser = this.TempData["DurationUser"] as List<TrainingRequestDurationUserViewModel>;
            if (listOfDurationUser == null)
            {
                listOfDurationUser = new List<TrainingRequestDurationUserViewModel>();
            }
            if (TrainingRequestID.HasValue && TrainingRequestID.Value > 0)
            {
                // For updating of TrainingRequest
                string errorMessage;
                var reposotory = new TrainingRequestRepository(this.db);
                listOfDurationUser = reposotory.GetListOfTrainingRequestDurationUser(TrainingRequestID, out errorMessage);
                if (listOfDurationUser == null)
                {
                    return this.Content("error");
                }
                this.TempData["DurationUser"] = listOfDurationUser;
                return this.PartialView("../TR/TrainingRequest/_ListOfEmployeeDuration", listOfDurationUser);
            }
            this.TempData["DurationUser"] = listOfDurationUser;
                                            
            return this.PartialView("../TR/TrainingRequest/_ListOfEmployeeDuration", listOfDurationUser);
            
        }

        ///<summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="listOfDuration"></param>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public ActionResult AddRowOfDurationUser(int EmpID, string FromDateInfo, string ToDateInfo, int TotalDaysInfo, int TotalHoursInfo, 
            List<TrainingRequestDurationViewModel> listOfDurationOnGrid)
        {
            // 1. Checking validation            
            DateTime FromDate = DateTimeUtils.ToDateTime(FromDateInfo, "dd/MM/yyyy");
            DateTime ToDate = DateTimeUtils.ToDateTime(ToDateInfo, "dd/MM/yyyy");            

            // Getting List of TrainingRequestDurationUser        
            var listOfDurationUser = this.TempData["DurationUser"] as List<TrainingRequestDurationUserViewModel>;
            if (listOfDurationUser == null)
            {
                listOfDurationUser = new List<TrainingRequestDurationUserViewModel>();
            }
            
            #region 2. Checking duplicate about from, to duration
            if (listOfDurationOnGrid == null)
            {
                listOfDurationOnGrid = new List<TrainingRequestDurationViewModel>();
            }
            bool checkDuplicate = false;
            for (int i = 0; i < listOfDurationOnGrid.Count; i++)
            {
                for (int j = i + 1; j < listOfDurationOnGrid.Count; j++)
                {
                    if (listOfDurationOnGrid[i].FromDate == listOfDurationOnGrid[j].FromDate 
                        && listOfDurationOnGrid[i].ToDate == listOfDurationOnGrid[j].ToDate)
                    {
                        checkDuplicate = true;
                        break;
                    }
                }
                if (checkDuplicate)
                {
                    break;
                }
            }
            if (checkDuplicate)
            {
                this.TempData["DurationUser"] = listOfDurationUser;
                return this.Content("ExistedDuration");
            }
            #endregion

            // 3. Checking of TrainingRequestDurationUser                                   
            bool IsExisted = false;            
            foreach (var item in listOfDurationUser)
            {
                if (DateTimeUtils.ToString(item.FromDate, "dd/MM/yyyy") == FromDateInfo && DateTimeUtils.ToString(item.ToDate, "dd/MM/yyyy") == ToDateInfo)  
                {
                    var itemForCheck = listOfDurationUser.Where(p => p.EmpID == EmpID).FirstOrDefault();
                    if (itemForCheck != null)
                    {
                        IsExisted = true;
                        break;               
                    }                                         
                }
            }
            if (IsExisted)
            {
                this.TempData["DurationUser"] = listOfDurationUser;
                return this.Content("Existed");
            }
            
            // 4. Adding TrainingRequestDuration
            // Getting List of TrainingRequestDuration
            var listOfDuration = this.TempData["Duration"] as List<TrainingRequestDurationViewModel>;
            if (listOfDuration == null)
            {
                listOfDuration = new List<TrainingRequestDurationViewModel>();
            }
            var checkExist = false;
            foreach (var duration in listOfDuration)
            {
                if (duration.FromDate == FromDate && duration.ToDate == ToDate)
                {
                    checkExist = true;
                    break;
                }
            }
            if (!checkExist)
            {
                var duration = new TrainingRequestDurationViewModel();
                duration.TrainingDurationID = 0;
                duration.Key = listOfDuration.Count;
                duration.FromDate = FromDate;
                duration.ToDate = ToDate;
                duration.TotalDaysAllowNull = TotalDaysInfo;
                duration.TotalDays = TotalDaysInfo;
                duration.TotalHours = TotalHoursInfo;
                listOfDuration.Add(duration);                
            }
            this.TempData["Duration"] = listOfDuration;

            // 5. Add TrainingRequestDurationUser    
            var employeeInfo = new EmployeeRepository(this.db).GetEmployee(EmpID, this.LanguageId);
            if (employeeInfo == null)
            {
                return this.Content("error");
            }
                    
            var durationUserInfo = new TrainingRequestDurationUserViewModel();
            durationUserInfo.TrainingDurationID = 0;
            durationUserInfo.EmpID = EmpID;
            durationUserInfo.EmployeeName = employeeInfo.FullName;
            durationUserInfo.CompanyName = employeeInfo.CompanyName;
            durationUserInfo.PositionName = employeeInfo.Position;
            
            durationUserInfo.FromDate = FromDate;
            durationUserInfo.ToDate = ToDate;
            durationUserInfo.TotalDays = TotalDaysInfo;
            durationUserInfo.TotalHours = TotalHoursInfo;
                        
            listOfDurationUser.Add(durationUserInfo);
            this.TempData["DurationUser"] = listOfDurationUser;
                                                    
            return this.PartialView("../TR/TrainingRequest/_ListOfEmployeeDuration", listOfDurationUser);                                                         
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListOfEmpID"></param>
        /// <param name="FromDateInfo"></param>
        /// <param name="ToDateInfo"></param>
        /// <param name="TotalDaysInfo"></param>
        /// <param name="TotalHoursInfo"></param>
        /// <param name="listOfDurationOnGrid"></param>
        /// <returns></returns>
        public ActionResult AddingListOfEmployee(string ListOfEmpIDInfo, string FromDateInfo, string ToDateInfo, int TotalDaysInfo, int TotalHoursInfo,
            List<TrainingRequestDurationViewModel> listOfDurationOnGrid)
        {
            var ListOfEmpID = new List<int>();
            if (String.IsNullOrEmpty(ListOfEmpIDInfo))
            {
                return this.Content("Error");
            }
            foreach (var tmp in ListOfEmpIDInfo.Split(';'))
            {
                if (!String.IsNullOrEmpty(tmp))
                {
                    try
                    {
                        ListOfEmpID.Add(Convert.ToInt32(tmp));
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                        return this.Content(Eagle.Resource.LanguageResource.PleaseInputTrainingRequestDuration);
                    }
                }                
            }
            // 1. Checking validation            
            DateTime FromDate = DateTimeUtils.ToDateTime(FromDateInfo, "dd/MM/yyyy");
            DateTime ToDate = DateTimeUtils.ToDateTime(ToDateInfo, "dd/MM/yyyy");

            // Getting List of TrainingRequestDurationUser        
            var listOfDurationUser = this.TempData["DurationUser"] as List<TrainingRequestDurationUserViewModel>;
            if (listOfDurationUser == null)
            {
                listOfDurationUser = new List<TrainingRequestDurationUserViewModel>();
            }            

            // 4. Adding TrainingRequestDuration
            // Getting List of TrainingRequestDuration
            var listOfDuration = this.TempData["Duration"] as List<TrainingRequestDurationViewModel>;
            if (listOfDuration == null)
            {
                listOfDuration = new List<TrainingRequestDurationViewModel>();
            }
            var checkExist = false;
            foreach (var duration in listOfDuration)
            {
                if (duration.FromDate == FromDate && duration.ToDate == ToDate)
                {
                    checkExist = true;
                    break;
                }
            }
            if (checkExist)
            {
                this.TempData["DurationUser"] = listOfDurationUser;
                return this.Content("ExistedDuration");
            }
            else
            {
                var duration = new TrainingRequestDurationViewModel();
                duration.TrainingDurationID = 0;
                duration.Key = listOfDuration.Count;
                duration.FromDate = FromDate;
                duration.ToDate = ToDate;
                duration.TotalDaysAllowNull = TotalDaysInfo;
                duration.TotalDays = TotalDaysInfo;
                duration.TotalHours = TotalHoursInfo;
                listOfDuration.Add(duration);
            }            
            
            this.TempData["Duration"] = listOfDuration;

            // 5. Add TrainingRequestDurationUser                
            foreach (var EmpID in ListOfEmpID)
            {
                var checkEnd = false;
                foreach (var item in listOfDurationUser)
                {

                    if (DateTimeUtils.ToString(item.FromDate, "dd/MM/yyyy") == FromDateInfo && DateTimeUtils.ToString(item.ToDate, "dd/MM/yyyy") == ToDateInfo && listOfDurationUser.Count > 1)
                    {
                        var itemForCheck = listOfDurationUser.Where(p => p.EmpID == EmpID).FirstOrDefault();
                        if (itemForCheck != null)
                        {
                            checkEnd = true;
                            break;
                        }
                    }
                }
                if (checkEnd)
                {
                    this.TempData["DurationUser"] = listOfDurationUser;
                    return this.Content("Existed");
                }
                var employeeInfo = new EmployeeRepository(this.db).GetEmployee(EmpID, this.LanguageId);
                if (employeeInfo == null)
                {
                    return this.Content("error");
                }

                var durationUserInfo = new TrainingRequestDurationUserViewModel();
                durationUserInfo.TrainingDurationID = 0;
                durationUserInfo.EmpID = EmpID;
                durationUserInfo.EmployeeName = employeeInfo.FullName;
                durationUserInfo.CompanyName = employeeInfo.CompanyName;
                durationUserInfo.PositionName = employeeInfo.Position;

                durationUserInfo.FromDate = FromDate;
                durationUserInfo.ToDate = ToDate;
                durationUserInfo.TotalDays = TotalDaysInfo;
                durationUserInfo.TotalHours = TotalHoursInfo;

                listOfDurationUser.Add(durationUserInfo);
                this.TempData["DurationUser"] = listOfDurationUser;
            }            
            return this.PartialView("../TR/TrainingRequest/_ListOfEmployeeDuration", listOfDurationUser);     
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteRowOfDurationUser(int id)
        {
            var listOfDurationUser = this.TempData["DurationUser"] as List<TrainingRequestDurationUserViewModel>;
            if (listOfDurationUser == null)
            {
                listOfDurationUser = new List<TrainingRequestDurationUserViewModel>();
            }
            foreach (var item in listOfDurationUser)
            {
                if (item.TrainingDurationID == id)
                {
                    listOfDurationUser.Remove(item);
                    break;
                }
            }            
            this.TempData["DurationUser"] = listOfDurationUser;

            return this.PartialView("../TR/TrainingRequest/_ListOfEmployeeDuration", listOfDurationUser);
        }

        #endregion

        #region Listing, Adding, Deleting of Expense
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _CreateCost()
        {
            return this.PartialView("../TR/TrainingRequest/_CreateCost", new TrainingRequestCostViewModel());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrainingRequestID"></param>
        /// <returns></returns>
        public ActionResult _ListOfCost(int? TrainingRequestID, int? TrainingPlanID)
        {            
            var listOfCost = this.TempData["Expense"] as List<TrainingRequestCostViewModel>;
            if (listOfCost == null)
            {
                listOfCost = new List<TrainingRequestCostViewModel>();
            }
            // For updating of TrainingRequest
            if (TrainingRequestID.HasValue && TrainingRequestID.Value > 0)     
            {
                var repository = new TrainingRequestRepository(this.db);
                string errorMessage;
                listOfCost = repository.GetListOfTrainingRequestCost(TrainingRequestID.Value, out errorMessage);
                if (listOfCost == null)
                {
                    return this.Content("error");
                }                
            }
            // For loading of TrainingRequest from TrainingPlan
            if (TrainingPlanID.HasValue)
            {
                var errorMessage = String.Empty;
                var repository = new TrainingPlanRepository(this.db);
                var listOfTrainingExpense = repository.GetListOfTrainingPlanExpense(TrainingPlanID.Value, out errorMessage);
                if (listOfTrainingExpense != null)
                {
                    foreach (var item in listOfTrainingExpense)
                    {
                        var cost = new TrainingRequestCostViewModel();
                        if (item.LSTrainingExpenseID.HasValue)
                        {
                            cost.LSTrainingExpenseID = item.LSTrainingExpenseID.Value;
                        }
                        if (item.LSCurrencyID.HasValue)
                        {
                            cost.LSCurrencyID = item.LSCurrencyID.Value;
                        }
                        cost.TrainingExpenseName = item.TrainingExpenseName;
                        cost.TrainingCurrencyName = item.TrainingCurrencyName;
                        cost.Cost = item.Cost;
                        cost.ActualCost = item.Cost;
                        listOfCost.Add(cost);
                    }
                }
            }
            this.TempData["Expense"] = listOfCost;

            return this.PartialView("../TR/TrainingRequest/_ListOfCost", listOfCost);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AddRowOfCost(TrainingRequestCostViewModel model)
        {
            var listOfExpense = this.TempData["Expense"] as List<TrainingRequestCostViewModel>;
            if (listOfExpense == null)
            {
                listOfExpense = new List<TrainingRequestCostViewModel>();
            }            
            // Getting information of TrainingRequestCost
            if (model.LSCurrencyIDAllowNull.HasValue)
            {
                model.LSCurrencyID = model.LSCurrencyIDAllowNull.Value;
            }
            if (model.LSTrainingExpenseIDAllowNull.HasValue)
            {
                model.LSTrainingExpenseID = model.LSTrainingExpenseIDAllowNull.Value;
            }
            model.ActualCost = model.Cost;
            // Checking exist
            foreach (var item in listOfExpense)
            {
                if (item.LSTrainingExpenseID == model.LSTrainingExpenseID)
                {
                    this.TempData["Expense"] = listOfExpense;
                    return this.Content("Existed");
                }
            }
            listOfExpense.Add(model);
            this.TempData["Expense"] = listOfExpense;

            return this.PartialView("../TR/TrainingRequest/_ListOfCost", listOfExpense);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteRowOfCost(int id)
        {
            var listOfExpense = this.TempData["Expense"] as List<TrainingRequestCostViewModel>;
            if (listOfExpense == null)
            {
                return this.Content("error");
            }
            // Checking exist
            foreach (var item in listOfExpense)
            {
                if (item.LSTrainingExpenseID == id)
                {
                    listOfExpense.Remove(item);
                    break;
                }
            }
            this.TempData["Expense"] = listOfExpense;

            return this.PartialView("../TR/TrainingRequest/_ListOfCost", listOfExpense);
        }

        #endregion

        #region Adding, SendForApproval, Approval, UnApproval TrainingRequest

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? TrainingRequestID, int? TrainingPlanID)
        {
            if (TrainingRequestID.HasValue && TrainingRequestID.Value > 0)
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri + "?TrainingRequestID=" + TrainingRequestID.ToString());
                    return null;
                }

                return View("../TR/TrainingRequest/Edit", TrainingRequestID);
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("../TR/TrainingRequest/_Reset", TrainingPlanID);
                }
                else
                {
                    if (CurrentAcc == null)
                    {
                        Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                        return null;
                    }
                    return View("../TR/TrainingRequest/Index", TrainingPlanID);
                }
            }               
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _Create(int? TrainingPlanID)
        {
            this.TempData.Clear();
            var request = new TrainingRequestViewModel();            
            if (TrainingPlanID.HasValue)
            {
                var errorMessage = String.Empty;
                var repository = new TrainingPlanRepository(this.db);
                var plan = repository.FindForEdit(TrainingPlanID.Value, out errorMessage);
                if (plan != null)
                {
                    request.TrainingPlanID = plan.TrainingPlanID;

                    request.LSTrainingCodeID = plan.LSTrainingCodeID;
                    request.LSTrainingCodeIDAllowNull = plan.LSTrainingCodeIDAllowNull;
                    request.TrainingCodeName = plan.TrainingCodeName;
                    
                    request.LSTrainingCourseID = plan.LSTrainingCourseID;
                    request.LSTrainingCourseIDAllowNull = plan.LSTrainingCourseIDAllowNull;
                    request.TrainingCourseName = plan.TrainingCourseName;

                    request.LearningObjective = plan.LearningObjective;

                    request.LSTrainingCategoryID = plan.LSTrainingCategoryID;
                    request.LSTrainingCategoryIDAllowNull = plan.LSTrainingCategoryIDAllowNull;
                    request.TrainingCategoryName = plan.TrainingCategoryName;

                    request.LSProviderID = plan.LSProviderID;
                    request.TrainingProviderName = plan.TrainingProviderName;

                    request.LSTrainingTypeID = plan.LSTrainingTypeID;
                    request.LSTrainingTypeIDAllowNull = plan.LSTrainingTypeIDAllowNull;
                    request.TrainingTypeName = plan.TrainingTypeName;

                    request.LSTrainingFormID = plan.LSTrainingFormID;
                    request.TrainingFormName = plan.TrainingFormName;

                    request.LSCompanyID = plan.LSCompanyID;
                    request.Company = plan.CompanyName;

                    request.LSTrainingLocationID = plan.LSTrainingLocationID;
                    request.TrainingLocationName = plan.TrainingLocationName;
                    request.EvaluationRequired = false;
                    request.EvaluationTemplateID = null;
                }
            }
            this.CreateViewBag(request);
            return PartialView("../TR/TrainingRequest/_Create", request);
        }
              
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void CreateViewBag(TrainingRequestViewModel model)
        {
            // Checking permission
            bool EnableSendAndSendForApproval = false, EnableApprovalAndUnApproval = false;            
            var processOnlineInfo = db.LS_tblOnlineProcess.Where(p => p.FunctionID == TRAININGREQUEST_FUNCTIONID).FirstOrDefault();
            int maxLevelApprove = 3;
            if (processOnlineInfo != null)
            {
                maxLevelApprove = processOnlineInfo.NoOfLevel;
            }
            
            var permission = new CommonRepository(this.db);
            permission.CheckPermission(model.Status, this.CurrentEmpId, model.EmpIDCreate, TRAININGREQUEST_FUNCTIONID, model.LevelApprove, maxLevelApprove, model.TrainingRequestID, ref EnableSendAndSendForApproval, ref EnableApprovalAndUnApproval);            
            
            // Setting ViewBag
            this.ViewBag.EnableSaveAndSendForApproval = this.IsAllowSaveAndSendForApproval(model) ? "" : "style=display:none;";
            this.ViewBag.EnableApprovalAndUnApproval = EnableApprovalAndUnApproval ? "" : "style=display:none;";
            if (model.TrainingPlanID.HasValue)
            {
                this.ViewBag.TrainingPlanID = model.TrainingPlanID.ToString();
            }
            else
            {
                this.ViewBag.TrainingPlanID = String.Empty;
            }
            LS_tblOnlineProcess opModel = db.LS_tblOnlineProcess.Where(p => p.FunctionID == TRAININGREQUEST_FUNCTIONID).FirstOrDefault();
            if (opModel != null)
            {
                ViewBag.IsStart = opModel.IsStart;
            }
            var listOfComment = (from comment in model.TR_tblTrainingRequestComment
                                 join emp in this.db.HR_tblEmp on comment.EmpID equals emp.EmpID into CommentEmployee
                                 from emp in CommentEmployee.DefaultIfEmpty()
                                 select new CommentViewModel
                                 {
                                     AvatarId = emp.FileId,
                                     Comment = comment.Comment,
                                     EmpComment = String.Format("{0} {1}", emp.LastName, emp.FirstName),
                                     StatusName = comment.StatusName,
                                     Time = comment.Time
                                 }).ToList();
            ViewBag.CommentList = listOfComment;
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LevelApprove"></param>
        /// <returns></returns>
        public ActionResult _popupUnapprove(int LevelApprove)
        {
            // trả về người tạo và các cấp dưới "LevelApprove"
            //VD: nếu là cấp 3 trả về người tạo và cấp 1 2
            //    nếu là cấp 1 chỉ trả về người tạo
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(0, Eagle.Resource.LanguageResource.CreateUser);
            var LS_tblOnlineProcessModel = db.LS_tblOnlineProcess.Where(p => p.FunctionID == TRAININGREQUEST_FUNCTIONID).FirstOrDefault();
            if (LS_tblOnlineProcessModel != null)
            {
                var SYS_tblProcessOnlineMasterModel = LS_tblOnlineProcessModel.SYS_tblProcessOnlineMaster.FirstOrDefault();
                if (SYS_tblProcessOnlineMasterModel != null)
                {
                    switch (LevelApprove)
                    {
                        case 2: //Add cấp 1
                            dic.Add(1, SYS_tblProcessOnlineMasterModel.StatusLevel1A);
                            break;
                        case 3: //Add cấp 1,2
                            dic.Add(1, SYS_tblProcessOnlineMasterModel.StatusLevel1A);
                            dic.Add(2, SYS_tblProcessOnlineMasterModel.StatusLevel2A);
                            break;
                        case 4: //Add cấp 1,2,3
                            dic.Add(1, SYS_tblProcessOnlineMasterModel.StatusLevel1A);
                            dic.Add(2, SYS_tblProcessOnlineMasterModel.StatusLevel2A);
                            dic.Add(3, SYS_tblProcessOnlineMasterModel.StatusLevel3A);
                            break;
                        case 5: //Add cấp 1,2,3,4
                            dic.Add(1, SYS_tblProcessOnlineMasterModel.StatusLevel1A);
                            dic.Add(2, SYS_tblProcessOnlineMasterModel.StatusLevel2A);
                            dic.Add(3, SYS_tblProcessOnlineMasterModel.StatusLevel3A);
                            dic.Add(4, SYS_tblProcessOnlineMasterModel.StatusLevel4A);
                            break;
                    }
                }
            }
            ViewBag.ReturnLevelApproveList = new SelectList(dic, "Key", "Value");
            
            return PartialView("../TR/TrainingRequest/_popupUnapprove");
           // return Content("zzzzzzzz");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool IsAllowSaveAndSendForApproval(TR_tblTrainingRequest model)
        {
            bool result = false;
            // Logging into system is admi
            if (this.CurrentEmpId == null || this.CurrentEmpId.Value == 0)
            {
                return false;
            }
            // For case of adding 
            if (model.TrainingRequestID == 0)
            {
                result = true;
            }
            // For case of updating
            else if (model.TrainingRequestID > 0)
            {
                if (model.Status == (int)TrainingPlanStatus.Save)
                {                    
                    // Creater = Updater
                    if (model.EmpIDCreate == CurrentEmpId.Value)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else if (model.Status >= (int)TrainingPlanStatus.WaitingForApproval)
                {
                    result = false;
                }
            }
            return result;
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _Edit(int id)
        {
            this.TempData.Clear();
            string errorMessage;
            TrainingRequestRepository repository = new TrainingRequestRepository(this.db);
            TrainingRequestViewModel model = repository.FindForEdit(id, out errorMessage);            
            if (model == null)
            {
                return this._Error(model, errorMessage);
            }
            this.CreateViewBag(model);
            return PartialView("../TR/TrainingRequest/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private TR_tblTrainingRequest GettingAndMappingTrainingRequestForAdding(TrainingRequestViewModel model, string mode)
        {             
            // Reset information from ControllAllowNull
            if (model.LSTrainingCategoryIDAllowNull.HasValue)
            {
                model.LSTrainingCategoryID = model.LSTrainingCategoryIDAllowNull.Value;
            }
            if (model.LSTrainingCodeIDAllowNull.HasValue)
            {
                model.LSTrainingCodeID = model.LSTrainingCodeIDAllowNull.Value;
            }
            if (model.LSTrainingCourseIDAllowNull.HasValue)
            {
                model.LSTrainingCourseID = model.LSTrainingCourseIDAllowNull.Value;
            }
            if (model.LSTrainingTypeIDAllowNull.HasValue)
            {
                model.LSTrainingTypeID = model.LSTrainingTypeIDAllowNull.Value;
            }
            if (model.LSCompanyID == 0)
            {
                model.LSCompanyID = null;
            }
            // Setting information for Duration, DurationUser, Expense
            var listOfDurationUser = this.TempData["DurationUser"] as List<TrainingRequestDurationUserViewModel>;
            var listOfExpense = this.TempData["expense"] as List<TrainingRequestCostViewModel>;
            var listOfDuration = this.TempData["Duration"] as List<TrainingRequestDurationViewModel>;
            if (listOfDurationUser == null || listOfExpense == null || listOfDuration == null)
            {
                return null;
            }
            model.NumOfStaff = listOfDurationUser.Count;                        
            decimal ActualCost = 0;
            foreach (var item in listOfExpense)
            {
                if (item.ActualCost.HasValue)
                {
                    ActualCost += item.ActualCost.Value;
                }                    
            }
            model.TotalCost = ActualCost;
            if (model.TrainingPlanID.HasValue)
            {
                model.TrainingType = (int)TrainingRequestType.FromPlan;
            }
            else
            {
                model.TrainingType = (int)TrainingRequestType.AdHoc;                
            }
            if (!model.EvaluationRequired)
            {
                model.EvaluationTemplateID = null;
            }
            model.CourseCompleteStatus = false;

            if (mode == "Save")
            {
                model.Status = (int)TrainingRequestStatus.Save;
                model.LevelApprove = (int)LevelApproveStatus.Level0;
                model.isFirstComment = true;
            }
            else if (mode == "SendForApproval")
            {
                model.Status = (int)TrainingRequestStatus.WaitingForApproval;
                model.LevelApprove = (int)LevelApproveStatus.Level1;
                model.isFirstComment = false;
            }

            // Information of adding, updating
            model.EmpIDCreate = this.CurrentEmpId.Value;
            model.CreateTime = DateTime.Now;
            model.Creater = this.CurrentEmpId.Value.ToString();
            
            // Mapping from TrainingRequestViewModel to TR_tblPlan
            Mapper.CreateMap<TrainingRequestViewModel, TR_tblTrainingRequest>();
            TR_tblTrainingRequest modelForAdd = Mapper.Map<TR_tblTrainingRequest>(model);

            modelForAdd.TR_tblTrainingRequestCost = (from expense in listOfExpense
                                               select new TR_tblTrainingRequestCost()
                                               {
                                                   TrainingRequestID = expense.TrainingRequestID,
                                                   LSTrainingExpenseID = expense.LSTrainingExpenseID,
                                                   LSCurrencyID = expense.LSCurrencyID,
                                                   Cost = expense.Cost,
                                                   ActualCost = expense.ActualCost
                                               }).ToList();


            modelForAdd.TR_tblTrainingRequestDuration = (from duration in listOfDuration
                                                   select new TR_tblTrainingRequestDuration()
                                                   {
                                                       TrainingDurationID = duration.TrainingDurationID,
                                                       TrainingRequestID = duration.TrainingRequestID,
                                                       FromDate = duration.FromDate,
                                                       ToDate = duration.ToDate,
                                                       TotalDays = duration.TotalDaysAllowNull.Value,
                                                       TotalHours = duration.TotalHours
                                                   }).ToList();

            foreach (var item in modelForAdd.TR_tblTrainingRequestDuration)
            {
                var items = (from durationUser in listOfDurationUser
                             where durationUser.FromDate == item.FromDate && durationUser.ToDate == item.ToDate
                             select new TR_tblDurationUser()
                             {
                                 TrainingDurationID = item.TrainingDurationID,
                                 EmpID = durationUser.EmpID
                             }).ToList();
                item.TR_tblDurationUser = items;
            }
            if (mode == "SendForApproval")
            {
                // Add a comment
                var comment = new TR_tblTrainingRequestComment
                {
                    Comment = model.Comment,
                    EmpID = this.CurrentAcc.EmpId,
                    StatusName = TrainingPlanStatus.WaitingForApproval.ToString(),
                    Time = DateTime.Now,
                    TraningRequestID = modelForAdd.TrainingRequestID
                };

                modelForAdd.TR_tblTrainingRequestComment.Add(comment);
            }
            return modelForAdd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="mode"></param>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        private TR_tblTrainingRequest GettingAndMappingTrainingRequestForUpdating(TrainingRequestViewModel model, string mode)
        {
            var errorMessage = String.Empty;
            var repository = new TrainingRequestRepository(this.db);
            var found = repository.Find(model.TrainingRequestID, out errorMessage);
            if (found == null)
            {
                return null;
            }
            // Reset information from ControllAllowNull
            if (model.LSTrainingCategoryIDAllowNull.HasValue)
            {
                found.LSTrainingCategoryID = model.LSTrainingCategoryIDAllowNull.Value;
            }
            if (model.LSTrainingCodeIDAllowNull.HasValue)
            {
                found.LSTrainingCodeID = model.LSTrainingCodeIDAllowNull.Value;
            }
            if (model.LSTrainingCourseIDAllowNull.HasValue)
            {
                found.LSTrainingCourseID = model.LSTrainingCourseIDAllowNull.Value;
            }
            if (model.LSTrainingTypeIDAllowNull.HasValue)
            {
                found.LSTrainingTypeID = model.LSTrainingTypeIDAllowNull.Value;
            }
            found.LSTrainingFormID = model.LSTrainingFormID;
            found.LSTrainingLocationID = model.LSTrainingLocationID;
            found.LSProviderID = model.LSProviderID;
            found.LSCompanyID = model.LSCompanyID;
            if (found.LSCompanyID == 0)
            {
                found.LSCompanyID = null;
            }
            found.Comment = model.Comment;
            if (model.TrainingPlanID.HasValue)
            {
                found.TrainingType = (int)TrainingRequestType.FromPlan;
            }
            else
            {
                found.TrainingType = (int)TrainingRequestType.AdHoc;
            }            
            found.CourseCompleteStatus = false;

            if (mode == "Save")
            {
                found.Status = (int)TrainingRequestStatus.Save;
                found.LevelApprove = (int)LevelApproveStatus.Level0;
                found.isFirstComment = true;
            }
            else if (mode == "SendForApproval")
            {
                found.Status = (int)TrainingRequestStatus.WaitingForApproval;
                found.LevelApprove = (int)LevelApproveStatus.Level1;
                found.isFirstComment = false;
            }
            found.EvaluationRequired = model.EvaluationRequired;
            if (found.EvaluationRequired)
            {
                found.EvaluationTemplateID = model.EvaluationTemplateID;
            }
            else
            {
                found.EvaluationTemplateID = null;
            }

            // Information of adding, updating
            found.CreateTime = DateTime.Now;
            found.Creater = this.CurrentEmpId.Value.ToString();

            // Setting information for Duration, DurationUser, Expense
            var listOfDurationUser = this.TempData["DurationUser"] as List<TrainingRequestDurationUserViewModel>;
            var listOfExpense = this.TempData["expense"] as List<TrainingRequestCostViewModel>;
            var listOfDuration = this.TempData["Duration"] as List<TrainingRequestDurationViewModel>;
            if (listOfDurationUser == null || listOfExpense == null || listOfDuration == null)
            {
                return null;
            }            
            
            // TrainingRequestCost         
            decimal TotalCost = 0;
            found.TR_tblTrainingRequestCost.Clear();
            foreach (var item in listOfExpense)
            {
                var cost = new TR_tblTrainingRequestCost();
                cost.TrainingRequestID = item.TrainingRequestID;
                cost.LSTrainingExpenseID = item.LSTrainingExpenseID;
                cost.LSCurrencyID = item.LSCurrencyID;
                cost.Cost = item.Cost;
                cost.ActualCost = item.ActualCost;
                TotalCost += item.ActualCost.Value;

                found.TR_tblTrainingRequestCost.Add(cost);                
            }
            found.TotalCost = TotalCost;

            // TrainingRequestDuration
            var result = db.TR_tblTrainingRequestDuration.Where(p => p.TrainingRequestID == found.TrainingRequestID).ToList();
            if (result != null)
            {
                foreach (var item in result)
                {

                   var check = listOfDuration.Where(p => p.TrainingDurationID == item.TrainingDurationID).FirstOrDefault();
                   if (check == null)
                   {
                       db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                   }
                   else
                   {
                       db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                   }
                }
            }
            foreach (var Duration in listOfDuration)
            {
                if (Duration.TrainingDurationID == 0)
                {
                    TR_tblTrainingRequestDuration modelAdd = new TR_tblTrainingRequestDuration();
                    Mapper.CreateMap<TrainingRequestDurationViewModel, TR_tblTrainingRequestDuration>();
                    Mapper.Map(Duration, modelAdd);
                    modelAdd.TrainingRequestID = model.TrainingRequestID;
                    db.Entry(modelAdd).State = System.Data.Entity.EntityState.Added;
                }
            }                       
            // TrainingRequestDurationUser
            foreach (var item in result)
            {
                List<TR_tblDurationUser> resultOfDurationUser = this.db.TR_tblDurationUser.Where(p => p.TrainingDurationID == item.TrainingDurationID).ToList();

                foreach (var itemDetail in resultOfDurationUser)
                {
                    var check = listOfDurationUser.Where(p => p.TrainingDurationID == itemDetail.TrainingDurationID && p.EmpID == itemDetail.EmpID).FirstOrDefault();
                    if (check == null)
                    {
                        this.db.Entry(itemDetail).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
            }
                                 
            foreach (var item in listOfDurationUser)
            {
                if (item.TrainingDurationID == 0)
                {
                    var itemDetail = listOfDuration.Where(p => p.FromDate == item.FromDate && p.ToDate == item.ToDate).FirstOrDefault();
                    if (itemDetail != null)
                    {
                        TR_tblDurationUser modelAdd = new TR_tblDurationUser();
                        Mapper.CreateMap<TrainingRequestDurationUserViewModel, TR_tblDurationUser>();
                        Mapper.Map(item, modelAdd);
                        modelAdd.TrainingDurationID = itemDetail.TrainingDurationID;
                        db.Entry(modelAdd).State = System.Data.Entity.EntityState.Added;                        
                    }
                }
            }
            if (mode == "SendForApproval")
            {
                // Add a comment
                var comment = new TR_tblTrainingRequestComment
                {
                    Comment = model.Comment,
                    EmpID = this.CurrentAcc.EmpId,
                    StatusName = TrainingPlanStatus.WaitingForApproval.ToString(),
                    Time = DateTime.Now,
                    TraningRequestID = found.TrainingRequestID
                };
                found.TR_tblTrainingRequestComment.Add(comment);
            }            
            return found;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<TR_tblDurationUser> GetListOfDurationUser(TR_tblTrainingRequest model)
        {
            var listOfDurationUser = this.TempData["DurationUser"] as List<TrainingRequestDurationUserViewModel>;
            if (listOfDurationUser == null)
            {
                return null;
            }
            var result = new List<TR_tblDurationUser>();
            foreach (var item in listOfDurationUser)
            {
                var itemOfDuration = model.TR_tblTrainingRequestDuration.Where(p => p.FromDate == item.FromDate && p.ToDate == item.ToDate).FirstOrDefault();
                if (itemOfDuration != null)
                {
                    result.Add(new TR_tblDurationUser()
                    {
                        TrainingDurationID = itemOfDuration.TrainingDurationID,
                        EmpID = item.EmpID
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string CheckValidation(TrainingRequestViewModel model)
        {
            var errorMessage = String.Empty;
            if (!this.ModelState.IsValid)
            {
                var errors = this.ModelState.Values.SelectMany(e => e.Errors);
                errorMessage = String.Empty;
                foreach (var errorModel in errors)
                {
                    errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", errorModel.ErrorMessage);
                }
            }
            var ListOfDuration = this.TempData["Duration"] as List<TrainingRequestDurationViewModel>;
            if (ListOfDuration == null)
            {
                ListOfDuration = new List<TrainingRequestDurationViewModel>();
            }
            if (ListOfDuration.Count == 0)
            {
                errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", Eagle.Resource.LanguageResource.PleaseInputTrainingRequestDuration);
            }
            var ListOfDurationUser = this.TempData["DurationUser"] as List<TrainingRequestDurationUserViewModel>;
            if (ListOfDurationUser == null)
            {
                ListOfDurationUser = new List<TrainingRequestDurationUserViewModel>();
            }
            if (ListOfDurationUser.Count == 0)
            {
                errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", Eagle.Resource.LanguageResource.PleaseInputTrainingRequestDurationUser);
            }
            var ListOfExpense = this.TempData["Expense"] as List<TrainingRequestCostViewModel>;
            if (ListOfExpense == null)
            {
                ListOfExpense = new List<TrainingRequestCostViewModel>();
            }
            if (ListOfExpense.Count == 0)
            {
                errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", Eagle.Resource.LanguageResource.PleaseInputTrainingRequestCost);
            }
            return errorMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(string mode, TrainingRequestViewModel model)
        {
            // 1. Checking timeout
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }                                    
            // 2. Checking of Bussiness          
            var errorMessage = this.CheckValidation(model);
            if (!String.IsNullOrEmpty(errorMessage))
            {
                return this._Error(model, errorMessage);
            }
            // 3. Getting data
            var modelTrainingRequest = this.GettingAndMappingTrainingRequestForAdding(model, mode);
            if (modelTrainingRequest == null)
            {
                return this._Error(model, errorMessage);
            }                        
            // 4. Updating database                        
            TrainingRequestRepository repository = new TrainingRequestRepository(this.db);
            bool result = repository.Add(modelTrainingRequest, out errorMessage);
            if (result)
            {
                this.TempData.Clear();
                return this.Content("success");
            }
            else
            {
                return this._Error(model, errorMessage);
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(TrainingRequestViewModel model, string mode)
        {
            // 1. Checking timeout
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }
            // 2. Checking of Bussiness          
            var errorMessage = this.CheckValidation(model);
            if (!String.IsNullOrEmpty(errorMessage))
            {
                return this._Error(model, errorMessage);
            }            
            // 3.2 Getting TrainingRequest updated
            var modelTrainingRequest = this.GettingAndMappingTrainingRequestForUpdating(model, mode);
            if (modelTrainingRequest == null)
            {
                return this._Error(model, errorMessage);
            }

            // 4. Updating database 
            var repository = new TrainingRequestRepository(this.db);
            var result = repository.Update(modelTrainingRequest, out errorMessage);
            if (result)
            {
                this.TempData.Clear();
                return this.Content("success");
            }
                        
            return this._Error(model, errorMessage);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Approval(TrainingRequestViewModel model, string mode, int? ReturnLevelApprove)
        {
            string errorMessage;
            var repository = new TrainingRequestRepository(this.db);
            var found = repository.Find(model.TrainingRequestID, out errorMessage);
            found.Comment = model.Comment;
            if (found == null)
            {
                return this._Error(model, errorMessage);
            }
            if (found.LevelApprove == model.LevelApprove)
            {
                var objOnlineProcess = this.db.LS_tblOnlineProcess.Where(p => p.FunctionID == TRAININGREQUEST_FUNCTIONID).FirstOrDefault();
                if (mode == "Approval")
                {
                    found.Status = found.LevelApprove * 2;
                    if (found.LevelApprove < objOnlineProcess.NoOfLevel)
                    {
                        found.LevelApprove += 1;
                    }                    
                }
                else if (mode == "UnApproval")
                {
                    found.Status = (found.LevelApprove * 2) + 1;
                    if (objOnlineProcess.IsStart == 1)
                    {
                        found.LevelApprove -= 1;
                    }
                    else if (objOnlineProcess.IsStart == 0)
                    {
                        found.Status = 0;
                    }
                    else if (objOnlineProcess.IsStart == 2)
                    {
                        if (ReturnLevelApprove != null)
                        {
                            found.LevelApprove = ReturnLevelApprove.Value;
                        }
                    }
                }
                // Add a comment                
                found.TR_tblTrainingRequestComment.Add(new TR_tblTrainingRequestComment
                {
                    TraningRequestID = found.TrainingRequestID,
                    Comment = found.Comment,
                    EmpID = this.CurrentAcc.EmpId,
                    StatusName = this.GetStatusName(found.Status),
                    Time = DateTime.Now
                });
                // Setting system information for approval, unapproval  
                found.Comment = model.Comment;               
                
                bool result = repository.Approval(found, out errorMessage);
                if (result)
                {
                    var resultOfSendMail = false;
                    if (mode == "Approval")
                    {
                        resultOfSendMail = this.SendMailForApproval(found, out errorMessage);

                    }
                    else if (mode == "UnApproval")
                    {
                        resultOfSendMail = this.SendMailForUnApproval(found, out errorMessage);
                    }                                      
                    return this.Content("success");
                }
                return this._Error(model, errorMessage);
            }
            return this.Content("Error");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private string GetStatusName(int status)
        {
            string statusName = String.Empty;
            var ProcessOnlineMaster = db.LS_tblOnlineProcess.Where(p => p.FunctionID == TRAININGREQUEST_FUNCTIONID).Select(p => p.SYS_tblProcessOnlineMaster).FirstOrDefault();
            if (ProcessOnlineMaster != null)
            {
                var statusModel = ProcessOnlineMaster.FirstOrDefault();
                if (statusModel != null)
                {
                    switch (status)
                    {
                        case 0:
                            statusName = TrainingRequestStatus.Save.ToString();
                            break;
                        case 1:
                            statusName = TrainingRequestStatus.WaitingForApproval.ToString();
                            break;
                        case 2:
                            statusName = statusModel.StatusLevel1A;
                            break;
                        case 3:
                            statusName = statusModel.StatusLevel1U;
                            break;
                        case 4:
                            statusName = statusModel.StatusLevel2A;
                            break;
                        case 5:
                            statusName = statusModel.StatusLevel2U;
                            break;
                        case 6:
                            statusName = statusModel.StatusLevel3A;
                            break;
                        case 7:
                            statusName = statusModel.StatusLevel3U;
                            break;
                        case 8:
                            statusName = statusModel.StatusLevel4A;
                            break;
                        case 9:
                            statusName = statusModel.StatusLevel4U;
                            break;
                        case 10:
                            statusName = statusModel.StatusLevel5A;
                            break;
                        case 11:
                            statusName = statusModel.StatusLevel4U;
                            break;
                    }
                }
            }
            return statusName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrainingRequestID"></param>
        /// <returns></returns>
        public ActionResult GetLearningObjective(int LSTrainingCourseID)
        {
            string errorMessage;
            var repository = new TrainingPlanRepository(this.db);
            var result = repository.GetLearningObjective(LSTrainingCourseID, out errorMessage);
            if (String.IsNullOrEmpty(errorMessage))
            {
                return this.Content(result);
            }
            return this.Content("error");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LSTrainingExpenseID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ActionResult GetValueOfTrainingExpense(int LSTrainingExpenseID, string value)
        {
            var listOfTrainingCost = this.TempData["Expense"] as List<TrainingRequestCostViewModel>;
            if (listOfTrainingCost == null)
            {
                return this.Content("error");
            }
            foreach (var item in listOfTrainingCost)
            {
                if (item.LSTrainingExpenseID == LSTrainingExpenseID)
                {
                    item.ActualCost = Convert.ToDecimal(value);
                    break;
                }
            }
            this.TempData["Expense"] = listOfTrainingCost;
            return this.PartialView("../TR/TrainingRequest/_ListOfCost", listOfTrainingCost);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="TotalDays"></param>
        /// <param name="TotalHours"></param>
        /// <returns></returns>
        public ActionResult GetValueOfTrainingDuration(string FromDate, string ToDate, int TotalDays, int TotalHours)
        {
            var listOfDuration = this.TempData["Duration"] as List<TrainingRequestDurationViewModel>;
            if (listOfDuration == null)
            {
                return this.Content("error");
            }
            foreach (var item in listOfDuration)
            {
                if (DateTimeUtils.ToString(item.FromDate.Value, "dd/MM/yyyy") == FromDate && DateTimeUtils.ToString(item.ToDate.Value, "dd/MM/yyyy") == ToDate)
                {
                    item.TotalDays = TotalDays;
                    item.TotalHours = TotalHours;
                    break;
                }
            }
            this.TempData["Duration"] = listOfDuration;
            return this.PartialView("../TR/TrainingRequest/_ListOfDuration", listOfDuration);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _Error(TrainingRequestViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new TrainingRequestViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            this.CreateViewBag(model);
            return PartialView("../TR/TrainingRequest/_Create", model);
        }
        
        #endregion

        #region Sending Mail Training Request

        /// <summary>
        /// List of mail template for sending mail
        /// </summary>
        public const int MAIL_TEMPLATE_ID_FOR_REQUEST_APPROVER = 24;
        public const int MAIL_TEMPLATE_ID_FOR_REQUEST_CREATER = 25;
        public const int MAIL_TEMPLATE_ID_FOR_REQUEST_UNAPPROVER = 26;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmpIDCreate"></param>
        /// <param name="LevelApproval"></param>
        /// <param name="FullName"></param>
        /// <param name="EmailAddress"></param>
        private void GetFullNameAndEmailAddressOfApprover(int EmpIDCreate, int LevelApproval, out string FullName, out string EmailAddress)
        {
            // Get Fullname and email address of approver 
            var employee = db.HR_tblEmp.Find(EmpIDCreate);
            var from = "";
            var dearToFullName = "";
            var toEmail = "";
            if (employee != null)
            {
                from = employee.LastName + " " + employee.FirstName;
                switch (LevelApproval)
                {
                    case 1:
                        // gửi tới nhân viên approve
                        var result = (from p in db.SYS_tblOnlineProcessEmp
                                      join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                      join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                      join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                      where p.ApproveLevel1 == true && p.LSCompanyID == employee.LSCompanyID &&
                                      o.FunctionID == TRAININGREQUEST_FUNCTIONID
                                      select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result != null)
                        {
                            dearToFullName = result.LastName + " " + result.FirstName;
                            toEmail = result.Email;
                        }
                        break;
                    case 2:
                        // gửi tới nhân viên approve
                        var result2 = (from p in db.SYS_tblOnlineProcessEmp
                                       join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                       join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                       join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                       where p.ApproveLevel2 == true &&
                                             p.LSCompanyID == employee.LSCompanyID &&

                                             o.FunctionID == TRAININGREQUEST_FUNCTIONID
                                       select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result2 != null)
                        {
                            dearToFullName = result2.LastName + " " + result2.FirstName;
                            toEmail = result2.Email;
                        }
                        break;
                    case 3:
                        // gửi tới nhân viên approve
                        var result3 = (from p in db.SYS_tblOnlineProcessEmp
                                       join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                       join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                       join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                       where p.ApproveLevel3 == true &&
                                             p.LSCompanyID == employee.LSCompanyID &&
                                       o.FunctionID == TRAININGREQUEST_FUNCTIONID
                                       select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result3 != null)
                        {
                            dearToFullName = result3.LastName + " " + result3.FirstName;
                            toEmail = result3.Email;
                        }
                        break;
                    case 4:
                        // gửi tới nhân viên approve
                        var result4 = (from p in db.SYS_tblOnlineProcessEmp
                                       join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                       join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                       join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                       where p.ApproveLevel4 == true &&
                                             p.LSCompanyID == employee.LSCompanyID &&
                                       o.FunctionID == TRAININGREQUEST_FUNCTIONID
                                       select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result4 != null)
                        {
                            dearToFullName = result4.LastName + " " + result4.FirstName;
                            toEmail = result4.Email;
                        }
                        break;
                    case 5:
                        // gửi tới nhân viên approve
                        var result5 = (from p in db.SYS_tblOnlineProcessEmp
                                       join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                       join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                       join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                       where p.ApproveLevel5 == true &&
                                             p.LSCompanyID == employee.LSCompanyID &&
                                       o.FunctionID == TRAININGREQUEST_FUNCTIONID
                                       select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result5 != null)
                        {
                            dearToFullName = result5.LastName + " " + result5.FirstName;
                            toEmail = result5.Email;
                        }
                        break;
                    default: break;
                }
            }
            FullName = dearToFullName;
            EmailAddress = toEmail;
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool SendMailForApproval(TR_tblTrainingRequest model, out string errorMessage)
        {
            try
            {
                // 1 Getting info about fullname, and emali address of Approver  
                string FullName = String.Empty;
                string ToMail = String.Empty;
                this.GetFullNameAndEmailAddressOfApprover(model.EmpIDCreate, model.LevelApprove, out FullName, out ToMail);

                // 2 Sending mail to approver
                this.SendMailOfTrainingRequest(MAIL_TEMPLATE_ID_FOR_REQUEST_APPROVER, ToMail, String.Empty, String.Empty, FullName, this.CurrentAcc.FullName, model.TrainingRequestID);

                // 3 Sending mail to creater 
                this.SendMailToRequestCreater(model, out errorMessage);
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
        private bool SendMailToRequestCreater(TR_tblTrainingRequest model, out string errorMessage)
        {
            try
            {
                string DearFullName = String.Empty;
                string ToEmail = String.Empty;
                string CCMail = String.Empty;
                string BCCMail = String.Empty;
                this.GetCcAndBcc(out CCMail, out BCCMail, TRAININGREQUEST_FUNCTIONID);

                var employee = db.HR_tblEmp.Find(model.EmpIDCreate);
                if (employee != null)
                {
                    DearFullName = employee.LastName + " " + employee.FirstName;
                    ToEmail = employee.Email;
                }
                this.SendMailOfTrainingRequest(MAIL_TEMPLATE_ID_FOR_REQUEST_CREATER, ToEmail, CCMail, BCCMail, DearFullName, CurrentAcc.FullName, model.TrainingRequestID);
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
        private bool SendMailForUnApproval(TR_tblTrainingRequest model, out string errorMessage)
        {
            try
            {
                string ccMail = String.Empty;
                string bccMail = String.Empty;
                GetCcAndBcc(out ccMail, out bccMail, TRAININGREQUEST_FUNCTIONID);
                var employee = db.HR_tblEmp.Find(model.EmpIDCreate);
                // 1. Sending mail for plan creater 
                this.SendMailOfTrainingRequest(MAIL_TEMPLATE_ID_FOR_REQUEST_UNAPPROVER, employee.Email, ccMail, bccMail, employee.LastName + " " + employee.FirstName, this.CurrentAcc.FullName, model.TrainingRequestID);

                // 2. Sending mail for plan unapproval
                if (model.LevelApprove != 0)
                {
                    HR_tblEmp Employee = new HR_tblEmp();
                    int OnlineProcessID = db.LS_tblOnlineProcess.Where(p => p.FunctionID == TRAININGREQUEST_FUNCTIONID).FirstOrDefault().SYS_tblProcessOnlineMaster.FirstOrDefault().OnlineProcessID;
                    switch (model.LevelApprove)
                    {
                        case 1:
                            Employee = (from opm in db.SYS_tblOnlineProcessEmp
                                        join em in db.HR_tblEmp on opm.EmpID equals em.EmpID
                                        where opm.OnlineProcessID == OnlineProcessID &&
                                        opm.LSCompanyID == employee.LSCompanyID && opm.ApproveLevel1 == true
                                        select em).FirstOrDefault();
                            break;
                        case 2:
                            Employee = (from opm in db.SYS_tblOnlineProcessEmp
                                        join em in db.HR_tblEmp on opm.EmpID equals em.EmpID
                                        where opm.OnlineProcessID == OnlineProcessID &&
                                        opm.LSCompanyID == employee.LSCompanyID && opm.ApproveLevel2 == true
                                        select em).FirstOrDefault();
                            break;
                        case 3:
                            Employee = (from opm in db.SYS_tblOnlineProcessEmp
                                        join em in db.HR_tblEmp on opm.EmpID equals em.EmpID
                                        where opm.OnlineProcessID == OnlineProcessID &&
                                        opm.LSCompanyID == employee.LSCompanyID && opm.ApproveLevel3 == true
                                        select em).FirstOrDefault();
                            break;
                        case 4:
                            Employee = (from opm in db.SYS_tblOnlineProcessEmp
                                        join em in db.HR_tblEmp on opm.EmpID equals em.EmpID
                                        where opm.OnlineProcessID == OnlineProcessID &&
                                        opm.LSCompanyID == employee.LSCompanyID && opm.ApproveLevel4 == true
                                        select em).FirstOrDefault();
                            break;
                    }
                    if (Employee != null)
                    {
                        this.SendMailOfTrainingRequest(MAIL_TEMPLATE_ID_FOR_REQUEST_UNAPPROVER, Employee.Email, String.Empty, String.Empty, Employee.LastName + " " + Employee.FirstName, CurrentAcc.FullName, model.TrainingRequestID);
                    }
                }
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
        /// <param name="ccMail"></param>
        /// <param name="bccMail"></param>
        /// <param name="functionID"></param>
        private void GetCcAndBcc(out string ccMail, out string bccMail, int functionID)
        {
            var resultCC = (from o in db.LS_tblOnlineProcess
                            join cc in db.LS_tblOnlineProcessMailCc on o.DMQuiTrinhID equals cc.DMQuiTrinhID
                            join emp in db.HR_tblEmp on cc.EmpID equals emp.EmpID
                            where o.FunctionID == functionID
                            select emp.Email).ToArray();
            var resultBcc = (from o in db.LS_tblOnlineProcess
                             join cc in db.LS_tblOnlineProcessMailBcc on o.DMQuiTrinhID equals cc.DMQuiTrinhID
                             join emp in db.HR_tblEmp on cc.EmpID equals emp.EmpID
                             where o.FunctionID == functionID
                             select emp.Email).ToArray();
            ccMail = string.Join(";", resultCC);
            bccMail = string.Join(";", resultBcc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MailTemplateID"></param>
        /// <param name="MailTo"></param>
        /// <param name="Cc"></param>
        /// <param name="Bcc"></param>
        /// <param name="DearFullName"></param>
        /// <param name="FromFullName"></param>
        /// <param name="TrainingPlanID"></param>
        /// <returns></returns>
        private bool SendMailOfTrainingRequest(int MailTemplateID, string MailTo, string Cc, string Bcc, string DearFullName, string FromFullName, int TrainingRequestID)
        {
            try
            {
                string link = domainUrl + "Admin/TrainingRequest/Index?TrainingRequestID=" + TrainingRequestID;
                Hashtable TemplateVariables = new Hashtable();
                TemplateVariables.Add("FullName", DearFullName);
                TemplateVariables.Add("From", FromFullName);
                TemplateVariables.Add("Link", link);

                return MailTemplateRespository.SendGMailByTemplate(TemplateVariables, MailTemplateID, MailTo, Cc, Bcc);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #endregion 


        #region Popup of Employee list
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _SearchAreasPartial()
        {
            return PartialView("../TR/TrainingRequest/_SearchAreasPartial");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _PopupEmployeePartial()
        {
            return PartialView("../TR/TrainingRequest/_PopupEmployeePartial");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmpCode"></param>
        /// <param name="FullName"></param>
        /// <param name="LSCompanyID"></param>
        /// <param name="Active"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public ActionResult _SearchResultsForPopupTrainingRequest(string EmpCode, string FullName, int? LSCompanyID, int? moduleId)
        {
            // nếu gọi bằng ajax thì show thông báo không tìm thấy kết quả
            if (!Request.IsAjaxRequest())
            {
                ViewBag.FirstRequest = true;
            }
            UserRepository _repository = new UserRepository(db);
            //Tìm trong db
            List<EmployeeViewModel> Employeelst = new List<EmployeeViewModel>();

            var account = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (account != null)
            {
                Employeelst = _repository.FindEmployee(EmpCode, FullName, LSCompanyID, true, account.UserName, moduleId, account.FAdm == 1);
            }


            return PartialView("../TR/TrainingRequest/_SearchResultsForPopupTrainingRequest", Employeelst);
        }
        #endregion
    }
}

       