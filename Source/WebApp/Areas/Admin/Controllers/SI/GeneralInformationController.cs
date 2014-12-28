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
    public class GeneralInformationController : BaseController
    {
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../SI/GeneralInformation/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?returnUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../SI/GeneralInformation/Index");
            }
        }
        // load danh sach grid nhom
       
        public ActionResult _Create()
        {
            InsuranceInformationViewModel model = new InsuranceInformationViewModel();
            EmployeeViewModel Emp = (EmployeeViewModel)Session["Emp"];
            if (Emp != null && Emp.EmpID != 0)
            {
                InsuranceInformationRepository _repository = new InsuranceInformationRepository(db);
                model = _repository.FindEdit(Emp.EmpID, LanguageId);
                if (model == null)
                {
                    model = new InsuranceInformationViewModel() { EmpID = Emp.EmpID };
                }
            }
            
            
            return PartialView("../SI/GeneralInformation/_Create", model);
        }


        [HttpPost]
        public ActionResult Save(InsuranceInformationViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = "";

            if (acc != null)
            {
                InsuranceInformationRepository _repository = new InsuranceInformationRepository(db);
                if (ModelState.IsValid)
                {
                    SI_tblInsuranceInformation modelUpdate = new SI_tblInsuranceInformation();
                    Mapper.CreateMap<InsuranceInformationViewModel, SI_tblInsuranceInformation>();
                    Mapper.Map(model, modelUpdate);
                    //Gán kết quả ngày
                    modelUpdate.JoinDateAtCompany = model.JoinDateAtCompanyAlowNull.Value;

                    if (_repository.Add(modelUpdate, out errorMessage))
                    {
                        return Content("success");
                    }
                    else
                    {
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
            else
            {
                errorMessage = Eagle.Resource.LanguageResource.TimeOutError;
                return _Error(model, errorMessage);
            }
        }

        public ActionResult _Error(InsuranceInformationViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new InsuranceInformationViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            return PartialView("../SI/GeneralInformation/_Create", model);
        }



        #region Previous Record
        public ActionResult _Tab2PreviousRecordPartial()
        {
            PreviousRecordRepository _repository = new PreviousRecordRepository(db);
            List<SI_tblPreviousRecord> list = _repository.GetBy(CurrentEmpId, LanguageId);
            return PartialView("../SI/GeneralInformation/_Tab2PreviousRecordPartial", list);
        }
        public ActionResult _popupTab2PreviousRecordPartial()
        {
            PreviousRecordViewModel model = new PreviousRecordViewModel();
            model.NoOfMonth = 1;
            return PartialView("../SI/GeneralInformation/_popupTab2PreviousRecordPartial", model);
        }
        public ActionResult _EditPreviousRecord(int id)
        {
            PreviousRecordRepository _repository = new PreviousRecordRepository(db);
            SI_tblPreviousRecord modelEntity = _repository.Find(id);
            Mapper.CreateMap<SI_tblPreviousRecord, PreviousRecordViewModel>();
            PreviousRecordViewModel model = Mapper.Map<PreviousRecordViewModel>(modelEntity);

            return PartialView("../SI/GeneralInformation/_popupTab2PreviousRecordPartial", model);
        }
        [HttpPost]
        public ActionResult _DeletePreviousRecord(int id)
        {
            PreviousRecordRepository _repository = new PreviousRecordRepository(db);
            string errorMessage = "";
            if (_repository.Delete(id, out errorMessage))
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }
        }

        [HttpPost]
        public ActionResult UpdatePreviousRecord(PreviousRecordViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = "";
            if (acc != null)
            {
                PreviousRecordRepository _repository = new PreviousRecordRepository(db);
                if (ModelState.IsValid)
                {
                    SI_tblPreviousRecord modelUpdate = new SI_tblPreviousRecord();
                    Mapper.CreateMap<PreviousRecordViewModel, SI_tblPreviousRecord>();
                    modelUpdate = Mapper.Map<SI_tblPreviousRecord>(model);

                    bool bResult = _repository.Update(modelUpdate, out errorMessage);
                    if (bResult)
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
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            errorMessage += "<br />";
                        }
                        errorMessage += modelError.ErrorMessage;
                    }
                    return Content(errorMessage);
                }
            }
            else
            {
                return Content(Eagle.Resource.LanguageResource.TimeOutError);
            }

        }
        [HttpPost]
        public ActionResult AddPreviousRecord(PreviousRecordViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = "";
            if (model.EmpID == 0)
            {
                Content(Eagle.Resource.LanguageResource.PleaseChooseEmployee);
            }
            if (acc != null)
            {
                if (ModelState.IsValid)
                {
                    if (model.FromMonth.CompareTo(model.ToMonth) > 0)
                    {
                        errorMessage = string.Format(Eagle.Resource.LanguageResource.DateCompareInvalid, Eagle.Resource.LanguageResource.ToMonth, Eagle.Resource.LanguageResource.FromMonth);
                        return Content(errorMessage);
                    }

                    PreviousRecordRepository _repository = new PreviousRecordRepository(db);

                    SI_tblPreviousRecord modelAdd = new SI_tblPreviousRecord();
                    Mapper.CreateMap<PreviousRecordViewModel, SI_tblPreviousRecord>();
                    modelAdd = Mapper.Map<SI_tblPreviousRecord>(model);

                    bool bResult = _repository.Add(modelAdd, out errorMessage);
                    if (bResult)
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
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            errorMessage += "<br />";
                        }
                        errorMessage += modelError.ErrorMessage;
                    }
                    return Content(errorMessage);
                }
            }
            else
            {
                return Content(Eagle.Resource.LanguageResource.TimeOutError);
            }

        }
        #endregion

        #region HI Card
        public ActionResult _Tab3HICardPartial()
        {
            HICardRepository _repository = new HICardRepository(db);
            List<HICardViewModel> list = _repository.GetBy(CurrentEmpId, LanguageId);
            return PartialView("../SI/GeneralInformation/_Tab3HICardPartial", list);
        }
        public ActionResult _popupTab3HICardPartial()
        {
            
            HICardViewModel model = new HICardViewModel();
            ViewBag.LanguageId = LanguageId;
            /*Tạo ViewBag bệnh viện*/
            InsuranceInformationRepository _repository = new InsuranceInformationRepository(db);
            var modelA = _repository.FindEdit(CurrentEmpId);
            if (modelA != null)
            {
                model.LSHospitalID = modelA.LSHospitalIDNew;
                model.LSHospitalName = modelA.LSHospitalIDNewName;
                model.oldHospitalName = modelA.LSHospitalIDNewName;
            }

            return PartialView("../SI/GeneralInformation/_popupTab3HICardPartial", model);
        }
        public ActionResult _EditHICard(int id)
        {
            HICardRepository _repository = new HICardRepository(db);
            HICardViewModel model = _repository.FindEdit(id,LanguageId);
            ViewBag.LanguageId = LanguageId;
            return PartialView("../SI/GeneralInformation/_popupTab3HICardPartial", model);
        }
        [HttpPost]
        public ActionResult _DeleteHICard(int id)
        {
            HICardRepository _repository = new HICardRepository(db);
            string errorMessage = "";
            if (_repository.Delete(id, out errorMessage))
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }
        }

        [HttpPost]
        public ActionResult UpdateHICard(HICardViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = "";
            if (acc != null)
            {
                HICardRepository _repository = new HICardRepository(db);
                if (ModelState.IsValid)
                {
                    SI_tblHICard modelUpdate = new SI_tblHICard();
                    Mapper.CreateMap<HICardViewModel, SI_tblHICard>();
                    modelUpdate = Mapper.Map<SI_tblHICard>(model);

                    bool bResult = _repository.Update(modelUpdate, out errorMessage);
                    if (bResult)
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
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            errorMessage += "<br />";
                        }
                        errorMessage += modelError.ErrorMessage;
                    }
                    return Content(errorMessage);
                }
            }
            else
            {
                return Content(Eagle.Resource.LanguageResource.TimeOutError);
            }

        }
        [HttpPost]
        public ActionResult AddHICard(HICardViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = "";
            if (model.EmpID == 0)
            {
                Content(Eagle.Resource.LanguageResource.PleaseChooseEmployee);
            }
            if (acc != null)
            {
                if (ModelState.IsValid)
                {
                    model.FromDate = model.FromDateAllowNull.Value;
                    model.ToDate = model.ToDateAllowNull.Value;
                    if (model.FromDateAllowNull.Value.CompareTo(model.ToDateAllowNull.Value) > 0)
                    {
                        errorMessage = string.Format(Eagle.Resource.LanguageResource.DateCompareInvalid, Eagle.Resource.LanguageResource.ToMonth, Eagle.Resource.LanguageResource.FromMonth);
                        return Content(errorMessage);
                    }

                    HICardRepository _repository = new HICardRepository(db);

                    SI_tblHICard modelAdd = new SI_tblHICard();
                    Mapper.CreateMap<HICardViewModel, SI_tblHICard>();
                    modelAdd = Mapper.Map<SI_tblHICard>(model);

                    bool bResult = _repository.Add(modelAdd, out errorMessage);
                    if (bResult)
                    {
                            //Nếu thay đổi thông bệnh viện
                            if (modelAdd.ChangePlace == true)
                            {
                                try
                                {
                                    //Cập nhật lại thông tin bệnh viện mới nhất
                                    InsuranceInformationRepository _InsuranceInformationRepository = new InsuranceInformationRepository(db);
                                    var InsuranceInformationModel = _InsuranceInformationRepository.FindByEmpId(CurrentEmpId);
                                    if (InsuranceInformationModel != null)
                                    {
                                        InsuranceInformationModel.LSHospitalIDNew = modelAdd.LSHospitalID;
                                        _InsuranceInformationRepository.Update(InsuranceInformationModel);
                                    }

                                }
                                catch { }
                                try
                                {
                                    //Ghi log vào table SI_tblChangingInsuranceInformationLog
                                    var changeType = db.SI_tblChangingInsuranceInformationType.Where(p => p.Code == "HOSPITAL").FirstOrDefault();
                                    if (changeType != null)
                                    {
                                        string error = string.Empty;

                                        ChangingInsuranceInformationLogRepository _CIILRepository = new ChangingInsuranceInformationLogRepository(db);
                                        SI_tblChangingInsuranceInformationLog log = new SI_tblChangingInsuranceInformationLog();
                                        log.EmpID = CurrentEmpId;
                                        log.ChangeTypeID = changeType.ChangeTypeID;
                                        log.ChangeTime = DateTime.Now;
                                        log.Old = model.oldHospitalName;
                                        log.New = model.LSHospitalName;
                                        
                                        _CIILRepository.Add(log, out error);
                                    }
                                }
                                catch
                                {
                                }
                         
	                        }
                        return Content("success");
                    }
                    else
                    {
                        return Content(errorMessage);
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            errorMessage += "<br />";
                        }
                        errorMessage += modelError.ErrorMessage;
                    }
                    return Content(errorMessage);
                }
            }
            else
            {
                return Content(Eagle.Resource.LanguageResource.TimeOutError);
            }

        }
        #endregion

        #region _Tab4InsNoticeData
        public ActionResult _Tab4InsNoticeData()
        {
            //List<GeneralInformation_InsNoticeDataViewModel> list = new List<GeneralInformation_InsNoticeDataViewModel>();
            var result = from i in db.SI_tblInsNoticeData
                         join t in db.LS_tblNguonBaoBH on i.SourceID equals t.NguonBaoBHID
                         join tt in db.LS_tblNguonBaoBH_Type on t.NguonBaoBH_TypeID equals tt.NguonBaoBH_TypeID
                         where i.EmpID == CurrentEmpId
                         orderby i.AriseDate descending
                         select new GeneralInformation_InsNoticeDataViewModel()
                         {
                             AriseDate = i.AriseDate,
                             NewSalary = i.NewSalary,
                             OldSalary = i.OldSalary,
                             NewSalaryCoef = i.NewSalaryCoef,
                             OldSalaryCoef = i.OldSalaryCoef,
                             Percent = i.Percent,
                             ToDate = i.ToDate,
                             TypeName = LanguageId == 10001? tt.Name: tt.VNName
 
                         };
            return PartialView("../SI/GeneralInformation/_Tab4InsNoticeData", result.ToList());
        }
        #endregion
    }
}
