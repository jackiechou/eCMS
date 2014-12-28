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
    /// <summary>
    /// 
    /// </summary>
    public class TrainingFormController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../TR/TrainingForm/_Reset");
            }
            else
            {
                return View("../TR/TrainingForm/Index");
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
            TrainingFormRepository repository = new TrainingFormRepository(this.db);
            string errorMesage;
            var result = repository.GetAll(out errorMesage);
            if (result == null)
            {
                return this.Content("Error");
            }
            return this.PartialView("../TR/TrainingForm/_List", result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _Create()
        {
            LS_tblTrainingForm model = new LS_tblTrainingForm();
            return PartialView("../TR/TrainingForm/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _Edit(int id)
        {
            string errorMessage;
            TrainingFormRepository _repository = new TrainingFormRepository(db);
            LS_tblTrainingForm model = _repository.Find(id, out errorMessage);
            if (model == null)
            {
                return this._Error(model, errorMessage);
            }
            return PartialView("../TR/TrainingForm/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(LS_tblTrainingForm model)
        {
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            string errorMessage;
            if (this.ModelState.IsValid)
            {
                TrainingFormRepository repository = new TrainingFormRepository(this.db);
                bool result = repository.Add(model, out errorMessage);
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
        public ActionResult Update(LS_tblTrainingForm model)
        {
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            string errorMessage;
            if (this.ModelState.IsValid)
            {
                TrainingFormRepository repository = new TrainingFormRepository(this.db);
                bool result = repository.Update(model, out errorMessage);
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
        public ActionResult _Delete(int id)
        {
            string errorMessage;
            TrainingFormRepository repository = new TrainingFormRepository(this.db);
            bool result = repository.Delete(id, out errorMessage);
            if (result)
            {
                return this.Content("success");
            }
            return this._Error(new LS_tblTrainingForm(), errorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _Error(LS_tblTrainingForm model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblTrainingForm();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            return PartialView("../TR/TrainingForm/_Create", model);
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
            TrainingFormRepository _repository = new TrainingFormRepository(db);
            List<AutoCompleteViewModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum).ToList();
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetTrainingCategory()
        {
            var list = db.LS_tblTrainingForm
                            .Select(p => new LS_tblTrainingForm()
                            {
                                LSTrainingFormID = p.LSTrainingFormID,
                                Name = p.Name,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            list.Insert(0, new LS_tblTrainingForm() { LSTrainingFormID = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            var retData = list.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.LSTrainingFormID.ToString()
            });
            return Json(retData, JsonRequestBehavior.AllowGet);
        }
    }
}
