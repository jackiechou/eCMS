using AutoMapper;
using Eagle.Common.Utilities;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.HR;
using Eagle.Core;
using Eagle.Common.Settings;
using Eagle.Repository.SYS.FileManager;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class CandidateInformationController : BaseController
    {
        //
        // GET: /Admin/CandidateInformation/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../Business/HumanResources/REC/CandidateInformation/Index");
        }
        public ActionResult _Create()
        {            
            CandidateViewModel model = new CandidateViewModel();
            if (Session["CandidateSelected"] != null)
            {
                CandidatetmpViewModel cadModel = (CandidatetmpViewModel)Session["CandidateSelected"];
                CandidateRepository _repository = new CandidateRepository(db);
                model = _repository.FindEdit(cadModel.CandidatetmpID, LanguageId);
            }

            CreateViewBag(model.Gender, model.LSMaritalID, model.LSCompanyID);
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Business/HumanResources/REC/CandidateInformation/InputAreasPartial", model);
            }
            else
            {
                return PartialView("../Business/HumanResources/REC/CandidateInformation/_Create", model);
            }
        }
        public ActionResult Tab4ExpectationPartial()
        {
            WorkingExpectationViewModel model = new WorkingExpectationViewModel();
            if (Session["CandidateSelected"] != null)
            {
                CandidatetmpViewModel cadModel = (CandidatetmpViewModel)Session["CandidateSelected"];
                WorkingExpectationRepository _repository = new WorkingExpectationRepository(db);
                model = _repository.FindEdit(cadModel.CandidatetmpID);   
            }
            return PartialView("../Business/HumanResources/REC/CandidateInformation/Tab4ExpectationPartial", model);
        }
        #region Working Bacground
        public ActionResult Tab5WorkingBackgroundPartial()
        {
            CandidatetmpViewModel cadModel = new CandidatetmpViewModel();
            if (Session["CandidateSelected"] != null)
            {
                cadModel = (CandidatetmpViewModel)Session["CandidateSelected"];
            }
            WorkingBackgroundRepository _repository = new WorkingBackgroundRepository(db);
            List<REC_tblWorkingBackground> list = _repository.GetBy(cadModel.CandidatetmpID);
            return PartialView("../Business/HumanResources/REC/CandidateInformation/Tab5WorkingBackgroundPartial", list);
        }
        public ActionResult _CreateWorkingBackground()
        {
            WorkingBackgroundViewModel model = new WorkingBackgroundViewModel();
            return PartialView("../Business/HumanResources/REC/CandidateInformation/_popupTab5WorkingBackgroundPartial", model);
        }
        public ActionResult _EditWorkingBackground(int id)
        {
            WorkingBackgroundRepository _repository = new WorkingBackgroundRepository(db);
            REC_tblWorkingBackground modelEntity = _repository.Find(id);
            Mapper.CreateMap<REC_tblWorkingBackground, WorkingBackgroundViewModel>();
            WorkingBackgroundViewModel model = Mapper.Map<WorkingBackgroundViewModel>(modelEntity);

            return PartialView("../Business/HumanResources/REC/CandidateInformation/_popupTab5WorkingBackgroundPartial", model);
        }
        [HttpPost]
        public ActionResult _DeleteWorkingBackground(int id)
        {
            WorkingBackgroundRepository _repository = new WorkingBackgroundRepository(db);
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
        public ActionResult UpdateWorkingBackground(WorkingBackgroundViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                WorkingBackgroundRepository _repository = new WorkingBackgroundRepository(db);
                if (ModelState.IsValid)
                {
                    REC_tblWorkingBackground modelUpdate = new REC_tblWorkingBackground();
                    Mapper.CreateMap<WorkingBackgroundViewModel, REC_tblWorkingBackground>();
                    modelUpdate = Mapper.Map<REC_tblWorkingBackground>(model);

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
                        errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
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
        public ActionResult AddWorkingBackground(WorkingBackgroundViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (model.CandidateID == 0)
            {
                Content(Eagle.Resource.LanguageResource.PleaseChooseCandidate);
            }
            if (acc != null)
            {
                WorkingBackgroundRepository _repository = new WorkingBackgroundRepository(db);
               
                REC_tblWorkingBackground modelAdd = new REC_tblWorkingBackground();
                Mapper.CreateMap<WorkingBackgroundViewModel, REC_tblWorkingBackground>();
                modelAdd = Mapper.Map<REC_tblWorkingBackground>(model);

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
                return Content(Eagle.Resource.LanguageResource.TimeOutError);
            }

        }
        #endregion
        #region Qualification
        public ActionResult Tab6QualificationPartial()
        {
            CandidatetmpViewModel cadModel = new CandidatetmpViewModel();
            if (Session["CandidateSelected"] != null)
            {
                cadModel = (CandidatetmpViewModel)Session["CandidateSelected"];
            }
            CandidateQualificationRepository _repository = new CandidateQualificationRepository(db);
            List<CandidateQualificationViewModel> list = _repository.GetBy(cadModel.CandidatetmpID, LanguageId);

            return PartialView("../Business/HumanResources/REC/CandidateInformation/Tab6QualificationPartial", list);
        }

        public ActionResult _popupTab6QualificationPartial()
        {
            CandidateQualificationViewModel model = new CandidateQualificationViewModel();
            model.PayByCompany = true;
            CreateViewBagQualification(model.LSCountryID, model.PayByCompany);
            return PartialView("../Business/HumanResources/REC/CandidateInformation/_popupTab6QualificationPartial", model);
        }

        public ActionResult _EditQualification(int id)
        {
            CandidateQualificationRepository _repository = new CandidateQualificationRepository(db);
            CandidateQualificationViewModel model = _repository.FindEdit(id);

            CreateViewBagQualification(model.LSCountryID, model.PayByCompany);
            return PartialView("../Business/HumanResources/REC/CandidateInformation/_popupTab6QualificationPartial", model);
        }

        [HttpPost]
        public ActionResult _DeleteQualification(int id)
        {
            CandidateQualificationRepository _repository = new CandidateQualificationRepository(db);
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

        private void CreateViewBagQualification(int? TrainingPlaceId = null, bool? PayByCompany = true)
        {
            TrainingPlaceModelList lst = new TrainingPlaceModelList(LanguageId);
            ViewBag.TrainingPlace = new SelectList(lst.TrainingPlaceList, "TrainingPlaceId", "TrainingPlaceName", TrainingPlaceId);

            Dictionary<bool, string> dic = new Dictionary<bool, string>();
            dic.Add(true, Eagle.Resource.LanguageResource.PayByCompany);
            dic.Add(false, Eagle.Resource.LanguageResource.PayByEmployee);
            ViewBag.PayByCompany = new SelectList(dic, "Key", "Value", PayByCompany);
        }

        [HttpPost]
        public ActionResult UpdateQualification(CandidateQualificationViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                CandidateQualificationRepository _repository = new CandidateQualificationRepository(db);
                if (ModelState.IsValid)
                {
                    REC_tblQualification modelUpdate = new REC_tblQualification();
                    Mapper.CreateMap<CandidateQualificationViewModel, REC_tblQualification>();
                    modelUpdate = Mapper.Map<REC_tblQualification>(model);
                    modelUpdate.QualificationDate = model.QualificationDateAlowNull.Value;
                    modelUpdate.LSQualificationID = model.LSQualificationIDAlowNull.Value;
                    modelUpdate.Priority = model.PriorityAlowNull.Value;

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
                        errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
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
        public ActionResult AddQualification(CandidateQualificationViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (model.CandidateID == 0)
            {
                errorMessage = Eagle.Resource.LanguageResource.PleaseChooseCandidate;
                return Content(errorMessage);
            }
            if (acc != null)
            {
                CandidateQualificationRepository _repository = new CandidateQualificationRepository(db);
                if (ModelState.IsValid)
                {
                    REC_tblQualification modelAdd = new REC_tblQualification();
                    Mapper.CreateMap<CandidateQualificationViewModel, REC_tblQualification>();
                    modelAdd = Mapper.Map<REC_tblQualification>(model);
                    modelAdd.QualificationDate = model.QualificationDateAlowNull.Value;
                    modelAdd.LSQualificationID = model.LSQualificationIDAlowNull.Value;
                    modelAdd.Priority = model.PriorityAlowNull.Value;

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
                        errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
                    }
                    return Content(errorMessage);
                }
            }
            else
            {
                errorMessage = "Time out error!";
                return Content(errorMessage);
            }
           
        }
        #endregion
        #region Attach File
        //public ActionResult _CreateWorkingBackground()
        //{
        //    WorkingBackgroundViewModel model = new WorkingBackgroundViewModel();
        //    return PartialView("../Business/HumanResources/REC/CandidateInformation/_popupTab7AttachFilesPartial", model);
        //}

        public ActionResult Tab7AttachFilesPartial()
        {
            CandidatetmpViewModel cadModel = new CandidatetmpViewModel();
            if (Session["CandidateSelected"] != null)
            {
                cadModel = (CandidatetmpViewModel)Session["CandidateSelected"];
            }
            DocumentRepository _repository = new DocumentRepository(db);
            List<DocumentViewModel> documentList = _repository.GetDocumentsBy(cadModel.CandidatetmpID, LanguageId);
            return PartialView("../Business/HumanResources/REC/CandidateInformation/Tab7AttachFilesPartial", documentList);
        }

        public ActionResult _popupTab7AttachFilesPartial()
        {
            DocumentViewModel model = new DocumentViewModel();
            CreateViewBagAttachFiles(model.LSReferenceId);
            return PartialView("../Business/HumanResources/REC/CandidateInformation/_popupTab7AttachFilesPartial", model);
        }

        [HttpPost]
        public ActionResult AddAttachFiles(DocumentViewModel model, int CandidateID)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (CandidateID == 0)
            {
                errorMessage = Eagle.Resource.LanguageResource.PleaseChooseCandidate;
                return Content(errorMessage);
            }
            
            if (acc != null)
            {
                CandidateRepository _repository = new CandidateRepository(db);
                REC_tblCandidate modelUpdate = _repository.Find(CandidateID);
                if (modelUpdate != null)
                {
                    if (ModelState.IsValid)
                    {
                        DOC_tblDocument documentModel = new DOC_tblDocument();
                        Mapper.CreateMap<DocumentViewModel, DOC_tblDocument>();
                        documentModel = Mapper.Map<DOC_tblDocument>(model);


                        #region //Upload File
                        byte[] data;
                        //Nếu mà có file mới cho đăng không thì báo lỗi
                        if (model.UploadFile != null)
                        {
                            try
                            {
                                using (Stream inputStream = model.UploadFile.InputStream)
                                {
                                    MemoryStream memoryStream = inputStream as MemoryStream;
                                    if (memoryStream == null)
                                    {
                                        memoryStream = new MemoryStream();
                                        inputStream.CopyTo(memoryStream);
                                    }
                                    data = memoryStream.ToArray();
                                }
                                if (data != null)
                                {
                                    documentModel.FileNamePhysical = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + StringUtils.ConvertToNoMarkString(model.UploadFile.FileName.Substring(0, model.UploadFile.FileName.LastIndexOf('.') - 1)) + model.UploadFile.FileName.Substring(model.UploadFile.FileName.LastIndexOf('.'));
                                    documentModel.FileNameVirtual = model.UploadFile.FileName;
                                    documentModel.Extension = model.UploadFile.FileName.Substring(model.UploadFile.FileName.LastIndexOf('.'));
                                    documentModel.ContentType = model.UploadFile.ContentType;
                                    documentModel.FolderId = 20;
                                    documentModel.FileContent = data;
                                    documentModel.Size = data.Length;
                                    documentModel.CreatedByUserId = CurrentAcc.EmpId;
                                    //documentModel.LastModifiedByUserId = CurrentAcc.EmpID;
                                    documentModel.CreatedOnDate = DateTime.Now;
                                    //documentModel.LastModifiedOnDate = DateTime.Now;

                                    modelUpdate.DOC_tblDocument.Add(documentModel);
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
                                    errorMessage = Eagle.Resource.LanguageResource.SystemError;
                                    return Content(errorMessage);
                                }

                            }
                            catch(Exception ex)
                            {
                                errorMessage = ex.Message;
                                return Content(errorMessage);
                            }
                        }
                        else
                        {
                            errorMessage = string.Format(Eagle.Resource.LanguageResource.Required, Eagle.Resource.LanguageResource.UploadFile);
                            return Content(errorMessage);
                        }
                        #endregion


                    }
                    else
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        foreach (var modelError in errors)
                        {
                            errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
                        }
                        return Content(errorMessage);
                    }
                }
                else
                {

                    errorMessage = "Could not found this candidate!";
                    return Content(errorMessage);
                }
                
            }
            else
            {
                errorMessage = "Time out error!";
                return Content(errorMessage);
            }

        }

        private void CreateViewBagAttachFiles(int? LSReferenceId)
        {
            Dictionary<int, string> list = db.DOC_tblDocumentReference.Select(p => new { Name = ((LanguageId == 124) ? p.Name : p.VNName) + ((p.Used == true) ? "" : " " + Eagle.Resource.LanguageResource.NotUsed), p.LSReferenceId }).ToDictionary(p => p.LSReferenceId, p => p.Name);
            
            ViewBag.LSReferenceId = new SelectList(list, "Key", "Value", LSReferenceId);
        }

        [HttpPost]
        public ActionResult _DeleteDocument(int id)
        {
            DocumentRepository _repository = new DocumentRepository(db);
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
        public ActionResult _EditDocument(int id)
        {
            DocumentRepository _repository = new DocumentRepository(db);
            DocumentViewModel model = _repository.FindEdit(id);
            CreateViewBagAttachFiles(model.LSReferenceId);

            string FilePath = "";
            string FileErrorMessage = "";
            _repository.GetFilePath(model.DocumentId, out FilePath, out FileErrorMessage);
            model.FilePath = FilePath;
            model.FileErrorMessage = FileErrorMessage;
            return PartialView("../Business/HumanResources/REC/CandidateInformation/_popupTab7AttachFilesPartial", model);
        }
        [HttpPost]
        public ActionResult UpdateDocument(DocumentViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                DocumentRepository _repository = new DocumentRepository(db);
                if (ModelState.IsValid)
                {
                    //Cập nhật thông tin
                    DOC_tblDocument modelUpdate = _repository.Find(model.DocumentId);
                    modelUpdate.LSReferenceId = model.LSReferenceId;
                    modelUpdate.FileTitle = model.FileTitle;
                    modelUpdate.Keywords = model.Keywords;
                    modelUpdate.FileDescription = model.FileDescription;

                    modelUpdate.LastModifiedByUserId = acc.EmpId;
                    modelUpdate.LastModifiedOnDate = DateTime.Now;

                    //Cập nhật file

                    #region //Upload File
                    byte[] data;
                    //Nếu mà có file mới cho đăng không thì báo lỗi
                    if (model.UploadFile != null)
                    {
                        try
                        {
                            using (Stream inputStream = model.UploadFile.InputStream)
                            {
                                MemoryStream memoryStream = inputStream as MemoryStream;
                                if (memoryStream == null)
                                {
                                    memoryStream = new MemoryStream();
                                    inputStream.CopyTo(memoryStream);
                                }
                                data = memoryStream.ToArray();
                            }
                            if (data != null)
                            {
                                modelUpdate.FileNamePhysical = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + StringUtils.ConvertToNoMarkString(model.UploadFile.FileName.Substring(0, model.UploadFile.FileName.LastIndexOf('.') - 1)) + model.UploadFile.FileName.Substring(model.UploadFile.FileName.LastIndexOf('.'));
                                modelUpdate.FileNameVirtual = model.UploadFile.FileName;
                                modelUpdate.Extension = model.UploadFile.FileName.Substring(model.UploadFile.FileName.LastIndexOf('.'));
                                modelUpdate.ContentType = model.UploadFile.ContentType;
                                modelUpdate.FolderId = 20;
                                modelUpdate.FileContent = data;
                                modelUpdate.Size = data.Length;                                
                            }

                        }
                        catch (Exception ex)
                        {
                            errorMessage = ex.Message;
                            return Content(errorMessage);
                        }
                    }
                    #endregion

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
                        errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
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
        public ActionResult Update(CandidateViewModel model, WorkingExpectationViewModel Expectation, HttpPostedFileBase FileUpload)
        {
            FileRepository _fileRepository = new FileRepository(db);
            string errorMessage = "";
            #region // Check Image file valid
            
            if (!_fileRepository.CheckImageTypeValid(FileUpload, out errorMessage))
	        {
                ModelState.AddModelError("ImageUpload", errorMessage);
	        }
            
            #endregion
            if (ModelState.IsValid)
            {

                CandidateRepository _repository = new CandidateRepository(db);
                REC_tblCandidate updateModel = new REC_tblCandidate();
                Mapper.CreateMap<CandidateViewModel, REC_tblCandidate>();
                updateModel = Mapper.Map<REC_tblCandidate>(model);


                #region //Upload File

                var OldFile = updateModel.FileId;

                 Eagle.Core.ApplicationFile fileModel = new  Eagle.Core.ApplicationFile() { };
                byte[] data;
                if (FileUpload != null)
                {
                    try
                    {
                        using (Stream inputStream = FileUpload.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            data = memoryStream.ToArray();
                        }
                        if (data != null)
                        {
                            fileModel.FileTitle = model.LastName + " " + model.FirstName;
                            fileModel.FileName = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + StringUtils.ConvertToNoMarkString(FileUpload.FileName.Substring(0, FileUpload.FileName.LastIndexOf('.') - 1)) + FileUpload.FileName.Substring(FileUpload.FileName.LastIndexOf('.'));
                            fileModel.Extension = FileUpload.FileName.Substring(FileUpload.FileName.LastIndexOf('.'));
                            fileModel.ContentType = FileUpload.ContentType;
                            fileModel.FolderId = 18;
                            fileModel.FileContent = data;
                            fileModel.FileDescription = "";
                            fileModel.Size = data.Length;
                            fileModel.CreatedByUserId = UserId;
                            fileModel.CreatedOnDate = DateTime.Now;
                            db.Entry(fileModel).State = System.Data.Entity.EntityState.Added;

                            updateModel.FileId = fileModel.FileId;
                        }

                    }
                    catch
                    {
                    }
                }

                #endregion

                if (_repository.Update(updateModel, Expectation, out errorMessage))
                {
                    //nếu thành công
                    //Xóa file ảnh cũ nếu có sửa
                    if (updateModel.FileId != OldFile)
                    {
                        string error = "";
                        FileRepository.Delete(OldFile, out error);
                    }
                    return Content("success");
                }
                else
                {
                    return _Error(model, errorMessage);
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _Error(model, errorMessage);
            
        }

        private ActionResult _Error(CandidateViewModel model, string errorMessage)
        {
            if (model == null)
            {
                model = new CandidateViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = errorMessage;
            CreateViewBag(model.Gender, model.LSMaritalID, model.LSCompanyID);
            return PartialView("../Business/HumanResources/REC/CandidateInformation/InputAreasPartial", model);
        }

        public void CreateViewBag(int? Gender = null, int? LSMaritalID = null, int? LSCompanyID = null)
        {
            //  Gender          
            ViewBag.Gender = new SelectList(CommonRepository.GetGenderList(), "Key", "Value", Gender);

            // LSMaritalID
            LS_tblMaritalRepository _LS_tblMaritalRespository = new LS_tblMaritalRepository(db);
            List<MaritalEntity> maritalLst = _LS_tblMaritalRespository.GetListForDropDownList(LanguageId);
            if (maritalLst.Count == 0)
            {
                maritalLst.Insert(0, new MaritalEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            }
            ViewBag.LSMaritalID = new SelectList(maritalLst, "Id", "Name", LSMaritalID);           
        }
        public ActionResult AvatarPartial()
        {
            CandidateViewModel model = new CandidateViewModel();
            if (Session["CandidateSelected"] != null)
            {
                CandidatetmpViewModel cadModel = (CandidatetmpViewModel)Session["CandidateSelected"];
                CandidateRepository _repository = new CandidateRepository(db);
                model = _repository.FindEdit(cadModel.CandidatetmpID, LanguageId);
            }
            return PartialView("../Business/HumanResources/REC/CandidateInformation/AvatarPartial", model);
        }
        //public ActionResult UpdateFileIds(int Id, string FileIds)
        //{
        //    bool flag = false;
        //    string message = string.Empty;
        //    CandidateRepository _repository = new CandidateRepository(db);
        //    flag = _repository.UpdateFileIds(Id, FileIds, out message);
        //    return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        //}
    }
}
