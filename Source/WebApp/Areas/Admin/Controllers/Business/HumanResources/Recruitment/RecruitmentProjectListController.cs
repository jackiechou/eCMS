using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class RecruitmentProjectListController : BaseController
    {
        //
        // GET: /Admin/RecruitmentProjectList/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }

            ViewBag.ParamSearch = new RecruitmentProjectSearchViewModel() { Status = 1, ProjectCode = "" };
            return View("../Business/HumanResources/REC/RecruitmentProjectList/Index");
        }
        public ActionResult _Create()
        {
            RecruitmentProjectSearchViewModel model = new RecruitmentProjectSearchViewModel();
            ViewBag.Status = new SelectList(CommonRepository.Status(), "Key", "Value");            
            return PartialView("../Business/HumanResources/REC/RecruitmentProjectList/_Create", model);
        }
        public ActionResult _List(RecruitmentProjectSearchViewModel model)
        {
            List<RecruitmentProjectViewModel> lst = new List<RecruitmentProjectViewModel>();
            RecruitmentProjectRepository _repository = new RecruitmentProjectRepository(db);
            lst = _repository.Search(model, LanguageId);
            return PartialView("../Business/HumanResources/REC/RecruitmentProjectList/_List", lst);
        }
        [HttpPost]
        public ActionResult _Delete(int Id)
        {
            RecruitmentProjectRepository _repository = new RecruitmentProjectRepository(db);
            string errorMessage = "";
            if (_repository.Delete(Id, out errorMessage))
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }
        }
    }
}
