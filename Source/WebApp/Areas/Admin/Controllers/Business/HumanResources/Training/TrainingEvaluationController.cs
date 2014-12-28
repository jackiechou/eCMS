using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;
using AutoMapper;
using Eagle.Common.Helpers;
using System.Collections;
using Eagle.Repository.Mail;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers 
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingEvaluationController : BaseController
    {        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? TrainingEvaluationID)
        {
            if (TrainingEvaluationID.HasValue)
            {
                if (this.CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri + "?EvaluationTemplateID=" + TrainingEvaluationID.Value.ToString());
                    return null;
                }

                return View("../Business/HumanResources/Training/TrainingEvaluation/Edit", TrainingEvaluationID);
            }
            else
            {
                if (this.Request.IsAjaxRequest())
                {
                    return this.PartialView("../Business/HumanResources/Training/TrainingEvaluation/_Reset");
                }
                else
                {
                    if (this.CurrentAcc == null)
                    {
                        return this.Redirect("/Admin/Login?desiredUrl=" + this.Request.Url.AbsoluteUri);
                    }
                    return this.View("../Business/HumanResources/Training/TrainingEvaluation/Index");
                }
            }                                                
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _Create(int? TrainingRequestID)
        {            
            var model = new TrainingEvaluationViewModel();
            if (TrainingRequestID.HasValue)
            {
                model.TrainingRequestID = TrainingRequestID.Value;
                var errorMessage = String.Empty;
                var repository = new TrainingRequestRepository(this.db);
                var request = repository.FindForEdit(TrainingRequestID.Value, out errorMessage);
                if (request != null)
                {
                    model.EvaluationTemplateID = request.EvaluationTemplateID.HasValue ? request.EvaluationTemplateID.Value : 0;
                    model.EvaluationEmpName = request.EmployeeEvaluation;
                    model.TrainingCode = request.TrainingCode;
                    model.TrainingCourseName = request.TrainingCourseName;
                    model.EvaluationTemplateName = request.EvaluationTemplateName;
                }
            }
            else
            {
                model.EvaluationTemplateID = 0;
                model.EvaluationTemplateName = Eagle.Resource.LanguageResource.PleaseInputEvaluationTemplate;
            }            
            this.CreateViewBag(model);
            return PartialView("../Business/HumanResources/Training/TrainingEvaluation/_Create", model);            
        }

        #region TrainingEvaluationPart

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ListOfTrainingEvaluationPart(int? TrainingEvaluationID, int? TrainingRequestID)
        {
            string errorMesage;
            var repository = new TrainingEvaluationRepository(this.db);
            if (TrainingEvaluationID > 0)
            {
                var found = repository.FindForEdit(TrainingEvaluationID.Value, out errorMesage);
                if (found == null)
                {
                    return this.Content("Error");
                }
                return this.PartialView("../Business/HumanResources/Training/TrainingEvaluation/_ListOfApprisalPart", found.ListOfTrainingEvaluationPart);
            }
            if (TrainingRequestID.HasValue)
            {
                var request = this.db.TR_tblTrainingRequest.Where(p => p.TrainingRequestID == TrainingRequestID).FirstOrDefault();
                if (request != null && request.EvaluationTemplateID.HasValue)
                {
                    repository = new TrainingEvaluationRepository(this.db);
                    var errorMessage = String.Empty;
                    var found = repository.GetTrainingEvaluationFromTemplate(request.EvaluationTemplateID.Value, out errorMessage);
                    if (found == null)
                    {
                        return this._Error(new TrainingEvaluationViewModel(), errorMessage);
                    }
                    return this.PartialView("../Business/HumanResources/Training/TrainingEvaluation/_ListOfApprisalPart", found.ListOfTrainingEvaluationPart);
                }
            }
            var model = new TrainingEvaluationViewModel();
            return this.PartialView("../Business/HumanResources/Training/TrainingEvaluation/_ListOfApprisalPart", model.ListOfTrainingEvaluationPart);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EvaluationTemplateID"></param>
        /// <returns></returns>
        public ActionResult ListOfTrainingEvaluationPartFromTemplate(int EvaluationTemplateID)
        {
            string errorMessage;
            var repository = new TrainingEvaluationRepository(this.db);
            var found = repository.GetTrainingEvaluationFromTemplate(EvaluationTemplateID, out errorMessage);
            if (found == null)
            {
                return this._Error(new TrainingEvaluationViewModel(), errorMessage);
            }
            return this.PartialView("../Business/HumanResources/Training/TrainingEvaluation/_ListOfApprisalPart", found.ListOfTrainingEvaluationPart);
        }
        
        #endregion
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _Edit(int? TrainingEvaluationID)
        {            
            string errorMessage;
            var repository = new TrainingEvaluationRepository(this.db);
            if (TrainingEvaluationID.HasValue)
            {
                var found = repository.FindForEdit(TrainingEvaluationID.Value, out errorMessage);
                if (found == null)
                {
                    return this._Error(found, errorMessage);
                }
                this.CreateViewBag(found);
                return PartialView("../Business/HumanResources/Training/TrainingEvaluation/_Create", found);
            }
            var model = new TrainingEvaluationViewModel();
            this.CreateViewBag(model);
            return PartialView("../Business/HumanResources/Training/TrainingEvaluation/_Create", model);
        }       
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(string mode, TrainingEvaluationViewModel model, List<TrainingEvaluationPartOnGrid> listOfTemplateDetail, 
            List<TrainingEvaluationDetailOnGrid> listOfApprisalItem, List<TrainingAnswerTypeOnGrid> listOfTrainingAnswer)
        {
            // Checking timeout
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }
            // Checking info of Evaluation Template
            string errorMessage;
            if (this.ModelState.IsValid)
            {
                if (this.CurrentAcc.EmpId.HasValue)
                {
                    model.Creater = this.CurrentAcc.EmpId.Value;
                    model.EvaluationEmpID = this.CurrentAcc.EmpId.Value;                    
                }
                model.CreateDate = DateTime.Now;
                model.EvaluationTemplateID = model.EvaluationTemplateID;
                if (mode == "Save")
                {
                    model.IsComplete = null;
                }
                else if (mode == "Complete")
                {
                    model.IsComplete = true;
                }
                // Checking list of Evaluation template detail
                model.ListOfTrainingEvaluationPart = this.GetListOfTrainingEvaluationPartFromGrid(listOfTemplateDetail, out errorMessage);
                if (model.ListOfTrainingEvaluationPart != null && model.ListOfTrainingEvaluationPart.Count == 0)
                {
                    return this._Error(model, Eagle.Resource.LanguageResource.PleaseInputTrainingApprisalPart);
                }
                // Checking list of template items
                var listOfTrainingEvaluationDetail = this.GetListOfTrainingEvaluationDetailFromGrid(listOfApprisalItem, out errorMessage);
                if (listOfTrainingEvaluationDetail != null && listOfTrainingEvaluationDetail.Count == 0)
                {
                    return this._Error(model, Eagle.Resource.LanguageResource.PleaseInputTrainingApprisalItem);
                }
                // updating database
                var repository = new TrainingEvaluationRepository(this.db);
                bool result = repository.Save(model, listOfTrainingEvaluationDetail, out errorMessage);
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
        /// <param name="listOnGrid"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private List<TrainingEvaluationPartViewModel> GetListOfTrainingEvaluationPartFromGrid(List<TrainingEvaluationPartOnGrid> listOnGrid, out string errorMessage)
        {
            try
            {
                var found = (from detail in listOnGrid            
                             where detail.Part_LSTrainingApprisalPartID != null
                             select new TrainingEvaluationPartViewModel
                             {
                                 TrainingEvaluationID = detail.Part_TrainingEvaluationID.HasValue ? detail.Part_TrainingEvaluationID.Value : 0, 
                                 LSTrainingApprisalPartID = detail.Part_LSTrainingApprisalPartID.HasValue ?  detail.Part_LSTrainingApprisalPartID.Value : 0
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
        /// <param name="listOnGrid"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private List<TrainingEvaluationDetailViewModel> GetListOfTrainingEvaluationDetailFromGrid(List<TrainingEvaluationDetailOnGrid> listOnGrid, out string errorMessage)
        {
            try
            {
                var found = (from item in listOnGrid
                             where item.Checked == true
                             select new TrainingEvaluationDetailViewModel
                             {
                                 TrainingEvaluationDetailID = item.TrainingEvaluationDetailID,
                                 TrainingEvaluationID = item.TrainingEvaluationID,
                                 LSTrainingApprisalItemID = item.LSTrainingApprisalItemID.Value,
                                 LSTrainingApprisalPartID = item.LSTrainingApprisalPartID.Value,
                                 LSTrainingAnswerTypeID = item.LSTrainingAnswerTypeID,
                                 IsAnswer = true,
                                 Checked = true
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
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(string mode, TrainingEvaluationViewModel model, List<TrainingEvaluationPartOnGrid> listOfTemplateDetail,
            List<TrainingEvaluationDetailOnGrid> listOfApprisalItem, List<TrainingAnswerTypeOnGrid> listOfTrainingAnswer)
        {
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }
            string errorMessage;
            // Checking info of Evaluation Template
            if (this.ModelState.IsValid)
            {
                if (this.CurrentAcc.EmpId.HasValue)
                {
                    model.Updater = this.CurrentAcc.EmpId.Value;
                    model.EvaluationEmpID = this.CurrentAcc.EmpId.Value;
                }
                model.UpdateDate = DateTime.Now;
                if (mode == "Save")
                {
                    model.IsComplete = null;
                }
                else if (mode == "Complete")
                {
                    model.IsComplete = true;
                }
                // Checking list of Evaluation template detail
                model.ListOfTrainingEvaluationPart = this.GetListOfTrainingEvaluationPartFromGrid(listOfTemplateDetail, out errorMessage);
                if (model.ListOfTrainingEvaluationPart != null && model.ListOfTrainingEvaluationPart.Count == 0)
                {
                    return this._Error(model, Eagle.Resource.LanguageResource.PleaseInputTrainingApprisalPart);
                }
                // Checking list of template items
                var listOfTrainingEvaluationDetail = this.GetListOfTrainingEvaluationDetailFromGrid(listOfApprisalItem, out errorMessage);
                if (listOfTrainingEvaluationDetail != null && listOfTrainingEvaluationDetail.Count == 0)
                {
                    return this._Error(model, Eagle.Resource.LanguageResource.PleaseInputTrainingApprisalItem);
                }                                             
                // updating database
                var repository = new TrainingEvaluationRepository(this.db);
                bool result = repository.Update(model, listOfTrainingEvaluationDetail, out errorMessage);
                if (result)
                {
                    return this.Content("success");
                }
                else
                {
                    return null;//this._Error(model, errorMessage);
                }
            }
            var errors = this.ModelState.Values.SelectMany(e => e.Errors);
            errorMessage = String.Empty;
            foreach (var errorModel in errors)
            {
                errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", errorModel.ErrorMessage);
            }
            return null;// this._Error(model, errorMessage);
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _Error(TrainingEvaluationViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new TrainingEvaluationViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            this.CreateViewBag(model);

            var errorMessage = String.Empty;
            var repository = new TrainingRequestRepository(this.db);
            var request = repository.FindForEdit(model.TrainingRequestID.Value, out errorMessage);
            if (request != null)
            {
                model.EvaluationTemplateID = request.EvaluationTemplateID.HasValue ? request.EvaluationTemplateID.Value : 0;
                model.EvaluationEmpName = request.EmployeeEvaluation;
                model.TrainingCode = request.TrainingCode;
                model.TrainingCourseName = request.TrainingCourseName;
                model.EvaluationTemplateName = request.EvaluationTemplateName;
            }
            return PartialView("../Business/HumanResources/Training/TrainingEvaluation/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag(TrainingEvaluationViewModel model)
        {
            this.ViewBag.TrainingEvaluationID = model.TrainingEvaluationID;
            this.ViewBag.NotAllowUpdate = (model.TrainingEvaluationID > 0 && model.IsComplete == true) ? "style=display:none;" : String.Empty;
            this.ViewBag.NotDisplayTemplate = model.TrainingRequestID > 0 ? "style=display:none;" : String.Empty;
            this.ViewBag.TrainingRequestID = model.TrainingRequestID;            
            return;
        }        
    }
}