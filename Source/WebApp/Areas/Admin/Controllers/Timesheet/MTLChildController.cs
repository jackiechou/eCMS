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
    public class MTLChildController : BaseController
    {
        //
        // GET: /Admin/MTLChild/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../Timesheet/MTLChild/Index");
        }
        /// <summary>
        /// tra ve danh sach  null khi moi load page
        /// </summary>
        /// <returns></returns>
        public ActionResult _List()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            List<MTLChildListViewModel> sources = new List<MTLChildListViewModel>();
            return PartialView("../Timesheet/MTLChild/_List", sources);
        }
        public ActionResult _Search(MTLChildViewModel model, string Status, int ModuleID)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            List<MTLChildListViewModel> sources = new List<MTLChildListViewModel>();
            MTLChildRepository _repository = new MTLChildRepository(db);

            if (Status == "True") // Tim kiem theo nhan vien chua thiet lap ngay phep
            {
                ViewBag.Display = "0";
                sources = _repository.ListNotIn(model.EmpCode, model.FullName, model.LSCompanyID, acc.FAdm == 1, acc.UserName, ModuleID).ToList();
            }
            else // Tim kiem theo kieu da thiet lap ngay phep chuan
            {
                ViewBag.Display = "1";
                sources = _repository.ListIn(model.EmpCode, model.FullName, model.LSCompanyID, acc.FAdm == 1, acc.UserName, ModuleID).ToList();
            }


            return PartialView("../Timesheet/MTLChild/_List", sources);
        }
        public ActionResult _Delete(int id)
        {
            MTLChildRepository _repository = new MTLChildRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult Save(List<MTLChildListViewModel> model)
        {
            List<Timesheet_tblMTLChild> ret = new List<Timesheet_tblMTLChild>();
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                MTLChildRepository _repository = new MTLChildRepository(db);
                foreach (var item in model)
                {
                    if (item.chkCheck)
                    {
                        if (item.strFromDate != "01/01/0001" && item.strToDate != "01/01/0001")
                        {
                            if (item.ToDate >= item.FromDate)
                            {
                                Timesheet_tblMTLChild add = new Timesheet_tblMTLChild()
                                {
                                    EmpID = item.EmpID,
                                    FromDate = item.FromDate,
                                    ToDate = item.ToDate
                                };
                                ret.Add(add);
                            }
                            else {
                                return Content("ValidateFromToDate");
                            }
                        }
                        else
                        {
                            return Content("InputDate");
                        }
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
        public ActionResult Update(List<MTLChildListViewModel> model)
        {
            List<Timesheet_tblMTLChild> ret = new List<Timesheet_tblMTLChild>();

            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                MTLChildRepository _repository = new MTLChildRepository(db);
                foreach (var item in model)
                {
                    if (item.chkCheck)
                    {
                        Timesheet_tblMTLChild add = new Timesheet_tblMTLChild()
                        {
                            MLChildID = item.MLChildID,
                            EmpID = item.EmpID,
                            FromDate = item.FromDate,
                            ToDate = item.ToDate
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
            return PartialView("../Timesheet/MTLChild/_HeaderSearchEmp");
        }
        public ActionResult _CreateError(List<MTLChildListViewModel> model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new List<MTLChildListViewModel>();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return View("../Timesheet/MTLChild/Index", model);

        }
    }
}
