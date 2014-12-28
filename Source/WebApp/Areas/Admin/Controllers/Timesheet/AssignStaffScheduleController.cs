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
    public class AssignStaffScheduleController : BaseController
    {
        //
        // GET: /Admin/AssignStaffSchedule/

        public ActionResult Index()
        {
            return View("../Timesheet/AssignStaffSchedule/Index");
        }
        public ActionResult _Create()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            int ModuleID = 9;
            AssignEmpScheduleRepository _repository = new AssignEmpScheduleRepository(db);
            IList<EmployeeViewModel> sources = _repository.ListBox(acc.FAdm == 1, acc.UserName, ModuleID).ToList();
            SelectList EmpList = new SelectList(sources, "EmpID", "FullName");
            ViewData["lstBox"] = EmpList;

            // form load 
            IList<EmployeeViewModel> sources1 = new List<EmployeeViewModel>();
            SelectList EmpListAssigned = new SelectList(sources1, "EmpID", "FullName"); 
            ViewData["lstBoxAssigned"] = EmpListAssigned;

            return PartialView("../Timesheet/AssignStaffSchedule/_Create");
        }
        public ActionResult _Search(string SearchTerm, int iYear)
        {
            try
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                if (acc == null)
                {
                    Response.Redirect("/");
                    return null;
                }
                int ModuleID = 9;
                AssignEmpScheduleRepository _repository = new AssignEmpScheduleRepository(db);
                var userList = _repository.ListBox(SearchTerm, iYear, acc.FAdm == 1, acc.UserName, ModuleID).ToList();

                var retData = userList.Select(m => new SelectListItem()
                {
                    Text =  m.FullName.ToString(),
                    Value = m.EmpID.ToString(),
                });
                return Json(retData, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Tim kiem nhan vien thuoc  lich va nam nhap vao
        /// </summary>
        /// <param name="SearchTerm"></param>
        /// <param name="iYear"></param>
        /// <param name="ScheduleID"></param>
        /// <returns></returns>
        public ActionResult _SearchInSchedule(string SearchTerm, int iYear, int ScheduleID)
        {
            try
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                if (acc == null)
                {
                    Response.Redirect("/");
                    return null;
                }
                int ModuleID = 9;
                AssignEmpScheduleRepository _repository = new AssignEmpScheduleRepository(db);
                var userList = _repository.ListBoxInSchedule(SearchTerm, iYear, ScheduleID, acc.FAdm == 1, acc.UserName, ModuleID).ToList();

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

        public JsonResult _ListUser(int ScheduleID, int iYear)
        {
            try
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                if (acc == null)
                {
                    Response.Redirect("/");
                    return null;
                }
                int ModuleID = 9;
                AssignEmpScheduleRepository _repository = new AssignEmpScheduleRepository(db);
                var userList = _repository.ListUserNotIn(ScheduleID, iYear, acc.FAdm == 1, acc.UserName, ModuleID).ToList();

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
        public JsonResult _ListUserInSchedule(int ScheduleID, int iYear)
        {
            try
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                if (acc == null)
                {
                    Response.Redirect("/");
                    return null;
                }
                int ModuleID = 9;
                AssignEmpScheduleRepository _repository = new AssignEmpScheduleRepository(db);
                var userList = _repository.ListUserIn(ScheduleID, iYear, acc.FAdm == 1, acc.UserName, ModuleID).ToList();

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
        public ActionResult Delete(AssignEmpScheduleCreateViewModel model ,List<int> lstBoxAssigned)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                AssignEmpScheduleRepository _repository = new AssignEmpScheduleRepository(db);
                if (ModelState.IsValid)
                {
                    bool bResult = _repository.Delete(model.ScheduleID, model.Year, lstBoxAssigned);
                    if (bResult)
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _Error(model, "error");
                    }

                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += modelError.ErrorMessage + "-";
            }
            return  _Error(model, "error");
        }
        public ActionResult Save(AssignEmpScheduleCreateViewModel model, IEnumerable<string> lstBox)
        {

            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                AssignEmpScheduleRepository _repository = new AssignEmpScheduleRepository(db);
                if (ModelState.IsValid)
                {

                    TimeSheet_tblAssignEmpSchedule add = new TimeSheet_tblAssignEmpSchedule()
                    {
                        EmpID = model.EmpID,
                        ScheduleID =model.ScheduleID,
                        Year = model.Year,
                        Creater = acc.UserName,
                        CreateTime = DateTime.Now

                    };
                    bool bResult = _repository.Add(add, lstBox);
                    if (bResult)
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _Error(model, "Error");
                    }

                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += modelError.ErrorMessage + "-";
            }
            return _Error(model, errorMessage);
        }
        public ActionResult _Error(AssignEmpScheduleCreateViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new AssignEmpScheduleCreateViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            return PartialView("../AssignStaffSchedule/_Create", model);
        }
    }
}
