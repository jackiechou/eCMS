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
    public class EvaluationTemplateController : BaseController
    {        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? EvaluationTemplateID)
        {
            if (EvaluationTemplateID.HasValue)
            {
                if (this.CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri + "?EvaluationTemplateID=" + EvaluationTemplateID.ToString());
                    return null;
                }

                return View("../Business/HumanResources/Training/EvaluationTemplate/Edit", EvaluationTemplateID);
            }
            else
            {
                if (this.Request.IsAjaxRequest())
                {
                    return this.PartialView("../Business/HumanResources/Training/EvaluationTemplate/_Reset");
                }
                else
                {
                    if (this.CurrentAcc == null)
                    {
                        return this.Redirect("/Admin/Login?desiredUrl=" + this.Request.Url.AbsoluteUri);
                    }
                    return this.View("../Business/HumanResources/Training/EvaluationTemplate/Index");
                }
            }                                                
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _Create(int? EvaluationTemplateID)
        {
            if (EvaluationTemplateID.HasValue)
            {
                var errorMessage = String.Empty;
                var repository = new EvaluationTemplateReposiory(this.db);
                var template = repository.FindForEdit(EvaluationTemplateID.Value, out errorMessage);
                if (template != null)
                {
                    this.CreateViewBag(template);
                    return this.PartialView("../Business/HumanResources/Training/EvaluationTemplate/_Create", template);
                }
            }
            var model = new EvaluationTemplateViewModel();
            this.CreateViewBag(model);
            return PartialView("../Business/HumanResources/Training/EvaluationTemplate/_Create", model);            
        }

        #region EvaluationTemplateDetail

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ListOfApprisalPart(int? EvaluationTemplateID)
        {
            string errorMesage;
            var repository = new EvaluationTemplateReposiory(this.db);
            if (EvaluationTemplateID.HasValue)
            {                
                var found = repository.FindForEdit(EvaluationTemplateID.Value, out errorMesage);
                if (found == null)
                {
                    return this.Content("Error");
                }
                this.CreateViewBag(found);
                return this.PartialView("../Business/HumanResources/Training/EvaluationTemplate/_ListOfApprisalPart", found.ListOfTemplateDetail);
            }
            var ListOfTemplateDetail = repository.GetListOfEvaluationTemplateDetail(out errorMesage);
            this.CreateViewBag(new EvaluationTemplateViewModel());
            return this.PartialView("../Business/HumanResources/Training/EvaluationTemplate/_ListOfApprisalPart", ListOfTemplateDetail);
        }        
        
        #endregion
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _Edit(int id)
        {            
            string errorMessage;
            var repository = new EvaluationTemplateReposiory(this.db);
            var found = repository.FindForEdit(id, out errorMessage);
            if (found == null)
            {
                return this._Error(found, errorMessage);
            }
            this.CreateViewBag(found);
            return PartialView("../Business/HumanResources/Training/EvaluationTemplate/_Create", found);
        }       
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(EvaluationTemplateViewModel model, List<EvaluationTemplateDetailOnGrid> listOfTemplateDetail, List<TrainingApprisalItemOnGrid> listOfApprisalItem)
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
                // Checking list of Evaluation template detail
                model.ListOfTemplateDetail = this.GetListOfEvaluationTemplateDetailFromGrid(listOfTemplateDetail, out errorMessage);
                if (model.ListOfTemplateDetail != null && model.ListOfTemplateDetail.Count == 0)
                {
                    return this._Error(model, Eagle.Resource.LanguageResource.PleaseInputTrainingApprisalPart);
                }
                // Checking list of template items
                var listOfTemplateItem = this.GetListOfTemplateItemFromGrid(listOfApprisalItem, out errorMessage);                
                if (listOfTemplateItem != null && listOfTemplateItem.Count == 0)
                {
                    return this._Error(model, Eagle.Resource.LanguageResource.PleaseInputTrainingApprisalItem);
                }
                // updating database
                var repository = new EvaluationTemplateReposiory(this.db);
                bool result = repository.Save(model, listOfTemplateItem, out errorMessage);
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
        private List<EvaluationTemplateDetailViewModel> GetListOfEvaluationTemplateDetailFromGrid(List<EvaluationTemplateDetailOnGrid> listOnGrid, out string errorMessage)
        {
            try
            {
                var found = (from detail in listOnGrid
                             where (detail.LSTrainingApprisalPartID != 0 && detail.Checked == true)
                             select new EvaluationTemplateDetailViewModel
                             {
                                 EvaluationTemplateID = detail.EvaluationTemplateID.Value,
                                 LSTrainingApprisalPartID = detail.LSTrainingApprisalPartID.Value
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
        private List<TemplateItemViewModel> GetListOfTemplateItemFromGrid(List<TrainingApprisalItemOnGrid> listOnGrid, out string errorMessage)
        {
            try
            {
                var found = (from item in listOnGrid
                             where item.Item_Checked == true
                             select new TemplateItemViewModel
                             {
                                 TemplateItemID = item.Item_TemplateItemID,
                                 EvaluationTemplateID = item.Item_EvaluationTemplateID.HasValue ? item.Item_EvaluationTemplateID.Value : 0,
                                 LSTrainingApprisalItemID = item.Item_LSTrainingApprisalItemID.HasValue ? item.Item_LSTrainingApprisalItemID.Value : 0,
                                 LSTrainingApprisalPartID = item.Item_LSTrainingApprisalPartID.HasValue ? item.Item_LSTrainingApprisalPartID.Value : 0
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
        public ActionResult Update(EvaluationTemplateViewModel model, List<EvaluationTemplateDetailOnGrid> listOfTemplateDetail, List<TrainingApprisalItemOnGrid> listOfApprisalItem)
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
                // Checking list of Evaluation template detail
                model.ListOfTemplateDetail = this.GetListOfEvaluationTemplateDetailFromGrid(listOfTemplateDetail, out errorMessage);
                if (model.ListOfTemplateDetail != null && model.ListOfTemplateDetail.Count == 0)
                {
                    return this._Error(model, Eagle.Resource.LanguageResource.PleaseInputTrainingApprisalPart);
                }
                // Checking list of template items
                var listOfTemplateItem = this.GetListOfTemplateItemFromGrid(listOfApprisalItem, out errorMessage);
                if (listOfTemplateItem != null && listOfTemplateItem.Count == 0)
                {
                    return this._Error(model, Eagle.Resource.LanguageResource.PleaseInputTrainingApprisalItem);
                }
                // Mapping data 
                var repository = new EvaluationTemplateReposiory(this.db);
                var objForUpdating = repository.FindForEdit(model.EvaluationTemplateID, out errorMessage);
                if (objForUpdating == null)
                {
                    return this._Error(model, Eagle.Resource.LanguageResource.TrainingEvaluationTemplateNotExistedForUdating);
                }
                objForUpdating.EvaluationTemplateCode = model.EvaluationTemplateCode;
                objForUpdating.EvaluationTemplateName = model.EvaluationTemplateName;
                objForUpdating.Note = model.Note;
                // updating database
                bool result = repository.Update(model, listOfTemplateItem, out errorMessage);
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
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _Error(EvaluationTemplateViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new EvaluationTemplateViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            this.CreateViewBag(model);
            return PartialView("../Business/HumanResources/Training/EvaluationTemplate/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag(EvaluationTemplateViewModel model)
        {
            this.ViewBag.EvaluationTemplateID = model.EvaluationTemplateID;
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            var repository = new TrainingEvaluationRepository(db);
            List<AutoCompleteViewModel> sources = repository.ListDropdown(searchTerm, pageSize, pageNum).ToList();
            int iTotal = sources.Count;

            //Translate the list into a format the select2 dropdown expects
            Select2PagedResultViewModel pagedList = ConvertListToSelect2Format(sources, iTotal);

            //Return the data as a jsonp result
            return new JsonpResult
            {
                Data = pagedList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}