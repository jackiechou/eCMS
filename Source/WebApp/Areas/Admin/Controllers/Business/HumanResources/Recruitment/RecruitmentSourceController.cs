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
    public class RecruitmentSourceController : BaseController
    {
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Business/HumanResources/REC/RecruitmentSource/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../Business/HumanResources/REC/RecruitmentSource/Index");
            }
        }
        // load danh sach grid nhom
        public ActionResult _List()
        {
            RecruitmentSourceRepository _repository = new RecruitmentSourceRepository(db);
            List<LS_tblRecruitmentSource> list = _repository.GetAll();
            return PartialView("../Business/HumanResources/REC/RecruitmentSource/_List", list);
        }
        public ActionResult _Create()
        {
            LS_tblRecruitmentSource model = new LS_tblRecruitmentSource();
            model.Used = true;
            return PartialView("../Business/HumanResources/REC/RecruitmentSource/_Create", model);
        }

        public ActionResult _Edit(int id)
        {
            RecruitmentSourceRepository _repository = new RecruitmentSourceRepository(db);
            LS_tblRecruitmentSource modelEntity = _repository.Find(id);
            return PartialView("../Business/HumanResources/REC/RecruitmentSource/_Create", modelEntity);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            RecruitmentSourceRepository _repository = new RecruitmentSourceRepository(db);
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
        public ActionResult Update(LS_tblRecruitmentSource model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                if (ModelState.IsValid)
                {
                    RecruitmentSourceRepository _repository = new RecruitmentSourceRepository(db);
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
        public ActionResult Save(LS_tblRecruitmentSource model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
          
            if (acc != null)
            {
                RecruitmentSourceRepository _repository = new RecruitmentSourceRepository(db);
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

        public ActionResult _Error(LS_tblRecruitmentSource model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblRecruitmentSource();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            return PartialView("../Business/HumanResources/REC/RecruitmentSource/_Create", model);
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
            RecruitmentSourceRepository _repository = new RecruitmentSourceRepository(db);
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
