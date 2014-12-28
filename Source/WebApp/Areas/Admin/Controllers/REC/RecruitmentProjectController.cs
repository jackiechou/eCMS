using AutoMapper;
using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class RecruitmentProjectController : BaseController
    {
        // GET: /Admin/RecruitmentProject/
        #region Get
        public ActionResult Index(int? ProjectID,string mode)
        {
            ViewBag.Mode = mode;
            var modelEdit = new RecruitmentProjectViewModel();
            if (ProjectID != null)
            {
                RecruitmentProjectRepository _repository = new RecruitmentProjectRepository(db);
                modelEdit = _repository.FindEdit(ProjectID, LanguageId);

                ViewBag.isEditMode = true;
            }
            else
            {
                //Chỉnh sửa
                ViewBag.isEditMode = false;
            }
           
            if (Request.IsAjaxRequest())
            {
                return PartialView("../REC/RecruitmentProject/_Reset", modelEdit);
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../REC/RecruitmentProject/Index", modelEdit);
            }
        }

        public ActionResult _Create(RecruitmentProjectViewModel model, string mode)
        {
            if (model == null)
            {
                model = new RecruitmentProjectViewModel();
            }
            //Danh sách các recruitment sources
            SelectData(model.ProjectID);
            RecruitmentProjectRepository _repository = new RecruitmentProjectRepository(db);
            var rpModel = _repository.Find(model.ProjectID);
            if (rpModel != null)
            {
                #region Recruitment tournament list
                List<ProjectTournamentViewModel> ptList = (from pr in rpModel.REC_tblProjectTournament
                                                          
                                                          join rt in db.LS_tblRecruitmentTournament on pr.LSRecruitmentTournamentID equals rt.LSRecruitmentTournamentID into listTmp
                                                          from recruitmentTournament in listTmp.DefaultIfEmpty()

                                                          //join t in db.LS_tblRecruitmentType on pr.Type equals t.LSRecruitmentTypeID into list2Tmp
                                                          //from recruitmentType in list2Tmp.DefaultIfEmpty()

                                                          join e in db.HR_tblEmp on pr.EmpID equals e.EmpID into list3Tmp
                                                          from employee in list3Tmp.DefaultIfEmpty()
                                                          
                                                          select new ProjectTournamentViewModel()
                                                          {
                                                              ProjectTournamentID = pr.ProjectTournamentID,
                                                              ProjectID = pr.ProjectID,
                                                              LSRecruitmentTournamentID = pr.LSRecruitmentTournamentID,
                                                              //Type = pr.Type,
                                                              SEQ = pr.SEQ,
                                                              FromDate = pr.FromDate,
                                                              ToDate = pr.ToDate,
                                                              EmpID = pr.EmpID,
                                                              FromDateAlowNull = pr.FromDate,
                                                              ToDateAlowNull = pr.ToDate,
                                                              LSRecruitmentTournamentIDAlowNull = pr.LSRecruitmentTournamentID,
                                                              LSRecruitmentTournamentName = LanguageId == 124 ? recruitmentTournament.Name : recruitmentTournament.VNName,
                                                              //TypeAlowNull = pr.Type,
                                                              //TypeName = LanguageId == 124 ? recruitmentType.Name : recruitmentType.VNName,
                                                              strEmpName = employee.LastName + " " + employee.FirstName,
                                                              REC_tblInterviewCriteria = pr.REC_tblInterviewCriteria
                                                         }).ToList();
                foreach (var projectTournament in ptList)
                {
                    var strTmp = "";
                    var arr = projectTournament.REC_tblInterviewCriteria.Select(p => p.InterviewCriteriaID).ToList();
                    if (arr != null && arr.Count > 0)
                    {
                        foreach (var item in arr)
                        {
                            strTmp += item.ToString() + ";";
                        }
                    }
                    projectTournament.InterviewCiteriaSelected = strTmp;
                }

                ViewBag.ProjectTournaments = ptList;
                if (ptList != null && ptList.Count > 0)
                {
                    ViewBag.chkRecruitmentTournamentList = true;
                }
                else
                {
                    ViewBag.chkRecruitmentTournamentList = false;
                }
                #endregion

                #region Candidate List
                List<CandidateResultViewModel> canList = (from ca in rpModel.REC_tblCandidate
                                                          select new CandidateResultViewModel() {
                                                              CandidateCode = ca.CandidateCode,
                                                              DOB = ca.DOB,
                                                              LastName = ca.LastName,
                                                              FirstName = ca.FirstName,
                                                              ApplyDate = ca.ApplyDate,
                                                              CandidateID = ca.CandidateID
                                                          }).ToList();
                ViewBag.Candidates = canList;
                if (canList != null && canList.Count > 0)
                {
                    ViewBag.chkCandidateList = true;
                }
                else
                {
                    ViewBag.chkCandidateList = false;
                }
                #endregion

                #region Recruitment Demand
                List<RecruitmentDemandResultViewModel> demandList = (from p in rpModel.REC_tblDemand
                                                                     join emp in db.HR_tblEmp on p.EmpID equals emp.EmpID into listTmp
                                                                     from employee in listTmp.DefaultIfEmpty()

                                                                     join com in db.LS_tblCompany on p.LSCompanyID equals com.LSCompanyID into list2Tmp
                                                                     from company in list2Tmp.DefaultIfEmpty()

                                                                     join pos in db.LS_tblPosition on p.LSPositionID equals pos.LSPositionID into list3Tmp
                                                                     from position in list3Tmp.DefaultIfEmpty()
                                                                     
                                                                     select new RecruitmentDemandResultViewModel()
                                                                      {
                                                                          DemandID = p.DemandID,
                                                                          DemandCode = p.DemandCode,
                                                                          EmpCreateName = employee.LastName + " " + employee.FirstName,
                                                                          LSCompanyName = LanguageId == 124 ? company.Name : company.VNName,
                                                                          LSPositionName = LanguageId == 124 ? position.Name : position.VNName,
                                                                          DemandQuantity = p.DemandQuantity
                                                                      }).ToList();
                ViewBag.RecruitmentDemands = demandList;
                if (demandList != null && demandList.Count > 0)
                {
                    ViewBag.chkListOfRecruitmentDemand = true;
                }
                else
                {
                    ViewBag.chkListOfRecruitmentDemand = false;
                }
                #endregion

                #region Recruitment Costs
                List<ProjectFeeViewModel> costList = (from p in rpModel.REC_tblProjectFee

                                                      join c in db.LS_tblCurrency on p.CurrencyID equals c.LSCurrencyID into listTmp
                                                      from currency in listTmp.DefaultIfEmpty()

                                                      join f in db.LS_tblRecruitmentFee on p.LSRecruitmentFeeID equals f.LSRecruitmentFeeID into list2Tmp
                                                      from fee in list2Tmp.DefaultIfEmpty()

                                                        select new ProjectFeeViewModel()
                                                        {
                                                            ProjectID = p.ProjectID,
                                                            LSRecruitmentFeeID = p.LSRecruitmentFeeID,
                                                            CurrencyID = p.CurrencyID,
                                                            LSRecruitmentFeeName = LanguageId == 124 ? fee.Name : fee.VNName,
                                                            Amount = p.Amount,
                                                            AmountAlowNull = p.Amount,
                                                            CurrencyName = LanguageId == 124 ? currency.Name : currency.VNName,
                                                            CurrencyIDAlowNull = p.CurrencyID,
                                                            LSRecruitmentFeeIDAlowNull = p.LSRecruitmentFeeID
                                                                         
                                                        }).ToList();
                ViewBag.RecruitmentCosts = costList;
                if (costList != null && costList.Count > 0)
                {
                    ViewBag.chkRecruitmentCosts = true;
                }
                else
                {
                    ViewBag.chkRecruitmentCosts = false;
                }
                #endregion
            }

            //nếu mà edit thì reset dữ liệu model
            if (mode == "copy")
            {
                model.ProjectID = 0;
                model.ProjectCode = "";
                model.ApplyDateFromAlowNull = null;
                model.ApplyDateToAlowNull = null;
                model.RecruitFromAlowNull = null;
                model.RecruitToAlowNull = null;

            }
            return PartialView("../REC/RecruitmentProject/_Create", model);
        }
      
        #endregion
        #region Recruitment Tournament
        //Tạo PartialView để popup lên
        public ActionResult _RecruitmentTournamentCreatePartial(int? ProjectTournamentID)
        {
            ProjectTournamentViewModel model = new ProjectTournamentViewModel();
            SelectDataForRecruitmentTournament();
            return PartialView("../REC/RecruitmentProject/_RecruitmentTournamentCreatePartial", model);
        }

        //Kiểm tra dữ liệu có hợp lệ không
        [HttpPost]
        public ActionResult _CheckAddRecruitmentTournamentToList(ProjectTournamentViewModel model, string mode, int? OldSEQ, List<ProjectTournamentViewModel> ProjectTournamentList)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                var check = false;
                if (model.FromDateAlowNull.Value.CompareTo(model.ToDateAlowNull.Value) > 0)
                {
                    errorMessage = string.Format(Eagle.Resource.LanguageResource.DateCompareInvalid,
                                                      Eagle.Resource.LanguageResource.ToDate,
                                                      Eagle.Resource.LanguageResource.FromDate);
                    check = true;
                }

                if (model.SEQ != OldSEQ && ProjectTournamentList != null && ProjectTournamentList.Where(p => p.SEQ == model.SEQ).FirstOrDefault() != null)
                {
                    if (errorMessage != "")
                    {
                        errorMessage += "<br />";
                    }
                    errorMessage += string.Format(Eagle.Resource.LanguageResource.Tmp0IsExists,Eagle.Resource.LanguageResource.SEQ);
                    check = true;
                }
                if (check)
                {
                    return _ProjectTournamentError(model, errorMessage);
                }
                else
                {
                    return Content("success");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var modelError in errors)
                {
                    if (errorMessage != "")
                    {
                        errorMessage += "<br />";
                    }
                    errorMessage += modelError.ErrorMessage;
                }


                return _ProjectTournamentError(model, errorMessage);
            }
        }

        //Thêm Tournament vào danh sách chính
        //Load lại danh sách này
        [HttpPost]
        public ActionResult _AddRecruitmentTournamentToList(ProjectTournamentViewModel model, string mode, int? OldSEQ, List<ProjectTournamentViewModel> ProjectTournamentList)
        {
            if (ProjectTournamentList == null)
            {
                ProjectTournamentList = new List<ProjectTournamentViewModel>();
            }

            //UpdateInterviewCiterias(InterviewCiteriaSelected, model);

            if (mode == "add")
            {
                ProjectTournamentList.Add(model);
            }
            else
            {
                //update ~ delete + add
                var modelDelete = ProjectTournamentList.Where(p => p.SEQ == OldSEQ).FirstOrDefault();
                if (modelDelete != null)
                {
                    ProjectTournamentList.Remove(modelDelete);
                }

                ProjectTournamentList.Add(model);
            }
            
            return PartialView("../REC/RecruitmentProject/_RecruitmentTournamentListPartial", ProjectTournamentList.OrderBy( p => p.SEQ));
        }

        [HttpPost]
        public ActionResult _DeleteTournament(int SEQ,List<ProjectTournamentViewModel> ProjectTournamentList)
        {
            if (ProjectTournamentList == null)
            {
                ProjectTournamentList = new List<ProjectTournamentViewModel>();
            }
            if (ProjectTournamentList.Count > 0)
            {
                var itemDelete = ProjectTournamentList.Where(p => p.SEQ == SEQ).FirstOrDefault();
                if (itemDelete != null)
                {
                    ProjectTournamentList.Remove(itemDelete);
                }
            }
            return PartialView("../REC/RecruitmentProject/_RecruitmentTournamentListPartial", ProjectTournamentList);
        }

        [HttpPost]
        public ActionResult _EditTournament(int SEQ, List<ProjectTournamentViewModel> ProjectTournamentList)
        {
            ProjectTournamentViewModel modelEdit = ProjectTournamentList.Where(p => p.SEQ == SEQ).FirstOrDefault();

            if (modelEdit != null)
            {
                SelectDataForRecruitmentTournament(modelEdit.InterviewCiteriaSelected);
                return PartialView("../REC/RecruitmentProject/_RecruitmentTournamentCreatePartial", modelEdit);
            }
            else
            {
                return Content("CouldNotFound");
            }
        }

        public ActionResult _RecruitmentTournamentListPartial(List<ProjectTournamentViewModel> lst)
        {
            if (lst == null)
            {
                lst = new List<ProjectTournamentViewModel>();    
            }

            return PartialView("../REC/RecruitmentProject/_RecruitmentTournamentListPartial", lst.OrderBy(p => p.SEQ));
        }
        #endregion
        #region Candidate
        /*Danh sách các Candidate được add vào project*/
        public ActionResult _CandidateListPartial(List<CandidateResultViewModel> lst)
        {
            if (lst == null)
            {
                lst = new List<CandidateResultViewModel>();
            }
            return PartialView("../REC/RecruitmentProject/_CandidateListPartial", lst);
        }

        /*Khung nhập dữ liệu tìm kiếm*/
        public ActionResult _CandidateCreatePartial()
        {
            CandidateSearchViewModel model = new CandidateSearchViewModel();
            CreateCandidateViewBag();
            return PartialView("../REC/RecruitmentProject/_CandidateCreatePartial", model);
        }
        /*Nhấn nút search candidate*/
        public ActionResult _CandidateCreateSearchResultPartial(CandidateSearchViewModel model, string CandidateSelectedList)
        {
            List<int> CandidateIDSelectedList = new List<int>();
            if (CandidateSelectedList != null)
            {
                string[] strTmp = CandidateSelectedList.Split(';');
                int tmp;
                foreach (var item in strTmp)
                {
                    try
                    {
                        tmp = Convert.ToInt32(item);
                        CandidateIDSelectedList.Add(tmp);
                    }
                    catch { }
                }
            }



            CandidateRepository _repository = new CandidateRepository(db);
            List<CandidateResultViewModel> result = _repository.Search(model, CandidateIDSelectedList);
            return PartialView("../REC/RecruitmentProject/_CandidateCreateSearchResultPartial", result);
        }
        
        /*Thêm vào danh sách ứng viên chính*/
        [HttpPost]
        public ActionResult _AddCandidateToList(string CandidateListAdd, List<CandidateResultViewModel> CandidateList)
        {
            
            if (CandidateList == null)
            {
                CandidateList = new List<CandidateResultViewModel>();
            }
            List<int> CandidateIDListAdd = new List<int>();
            if (CandidateListAdd != null)
            {
                string[] strTmp = CandidateListAdd.Split(';');
                int tmp;
                foreach (var item in strTmp)
                {
                    try
                    {
                        tmp = Convert.ToInt32(item);
                        if (!CandidateIDListAdd.Contains(tmp))
                        {
                            CandidateIDListAdd.Add(tmp);
                        }
                    }
                    catch { }
                }
            }

            CandidateRepository _repository = new CandidateRepository(db);
            List<CandidateResultViewModel> modelAdds = _repository.GetByListID(CandidateIDListAdd);

            CandidateList.AddRange(modelAdds);

            return PartialView("../REC/RecruitmentProject/_CandidateListPartial", CandidateList);
        }

        
        [HttpPost]
        public ActionResult _DeleteCandidateList(string CandidateSelectedList, List<CandidateResultViewModel> CandidateList)
        {
            
            if (CandidateList == null)
            {
                CandidateList = new List<CandidateResultViewModel>();
            }
            List<int> CandidateIDListAdd = new List<int>();
            if (CandidateSelectedList != null)
            {
                string[] strTmp = CandidateSelectedList.Split(';');
                int tmp;
                foreach (var item in strTmp)
                {
                    try
                    {
                        tmp = Convert.ToInt32(item);
                        CandidateIDListAdd.Add(tmp);
                    }
                    catch { }
                }
            }
            //xóa
            foreach (var item in CandidateIDListAdd)
            {
                var modelDelete = CandidateList.FirstOrDefault(p => p.CandidateID == item);
                if (modelDelete != null)
                {
                    CandidateList.Remove(modelDelete);   
                }
            }

            return PartialView("../REC/RecruitmentProject/_CandidateListPartial", CandidateList);
        }


        private void CreateCandidateViewBag(int? Result = null, int? Gender = null)
        {
            ViewBag.Result = new SelectList(CommonRepository.GetCadidateResult(), "Key", "Value", Result);

            ViewBag.Gender = new SelectList(CommonRepository.GetGenderList(), "Key", "Value", Gender);
        }


        #endregion
        #region List Of Recruitment Demand
        public ActionResult _ListOfRecruitmentDemandPartial(List<RecruitmentDemandResultViewModel> lst)
        {
            if (lst == null)
            {
                lst = new List<RecruitmentDemandResultViewModel>();
            }
            return PartialView("../REC/RecruitmentProject/_ListOfRecruitmentDemandPartial", lst);
        }
        /*Khung nhập dữ liệu tìm kiếm*/
        public ActionResult _DemandCreatePartial()
        {
            RecruitmentDemandResultViewModel model = new RecruitmentDemandResultViewModel();
            model.Year = DateTime.Now.Year;
            return PartialView("../REC/RecruitmentProject/_RecruitmentDemandCreatePartial", model);
        }
        /*Nhấn nút search candidate*/
        public ActionResult _DemandCreateSearchResultPartial(int? Year, int? LSCompanyID, int? LSPositionID, string DemandSelectedList)
        {
            var ModuleID = 8;
            List<int> DemandIDSelectedList = new List<int>();
            if (DemandSelectedList != null)
            {
                string[] strTmp = DemandSelectedList.Split(';');
                int tmp;
                foreach (var item in strTmp)
                {
                    try
                    {
                        tmp = Convert.ToInt32(item);
                        DemandIDSelectedList.Add(tmp);
                    }
                    catch { }
                }
            }

            DemandRepository _repository = new DemandRepository(db);
            List<RecruitmentDemandResultViewModel> List = _repository.Search(Year, LSCompanyID, LSPositionID, CurrentEmpId, ModuleID, CurrentAcc.UserName, LanguageId, DemandIDSelectedList);

            return PartialView("../REC/RecruitmentProject/_RecruitmentDemandCreateSearchResultPartial", List);
        }
      
         [HttpPost]
        public ActionResult _AddDemandToList(string DemandListAdd, List<RecruitmentDemandResultViewModel> DemandList)
        {

            if (DemandList == null)
            {
                DemandList = new List<RecruitmentDemandResultViewModel>();
            }
            List<int> DemandIDListAdd = new List<int>();
            if (DemandListAdd != null)
            {
                string[] strTmp = DemandListAdd.Split(';');
                int tmp;
                foreach (var item in strTmp)
                {
                    try
                    {
                        tmp = Convert.ToInt32(item);
                        if (!DemandIDListAdd.Contains(tmp))
                        {
                            DemandIDListAdd.Add(tmp);
                        }
                    }
                    catch { }
                }
            }

            DemandRepository _repository = new DemandRepository(db);
            List<RecruitmentDemandResultViewModel> modelAdds = _repository.GetByListID(DemandIDListAdd, LanguageId);

            DemandList.AddRange(modelAdds);

            return PartialView("../REC/RecruitmentProject/_ListOfRecruitmentDemandPartial", DemandList);
        }
               
        [HttpPost]
         public ActionResult _DeleteDemandList(string DemandSelectedList, List<RecruitmentDemandResultViewModel> DemandList)
        {

            if (DemandList == null)
            {
                DemandList = new List<RecruitmentDemandResultViewModel>();
            }
            List<int> DemandIDListAdd = new List<int>();
            if (DemandSelectedList != null)
            {
                string[] strTmp = DemandSelectedList.Split(';');
                int tmp;
                foreach (var item in strTmp)
                {
                    try
                    {
                        tmp = Convert.ToInt32(item);
                        DemandIDListAdd.Add(tmp);
                    }
                    catch { }
                }
            }
            //xóa
            foreach (var item in DemandIDListAdd)
            {
                var modelDelete = DemandList.FirstOrDefault(p => p.DemandID == item);
                if (modelDelete != null)
                {
                    DemandList.Remove(modelDelete);   
                }
            }

            return PartialView("../REC/RecruitmentProject/_ListOfRecruitmentDemandPartial", DemandList);
        }

        #endregion
        #region Fee
        /*Khung nhập để thêm mới chi phí của phương án tuyển dụng*/
        public ActionResult _FeeCreatePartial()
        {
            ProjectFeeViewModel model = new ProjectFeeViewModel();
            return PartialView("../REC/RecruitmentProject/_FeeCreatePartial", model);
        }
        /*Danh sách chi phí*/
        public ActionResult _FeeListPartial(List<ProjectFeeViewModel> lst)
        {
            if (lst == null)
            {
                lst = new List<ProjectFeeViewModel>();
            }
            return PartialView("../REC/RecruitmentProject/_FeeListPartial", lst);
        }

        [HttpPost]
        public ActionResult _CheckFeeToList(ProjectFeeViewModel model, string mode, int? OldLSRecruitmentFeeID, List<ProjectFeeViewModel> ProjectFeeList)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                var check = false;
                //Kiểm tra trùng LSRecruitmentFeeID
                if (model.LSRecruitmentFeeIDAlowNull.Value != OldLSRecruitmentFeeID && ProjectFeeList != null && ProjectFeeList.Where(p => p.LSRecruitmentFeeID == model.LSRecruitmentFeeIDAlowNull.Value).FirstOrDefault() != null)
                {
                    if (errorMessage != "")
                    {
                        errorMessage += "<br />";
                    }
                    errorMessage += string.Format(Eagle.Resource.LanguageResource.Tmp0IsExists, Eagle.Resource.LanguageResource.RecruitmentFee);
                    check = true;
                }
                //Kiểm tra tiền phải lớn hơn 0
                if (model.AmountAlowNull.Value <= 0)
                {
                    if (errorMessage != "")
                    {
                        errorMessage += "<br />";
                    }
                    errorMessage += string.Format(Eagle.Resource.LanguageResource.ErrorInputLessThan,
                                                Eagle.Resource.LanguageResource.Amount, "0");
                    check = true;
                }

                if (check)
                {
                    return _ProjectFeeError(model, errorMessage);
                }
                else
                {
                    return Content("success");
                }
            }
            else
            {
                ViewBag.OldLSRecruitmentFeeID = OldLSRecruitmentFeeID;

                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var modelError in errors)
                {
                    if (errorMessage != "")
                    {
                        errorMessage += "<br />";
                    }
                    errorMessage += modelError.ErrorMessage;
                }

                return _ProjectFeeError(model, errorMessage);
            }
        }

        [HttpPost]
        public ActionResult _AddFeeToList(ProjectFeeViewModel model, string mode, int? OldLSRecruitmentFeeID, List<ProjectFeeViewModel> ProjectFeeList)
        {
            if (ProjectFeeList == null)
            {
                ProjectFeeList = new List<ProjectFeeViewModel>();
            }
            //model.LSRecruitmentFeeID = model.LSRecruitmentFeeIDAlowNull.Value;
            //model.CurrencyID = model.LSRecruitmentFeeIDAlowNull.Value;
            if (mode == "add")
            {

                ProjectFeeList.Add(model);
            }
            else
            {
                //update ~ delete + add
                var modelDelete = ProjectFeeList.Where(p => p.LSRecruitmentFeeID == OldLSRecruitmentFeeID).FirstOrDefault();
                if (modelDelete != null)
                {
                    ProjectFeeList.Remove(modelDelete);
                }

                ProjectFeeList.Add(model);
            }

            return PartialView("../REC/RecruitmentProject/_FeeListPartial", ProjectFeeList);
        }

        [HttpPost]
        public ActionResult _EditFee(int LSRecruitmentFeeID, List<ProjectFeeViewModel> ProjectFeeList)
        {
            var modelEdit = ProjectFeeList.Where(p => p.LSRecruitmentFeeID == LSRecruitmentFeeID).FirstOrDefault();

            if (modelEdit != null)
            {
                return PartialView("../REC/RecruitmentProject/_FeeCreatePartial", modelEdit);
            }
            else
            {
                return Content("CouldNotFound");
            }
        }

        [HttpPost]
        public ActionResult _DeleteFee(int LSRecruitmentFeeID, List<ProjectFeeViewModel> ProjectFeeList)
        {
            if (ProjectFeeList == null)
            {
                ProjectFeeList = new List<ProjectFeeViewModel>();
            }
            if (ProjectFeeList.Count > 0)
            {
                var itemDelete = ProjectFeeList.Where(p => p.LSRecruitmentFeeID == LSRecruitmentFeeID).FirstOrDefault();
                if (itemDelete != null)
                {
                    ProjectFeeList.Remove(itemDelete);
                }
            }
            return PartialView("../REC/RecruitmentProject/_FeeListPartial", ProjectFeeList);
        }



        private ActionResult _ProjectFeeError(ProjectFeeViewModel model, string ErrorMessage)
        {

            if (model == null)
            {
                model = new ProjectFeeViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            return PartialView("../REC/RecruitmentProject/_FeeCreateFormInnerPartial", model);
        }
        
        #endregion
        #region Post 

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(RecruitmentProjectViewModel model, 
            bool chkRecruitmentTournamentList, bool chkCandidateList, bool chkListOfRecruitmentDemand, bool chkRecruitmentCosts, 
            string[] sourceSelected, 
            List<ProjectTournamentViewModel> ProjectTournamentList,
            List<CandidateResultViewModel> CandidateList,
            List<RecruitmentDemandResultViewModel> DemandList,
            List<ProjectFeeViewModel> ProjectFeeList
            )
        {
            //Nếu model.ProjectId: == 0 => add; != 0 => edit
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";

            if (acc != null)
            {
                string template = Eagle.Resource.LanguageResource.htmlComment;
                RecruitmentProjectRepository _repository = new RecruitmentProjectRepository(db);
                if (ModelState.IsValid)
                {
                    #region check validation
                    bool check = false;
                    if (acc.EmpId == null)
                    {
                        errorMessage = Eagle.Resource.LanguageResource.SystemAccountNotWorking;
                        check = true;
                    }
                    //Check "nộp đơn tới ngày" phải sau thời gian "nộp đơn từ ngày"
                    if (model.ApplyDateFromAlowNull.Value.CompareTo(model.ApplyDateToAlowNull.Value) > 0)
                    {
                        if (errorMessage != "")
                        {
                            errorMessage += "<br />";
                        }
                        errorMessage += string.Format(Eagle.Resource.LanguageResource.DateCompareInvalid,
                                                   Eagle.Resource.LanguageResource.ApplyDateTo,
                                                   Eagle.Resource.LanguageResource.ApplyDateFrom);
                        check = true;
                    }

                    //Check "phỏng vấn tới ngày" phải sau thời gian "phỏng vấn từ ngày"
                    if (model.RecruitFromAlowNull.Value.CompareTo(model.RecruitToAlowNull.Value) > 0)
                    {
                        if (errorMessage != "")
                        {
                            errorMessage += "<br />";
                        }
                        errorMessage += string.Format(Eagle.Resource.LanguageResource.DateCompareInvalid,
                                                   Eagle.Resource.LanguageResource.RecruitFrom,
                                                   Eagle.Resource.LanguageResource.RecruitTo);
                        check = true;
                    }
                    
                    if (check)
                    {
                        return _Error(model, errorMessage, sourceSelected, ProjectTournamentList, CandidateList, DemandList, ProjectFeeList);
                    }
                    #endregion
                    #region update data
                    //Add
                    if (model.ProjectID == 0)
                    {
                        #region Add
                        REC_tblProject modelAdd = new REC_tblProject();
                        //if (model.ProjectID != 0)
                        //{
                        //    modelAdd = _repository.Find(model.ProjectID);
                        //}
                        Mapper.CreateMap<RecruitmentProjectViewModel, REC_tblProject>();
                        modelAdd = Mapper.Map<REC_tblProject>(model);
                        modelAdd.Status = 1;
                        modelAdd.ApplyDateFrom = model.ApplyDateFromAlowNull.Value;
                        modelAdd.ApplyDateTo = model.ApplyDateToAlowNull.Value;
                        modelAdd.RecruitFrom = model.RecruitFromAlowNull.Value;
                        modelAdd.RecruitTo = model.RecruitToAlowNull.Value;


                        //Cập nhật thông tin các nguồn tuyển dụng
                        UpdateSoureces(sourceSelected, modelAdd);

                        //Cập nhật "Danh sách ứng viên"
                        if (chkCandidateList)
                        {
                            UpdateCandidates(CandidateList, modelAdd);
                        }
                        else
                        {
                            UpdateCandidates(null, modelAdd);
                        }

                        //Cập nhật "Danh sách yêu cầu tuyển dụng liên quan"
                        if (chkListOfRecruitmentDemand)
                        {
                            UpdateRecruitmentDemands(DemandList, modelAdd);
                        }
                        else
                        {
                            UpdateRecruitmentDemands(null, modelAdd);
                        }

                        //Thêm "Danh sách vòng tuyển dụng"
                        if (chkRecruitmentTournamentList)
                        {
                            AddTournamentsToProject(ProjectTournamentList, modelAdd);
                        }
                        else
                        {
                            AddTournamentsToProject(null, modelAdd);
                        }

                        //Thêm "Chi phí tuyển dụng"
                        if (chkRecruitmentCosts)
                        {
                            AddRecruitmentCostsToProject(ProjectFeeList, modelAdd);
                        }
                        else
                        {
                            modelAdd.REC_tblProjectFee = new List<REC_tblProjectFee>();
                        }

                        //Thêm mới
                        if (_repository.Add(modelAdd, out errorMessage))
                        {
                            return Content("success");
                        }
                        else
                        {
                            return _Error(model, errorMessage, sourceSelected, ProjectTournamentList, CandidateList, DemandList, ProjectFeeList);
                        }
                        #endregion
                    }
                    else
                    {
                        #region Update
                        REC_tblProject modelUpdate = _repository.Find(model.ProjectID);
                        modelUpdate.ProjectCode = model.ProjectCode;
                        modelUpdate.ApplyDateFrom = model.ApplyDateFromAlowNull.Value;
                        modelUpdate.ApplyDateTo = model.ApplyDateToAlowNull.Value;
                        modelUpdate.RecruitFrom = model.RecruitFromAlowNull.Value;
                        modelUpdate.RecruitTo = model.RecruitToAlowNull.Value;

                        modelUpdate.PlanCost = model.PlanCost;
                        modelUpdate.ActualCost = model.ActualCost;
                        modelUpdate.LSCurrencyID = model.LSCurrencyID;
                        modelUpdate.LSLocationID = model.LSLocationID;
                        modelUpdate.Content = model.Content;
                        modelUpdate.Description = model.Description;

                        //Cập nhật thông tin các nguồn tuyển dụng
                        UpdateSoureces(sourceSelected, modelUpdate);

                        //Cập nhật "Danh sách ứng viên"
                        if (chkCandidateList)
                        {
                            UpdateCandidates(CandidateList, modelUpdate);
                        }
                        else
                        {
                            UpdateCandidates(null, modelUpdate);
                        }

                        //Cập nhật "Danh sách yêu cầu tuyển dụng liên quan"
                        if (chkListOfRecruitmentDemand)
                        {
                            UpdateRecruitmentDemands(DemandList, modelUpdate);
                        }
                        else
                        {
                            UpdateRecruitmentDemands(null, modelUpdate);
                        }

                        #region Cập nhật dùng hàm khác
                        //Thêm "Danh sách vòng tuyển dụng"
                        if (chkRecruitmentTournamentList)
                        {
                            UpdateTournamentsToProject(ProjectTournamentList, modelUpdate);
                        }
                        else
                        {
                            UpdateTournamentsToProject(null, modelUpdate);
                        }

                        //Thêm "Chi phí tuyển dụng"
                        if (chkRecruitmentCosts)
                        {
                            UpdateRecruitmentCostsToProject(ProjectFeeList, modelUpdate);
                        }
                        else
                        {
                            UpdateRecruitmentCostsToProject(null, modelUpdate);
                        }

                        #endregion

                        //Cập nhật
                        if (_repository.Update(modelUpdate, out errorMessage))
                        {
                            return Content("success");
                        }
                        else
                        {
                            return _Error(model, errorMessage, sourceSelected, ProjectTournamentList, CandidateList, DemandList, ProjectFeeList);
                        }
                        #endregion
                    }
                    #endregion
                }
            }
            else
            {
                errorMessage = Eagle.Resource.LanguageResource.TimeOutError;
                return _Error(model, errorMessage, sourceSelected, ProjectTournamentList, CandidateList, DemandList, ProjectFeeList);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                if (errorMessage != "")
                {
                    errorMessage += "<br />";
                }
                errorMessage +=  modelError.ErrorMessage;
            }
            return _Error(model, errorMessage, sourceSelected, ProjectTournamentList, CandidateList, DemandList, ProjectFeeList);
        }

        private void UpdateTournamentsToProject(List<ProjectTournamentViewModel> ptList, REC_tblProject modelUpdate)
        {    

            //Xóa hết nếu post lên từ view không có Tournament nào
            if (ptList == null || ptList.Count == 0)
            {
                if (modelUpdate.REC_tblProjectTournament != null && modelUpdate.REC_tblProjectTournament.Count > 0)
                {

                    List<REC_tblProjectTournament> deleteItems = new List<REC_tblProjectTournament>();

                    foreach (var item in modelUpdate.REC_tblProjectTournament)
                    {
                        deleteItems.Add(item);
                    }

                    foreach (var item in deleteItems)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
            }
            else
            {
                List<REC_tblProjectTournament> ProjectTournamentList = new List<REC_tblProjectTournament>();
                Mapper.CreateMap<ProjectTournamentViewModel, REC_tblProjectTournament>();
                foreach (var item in ptList)
                {
                    REC_tblProjectTournament modelCheck = new REC_tblProjectTournament();
                    Mapper.Map(item, modelCheck);

                    UpdateInterviewCiterias(item.InterviewCiteriaSelected, modelCheck);

                    ProjectTournamentList.Add(modelCheck);
                }


                //Cập nhật deleteItems
                List<REC_tblProjectTournament> deleteItems = new List<REC_tblProjectTournament>();

                if (modelUpdate.REC_tblProjectTournament != null && modelUpdate.REC_tblProjectTournament.Count > 0)
                {
                    //lập tất cả các phần tử trong csld
                    foreach (var item in modelUpdate.REC_tblProjectTournament)
                    {
                        //Nếu không có trong list thì xóa
                        //Có thì cập nhật
                        var modelCheck = ProjectTournamentList.Where(p => p.ProjectTournamentID == item.ProjectTournamentID).FirstOrDefault();
                        if (modelCheck == null)
                        {
                            deleteItems.Add(item);
                        }
                        else
                        {
                            if (item.REC_tblInterviewCriteria == null || item.REC_tblInterviewCriteria.Count == 0)
                            {
                                item.REC_tblInterviewCriteria = modelCheck.REC_tblInterviewCriteria;
                            }
                            else
                            {
                                item.REC_tblInterviewCriteria = new List<REC_tblInterviewCriteria>();
                                item.REC_tblInterviewCriteria = modelCheck.REC_tblInterviewCriteria;
                            }
                            item.LSRecruitmentTournamentID = modelCheck.LSRecruitmentTournamentID;
                            item.SEQ = modelCheck.SEQ;
                            item.FromDate = modelCheck.FromDate;
                            item.ToDate = modelCheck.ToDate;
                            item.EmpID = modelCheck.EmpID;
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                }
                foreach (var item in deleteItems)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }

                //Nếu chưa có trong csdl thì thêm
                foreach (var item in ProjectTournamentList)
                {
                    if (item.ProjectTournamentID == 0)
                    {

                        db.Entry(item).State = System.Data.Entity.EntityState.Added;
                    }
                }
 
            }
        }

        private void UpdateRecruitmentCostsToProject(List<ProjectFeeViewModel> ptList, REC_tblProject modelUpdate)
        {
            //Xóa hết nếu post lên từ view không có Tournament nào
            if (ptList == null || ptList.Count == 0)
            {
                if (modelUpdate.REC_tblProjectFee != null && modelUpdate.REC_tblProjectFee.Count > 0)
                {

                    List<REC_tblProjectFee> deleteItems = new List<REC_tblProjectFee>();

                    foreach (var item in modelUpdate.REC_tblProjectFee)
                    {
                        deleteItems.Add(item);
                    }

                    foreach (var item in deleteItems)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
            }
            else
            {

                List<REC_tblProjectFee> modelList = new List<REC_tblProjectFee>();
                Mapper.CreateMap<ProjectFeeViewModel, REC_tblProjectFee>();
                foreach (var item in ptList)
                {
                    REC_tblProjectFee modelCheck = new REC_tblProjectFee();
                    Mapper.Map(item, modelCheck);
                    modelList.Add(modelCheck);
                }

                //Cập nhật
                List<REC_tblProjectFee> deleteItems = new List<REC_tblProjectFee>();

                //Cập nhật deleteItems
                if (modelUpdate.REC_tblProjectFee != null && modelUpdate.REC_tblProjectFee.Count > 0)
                {
                    //lập tất cả các phần tử trong csld
                    foreach (var item in modelUpdate.REC_tblProjectFee)
                    {
                        //Nếu không có trong list thì xóa
                        //Có thì cập nhật
                        var modelCheck = modelList.Where(p => p.LSRecruitmentFeeID == item.LSRecruitmentFeeID).FirstOrDefault();
                        if (modelCheck == null)
                        {
                            deleteItems.Add(item);
                        }
                        else
                        {
                            item.CurrencyID = modelCheck.CurrencyID;
                            item.Amount = modelCheck.Amount;
                            item.LSRecruitmentFeeID = modelCheck.LSRecruitmentFeeID;
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                }
                foreach (var item in deleteItems)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }

                //Nếu chưa có trong csdl thì thêm
                foreach (var item in modelList)
                {
                    if (modelUpdate.REC_tblProjectFee.Where(p=> p.LSRecruitmentFeeID == item.LSRecruitmentFeeID).FirstOrDefault() == null)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Added;
                    }
                }

            }
        }


       private ActionResult _ProjectTournamentError(ProjectTournamentViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new ProjectTournamentViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            SelectDataForRecruitmentTournament(model.InterviewCiteriaSelected);

            return PartialView("../REC/RecruitmentProject/_RecruitmentTournamentCreateFormInnerPartial", model);
        }

        private ActionResult _Error(RecruitmentProjectViewModel model, string ErrorMessage,
            string[] sourceSelected,
            List<ProjectTournamentViewModel> ProjectTournamentList,
            List<CandidateResultViewModel> CandidateList,
            List<RecruitmentDemandResultViewModel> DemandList,
            List<ProjectFeeViewModel> ProjectFeeList)
        {
            if (model == null)
            {
                model = new RecruitmentProjectViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            ViewBag.ProjectTournaments = ProjectTournamentList;
            ViewBag.Candidates = CandidateList;
            ViewBag.RecruitmentDemands = DemandList;
            ViewBag.RecruitmentCosts = ProjectFeeList;

            var selectedSourceHS = new HashSet<string>(sourceSelected);

            List<int> selectedSource = new List<int>();
            int tmp = 0;
            foreach (var item in selectedSourceHS)
            {
                if (int.TryParse(item, out tmp))
                {
                    selectedSource.Add(tmp);
                }
                
            }

            List<RecruitmentSourceSelectedViewModel> rslst = (from rs in db.LS_tblRecruitmentSource
                                                             select new RecruitmentSourceSelectedViewModel() { 
                                                              LSRecruitmentSourceID = rs.LSRecruitmentSourceID,
                                                              LSRecruitmentSourceName = rs.Name,
                                                              isSelected = selectedSource.Contains(rs.LSRecruitmentSourceID)
                                                             }).ToList();

            ViewBag.RecruitmentSources = rslst;

            return PartialView("../REC/RecruitmentProject/_Create", model);
        }
        #endregion
        #region common

        private void UpdateRecruitmentDemands(List<RecruitmentDemandResultViewModel> DemandList, REC_tblProject modelAdd)
        {
            // nếu post sourceSelected lên == null
            if (DemandList == null)
            {
                modelAdd.REC_tblDemand = new List<REC_tblDemand>();
                return;
            }
            // có dữ liệu
            //selectedPagesHS : danh sách ID được chọn từ giao diện
            var selectedPagesHS = DemandList.Select(p => p.DemandID).ToList();
            //roles: là tất cả các ID trong database (đã liên kết nhiều nhiều)
            var roles = new HashSet<int>
                (modelAdd.REC_tblDemand.Select(c => c.DemandID));

            //for each tất cả các phần tử
            foreach (var source in db.REC_tblDemand)
            {
                // nếu mà được chọn có trong database
                if (selectedPagesHS.Contains(source.DemandID))
                {
                    //chưa có thì add
                    if (!roles.Contains(source.DemandID))
                    {
                        modelAdd.REC_tblDemand.Add(source);
                    }
                }
                else
                {
                    //không có this xóa
                    if (roles.Contains(source.DemandID))
                    {
                        modelAdd.REC_tblDemand.Remove(source);
                    }
                }
            }
        }

        private void UpdateCandidates(List<CandidateResultViewModel> CandidateList, REC_tblProject modelAdd)
        {
            // nếu post sourceSelected lên == null
            if (CandidateList == null)
            {
                modelAdd.REC_tblCandidate = new List<REC_tblCandidate>();
                return;
            }
            // có dữ liệu
            //selectedPagesHS : danh sách ID được chọn từ giao diện
            var selectedPagesHS = CandidateList.Select(p => p.CandidateID).ToList();
            //roles: là tất cả các ID trong database (đã liên kết nhiều nhiều)
            var roles = new HashSet<int>
                (modelAdd.REC_tblCandidate.Select(c => c.CandidateID));

            //for each tất cả các phần tử
            foreach (var source in db.REC_tblCandidate)
            {
                // nếu mà được chọn có trong database
                if (selectedPagesHS.Contains(source.CandidateID))
                {
                    //chưa có thì add
                    if (!roles.Contains(source.CandidateID))
                    {
                        modelAdd.REC_tblCandidate.Add(source);
                    }
                }
                else
                {
                    //không có this xóa
                    if (roles.Contains(source.CandidateID))
                    {
                        modelAdd.REC_tblCandidate.Remove(source);
                    }
                }
            }
        }

        private void UpdateSoureces(string[] sourceSelected, REC_tblProject modelAdd)
        {
            // nếu post sourceSelected lên == null
            if (sourceSelected == null)
            {
                modelAdd.LS_tblRecruitmentSource = new List<LS_tblRecruitmentSource>();
                return;
            }
            // có dữ liệu
            //selectedPagesHS : danh sách ID được chọn từ giao diện
            var selectedPagesHS = new HashSet<string>(sourceSelected);
            //roles: là tất cả các ID trong database (đã liên kết nhiều nhiều)
            var roles = new HashSet<int>
                (modelAdd.LS_tblRecruitmentSource.Select(c => c.LSRecruitmentSourceID));

            //for each tất cả các phần tử
            foreach (var source in db.LS_tblRecruitmentSource)
            {
                // nếu mà được chọn có trong database
                if (selectedPagesHS.Contains(source.LSRecruitmentSourceID.ToString()))
                {
                    //chưa có thì add
                    if (!roles.Contains(source.LSRecruitmentSourceID))
                    {
                        modelAdd.LS_tblRecruitmentSource.Add(source);
                    }
                }
                else
                {
                    //không có this xóa
                    if (roles.Contains(source.LSRecruitmentSourceID))
                    {
                        modelAdd.LS_tblRecruitmentSource.Remove(source);
                    }
                }
            }
        }
        private void UpdateInterviewCiterias(string interviewCiteriaSelected, REC_tblProjectTournament modelAdd)
        {
            // nếu post interviewCiteriaSelected lên == null
            if (interviewCiteriaSelected == null)
            {
                modelAdd.REC_tblInterviewCriteria = new List<REC_tblInterviewCriteria>();
                return;
            }
            // có dữ liệu
            List<int> interviewCiteriaIDSelectedList = new List<int>();
            if (interviewCiteriaSelected != null)
            {
                string[] strTmp = interviewCiteriaSelected.Split(';');
                int tmp;
                foreach (var item in strTmp)
                {
                    try
                    {
                        tmp = Convert.ToInt32(item);
                        interviewCiteriaIDSelectedList.Add(tmp);
                    }
                    catch { }
                }
            }
            
            //roles: là tất cả các ID trong database (đã liên kết nhiều nhiều)
            var roles = new HashSet<int>
                (modelAdd.REC_tblInterviewCriteria.Select(c => c.InterviewCriteriaID));

            //for each tất cả các phần tử
            foreach (var source in db.REC_tblInterviewCriteria)
            {
                // nếu mà được chọn có trong database
                if (interviewCiteriaIDSelectedList.Contains(source.InterviewCriteriaID))
                {
                    //chưa có thì add
                    if (!roles.Contains(source.InterviewCriteriaID))
                    {
                        modelAdd.REC_tblInterviewCriteria.Add(source);
                    }
                }
                else
                {
                    //không có this xóa
                    if (roles.Contains(source.InterviewCriteriaID))
                    {
                        modelAdd.REC_tblInterviewCriteria.Remove(source);
                    }
                }
            }
        }

        private void AddTournamentsToProject(List<ProjectTournamentViewModel> ProjectTournamentList, REC_tblProject modelAdd)
        {
            List<REC_tblProjectTournament> tournamentList = ConvertTournamentViewToTournamentEntity(ProjectTournamentList);
            // nếu post sourceSelected lên == null
            if (tournamentList == null || tournamentList.Count == 0)
            {
                modelAdd.REC_tblProjectTournament = new List<REC_tblProjectTournament>();
            }
            else
            {
                foreach (var item in tournamentList)
                {
                    modelAdd.REC_tblProjectTournament.Add(item);
                }
            }
        }

        private void AddRecruitmentCostsToProject(List<ProjectFeeViewModel> ProjectFeeList, REC_tblProject modelAdd)
        {
            List<REC_tblProjectFee> tournamentList = ConvertProjectFeeViewModelToProjectFeeEntity(ProjectFeeList);
            // nếu post sourceSelected lên == null
            if (tournamentList == null || tournamentList.Count == 0)
            {
                modelAdd.REC_tblProjectFee = new List<REC_tblProjectFee>();
            }
            else
            {
                foreach (var item in tournamentList)
                {
                    modelAdd.REC_tblProjectFee.Add(item);
                }
            }
        }

        private List<REC_tblProjectFee> ConvertProjectFeeViewModelToProjectFeeEntity(List<ProjectFeeViewModel> ProjectFeeList)
        {
            List<REC_tblProjectFee> ret = new List<REC_tblProjectFee>();
            if (ProjectFeeList != null)
            {
                foreach (var item in ProjectFeeList)
                {
                    REC_tblProjectFee modelAdd = new REC_tblProjectFee();
                    modelAdd.Amount = item.Amount;
                    modelAdd.CurrencyID = item.CurrencyID;
                    modelAdd.LSRecruitmentFeeID = item.LSRecruitmentFeeID;
                    ret.Add(modelAdd);
                }
            }
            return ret;
        }

        //Hàm convert từ Danh sách ProjectTournamentViewModel sang REC_tblProjectTournament
        private List<REC_tblProjectTournament> ConvertTournamentViewToTournamentEntity(List<ProjectTournamentViewModel> ProjectTournamentList)
        {
            List<REC_tblProjectTournament> ret = new List<REC_tblProjectTournament>();
            if (ProjectTournamentList != null)
            {
                foreach (var item in ProjectTournamentList)
                {
                    
                    REC_tblProjectTournament modelAdd = new REC_tblProjectTournament();
                    modelAdd.LSRecruitmentTournamentID = item.LSRecruitmentTournamentID;
                    modelAdd.SEQ = item.SEQ;
                    modelAdd.FromDate = (item.FromDateAlowNull == null ? new DateTime() : item.FromDateAlowNull.Value);
                    modelAdd.ToDate = (item.ToDateAlowNull == null ? new DateTime() : item.ToDateAlowNull.Value);
                    modelAdd.EmpID = item.EmpID;

                    UpdateInterviewCiterias(item.InterviewCiteriaSelected, modelAdd);

                    ret.Add(modelAdd);
                }
            }
            return ret;
        }

        private void SelectData(int? ProjectId = null)
        {
            REC_tblProject model = null;
            if (ProjectId != null)
            {
                RecruitmentProjectRepository _repository = new RecruitmentProjectRepository(db);
                model = _repository.Find(ProjectId);
            }
            List<LS_tblRecruitmentSource> allRecruitmentSource = db.LS_tblRecruitmentSource.OrderBy(p => p.Rank).ToList();
            var viewModel = new List<RecruitmentSourceSelectedViewModel>();
            if (model != null)
            {
                var RecruitmentSources = new HashSet<int>(model.LS_tblRecruitmentSource.Select(c => c.LSRecruitmentSourceID));
                foreach (var r in allRecruitmentSource)
                {
                    viewModel.Add(new RecruitmentSourceSelectedViewModel
                    {
                        LSRecruitmentSourceID = r.LSRecruitmentSourceID,
                        LSRecruitmentSourceName = r.Name,
                        isSelected = RecruitmentSources.Contains(r.LSRecruitmentSourceID)
                    });
                }
            }
            else
            {
                foreach (var r in allRecruitmentSource)
                {
                    viewModel.Add(new RecruitmentSourceSelectedViewModel
                    {
                        LSRecruitmentSourceID = r.LSRecruitmentSourceID,
                        LSRecruitmentSourceName = r.Name,
                        isSelected = false
                    });
                }
            }

            ViewBag.RecruitmentSources = viewModel;
        }



        private void SelectDataForRecruitmentTournament(string InterviewCiteriaSelected)
        {

            List<int> InterviewCiteriaIDList = Eagle.Common.Extensions.CollectionUtils.ConvertStringToListInt(InterviewCiteriaSelected, ';');
                      

            List<REC_tblInterviewCriteria> allInterviewCriteria = db.REC_tblInterviewCriteria.OrderBy(p => p.Rank).ToList();
            var viewModel = new List<SelectedViewModel>();
           

            foreach (var r in allInterviewCriteria)
            {
                viewModel.Add(new SelectedViewModel
                {
                    ID = r.InterviewCriteriaID,
                    Name = r.Name,
                    Type = r.InterviewCriteriaTypeID,
                    isSelected = InterviewCiteriaIDList.Contains(r.InterviewCriteriaID)
                });
            }

            
            List<int?> typelst = viewModel.GroupBy(x => x.Type).Select(g => g.First().Type).ToList();
            ViewBag.InterviewCiteriaTypes = db.REC_tblInterviewCriteriaType
                                              .Where(p => typelst.Contains(p.InterviewCriteriaTypeID))
                                              .ToList();
            ViewBag.InterviewCiterias = viewModel;
        }

        private void SelectDataForRecruitmentTournament(int? ProjectTournamentID = null)
        {
            REC_tblProjectTournament model = null;
            if (ProjectTournamentID != null)
            {
                model = db.REC_tblProjectTournament.Find(ProjectTournamentID);
            }
            List<REC_tblInterviewCriteria> allInterviewCriteria = db.REC_tblInterviewCriteria.OrderBy(p => p.Rank).ToList();
            var viewModel = new List<SelectedViewModel>();
            if (model != null)
            {
                var ProjectTournaments = new HashSet<int>(model.REC_tblInterviewCriteria.Select(c => c.InterviewCriteriaID));
                foreach (var r in allInterviewCriteria)
                {
                    viewModel.Add(new SelectedViewModel
                    {
                        ID = r.InterviewCriteriaID,
                        Name = r.Name,
                        Type = r.InterviewCriteriaTypeID,
                        isSelected = ProjectTournaments.Contains(r.InterviewCriteriaID)
                    });
                }
            }
            else
            {
                foreach (var r in allInterviewCriteria)
                {
                    viewModel.Add(new SelectedViewModel
                    {
                        ID = r.InterviewCriteriaID,
                        Name = r.Name,
                        Type = r.InterviewCriteriaTypeID,
                        isSelected = false
                    });
                }
            }
            List<int?> typelst = viewModel.GroupBy(x => x.Type).Select(g => g.First().Type).ToList();
            ViewBag.InterviewCiteriaTypes = db.REC_tblInterviewCriteriaType
                                              .Where(p => typelst.Contains(p.InterviewCriteriaTypeID))
                                              .ToList();
            ViewBag.InterviewCiterias = viewModel;
        }
        private void CreateSourecesViewBag(string[] sourceSelected)
        {
            var RecruitmentSource = new List<RecruitmentSourceSelectedViewModel>();
            var selectedPagesHS = new HashSet<string>();
            if (sourceSelected != null)
            {
                selectedPagesHS = new HashSet<string>(sourceSelected);
            }

            foreach (var source in db.LS_tblRecruitmentSource)
            {
                RecruitmentSourceSelectedViewModel model = new RecruitmentSourceSelectedViewModel()
                {
                    LSRecruitmentSourceID = source.LSRecruitmentSourceID,
                    LSRecruitmentSourceName = source.Name,
                    isSelected = selectedPagesHS.Contains(source.LSRecruitmentSourceID.ToString())
                };
                RecruitmentSource.Add(model);
            }

            ViewBag.RecruitmentSources = RecruitmentSource;
        }
        #endregion
    }
}
