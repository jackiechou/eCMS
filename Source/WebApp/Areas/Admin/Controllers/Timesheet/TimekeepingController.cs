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
    public class TimekeepingController : BaseController
    {
        //
        // GET: /Admin/Timekeeping/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../Timesheet/Timekeeping/Index");
        }
        public ActionResult _List()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            List<TimekeepingListViewModel> sources = new List<TimekeepingListViewModel>();
            return PartialView("../Timesheet/Timekeeping/_List", sources);
        }
        public ActionResult _SearchHeaderEmp()
        {
            return PartialView("../Timesheet/Timekeeping/_HeaderSearchEmp");
        }
        public ActionResult _CreateError(TimekeepingSearchViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new TimekeepingSearchViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            return View("../Timesheet/Timekeeping/Index", model);

        }
        public ActionResult _Search(TimekeepingSearchViewModel model, int ModuleID)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string FromDate = "";
            string ToDate = "";
            if (acc != null)
            {
                if (ModelState.IsValid)
                {
                    if (model.strFromDate == "" || model.strFromDate == null) 
                        FromDate = "01/01/1900";
                    else
                        FromDate = model.strFromDate;

                    if (model.strToDate == "" || model.strFromDate == null)
                        ToDate = "31/12/3000";
                    else
                        ToDate = model.strToDate;

                    DateTime? dFromDate = new DateTime();
                    DateTimeUtils.TryConvertToDate(FromDate, out dFromDate);
                    var dConvertFrom = dFromDate.Value;
                    //
                    DateTime? dToDate = new DateTime();
                    DateTimeUtils.TryConvertToDate(ToDate, out dToDate);
                    var dConvertTo = dToDate.Value;
                    List<TimekeepingListViewModel> sources = new List<TimekeepingListViewModel>();
                    TimekeepingRepository _repository = new TimekeepingRepository(db);
                    sources = _repository._Search(model.MMYYYY, dConvertFrom, dConvertTo, model.EmpCode, model.FullName, model.SearchLSCompanyID, acc.FAdm == 1, acc.UserName, ModuleID, model.TimekeepingType).ToList();
                    return PartialView("../Timesheet/Timekeeping/_List", sources);
                }
                else{
                    return Content("error");
                }
            }
            return Content("success");
        }
        public ActionResult _GetData(TimekeepingSearchViewModel model, int ModuleID)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            if (model.TimekeepingType == 1)
            {
                //Chấm công tay
                var ret = db.Timesheet_spfrmTimekeepingSearch(acc.UserName, 9, model.EmpCode, model.FullName, model.SearchLSCompanyID, model.strFromDate, model.strToDate, model.MMYYYY);
            }
            else
            {
                //Chấm công máy
                var ret = db.Timesheet_spfrmTimekeepingMachine(acc.UserName, 9, model.EmpCode, model.FullName, model.SearchLSCompanyID, model.strFromDate, model.strToDate, model.MMYYYY);
            }

            

            return Content("success");
        }

        [HttpPost]
        public ActionResult _Delete(int EmpID, string MMYYYY,TimekeepingSearchViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc != null)
            {
                TimekeepingRepository _repository = new TimekeepingRepository(db);
                if (ModelState.IsValid)
                {
                    var ret = db.Timesheet_spfrmTimekeepingDelete(EmpID, MMYYYY);
                    return Content("success");
                }
            }
            return Content("error");
        }
        [HttpPost]
        public ActionResult _DeletePopup(int TimekeepingID, string MMYYYY)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc != null)
            {
                TimekeepingRepository _repository = new TimekeepingRepository(db);
                if (ModelState.IsValid)
                {
                    var ret = db.Timesheet_spfrmTimekeepingDeleteByDate(TimekeepingID, MMYYYY);
                    return Content("success");
                }
            }
            return Content("error");
        }
        public ActionResult _PopupEditTimeKeeping(int EmpID, string MMYYYY)
        {
            TimekeepingRepository _repository = new TimekeepingRepository(db);
            List<TimekeepingListViewModel> sources = _repository._ListPopup(MMYYYY, EmpID);
            ViewBag.PopupEmpID = EmpID;
            ViewBag.PopupMMYYYY = MMYYYY;
            return PartialView("../Timesheet/Timekeeping/_PopupEditTimeKeeping",sources);
        }

        public ActionResult _PopupEditTimeKeepingFormInner(int EmpID, string MMYYYY)
        {
            TimekeepingRepository _repository = new TimekeepingRepository(db);
            List<TimekeepingListViewModel> sources = _repository._ListPopup(MMYYYY, EmpID);
            return PartialView("../Timesheet/Timekeeping/_PopupEditTimeKeepingFormInner", sources);
        }  
        public ActionResult _UpdateData(int TimekeepingID, decimal WorkDays, TimekeepingSearchViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc != null)
            {
                TimekeepingRepository _repository = new TimekeepingRepository(db);
                if (ModelState.IsValid)
                {
                    var ret = db.Timesheet_spfrmTimekeepingUpdate(TimekeepingID, WorkDays, model.MMYYYY);
                    return Content("success");
                }
            }
            return Content("error");
        }
    }
}
