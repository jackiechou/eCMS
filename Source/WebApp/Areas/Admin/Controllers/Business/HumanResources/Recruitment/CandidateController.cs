using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.HR;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class CandidateController : BaseController
    {
        #region Candidate box -----------------------------------------------
        //header
        public PartialViewResult _CandidateBox()
        {

            CandidatetmpViewModel candidateModel = (CandidatetmpViewModel)Session["CandidateSelected"];
            if (candidateModel == null)
            {
                candidateModel = new CandidatetmpViewModel();
            }
            return PartialView("../Business/HumanResources/REC/Candidate/_CandidateBox", candidateModel);
        }
        #endregion

        //Khung popup
        public ActionResult _PopupCandidatePartial()
        {
            return PartialView("../Business/HumanResources/REC/Candidate/_PopupCandidatePartial");
        }
        //Vùng nhập dữ liệu search
        public ActionResult _SearchAreasPartial()
        {
            CreateViewBag();
            return PartialView("../Business/HumanResources/REC/Candidate/_SearchAreasPartial");
        }
        //table kết quả trả về khi nhấn nút search
        public ActionResult _SearchResultsForPopup(CandidateSearch2ViewModel searchModel)
        {
            CandidateRepository _repository = new CandidateRepository(db);
            List<CandidateResultViewModel> lst = _repository.Search(searchModel);
            return PartialView("../Business/HumanResources/REC/Candidate/_SearchResultsForPopup", lst);
        }
        //thay đổi session CandidateSelected
        [HttpPost]
        public ActionResult ChangeCandidateID(int CandidateID)
        {
            try
            {
                CandidateRepository _repository = new CandidateRepository(db);
         
                CandidatetmpViewModel model = _repository.FindCandidatetmp(CandidateID);
                Session["CandidateSelected"] = model;
                return Content("success");
            }
            catch
            {
                return Content("error");
            }
        }
        #region Create ViewBag
        private void CreateViewBag(int? Result = null, int? Gender = null)
        {
            ViewBag.ResultSearch = new SelectList(CommonRepository.GetCadidateResult(), "Key", "Value", Result);

            ViewBag.GenderSearch = new SelectList(CommonRepository.GetGenderList(), "Key", "Value", Gender);
        }
        #endregion
    }
}
