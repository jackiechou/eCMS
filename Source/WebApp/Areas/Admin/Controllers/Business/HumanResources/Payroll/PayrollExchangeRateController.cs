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
    public class PayrollExchangeRateController : BaseController
    {
        //
        // GET: /Admin/PayrollExchangeRate/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/PayrollExchangeRate/_Index");
            }
            return View("../Payroll/PayrollExchangeRate/Index");
        }
        public ActionResult _Create()
        {
            PayrollExchangeRateViewModels model = new PayrollExchangeRateViewModels();
            return PartialView("../Payroll/PayrollExchangeRate/_Create", model);
        }
        public ActionResult _List()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            PayrollExchangeRateRepository _repository = new PayrollExchangeRateRepository(db);
            IList<PayrollExchangeRateViewModels> sources = _repository.List(LanguageId).ToList();
            return PartialView("../Payroll/PayrollExchangeRate/_List", sources);
        }
        public ActionResult _CreateError(PayrollExchangeRateViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new PayrollExchangeRateViewModels();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            return PartialView("../Payroll/PayrollExchangeRate/_Create", model);
        }
        [HttpPost]
        public ActionResult Save(PayrollExchangeRateViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                PayrollExchangeRateRepository _repository = new PayrollExchangeRateRepository(db);
                if (ModelState.IsValid)
                {
                    PR_tblPayrollExchangeRate add = new PR_tblPayrollExchangeRate()
                    {
                        EffectiveDate = model.EffectiveDate,
                        LSCurrencyID = model.LSCurrencyID,
                        ExchangeRate = model.ExchangeRate,
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
        public ActionResult Update(PayrollExchangeRateViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                PayrollExchangeRateRepository _repository = new PayrollExchangeRateRepository(db);
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
            PayrollExchangeRateRepository _repository = new PayrollExchangeRateRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _Edit(int id)
        {
            PayrollExchangeRateRepository _repository = new PayrollExchangeRateRepository(db);
            PayrollExchangeRateViewModels model = _repository.FindEdit(id, LanguageId);
            return PartialView("../Payroll/PayrollExchangeRate/_Create", model);
        }

    }
}
