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
using Eagle.Common.Utilities;
using Eagle.Common.Helpers;
using Eagle.Common.Settings;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class LeaveDayTypeController : BaseController
    {
        //
        // GET: /Admin/LeaveDayType/

        public ActionResult Index()
        {

            if (Request.IsAjaxRequest())
            {
                return PartialView("../Timesheet/LeaveDayType/_Index");
            }
            else
            {
                return View("../Timesheet/LeaveDayType/Index");
            }
        }
        public ActionResult _Create()
        {
            LeaveDayTypeCreateViewModel model = new LeaveDayTypeCreateViewModel();

            return PartialView("../Timesheet/LeaveDayType/_Create", model);
        }
        public ActionResult _List()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }

            LeaveDayTypeRepository _repository = new LeaveDayTypeRepository(db);
            IList<LeaveDayTypeViewModel> sources = _repository.List().ToList();
            return PartialView("../Timesheet/LeaveDayType/_List", sources);
        }
        public ActionResult _Edit(int id)
        {
            LeaveDayTypeRepository _repository = new LeaveDayTypeRepository(db);
            LeaveDayTypeCreateViewModel model = _repository.FindEdit(id);
            return PartialView("../Timesheet/LeaveDayType/_Create", model);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            LeaveDayTypeRepository _repository = new LeaveDayTypeRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult Save(LeaveDayTypeCreateViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LeaveDayTypeRepository _repository = new LeaveDayTypeRepository(db);
                if (ModelState.IsValid)
                {
                    Timesheet_tbLeaveDayType add = new Timesheet_tbLeaveDayType()
                    {
                        LSLeaveDayTypeCode = model.LSLeaveDayTypeCode,
                        LSLeaveTypeID = model.LSLeaveTypeID,
                        Name = model.Name,
                        ExceptedHolidays = model.ExceptedHolidays,
                        DaysPerYear = model.DaysPerYear,
                        DaysPerPeriod = model.DaysPerPeriod,
                        ConvalescenceLeave = model.ConvalescenceLeave,
                        PercentSI = model.PercentSI,
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
        public ActionResult Update(LeaveDayTypeCreateViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LeaveDayTypeRepository _repository = new LeaveDayTypeRepository(db);
                if (ModelState.IsValid)
                {
                    string bResult = _repository.Update(model);
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
        /// <summary>
        /// Dung hien thi thong bao loi tren man hinh 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _CreateError(LeaveDayTypeCreateViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LeaveDayTypeCreateViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Timesheet/LeaveDayType/_Create", model);

        }
        #region  Dropdowmlist
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            LeaveDayTypeRepository _repository = new LeaveDayTypeRepository(db);
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

        [HttpGet]
        public ActionResult DropdownListSI(string searchTerm, int pageSize, int pageNum)
        {
            LeaveDayTypeRepository _repository = new LeaveDayTypeRepository(db);
            List<AutoCompleteViewModel> sources = _repository.ListDropdown2(searchTerm, pageSize, pageNum).ToList();
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

        
        #endregion
    }
}
