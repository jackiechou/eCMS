using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;
using Eagle.Model.HR;

using System.Web.Routing;
using Eagle.Common.Utilities;
using Eagle.Common.Helpers;
using Eagle.Common.Settings;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class ScheduleChangeController : BaseController
    {
        //
        // GET: /Admin/ScheduleChange/

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Business/HumanResources/Timesheet/ScheduleChange/_Index");
            }
            else
            {
                return View("../Business/HumanResources/Timesheet/ScheduleChange/Index");
            }

        }
        public ActionResult _List()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            int ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"].ToString());
            ScheduleChangeRepository _repository = new ScheduleChangeRepository(db);
            IList<ScheduleChangeCreateViewModel> sources = _repository.List(acc.FAdm == 1, acc.UserName, ModuleID).ToList();
            return PartialView("../Business/HumanResources/Timesheet/ScheduleChange/_List", sources);
        }
        [HttpPost]
        public ActionResult Save(ScheduleChangeCreateViewModel model, List<int> lstBox)
        {

            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                ScheduleChangeRepository _repository = new ScheduleChangeRepository(db);
                if (ModelState.IsValid)
                {
                    DateTime? dFromDate = new DateTime();
                    DateTimeUtils.TryConvertToDate(model.strFromDate,out dFromDate);
                    var dConvertFrom = dFromDate.Value;
                    //
                    DateTime? dToDate = new DateTime();
                    DateTimeUtils.TryConvertToDate(model.strToDate, out dToDate);
                    var dConvertTo = dToDate.Value;
                    Timesheet_tblScheduleChange add = new Timesheet_tblScheduleChange()
                    {
                        EmpID = model.EmpID,
                        FromDate = dConvertFrom,
                        ToDate = dConvertTo,
                        ScheduleID_From = model.ScheduleID_From.Value,
                        ScheduleID_To = model.ScheduleID_To.Value

                    };
                    bool bResult = _repository.Add(add, lstBox, out errorMessage);
                    if (bResult)
                    {
                      return Content("success");
                    }
                    else
                    {
                        return Content("isExists");
                    }

                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += modelError.ErrorMessage + "-";
            }
            return _CreateError(model, errorMessage);
        }
        public ActionResult _Delete(int id)
        {
            ScheduleChangeRepository _repository = new ScheduleChangeRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _Create()
        {
            ScheduleChangeCreateViewModel model = new ScheduleChangeCreateViewModel();
            // page load dua cac listbox rong
            IList<EmployeeViewModel> sources = new List<EmployeeViewModel>();
            SelectList EmpList = new SelectList(sources, "EmpID", "FullName");
            ViewData["lstBox"] = EmpList;
            // page load 
            IList<EmployeeViewModel> sources1 = new List<EmployeeViewModel>();
            SelectList EmpListAssigned = new SelectList(sources1, "EmpID", "FullName");
            ViewData["lstBoxAssigned"] = EmpListAssigned;

            return PartialView("../Business/HumanResources/Timesheet/ScheduleChange/_Create", model);
        }
        public JsonResult _ListUserFrom(int ScheduleID, int iYear, string strSearch="")
        {
            try
            {
                ScheduleChangeRepository _repository = new ScheduleChangeRepository(db);
                var userList = _repository.ListStaffFrom(ScheduleID, iYear,strSearch).ToList();

                var retData = userList.Select(m => new SelectListItem()
                {
                    Text = m.FullName.ToString(),
                    Value = m.EmpID.ToString(),
                });
                return Json(retData, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult _CreateError(ScheduleChangeCreateViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new ScheduleChangeCreateViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            // page load dua cac listbox rong
            IList<EmployeeViewModel> sources = new List<EmployeeViewModel>();
            SelectList EmpList = new SelectList(sources, "EmpID", "FullName");
            ViewData["lstBox"] = EmpList;
            return PartialView("../Business/HumanResources/Timesheet/ScheduleChange/_Create", model);

        }
    }
}
