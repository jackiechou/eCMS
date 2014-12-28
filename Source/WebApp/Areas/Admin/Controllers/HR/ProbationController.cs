using Eagle.Repository;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model.HR;

namespace Eagle.WebApp.Areas.Admin.Controllers.HR
{
    public class ProbationController : BaseController
    {
        //
        // GET: /Admin/Probation/

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView("../Services/Probation/_ProbationaryService");
            else
                return View("../Services/Probation/Index");
        }

        //Thong tin nhan vien thuc tap theo type 1: today, 7:week, 60: 2 thang
        public ActionResult GetProbationaryList(int? Type)
        {
            List<EmployeeViewModel> lst = EmployeeRepository.GetProbationaryList(Type, LanguageId);
            return PartialView("../Services/Probation/_ProbationaryList", lst);
        }
    }
}
