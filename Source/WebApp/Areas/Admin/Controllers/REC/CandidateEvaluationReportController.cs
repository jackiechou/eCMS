using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class CandidateEvaluationReportController : BaseController
    {
        //
        // GET: /Admin/CandidateEvaluationReport/

        public ActionResult Index()
        {
            return View("../REC/CandidateEvaluationReport/Index");
        }

    }
}
