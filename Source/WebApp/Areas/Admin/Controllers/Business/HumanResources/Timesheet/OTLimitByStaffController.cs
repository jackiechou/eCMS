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
using Eagle.Common.Extensions;
using Eagle.Model.HR;
using Eagle.Common.Settings;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class OTLimitByStaffController : BaseController
    {
        //
        // GET: /Admin/OTLimitByStaff/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../Business/HumanResources/Timesheet/OTLimitByStaff/Index");
        }
        public ActionResult _List()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            int ModuleID = 9;

            OTLimitByStaffRepository _repository = new OTLimitByStaffRepository(db);
            List<OTLimitByStaffListViewModel> sources = _repository.List(acc.FAdm == 1, acc.UserName, ModuleID).ToList();
            return PartialView("../Business/HumanResources/Timesheet/OTLimitByStaff/_List", sources);
        }
        public ActionResult _Search(OTLimitByStaffCreateViewModel model, string Status, int ModuleID)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            List<OTLimitByStaffListViewModel> sources = new List<OTLimitByStaffListViewModel>();
            OTLimitByStaffRepository _repository = new OTLimitByStaffRepository(db);

            if (Status == "True") // Tim kiem theo nhan vien chua thiet lap ngay phep
                sources = _repository.ListNotIn(model.EmpCode, model.FullName, model.SearchLSCompanyID, acc.FAdm == 1, acc.UserName, ModuleID).ToList();
            else // Tim kiem theo kieu da thiet lap ngay phep chuan
                sources = _repository.ListIn(model.EmpCode, model.FullName, model.SearchLSCompanyID, acc.FAdm == 1, acc.UserName, ModuleID).ToList();


            return PartialView("../Business/HumanResources/Timesheet/OTLimitByStaff/_List", sources);
        }
        public ActionResult Save(List<OTLimitByStaffListViewModel> model)
        {
            List<Timesheet_tblOTLimitByStaff> ret = new List<Timesheet_tblOTLimitByStaff>();

            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                OTLimitByStaffRepository _repository = new OTLimitByStaffRepository(db);
                foreach (var item in model)
                {
                    if (item.chkCheck)
                    {
                        Timesheet_tblOTLimitByStaff add = new Timesheet_tblOTLimitByStaff()
                        {
                            EmpID = item.EmpID,
                            Hours = item.Hours
                        };
                        ret.Add(add);
                    }
                }
                // add du lieu vao database
                bool bResult = _repository.Add(ret, out errorMessage);
                if (bResult)
                    return Content("success");
                else
                    return _CreateError(model, errorMessage);

            }
            return _CreateError(model, errorMessage);
        }
        public ActionResult Update(List<OTLimitByStaffListViewModel> model)
        {
            List<Timesheet_tblOTLimitByStaff> ret = new List<Timesheet_tblOTLimitByStaff>();

            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                OTLimitByStaffRepository _repository = new OTLimitByStaffRepository(db);
                foreach (var item in model)
                {
                    if (item.chkCheck)
                    {
                        Timesheet_tblOTLimitByStaff add = new Timesheet_tblOTLimitByStaff()
                        {
                            OTLimitByStaffID = item.OTLimitByStaffID,
                            EmpID = item.EmpID,
                            Hours = item.Hours
                        };
                        ret.Add(add);
                    }
                }
                // add du lieu vao database
                bool bResult = _repository.Update(ret, out errorMessage);
                if (bResult)
                    return Content("success");
                else
                    return Content("error");

            }
            return _CreateError(model, errorMessage);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            OTLimitByStaffRepository _repository = new OTLimitByStaffRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _SearchHeaderEmp()
        {
            return PartialView("../Business/HumanResources/Timesheet/OTLimitByStaff/_HeaderSearchEmp");
        }
        public ActionResult _CreateError(List<OTLimitByStaffListViewModel> model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new List<OTLimitByStaffListViewModel>();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            return View("../Business/HumanResources/Timesheet/OTLimitByStaff/Index", model);
        }

    }
}
