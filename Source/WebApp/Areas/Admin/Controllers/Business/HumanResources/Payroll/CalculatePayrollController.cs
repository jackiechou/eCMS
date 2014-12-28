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
    public class CalculatePayrollController : BaseController
    {
        //
        // GET: /Admin/CalculatePayroll/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../Payroll/CalculatePayroll/Index");
        }

        public ActionResult _HeaderSearchList()
        {
            return PartialView("../Payroll/CalculatePayroll/_HeaderSearchList");
        }
        public ActionResult _CalculatePayroll(SearchForPayrollViewModels model, int ModuleID)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            var ret = db.PR_spfrmPayrollCalculation(model.MMYYYY, model.EmpCode, model.FullName, model.LSCompanyID);
            return Content("success");

        }
        public ActionResult _Search(SearchForPayrollViewModels model, int ModuleID)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc != null)
            {
                CalculatePayrollRepository _Repository = new CalculatePayrollRepository(db);
                List<PayrollViewModels> sources = _Repository.List(model, acc.FAdm == 1, acc.UserName, ModuleID, LanguageId);
                return PartialView("../Payroll/CalculatePayroll/_List", sources);
            }
            return Content("success");
        }

        public ActionResult _List()
        {
            List<PayrollViewModels> sources = new List<PayrollViewModels>();
            return PartialView("../Payroll/CalculatePayroll/_List", sources);
        }
    }
}
