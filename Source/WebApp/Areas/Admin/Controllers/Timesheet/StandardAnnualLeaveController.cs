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
namespace WebUI.Areas.Admin.Controllers
{
    public class StandardAnnualLeaveController : BaseController
    {
        //
        // GET: /Admin/StandardAnnualLeave/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?returnUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../Timesheet/StandardAnnualLeave/Index");
        }
        public ActionResult _List()
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            int ModuleID = 9;
                
            StandardAnnualLeaveRepository _repository = new StandardAnnualLeaveRepository(db);
            List<StandardAnnualLeaveCreateViewModel> sources = _repository.List(acc.FAdm == 1, acc.UserName, ModuleID).ToList();
            return PartialView("../Timesheet/StandardAnnualLeave/_List", sources);
        }

        public ActionResult _Search(StandardAnnualLeaveCreateViewModel model, string Status, int ModuleID)
        {
           AccountViewModel acc = (AccountViewModel)Session["acc"];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            List<StandardAnnualLeaveCreateViewModel> sources = new List<StandardAnnualLeaveCreateViewModel>();
            StandardAnnualLeaveRepository _repository = new StandardAnnualLeaveRepository(db);

            if (Status == "True") // Tim kiem theo nhan vien chua thiet lap ngay phep
            {
                sources = _repository.ListNotIn(model.EmpCode, model.FullName, model.SearchLSCompanyID, acc.FAdm == 1, acc.UserName, ModuleID).ToList();
                ViewBag.Display = "0";
            }
            else // Tim kiem theo kieu da thiet lap ngay phep chuan
            {
                sources = _repository.ListIn(model.EmpCode, model.FullName, model.SearchLSCompanyID, acc.FAdm == 1, acc.UserName, ModuleID).ToList();
                ViewBag.Display = "1";
            }


            return PartialView("../Timesheet/StandardAnnualLeave/_List", sources);
        }
        public ActionResult Save(List<StandardAnnualLeaveCreateViewModel> model)
        {
            List<Timesheet_tblStandAnualLeaveDays> ret = new List<Timesheet_tblStandAnualLeaveDays>();

            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = "";
            if (acc != null)
            {
                StandardAnnualLeaveRepository _repository = new StandardAnnualLeaveRepository(db);
                foreach (var item in model)
                {
                    if (item.chkCheck)
                    {
                        Timesheet_tblStandAnualLeaveDays add = new Timesheet_tblStandAnualLeaveDays()
                        {
                            EmpID = item.EmpID,
                            StandardALDays = item.StandardALDays
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
        public ActionResult Update(List<StandardAnnualLeaveCreateViewModel> model)
        {
            List<Timesheet_tblStandAnualLeaveDays> ret = new List<Timesheet_tblStandAnualLeaveDays>();

            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = "";
            if (acc != null)
            {
                StandardAnnualLeaveRepository _repository = new StandardAnnualLeaveRepository(db);
                foreach (var item in model)
                {
                    if (item.chkCheck)
                    {
                        Timesheet_tblStandAnualLeaveDays add = new Timesheet_tblStandAnualLeaveDays()
                        {
                            StandardALID = item.StandardALID,
                            EmpID = item.EmpID,
                            StandardALDays = item.StandardALDays
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
        public ActionResult _SearchHeaderEmp()
        {
            return PartialView("../Timesheet/StandardAnnualLeave/_HeaderSearchEmp");
        }
        // tra ve partial view popup
        public ActionResult _PopupDetail(int? EmpID)
        {
            return PartialView("../Timesheet/StandardAnnualLeave/_popupAdjustAnnualLeave", EmpID);
        }

        public ActionResult _ListAdjust(int? EmpID)
        {
            StandardAnnualLeaveRepository _repository = new StandardAnnualLeaveRepository(db);
            List<StandardAnnualLeaveAdjustViewModel> sources = _repository.ListAdjust(EmpID).ToList();
            return PartialView("../Timesheet/StandardAnnualLeave/_ListAdjust",sources);
        }
        
        public ActionResult _ReloadInput()
        {
            StandardAnnualLeaveAdjustViewModel model = new StandardAnnualLeaveAdjustViewModel();
            return PartialView("../Timesheet/StandardAnnualLeave/_ReloadInput", model);
        }

        public ActionResult SaveAdjust(StandardAnnualLeaveAdjustViewModel model, int EmpID)
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            if (acc != null)
            {
                StandardAnnualLeaveRepository _repository = new StandardAnnualLeaveRepository(db);
                if (ModelState.IsValid)
                {
                    Timesheet_tblAnnualLeaveAdjust add = new Timesheet_tblAnnualLeaveAdjust()
                    {
                        EmpID = model.EmpID,
                        Days = model.Days,
                        Year = model.Year,
                        AdjustDate = System.DateTime.Today,
                        Note = model.Note
                    };
                    // add du lieu vao database
                    string bResult = _repository.AddAdjust(add);
                    if (bResult == "success")
                    {
                        return Content("success");
                    }
                    else
                    {
                        return Content("error");
                    }

                }
            }
            return Content("error");
        }
        [HttpPost]
        public ActionResult UpdateAdjust(StandardAnnualLeaveAdjustViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            if (acc != null)
            {
                StandardAnnualLeaveRepository _repository = new StandardAnnualLeaveRepository(db);
                if (ModelState.IsValid)
                {
                    string bResult = _repository.UpdateAdjust(model);
                    if (bResult == "success")
                        return Content("success");
                    else
                        return Content("error");
                }
            }
           return Content("success");
        }
        public ActionResult _EditAdjust(int id)
        {
            StandardAnnualLeaveRepository _repository = new StandardAnnualLeaveRepository(db);
            StandardAnnualLeaveAdjustViewModel model = _repository.FindEdit(id);
            return PartialView("../Timesheet/StandardAnnualLeave/_ReloadInput", model);
        }
        [HttpPost]
        public ActionResult _DeletePopup(int ALAdjustID)
        {
            StandardAnnualLeaveRepository _repository = new StandardAnnualLeaveRepository(db);
            bool bResult = _repository.DeleteAdjust(ALAdjustID);
            return Content("success");
        }

        public ActionResult _CreateError(List<StandardAnnualLeaveCreateViewModel> model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new List<StandardAnnualLeaveCreateViewModel>();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return View("../Timesheet/StandardAnnualLeave/Index", model);

        }
    }
}
