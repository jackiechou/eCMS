using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Core;
using AutoMapper;
using System.IO;
using Eagle.Common.Utilities;
using Eagle.Repository.SYS.FileManager;
using Eagle.Common.Settings;


namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class NewCandidateController : BaseController
    {
        //
        // GET: /Admin/Candidate/
        //string errorMessage = "";
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {

                CreateViewBag();
                CandidateViewModel model = new CandidateViewModel();
                return PartialView("../Business/HumanResources/REC/NewCandidate/InputAreasPartial", model);
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }

                CreateViewBag();
                CandidateViewModel model = new CandidateViewModel();
                return View("../Business/HumanResources/REC/NewCandidate/Create", model);
            }
        }

        [HttpPost]
        public ActionResult Create(CandidateViewModel model, HttpPostedFileBase FileUpload, int mode)
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

                REC_tblCandidate modeladd = new REC_tblCandidate();
                Mapper.CreateMap<CandidateViewModel,REC_tblCandidate>();
                modeladd = Mapper.Map<REC_tblCandidate>(model);               

                #region //Upload File

                Eagle.Core.ApplicationFile fileModel = new Eagle.Core.ApplicationFile() { };
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

                            modeladd.FileId = fileModel.FileId;
                        }

                    }
                    catch
                    {
                    }
                }

                #endregion

                if (_repository.Add(modeladd,out errorMessage))
                {
                    if (mode == 2)
                    {
                        CandidatetmpViewModel cadModel = new CandidatetmpViewModel() { 
                            CandidatetmpID = modeladd.CandidateID,
                            CandidatetmpCode = modeladd.CandidateCode,
                            DOBtmp = modeladd.DOB,
                            ApplyDatetmp = modeladd.ApplyDate,
                            FirstNametmp = modeladd.FirstName,
                            LastNametmp = modeladd.LastName,
                            Result = modeladd.Result,
                            FullNametmp = modeladd.LastName + " " + modeladd.FirstName,
                            IDNotmp = modeladd.IDNo,
                            Gender = modeladd.Gender
                        };

                        Session["CandidateSelected"] = cadModel;
                    }
                    return Content("success");
                }
                else
                {

                    return _Error(model, errorMessage, true);
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _Error(model, errorMessage, true);
        }

        private ActionResult _Error(CandidateViewModel model, string ErrorMessage, bool EditMode)
        {
            if (model == null)
            {
                model = new CandidateViewModel();
            }

            CreateViewBag(model.Gender, model.LSMaritalID, model.LSCompanyID);

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            ViewBag.PasswordVis = !EditMode;

            return PartialView("../Business/HumanResources/REC/NewCandidate/InputAreasPartial", model);
        }
        public void CreateViewBag(int? Gender = null, int? LSMaritalID = null, int? LSCompanyID = null, int? LSLevel1ID = null, int? LSLevel2ID = null)
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
    }
}
