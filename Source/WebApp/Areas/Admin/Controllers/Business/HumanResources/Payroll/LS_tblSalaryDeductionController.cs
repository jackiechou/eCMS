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
    public class LS_tblSalaryDeductionController : BaseController
    {
        //
        // GET: /Admin/LS_tblSalaryDeduction/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/LS_tblSalaryDeduction/_Index");
            }
            return View("../Payroll/LS_tblSalaryDeduction/Index");
        }
        public ActionResult _Create()
        {
            SalaryDeductionViewModels model = new SalaryDeductionViewModels() { Used = true };

            return PartialView("../Payroll/LS_tblSalaryDeduction/_Create", model);
        }
        public ActionResult _List()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }

            LS_tblSalaryDeductionRepository _repository = new LS_tblSalaryDeductionRepository(db);
            List<SalaryDeductionViewModels> sources = _repository.List().ToList();
            return PartialView("../Payroll/LS_tblSalaryDeduction/_List", sources);
        }
        public ActionResult _CreateError(SalaryDeductionViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new SalaryDeductionViewModels();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Payroll/LS_tblSalaryDeduction/_Create", model);

        }
        public ActionResult Save(SalaryDeductionViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LS_tblSalaryDeductionRepository _repository = new LS_tblSalaryDeductionRepository(db);
                if (ModelState.IsValid)
                {
                    LS_tblSalaryDeduction add = new LS_tblSalaryDeduction()
                    {
                        LSDeductSalaryCode = model.LSDeductSalaryCode,
                        VNName = model.VNName,
                        Name = model.Name,
                        Rank = model.Rank,
                        BeforeTax = model.BeforeTax,
                        Used = model.Used,
                        Note = model.Note
                    };
                    // add du lieu vao database
                    string bResult = _repository.Add(add);
                    if (bResult == "success")
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _CreateError(model, bResult);
                    }

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
        public ActionResult Update(SalaryDeductionViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LS_tblSalaryDeductionRepository _repository = new LS_tblSalaryDeductionRepository(db);
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
            LS_tblSalaryDeductionRepository _repository = new LS_tblSalaryDeductionRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _Edit(int id)
        {
            LS_tblSalaryDeductionRepository _repository = new LS_tblSalaryDeductionRepository(db);
            SalaryDeductionViewModels model = _repository.FindEdit(id);
            return PartialView("../Payroll/LS_tblSalaryDeduction/_Create", model);
        }
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            LS_tblSalaryDeductionRepository _repository = new LS_tblSalaryDeductionRepository(db);
            List<AutoCompleteViewModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum, LanguageId).ToList();
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
