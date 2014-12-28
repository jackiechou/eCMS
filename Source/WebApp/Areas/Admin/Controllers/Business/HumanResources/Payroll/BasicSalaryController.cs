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
    public class BasicSalaryController : BaseController
    {
        //
        // GET: /Admin/BasicSalary/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/BasicSalary/_Index");
            }
            return View("../Payroll/BasicSalary/Index");
        }
        public ActionResult _Create()
        {
            var ret = db.SYS_tblParameter.FirstOrDefault().TypeOfSalary;
            var strPartialView = "../Payroll/BasicSalary/_Create";
            

            if (ret == 1)
            {
                SalaryViewModels model = new SalaryViewModels() { PercentBasicSalary = 100 };
                strPartialView = "../Payroll/BasicSalary/_Create";
                return PartialView(strPartialView, model);

            }
            else
            {
                SalaryCoefViewModels model = new SalaryCoefViewModels() { PercentBasicCoef= 100 };
                strPartialView = "../Payroll/BasicSalary/_CreateCoef";
                return PartialView(strPartialView, model);
            }

        }
        public ActionResult _List()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            var ret = db.SYS_tblParameter.FirstOrDefault().TypeOfSalary;
            var strPartialView = "../Payroll/BasicSalary/_List";


            BasicSalaryRepository _repository = new BasicSalaryRepository(db);
            if (ret == 1)
            {
                strPartialView = "../Payroll/BasicSalary/_List";
                List<SalaryViewModels> sources = _repository.List(CurrentEmp.EmpID, LanguageId).ToList();
                return PartialView(strPartialView, sources);
            }
            else
            {
                strPartialView = "../Payroll/BasicSalary/_ListCoef";
                List<SalaryCoefViewModels> sources = _repository.ListCoef(CurrentEmp.EmpID, LanguageId).ToList();
                return PartialView(strPartialView, sources);
            }


            
        }
        public ActionResult GetList(int? EmpID, int? ModuleID)
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            BasicSalaryRepository _repository = new BasicSalaryRepository(db);
            List<SalaryViewModels> sources = _repository.List(CurrentEmp.EmpID, LanguageId).ToList();
            return PartialView("../Payroll/BasicSalary/_List", sources);
        }

        public ActionResult _CreateError(SalaryViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new SalaryViewModels();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Payroll/BasicSalary/_Create", model);

        }

        public ActionResult _CreateErrorCoef(SalaryCoefViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new SalaryCoefViewModels();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Payroll/BasicSalary/_CreateCoef", model);

        }
        [HttpPost]
        public ActionResult Save(SalaryViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                BasicSalaryRepository _repository = new BasicSalaryRepository(db);
                if (ModelState.IsValid)
                {
                    PR_tblSalary add = new PR_tblSalary()
                    {
                        EffectiveDate = model.EffectiveDate,
                        EmpID = model.EmpID,
                        BasicSalary = model.BasicSalary,
                        PercentBasicSalary = model.PercentBasicSalary,
                        ActualSalary = model.ActualSalary,
                        isGross = model.GROSSNET.Value,
                        TypeSalary = 1,
                        LSCurrencyID = model.LSCurrencyID,
                        InsuranceSalary = model.InsuranceSalary,
                        InsuranceLSCurrencyID = model.InsuranceLSCurrencyID,
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
        public ActionResult SaveCoef(SalaryCoefViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                BasicSalaryRepository _repository = new BasicSalaryRepository(db);
                if (ModelState.IsValid)
                {
                    PR_tblSalary add = new PR_tblSalary()
                    {
                        EffectiveDate = model.EffectiveDate,
                        EmpID = model.EmpID,
                        SalaryScaleID = model.SalaryScaleID,
                        SalaryGradeID = model.SalaryGradeID,
                        SalaryRankID = model.SalaryRankID,
                        BasicCoef = model.BasicCoef,
                        PercentBasicCoef = model.PercentBasicCoef,
                        ActualCoef = model.ActualCoef,
                        isGross = true,
                        TypeSalary = 2,
                        InsuranceSalary = model.InsuranceSalary,
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
                        return _CreateErrorCoef(model, bResult);
                    }

                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _CreateErrorCoef(model, errorMessage);
        }
        public ActionResult _Edit(int id)
        {
            BasicSalaryRepository _repository = new BasicSalaryRepository(db);
            SalaryViewModels model = _repository.FindEdit(id, LanguageId);
            return PartialView("../Payroll/BasicSalary/_Create", model);
        }
        [HttpPost]
        public ActionResult Update(SalaryViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                BasicSalaryRepository _repository = new BasicSalaryRepository(db);
                if (ModelState.IsValid)
                {
                    string bResult = _repository.Update(model);
                    if (bResult == "success")
                        return Content("success");
                    else
                        return _CreateError(model, bResult);

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
        public ActionResult _Delete(int id)
        {
            BasicSalaryRepository _repository = new BasicSalaryRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
    }
}
