using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;
using AutoMapper;
using Eagle.Common.Helpers;

using Eagle.Repository.Mail;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers 
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingPlanController : BaseController
    {
        public const int TRAININGPLAN_FUNCTIONID = 446;        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? TrainingPlanID)
        {
            if (TrainingPlanID.HasValue)
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri + "?TrainingPlanID=" + TrainingPlanID.ToString());
                    return null;
                }

                return View("../Business/HumanResources/Training/TrainingPlan/Edit", TrainingPlanID);
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("../Business/HumanResources/Training/TrainingPlan/_Reset", TrainingPlanID);
                }
                else
                {
                    if (CurrentAcc == null)
                    {
                        Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                        return null;
                    }
                    return View("../Business/HumanResources/Training/TrainingPlan/Index");
                }
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _Create()
        {
            this.TempData.Clear();
            TrainingPlanViewModel model = new TrainingPlanViewModel();            
            model.ListOfTrainingExpense = new List<TrainingPlanExpenseModelView>();
            model.TR_tblPlanComment = new List<TR_tblPlanComment>();
            this.CreateViewBag(model, DateTime.Now.Month, DateTime.Now.Year);
            return PartialView("../Business/HumanResources/Training/TrainingPlan/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _List(int? TrainingPlanID)
        {
            if (TrainingPlanID.HasValue)
            {
                var repository = new TrainingPlanRepository(this.db);
                string errorMesage;
                var result = repository.GetListOfTrainingPlanExpense(TrainingPlanID.Value, out errorMesage);
                if (result == null)
                {
                    return this.Content("Error");
                }
                return this.PartialView("../Business/HumanResources/Training/TrainingPlan/_List", result);    
            }
            return this.Content("error");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AddItem(TrainingPlanViewModel model)
        {
            // Get information of TrainingPlanExpense
            var result = new TrainingPlanExpenseModelView();
            if (model.LSTrainingExpenseID.HasValue)
            {
                result.TrainingPlanExpenseID = model.LSTrainingExpenseID.Value; 
            }            
            result.TrainingPlanID = model.TrainingPlanID;
            if (model.LSTrainingExpenseID.HasValue)
            {
                result.LSTrainingExpenseID = model.LSTrainingExpenseID.Value;
            }
            result.TrainingExpenseName = model.TrainingExpenseName;
            result.LSCurrencyID = model.LSCurrencyID;
            result.TrainingCurrencyName = model.TrainingCurrencyName;
            result.Cost = model.Cost;

            bool found = false;
            foreach (var value in this.TempData.Values)
            {
                var objExpense = value as TrainingPlanExpenseModelView;
                if (objExpense != null)
                {
                    if (objExpense.LSTrainingExpenseID == result.LSTrainingExpenseID)
                    {
                        found = true;
                        break;
                    }
                }
            }
            if (found == false)
            {
                this.TempData.Add(this.TempData.Count.ToString(), result);
            }
            model.ListOfTrainingExpense = new List<TrainingPlanExpenseModelView>();
            foreach (var item in this.TempData.ToList())
            {
                var value = item.Value as TrainingPlanExpenseModelView;
                if (value != null)
                {
                    model.ListOfTrainingExpense.Add(value);
                }
            }
            if (found)
            {
                return this._Error(model, Eagle.Resource.LanguageResource.TrainingExpenseExisted);
            }
            
            this.CreateViewBag(model, DateTime.Now.Month, DateTime.Now.Year);
            return this.PartialView("../Business/HumanResources/Training/TrainingPlan/_Create", model);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _DeleteExpense(TrainingPlanViewModel model, int id)
        {
            foreach (var temp in this.TempData)
            {
                var objExpense = temp.Value as TrainingPlanExpenseModelView;
                if (objExpense != null && objExpense.LSTrainingExpenseID == id)
                {
                    this.TempData.Remove(temp.Key);
                    break;
                }
            }
            model.ListOfTrainingExpense = new List<TrainingPlanExpenseModelView>();
            foreach (var item in this.TempData.ToList())
            {
                var value = item.Value as TrainingPlanExpenseModelView;
                if (value != null)
                {
                    model.ListOfTrainingExpense.Add(value); 
                }
            }
            this.CreateViewBag(model, DateTime.Now.Month, DateTime.Now.Year);
            return this.PartialView("../Business/HumanResources/Training/TrainingPlan/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool IsAllowSaveAndSendForApproval(TR_tblPlan model)
        {
            bool result = false;
            // Logging into system is admin
            if (this.CurrentEmpId == null || this.CurrentEmpId.Value == 0)
            {
                return false;
            }
            // For case of adding 
            if (model.TrainingPlanID == 0)
            {
                result = true;
            }
            // For case of updating
            else if (model.TrainingPlanID > 0)
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
        private void CreateViewBag(TR_tblPlan model, int month, int year)
        {
            // Setting DropDownList of months, years
            List<int> months = new List<int>();            
            for (int i = 1; i <= 12; i++)
            {
                months.Add(i);
            }
            ViewBag.TrainingMonths = new SelectList(months, month);
            var years = new List<int>();            
            for (int i = DateTime.Now.Year - 10; i <= DateTime.Now.Year + 10; i++)
            {
                years.Add(i);
            }
            this.ViewBag.TrainingYears = new SelectList(years, year);

            // Checking permission
            var permission = new CommonRepository(this.db);
            bool EnableSendAndSendForApproval = false, EnableApprovalAndUnApproval = false;            
            int maxLevelApprove = db.LS_tblOnlineProcess.Where(p => p.FunctionID == TRAININGPLAN_FUNCTIONID).FirstOrDefault().NoOfLevel;

            permission.CheckPermission(model.Status, CurrentEmpId, model.EmpIDCreate, TRAININGPLAN_FUNCTIONID, model.LevelApprove, maxLevelApprove, model.TrainingPlanID, ref EnableSendAndSendForApproval, ref EnableApprovalAndUnApproval);

            // Setting the enable of button
            this.ViewBag.EnableSaveAndSendForApproval = this.IsAllowSaveAndSendForApproval(model) ? "" : "style=display:none;";
            this.ViewBag.EnableApprovalAndUnApproval = EnableApprovalAndUnApproval ? "" : "style=display:none;";
            LS_tblOnlineProcess opModel = db.LS_tblOnlineProcess.Where(p => p.FunctionID == TRAININGPLAN_FUNCTIONID).FirstOrDefault();
            if (opModel != null)
            {
                ViewBag.IsStart = opModel.IsStart;
            }
            var listOfComment = (from comment in model.TR_tblPlanComment
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
        public PartialViewResult _popupUnapprove(int LevelApprove)
        {
            // trả về người tạo và các cấp dưới "LevelApprove"
            //VD: nếu là cấp 3 trả về người tạo và cấp 1 2
            //    nếu là cấp 1 chỉ trả về người tạo
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(0, Eagle.Resource.LanguageResource.CreateUser);
            var LS_tblOnlineProcessModel = db.LS_tblOnlineProcess.Where(p => p.FunctionID == TRAININGPLAN_FUNCTIONID).FirstOrDefault();
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
            return PartialView("../Business/HumanResources/Training/TrainingPlan/_popupUnapprove");
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
            TrainingPlanRepository repository = new TrainingPlanRepository(this.db);
            TrainingPlanViewModel model = repository.FindForEdit(id, out errorMessage);            
            if (model == null)
            {
                return this._Error(model, errorMessage);
            }
            int key = 0;
            foreach (var item in model.ListOfTrainingExpense)
            {
                this.TempData.Add(key.ToString(), item);
                key++;
            }
            this.CreateViewBag(model, model.PlanMonth, model.PlanYear);           

            return PartialView("../Business/HumanResources/Training/TrainingPlan/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="mode"></param>
        /// <param name="TrainingMonths"></param>
        /// <param name="TrainingYears"></param>
        /// <returns></returns>
        private TR_tblPlan GetModelOfTrainingPlanForAdding(TrainingPlanViewModel model, string mode)
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
            if (model.NumOfStaffAllNull.HasValue)
            {
                model.NumOfStaff = model.NumOfStaffAllNull.Value;
            }
            if (mode == "Save")
            {
                model.Status = (int)TrainingPlanStatus.Save;
                model.LevelApprove = (int)LevelApproveStatus.Level0;
                model.CreateTime = DateTime.Now;
                model.isFirstComment = true;
                model.CurrentComment = model.Comment;
            }
            else if (mode == "SendForApproval")
            {
                model.Status = (int)TrainingPlanStatus.WaitingForApproval;
                model.LevelApprove = (int)LevelApproveStatus.Level1;
                model.DateLevel1 = DateTime.Now;
                model.isFirstComment = false;                
                model.CurrentComment = String.Empty;
            }
            // Set information from ViewBag
            if (!String.IsNullOrEmpty(model.MonthYear))
            {
                string monthInfo = model.MonthYear.Substring(0, 2);
                if (Eagle.Common.Utilities.ValidatorUtils.CheckNumber(monthInfo))
                {
                    model.PlanMonth = Convert.ToInt16(monthInfo);
                }
                string yearInfo = model.MonthYear.Substring(6, 4);
                if (Eagle.Common.Utilities.ValidatorUtils.CheckNumber(yearInfo))
                {
                    model.PlanYear = Convert.ToInt16(yearInfo);
                }
            }
            

            // Setting the default information about user, system date for adding
            AccountViewModel account = Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return null;
            }
            if (account.EmpId.HasValue)
            {
                model.EmpIDCreate = account.EmpId.Value;
                model.Creater = account.EmpId.Value.ToString();
                model.EmpIDLevel1 = account.EmpId.Value;
            }

            // Mapping from TrainingPlanViewModel to TR_tblPlan
            Mapper.CreateMap<TrainingPlanViewModel, TR_tblPlan>();
            TR_tblPlan modelForAdd = Mapper.Map<TR_tblPlan>(model);
            if (mode == "SendForApproval")
            {                
                // Add a comment
                var comment = new TR_tblPlanComment
                {
                    Comment = model.Comment,
                    EmpID = this.CurrentAcc.EmpId,
                    StatusName = TrainingPlanStatus.WaitingForApproval.ToString(),
                    Time = DateTime.Now,
                    TraningPlanID = modelForAdd.TrainingPlanID
                };

                modelForAdd.TR_tblPlanComment.Add(comment);
            }
            return modelForAdd;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(TrainingPlanViewModel model, string mode)
        {           
            string errorMessage;
            if (this.ModelState.IsValid)
            {
                if (model.LSCompanyID == 0)
                {
                    model.LSCompanyID = null;
                }
                //Master
                var modelTrainingPlan = this.GetModelOfTrainingPlanForAdding(model, mode);
                model.ListOfTrainingExpense = new List<TrainingPlanExpenseModelView>();
                //Detail
                modelTrainingPlan.TR_tblTrainingPlanExpense.Clear();
                foreach (var item in this.TempData.ToList())
                {
                    var value = item.Value as TrainingPlanExpenseModelView;
                    if (value != null)
                    {
                        modelTrainingPlan.TR_tblTrainingPlanExpense.Add(new TR_tblTrainingPlanExpense()
                        {
                            TrainingPlanID = modelTrainingPlan.TrainingPlanID,
                            LSTrainingExpenseID = value.LSTrainingExpenseID,
                            LSCurrencyID = value.LSCurrencyID,
                            Cost = value.Cost
                        });

                    }
                }

                TrainingPlanRepository repository = new TrainingPlanRepository(this.db);
                bool result = repository.Add(modelTrainingPlan, out errorMessage);
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
            var errors = this.ModelState.Values.SelectMany(e => e.Errors);            
            errorMessage = String.Empty;            
            foreach (var errorModel in errors)
            {
                errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", errorModel.ErrorMessage);
            }
            return this._Error(model, errorMessage);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(TrainingPlanViewModel model, string mode)
        {
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            string errorMessage;
            if (this.ModelState.IsValid)
            {
                var repository = new TrainingPlanRepository(this.db);
                var found = repository.Find(model.TrainingPlanID, out errorMessage);
                if (found == null)
                {
                    return this._Error(model, errorMessage);
                }
                found.LSCompanyID = model.LSCompanyID;
                if (found.LSCompanyID == 0)
                {
                    found.LSCompanyID = null;
                }
                //found.LSLevel1ID = model.LSLevel1ID;
                //found.LSLevel2ID = model.LSLevel2ID;
                if (model.LSTrainingCodeIDAllowNull.HasValue)
                {
                    found.LSTrainingCodeID = model.LSTrainingCodeIDAllowNull.Value;
                }
                if (model.LSTrainingCourseIDAllowNull.HasValue)
                {
                    found.LSTrainingCourseID = model.LSTrainingCourseIDAllowNull.Value;
                }                
                if (model.LSTrainingCategoryIDAllowNull.HasValue)
                {
                    found.LSTrainingCategoryID = model.LSTrainingCategoryIDAllowNull.Value;    
                }
                if (model.LSTrainingTypeIDAllowNull.HasValue)
                {
                    found.LSTrainingTypeID = model.LSTrainingTypeIDAllowNull.Value;
                }
                if (model.NumOfStaffAllNull.HasValue)
                {
                    found.NumOfStaff = model.NumOfStaffAllNull.Value;
                }
                found.LSProviderID = model.LSProviderID;
                found.LearningObjective = model.LearningObjective;                                
                found.LSTrainingFormID = model.LSTrainingFormID;
                found.LSTrainingLocationID = model.LSTrainingLocationID;
                found.Comment = model.Comment;
                found.CurrentComment = String.Empty;
                found.PlanHours = model.PlanHours;
                found.PlanDays = model.PlanDays;
                
                // Set information from ViewBag
                if (!String.IsNullOrEmpty(model.MonthYear))
                {
                    string monthInfo = model.MonthYear.Substring(0, 2);
                    if (Eagle.Common.Utilities.ValidatorUtils.CheckNumber(monthInfo))
                    {
                        found.PlanMonth = Convert.ToInt16(monthInfo);
                    }
                    string yearInfo = model.MonthYear.Substring(6, 4);
                    if (Eagle.Common.Utilities.ValidatorUtils.CheckNumber(yearInfo))
                    {
                        found.PlanYear = Convert.ToInt16(yearInfo);
                    }
                }
                if (mode == "Save")
                {
                    found.Status = (int)TrainingPlanStatus.Save;
                    found.LevelApprove = (int)LevelApproveStatus.Level0;
                    found.isFirstComment = true;
                }
                else if (mode == "SendForApproval")
                {
                    found.Status = (int)TrainingPlanStatus.WaitingForApproval;
                    found.LevelApprove = (int)LevelApproveStatus.Level1;
                    found.isFirstComment = false;                
                    // Add a comment
                    found.TR_tblPlanComment.Add(new TR_tblPlanComment 
                    { 
                        TraningPlanID = found.TrainingPlanID, 
                        Comment = found.Comment,
                        EmpID = this.CurrentAcc.EmpId, 
                        StatusName =  TrainingPlanStatus.WaitingForApproval.ToString(), 
                        Time = DateTime.Now
                    });
                    found.CurrentComment = String.Empty;
                }
                                
                // Set default information about system user, date for updating                
                if (account.EmpId.HasValue)
                {
                    found.Creater = account.EmpId.Value.ToString();
                    found.EmpIDLevel1 = account.EmpId.Value;
                }
                found.CreateTime = DateTime.Now;
                found.DateLevel1 = DateTime.Now;                               

                // Details
                found.TR_tblTrainingPlanExpense.Clear();                
                foreach (var item in this.TempData.ToList())
                {
                    var value = item.Value as TrainingPlanExpenseModelView;
                    if (value != null)
                    {
                        found.TR_tblTrainingPlanExpense.Add(new TR_tblTrainingPlanExpense()
                        {
                            TrainingPlanID = found.TrainingPlanID,
                            LSTrainingExpenseID = value.LSTrainingExpenseID,
                            LSCurrencyID = value.LSCurrencyID,
                            Cost = value.Cost
                        });

                    }
                }
                
                // Perform updating                
                bool result = repository.Update(found, out errorMessage);
                if (result)
                {
                    return this.Content("success");
                }
                else
                {
                    return this._Error(model, errorMessage);
                }
            }
            var errors = this.ModelState.Values.SelectMany(e => e.Errors);
            errorMessage = String.Empty;
            foreach (var errorModel in errors)
            {
                errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", errorModel.ErrorMessage);
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
        public ActionResult Approval(TrainingPlanViewModel model, string mode, int? ReturnLevelApprove)
        {
            string errorMessage;
            var repository = new TrainingPlanRepository(this.db);
            var found = repository.Find(model.TrainingPlanID, out errorMessage);
            if (found == null)
            {
                return this._Error(model, errorMessage);
            }
            found.Comment = model.Comment;
            if (found.LevelApprove == model.LevelApprove)
            {
                var objOnlineProcess = this.db.LS_tblOnlineProcess.Where(p => p.FunctionID == TRAININGPLAN_FUNCTIONID).FirstOrDefault();
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
                found.TR_tblPlanComment.Add(new TR_tblPlanComment
                {
                    TraningPlanID = found.TrainingPlanID,
                    Comment = found.Comment,
                    EmpID = this.CurrentAcc.EmpId,
                    StatusName = this.GetStatusName(found.Status),
                    Time = DateTime.Now
                });
                                              
  
                bool result = repository.Update(found, out errorMessage);
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
                    if (!resultOfSendMail)
                    {
                        return this.Content("successWithoutMail");
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
            var ProcessOnlineMaster = db.LS_tblOnlineProcess.Where(p => p.FunctionID == TRAININGPLAN_FUNCTIONID).Select(p => p.SYS_tblProcessOnlineMaster).FirstOrDefault();
            if (ProcessOnlineMaster != null)
            {
                var statusModel = ProcessOnlineMaster.FirstOrDefault();
                if (statusModel != null)
                {
                    switch (status)
                    {
                        case 0 :
                            statusName = TrainingPlanStatus.Save.ToString();
                            break;
                        case 1 :
                            statusName = TrainingPlanStatus.WaitingForApproval.ToString();
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
        /// <param name="trainingPlanID"></param>
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
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _Error(TrainingPlanViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new TrainingPlanViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            this.CreateViewBag(model, DateTime.Now.Month, DateTime.Now.Year);
            return PartialView("../Business/HumanResources/Training/TrainingPlan/_Create", model);
        }

        #region Sending Mail Training Plan

        /// <summary>
        /// List of mail template for sending mail
        /// </summary>
        public const int MAIL_TEMPLATE_ID_FOR_PLAN_APPROVER = 15;
        public const int MAIL_TEMPLATE_ID_FOR_PLAN_CREATER = 16;
        public const int MAIL_TEMPLATE_ID_FOR_PLAN_UNAPPROVER = 17;

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
                                      o.FunctionID == TRAININGPLAN_FUNCTIONID
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

                                             o.FunctionID == TRAININGPLAN_FUNCTIONID
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
                                       o.FunctionID == TRAININGPLAN_FUNCTIONID
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
                                       o.FunctionID == TRAININGPLAN_FUNCTIONID
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
                                       o.FunctionID == TRAININGPLAN_FUNCTIONID
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
        private bool SendMailForApproval(TR_tblPlan model, out string errorMessage)
        {
            try
            {
                // 1 Getting info about fullname, and emali address of Approver  
                string FullName = String.Empty;
                string ToMail = String.Empty;
                this.GetFullNameAndEmailAddressOfApprover(model.EmpIDCreate, model.LevelApprove, out FullName, out ToMail);   
                
                // 2 Sending mail to approver
                this.SendMailOfTrainingPlan(MAIL_TEMPLATE_ID_FOR_PLAN_APPROVER, ToMail, String.Empty, String.Empty, FullName, FullName, model.TrainingPlanID);
                
                // 3 Sending mail to creater 
                this.SendMailToPlanCreater(model, out errorMessage);
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
        private bool SendMailToPlanCreater(TR_tblPlan model, out string errorMessage)
        {
            try
            {                
                string DearFullName = String.Empty;
                string ToEmail = String.Empty;                
                string CCMail = String.Empty;
                string BCCMail = String.Empty;
                this.GetCcAndBcc(out CCMail, out BCCMail, TRAININGPLAN_FUNCTIONID);

                var employee = db.HR_tblEmp.Find(model.EmpIDCreate);
                if (employee != null)
                {
                    DearFullName = employee.LastName + " " + employee.FirstName;
                    ToEmail = employee.Email;                    
                }
                this.SendMailOfTrainingPlan(MAIL_TEMPLATE_ID_FOR_PLAN_CREATER, ToEmail, CCMail, BCCMail, DearFullName, CurrentAcc.FullName, model.TrainingPlanID);                
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
        private bool SendMailForUnApproval(TR_tblPlan model, out string errorMessage)
        {                        
            try
            {
                string ccMail = String.Empty;
                string bccMail = String.Empty;
                GetCcAndBcc(out ccMail, out bccMail, TRAININGPLAN_FUNCTIONID);
                var employee = db.HR_tblEmp.Find(model.EmpIDCreate);
                // 1. Sending mail for plan creater 
                this.SendMailOfTrainingPlan(MAIL_TEMPLATE_ID_FOR_PLAN_UNAPPROVER, employee.Email, ccMail, bccMail, employee.LastName + " " + employee.FirstName, CurrentAcc.FullName, model.TrainingPlanID);

                // 2. Sending mail for plan unapproval
                if (model.LevelApprove != 0)
                {
                    HR_tblEmp Employee = new HR_tblEmp();
                    int OnlineProcessID = db.LS_tblOnlineProcess.Where(p => p.FunctionID == TRAININGPLAN_FUNCTIONID).FirstOrDefault().SYS_tblProcessOnlineMaster.FirstOrDefault().OnlineProcessID;
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
                        this.SendMailOfTrainingPlan(MAIL_TEMPLATE_ID_FOR_PLAN_UNAPPROVER, Employee.Email, String.Empty, String.Empty, Employee.LastName + " " + Employee.FirstName, CurrentAcc.FullName, model.TrainingPlanID);
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
        private bool SendMailOfTrainingPlan(int MailTemplateID, string MailTo, string Cc, string Bcc, string DearFullName, string FromFullName, int TrainingPlanID)
        {
            try
            {
                string link = domainUrl + "Admin/TrainingPlan/Index?TrainingPlanID=" + TrainingPlanID;
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
    }
}