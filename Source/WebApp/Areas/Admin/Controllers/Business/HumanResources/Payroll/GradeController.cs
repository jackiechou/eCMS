using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;

using System.Web.Routing;
using Eagle.Common.Helpers;
using Eagle.Common.Settings;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class GradeController : BaseController
    {
        //
        // GET: /Admin/Grade/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/Grade/_Index");
            }
            return View("../Payroll/Grade/Index");
        }
        public ActionResult _Create()
        {
            GradeViewModels model = new GradeViewModels() { Used = true };
            return PartialView("../Payroll/Grade/_Create", model);
        }
        public ActionResult _List()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }

            GradeRepository _repository = new GradeRepository(db);
            IList<GradeViewModels> sources = _repository.List(LanguageId).ToList();
            return PartialView("../Payroll/Grade/_List", sources);
        }
        public ActionResult _CreateError(GradeViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new GradeViewModels();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Payroll/Grade/_Create", model);

        }
        [HttpPost]
        public ActionResult Save(GradeViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                GradeRepository _repository = new GradeRepository(db);
                if (ModelState.IsValid)
                {
                    PR_tblSalaryGrade add = new PR_tblSalaryGrade()
                    {
                        SalaryScaleID = model.SalaryScaleIDNull.Value,
                        SalaryGradeCode = model.SalaryGradeCode,
                        VNName = model.VNName,
                        Name = model.Name,
                        Rank = model.Rank,
                        Used = model.Used,
                        Note = model.Note
                    };
                    // add du lieu vao database
                    string bResult = _repository.Add(add);
                    if (bResult == "success")
                        return Content("success");
                    else
                        return _CreateError(model, bResult);
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _CreateError(model, errorMessage);
        }
        [HttpPost]
        public ActionResult Update(GradeViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                GradeRepository _repository = new GradeRepository(db);
                if (ModelState.IsValid)
                {
                    string bResult = _repository.Update(model);
                    if (bResult == "success")
                        return Content("success");
                    else
                        return _CreateError(model, bResult);

                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _CreateError(model, errorMessage);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            GradeRepository _repository = new GradeRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _Edit(int id)
        {
            GradeRepository _repository = new GradeRepository(db);
            GradeViewModels model = _repository.FindEdit(id, LanguageId);
            return PartialView("../Payroll/Grade/_Create", model);
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
            GradeRepository _repository = new GradeRepository(db);
            List<AutoCompleteViewModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum,LanguageId).ToList();
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
        [HttpGet]
        public ActionResult DropdownListByID(string searchTerm, int pageSize, int pageNum, int SalaryScaleID)
        {
            GradeRepository _repository = new GradeRepository(db);
            List<AutoCompleteViewModel> sources = _repository.ListDropdownByID(searchTerm, pageSize, pageNum, LanguageId, SalaryScaleID).ToList();
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
