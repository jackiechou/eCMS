using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;
using Eagle.Common.Helpers;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class TrainingApprisalItemController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri + "?TrainingApprisalItemID=" + id.ToString());
                    return null;
                }
                return View("../Business/HumanResources/Training/TrainingPlan/Edit", id);
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return this.PartialView("../Business/HumanResources/Training/TrainingApprisalItem/_Reset");
                }
                else
                {
                    if (CurrentAcc == null)
                    {
                        Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                        return null;
                    }
                    return View("../Business/HumanResources/Training/TrainingApprisalItem/Index");
                }
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listOfTrainingAnswer"></param>
        /// <returns></returns>
        public ActionResult AddRowOfTrainingAnswer(List<TrainingAnswerTypeViewModel> listOfTrainingAnswer)
        {
            if (listOfTrainingAnswer == null)
            {
                listOfTrainingAnswer = new List<TrainingAnswerTypeViewModel>();
            }
            listOfTrainingAnswer.Add(new TrainingAnswerTypeViewModel());            
            return this.PartialView("../Business/HumanResources/Training/TrainingApprisalItem/_ListOfTrainingAnswer", listOfTrainingAnswer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listOfTrainingAnswer"></param>
        /// <returns></returns>
        public ActionResult DeleteRowOfTrainingAnswer(string LSTrainingAnswerTypeCode, List<TrainingAnswerTypeViewModel> listOfTrainingAnswer)
        {
            if (listOfTrainingAnswer == null)
            {
                listOfTrainingAnswer = new List<TrainingAnswerTypeViewModel>();
            }
            foreach (var item in listOfTrainingAnswer)
            {
                if (item.LSTrainingAnswerTypeCode == LSTrainingAnswerTypeCode)
                {
                    listOfTrainingAnswer.Remove(item);
                    break;
                }
            }
            
            return this.PartialView("../Business/HumanResources/Training/TrainingApprisalItem/_ListOfTrainingAnswer", listOfTrainingAnswer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _List()
        {
            AccountViewModel accout = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (accout == null)
            {
                return this.Content("Timeout");
            }
            var repository = new TrainingApprisalItemRepository(this.db);
            string errorMesage;
            var result = repository.GetOfTrainingApprisalItem(out errorMesage);
            if (result == null)
            {
                return this.Content("Error");
            }            
            return this.PartialView("../Business/HumanResources/Training/TrainingApprisalItem/_List", result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrainingApprisalItemID"></param>
        /// <returns></returns>
        public ActionResult ListOfTrainingAnswer(int? id)
        {
            if (id.HasValue)
            {
                string errorMessage;
                var repository = new TrainingAnswerTypeRepository(this.db);
                var listOfTrainingAnswer = repository.GetListOfTrainingAnswerTypeForTrainingItem(id.Value, out errorMessage);
                if (listOfTrainingAnswer != null)
                {                    
                    return this.PartialView("../Business/HumanResources/Training/TrainingApprisalItem/_ListOfTrainingAnswer", listOfTrainingAnswer);
                }
            }
            var listOfTrainingAnswerForAdding = new List<TrainingAnswerTypeViewModel>();
            listOfTrainingAnswerForAdding.Add(new TrainingAnswerTypeViewModel());            
            return this.PartialView("../Business/HumanResources/Training/TrainingApprisalItem/_ListOfTrainingAnswer", listOfTrainingAnswerForAdding);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _Create()
        {
            var model = new TrainingApprisalItemViewModel();
            model.Used = true;
            this.CreateViewBag(model);

            return PartialView("../Business/HumanResources/Training/TrainingApprisalItem/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _Edit(int id)
        {
            string errorMessage;
            var repository = new TrainingApprisalItemRepository(db);
            var model = repository.FindForEdit(id, out errorMessage);
            if (model == null)
            {
                return this._Error(model, errorMessage);
            }
            this.CreateViewBag(model);

            return PartialView("../Business/HumanResources/Training/TrainingApprisalItem/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(TrainingApprisalItemViewModel model, List<TrainingAnswerTypeViewModel> listOfTrainingAnswer)
        {
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            string errorMessage = String.Empty;            
            if (listOfTrainingAnswer.Count < 2)
            {
                errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", Eagle.Resource.LanguageResource.TrainingAnswerTypeErrorLessThanTwoItem);
            }
            foreach (var item in listOfTrainingAnswer)
            {
                var checking = this.db.LS_tblTrainingAnswerType.Where(p => p.LSTrainingAnswerTypeCode == item.LSTrainingAnswerTypeCode).FirstOrDefault();
                if (checking != null)
                {
                    errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", Eagle.Resource.LanguageResource.TrainingAnswerTypeExisted);
                    break;
                }
            }
            if (this.ModelState.IsValid && String.IsNullOrEmpty(errorMessage))
            {                
                var repository = new TrainingApprisalItemRepository(this.db);
                AutoMapper.Mapper.CreateMap<TrainingApprisalItemViewModel, LS_tblTrainingApprisalItem>();
                var modelForAdd = AutoMapper.Mapper.Map<LS_tblTrainingApprisalItem>(model);
                if (modelForAdd.LSTrainingApprisalPartID == 0)
                {
                    modelForAdd.LSTrainingApprisalPartID = null;
                }
                if (modelForAdd.LSAnswerTypeID == 0)
                {
                    modelForAdd.LSAnswerTypeID = null;
                }
                foreach (var item in listOfTrainingAnswer)
                {
                    AutoMapper.Mapper.CreateMap<TrainingAnswerTypeViewModel, LS_tblTrainingAnswerType>();
                    var objTrainingAnswer = AutoMapper.Mapper.Map<LS_tblTrainingAnswerType>(item);
                    modelForAdd.LS_tblTrainingAnswerType.Add(objTrainingAnswer);
                }
                bool result = repository.Save(modelForAdd, out errorMessage);
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
        public ActionResult Update(TrainingApprisalItemViewModel model, List<TrainingAnswerTypeViewModel> listOfTrainingAnswer)
        {
            if (model == null || listOfTrainingAnswer == null)
            {
                return this.Content("Error");
            }
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            string errorMessage = String.Empty;
            if (listOfTrainingAnswer.Count < 2)
            {
                errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", Eagle.Resource.LanguageResource.TrainingAnswerTypeErrorLessThanTwoItem);
            }
            foreach (var item in listOfTrainingAnswer)
            {
                var checking = this.db.LS_tblTrainingAnswerType.Where(p => p.LSTrainingAnswerTypeCode == item.LSTrainingAnswerTypeCode).FirstOrDefault();
                if (checking != null && item.LSTrainingAnswerTypeID == 0)
                {
                    errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", Eagle.Resource.LanguageResource.TrainingAnswerTypeExisted);
                    break;
                }
            }
            if (this.ModelState.IsValid && String.IsNullOrEmpty(errorMessage))                      
            {                
                AutoMapper.Mapper.CreateMap<TrainingApprisalItemViewModel, LS_tblTrainingApprisalItem>();
                var modelForUpdate = AutoMapper.Mapper.Map<LS_tblTrainingApprisalItem>(model);
                var repository = new TrainingApprisalItemRepository(this.db);
                if (modelForUpdate.LSTrainingApprisalPartID == 0)
                {
                    modelForUpdate.LSTrainingApprisalPartID = null;
                }
                if (modelForUpdate.LSAnswerTypeID == 0)
                {
                    modelForUpdate.LSAnswerTypeID = null;
                }
                foreach (var item in listOfTrainingAnswer)
                {
                    AutoMapper.Mapper.CreateMap<TrainingAnswerTypeViewModel, LS_tblTrainingAnswerType>();
                    var objTrainingAnswer = AutoMapper.Mapper.Map<LS_tblTrainingAnswerType>(item);
                    modelForUpdate.LS_tblTrainingAnswerType.Add(objTrainingAnswer);
                }
                bool result = repository.Update(modelForUpdate, out errorMessage);
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
            foreach (var errorModel in errors)
            {
                errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", errorModel.ErrorMessage);
            }
            return this._Error(model, errorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string errorMessage;
            var repository = new TrainingApprisalItemRepository(this.db);
            bool result = repository.Delete(id, out errorMessage);
            if (result)
            {
                return this.Content("success");
            }
            return this._Error(new TrainingApprisalItemViewModel(), errorMessage);
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void CreateViewBag(TrainingApprisalItemViewModel model)
        {
            var SelectListOfAnswerType = (new AnswerTypeController()).PopulateAnswerTypeToDropDownList(model.LSAnswerTypeID);
            if (SelectListOfAnswerType != null)
            {
                this.ViewBag.LSAnswerTypeID = SelectListOfAnswerType;
            }
            var SelectListOfTrainingApprisalPart = (new TrainingApprisalPartController()).PopulateAnswerTypeToDropDownList(model.LSTrainingApprisalPartID);
            if (SelectListOfTrainingApprisalPart != null)
            {
                this.ViewBag.LSTrainingApprisalPartID = SelectListOfTrainingApprisalPart;
            }
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _Error(TrainingApprisalItemViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new TrainingApprisalItemViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            this.CreateViewBag(model);

            return PartialView("../Business/HumanResources/Training/TrainingApprisalItem/_Create", model);
        }        
    }

}