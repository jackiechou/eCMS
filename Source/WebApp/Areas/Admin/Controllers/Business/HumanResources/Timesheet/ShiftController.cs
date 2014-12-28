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
    public class ShiftController : BaseController
    {
        //
        // GET: /Admin/Shift/
        public ActionResult Index()
        {
            return View("../Business/HumanResources/Timesheet/Shift/Index");
        }

        public ActionResult _List()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }

            ShiftRepository _repository = new ShiftRepository(db);
            IList<ShiftViewModel> sources = _repository.List().ToList();
            return PartialView("../Business/HumanResources/Timesheet/Shift/_List", sources);
        }
        public ActionResult _Edit(int id)
        {
            ShiftRepository _repository = new ShiftRepository(db);
            ShiftCreateViewModel model = _repository.FindEdit(id);
            return PartialView("../Business/HumanResources/Timesheet/Shift/_Create", model);
        }
        public ActionResult _Create()
        {
            ShiftCreateViewModel model = new ShiftCreateViewModel();

            return PartialView("../Business/HumanResources/Timesheet/Shift/_Create", model);
        }
        public ActionResult Save(ShiftCreateViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                ShiftRepository _repository = new ShiftRepository(db);
                if (ModelState.IsValid)
                {
                    Timesheet_tblShift add = new Timesheet_tblShift()
                    {
                        LSShiftCode = model.LSShiftCode,
                        ShiftName =model.ShiftName,
                        // thoi gian chinh lam viec cua ca
                        WorkTimeBegin = System.DateTime.Today.Add(model.WorkTimeBegin),
                        WorkTimeEnd = System.DateTime.Today.Add(model.WorkTimeEnd),
                        // thoi gian nghi giua ca
                        BreakTimeBegin = System.DateTime.Today.Add(model.BreakTimeBegin),
                        BreakTimeEnd = System.DateTime.Today.Add(model.BreakTimeEnd),
                        // di tre / ve som
                        TimeLate = System.DateTime.Today.Add(model.TimeLate),
                        TimeEarly = System.DateTime.Today.Add(model.TimeEarly),
                        isNextDate_OTTimeEnd = model.isNextDate_OTTimeEnd,
                        // thoi gian OT dem
                        OTNightBegin = System.DateTime.Today.Add(model.OTNightBegin),
                        OTNightEnd = System.DateTime.Today.Add(model.OTNightEnd),
                        IsOTNightEnd = model.IsOTNightEnd,
                        WorkHour = model.WorkHour,
                        Note = ""
                    };
                    // add du lieu vao database
                    bool bResult = _repository.Add(add);
                    if (bResult)
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _CreateError(model, "error");
                        //return Content("error");
                    }

                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += modelError.ErrorMessage + "<br/>";
            }
            return _CreateError(model, errorMessage);
        }

        [HttpPost]
        public ActionResult Update(ShiftCreateViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                ShiftRepository _repository = new ShiftRepository(db);
                if (ModelState.IsValid)
                {
                    bool bResult = _repository.Update(model);
                    if (bResult)
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _CreateError(model, "error");
                    }

                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += modelError.ErrorMessage + "<br/>";
            }
            return _CreateError(model, errorMessage);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            ShiftRepository _repository = new ShiftRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _CreateError(ShiftCreateViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new ShiftCreateViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Business/HumanResources/Timesheet/Shift/_Create", model);
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
            ShiftRepository _repository = new ShiftRepository(db);
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
