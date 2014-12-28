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
    public class ScaleController : BaseController
    {
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/Scale/_Index");
            }
            return View("../Payroll/Scale/Index");
        }
        public ActionResult _Create()
        {
            ScaleCreateViewModels model = new ScaleCreateViewModels() { Used = true};

            return PartialView("../Payroll/Scale/_Create", model);
        }
        public ActionResult _List()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }

            ScaleRepository _repository = new ScaleRepository(db);
            IList<ScaleCreateViewModels> sources = _repository.List().ToList();
            return PartialView("../Payroll/Scale/_List", sources);
        }
        public ActionResult _CreateError(ScaleCreateViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new ScaleCreateViewModels();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Payroll/Scale/_Create", model);

        }
        public ActionResult Save(ScaleCreateViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                ScaleRepository _repository = new ScaleRepository(db);
                if (ModelState.IsValid)
                {
                    PR_tblSalaryScale add = new PR_tblSalaryScale()
                    {
                        SalaryScaleCode = model.SalaryScaleCode,
                        VNName = model.VNName,
                        Name = model.Name,
                        Rank = model.Rank,
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
        public ActionResult Update(ScaleCreateViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                ScaleRepository _repository = new ScaleRepository(db);
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
            ScaleRepository _repository = new ScaleRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _Edit(int id)
        {
            ScaleRepository _repository = new ScaleRepository(db);
            ScaleCreateViewModels model = _repository.FindEdit(id);
            return PartialView("../Payroll/Scale/_Create", model);
        }
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            ScaleRepository _repository = new ScaleRepository(db);
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
