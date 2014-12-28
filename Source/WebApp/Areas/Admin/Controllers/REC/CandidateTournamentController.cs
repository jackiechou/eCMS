using AutoMapper;
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
    public class CandidateTournamentController : BaseController
    {
        //
        // GET: /Admin/CandidateTournament/

        public ActionResult Index(int? ProjectTournamentID = null, int? CandidateID = null, string mode = "search", ResultsRecordSearchViewModel ParamSearch = null)
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            //Tìm kiếm
            if (mode == "search")
            {
                ViewBag.ParamSearch = ParamSearch;
                return View("../REC/CandidateTournament/Index");
            }
            //Edit
            else
            {
                ViewBag.ProjectTournamentID = ProjectTournamentID;
                ViewBag.CandidateID = CandidateID;
                return View("../REC/CandidateTournament/Edit/Edit");
            }

        }
      
        #region //Search
        //Tạo các input để search
        public ActionResult _Create(ResultsRecordSearchViewModel model)
        {
            if (model == null)
            {
                model = new ResultsRecordSearchViewModel();
            }
            CreateViewBag(model.ProjectID, model.ProjectTournamentID, model.Result);
            return PartialView("../REC/CandidateTournament/_Create", model);
        }
        //Nhấn nút search thì hiển thị danh sách này
        public ActionResult _List(ResultsRecordSearchViewModel model)
        {
            CandidateTournamentRepository _repository = new CandidateTournamentRepository(db);
            bool IsFirstRound = false;
            List<CandidateTournamentViewModel> list = _repository.Search(model, out IsFirstRound);
            //Nếu là vòng đầu tiên thì Hiển thị "Not reached" còn các vòng sau hiển thị Surpassed previos round
            ViewBag.IsFirstRound = IsFirstRound;

            return PartialView("../REC/CandidateTournament/_List", list);
        }
       //Tạo viewbag cho search box
        private void CreateViewBag(int? ProjectID = null, int? ProjectTournamentID = null, int? Result = null)
        {
            RecruitmentProjectRepository _recruitmentProjectRepository = new RecruitmentProjectRepository(db);
            ProjectTournamentRepository _projectTournamentRepository = new ProjectTournamentRepository(db);

            //Lấy danh sách các Phương án tuyển dụng mà nhân viên đang đăng nhập được phỏng vấn
            Dictionary<int, string> prList = _recruitmentProjectRepository.GetProjectCodeBy(CurrentAcc.EmpId);
            //Nếu ProjectID null set mặc định ProjectID && ProjectTournamentID
            if (ProjectID == null && prList.Count > 1)
            {
                ProjectID = prList.Where(p => p.Key > 0).FirstOrDefault().Key;

                ProjectTournamentID = _projectTournamentRepository.GetTournamentDefaultValueByProjectID(ProjectID, CurrentAcc.EmpId);
            }
            ViewBag.ProjectID = new SelectList(prList, "Key", "Value", ProjectID);

            //Lấy tất cả các danh sách vòng tuyển dụng thuộc phương án tuyển dụng được chọn
            Dictionary<int, string> rtList = _projectTournamentRepository.GetTournamentByProjectID(ProjectID);
            ViewBag.ProjectTournamentID = new SelectList(rtList, "Key", "Value", ProjectTournamentID);

            ViewBag.Result = new SelectList(CommonRepository.GetProjectResult(), "Key", "Value", Result);
        }

        #endregion
     
        #region //Edit
        //Tạo các input cho lúc phỏng vấn
        public ActionResult _ModifiCandidateTournament(int? CandidateID = null, int? ProjectTournamentID = null)
        {
            #region
            int catId = 0;
            int proTouId = 0;
            try
            {
                catId = CandidateID.Value;
                proTouId = ProjectTournamentID.Value;
            }
            catch { }
            CandidateTournamentRepository _repository = new CandidateTournamentRepository(db);
            #endregion
            //Lấy CandidateTournament cần duyệt
            //Nếu chưa có tạo model tạm
            CandidateTournamentViewModel candidateTournamentmodel = new CandidateTournamentViewModel() { CandidateID = catId, ProjectTournamentID = proTouId };
            if (ProjectTournamentID != null)
            {
                candidateTournamentmodel = _repository.Find(CandidateID, ProjectTournamentID);
            }
            CreateViewBagForModifi(candidateTournamentmodel);

            #region check quyền và báo nếu k cho quyền phỏng vấn
            string errorMessage = "";
            ViewBag.SavePermission = CheckSavePermission(catId, proTouId, CurrentAcc.EmpId, out errorMessage);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.DisplayErrorMessage = true;
                ViewBag.CssClass = "alert alert-warning";
                ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
                ViewBag.Message = errorMessage;
            }
            #endregion
           
            return PartialView("../REC/CandidateTournament/Edit/_ModifiCandidateTournament", candidateTournamentmodel);
        }
        
        //Quyền được quyền save
        private bool CheckSavePermission(int catId, int proTouId,int? empID, out string errorMessage)
        {
            if (empID == null || empID == 0)
            {
                errorMessage = Eagle.Resource.LanguageResource.CouldNotFound;
                return false;
            }
            else
            {
                errorMessage = "";
                //1. Nếu là Employee phỏng vấn vòng này thì mới có quyền sửa
                //2. Nếu chưa đánh giá thì mới có quyền sửa
                //3. Nếu cấp trước đã đánh giá thì mới được đánh cấp hiện tại (trừ cấp 1)
                #region 1. Check quyền employee
                var ptournament = db.REC_tblProjectTournament.Find(proTouId);
                if (ptournament != null)
                {
                    if (ptournament.EmpID != empID.Value)
                    {
                        //Không phải nhân viên này được quyền phỏng vấn vòng này
                        if (ptournament.HR_tblEmp != null)
                        {
                            string employeeInterview = ptournament.HR_tblEmp.LastName + " " + ptournament.HR_tblEmp.FirstName;
                            errorMessage = string.Format(Eagle.Resource.LanguageResource.CandidateTournament_ErrorMessage3, employeeInterview);
                        }
                        return false;
                    }
                }
                else
                {
                    //Không tìm thấy vòng phỏng vấn
                    errorMessage = Eagle.Resource.LanguageResource.CouldNotFoundThisRound;
                    return false;
                }
                #endregion
                #region 2. Nếu chưa đánh giá mới có quyền sửa
                var check = db.REC_tblCandidateTournament.Where(p => p.CandidateID == catId && p.ProjectTournamentID == proTouId).FirstOrDefault();
                if (check != null)
                {
                    return false;
                }
                #endregion
                #region 3. Nếu cấp trước đã đánh giá là pass thì mới được đánh cấp hiện tại (trừ cấp 1)
                //Vòng duyệt trước
                var preProjectTournament = (from pt in db.REC_tblProjectTournament
                                            where pt.ProjectID == ptournament.ProjectID && pt.SEQ < ptournament.SEQ
                                            orderby pt.SEQ descending
                                            select pt).FirstOrDefault();
                //-------Nếu bằng null => đây là vòng đầu => được quyền chỉnh sửa--------
                //Nếu khác null => đây không phải là vòng đầu => xét cấp trước phải có kết quả pass mới cho phỏng vấn
                if (preProjectTournament != null)
                {
                    var checkModel = db.REC_tblCandidateTournament.Where(p => p.CandidateID == catId && p.ProjectTournamentID == preProjectTournament.ProjectTournamentID).FirstOrDefault();
                    //Nếu checkModel == null => chưa phỏng vấn loại
                    if (checkModel == null)
                    {
                        errorMessage = Eagle.Resource.LanguageResource.CandidateTournament_ErrorMessage1;
                        return false;
                    }
                    else
                    {
                        //Nếu kết quả bằng Flase thì loại luôn
                        if (checkModel.Result == false)
                        {
                            errorMessage = Eagle.Resource.LanguageResource.CandidateTournament_ErrorMessage2;
                            return false;
                        }       
                    }
                }
                #endregion

                return true;
               
            }
        }

        [HttpPost]
        public ActionResult SaveModifi(CandidateTournamentViewModel model)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                CandidateTournamentRepository _repository = new CandidateTournamentRepository(db);
                //model.ProjectTournamentID == 0 => add mode
                //else => edit mode
                if (model.CandidateTournamentID == 0)
                {
                    REC_tblCandidateTournament modelAdd = new REC_tblCandidateTournament();
                    Mapper.CreateMap<CandidateTournamentViewModel, REC_tblCandidateTournament>();
                    Mapper.Map(model, modelAdd);

                    #region Cập nhật lại Candidate 
                    //Cập nhật lại candidate
                    //Nếu loại thì cập nhật ứng viên ngay =>            Candidate.Result = 0
                    //Nếu Pass vòng cuối cùng cập nhật lại ứng viên=>   Candidate.Result = 1

                    //Trường hợp đặc biệt thì 1 ứng viên nằm trong 2 phương án tuyển dụng.
                    //Nếu đã pass 1 bên thì bên kia không được cập nhật thành k pass
                    if (model.Result == false)
                    {
                        CandidateRepository candidateRepository = new CandidateRepository(db);
                        REC_tblCandidate candidateModel = candidateRepository.Find(model.CandidateID);
                        if (candidateModel.Result != 1)
                        {
                            candidateModel.Result = 0;
                            db.Entry(candidateModel).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                    else
                    {

                        if (_repository.IsLastTournament(model.ProjectTournamentID))
                        {
                            CandidateRepository candidateRepository = new CandidateRepository(db);
                            REC_tblCandidate candidateModel = candidateRepository.Find(model.CandidateID);
                            candidateModel.Result = 1;
                            db.Entry(candidateModel).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                    #endregion

                    if (_repository.Add(modelAdd, out errorMessage))
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _Error(model, errorMessage);
                    }
                }else
                {
                    //Update
                    REC_tblCandidateTournament modelAdd = new REC_tblCandidateTournament();
                    Mapper.CreateMap<CandidateTournamentViewModel, REC_tblCandidateTournament>();
                    Mapper.Map(model, modelAdd);
                    if (_repository.Edit(modelAdd, out errorMessage))
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _Error(model, errorMessage);
                    }
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var modelError in errors)
                {
                    errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
                }
                return _Error(model, errorMessage);
            }
        }

        public ActionResult _PreviousRoundInfo(int? CandidateID, int? ProjectTournamentID)
        {
            CandidateTournamentRepository _repository = new CandidateTournamentRepository(db);
            List<PreviousRoundViewModel> listModel = _repository.GetPreviousRound(CandidateID, ProjectTournamentID);
            return PartialView("../REC/CandidateTournament/Edit/_PreviousRoundInfo", listModel);
        }

        private void CreateViewBagForModifi(CandidateTournamentViewModel model)
        {
            /*ViewBag KQ*/
            Dictionary<bool?, string> ret = new Dictionary<bool?, string>();
            ret.Add(true, Eagle.Resource.LanguageResource.ProjectResult_1);
            ret.Add(false, Eagle.Resource.LanguageResource.ProjectResult_2);
            ViewBag.Result = new SelectList(ret, "Key", "Value", model.Result);
            /*ViewBag Rank*/
            ViewBag.Rank = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; ;

            //Nếu mà null
            if (model == null || model.CandidateTournamentID == 0)
            {
                ProjectTournamentRepository _projectTournamentRepository = new ProjectTournamentRepository(db);
                REC_tblProjectTournament modelProjectTournament = _projectTournamentRepository.Find(model.ProjectTournamentID);

                List<REC_tblInterviewCriteria> list = new List<REC_tblInterviewCriteria>();
                if (model != null)
                {
                    list = modelProjectTournament.REC_tblInterviewCriteria.ToList();
                }

                foreach (var item in list)
                {
                    model.REC_tblCandidateTournament_InterviewCriteria.Add(new REC_tblCandidateTournament_InterviewCriteria()
                    {
                        CandidateTournamentID = 0,
                        InterviewCriteriaID = item.InterviewCriteriaID,
                        Note = "",
                        Rank = 0,
                        REC_tblInterviewCriteria = new REC_tblInterviewCriteria() { Name = item.Name }
                    });
                }

            }
        }
        public ActionResult _Error(CandidateTournamentViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new CandidateTournamentViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            //CreateViewBag(model.LSCompanyID, model.PlanID);
            //CreateSourecesViewBag(sourceSelected);

            return PartialView("../REC/CandidateTournament/Edit/_ModifiCandidateTournament", model);
        }
        #endregion

        public JsonResult GetTournaments(int ProjectID)
        {
            ProjectTournamentRepository _projectTournamentRepository = new ProjectTournamentRepository(db);
            Dictionary<int, string> rtList = _projectTournamentRepository.GetTournamentByProjectID(ProjectID);

            var retData = rtList.Select(m => new SelectListItem()
            {
                Text =  m.Value,
                Value = m.Key.ToString()
            });
            return Json(retData, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetInterviewer(int ProjectTournamentID)
        {
            ProjectTournamentRepository _projectTournamentRepository = new ProjectTournamentRepository(db);
            InterviewCalendarInfoViewModel model = _projectTournamentRepository.GetInterviewer(ProjectTournamentID);

            return Json(model, JsonRequestBehavior.AllowGet);

        }


        #region Candidate Infomation
        public ActionResult _CandidateInfomation(int? CandidateID)
        { 
            #region lấy thông tin Nguyện vọng
            WorkingExpectationRepository _expectationRepository = new WorkingExpectationRepository(db);
            WorkingExpectationViewModel expectationModel = _expectationRepository.FindEdit(CandidateID);
            ViewBag.ExpectationPosition = expectationModel.LSPositionName;
            if (expectationModel.ExpectationSalary != null)
            {
                ViewBag.ExpectationSalary = expectationModel.ExpectationSalary.Value.ToString("#,##0") + " " + expectationModel.LSCurrencyName;
            }
            #endregion
            CandidateRepository _repository = new CandidateRepository(db);
            CandidateViewModel candidate = _repository.FindEdit(CandidateID, LanguageId);
            return PartialView("../REC/CandidateTournament/Edit/_CandidateInfomation", candidate);
        }
        #endregion

        #region Working background
        //List
        public ActionResult _WorkingBackGround(int? CandidateID)
        {
            WorkingBackgroundRepository _repository = new WorkingBackgroundRepository(db);
            List<REC_tblWorkingBackground> list = _repository.GetBy(CandidateID);
            return PartialView("../REC/CandidateTournament/Edit/_WorkingBackGround", list);
        }
        //View

        public ActionResult _popupWorkingBackground(int WorkingBackgroundId)
        {
            WorkingBackgroundRepository _repository = new WorkingBackgroundRepository(db);
            REC_tblWorkingBackground modelEntity = _repository.Find(WorkingBackgroundId);
            Mapper.CreateMap<REC_tblWorkingBackground, WorkingBackgroundViewModel>();
            WorkingBackgroundViewModel model = Mapper.Map<WorkingBackgroundViewModel>(modelEntity);

            return PartialView("../REC/CandidateTournament/Edit/_popupWorkingBackground", model);
        }
        #endregion

        #region Qualification
        //List
        public ActionResult _Qualification(int? CandidateID)
        {
            CandidateQualificationRepository _repository = new CandidateQualificationRepository(db);
            List<CandidateQualificationViewModel> list = _repository.GetBy(CandidateID,LanguageId);
            return PartialView("../REC/CandidateTournament/Edit/_Qualification", list);
        }
        //View
        public ActionResult _popupQualification(int QualificationId)
        {
            CandidateQualificationRepository _repository = new CandidateQualificationRepository(db);
            CandidateQualificationViewModel model = _repository.FindEdit(QualificationId);

            CreateViewBagQualification(model.LSCountryID, model.PayByCompany);
            return PartialView("../REC/CandidateTournament/Edit/_popupQualification", model);
        }

        private void CreateViewBagQualification(int? TrainingPlaceId, bool? PayByCompany)
        {
            TrainingPlaceModelList lst = new TrainingPlaceModelList(LanguageId);
            ViewBag.TrainingPlace = new SelectList(lst.TrainingPlaceList, "TrainingPlaceId", "TrainingPlaceName", TrainingPlaceId);

            Dictionary<bool, string> dic = new Dictionary<bool, string>();
            dic.Add(true, Eagle.Resource.LanguageResource.PayByCompany);
            dic.Add(false, Eagle.Resource.LanguageResource.PayByEmployee);
            ViewBag.PayByCompany = new SelectList(dic, "Key", "Value", PayByCompany);
        }
        #endregion

        #region Attach file
        //List
        public ActionResult _AttachFile(int? CandidateID)
        {
            DocumentRepository _repository = new DocumentRepository(db);
            List<DocumentViewModel> documentList = _repository.GetDocumentsBy(CandidateID, LanguageId);
            return PartialView("../REC/CandidateTournament/Edit/_AttachFile", documentList);
        }
        //View
        public ActionResult _popupAttachFile(int DocumentId)
        {
            DocumentRepository _repository = new DocumentRepository(db);
            DocumentViewModel document = _repository.FindEdit(DocumentId);
            if (document != null)
            {
                if (document.LSReferenceId != null)
                {
                    var referenceModel = db.DOC_tblDocumentReference.Find(document.LSReferenceId);
                    ViewBag.ReferenceType = referenceModel.Name;
                    ViewBag.ReferenceNo = referenceModel.LSReferenceId;
                    ViewBag.ReferenceDescription = referenceModel.Note;
                }

                string FilePath = "";
                string FileErrorMessage = "";
                _repository.GetFilePath(document.DocumentId, out FilePath, out FileErrorMessage);
                document.FilePath = FilePath;
                document.FileErrorMessage = FileErrorMessage;
            }
            return PartialView("../REC/CandidateTournament/Edit/_popupAttachFile", document);
        }
        #endregion

        public ActionResult UpdateCandidate(int CandidateId, string Skill)
        {
            CandidateRepository _repository = new CandidateRepository(db);
            var model = _repository.Find(CandidateId);
            model.Skill = Skill;
            string errorMessage = "";
            _repository.Update(model, out errorMessage);

            return Content("success");
        }
    }
}
