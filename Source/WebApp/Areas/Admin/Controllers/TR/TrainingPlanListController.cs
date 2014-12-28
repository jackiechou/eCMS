﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Eagle.Model;
using Eagle.Core;
using Eagle.Repository;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class TrainingPlanListController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../TR/TrainingPlanList/_Reset");
            }
            else
            {
                return View("../TR/TrainingPlanList/Index");
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _List(TrainingPlanSearchViewModel model)
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            this.ViewBag.EmpId = account.EmpId;  
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
            string errorMesage;
            var repository = new TrainingPlanRepository(this.db);
            var result = repository.GetTrainingPlanList(account, 3, model, out errorMesage);
            if (result == null)
            {
                return this.Content("Error");
            }                        
            return this.PartialView("../TR/TrainingPlanList/_List", result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _Create()
        {
            this.CreateViewBag();                        
            return PartialView("../TR/TrainingPlanList/_Create", new TrainingPlanSearchViewModel());
        }        

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag()
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            this.ViewBag.EmpId = account.EmpId;                        
            var NoLevelOfApproval = 3;
            var process = this.db.LS_tblOnlineProcess.Where(p => p.FunctionID == TrainingPlanController.TRAININGPLAN_FUNCTIONID).FirstOrDefault();
            if (process != null)
            {
                NoLevelOfApproval = process.NoOfLevel;
            }
            var listOfStatus = GetListOfTrainingRequestStatus(NoLevelOfApproval);
            var levelOfEmpLogin = this.GetLevelOfApproval(account.EmpId.Value, TrainingPlanController.TRAININGPLAN_FUNCTIONID);
            
            this.ViewBag.DefaultOfStatus = levelOfEmpLogin;
            this.ViewBag.Status = new SelectList(listOfStatus, "Value", "Text", levelOfEmpLogin);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _ListByDefaultLevelApproval(TrainingPlanSearchViewModel model)
        {
            this.ViewBag.EmpID = this.CurrentAcc.EmpId;
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            var DefaultLevelApproval = -1;
            if (account != null)
            {
                DefaultLevelApproval = this.GetLevelOfApproval(account.EmpId.Value, TrainingPlanController.TRAININGPLAN_FUNCTIONID);                
            }
            model.Status = DefaultLevelApproval;
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
            string errorMesage;
            var repository = new TrainingPlanRepository(this.db);
            var result = repository.GetTrainingPlanList(account, 3, model, out errorMesage);
            if (result == null)
            {
                return this.Content("Error");
            }            
            return this.PartialView("../TR/TrainingPlanList/_List", result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="FunctionID"></param>
        /// <returns></returns>
        private int GetLevelOfApproval(int EmpID, int FunctionID)
        {
            var result = -1;
            var found = (from online in this.db.SYS_tblOnlineProcessEmp
                         join master in this.db.SYS_tblProcessOnlineMaster on online.OnlineProcessID equals master.OnlineProcessID
                         join process in this.db.LS_tblOnlineProcess on master.DMQuiTrinhID equals process.DMQuiTrinhID
                         where online.EmpID == EmpID && process.FunctionID == FunctionID
                         select new { online.ApproveLevel1, online.ApproveLevel2, online.ApproveLevel3, online.ApproveLevel4, online.ApproveLevel5 }).FirstOrDefault();
            if (found != null)
            {
                if (found.ApproveLevel1 == true)
                {
                    result = 1;
                }
                else if (found.ApproveLevel2 == true)
                {
                    result = 2;
                }
                else if (found.ApproveLevel3 == true)
                {
                    result = 4;
                }
                else if (found.ApproveLevel4 == true)
                {
                    result = 6;
                }
                else if (found.ApproveLevel5 == true)
                {
                    result = 8;
                }
                else
                {
                    result = -1;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NoLevelOfApproval"></param>
        /// <returns></returns>
        private List<SelectListItem> GetListOfTrainingRequestStatus(int NoLevelOfApproval)
        {
            var found = new List<SelectListItem>();
            found.Add(new SelectListItem { Value = "-1", Text = Eagle.Resource.LanguageResource.SelectAll });
            found.Add(new SelectListItem { Value = "0", Text = TrainingRequestStatus.Save.ToString() });
            found.Add(new SelectListItem { Value = "1", Text = TrainingRequestStatus.WaitingForApproval.ToString() });
            switch (NoLevelOfApproval)
            {
                case 1: 
                    found.Add(new SelectListItem { Value = "2", Text = TrainingRequestStatus.Level1Approval.ToString() });
                    found.Add(new SelectListItem { Value = "3", Text = TrainingRequestStatus.Level1UnApproval.ToString() });
                    break;
                case 2: 
                    found.Add(new SelectListItem { Value = "2", Text = TrainingRequestStatus.Level1Approval.ToString() });
                    found.Add(new SelectListItem { Value = "3", Text = TrainingRequestStatus.Level1UnApproval.ToString() });
                    found.Add(new SelectListItem { Value = "4", Text = TrainingRequestStatus.Level2Approval.ToString() });
                    found.Add(new SelectListItem { Value = "5", Text = TrainingRequestStatus.Level2UnApproval.ToString() });            
                    break;
                case 3: 
                    found.Add(new SelectListItem { Value = "2", Text = TrainingRequestStatus.Level1Approval.ToString() });
                    found.Add(new SelectListItem { Value = "3", Text = TrainingRequestStatus.Level1UnApproval.ToString() });
                    found.Add(new SelectListItem { Value = "4", Text = TrainingRequestStatus.Level2Approval.ToString() });
                    found.Add(new SelectListItem { Value = "5", Text = TrainingRequestStatus.Level2UnApproval.ToString() });
                    found.Add(new SelectListItem { Value = "6", Text = TrainingRequestStatus.Level3Approval.ToString() });
                    found.Add(new SelectListItem { Value = "7", Text = TrainingRequestStatus.Level3UnApproval.ToString() });            
                    break;
                case 4: 
                    found.Add(new SelectListItem { Value = "2", Text = TrainingRequestStatus.Level1Approval.ToString() });
                    found.Add(new SelectListItem { Value = "3", Text = TrainingRequestStatus.Level1UnApproval.ToString() });
                    found.Add(new SelectListItem { Value = "4", Text = TrainingRequestStatus.Level2Approval.ToString() });
                    found.Add(new SelectListItem { Value = "5", Text = TrainingRequestStatus.Level2UnApproval.ToString() });
                    found.Add(new SelectListItem { Value = "6", Text = TrainingRequestStatus.Level3Approval.ToString() });
                    found.Add(new SelectListItem { Value = "7", Text = TrainingRequestStatus.Level3UnApproval.ToString() });
                    found.Add(new SelectListItem { Value = "8", Text = TrainingRequestStatus.Level4Approval.ToString() });
                    found.Add(new SelectListItem { Value = "9", Text = TrainingRequestStatus.Level4UnApproval.ToString() });            
                    break;
                case 5: 
                    found.Add(new SelectListItem { Value = "2", Text = TrainingRequestStatus.Level1Approval.ToString() });
                    found.Add(new SelectListItem { Value = "3", Text = TrainingRequestStatus.Level1UnApproval.ToString() });
                    found.Add(new SelectListItem { Value = "4", Text = TrainingRequestStatus.Level2Approval.ToString() });
                    found.Add(new SelectListItem { Value = "5", Text = TrainingRequestStatus.Level2UnApproval.ToString() });
                    found.Add(new SelectListItem { Value = "6", Text = TrainingRequestStatus.Level3Approval.ToString() });
                    found.Add(new SelectListItem { Value = "7", Text = TrainingRequestStatus.Level3UnApproval.ToString() });
                    found.Add(new SelectListItem { Value = "8", Text = TrainingRequestStatus.Level4Approval.ToString() });
                    found.Add(new SelectListItem { Value = "9", Text = TrainingRequestStatus.Level4UnApproval.ToString() });            
                    found.Add(new SelectListItem { Value = "10", Text = TrainingRequestStatus.Level5Approval.ToString() });
                    found.Add(new SelectListItem { Value = "11", Text = TrainingRequestStatus.Level5UnApproval.ToString() });
                    break;
                default: break;
            }            
            return found;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _Delete(int id)
        {            
            string errorMessage;
            var repository = new TrainingPlanRepository(this.db);
            var found = repository.Find(id, out errorMessage);
            if (found == null)
            {
                return this.Content("error");
            }
            if ((CurrentEmpId.HasValue && CurrentEmpId.Value != found.EmpIDCreate) || found.Status >= (int)TrainingPlanStatus.WaitingForApproval)
            {
                return this.Content("No permisson");
            }
            var result = repository.Delete(id, out errorMessage);
            if (result)
            {
                return this.Content("success");
            }            
            this.CreateViewBag();
            return this._Error(new TrainingPlanSearchViewModel(), errorMessage);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _Error(TrainingPlanSearchViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new TrainingPlanSearchViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;            
            this.CreateViewBag();
            return PartialView("../TR/TrainingPlanList/_Create", model);
        }          
    }
}
