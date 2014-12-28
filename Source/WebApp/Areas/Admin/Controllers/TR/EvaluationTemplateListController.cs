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
    public class EvaluationTemplateListController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? EvaluationTemplateID)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../TR/EvaluationTemplateList/_Reset");
            }
            else
            {
                if (this.CurrentAcc == null)
                {
                    return this.Redirect("/Admin/Login?desiredUrl=" + this.Request.Url.AbsoluteUri);
                }
                return View("../TR/EvaluationTemplateList/Index");
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult _Create(EvaluationTemplateViewModel model)
        {
            this.CreateViewBag();
            return this.PartialView("../TR/EvaluationTemplateList/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _Edit(int EvaluationTemplateID)
        {
            string errorMessage;
            this.CreateViewBag();
            var model = (new EvaluationTemplateReposiory(this.db)).FindForEdit(EvaluationTemplateID, out errorMessage);
            if (model == null)
            {
                return this._Error(new EvaluationTemplateViewModel(), Eagle.Resource.LanguageResource.TrainingEvaluationTemplateNotExistedForUdating);
            }
            return this.PartialView("../TR/EvaluationTemplateList/_Create", model);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _List(EvaluationTemplateViewModel model)
        {
            var accout = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (accout == null)
            {
                return this.Content("Timeout");
            }            
            string errorMesage;
            var repository = new EvaluationTemplateReposiory(this.db);            
            var result = repository.GetListOfEvaluationTemplate(model, out errorMesage);
            if (result == null)
            {
                return this.Content("Error");
            }
            this.CreateViewBag();
            return this.PartialView("../TR/EvaluationTemplateList/_List", result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        private void CreateViewBag()
        {                                    
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
            var repository = new EvaluationTemplateReposiory(this.db);                      
            var result = repository.Delete(id, out errorMessage);
            if (result)
            {
                return this.Content("success");
            }
            this.CreateViewBag();
            return this._Error(new EvaluationTemplateViewModel(), errorMessage);                                      
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
            this.CreateViewBag();
            return PartialView("../TR/EvaluationTemplateList/_Create", model);
        }        
    }
}
