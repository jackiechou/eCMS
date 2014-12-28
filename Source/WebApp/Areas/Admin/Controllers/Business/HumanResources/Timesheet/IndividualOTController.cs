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
    public class IndividualOTController : BaseController
    {
        //
        // GET: /Admin/IndividualOT/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../Business/HumanResources/Timesheet/IndividualOT/Index");
        }
        public ActionResult _SearchHeaderEmp()
        {
            return PartialView("../Business/HumanResources/Timesheet/IndividualOT/_HeaderEmpSearch");
        }
        public ActionResult _List()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            IndividualOTRepository _repository = new IndividualOTRepository(db);
            List<IndividualOTListViewModel> sources = new List<IndividualOTListViewModel>();
            return PartialView("../Business/HumanResources/Timesheet/IndividualOT/_List", sources);
        }
        public ActionResult _Search(IndividualOTSearchViewModel model, string Status, int ModuleID)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            List<IndividualOTListViewModel> sources = new List<IndividualOTListViewModel>();
            IndividualOTRepository _repository = new IndividualOTRepository(db);
            sources = _repository.Search(model.EmpCode, model.FullName, model.LSCompanyID, acc.FAdm == 1, acc.UserName, ModuleID).ToList();

            return PartialView("../Business/HumanResources/Timesheet/IndividualOT/_List", sources);
        }
    }
}
