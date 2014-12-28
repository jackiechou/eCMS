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
    public class InterviewCriteriaTypeController : BaseController
    {
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Business/HumanResources/REC/InterviewCriteriaType/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../Business/HumanResources/REC/InterviewCriteriaType/Index");
            }
        }
        // load danh sach grid nhom
        public ActionResult _List()
        {
            InterviewCriteriaTypeRepository _repository = new InterviewCriteriaTypeRepository(db);
            List<REC_tblInterviewCriteriaType> list = _repository.GetAll();
            return PartialView("../Business/HumanResources/REC/InterviewCriteriaType/_List", list);
        }
        public ActionResult _Create()
        {
            REC_tblInterviewCriteriaType model = new REC_tblInterviewCriteriaType();
            model.Used = true;
            return PartialView("../Business/HumanResources/REC/InterviewCriteriaType/_Create", model);
        }

        public ActionResult _Edit(int id)
        {
            InterviewCriteriaTypeRepository _repository = new InterviewCriteriaTypeRepository(db);
            REC_tblInterviewCriteriaType modelEntity = _repository.Find(id);
            return PartialView("../Business/HumanResources/REC/InterviewCriteriaType/_Create", modelEntity);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            InterviewCriteriaTypeRepository _repository = new InterviewCriteriaTypeRepository(db);
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
        public ActionResult Update(REC_tblInterviewCriteriaType model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                if (ModelState.IsValid)
                {
                    InterviewCriteriaTypeRepository _repository = new InterviewCriteriaTypeRepository(db);
                    if (_repository.Update(model, out errorMessage))
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
        public ActionResult Save(REC_tblInterviewCriteriaType model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
          
            if (acc != null)
            {
                InterviewCriteriaTypeRepository _repository = new InterviewCriteriaTypeRepository(db);
                if (ModelState.IsValid)
                {
                    if (_repository.Add(model, out errorMessage))
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

        public ActionResult _Error(REC_tblInterviewCriteriaType model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new REC_tblInterviewCriteriaType();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            return PartialView("../Business/HumanResources/REC/InterviewCriteriaType/_Create", model);
        }

        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            InterviewCriteriaTypeRepository _repository = new InterviewCriteriaTypeRepository(db);
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
