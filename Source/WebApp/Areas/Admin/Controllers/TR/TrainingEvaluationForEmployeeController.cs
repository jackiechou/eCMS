using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Eagle.Core;
using Eagle.Model;
using Eagle.Repository;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class TrainingEvaluationForEmployeeController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string Mode, int? TrainingEvaluationID, int? TrainingRequestID)
        {
            if (string.IsNullOrEmpty(Mode))
            {
                if (this.Request.IsAjaxRequest())
                {
                    return this.PartialView("../TR/TrainingEvaluationForEmployee/_Reset");
                }
                else
                {
                    if (this.CurrentAcc == null)
                    {
                        return this.Redirect("/Admin/Login?desiredUrl=" + this.Request.Url.AbsoluteUri);
                    }
                    return this.View("../TR/TrainingEvaluationForEmployee/Index");
                }
            }
            else
            {
                if (TrainingRequestID.HasValue)
                {
                    return this.View("../TR/TrainingEvaluationForEmployee/TrainingEvaluation/Index", TrainingRequestID);
                }
                else
                {
                    return this.View("../TR/TrainingEvaluationForEmployee/TrainingEvaluation/Edit", TrainingEvaluationID);
                }
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new TrainingRequestSearchViewModel();
            model.FromDate = DateTime.ParseExact(String.Format("01/{0}", DateTime.Now.ToString("MM/yyyy")), "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            model.ToDate = DateTime.ParseExact(String.Format("{0}/{1}", DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), DateTime.Now.ToString("MM/yyyy")), "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            model.EmpID = this.CurrentAcc.EmpId;
            model.EmplyeeName = this.CurrentAcc.FullName;
            this.CreateViewBag();
            return this.PartialView("../TR/TrainingEvaluationForEmployee/_Create", model);             
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult List(TrainingRequestSearchViewModel model)
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            ViewBag.EmpId = account.EmpId;
            if (!model.FromDate.HasValue || !model.ToDate.HasValue)
            {
                model.FromDate = DateTime.ParseExact(String.Format("01/{0}", DateTime.Now.ToString("MM/yyyy")), "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
                model.ToDate = DateTime.ParseExact(String.Format("{0}/{1}", DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), DateTime.Now.ToString("MM/yyyy")), "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            }
            var errorMessage = String.Empty;
            var repository = new TrainingEvaluationForEmployeeRepository(this.db);
            var listOfTrainingRequest = repository.GetListOfTrainingRequestForEvaluation(account, 3, model, out errorMessage);
            if (listOfTrainingRequest == null)
            {
                return this.Content("Error");
            }

            return this.PartialView("../TR/TrainingEvaluationForEmployee/_List", listOfTrainingRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ListByDefaultLevelApproval(TrainingRequestSearchViewModel model)
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            ViewBag.EmpId = account.EmpId;
            var DefaultLevelApproval = -1;
            if (account != null)
            {
                DefaultLevelApproval = this.GetLevelOfApproval(account.EmpId.Value, TrainingRequestController.TRAININGREQUEST_FUNCTIONID);
            }
            model.Status = DefaultLevelApproval;
            if (!model.FromDate.HasValue || !model.ToDate.HasValue)
            {
                model.FromDate = DateTime.ParseExact(String.Format("01/{0}", DateTime.Now.ToString("MM/yyyy")), "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
                model.ToDate = DateTime.ParseExact(String.Format("{0}/{1}", DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), DateTime.Now.ToString("MM/yyyy")), "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            }
            var errorMessage = String.Empty;
            var repository = new TrainingEvaluationForEmployeeRepository(this.db);
            var listOfTrainingRequest = repository.GetListOfTrainingRequestForEvaluation(account, 3, model, out errorMessage);
            if (listOfTrainingRequest == null)
            {
                return this.Content("Error");
            }

            return this.PartialView("../TR/TrainingEvaluationForEmployee/_List", listOfTrainingRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult Error(TrainingEvaluationForEmployeeViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new TrainingEvaluationForEmployeeViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            return PartialView("../TR/TrainingEvaluationForEmployee/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag()
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            this.ViewBag.EmpId = account.EmpId;
            var NoLevelOfApproval = 3;
            var process = this.db.LS_tblOnlineProcess.Where(p => p.FunctionID == TrainingRequestController.TRAININGREQUEST_FUNCTIONID).FirstOrDefault();
            if (process != null)
            {
                NoLevelOfApproval = process.NoOfLevel;
            }
            var listOfStatus = GetListOfTrainingRequestStatus(NoLevelOfApproval);
            var levelOfEmpLogin = this.GetLevelOfApproval(account.EmpId.Value, TrainingRequestController.TRAININGREQUEST_FUNCTIONID);

            this.ViewBag.DefaultOfStatus = levelOfEmpLogin;
            this.ViewBag.Status = new SelectList(listOfStatus, "Value", "Text", levelOfEmpLogin);
            this.ViewBag.StatusOfEvaluation = new SelectList(this.GetListOfEvaluationStatus(), "Value", "Text");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetListOfEvaluationStatus()
        {
            var found = new List<SelectListItem>();
            found.Add(new SelectListItem { Value = "0", Text = Eagle.Resource.LanguageResource.SelectAll, Selected = true });
            found.Add(new SelectListItem { Value = "1", Text = Eagle.Resource.LanguageResource.Evaluated, Selected = false });
            found.Add(new SelectListItem { Value = "2", Text = Eagle.Resource.LanguageResource.NotEvaluated, Selected = false });
            return found;
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
                    result = 4;
                }
                else if (found.ApproveLevel3 == true)
                {
                    result = 6;
                }
                else if (found.ApproveLevel4 == true)
                {
                    result = 8;
                }
                else if (found.ApproveLevel5 == true)
                {
                    result = 10;
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
    }
}