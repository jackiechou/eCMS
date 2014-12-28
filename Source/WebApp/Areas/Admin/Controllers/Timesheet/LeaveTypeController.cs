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
    public class LeaveTypeController : BaseController
    {
        //
        // GET: /Admin/LeaveType/

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Timesheet/LeaveType/_Index");
            }
            else
            {
                return View("../Timesheet/LeaveType/Index");
            }
            
        }
        public ActionResult _Create()
        {
            LeaveTypeCreateViewModel model = new LeaveTypeCreateViewModel();

            return PartialView("../Timesheet/LeaveType/_Create", model);
        }
        public ActionResult _List()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }

            LeaveTypeRepository _repository = new LeaveTypeRepository(db);
            IList<LeaveTypeViewModel> sources = _repository.List();
            return PartialView("../Timesheet/LeaveType/_List", sources);
        }
        public ActionResult Save(LeaveTypeCreateViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LeaveTypeRepository _repository = new LeaveTypeRepository(db);
                if (ModelState.IsValid)
                {
                    LS_tblLeaveType add = new LS_tblLeaveType()
                    {
                        LSLeaveTypeCode = model.LSLeaveTypeCode,
                        Name = model.Name,
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
                        //return RedirectToAction("_CreateError", "LeaveType", new { model = model, ErrorMessage = bResult });
                    }

                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _CreateError(model, errorMessage );
        }

        [HttpPost]
        public ActionResult Update(LeaveTypeCreateViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LeaveTypeRepository _repository = new LeaveTypeRepository(db);
                if (ModelState.IsValid)
                {
                    string  bResult = _repository.Update(model);
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
            //return RedirectToAction("_CreateError", "LeaveType", new { id = model, ErrorMessage = errorMessage });
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            LeaveTypeRepository _repository = new LeaveTypeRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _Edit(int id)
        {
            LeaveTypeRepository _repository = new LeaveTypeRepository(db);
            LeaveTypeCreateViewModel model = _repository.FindEdit(id);
            return PartialView("../Timesheet/LeaveType/_Create", model);
        }
        public ActionResult _CreateError(LeaveTypeCreateViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LeaveTypeCreateViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Timesheet/LeaveType/_Create", model);

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
            LeaveTypeRepository _repository = new LeaveTypeRepository(db);
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
