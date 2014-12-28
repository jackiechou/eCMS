using AutoMapper;
using Eagle.Common.Helpers;
using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class InterviewCriteriaController : BaseController
    {
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../REC/InterviewCriteria/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../REC/InterviewCriteria/Index");
            }
        }
        // load danh sach grid nhom
        public ActionResult _List()
        {
            InterviewCriteriaRepository _repository = new InterviewCriteriaRepository(db);
            List<InterviewCriteriaViewModel> list = _repository.GetAll();
            return PartialView("../REC/InterviewCriteria/_List", list);
        }
        public ActionResult _Create()
        {
            InterviewCriteriaViewModel model = new InterviewCriteriaViewModel();
            model.Used = true;
            return PartialView("../REC/InterviewCriteria/_Create", model);
        }

        public ActionResult _Edit(int id)
        {
            InterviewCriteriaRepository _repository = new InterviewCriteriaRepository(db);
            InterviewCriteriaViewModel modelEntity = _repository.FindEdit(id);
            return PartialView("../REC/InterviewCriteria/_Create", modelEntity);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            InterviewCriteriaRepository _repository = new InterviewCriteriaRepository(db);
            string errorMessage = "";
            if (_repository.Delete(id, out errorMessage))
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }
        }

        [HttpPost]
        public ActionResult Update(InterviewCriteriaViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                if (ModelState.IsValid)
                {
                    InterviewCriteriaRepository _repository = new InterviewCriteriaRepository(db);

                    REC_tblInterviewCriteria modelUpdate = new REC_tblInterviewCriteria();
                    Mapper.CreateMap<InterviewCriteriaViewModel, REC_tblInterviewCriteria>();
                    Mapper.Map(model, modelUpdate);

                    if (_repository.Update(modelUpdate, out errorMessage))
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _Error(model, errorMessage);
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
                    }
                    return _Error(model, errorMessage);
                }
            }
            else
            {
                errorMessage = "Time out error!";
                return _Error(model, errorMessage);
            }

        }
        [HttpPost]
        public ActionResult Save(InterviewCriteriaViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
          
            if (acc != null)
            {
                InterviewCriteriaRepository _repository = new InterviewCriteriaRepository(db);
                if (ModelState.IsValid)
                {
                    REC_tblInterviewCriteria modelUpdate = new REC_tblInterviewCriteria();
                    Mapper.CreateMap<InterviewCriteriaViewModel, REC_tblInterviewCriteria>();
                    Mapper.Map(model, modelUpdate);

                    if (_repository.Add(modelUpdate, out errorMessage))
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _Error(model, errorMessage);
                    }
                }
            }
            else
            {
                errorMessage = "Time out error!";
                return _Error(model, errorMessage);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _Error(model, errorMessage);
        }

        public ActionResult _Error(InterviewCriteriaViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new InterviewCriteriaViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            return PartialView("../REC/InterviewCriteria/_Create", model);
        }

        /// <summary>
        /// Dùng cho viec binding du lieu cho dropdownlist autocomplete
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            InterviewCriteriaRepository _repository = new InterviewCriteriaRepository(db);
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

    }
}
