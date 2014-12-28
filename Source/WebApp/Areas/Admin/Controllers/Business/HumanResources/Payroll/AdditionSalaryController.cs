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
    public class AdditionSalaryController : BaseController
    {
        //
        // GET: /Admin/AdditionSalary/
        #region man hinh chinh 
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/AdditionSalary/_Index");
            }
            return View("../Payroll/AdditionSalary/Index");
        }
        public ActionResult _List()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            List<AdditionSalaryViewModels> sources = new List<AdditionSalaryViewModels>();
            return PartialView("../Payroll/AdditionSalary/_List", sources);
        }
        public ActionResult _Create()
        {
            AdditionSalaryViewModels model = new AdditionSalaryViewModels();
            return PartialView("../Payroll/AdditionSalary/_Create", model);
        }
        public ActionResult _GetData(AdditionSalaryViewModels model, int ModuleID)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            ViewBag.TypeList = db.LS_tblSalaryAdjust.Select(p => new { ID = p.LSSalaryAdjustID, Name = LanguageId == 124 ? p.Name : p.VNName });
            List<AdditionSalaryViewModels> sources = new List<AdditionSalaryViewModels>();
            AddSalaryByStaffRepository _repository = new AddSalaryByStaffRepository(db);
            sources = _repository.GetData(model,LanguageId, model.EmpCode, model.FullName, model.LSCompanyID, model.LSPositionID, acc.FAdm == 1, acc.UserName, ModuleID).ToList();
            return PartialView("../Payroll/AdditionSalary/_List", sources);
        }
        public ActionResult _CreateError(AdditionSalaryViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new AdditionSalaryViewModels();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Payroll/AdditionSalary/_Create", model);

        }
        public ActionResult Save(AdditionSalaryViewModels model, List<EmployeeForPayrollViewModels> StaffList)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                AddSalaryByStaffRepository _repository = new AddSalaryByStaffRepository(db);
                if (ModelState.IsValid)
                {
                    if (StaffList != null && StaffList.Count > 0)
                    {
                        try
                        {
                            foreach (var item in StaffList)
                            {
                                PR_tblAdditionSalary add = new PR_tblAdditionSalary()
                                {
                                    EmpID = item.EmpID,
                                    LSSalaryAdjustID = model.LSSalaryAdjustIDNull.Value,
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
            return PartialView("../Payroll/AdditionSalary/_ListStaffPartial", lst);
        }

        public ActionResult _SearchEmployeePartial()
        {
            EmployeeForPayrollViewModels model = new EmployeeForPayrollViewModels();
            return PartialView("../Payroll/AdditionSalary/_SearchEmployeePartial", model);
        }
        public ActionResult _ListStaffPopup()
        {
            List<EmployeeForPayrollViewModels> lst = new List<EmployeeForPayrollViewModels>();
            return PartialView("../Payroll/AdditionSalary/_StaffSearchResultPartial", lst);
        }
        public ActionResult _SearchPopup(string StaffList, EmployeeForPayrollViewModels model, int ModuleID)
        {

            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
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



            AddSalaryByStaffRepository _Repository = new AddSalaryByStaffRepository(db);
            List<EmployeeForPayrollViewModels> lst = _Repository.List(model, acc.FAdm == 1, acc.UserName, ModuleID, StaffIDSelectedList, LanguageId);
            return PartialView("../Payroll/AdditionSalary/_StaffSearchResultPartial", lst);    
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

            AddSalaryByStaffRepository _repository = new AddSalaryByStaffRepository(db);
            List<EmployeeForPayrollViewModels> modelAdds = _repository.GetByListID(StaffIDListAdd, LanguageId);

            StaffList.AddRange(modelAdds);

            return PartialView("../Payroll/AdditionSalary/_ListStaffPartial", StaffList);
 
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

            return PartialView("../Payroll/AdditionSalary/_ListStaffPartial", StaffList);
        }
        #endregion
        #region Danh sách các khoản them luong
        public ActionResult IndexList()
        {
         if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
         return View("../Payroll/AdditionSalary/IndexList");
        }

        public ActionResult _HeaderSearchList()
        {
            return PartialView("../Payroll/AdditionSalary/_HeaderSearchList");
        }
        public ActionResult _ListAdditionSalary()
        {

            List<AdditionSalaryViewModels> sources = new List<AdditionSalaryViewModels>();
            return PartialView("../Payroll/AdditionSalary/_ListAdditionSalary", sources);
        }
                             
        public ActionResult _SearchList(AdditionSalaryViewModels model, int ModuleID )
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            AddSalaryByStaffRepository _repository = new AddSalaryByStaffRepository(db);
            List<AdditionSalaryViewModels> sources = _repository.ListSearch(model, CurrentAcc.FAdm == 1, CurrentAcc.UserName, ModuleID, LanguageId).ToList();
            return PartialView("../Payroll/AdditionSalary/_ListAdditionSalary", sources);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            AddSalaryByStaffRepository _repository = new AddSalaryByStaffRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _PopupEditAdditionSalary(int SalaryAdjustID )
        {
            AddSalaryByStaffRepository _Repository = new AddSalaryByStaffRepository(db);
            AdditionSalaryViewModels sources = _Repository.FindEdit(SalaryAdjustID, LanguageId);
            return PartialView("../Payroll/AdditionSalary/_PopupEditAdditionSalary", sources);
        }
        //
        [HttpPost]
        public ActionResult Update(AdditionSalaryViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                AddSalaryByStaffRepository _repository = new AddSalaryByStaffRepository(db);
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

        public ActionResult _PopupError(AdditionSalaryViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new AdditionSalaryViewModels();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Payroll/AdditionSalary/_PopupEditAdditionSalaryFormInterPartial", model);
        }
     
        #endregion
    }
}
