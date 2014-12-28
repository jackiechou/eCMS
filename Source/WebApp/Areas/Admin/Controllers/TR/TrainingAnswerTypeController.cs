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
    public class TrainingAnswerTypeController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../TR/TrainingAnswerType/_Reset");
            }
            else
            {
                return View("../TR/TrainingAnswerType/Index");
            }
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
            var repository = new TrainingAnswerTypeRepository(this.db);
            string errorMesage;
            var result = repository.GetListOfTrainingAnswerType(null, out errorMesage);
            if (result == null)
            {
                return this.Content("Error");
            }            

            return this.PartialView("../TR/TrainingAnswerType/_List", result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _Create()
        {
            var model = new TrainingAnswerTypeViewModel();
            model.Used = true;
            this.CreateViewBag(model);

            return PartialView("../TR/TrainingAnswerType/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _Edit(int id)
        {
            string errorMessage;
            var repository = new TrainingAnswerTypeRepository(db);
            var model = repository.FindForEdit(id, out errorMessage);
            if (model == null)
            {
                return this._Error(model, errorMessage);
            }
            this.CreateViewBag(model);

            return PartialView("../TR/TrainingAnswerType/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(TrainingAnswerTypeViewModel model)
        {
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            string errorMessage;
            if (this.ModelState.IsValid)
            {
                var repository = new TrainingAnswerTypeRepository(this.db);
                AutoMapper.Mapper.CreateMap<TrainingAnswerTypeViewModel, LS_tblTrainingAnswerType>();
                var modelForAdd = AutoMapper.Mapper.Map<LS_tblTrainingAnswerType>(model);                
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
        public ActionResult Update(TrainingAnswerTypeViewModel model)
        {
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            string errorMessage;
            if (this.ModelState.IsValid)
            {
                AutoMapper.Mapper.CreateMap<TrainingAnswerTypeViewModel, LS_tblTrainingAnswerType>();
                var modelForUpdate = AutoMapper.Mapper.Map<LS_tblTrainingAnswerType>(model);
                var repository = new TrainingAnswerTypeRepository(this.db);                
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string errorMessage;
            var repository = new TrainingAnswerTypeRepository(this.db);
            bool result = repository.Delete(id, out errorMessage);
            if (result)
            {
                return this.Content("success");
            }
            return this._Error(new TrainingAnswerTypeViewModel(), errorMessage);
        }       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void CreateViewBag(TrainingAnswerTypeViewModel model)
        {
            
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _Error(TrainingAnswerTypeViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new TrainingAnswerTypeViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            this.CreateViewBag(model);

            return PartialView("../TR/TrainingAnswerType/_Create", model);
        }        
    }

}