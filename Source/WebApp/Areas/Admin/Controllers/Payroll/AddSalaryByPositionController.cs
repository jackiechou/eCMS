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
    public class AddSalaryByPositionController : BaseController
    {
        //
        // GET: /Admin/AddSalaryByPosition/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/AddSalaryByPosition/_Index");
            }
            return View("../Payroll/AddSalaryByPosition/Index");
        }
        public ActionResult _Create()
        {
            AddSalaryByPositionViewModels model = new AddSalaryByPositionViewModels();
            return PartialView("../Payroll/AddSalaryByPosition/_Create", model);
        }
        public ActionResult _List()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            AddSalaryByPositionRepository _repository = new AddSalaryByPositionRepository(db);
            List<AddSalaryByPositionViewModels> sources = _repository.List(LanguageId).ToList();
            return PartialView("../Payroll/AddSalaryByPosition/_List", sources);
        }
        public ActionResult _CreateError(AddSalaryByPositionViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new AddSalaryByPositionViewModels();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Payroll/AddSalaryByPosition/_Create", model);

        }
        [HttpPost]
        public ActionResult Save(AddSalaryByPositionViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                AddSalaryByPositionRepository _repository = new AddSalaryByPositionRepository(db);
                if (ModelState.IsValid)
                {
                    PR_tblAddSalaryByPosition add = new PR_tblAddSalaryByPosition()
                    {
                        LSPositionID = model.LSPositionIDNull.Value,
                        LSSalaryAdjustID = model.LSSalaryAdjustIDNull.Value,
                        PaymentMethod = model.PaymentMethod,
                        Amount = model.Amount,
                        FromMonth = model.FromMonth,
                        ToMonth = model.ToMonth,
                        isGross = model.GROSSNET.Value,
                        LSCurrencyID = model.LSCurrencyID,
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
        public ActionResult _Edit(int id)
        {
            AddSalaryByPositionRepository _repository = new AddSalaryByPositionRepository(db);
            AddSalaryByPositionViewModels model = _repository.FindEdit(id, LanguageId);
            return PartialView("../Payroll/AddSalaryByPosition/_Create", model);
        }
        [HttpPost]
        public ActionResult Update(AddSalaryByPositionViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                AddSalaryByPositionRepository _repository = new AddSalaryByPositionRepository(db);
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
            AddSalaryByPositionRepository _repository = new AddSalaryByPositionRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
    }
}
