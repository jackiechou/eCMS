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
    public class InsuranceExchangeRateController : BaseController
    {
        //
        // GET: /Admin/InsuranceExchangeRate/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/InsuranceExchangeRate/_Index");
            }
            return View("../Payroll/InsuranceExchangeRate/Index");
        }
        public ActionResult _Create()
        {
            InsuranceExchangeRateViewModels model = new InsuranceExchangeRateViewModels();
            return PartialView("../Payroll/InsuranceExchangeRate/_Create", model);
        }
        public ActionResult _List()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            InsuranceExchangeRateRepository _repository = new InsuranceExchangeRateRepository(db);
            IList<InsuranceExchangeRateViewModels> sources = _repository.List(LanguageId).ToList();
            return PartialView("../Payroll/InsuranceExchangeRate/_List", sources);
        }
        public ActionResult _CreateError(InsuranceExchangeRateViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new InsuranceExchangeRateViewModels();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            return PartialView("../Payroll/InsuranceExchangeRate/_Create", model);
        }
        [HttpPost]
        public ActionResult Save(InsuranceExchangeRateViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                InsuranceExchangeRateRepository _repository = new InsuranceExchangeRateRepository(db);
                if (ModelState.IsValid)
                {
                    PR_tblInsuranceExchangeRate add = new PR_tblInsuranceExchangeRate()
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
        public ActionResult Update(InsuranceExchangeRateViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                InsuranceExchangeRateRepository _repository = new InsuranceExchangeRateRepository(db);
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
            InsuranceExchangeRateRepository _repository = new InsuranceExchangeRateRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _Edit(int id)
        {
            InsuranceExchangeRateRepository _repository = new InsuranceExchangeRateRepository(db);
            InsuranceExchangeRateViewModels model = _repository.FindEdit(id, LanguageId);
            return PartialView("../Payroll/InsuranceExchangeRate/_Create", model);
        }

    }
}
