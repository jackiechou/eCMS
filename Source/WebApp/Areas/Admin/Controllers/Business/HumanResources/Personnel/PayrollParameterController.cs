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
    public class PayrollParameterController : BaseController
    {//
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Sys/PayrollParameter/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../Sys/PayrollParameter/Index");
            }
        }
        // load danh sach grid nhom
        public ActionResult _List()
        {
            PayrollParameterRepository _repository = new PayrollParameterRepository(db);
            List<SYS_tblPayrollParameter> sources = _repository.GetAll();
            return PartialView("../Sys/PayrollParameter/_List", sources);
        }
        public ActionResult _Create()
        {
            PayrollParameterViewModel acc = new PayrollParameterViewModel();
            return PartialView("../Sys/PayrollParameter/_Create", acc);
        }
        public ActionResult _Edit(int id)
        {
            PayrollParameterRepository _repository = new PayrollParameterRepository(db);
            PayrollParameterViewModel model = _repository.FindEdit(id);
            return PartialView("../Sys/PayrollParameter/_Create", model);
        }

        [HttpPost]
        public ActionResult _Delete(int id)
        {
            PayrollParameterRepository _repository = new PayrollParameterRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }

        [HttpPost]
        public ActionResult Update(PayrollParameterViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                PayrollParameterRepository _repository = new PayrollParameterRepository(db);
                if (ModelState.IsValid)
                {
                    if (model.Description != null && model.Description.Length > 200)
                    {
                        return Content(string.Format(Eagle.Resource.LanguageResource.MaxLengthInvalid,Eagle.Resource.LanguageResource.Description,"200"));
                    }

                    bool bResult = _repository.Update(model);
                    if (bResult)
                    {
                        return Content("success");
                    }
                    else
                    {
                        errorMessage = Eagle.Resource.LanguageResource.SystemError;
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
        [HttpPost]
        public ActionResult Save(PayrollParameterViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                PayrollParameterRepository _repository = new PayrollParameterRepository(db);
                if (ModelState.IsValid)
                {
                    DateTime EffectiveDate = (DateTime)model.EffectiveDate;
                    decimal DependDeduction = 0;
                    if (model.DependDeduction != null)
                    {
                        DependDeduction = (decimal)model.DependDeduction;
                    }
                    SYS_tblPayrollParameter add = new SYS_tblPayrollParameter()
                    {
                        EffectiveDate = EffectiveDate,
                        CoefInsS_E = model.CoefInsS_E,
                        CoefInsS_C = model.CoefInsS_C,
                        CoefInsH_E = model.CoefInsH_E,
                        CoefInsH_C = model.CoefInsH_C,
                        CoefInsE_E = model.CoefInsE_E,
                        CoefInsE_C = model.CoefInsE_C,
                        MinSalary = model.MinSalary,
                        CoefOver = model.CoefOver,
                        SelfDeduction = model.SelfDeduction,
                        DependDeduction = DependDeduction,
                        Description = model.Description

                    };
                    bool bResult = _repository.Add(add);
                    if (bResult)
                    {
                        return Content("success");
                    }
                    else
                    {
                        errorMessage = "Error";
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

        public ActionResult _Error(PayrollParameterViewModel acc, string ErrorMessage)
        {
            if (acc == null)
            {
                acc = new PayrollParameterViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            return PartialView("../Sys/PayrollParameter/_Create", acc);
        }


    }
}
