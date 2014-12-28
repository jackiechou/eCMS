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
    public class IncomeTaxGrossController : BaseController
    {
        //
        // GET: /Admin/IncomeTaxGross/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/IncomeTaxGross/_Index");
            }
            return View("../Payroll/IncomeTaxGross/Index");
        }
        public ActionResult _Create()
        {
            IncometaxGrossViewModels model = new IncometaxGrossViewModels();
            return PartialView("../Payroll/IncomeTaxGross/_Create", model);
        }
        public ActionResult _List()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            IncomeTaxGrossRepository _repository = new IncomeTaxGrossRepository(db);
            IList<IncometaxGrossViewModels> sources = _repository.List().ToList();
            return PartialView("../Payroll/IncomeTaxGross/_List", sources);
        }
        public ActionResult _CreateError(IncometaxGrossViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new IncometaxGrossViewModels();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Payroll/IncomeTaxGross/_Create", model);

        }
        [HttpPost]
        public ActionResult Save(IncometaxGrossViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                IncomeTaxGrossRepository _repository = new IncomeTaxGrossRepository(db);
                if (ModelState.IsValid)
                {
                    PR_tblIncomeTaxGROSS add = new PR_tblIncomeTaxGROSS()
                    {
                        EffectiveDate = model.EffectiveDate,
                        IncomeTo1 = model.IncomeTo1,
                        IncomeTo2 = model.IncomeTo2,
                        IncomeTo3 = model.IncomeTo3,
                        IncomeTo4 = model.IncomeTo4,
                        IncomeTo5 = model.IncomeTo5,
                        IncomeTo6 = model.IncomeTo6,
                        IncomeTo7 = model.IncomeTo7,
                        Rate1 =model.Rate1,
                        Rate2 = model.Rate2,
                        Rate3 = model.Rate3,
                        Rate4 = model.Rate4,
                        Rate5 = model.Rate5,
                        Rate6 = model.Rate6,
                        Rate7 = model.Rate7
                        
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
        public ActionResult Update(IncometaxGrossViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                IncomeTaxGrossRepository _repository = new IncomeTaxGrossRepository(db);
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
            IncomeTaxGrossRepository _repository = new IncomeTaxGrossRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _Edit(int id)
        {
            IncomeTaxGrossRepository _repository = new IncomeTaxGrossRepository(db);
            IncometaxGrossViewModels model = _repository.FindEdit(id);
            return PartialView("../Payroll/IncomeTaxGross/_Create", model);
        }

    }
}
