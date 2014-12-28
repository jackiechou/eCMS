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
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class TrainingCourseController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../TR/TrainingCourse/_Reset");
            }
            else
            {
                return View("../TR/TrainingCourse/Index");
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
            TrainingCourseRepository repository = new TrainingCourseRepository(this.db);
            string errorMesage;
            var result = repository.GetAll(out errorMesage);
            if (result == null)
            {
                return this.Content("Error");
            }
            return this.PartialView("../TR/TrainingCourse/_List", result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _Create()
        {            
            return PartialView("../TR/TrainingCourse/_Create", new TrainingCourseViewModel());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _Edit(int id)
        {
            string errorMessage;
            TrainingCourseRepository _repository = new TrainingCourseRepository(db);
            TrainingCourseViewModel model = _repository.FindForEdit(id, out errorMessage);            
            if (model == null)
            {
                return this._Error(model, errorMessage);
            }
            return PartialView("../TR/TrainingCourse/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(TrainingCourseViewModel model)
        {
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            string errorMessage;
            if (this.ModelState.IsValid)
            {
                if (model.LSTrainingCodeIDAllowNull.HasValue)
                {
                    model.LSTrainingCodeID = model.LSTrainingCodeIDAllowNull.Value;
                }
                if (model.LSTrainingCategoryIDAllowNull.HasValue)
                {
                    model.LSTrainingCategoryID = model.LSTrainingCategoryIDAllowNull.Value;
                }
                
                Mapper.CreateMap<TrainingCourseViewModel, LS_tblTrainingCourse>();
                LS_tblTrainingCourse modelForAdd = Mapper.Map<LS_tblTrainingCourse>(model);
                TrainingCourseRepository repository = new TrainingCourseRepository(this.db);                 

                bool result = repository.Add(modelForAdd, out errorMessage);
                if (result)
                {
                    return this.Content("success");
                }
                else
                {
                    return this._Error(modelForAdd, errorMessage);
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
        public ActionResult Update(TrainingCourseViewModel model)
        {
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            string errorMessage;
            if (this.ModelState.IsValid)
            {
                if (model.LSTrainingCodeIDAllowNull.HasValue)
                {
                    model.LSTrainingCodeID = model.LSTrainingCodeIDAllowNull.Value;
                }
                if (model.LSTrainingCategoryIDAllowNull.HasValue)
                {
                    model.LSTrainingCategoryID = model.LSTrainingCategoryIDAllowNull.Value;
                }

                Mapper.CreateMap<TrainingCourseViewModel, LS_tblTrainingCourse>();
                LS_tblTrainingCourse modelForUpdate = Mapper.Map<LS_tblTrainingCourse>(model);

                TrainingCourseRepository repository = new TrainingCourseRepository(this.db);                                        
                bool result = repository.Update(modelForUpdate, out errorMessage);
                if (result)
                {
                    return this.Content("success");
                }
                else
                {
                    return this._Error(modelForUpdate, errorMessage);
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
            TrainingCourseRepository repository = new TrainingCourseRepository(this.db);
            bool result = repository.Delete(id, out errorMessage);
            if (result)
            {
                return this.Content("success");
            }
            return this._Error(new LS_tblTrainingCourse(), errorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _Error(LS_tblTrainingCourse model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblTrainingCourse();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            return PartialView("../TR/TrainingCourse/_Create", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int? LSTrainingCodeID, int pageSize, int pageNum)
        {
            TrainingCourseRepository _repository = new TrainingCourseRepository(db);
            List<AutoCompleteViewModel> sources = _repository.ListDropdown(searchTerm, LSTrainingCodeID, pageSize, pageNum).ToList();
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
            var list = db.LS_tblTrainingCategory
                            .Select(p => new LS_tblTrainingCourse()
                            {
                                LSTrainingCategoryID = p.LSTrainingCategoryID,
                                Name = p.Name,
                                Rank = p.Rank,
                                Used = p.Used.Value,
                                Note = p.Note
                            }).ToList();
            list.Insert(0, new LS_tblTrainingCourse() { LSTrainingCategoryID = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            var retData = list.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.LSTrainingCategoryID.ToString()
            });
            return Json(retData, JsonRequestBehavior.AllowGet);
        }
    }
}
