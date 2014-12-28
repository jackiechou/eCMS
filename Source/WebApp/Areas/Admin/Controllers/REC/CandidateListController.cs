using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Repository;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class CandidateListController : BaseController
    {
        //
        // GET: /Admin/CandidateList/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../REC/CandidateList/Index");
        }

        public ActionResult _Create()
        {
            CreateViewBag();
            return PartialView("../REC/CandidateList/_Create");
        }

        public ActionResult _List(CandidateSearchViewModel model)
        {
            CandidateRepository _repository = new CandidateRepository(db);
            List<CandidateResultViewModel> result = _repository.Search(model);
            return PartialView("../REC/CandidateList/_List", result);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            CandidateRepository _repository = new CandidateRepository(db);
            string errorMessage = string.Empty;
            bool Result = _repository.Delete(id,out errorMessage);
            if (Result)
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }
        }
        public ActionResult _DeleteList(string[] chkDelete)
        {
            if (chkDelete != null)
            {
                chkDelete = chkDelete.Distinct().ToArray();
            }
            CandidateRepository _repository = new CandidateRepository(db);
            string errorMessage = "";
            bool result =  _repository.Delete(chkDelete,out errorMessage);
            if (result)
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }
        }
        #region Create ViewBag
        private void CreateViewBag(int? Result = null, int? Gender = null)
        {
            ViewBag.Result = new SelectList(CommonRepository.GetCadidateResult(), "Key", "Value", Result);

            ViewBag.Gender = new SelectList(CommonRepository.GetGenderList(), "Key", "Value", Gender);

        }
        #endregion

    }
}
