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
namespace Eagle.WebApp.Areas.Administration.Controllers
{
    public class SalaryDeductionController : BaseController
    {
        //
        // GET: /Administration/SalaryDeduction/

        #region man hinh chinh
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Administration/Login?returnUrl=" + Request.Url.AbsoluteUri);
                return null;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/SalaryDeduction/_Index");
            }
            return View("../Payroll/SalaryDeduction/Index");
        }
        public ActionResult _List()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Administration/Login?returnUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            List<DeductionSalaryViewModels> sources = new List<DeductionSalaryViewModels>();
            return PartialView("../Payroll/SalaryDeduction/_List", sources);
        }
        public ActionResult _Create()
        {
            DeductionSalaryViewModels model = new DeductionSalaryViewModels();
            return PartialView("../Payroll/SalaryDeduction/_Create", model);
        }
        public ActionResult _CreateError(DeductionSalaryViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new DeductionSalaryViewModels();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Payroll/SalaryDeduction/_Create", model);

        }
        public ActionResult Save(DeductionSalaryViewModels model, List<EmployeeForPayrollViewModels> StaffList)
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = "";
            if (acc != null)
            {
                SalaryDeductionByStaffRepository _repository = new SalaryDeductionByStaffRepository(db);
                if (ModelState.IsValid)
                {
                    if (StaffList != null && StaffList.Count > 0)
                    {
                        try
                        {
                            foreach (var item in StaffList)
                            {
                                PR_tblDeductionSalary add = new PR_tblDeductionSalary()
                                {
                                    EmpID = item.EmpID,
                                    LSDeductSalaryID = model.LSDeductSalaryIDNull.Value,
                                    PaymentMethod = model.PaymentMethod,
                                    Amount = model.Amount,
                                    FromMonth = model.FromMonth,
                                    ToMonth = model.ToMonth,
                                    isGross = model.GROSSNET.Value,
                                    LSCurrencyID = model.LSCurrencyID,
                                    Note = model.Note
                                };
                                db.Entry(add).State = System.Data.Entity.EntityState.Added;
                            }

                            db.SaveChanges();
                            return Content("success");
                        }
                        catch (Exception ex)
                        {
                            return _CreateError(model, ex.Message);
                        }
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
        #endregion
        #region Partial List Search and Add Employee (popup)
        public ActionResult _ListStaffPartial()
        {
            List<EmployeeForPayrollViewModels> lst = new List<EmployeeForPayrollViewModels>();
            return PartialView("../Payroll/SalaryDeduction/_ListStaffPartial", lst);
        }

        public ActionResult _SearchEmployeePartial()
        {
            EmployeeForPayrollViewModels model = new EmployeeForPayrollViewModels();
            return PartialView("../Payroll/SalaryDeduction/_SearchEmployeePartial", model);
        }
        public ActionResult _ListStaffPopup()
        {
            List<EmployeeForPayrollViewModels> lst = new List<EmployeeForPayrollViewModels>();
            return PartialView("../Payroll/SalaryDeduction/_StaffSearchResultPartial", lst);
        }
        public ActionResult _SearchPopup(string StaffList, EmployeeForPayrollViewModels model, int ModuleID)
        {

            AccountViewModel acc = (AccountViewModel)Session["acc"];
            if (acc == null)
            {
                Content("TimeOutError");
            }

            List<int> StaffIDSelectedList = new List<int>();
            if (StaffList != null)
            {
                string[] strTmp = StaffList.Split(';');
                int tmp;
                foreach (var item in strTmp)
                {
                    try
                    {
                        tmp = Convert.ToInt32(item);
                        StaffIDSelectedList.Add(tmp);
                    }
                    catch { }
                }
            }



            SalaryDeductionByStaffRepository _Repository = new SalaryDeductionByStaffRepository(db);
            List<EmployeeForPayrollViewModels> lst = _Repository.List(model, acc.FAdm == 1, acc.UserName, ModuleID, StaffIDSelectedList, LanguageId);
            return PartialView("../Payroll/SalaryDeduction/_StaffSearchResultPartial", lst);
        }
        public ActionResult _AddStaffToList(string StaffListAdd, List<EmployeeForPayrollViewModels> StaffList)
        {
            if (StaffList == null)
            {
                StaffList = new List<EmployeeForPayrollViewModels>();
            }
            List<int> StaffIDListAdd = new List<int>();
            if (StaffListAdd != null)
            {
                string[] strTmp = StaffListAdd.Split(';');
                int tmp;
                foreach (var item in strTmp)
                {
                    try
                    {
                        tmp = Convert.ToInt32(item);
                        if (!StaffIDListAdd.Contains(tmp))
                        {
                            StaffIDListAdd.Add(tmp);
                        }
                    }
                    catch { }
                }
            }

            SalaryDeductionByStaffRepository _repository = new SalaryDeductionByStaffRepository(db);
            List<EmployeeForPayrollViewModels> modelAdds = _repository.GetByListID(StaffIDListAdd, LanguageId);

            StaffList.AddRange(modelAdds);

            return PartialView("../Payroll/SalaryDeduction/_ListStaffPartial", StaffList);

        }
        [HttpPost]
        public ActionResult _DeleteStaffList(string StaffSelectedList, List<EmployeeForPayrollViewModels> StaffList)
        {

            if (StaffList == null)
            {
                StaffList = new List<EmployeeForPayrollViewModels>();
            }
            List<int> StaffIDListAdd = new List<int>();
            if (StaffSelectedList != null)
            {
                string[] strTmp = StaffSelectedList.Split(';');
                int tmp;
                foreach (var item in strTmp)
                {
                    try
                    {
                        tmp = Convert.ToInt32(item);
                        StaffIDListAdd.Add(tmp);
                    }
                    catch { }
                }
            }
            //xóa
            foreach (var item in StaffIDListAdd)
            {
                var modelDelete = StaffList.FirstOrDefault(p => p.EmpID == item);
                if (modelDelete != null)
                {
                    StaffList.Remove(modelDelete);
                }
            }

            return PartialView("../Payroll/SalaryDeduction/_ListStaffPartial", StaffList);
        }
        #endregion
        #region Danh sách các khoản tru luong
        public ActionResult IndexList()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Administration/Login?returnUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../Payroll/SalaryDeduction/IndexList");
        }
        public ActionResult _HeaderSearchList()
        {
            return PartialView("../Payroll/SalaryDeduction/_HeaderSearchList");
        }
        public ActionResult _ListDeductionSalary()
        {

            List<DeductionSalaryViewModels> sources = new List<DeductionSalaryViewModels>();
            return PartialView("../Payroll/SalaryDeduction/_ListDeductionSalary", sources);
        }
        public ActionResult _SearchList(DeductionSalarySearchViewModels model, int ModuleID)
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Administration/Login?returnUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            SalaryDeductionByStaffRepository _repository = new SalaryDeductionByStaffRepository(db);
            List<DeductionSalaryViewModels> sources = _repository.ListSearch(model, CurrentAcc.FAdm == 1, CurrentAcc.UserName, ModuleID, LanguageId).ToList();
            return PartialView("../Payroll/SalaryDeduction/_ListDeductionSalary", sources);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            SalaryDeductionByStaffRepository _repository = new SalaryDeductionByStaffRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _PopupEditDeductionSalary(int DeductionSalaryID)
        {
            SalaryDeductionByStaffRepository _Repository = new SalaryDeductionByStaffRepository(db);
            DeductionSalaryViewModels sources = _Repository.FindEdit(DeductionSalaryID, LanguageId);
            return PartialView("../Payroll/SalaryDeduction/_PopupEditDeductionSalary", sources);
        }
        [HttpPost]
        public ActionResult Update(DeductionSalaryViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = "";
            if (acc != null)
            {
                SalaryDeductionByStaffRepository _repository = new SalaryDeductionByStaffRepository(db);
                if (ModelState.IsValid)
                {
                    string bResult = _repository.Update(model);
                    if (bResult == "success")
                        return Content("success");
                    else
                        return _PopupError(model, bResult);

                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _PopupError(model, errorMessage);
        }

        public ActionResult _PopupError(DeductionSalaryViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new DeductionSalaryViewModels();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Payroll/SalaryDeduction/_PopupEditDeductionSalaryFormInterPartial", model);
        }

        #endregion

    }
}
