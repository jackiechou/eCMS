using System;
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
    public class TrainingEvaluationListController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../TR/TrainingEvaluationList/_Reset");
            }
            else
            {
                if (this.CurrentAcc == null)
                {
                    return this.Redirect("/Admin/Login?desiredUrl=" + this.Request.Url.AbsoluteUri);
                }
                return View("../TR/TrainingEvaluationList/Index");
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult _Create(TrainingEvaluationViewModel model)
        {
            model.EvaluationTemplateID = 0;
            model.EvaluationTemplateName = Eagle.Resource.LanguageResource.PleaseInputEvaluationTemplate;
            return this.PartialView("../TR/TrainingEvaluationList/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _Edit(int TrainingEvaluationID)
        {
            string errorMessage;            
            var model = (new TrainingEvaluationRepository(this.db)).FindForEdit(TrainingEvaluationID, out errorMessage);
            if (model == null)
            {
                return this._Error(new TrainingEvaluationViewModel(), Eagle.Resource.LanguageResource.TrainingEvaluationTemplateNotExistedForUdating);
            }
            return this.PartialView("../TR/TrainingEvaluation/_Create", model);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _List(TrainingEvaluationViewModel model)
        {
            var accout = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (accout == null)
            {
                return this.Content("Timeout");
            }            
            string errorMesage;
            var repository = new TrainingEvaluationRepository(this.db);
            var result = repository.GetListOfTrainingEvaluation(model, this.CurrentAcc.EmpId.Value, out errorMesage);
            if (result == null)
            {
                return this.Content("Error");
            }            
            return this.PartialView("../TR/TrainingEvaluationList/_List", result);
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
            var repository = new TrainingEvaluationRepository(this.db);                      
            var result = repository.Delete(id, out errorMessage);
            if (result)
            {
                return this.Content("success");
            }
            return this._Error(new TrainingEvaluationViewModel(), errorMessage);                                      
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
            return PartialView("../TR/TrainingEvaluationList/_Create", model);
        }        
    }
}
