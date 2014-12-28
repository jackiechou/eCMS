using Eagle.Repository.SYS.Session;
using Eagle.Core;
using Eagle.Repository;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using System.Data.Entity.Core.Objects;


namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class TransferCandidateController : BaseController
    {
        //
        // GET: /Admin/TransferCandidate/

        public ActionResult Index(string mode = "search", TransferCandidateViewModel modelSearch = null, int? CandidateID = null)
        {

            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }

            if (mode == "edit")
            {
                return View("../REC/TransferCandidate/Edit/Edit");
            }
            else
            {
                if (modelSearch == null)
                {
                    modelSearch = new TransferCandidateViewModel() { Status = 1 };
                }
                else
                {
                    if (modelSearch.Status == null)
                    {
                        modelSearch.Status = 1;
                    }
                }
                ViewBag.ModelSearch = modelSearch;
                return View("../REC/TransferCandidate/Index");
            }
        }

        [SessionExpiration]
        public ActionResult _Create(TransferCandidateViewModel model)
        {
            if (model == null)
            {
                model = new TransferCandidateViewModel() { Status = 1 };
            }
            
            CreateViewBag(model.Gender, model.Status);
            return PartialView("../REC/TransferCandidate/_Create", model);
        }

        [SessionExpiration]
        public ActionResult _Edit(int CandidateID, TransferCandidateViewModel TransferCandidate)
        {
            CandidateRepository _repository = new CandidateRepository(db);
            TransferEditViewModel model = _repository.FindForTransfer(CandidateID);
            ViewBag.TransferCandidate = TransferCandidate;
            if (model != null)
            {
                //Result tập hợp thông tin ứng viên này tham gia đạt tại vòng cao nhất (SEQ) ở phương án tuyển dụng đề ra (ProjectID)
                var result = db.REC_spCheckCandidate(CandidateID).ToList();
                if (result != null && result.Count > 0)
                {
                    //Lọc để lấy ra những vị trí mà ứng viên này đạt ở vòng cuối cùng
                    List<REC_spCheckCandidate_Result> tmpList = new List<REC_spCheckCandidate_Result>();
                    CandidateTournamentRepository _candidateTournamentRepository = new CandidateTournamentRepository(db);
                    int LastRound = 0;
                    foreach (var item in result)
                    {
                        LastRound = _candidateTournamentRepository.FindSEQLastRound(item.ProjectID);
                        if (item.SEQ == LastRound)
                        {
                            tmpList.Add(item);
                        }
                    }

                    //Lấy dòng đầu tiên (có giá trị LSCompany và LSPosition) gán giá trị vào model
                    foreach (var item in tmpList)
                    {
                        if (item.LSCompanyID != null && item.LSPositionID != null)
                        {
                            model.LSCompanyID = item.LSCompanyID;
                            model.LSPositionID = item.LSPositionID;

                            string LSPositionName = db.LS_tblPosition.Where(p => p.LSPositionID == item.LSPositionID).Select(p => (LanguageId == 124 ? p.Name : p.VNName)).FirstOrDefault();
                            string LSCompanyName = db.LS_tblCompany.Where(p => p.LSCompanyID == item.LSCompanyID).Select(p =>  (LanguageId == 124 ? p.Name : p.VNName)).FirstOrDefault();

                            model.LSPositionName = LSPositionName;
                            model.LSCompanyName = LSCompanyName;
                        }
                    }
                }
            }            
      
            model.EmployeeCode = EmployeeRepository.GenerateEmployeeCode(10);
            return PartialView("../REC/TransferCandidate/Edit/_Edit", model);
        }
       
        [HttpPost]
        public ActionResult Transfer(TransferEditViewModel model)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                CandidateRepository _repository = new CandidateRepository(db);
                var candidateModel = _repository.Find(model.CandidateID);

                if (candidateModel != null)
                {
                    //Kiểm tra nếu đã "Chưa đạt (0)" hoặc là "nhân viên (2)" hoặc đã "bị từ chối (3)" hoặc "Chưa đánh giá(4)" thì không được chuyển.
                    //=> kq = false
                    //Còn lại thì nếu ứng viên "đạt (1)" hoặc "Ứng viên tiềm năng (5)" thì được chuyển thành nhân viên
                    //=> kq = true

                    bool kq = true;
                    switch (candidateModel.Result)
                    {
                        //Chưa đạt
                        case 0:
                            errorMessage = string.Format(Eagle.Resource.LanguageResource.TransferErrorMessage,
                                                        Eagle.Resource.LanguageResource.Result_0);
                            kq = false;
                            break;
                        //case 2:
                        //    errorMessage = string.Format(Eagle.Resource.LanguageResource.TransferErrorMessage,
                        //                                Eagle.Resource.LanguageResource.Result_2);
                        //    kq = false;
                        //    break;
                        //Ứng viên bị từ chối
                        case 3:
                            errorMessage = string.Format(Eagle.Resource.LanguageResource.TransferErrorMessage,
                                                        Eagle.Resource.LanguageResource.Result_3);
                            kq = false;
                            break;
                        //Chưa xét tuyển
                        case 4:
                            errorMessage = string.Format(Eagle.Resource.LanguageResource.TransferErrorMessage,
                                                        Eagle.Resource.LanguageResource.Result_4);
                            kq = false;
                            break;
                        //Đã chuyển thành nhân viên
                        case 6:
                            errorMessage = string.Format(Eagle.Resource.LanguageResource.TransferErrorMessage,
                                                        Eagle.Resource.LanguageResource.Result_6);
                            kq = false;
                            break;
                    }
                    if (kq)
                    {
                        //Result tập hợp thông tin ứng viên này tham gia đạt tại vòng cao nhất (SEQ) ở phương án tuyển dụng đề ra (ProjectID)
                        var result = db.REC_spCheckCandidate(model.CandidateID).ToList();

                        //Lấy thông tin dữ liệu ProjectID,DemandID,PlanID,isAdhoc nếu có

                        var data = result.Where(p => p.LSCompanyID == model.LSCompanyID && p.LSPositionID == model.LSPositionID).FirstOrDefault();
                        int? DemandID = null;
                        int? PlanID = null;
                        if (data != null)
                        {
                            DemandID = data.DemandID;
                            PlanID = data.PlanID;
                        }
                        bool check = true;
                        //Kiểm tra LScompanyID

                        //Kiểm tra trùng EmployeeCode
                        if (db.HR_tblEmp.Where(p => p.EmpCode == model.EmployeeCode).Select(p => p.EmpCode).FirstOrDefault() != null)
                        {
                            errorMessage = string.Format(Eagle.Resource.LanguageResource.Tmp0IsExists,
                                                        Eagle.Resource.LanguageResource.EmpCode);
                            check = false;
                        }
                        if (check)
                        {
                            ObjectParameter resultParam = new ObjectParameter( "Result", typeof(int));
                            db.REC_spTransferCandidateToEmployee(model.CandidateID, model.EmployeeCode, model.JoinDate, model.LSCompanyID, model.LSPositionID, model.LineManagerID, DemandID, PlanID, resultParam);
                            int? checkResult = Convert.IsDBNull(resultParam.Value) ? null : (int?)resultParam.Value;
                            if (checkResult != null)
                            {
                                switch (checkResult.Value)
                                {
                                    case 1:
                                        return Content("success");
                                    case 2:
                                        return _Error(model, Eagle.Resource.LanguageResource.SystemError);
                                    case 3:
                                        return _Error(model, Eagle.Resource.LanguageResource.CandidateHasBeenTransformed);
                                    default:
                                        return _Error(model, Eagle.Resource.LanguageResource.SystemError);
                                        
                                }
                            }
                            else
                            {
                                return _Error(model, Eagle.Resource.LanguageResource.SystemError);
                            }
                        }
                        else
                        {
                            return _Error(model, errorMessage);
                        }
                    }
                    else
                    {
                        return _Error(model, errorMessage);
                    }
                }
                else
                {
                    //Trường hợp không tìm thấy ứng viên này trong csdl
                    errorMessage = Eagle.Resource.LanguageResource.UnableToFindCandidate;
                    return _Error(model, errorMessage);
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
        public ActionResult _Error(TransferEditViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new TransferEditViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            return PartialView("../REC/TransferCandidate/Edit/_Edit", model);
        }
        public ActionResult _List(TransferCandidateViewModel model)
        {
            if (model == null)
            {
                model = new TransferCandidateViewModel() { Status = 1 };
            }
            ViewBag.Status = model.Status;
            CandidateRepository _repository = new CandidateRepository(db);
            List<CandidateResultViewModel> result = _repository.SearchForTransferCandidate(model);
            return PartialView("../REC/TransferCandidate/_List", result);
        }
        [HttpPost]
        public ActionResult Deny(int CandidateID)
        {
            string errorMessage = "";
            CandidateRepository _repository = new CandidateRepository(db);
            var candidate = _repository.Find(CandidateID);
            if (candidate != null)
            {
                //Chức năng gửi mail đã được yêu cầu bỏ
                #region
                /*
                //Nếu từ chối ứng viên tiềm năng thì không cần gửi mail
                //Do đã gửi 1 lần rồi
                bool isPotentialCandidate = (candidate.Result == 5);
                
                //Result == 3 => từ chối
                candidate.Result = 3;
                //Gửi mail
                if (_repository.Update(candidate, out errorMessage))
                {
                    if (!isPotentialCandidate)
                    {
                        //Gửi mail
                        //try
                        //{

                        //}
                        //catch (Exception ex)
                        //{

                        //}

                    }
                    return Content("success");
                }
                else
                {
                    return Content(errorMessage);
                }
                */
                #endregion
                //Result == 3 => từ chối
                candidate.Result = 3;

                if (_repository.Update(candidate, out errorMessage))
                {
                    return Content("success");
                }
                else
                {
                    return Content(errorMessage);
                }
            }
            else
            {
                return Content(Eagle.Resource.LanguageResource.CouldNotFound);
            }
            
            
        }

        [HttpPost]
        public ActionResult TransferToWaiting(int CandidateID)
        {
            string errorMessage = "";
            CandidateRepository _repository = new CandidateRepository(db);
            var candidate = _repository.Find(CandidateID);
            if (candidate != null)
            {
                candidate.Result = 2;
                if (_repository.Update(candidate, out errorMessage))
                {
                    return Content("success");
                }
                else
                {
                    return Content(errorMessage);
                }
            }
            else
            {
                return Content(Eagle.Resource.LanguageResource.CouldNotFound);
            }


        }
        [HttpPost]
        public ActionResult PotentialCandidate(int CandidateID)
        {
            string errorMessage = "";
            CandidateRepository _repository = new CandidateRepository(db);
            var candidate = _repository.Find(CandidateID);
            if (candidate != null)
            {
                candidate.Result = 5;
                if (_repository.Update(candidate, out errorMessage))
                {
                    //Gửi mail
                    //try
                    //{

                    //}
                    //catch (Exception ex)
                    //{

                    //}
                    return Content("success");
                }
                else
                {
                    return Content(errorMessage);
                }
            }
            else
            {
                return Content(Eagle.Resource.LanguageResource.CouldNotFound);
            }
        }

        public void CreateViewBag(int? Gender = null, int? Status = null)
        {
            ViewBag.Gender = new SelectList(CommonRepository.GetGenderList(), "Key", "Value", Gender);
            ViewBag.Status = new SelectList(CommonRepository.StatusForTransferCandidate(), "Key", "Value", Status);
        }

    }
}
