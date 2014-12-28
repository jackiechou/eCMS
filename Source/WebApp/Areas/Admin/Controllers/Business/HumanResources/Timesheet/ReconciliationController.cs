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
using Eagle.Common.Settings;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class ReconciliationController : BaseController
    {
        //
        // GET: /Admin/Reconciliation/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../Business/HumanResources/Timesheet/Reconciliation/Index");
        }
        public ActionResult _HeaderSearchEmp()
        {
            ViewBag.Year = System.DateTime.Now.Year;
            return PartialView("../Business/HumanResources/Timesheet/Reconciliation/_HeaderSearchEmp");
        }
        /// <summary>
        /// Load luoi rỗng khi pageload
        /// </summary>
        /// <returns></returns>
        public ActionResult _List()
        {
            List<ReconciliationViewModel> Sources = new List<ReconciliationViewModel>();
            return PartialView("../Business/HumanResources/Timesheet/Reconciliation/_List",Sources);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            ReconciliationRepository _repository = new ReconciliationRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _Search(ReconciliationViewModel model, int ModuleID, string Status)
        {

            AccountViewModel acc = CurrentAcc;
            if (acc != null)
            {
                List<ReconciliationViewModel> sources = new List<ReconciliationViewModel>();
                ReconciliationRepository _repository = new ReconciliationRepository(db);
                ViewBag.LanguageId = LanguageId;
                if (Status == "True")
                    sources = _repository.ListNotIn(model.EmpCode, model.FullName, acc.FAdm == 1, acc.UserName, ModuleID, model.Year, model.LSCompanyID);
                else
                    sources = _repository.ListIn(model.EmpCode, model.FullName, acc.FAdm == 1, acc.UserName, ModuleID, model.Year, model.LSCompanyID).ToList();

                return PartialView("../Business/HumanResources/Timesheet/Reconciliation/_List", sources);
            }
            else
            {
                return Content("timeout");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Save(List<ReconciliationViewModel> model)
        {
            List<Timesheet_tblReconciliation> ret = new List<Timesheet_tblReconciliation>();

            SYS_tblUserAccount acc = (SYS_tblUserAccount)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                ReconciliationRepository _repository = new ReconciliationRepository(db);
                foreach (var item in model)
                {
                    if (item.chkCheck && item.LeaveForward > 0)
                    {
                        if (item.LeaveForward > item.ALBalancePrevious)
                        {
                            return _CreateError(model, Eagle.Resource.LanguageResource.Reconciliation_ErrorLeaveForward);
                        }
                        Timesheet_tblReconciliation add = new Timesheet_tblReconciliation()
                        {
                            EmpID = item.EmpID,
                            ALPrevious = item.ALPrevious,
                            ALTakenPrevious = item.ALTakenPrevious,
                            ALBalancePrevious = item.ALBalancePrevious,
                            Year = item.Year + 1,
                            LeaveForward = item.LeaveForward,
                            LeaveForwardTaken = 0
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
        public ActionResult _CreateError(List<ReconciliationViewModel> model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new List<ReconciliationViewModel>();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Business/HumanResources/Timesheet/Reconciliation/_Create", model);

        }
    }
}
