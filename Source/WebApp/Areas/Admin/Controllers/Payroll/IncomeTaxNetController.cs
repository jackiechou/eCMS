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
    public class IncomeTaxNetController : BaseController
    {
        //
        // GET: /Admin/IncomeTaxNet/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/IncomeTaxNet/_Index");
            }
            return View("../Payroll/IncomeTaxNet/Index");
        }
        public ActionResult _Create()
        {
            IncometaxNetViewModels model = new IncometaxNetViewModels() ;
            return PartialView("../Payroll/IncomeTaxNet/_Create", model);
        }
        public ActionResult _List()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            IncomeTaxNetRepository _repository = new IncomeTaxNetRepository(db);
            IList<IncometaxNetViewModels> sources = _repository.List().ToList();
            return PartialView("../Payroll/IncomeTaxNet/_List", sources);
        }
        public ActionResult _CreateError(IncometaxNetViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new IncometaxNetViewModels();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Payroll/IncomeTaxNet/_Create", model);

        }
        [HttpPost]
        public ActionResult Save(IncometaxNetViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                IncomeTaxNetRepository _repository = new IncomeTaxNetRepository(db);
                if (ModelState.IsValid)
                {
                    PR_tblIncometaxNET add = new PR_tblIncometaxNET()
                    {
                        EffectiveDate = model.EffectiveDate,
                        IncomeTo1 = model.IncomeTo1,
                        IncomeTo2 = model.IncomeTo2,
                        IncomeTo3 = model.IncomeTo3,
                        IncomeTo4 = model.IncomeTo4,
                        IncomeTo5 = model.IncomeTo5,
                        IncomeTo6 = model.IncomeTo6,
                        IncomeTo7 = model.IncomeTo7,
                        Subtract1 = model.Subtract1,
                        Subtract2 = model.Subtract2,
                        Subtract3 = model.Subtract3,
                        Subtract4 = model.Subtract4,
                        Subtract5 = model.Subtract5,
                        Subtract6 = model.Subtract6,
                        Subtract7 = model.Subtract7,
                        Divide1 = model.Divide1,
                        Divide2 = model.Divide2,
                        Divide3 = model.Divide3,
                        Divide4 = model.Divide4,
                        Divide5 = model.Divide5,
                        Divide6 = model.Divide6,
                        Divide7 = model.Divide7
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
        public ActionResult Update(IncometaxNetViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                IncomeTaxNetRepository _repository = new IncomeTaxNetRepository(db);
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
            IncomeTaxNetRepository _repository = new IncomeTaxNetRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _Edit(int id)
        {
            IncomeTaxNetRepository _repository = new IncomeTaxNetRepository(db);
            IncometaxNetViewModels model = _repository.FindEdit(id);
            return PartialView("../Payroll/IncomeTaxNet/_Create", model);
        }

    }
}
